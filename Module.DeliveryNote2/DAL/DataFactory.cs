using FrameworkSetting;
using Library;
using Library.DTO;
using Module.DeliveryNote2.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Module.DeliveryNote2.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();

        public DataFactory() { }

        private DeliveryNote2Entities CreateContext()
        {
            return new DeliveryNote2Entities(Library.Helper.CreateEntityConnectionString("DAL.DeliveryNote2Model"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DeliveryNote2Entities context = CreateContext())
                {
                    var dbItem = context.DeliveryNote.Where(o => o.DeliveryNoteID == id).FirstOrDefault();
                    context.DeliveryNote.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }

        }

        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
            int? companyID = fw_factory.GetCompanyID(userId);
            DTO.EditFormData editFormData = new DTO.EditFormData();
            editFormData.Data = new DTO.DeliveryNoteDTO();
            editFormData.Data.DeliveryNoteDetailDTOs = new List<DTO.DeliveryNoteDetailDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            editFormData.FactoryWarehouses = new List<FactoryWarehouseDTO>();
            editFormData.FactoryWarehousePallets = new List<Support.DTO.FactoryWarehousePallet>();
            try
            {
                using (DeliveryNote2Entities context = CreateContext())
                {
                    if (id > 0)
                    {
                        DeliveryNoteMng_DeliveryNote_View dbItem;
                        dbItem = context.DeliveryNoteMng_DeliveryNote_View.Include("DeliveryNoteMng_DeliveryNoteDetail_View").FirstOrDefault(o => o.DeliveryNoteID == id);
                        editFormData.Data = converter.DB2DTO_DeliveryNote(dbItem);
                    }

                    editFormData.FactoryWarehouses = AutoMapper.Mapper.Map<List<DeliveryNoteMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.DeliveryNoteMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    editFormData.FactoryWarehousePallets = support_factory.GetFactoryWarehousePallet(companyID);

                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return editFormData;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.DeliveryNote.FirstOrDefault(o => o.DeliveryNoteID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsApproved = false;
                    dbItem.ResetBy = userId;
                    dbItem.ResetDate = DateTime.Now;
                    dbItem.ApprovedBy = null;
                    dbItem.ApprovedDate = null;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.DeliveryNoteID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }

            return true;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DeliveryNoteDTO dtoDeliveryNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.DeliveryNoteDTO>();

            string emailSubject = "DELTA OVERLOAD";
            string emailBody = string.Empty;

            try
            {
                if (!dtoDeliveryNote.StatusTypeID.HasValue)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Status type is null!";
                    return false;
                }

                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (DeliveryNote2Entities context = CreateContext())
                {
                    // Check work center block material
                    var workCenter = context.DeliveryNoteMng_function_GetWorkCenter(dtoDeliveryNote.WorkOrderIDs).FirstOrDefault(o => o.WorkCenterID == dtoDeliveryNote.WorkCenterID);
                    if (workCenter != null && workCenter.IsBlocked.HasValue && workCenter.IsBlocked.Value)
                    {
                        throw new Exception("Work center " + workCenter.WorkCenterNM + " blocked. You can't update data");
                    }

                    foreach (var item in dtoDeliveryNote.DeliveryNoteDetailDTOs.Where(o => o.ProductionItemID.HasValue))
                    {
                        if (item.FromFactoryWarehouseID == null)
                        {
                            throw new Exception("missing Warehouse for: " + item.ProductionItemNM);
                        }
                    }

                    DeliveryNote dbItem = null;

                    if (id == 0)
                    {
                        dbItem = new DeliveryNote();
                        context.DeliveryNote.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DeliveryNote.Where(o => o.DeliveryNoteID == id).FirstOrDefault();

                        //CheckPermission
                        if (dbItem.CreatedBy.Value != userId)
                        {
                            throw new Exception("You can't update because you are not user who created this receipt");
                        }
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //check price status is locked                    
                        if (dtoDeliveryNote.DeliveryNoteTypeID != 1)
                        {
                            DateTime? receivingDate = dtoDeliveryNote.DeliveryNoteDate.ConvertStringToDateTime();
                            int month = receivingDate.Value.Month;
                            int? year = receivingDate.Value.Year;
                            List<int> productionItemTypeIDs = dtoDeliveryNote.DeliveryNoteDetailDTOs.Where(o => o.DeliveryNoteDetailID > 0 && o.ProductionItemTypeID.HasValue).Select(s => s.ProductionItemTypeID.Value).ToList();
                            List<int> productionItemIDs = dtoDeliveryNote.DeliveryNoteDetailDTOs.Where(o => o.DeliveryNoteDetailID < 0 && o.ProductionItemID.HasValue).Select(s => s.ProductionItemID.Value).ToList();

                            var productionItem = context.ProductionItem.Where(o => productionItemIDs.Contains(o.ProductionItemID) && o.ProductionItemTypeID.HasValue).Select(o => new { o.ProductionItemID, o.ProductionItemNM, o.ProductionItemTypeID }).ToList();
                            List<int> _productionItemTypeIDs = productionItem.Select(o => o.ProductionItemTypeID.Value).ToList();
                            productionItemTypeIDs.AddRange(_productionItemTypeIDs);

                            foreach (var productionItemTypeID in productionItemTypeIDs)
                            {
                                var productionPrice = context.ProductionPrice.Where(o => o.CompanyID == companyID && o.ProductionItemTypeID == productionItemTypeID && o.Month == month && o.Year == year).FirstOrDefault();
                                if (productionPrice != null && productionPrice.IsLocked.HasValue && productionPrice.IsLocked.Value)
                                {
                                    string productionItemNM = "";
                                    var x = dtoDeliveryNote.DeliveryNoteDetailDTOs.Where(o => o.ProductionItemTypeID == productionItemTypeID).FirstOrDefault();
                                    if (x != null)
                                    {
                                        productionItemNM = x.ProductionItemNM;
                                    }
                                    else
                                    {
                                        var y = productionItem.Where(o => o.ProductionItemTypeID.Value == productionItemTypeID).FirstOrDefault();
                                        if (y != null)
                                        {
                                            productionItemNM = y.ProductionItemNM;
                                        }
                                    }
                                    throw new Exception("The price table for " + productionItemNM + " at " + month.ToString() + "/" + year.ToString() + " has been locked. You have to unlock before delivery item");
                                }
                            }
                        }
                        //check workorder status
                        /*
                         *  WorkOrderStatusID :
                         *      1 : Open
                         *      5 : Confirm Frame
                         *      2 : Confimred
                         *      3 : Finished
                         *      4 : Cancel                     
                         */
                        List<int?> workOrderIDs = dtoDeliveryNote.DeliveryNoteDetailDTOs.Select(o => o.WorkOrderID).Distinct().ToList();
                        var wo = context.WorkOrder.Where(o => workOrderIDs.Contains(o.WorkOrderID)).Select(s => new { s.WorkOrderID, s.WorkOrderStatusID }).ToList();
                        foreach (var item in wo)
                        {
                            if (item.WorkOrderStatusID == 1)
                            {
                                throw new Exception("WorkOrder is opening you need confirm before delivery product");
                            }
                            else if (item.WorkOrderStatusID == 3)
                            {
                                throw new Exception("WorkOrder is finished you can not delivery product");
                            }
                            else if (item.WorkOrderStatusID == 4)
                            {
                                throw new Exception("WorkOrder is canceled you can not delivery product product");
                            }
                        }

                        ///ConversionFacror
                        foreach (var item in dtoDeliveryNote.DeliveryNoteDetailDTOs)
                        {
                            foreach (var uItem in item.ProductionItemUnits)
                            {
                                if (item.ProductionItemID == uItem.ProductionItemID && item.UnitID == uItem.UnitID)
                                {
                                    item.Qty = item.QtyByUnit * uItem.ConversionFactor;
                                }
                            }
                        }

                        //validate stock qnt
                        ValidateStockQnt(dtoDeliveryNote);

                        //convert dto to db
                        converter.DTO2DB_DeliveryNote(dtoDeliveryNote, ref dbItem);
                        dbItem.CompanyID = companyID;

                        if (id == 0)
                        {
                            dbItem.CreatedDate = DateTime.Now;
                            dbItem.CreatedBy = userId;
                            if (!string.IsNullOrEmpty(dtoDeliveryNote.FactorySaleOrderUD))
                            {
                                dbItem.Description = dbItem.Description + " (" + dtoDeliveryNote.FactorySaleOrderUD + ")";
                            }
                        }

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        dbItem.IsApproved = true;
                        dbItem.ApprovedBy = userId;
                        dbItem.ApprovedDate = DateTime.Now;

                        //remove orphan
                        context.DeliveryNoteDetail.Local.Where(o => o.DeliveryNote == null).ToList().ForEach(o => context.DeliveryNoteDetail.Remove(o));

                        //save data
                        context.SaveChanges();
                        //generate code
                        if (string.IsNullOrEmpty(dbItem.DeliveryNoteUD))
                        {
                            context.DeliveryNoteMng_function_GenerateDeliveryNoteUD(dbItem.DeliveryNoteID, dbItem.CompanyID, dbItem.DeliveryNoteDate.Value.Year, dbItem.DeliveryNoteDate.Value.Month);
                        }
                        //get return data
                        dtoItem = GetData(userId, dbItem.DeliveryNoteID, out notification).Data;

                        if (dtoDeliveryNote.StatusTypeID == 1)
                        {
                            var getWOCodeFromDilivery = dtoDeliveryNote.DeliveryNoteDetailDTOs;

                            foreach (var item in getWOCodeFromDilivery)
                            {

                                var getProductionItemFormWO = context.WorkOrderMng_BOM_View.Where(x => x.WorkOrderID == item.WorkOrderID && x.ProductionItemID == item.ProductionItemID).ToList();
                                foreach (var item2 in getProductionItemFormWO)
                                {
                                    if (item2.Delta > item2.WastagePercent)
                                    {
                                        emailBody = $"Please check {FrameworkSetting.Setting.FrontendUrl}/WorkOrder/Edit/{item2.WorkOrderID}";
                                        break;
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(emailBody))
                            {
                                SendNotification(emailSubject, emailBody);
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        /// <summary>
        /// get delivery note base on workOrder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filters"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.EditFormData GetData(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                int id = Convert.ToInt32(filters["id"]);
                int branchID = Convert.ToInt32(filters["branchID"]);
                string workOrderIDs = null;
                using (DeliveryNote2Entities context = CreateContext())
                {
                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        DeliveryNoteMng_DeliveryNote_View dbItem;
                        dbItem = context.DeliveryNoteMng_DeliveryNote_View.Include("DeliveryNoteMng_DeliveryNoteDetail_View").Where(o => o.CompanyID == companyID).FirstOrDefault(o => o.DeliveryNoteID == id);
                        editFormData.Data = converter.DB2DTO_DeliveryNote(dbItem);
                        foreach (var item in editFormData.Data.DeliveryNoteDetailDTOs)
                        {
                            var unitItem = context.DeliveryNoteMng_Function_ProductionItemUnitByValidDate(dbItem.DeliveryNoteDate, item.ProductionItemID);
                            item.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(unitItem.ToList()));
                        }
                    }
                    else
                    {
                        //get param data
                        if (filters["workOrderIDs"] != null)
                        {
                            workOrderIDs = filters["workOrderIDs"].ToString();
                        }
                        //initialize data
                        editFormData.Data = new DTO.DeliveryNoteDTO();
                        editFormData.Data.WorkOrderIDs = workOrderIDs;
                        editFormData.Data.DeliveryNoteDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.DeliveryNoteDetailDTOs = new List<DTO.DeliveryNoteDetailDTO>();
                        editFormData.WorkOrderList = new List<WorkOrderDTO>();
                    }
                    //get workOrderIDs
                    workOrderIDs = editFormData.Data.WorkOrderIDs;

                    //get support list
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    editFormData.FactoryWarehousePallets = support_factory.GetFactoryWarehousePallet(companyID);
                    editFormData.ProductionTeams = support_factory.GetProductionTeam(companyID);
                    editFormData.ProductionItemTypes = support_factory.GetProductionItemType();
                    editFormData.SubSuppliers = converter.DB2DTO_GetSubSuppliers(context.SupportMng_SubSupplierQuickSearch_View.ToList());
                    editFormData.TransportationServiceDTOs = AutoMapper.Mapper.Map<List<DeliveryNoteMng_TransportationService_View>, List<DTO.TransportationServiceDTO>>(context.DeliveryNoteMng_TransportationService_View.ToList());
                    editFormData.OutsourcingCostDTOs = AutoMapper.Mapper.Map<List<DeliveryNoteMng_OutsourcingCost_View>, List<DTO.OutsourcingCostDTO>>(context.DeliveryNoteMng_OutsourcingCost_View.ToList());
                    editFormData.FactoryWarehouses = AutoMapper.Mapper.Map<List<DeliveryNoteMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.DeliveryNoteMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID && o.BranchID == branchID).ToList());
                    editFormData.StatusTypes = new List<DTO.StatusTypeDTO>();

                    //get BOM & WorkCenter base on workOrder
                    if (string.IsNullOrEmpty(workOrderIDs))
                    {
                        //get all workcenter
                        editFormData.AllWorkCenters = support_factory.GetWorkCenter().Where(o => o.WorkCenterID != 10).ToList(); // Remove workcenter is finishing
                    }
                    else
                    {
                        //get workcenter base on workorder
                        var workCenters = GetWorkCenter(workOrderIDs, out notification); // Remove workcenter is finishing
                        int? workCenterID = null;
                        editFormData.WorkCenters = new List<WorkCenterDTO>();

                        foreach (var item in workCenters)
                        {
                            if (workCenterID == null || workCenterID != item.WorkCenterID)
                            {
                                workCenterID = item.WorkCenterID;
                                editFormData.WorkCenters.Add(item);
                            }
                        }

                        //get workorder info
                        List<int> woIds = workOrderIDs.Split(',').Select(Int32.Parse).ToList();
                        var wo = context.WorkOrder.Where(o => woIds.Contains(o.WorkOrderID)).ToList();
                        editFormData.WorkOrderList = AutoMapper.Mapper.Map<List<WorkOrder>, List<DTO.WorkOrderDTO>>(wo);

                        // Case list wok-order
                        editFormData.Data.StatusTypeID = 1; // From Production
                        editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 1, StatusTypeNM = "From Production" });
                    }

                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 2, StatusTypeNM = "From Return" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 3, StatusTypeNM = "From Other" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 4, StatusTypeNM = "From Outsource" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 5, StatusTypeNM = "From Sale Order" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 6, StatusTypeNM = "From Factory Order" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 7, StatusTypeNM = "From Transfer Warehouse" });

                    //get reason for other price list
                    editFormData.ReasonOtherPrices = new List<object>();
                    editFormData.ReasonOtherPrices.Add(new { ReasonOtherPriceID = 1, ReasonOtherPriceName = "A" });
                    editFormData.ReasonOtherPrices.Add(new { ReasonOtherPriceID = 2, ReasonOtherPriceName = "B" });

                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return editFormData;
            }
        }

        /// <summary>
        /// get delivery note without workOrder (warehouse)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.EditFormData GetData(int userId, int id, int branchID, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            editFormData.Data = new DTO.DeliveryNoteDTO();
            editFormData.Data.DeliveryNoteDetailDTOs = new List<DTO.DeliveryNoteDetailDTO>();
            editFormData.supportFactoryWareHouseLists = new List<DTO.SupportFactoryWareHouseList>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        editFormData.Data = converter.DB2DTO_DeliveryNote(context.DeliveryNoteMng_DeliveryNote_View.Include("DeliveryNoteMng_DeliveryNoteDetail_View").FirstOrDefault(o => o.DeliveryNoteID == id));
                    }
                    else
                    {
                        editFormData.Data.DeliveryNoteDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    // Get factory warehouse with branch and company
                    editFormData.FactoryWarehouses = AutoMapper.Mapper.Map<List<DeliveryNoteMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.DeliveryNoteMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID && o.BranchID == branchID).ToList());

                    editFormData.supportFactoryWareHouseLists = converter.DB2DTO_GetlistWareHouseList(context.DeliveryNoteMng_SupportFactoryWaseHouseList_View.ToList());
                }
                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                //editFormData.FactoryWarehouses = support_factory.GetFactoryWarehouse(companyID);
                editFormData.FactoryWarehousePallets = support_factory.GetFactoryWarehousePallet(companyID);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return editFormData;
        }

        public DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            searchFormData.supportFactoryWareHouseLists = new List<DTO.SupportFactoryWareHouseList>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                int? companyID = null;
                string deliveryNoteUD = null;
                string deliveryNoteDatest = null;
                string workCenterNM = null;
                string fromProductionTeamNM = null;
                string toProductionTeamNM = null;
                string workOrderUD = null;
                string modelUD = null;
                string modelNM = null;
                string productArticleCode = null;
                string productDescription = null;
                string description = null;
                string updatedDatest = null;
                string updatorName = null;
                int? deliveryNoteTypeID = null;
                int? factoryWarehouseID = null;
                string fromDeliveryNoteDate = null;
                string toDeliveryNoteDate = null;
                int? statusTypeID = null;
                string fromUpdatedDate = null;
                string toUpdatedDate = null;
                int? workCenterID = null;
                int? fromProductionTeamID = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("deliveryNoteUD") && !string.IsNullOrEmpty(filters["deliveryNoteUD"].ToString()))
                {
                    deliveryNoteUD = filters["deliveryNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("deliveryNoteDate") && !string.IsNullOrEmpty(filters["deliveryNoteDate"].ToString()))
                {
                    deliveryNoteDatest = filters["deliveryNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workCenterNM") && !string.IsNullOrEmpty(filters["workCenterNM"].ToString()))
                {
                    workCenterNM = filters["workCenterNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamNM") && !string.IsNullOrEmpty(filters["fromProductionTeamNM"].ToString()))
                {
                    fromProductionTeamNM = filters["fromProductionTeamNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toProductionTeamNM") && !string.IsNullOrEmpty(filters["toProductionTeamNM"].ToString()))
                {
                    toProductionTeamNM = filters["toProductionTeamNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workOrderUD") && !string.IsNullOrEmpty(filters["workOrderUD"].ToString()))
                {
                    workOrderUD = filters["workOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelNM") && !string.IsNullOrEmpty(filters["modelNM"].ToString()))
                {
                    modelNM = filters["modelNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productArticleCode") && !string.IsNullOrEmpty(filters["productArticleCode"].ToString()))
                {
                    productArticleCode = filters["productArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productDescription") && !string.IsNullOrEmpty(filters["productDescription"].ToString()))
                {
                    productDescription = filters["productDescription"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatorName") && !string.IsNullOrEmpty(filters["updatorName"].ToString()))
                {
                    updatorName = filters["updatorName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatedDate") && !string.IsNullOrEmpty(filters["updatedDate"].ToString()))
                {
                    updatedDatest = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("deliveryNoteTypeID") && filters["deliveryNoteTypeID"] != null)
                {
                    deliveryNoteTypeID = Convert.ToInt32(filters["deliveryNoteTypeID"]);
                }
                if (filters.ContainsKey("factoryWarehouseID") && filters["factoryWarehouseID"] != null)
                {
                    factoryWarehouseID = Convert.ToInt32(filters["factoryWarehouseID"]);
                }

                //search detail
                if (filters.ContainsKey("fromDeliveryNoteDate") && !string.IsNullOrEmpty(filters["fromDeliveryNoteDate"].ToString()))
                {
                    fromDeliveryNoteDate = filters["fromDeliveryNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toDeliveryNoteDate") && !string.IsNullOrEmpty(filters["toDeliveryNoteDate"].ToString()))
                {
                    toDeliveryNoteDate = filters["toDeliveryNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromUpdatedDate") && !string.IsNullOrEmpty(filters["fromUpdatedDate"].ToString()))
                {
                    fromUpdatedDate = filters["fromUpdatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toUpdatedDate") && !string.IsNullOrEmpty(filters["toUpdatedDate"].ToString()))
                {
                    toUpdatedDate = filters["toUpdatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("statusTypeID") && filters["statusTypeID"] != null)
                {
                    statusTypeID = Convert.ToInt32(filters["statusTypeID"]);
                }
                if (filters.ContainsKey("fromProductionTeamID") && filters["fromProductionTeamID"] != null)
                {
                    fromProductionTeamID = Convert.ToInt32(filters["fromProductionTeamID"]);
                }
                if (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }


                //DateTime? deliveryNoteDate = deliveryNoteDatest.ConvertStringToDateTime();
                //DateTime? updatedDate = updatedDatest.ConvertStringToDateTime();

                using (DeliveryNote2Entities context = CreateContext())
                {
                    totalRows = context.DeliveryNoteMng_function_SearchDeliveryNote(companyID, orderBy, orderDirection, deliveryNoteUD, deliveryNoteDatest, workCenterNM, fromProductionTeamNM, toProductionTeamNM, workOrderUD, modelUD, modelNM, productArticleCode, productDescription, description, updatedDatest, updatorName, deliveryNoteTypeID, factoryWarehouseID, fromDeliveryNoteDate, toDeliveryNoteDate, statusTypeID, fromUpdatedDate, toUpdatedDate, workCenterID, fromProductionTeamID).Count();
                    var result = context.DeliveryNoteMng_function_SearchDeliveryNote(companyID, orderBy, orderDirection, deliveryNoteUD, deliveryNoteDatest, workCenterNM, fromProductionTeamNM, toProductionTeamNM, workOrderUD, modelUD, modelNM, productArticleCode, productDescription, description, updatedDatest, updatorName, deliveryNoteTypeID, factoryWarehouseID, fromDeliveryNoteDate, toDeliveryNoteDate, statusTypeID, fromUpdatedDate, toUpdatedDate, workCenterID, fromProductionTeamID);
                    searchFormData.Data = converter.DB2DTO_DeliveryNoteSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchFormData.supportFactoryWareHouseLists = converter.DB2DTO_GetlistWareHouseList(context.DeliveryNoteMng_SupportFactoryWaseHouseList_View.ToList());
                    //Add Status
                    searchFormData.StatusTypes = new List<StatusTypeDTO>();
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 1, StatusTypeNM = "From Production" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 2, StatusTypeNM = "From Return" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 3, StatusTypeNM = "From Other" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 4, StatusTypeNM = "From Outsource" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 5, StatusTypeNM = "From Sale Order" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 6, StatusTypeNM = "From Factory Order" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 7, StatusTypeNM = "From Transfer Warehouse" });

                    foreach (var item in searchFormData.Data.ToList())
                    {
                        if (item.StatusTypeID == 1)
                        {
                            item.StatusTypeNM = "From Production";
                        }
                        else
                        {
                            if (item.StatusTypeID == 2)
                            {
                                item.StatusTypeNM = "From Return";
                            }
                            else
                            {
                                if (item.StatusTypeID == 3)
                                {
                                    item.StatusTypeNM = "From Other";
                                }
                                else
                                {
                                    if (item.StatusTypeID == 4)
                                    {
                                        item.StatusTypeNM = "From Outsource";
                                    }
                                    else
                                    {
                                        if(item.StatusTypeID == 5)
                                        {
                                            item.StatusTypeNM = "From Sale Order";
                                        }
                                        else
                                        {
                                            if(item.StatusTypeID == 6)
                                            {
                                                item.StatusTypeNM = "From Factory Order";
                                            }
                                            else
                                            {
                                                item.StatusTypeNM = "From Transfer Warehouse";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    int hasDeliveryNoteType = 0;
                    hasDeliveryNoteType = searchFormData.Data.Where(o => o.DeliveryNoteTypeID != null).Count();
                    if (hasDeliveryNoteType == 0)
                    {
                        //load workorder
                        if (searchFormData.Data.Count > 0)
                        {
                            int? numberWO = searchFormData.Data.Where(o => !string.IsNullOrEmpty(o.WorkOrderIDs)).Count();
                            if( numberWO != 0 || numberWO != null)
                            {
                                string x = searchFormData.Data.Where(o => !string.IsNullOrEmpty(o.WorkOrderIDs)).Select(s => s.WorkOrderIDs).Aggregate((i, j) => i + "," + j);
                                string[] woIDs = x.Split(',');
                                List<int> woIDi = new List<int>();
                                woIDi = woIDs.Select(int.Parse).ToList();
                                var allWorkOrders = context.DeliveryNoteMng_WorkOrder_SearchView.Where(o => woIDi.Contains(o.WorkOrderID.Value)).ToList();
                                foreach (var item in searchFormData.Data)
                                {
                                    item.WorkOrderSearchDTOs = new List<DTO.WorkOrderSearchDTO>();
                                    if (item.WorkOrderIDs != null)
                                    {
                                        List<int> _woID = item.WorkOrderIDs.Split(',').Select(int.Parse).ToList();
                                        var _wo = allWorkOrders.Where(o => _woID.Contains(o.WorkOrderID.Value)).ToList();
                                        foreach (var woItem in _wo)
                                        {
                                            if (woItem.DeliveryNoteID.Value == item.DeliveryNoteID)
                                            {
                                                item.WorkOrderSearchDTOs.Add(new DTO.WorkOrderSearchDTO { WorkOrderID = woItem.WorkOrderID, WorkOrderUD = woItem.WorkOrderUD, ModelUD = woItem.ModelUD, ModelNM = woItem.ModelNM });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return searchFormData;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return searchFormData;
            }
        }

        public string GetDeliveryNotePrintout(int deliveryNoteID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DeliveryNoteMng_function_GetDeliveryNotePrintout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@DeliveryNoteID", deliveryNoteID);
                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.Fill(ds);
                return Library.Helper.CreateReceiptPrintout(ds.Tables["Receipt"], ds.Tables["ReceiptDetail"], "DeliveryNotePrintout.rdlc");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }

        public DTO.DeliveryNotePrintoutDTO GetDeliveryNotePrintoutHTML(int deliveryNoteID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DeliveryNotePrintoutDTO deliveryNotePrintout = new DTO.DeliveryNotePrintoutDTO();
            deliveryNotePrintout.DeliveryNoteDetailPrintoutDTOs = new List<DTO.DeliveryNoteDetailPrintoutDTO>();
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DeliveryNoteMng_function_GetDeliveryNotePrintout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@DeliveryNoteID", deliveryNoteID);
                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.TableMappings.Add("Table2", "ReceiptQtyDetail");
                adap.Fill(ds);

                //read to delivery note
                var deliveryNote = ds.Receipt.FirstOrDefault();
                deliveryNotePrintout.ReceiptNo = deliveryNote.ReceiptNo;
                deliveryNotePrintout.ReceiptDate = deliveryNote.ReceiptDate;
                deliveryNotePrintout.DeliverName = deliveryNote.DeliverName;
                deliveryNotePrintout.DeliverAddress = deliveryNote.DeliverAddress;
                deliveryNotePrintout.ReceiverName = deliveryNote.ReceiverName;
                deliveryNotePrintout.ReceiverAddress = deliveryNote.ReceiverAddress;
                deliveryNotePrintout.StockName = deliveryNote.StockName;
                deliveryNotePrintout.StockAddress = deliveryNote.StockAddress;
                deliveryNotePrintout.Reason = deliveryNote.Reason;
                deliveryNotePrintout.DayReceipt = deliveryNote.DayReceipt;
                deliveryNotePrintout.MonthReceipt = deliveryNote.MonthReceipt;
                deliveryNotePrintout.YearReceipt = deliveryNote.YearReceipt;

                //read delivery note detail
                DTO.DeliveryNoteDetailPrintoutDTO deliveryNoteDetail;
                foreach (var itemqty in ds.ReceiptQtyDetail)
                {
                    deliveryNoteDetail = new DTO.DeliveryNoteDetailPrintoutDTO();
                    deliveryNotePrintout.DeliveryNoteDetailPrintoutDTOs.Add(deliveryNoteDetail);

                    foreach (var item in ds.ReceiptDetail)
                    {
                        if (itemqty.ProductionItemID == item.ProductionItemID)
                        {
                            deliveryNoteDetail.ProductionItemNM = item.FactoryMaterialNM;
                            deliveryNoteDetail.ProductionItemUD = item.FactoryMaterialUD;
                            deliveryNoteDetail.UnitNM = item.UnitNM;
                            deliveryNoteDetail.Quantity = itemqty.Quantity;
                            deliveryNoteDetail.Price = itemqty.Price;
                            deliveryNoteDetail.Amount = itemqty.Amount;
                            if (item.Description != "") { deliveryNoteDetail.Description += item.Description + ", "; };
                            if (item.ClientID != "") { deliveryNoteDetail.ClientID += item.ClientID + ", "; };
                            if (item.WorkOrderUD != "") { deliveryNoteDetail.WorkOrderUD += item.WorkOrderUD + ", "; };
                            deliveryNoteDetail.ProductionItemID = item.ProductionItemID;
                        }
                    }
                }
                return deliveryNotePrintout;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return deliveryNotePrintout;
            }
        }

        public List<DTO.BOMDTO> GetBOM(string workOrderIDs, int branchID, string deliveryNoteDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.BOMDTO> data = new List<DTO.BOMDTO>();
            try
            {
                using (DeliveryNote2Entities context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<DeliveryNoteMng_BOM_View>, List<DTO.BOMDTO>>(context.DeliveryNoteMng_function_GetBom(workOrderIDs, branchID, deliveryNoteDate).ToList());

                    foreach (var item in data)
                    {
                        var itemUnit = context.DeliveryNoteMng_Function_ProductionItemUnitByValidDate(deliveryNoteDate.ConvertStringToDateTime(), item.ProductionItemID);
                        List<ProductionItemUnitBOM> x = AutoMapper.Mapper.Map<List<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<ProductionItemUnitBOM>>(itemUnit.ToList());
                        item.ProductionItemUnits.AddRange(x);
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.BOMDTO>();
            }
        }

        private List<DTO.WorkCenterDTO> GetWorkCenter(string workOrderIDs, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.WorkCenterDTO> data = new List<DTO.WorkCenterDTO>();
            try
            {
                using (DeliveryNote2Entities context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<DeliveryNoteMng_WorkCenter_View>, List<DTO.WorkCenterDTO>>(context.DeliveryNoteMng_function_GetWorkCenter(workOrderIDs).Where(o => o.WorkCenterID != 10).ToList()); // Remove workcenter is finishing
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.WorkCenterDTO>();
            }
        }

        public bool DeleteWithPermission(int id, int createdBy, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DeliveryNote2Entities context = CreateContext())
                {
                    var dbItem = context.DeliveryNote.FirstOrDefault(o => o.DeliveryNoteID == id);

                    if (dbItem == null)
                    {
                        // return message notification.
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Can not find Data !" };
                        return false;
                    }

                    if (dbItem.CreatedBy.HasValue && dbItem.CreatedBy.Value == userId)
                    {
                        // execute delete id.
                        context.DeliveryNote.Remove(dbItem);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        // execute delete id.
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Only the created has the right to delete" };
                        return false;
                    }

                    #region Delete old version
                    //if (createdBy != 0)
                    //{
                    //    if (dbItem.CreatedBy.Value == userId)
                    //    {
                    //        // execute delete id.
                    //        context.DeliveryNote.Remove(dbItem);
                    //        context.SaveChanges();
                    //        return true;
                    //    }

                    //    if (dbItem.CreatedBy.Value != userId)
                    //    {
                    //        // execute delete id.
                    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Only the created has the right to delete" };
                    //        return false;
                    //    }
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public DTO.OverviewForm GetViewData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            DTO.OverviewForm overviewForm = new DTO.OverviewForm();
            overviewForm.Data = new DTO.DeliveryNoteFormViewDTO();
            overviewForm.Data.deliveryNoteDetailFormViewDTOs = new List<DTO.DeliveryNoteDetailFormViewDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        overviewForm.Data = converter.DB2DTO_DeliveryNoteFormView(context.DeliveryNoteMng_DeliveryNote_FormView.Include("DelivetyNoteMng_DeliveryNoteDetail_FormView").FirstOrDefault(o => o.DeliveryNoteID == id));
                    }
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return overviewForm;
        }

        public List<DTO.ProductionItem> GetProductionItem(string searchQuery, int userId, string deliveryNoteDateStr, int branchID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            List<DTO.ProductionItem> data = new List<DTO.ProductionItem>();

            try
            {
                // Get company from framework
                //Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                //int? companyID = fwFactory.GetCompanyID(userId);
                DateTime? deliveryNoteDate = deliveryNoteDateStr.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    //var dbItem = context.DeliveryNoteMng_SupportProductionItem_View.Where(o => o.CompanyID == companyID &&)
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    //int? branchID2 = context.Employee.FirstOrDefault(o => o.UserID == userId).BranchID;
                    int productionItemID;
                    //Use When change Unit
                    if (Int32.TryParse(searchQuery, out productionItemID))
                    {
                        List<DeliveryNoteMng_SupportProductionItem_View> dbItems;
                        var itemx = context.DeliveryNoteMng_SupportProductionItem_View.Where(o => o.ProductionItemID == productionItemID && o.BranchID == branchID);
                        dbItems = itemx.ToList();
                        data = AutoMapper.Mapper.Map<List<DeliveryNoteMng_SupportProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
                    }
                    else
                    {
                        //Use When search ProductionItem
                        List<DeliveryNoteMng_Function_SupportProductionItem_View_Result> dbItems2;
                        var itemDatas = context.DeliveryNoteMng_Function_SupportProductionItem_View(companyID, searchQuery, branchID).Where(o => o.BranchID == branchID);
                        dbItems2 = itemDatas.ToList();
                        data = AutoMapper.Mapper.Map<List<DeliveryNoteMng_Function_SupportProductionItem_View_Result>, List<DTO.ProductionItem>>(dbItems2);
                        foreach (var item in data)
                        {
                            var itemUnit = context.DeliveryNoteMng_Function_ProductionItemUnitByValidDate(deliveryNoteDate, item.ProductionItemID);
                            item.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(itemUnit.ToList()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public void ValidateStockQnt(DTO.DeliveryNoteDTO dtoDeliveryNote)
        {
            if (!string.IsNullOrEmpty(dtoDeliveryNote.ViewName) &&
                (dtoDeliveryNote.ViewName == "Warehouse2Team" || dtoDeliveryNote.ViewName == "AmendWarehouse2Team" || dtoDeliveryNote.ViewName == "SaleOrderWithoutWorkOrder"))
            {
                using (var context = CreateContext())
                {
                    foreach (var dtoDetailItem in dtoDeliveryNote.DeliveryNoteDetailDTOs.ToList())
                    {
                        var totalDelivery = GetTotalDelivery(dtoDetailItem.ProductionItemID, dtoDetailItem.FromFactoryWarehouseID, dtoDeliveryNote.DeliveryNoteDetailDTOs, context);

                        //var dbDetailItem = context.DeliveryNoteMng_SupportProductionItem_View.FirstOrDefault(o => o.ProductionItemID == dtoDetailItem.ProductionItemID && o.BranchID == dtoDeliveryNote.BranchID);

                        if (dtoDetailItem.DeliveryNoteDetailID <= 0 && dtoDetailItem.StockQnt.HasValue && dtoDetailItem.StockQnt.Value == 0 && totalDelivery.HasValue && totalDelivery.Value > 0)
                        {
                            throw new Exception("Quantity for item " + dtoDetailItem.ProductionItemNM + " not enough to delivery");
                        }

                        if (dtoDetailItem.StockQnt.HasValue && totalDelivery.HasValue)
                        {
                            if ((dtoDetailItem.DeliveryNoteDetailID > 0 && (dtoDetailItem.StockQnt.Value + totalDelivery) < 0) || (dtoDetailItem.DeliveryNoteDetailID < 0 && (dtoDetailItem.StockQnt.Value - totalDelivery) < 0))
                            {
                                throw new Exception("Quantity for item " + dtoDetailItem.ProductionItemNM + " must be less or equal than stock qnt");
                            }
                        }
                    }
                }
            }
        }

        public List<DTO.FactorySaleOrderDTO> GetFactorySaleOrder(int userId, string factorySalesOrderUD, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactorySaleOrderDTO> data = new List<DTO.FactorySaleOrderDTO>();
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (DeliveryNote2Entities context = CreateContext())
                {
                    var x = context.DeliveryNoteMng_QuickSearchFactorySaleOrder_View.Where(o => o.CompanyID == companyID && o.FactorySaleOrderUD.Contains(factorySalesOrderUD) && o.IsConfirmed == true).ToList();
                    data = AutoMapper.Mapper.Map<List<DeliveryNoteMng_QuickSearchFactorySaleOrder_View>, List<DTO.FactorySaleOrderDTO>>(x);

                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.FactorySaleOrderDTO>();
            }
        }

        public string GetDeliveryNoteExportToExcelList(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ListDeliveryNoteDataObject ds = new ListDeliveryNoteDataObject();

            //try to get data
            try
            {
                int? companyID = null;
                string deliveryNoteUD = null;
                string deliveryNoteDatest = null;
                string workCenterNM = null;
                string fromProductionTeamNM = null;
                string toProductionTeamNM = null;
                string workOrderUD = null;
                string modelUD = null;
                string modelNM = null;
                string productArticleCode = null;
                string productDescription = null;
                string description = null;
                string updatedDatest = null;
                string updatorName = null;
                int? deliveryNoteTypeID = null;
                int? factoryWarehouseID = null;
                //Detail Search
                string fromDeliveryNoteDate = null;
                string toDeliveryNoteDate = null;
                int? statusTypeID = null;
                string statusTypeNM = null;
                string fromUpdatedDate = null;
                string toUpdatedDate = null;
                int? workCenterID = null;
                int? fromProductionTeamID = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("deliveryNoteUD") && !string.IsNullOrEmpty(filters["deliveryNoteUD"].ToString()))
                {
                    deliveryNoteUD = filters["deliveryNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("deliveryNoteDate") && !string.IsNullOrEmpty(filters["deliveryNoteDate"].ToString()))
                {
                    deliveryNoteDatest = filters["deliveryNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workCenterNM") && !string.IsNullOrEmpty(filters["workCenterNM"].ToString()))
                {
                    workCenterNM = filters["workCenterNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamNM") && !string.IsNullOrEmpty(filters["fromProductionTeamNM"].ToString()))
                {
                    fromProductionTeamNM = filters["fromProductionTeamNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toProductionTeamNM") && !string.IsNullOrEmpty(filters["toProductionTeamNM"].ToString()))
                {
                    toProductionTeamNM = filters["toProductionTeamNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workOrderUD") && !string.IsNullOrEmpty(filters["workOrderUD"].ToString()))
                {
                    workOrderUD = filters["workOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelNM") && !string.IsNullOrEmpty(filters["modelNM"].ToString()))
                {
                    modelNM = filters["modelNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productArticleCode") && !string.IsNullOrEmpty(filters["productArticleCode"].ToString()))
                {
                    productArticleCode = filters["productArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productDescription") && !string.IsNullOrEmpty(filters["productDescription"].ToString()))
                {
                    productDescription = filters["productDescription"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatorName") && !string.IsNullOrEmpty(filters["updatorName"].ToString()))
                {
                    updatorName = filters["updatorName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatedDate") && !string.IsNullOrEmpty(filters["updatedDate"].ToString()))
                {
                    updatedDatest = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("deliveryNoteTypeID") && filters["deliveryNoteTypeID"] != null)
                {
                    deliveryNoteTypeID = Convert.ToInt32(filters["deliveryNoteTypeID"]);
                }
                if (filters.ContainsKey("factoryWarehouseID") && filters["factoryWarehouseID"] != null)
                {
                    factoryWarehouseID = Convert.ToInt32(filters["factoryWarehouseID"]);
                }

                //search detail
                if (filters.ContainsKey("fromDeliveryNoteDate") && !string.IsNullOrEmpty(filters["fromDeliveryNoteDate"].ToString()))
                {
                    fromDeliveryNoteDate = filters["fromDeliveryNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toDeliveryNoteDate") && !string.IsNullOrEmpty(filters["toDeliveryNoteDate"].ToString()))
                {
                    toDeliveryNoteDate = filters["toDeliveryNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromUpdatedDate") && !string.IsNullOrEmpty(filters["fromUpdatedDate"].ToString()))
                {
                    fromUpdatedDate = filters["fromUpdatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toUpdatedDate") && !string.IsNullOrEmpty(filters["toUpdatedDate"].ToString()))
                {
                    toUpdatedDate = filters["toUpdatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("statusTypeID") && filters["statusTypeID"] != null)
                {
                    statusTypeID = Convert.ToInt32(filters["statusTypeID"]);
                }
                if (filters.ContainsKey("statusTypeNM") && !string.IsNullOrEmpty(filters["statusTypeNM"].ToString()))
                {
                    statusTypeNM = filters["statusTypeNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamID") && filters["fromProductionTeamID"] != null)
                {
                    fromProductionTeamID = Convert.ToInt32(filters["fromProductionTeamID"]);
                }
                if (filters.ContainsKey("workCenterID") && filters["workCenterID"] != null)
                {
                    workCenterID = Convert.ToInt32(filters["workCenterID"]);
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DeliveryNoteMng_function_ExportToExcelListReceivingNote", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.SelectCommand.Parameters.AddWithValue("@DeliveryNoteUD", deliveryNoteUD);
                adap.SelectCommand.Parameters.AddWithValue("@DeliveryNoteDate", deliveryNoteDatest);
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterNM", workCenterNM);
                adap.SelectCommand.Parameters.AddWithValue("@FromProductionTeamNM", fromProductionTeamNM);
                adap.SelectCommand.Parameters.AddWithValue("@ToProductionTeamNM", toProductionTeamNM);
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderUD", workOrderUD);
                adap.SelectCommand.Parameters.AddWithValue("@ModelUD", modelUD);
                adap.SelectCommand.Parameters.AddWithValue("@ModelNM", modelNM);
                adap.SelectCommand.Parameters.AddWithValue("@ProductArticleCode", productArticleCode);
                adap.SelectCommand.Parameters.AddWithValue("@ProductDescription", productDescription);
                adap.SelectCommand.Parameters.AddWithValue("@Description", description);
                adap.SelectCommand.Parameters.AddWithValue("@UpdatorName", updatorName);
                adap.SelectCommand.Parameters.AddWithValue("@UpdatedDate", updatedDatest);
                adap.SelectCommand.Parameters.AddWithValue("@DeliveryNoteTypeID", deliveryNoteTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehouseID", factoryWarehouseID);
                adap.SelectCommand.Parameters.AddWithValue("@FromDeliveryNoteDate", fromDeliveryNoteDate);
                adap.SelectCommand.Parameters.AddWithValue("@ToDeliveryNoteDate", toDeliveryNoteDate);
                adap.SelectCommand.Parameters.AddWithValue("@StatusTypeID", statusTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@StatusTypeNM", statusTypeNM);
                adap.SelectCommand.Parameters.AddWithValue("@FromUpdatedDate", fromUpdatedDate);
                adap.SelectCommand.Parameters.AddWithValue("@ToUpdatedDate", toUpdatedDate);
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterID", workCenterID);
                adap.SelectCommand.Parameters.AddWithValue("@FromProductionTeamID", fromProductionTeamID);

                adap.TableMappings.Add("Table", "ExportToExcelDeliveryNote");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListDeliveryNote");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return string.Empty;
            }
        }

        public object GetSearchDetail(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            int? companyID = null;
            DTO.ListSearchDetail data = new DTO.ListSearchDetail();
            Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
            companyID = fw_factory.GetCompanyID(userId);
            try
            {
                using (var context = CreateContext())
                {
                    data.WorkCenters = supportFactory.GetWorkCenter();
                    data.ProductionTeams = supportFactory.GetProductionTeam(companyID);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public decimal? GetTotalDelivery(int? productionItemID, int? factoryWarehouseID, List<DTO.DeliveryNoteDetailDTO> dtoDeliveryNoteDetail, DeliveryNote2Entities context)
        {
            decimal? totalDelivery = 0;

            foreach (var dtoDetailItem in dtoDeliveryNoteDetail.ToList())
            {
                if (dtoDetailItem.Qty.HasValue && dtoDetailItem.ProductionItemID == productionItemID && dtoDetailItem.FromFactoryWarehouseID == factoryWarehouseID)
                {
                    if (dtoDetailItem.DeliveryNoteDetailID < 0)
                    {
                        totalDelivery = totalDelivery + dtoDetailItem.Qty.Value;
                    }
                    else
                    {
                        var dbDeliveryNoteDetail = context.DeliveryNoteMng_DeliveryNoteDetail_View.FirstOrDefault(o => o.DeliveryNoteDetailID == dtoDetailItem.DeliveryNoteDetailID);
                        if (dbDeliveryNoteDetail != null)
                        {
                            totalDelivery = totalDelivery + (dbDeliveryNoteDetail.Qty.Value - dtoDetailItem.Qty.Value);
                        }
                    }
                }
            }

            return totalDelivery;
        }

        public object PurchaseOrderQuickseach(int userID, string purchaseOrderUD, int branchID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<PurchaseOrderDTO> data = new List<PurchaseOrderDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<DeliveryNote2Mng_PurchaseOrder_View>, List<PurchaseOrderDTO>>(context.DeliveryNote2Mng_PurchaseOrder_View.Where(o => o.PurchaseOrderUD.Contains(purchaseOrderUD)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object PurchaseOrderDetailQuicksearch(int purchaseOrderID, int branchID, string deliveryNoteDate, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<PurchaseOrderDetailDTO> data = new List<PurchaseOrderDetailDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.DeliveryNote2Mng_function_PurchaseOrderDetail(purchaseOrderID, branchID);
                    data = AutoMapper.Mapper.Map<List<DeliveryNote2Mng_PurchaseOrderDetail_View>, List<PurchaseOrderDetailDTO>>(dbItem.ToList());
                    foreach (var item in data)
                    {
                        var itemUnit = context.DeliveryNoteMng_Function_ProductionItemUnitByValidDate(deliveryNoteDate.ConvertStringToDateTime(), item.ProductionItemID);
                        item.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(itemUnit.ToList()));
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object FactorySaleOrderDetailQuicksearch(int factorySaleOrderID, int branchID, string deliveryNoteDate, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<FactorySaleOrderDetailDTO> data = new List<FactorySaleOrderDetailDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.DeliveryNote2Mng_function_FactorySaleOrderDetail(factorySaleOrderID, branchID);
                    data = AutoMapper.Mapper.Map<List<DeliveryNoteMng_QuickSearchFactorySaleOrderDetail_View>, List<FactorySaleOrderDetailDTO>>(dbItem.ToList());
                    foreach (var item in data)
                    {
                        var itemUnit = context.DeliveryNoteMng_Function_ProductionItemUnitByValidDate(deliveryNoteDate.ConvertStringToDateTime(), item.ProductionItemID);
                        item.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(itemUnit.ToList()));
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        #region Send email notification group
        private void SendNotification(string emailSubject, string emailBody)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (DeliveryNote2Entities context = CreateContext())
                {
                    // Custome email group
                    var dbNotificationEmails = context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "DELTAWARN");
                    foreach (var dbNotificationEmail in dbNotificationEmails.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbNotificationEmail.Email1) && !emailAddress.Contains(dbNotificationEmail.Email1))
                        {
                            emailAddress.Add(dbNotificationEmail.Email1);
                        }

                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = dbNotificationEmail.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTION;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }

                    string sendToEmail = string.Empty;
                    foreach (string eAddress in emailAddress)
                    {
                        if (string.IsNullOrEmpty(sendToEmail))
                        {
                            sendToEmail += eAddress;
                        }
                        else
                        {
                            sendToEmail += ";" + eAddress;
                        }
                    }
                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    dbEmail.EmailBody = emailBody;
                    dbEmail.EmailSubject = emailSubject;
                    dbEmail.SendTo = sendToEmail;
                    context.EmailNotificationMessage.Add(dbEmail);
                    context.SaveChanges();
                }
            }
            catch { }
        }
        #endregion
    }
}
