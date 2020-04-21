using Library;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Module.ReceivingNote.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();

        public readonly string notificationGroupUD = "R-1907-001";

        public DataFactory() { }

        private ReceivingNoteEntities CreateContext()
        {
            return new ReceivingNoteEntities(Library.Helper.CreateEntityConnectionString("DAL.ReceivingNoteModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ReceivingNote.FirstOrDefault(o => o.ReceivingNoteID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsConfirm = true;
                    dbItem.ConfirmBy = userId;
                    dbItem.ConfirmDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.ReceivingNoteID, out notification).Data;
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReceivingNoteEntities context = CreateContext())
                {
                    var dbItem = context.ReceivingNote.Where(o => o.ReceivingNoteID == id).FirstOrDefault();
                    context.ReceivingNote.Remove(dbItem);
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReceivingNoteEntities context = CreateContext())
                {
                    ReceivingNoteMng_ReceivingNote_View dbItem;
                    dbItem = context.ReceivingNoteMng_ReceivingNote_View.Include("ReceivingNoteMng_ReceivingNoteDetail_View").FirstOrDefault(o => o.ReceivingNoteID == id);
                    editFormData.Data = converter.DB2DTO_ReceivingNote(dbItem);
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
            throw new Exception();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ReceivingNote.FirstOrDefault(o => o.ReceivingNoteID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsConfirm = false;
                    dbItem.ResetBy = userId;
                    dbItem.ResetDate = DateTime.Now;
                    dbItem.ConfirmBy = null;
                    dbItem.ConfirmDate = null;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.ReceivingNoteID, out notification).Data;
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
        /// <summary>
        /// NVDANG
        /// Kiểm tra Quantity WorkOrder Auto Finish
        /// </summary>
        /// <param name="dtoItem"></param>

        private void Verify_Status_WorkOrder(DTO.ReceivingNoteDTO dtoItem, int? branchID)
        {
            using (ReceivingNoteEntities context = CreateContext())
            {
                var workorderids = dtoItem.ReceivingNoteDetailDTOs.GroupBy(x => x.WorkOrderID);
                foreach (var item in workorderids)
                {
                    List<DTO.BOMDTO> bOMDTOs = converter.DB2DTO_BOM(context.ReceivingNoteMng_function_GetBom(item.Key.ToString(), branchID, dtoItem.ReceivingNoteDate).Where(x => x.WorkCenterID == dtoItem.WorkCenterID).ToList());
                    int count = 0;
                    foreach (DTO.BOMDTO itemdto in bOMDTOs)
                    {
                        WorkOrder workOrder = context.WorkOrder.Where(x => x.WorkOrderID == item.Key).FirstOrDefault();
                        if (workOrder != null)
                        {
                            OPSequenceDetail oPSequenceDetail = context.OPSequenceDetail.Where(x => x.OPSequence.OPSequenceID == workOrder.OPSequence.OPSequenceID).OrderByDescending(y => y.SequenceIndexNumber).FirstOrDefault();
                            if (oPSequenceDetail != null && oPSequenceDetail.WorkCenter.WorkCenterID == dtoItem.WorkCenterID && itemdto.BOMQnt <= itemdto.TotalReceive)
                            {
                                count++;
                            }
                        }
                        //TO DO Change WorkOrder Status
                        if (count == bOMDTOs.Count)
                        {
                            workOrder.WorkOrderStatusID = 3;
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReceivingNoteDTO dtoReceivingNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ReceivingNoteDTO>();
            dtoReceivingNote.PurchasingOrderNo = dtoReceivingNote.PurchaseOrderUD;

            string baseUrl = FrameworkSetting.Setting.FrontendUrl;

            try
            {
                if (!dtoReceivingNote.StatusTypeID.HasValue)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Status type is null!";
                    return false;
                }

                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);

                // Get branch(for process Verify_Status_WorkOrder)
                int? branchID = dtoReceivingNote.BranchID;

                using (ReceivingNoteEntities context = CreateContext())
                {
                    //Check Warehouse 
                    foreach (var item in dtoReceivingNote.ReceivingNoteDetailDTOs.Where(o => o.ProductionItemID.HasValue))
                    {
                        if (item.ToFactoryWarehouseID == null)
                        {
                            throw new Exception("missing Warehowse for: " + item.ProductionItemNM);
                        }
                    }

                    ReceivingNote dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ReceivingNote();
                        context.ReceivingNote.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ReceivingNote.Where(o => o.ReceivingNoteID == id).FirstOrDefault();

                        //CheckPermission
                        if (dbItem.CreatedBy.HasValue && dbItem.CreatedBy.Value != userId)
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
                        if (string.IsNullOrEmpty(dtoReceivingNote.ReceivingNoteDate))
                        {
                            throw new Exception("You have to fill-in date receiving");
                        }

                        //check price status in case receiving note: PO2WarehouseWithoutWorkOrder
                        //if (dtoReceivingNote.ViewName == "PO2WarehouseWithoutWorkOrder"){ }
                        if (dtoReceivingNote.ReceivingNoteTypeID != 3)
                        {
                            DateTime? receivingDate = dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime();
                            int month = receivingDate.Value.Month;
                            int? year = receivingDate.Value.Year;
                            List<int> productionItemTypeIDs = dtoReceivingNote.ReceivingNoteDetailDTOs.Where(o => o.ReceivingNoteDetailID > 0 && o.ProductionItemTypeID.HasValue).Select(s => s.ProductionItemTypeID.Value).ToList();
                            List<int> productionItemIDs = dtoReceivingNote.ReceivingNoteDetailDTOs.Where(o => o.ReceivingNoteDetailID < 0 && o.ProductionItemID.HasValue).Select(s => s.ProductionItemID.Value).ToList();

                            var productionItem = context.ProductionItem.Where(o => productionItemIDs.Contains(o.ProductionItemID) && o.ProductionItemTypeID.HasValue).Select(o => new { o.ProductionItemID, o.ProductionItemNM, o.ProductionItemTypeID }).ToList();
                            List<int> _productionItemTypeIDs = productionItem.Select(o => o.ProductionItemTypeID.Value).ToList();
                            productionItemTypeIDs.AddRange(_productionItemTypeIDs);

                            foreach (var productionItemTypeID in productionItemTypeIDs)
                            {
                                var productionPrice = context.ProductionPrice.Where(o => o.CompanyID == companyID && o.ProductionItemTypeID == productionItemTypeID && o.Month == month && o.Year == year).FirstOrDefault();
                                if (productionPrice != null && productionPrice.IsLocked.HasValue && productionPrice.IsLocked.Value)
                                {
                                    string productionItemNM = "";
                                    var x = dtoReceivingNote.ReceivingNoteDetailDTOs.Where(o => o.ProductionItemTypeID == productionItemTypeID).FirstOrDefault();
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
                        List<int?> workOrderIDs = dtoReceivingNote.ReceivingNoteDetailDTOs.Select(o => o.WorkOrderID).Distinct().ToList();
                        var wo = context.WorkOrder.Where(o => workOrderIDs.Contains(o.WorkOrderID)).Select(s => new { s.WorkOrderID, s.WorkOrderStatusID }).ToList();
                        foreach (var item in wo)
                        {
                            if (item.WorkOrderStatusID == 1)
                            {
                                throw new Exception("WorkOrder is opening you need confirm before receiving product");
                            }
                            else if (item.WorkOrderStatusID == 3)
                            {
                                throw new Exception("WorkOrder is finished you can not receiving product");
                            }
                            else if (item.WorkOrderStatusID == 4)
                            {
                                throw new Exception("WorkOrder is canceled you can not receiving product product");
                            }
                        }

                        ///ConversionFacror
                        foreach (var item in dtoReceivingNote.ReceivingNoteDetailDTOs)
                        {
                            foreach (var uItem in item.ProductionItemUnits)
                            {
                                if (item.ProductionItemID == uItem.ProductionItemID && item.UnitID == uItem.UnitID)
                                {
                                    item.Quantity = item.QtyByUnit * uItem.ConversionFactor;
                                }
                            }
                        }
                        //convert dto to db
                        converter.DTO2DB_ReceivingNote(dtoReceivingNote, ref dbItem);
                        dbItem.CompanyID = companyID;
                        if (id == 0)
                        {
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                            if (!string.IsNullOrEmpty(dbItem.PurchasingOrderNo))
                            {
                                dbItem.Description = dbItem.Description + " (" + dbItem.PurchasingOrderNo + ")";
                            }
                        }
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.ConfirmBy = userId;
                        dbItem.ConfirmDate = DateTime.Now;
                        dbItem.IsConfirm = true;

                        /// cuong.tran: ProductionFrameWeight - start
                        if (dbItem.StatusTypeID == 1 && (dbItem.WorkCenterID == 7 || dbItem.WorkCenterID == 8))
                        {
                            foreach (var item in dtoReceivingNote.ReceivingNoteDetailDTOs)
                            {
                                /// Only material is component
                                if (item.ProductionItemTypeID == 1 && item.FrameWeight.HasValue)
                                {
                                    var dtoFrameWeight = context.ProductionFrameWeight.FirstOrDefault(o => o.ProductionItemID == item.ProductionItemID);

                                    if (dtoFrameWeight == null)
                                    {
                                        // Insert table ProductionFrameWeight
                                        ProductionFrameWeight dbFrameWeight = new ProductionFrameWeight();
                                        context.ProductionFrameWeight.Add(dbFrameWeight);

                                        dbFrameWeight.ProductionItemID = item.ProductionItemID;
                                        dbFrameWeight.FrameWeight = item.FrameWeight;
                                        dbFrameWeight.UpdatedBy = userId;
                                        dbFrameWeight.UpdatedDate = DateTime.Now;

                                        context.SaveChanges();

                                        // Insert table ProductionFrameWeightHistory
                                        ProductionFrameWeightHistory dbFrameWeightHistory = new ProductionFrameWeightHistory();
                                        context.ProductionFrameWeightHistory.Add(dbFrameWeightHistory);

                                        dbFrameWeightHistory.ProductionFrameWeightID = dbFrameWeight.ProductionFrameWeightID;
                                        dbFrameWeightHistory.FrameWeight = item.FrameWeight;
                                        dbFrameWeightHistory.UpdatedBy = userId;
                                        dbFrameWeightHistory.UpdatedDate = DateTime.Now;

                                        context.SaveChanges();
                                    }
                                }
                            }
                        }
                        /// cuong.tran: ProductionFrameWeight - e n d

                        //remove orphan
                        context.ReceivingNoteDetail.Local.Where(o => o.ReceivingNote == null).ToList().ForEach(o => context.ReceivingNoteDetail.Remove(o));
                        context.ReceivingNoteWorkOrderDetail.Local.Where(o => o.ReceivingNoteDetail == null).ToList().ForEach(o => context.ReceivingNoteWorkOrderDetail.Remove(o));
                        context.ReceivingNoteDetailSubUnit.Local.Where(o => o.ReceivingNoteDetail == null).ToList().ForEach(o => context.ReceivingNoteDetailSubUnit.Remove(o));

                        //save data
                        context.SaveChanges();

                        //generate code
                        if (string.IsNullOrEmpty(dbItem.ReceivingNoteUD))
                        {
                            context.ReceivingNoteMng_function_GenerateReceivingNoteUD(dbItem.ReceivingNoteID, dbItem.CompanyID, dbItem.ReceivingNoteDate.Value.Year, dbItem.ReceivingNoteDate.Value.Month);
                        }

                        if (dbItem.StatusTypeID == 2)
                        {
                            if (dbItem.PurchaseOrderID != null && dbItem.PurchaseOrderID > 0)
                            {
                                var totalReceivingItems = context.ReceivingNoteMng_TotalReceivingItem_View.Where(o => o.PurchaseOrderID == dbItem.PurchaseOrderID).ToList();
                                var purchaseOrderDetails = context.PurchaseOrderDetail.Where(d => d.PurchaseOrderID == dbItem.PurchaseOrderID).ToList();
                                int checkQuantity = 0;

                                for (int i = 0; i < purchaseOrderDetails.Count(); i++)
                                {
                                    for (int j = 0; j < totalReceivingItems.Count(); j++)
                                    {
                                        if (purchaseOrderDetails[i].ProductionItemID == totalReceivingItems[j].ProductionItemID)
                                        {
                                            if (totalReceivingItems[j].TotalReceiving >= purchaseOrderDetails[i].OrderQnt)
                                            {
                                                checkQuantity++;
                                            }
                                        }
                                    }
                                }

                                if (checkQuantity == purchaseOrderDetails.Count())
                                {
                                    var poItem = context.PurchaseOrder.FirstOrDefault(o => o.PurchaseOrderID == dbItem.PurchaseOrderID);
                                    poItem.PurchaseOrderStatusID = 4;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    context.SaveChanges();
                                }
                            }
                        }

                        //get return data
                        var receivingNote = GetData(dbItem.ReceivingNoteID, out notification).Data;

                        if (receivingNote != null)
                        {
                            Verify_Status_WorkOrder(receivingNote, branchID);

                            // Check notitifation for Purchasing
                            if (receivingNote.StatusTypeID == 2)
                            {
                                SendEmailNotification(receivingNote, notificationGroupUD, baseUrl, out notification);
                            }
                        }

                        dtoItem = receivingNote;
                        
                        //Update Component # Piece for FactoryProductionStatus
                        #region Update FactoryOrderDetail for FactoryProductionStatus
                        if (dtoReceivingNote.StatusTypeID == 1)
                        {
                            if (dtoReceivingNote.WorkCenterID == 11)
                            {
                                //Piece
                                foreach (var item in dtoReceivingNote.ReceivingNoteDetailDTOs.ToList())
                                {
                                    decimal qty = 0;
                                    if (item.OldQuantity == null)
                                    {
                                        if(item.Quantity != null)
                                        {
                                            qty = (int)item.Quantity;
                                        }
                                    }
                                    else
                                    {
                                        if(item.Quantity != null)
                                        {
                                            if (item.OldQuantity != null)
                                            {
                                                qty = (int)item.Quantity - (int)item.OldQuantity;
                                            }
                                            else
                                            {
                                                qty = (int)item.Quantity;
                                            }
                                            
                                        }
                                        else
                                        {
                                            if(item.OldQuantity != null)
                                            {
                                                qty = 0 - (int)item.OldQuantity;
                                            }
                                        }
                                    }
                                    //1 WorkOrder have 1 WorkOrderDetail. Confirm from Mr Hanh
                                    var modelSetWO = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.WorkOrderID == item.WorkOrderID).FirstOrDefault();
                                    if(modelSetWO != null)
                                    {
                                        var modelUDSetWO = modelSetWO.ArticleCode.Substring(0, 4).ToString();
                                        var listModelSet = context.ReceivingNoteMng_ListModel_Set_View.Where(o => o.ModelUD == modelUDSetWO).FirstOrDefault();
                                        if (listModelSet == null) // Type != Set
                                        {
                                            var dbWOItem = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.ArticleCode == item.ProductionItemUD && o.WorkOrderID == item.WorkOrderID).ToList();
                                            int? weekNo = null;
                                            var season = GetSeason(dtoReceivingNote.ReceivingNoteDate);
                                            foreach (var subItem in dbWOItem.ToList())
                                            {
                                                var seasonDate = supportFactory.GetWeekInSeason(season).ToList();
                                                for (int i = 0; i < seasonDate.Count; i++)
                                                {
                                                    var seasonItem = seasonDate[i];
                                                    DateTime? valStartDate = seasonItem.StartDate.ConvertStringToDateTime();
                                                    DateTime? valEndDate = seasonItem.EndDate.ConvertStringToDateTime();
                                                    if (dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() >= valStartDate && dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() <= valEndDate)
                                                    {
                                                        weekNo = seasonItem.WeekNo;
                                                        break;
                                                    }
                                                }
                                                context.ReceivingNoteMng_function_UpdateFactoryProductionStatus(subItem.FactoryOrderDetailID, subItem.FactoryOrderID, subItem.FactoryID, season, qty, weekNo);
                                            }
                                        }
                                    }  
                                }
                                
                                //Set
                                var list_ReceivingNoteDetail = dtoReceivingNote.ReceivingNoteDetailDTOs.OrderBy(o => o.WorkOrderID).ToList();
                                int? workOrderID = null;
                                for (int i = 0; i < list_ReceivingNoteDetail.Count; i++)
                                {
                                    var item = list_ReceivingNoteDetail[i];
                                    if(item.WorkOrderID != workOrderID)
                                    {
                                        workOrderID = item.WorkOrderID;
                                        var modelUD = item.ProductionItemUD.Substring(0, 4).ToString();

                                        //1 WorkOrder have 1 WorkOrderDetail. Confirm from Mr Hanh
                                        var modelSetWO = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.WorkOrderID == item.WorkOrderID).FirstOrDefault();
                                        if(modelSetWO != null)
                                        {
                                            var modelUDSetWO = modelSetWO.ArticleCode.Substring(0, 4).ToString();
                                            var listModelSet = context.ReceivingNoteMng_ListModel_Set_View.Where(o => o.ModelUD == modelUDSetWO).FirstOrDefault();
                                            if (listModelSet != null)
                                            {
                                                var setModelUD = listModelSet.ModelUD;

                                                // lay qnt nho nhat trong từng WorkOrder để làm qnt cho Model = set
                                                int qty_SET = GetQnt_ModelSet_ReceivingNoteDetail(item, list_ReceivingNoteDetail);

                                                //
                                                var dbWOItem = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.ArticleCode.Contains(setModelUD) && o.WorkOrderID == item.WorkOrderID).ToList();
                                                int? weekNo = null;
                                                var season = GetSeason(dtoReceivingNote.ReceivingNoteDate);
                                                foreach (var subItem in dbWOItem.ToList())
                                                {
                                                    var seasonDate = supportFactory.GetWeekInSeason(season).ToList();
                                                    for (int j = 0; j < seasonDate.Count; j++)
                                                    {
                                                        var seasonItem = seasonDate[j];
                                                        DateTime? valStartDate = seasonItem.StartDate.ConvertStringToDateTime();
                                                        DateTime? valEndDate = seasonItem.EndDate.ConvertStringToDateTime();
                                                        if (dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() >= valStartDate && dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() <= valEndDate)
                                                        {
                                                            weekNo = seasonItem.WeekNo;
                                                            break;
                                                        }
                                                    }
                                                    context.ReceivingNoteMng_function_UpdateFactoryProductionStatus(subItem.FactoryOrderDetailID, subItem.FactoryOrderID, subItem.FactoryID, season, qty_SET, weekNo);
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
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
        /// get receiving note base on workOrder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filters"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.EditFormData GetData(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.EditFormData editFormData = new DTO.EditFormData();
            editFormData.Data = new DTO.ReceivingNoteDTO();
            editFormData.Data.ReceivingNoteDetailDTOs = new List<DTO.ReceivingNoteDetailDTO>();
            editFormData.FactoryWarehousePallets = new List<Support.DTO.FactoryWarehousePallet>();
            editFormData.ProductionTeams = new List<Support.DTO.ProductionTeam>();
            editFormData.ProductionItemTypes = new List<Support.DTO.ProductionItemType>();
            editFormData.WorkCenters = new List<DTO.WorkCenter>();
            editFormData.WorkOrderList = new List<DTO.WorkOrderDTO>();
            editFormData.TransportationServiceDTOs = new List<DTO.TransportationServiceDTO>();
            editFormData.StatusTypes = new List<DTO.StatusTypeDTO>();
            editFormData.ReasonOtherPrices = new List<object>();
            try
            {
                int id = (filters.ContainsKey("id") && filters["id"] != null) ? Convert.ToInt32(filters["id"].ToString()) : 0;
                string workOrderIDs = (filters.ContainsKey("workOrderIDs") && filters["workOrderIDs"] != null) ? filters["workOrderIDs"].ToString() : null;
                int branchID = (filters.ContainsKey("branchID") && filters["branchID"] != null && !string.IsNullOrEmpty(filters["branchID"].ToString())) ? Convert.ToInt32(filters["branchID"].ToString()) : 0;

                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        //update column OldQuantity
                        var dbChangeItem = context.ReceivingNoteDetail.Where(o => o.ReceivingNoteID == id).ToList();
                        foreach (var item in dbChangeItem)
                        {
                            item.OldQuantity = item.Quantity;
                        }
                        context.SaveChanges();

                        ReceivingNoteMng_ReceivingNote_View dbItem;
                        dbItem = context.ReceivingNoteMng_ReceivingNote_View.FirstOrDefault(o => o.ReceivingNoteID == id);
                        editFormData.Data = converter.DB2DTO_ReceivingNote(dbItem);
                        //Get Unit And SubUnit for ProductionItem
                        foreach (var item in editFormData.Data.ReceivingNoteDetailDTOs)
                        {
                            //Get SubUnit Data
                            var unitItem = context.ReceivingNoteMng_Function_ProductionItemUnitByValidDate(dbItem.ReceivingNoteDate, item.ProductionItemID).ToList();
                            if(item.SubUnitID != null)
                            {
                                //Main Unit
                                var dataUnit = unitItem.FirstOrDefault(o => o.UnitID == item.UnitID);
                                item.ConversionFactorMainUnit = dataUnit.ConversionFactor;
                                //SubUnit
                                var dataSubUnit = unitItem.FirstOrDefault(o => o.UnitID == item.SubUnitID);
                                item.ConversionFactorSubUnit = dataSubUnit.ConversionFactor;

                                item.ConversionFactorAffter = item.ConversionFactorSubUnit / item.ConversionFactorMainUnit;
                            }
                            //Mapping data 2 SubUnit
                            item.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(unitItem));
                        }
                        workOrderIDs = dbItem.WorkOrderIDs;

                        if (dbItem.WorkCenterID == 7 || dbItem.WorkCenterID == 8)
                        {
                            foreach (var item in editFormData.Data.ReceivingNoteDetailDTOs)
                            {
                                if (item != null && item.FrameWeight.HasValue && item.FrameWeight.Value != 0)
                                {
                                    item.IsHasFrameWeight = true;
                                }
                                else
                                {
                                    item.IsHasFrameWeight = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        editFormData.Data.WorkOrderIDs = workOrderIDs;
                        editFormData.Data.ReceivingNoteDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    if (!string.IsNullOrEmpty(workOrderIDs))
                    {
                        // Case list wok-order
                        editFormData.Data.StatusTypeID = 1; // From Production

                        string[] arrayIDs = workOrderIDs.Split(',');
                        foreach (string itemIDs in arrayIDs)
                        {
                            int ids = int.Parse(itemIDs);
                            DTO.WorkOrderDTO workOrderDTO = new DTO.WorkOrderDTO();
                            WorkOrder wo = context.WorkOrder.Where(o => o.WorkOrderID == ids).FirstOrDefault();
                            if (wo != null)
                            {
                                workOrderDTO.WorkOrderID = wo.WorkOrderID;
                                workOrderDTO.WorkOrderUD = wo.WorkOrderUD;
                                editFormData.WorkOrderList.Add(workOrderDTO);
                            }
                        }

                        editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 1, StatusTypeNM = "From Production" });
                    }
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 2, StatusTypeNM = "From Purchasing" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 3, StatusTypeNM = "From Other" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 4, StatusTypeNM = "From Outsource" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 5, StatusTypeNM = "From Transfer Warehouse" });
                    editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 6, StatusTypeNM = "Return From SO" });

                    // 
                    //get support list
                    //
                    Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                    editFormData.FactoryWarehousePallets = supportFactory.GetFactoryWarehousePallet(companyID);
                    editFormData.ProductionTeams = supportFactory.GetProductionTeam(companyID);
                    editFormData.ProductionItemTypes = supportFactory.GetProductionItemType();
                    editFormData.FactoryWarehouses = AutoMapper.Mapper.Map<List<ReceivingNoteMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.ReceivingNoteMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID && o.BranchID == branchID).ToList());
                    editFormData.TransportationServiceDTOs = AutoMapper.Mapper.Map<List<ReceivingNoteMng_TransportationService_View>, List<DTO.TransportationServiceDTO>>(context.ReceivingNoteMng_TransportationService_View.ToList());
                    //get workcenter
                    var workCenters = converter.DB2DTO_WorkCenter(context.ReceivingNoteMng_function_GetWorkCenter(editFormData.Data.WorkOrderIDs).Where(o => o.WorkCenterID != 10).ToList()); // Remove workcenter is finishing
                    int? workCenterID = null;
                    foreach (var item in workCenters)
                    {
                        if (workCenterID == null || workCenterID != item.WorkCenterID)
                        {
                            workCenterID = item.WorkCenterID;
                            editFormData.WorkCenters.Add(item);
                        }
                    }
                    if (editFormData.WorkCenters.Count == 0)
                    {
                        editFormData.WorkCenters = AutoMapper.Mapper.Map<List<WorkCenter>, List<DTO.WorkCenter>>(context.WorkCenter.Where(o => o.WorkCenterID != 10).ToList()); // Remove workcenter is finishing
                    }
                    //get reason for other price list
                    editFormData.ReasonOtherPrices.Add(new { ReasonOtherPriceID = 1, ReasonOtherPriceName = "A" });
                    editFormData.ReasonOtherPrices.Add(new { ReasonOtherPriceID = 2, ReasonOtherPriceName = "B" });
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return editFormData;
        }

        /// <summary>
        /// get receiving note without workOrder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            editFormData.Data = new DTO.ReceivingNoteDTO();
            editFormData.Data.ReceivingNoteDetailDTOs = new List<DTO.ReceivingNoteDetailDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (ReceivingNoteEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ReceivingNoteMng_ReceivingNote_View dbItem;
                        dbItem = context.ReceivingNoteMng_ReceivingNote_View.Include("ReceivingNoteMng_ReceivingNoteDetail_View").FirstOrDefault(o => o.ReceivingNoteID == id);
                        editFormData.Data = converter.DB2DTO_ReceivingNote(dbItem);
                    }
                    else
                    {
                        editFormData.Data.ReceivingNoteDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.StatusTypeID = 1;
                    }

                    // Get support FactoryWarehouse (new version).
                    editFormData.FactoryWarehouses = AutoMapper.Mapper.Map<List<ReceivingNoteMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.ReceivingNoteMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }

                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                editFormData.FactoryWarehousePallets = support_factory.GetFactoryWarehousePallet(companyID);

                editFormData.StatusTypes = new List<DTO.StatusTypeDTO>();
                editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 1, StatusTypeNM = "From Production" });
                editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 2, StatusTypeNM = "From Purchasing" });
                editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 3, StatusTypeNM = "From Other" });
                editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 4, StatusTypeNM = "From Outsource" });
                editFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 5, StatusTypeNM = "From Transfer Warehouse" });
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return editFormData;
        }

        //public List<DTO.ProductionItem> ReceivingProductionItemFromTeamToWarehouse(int userId, Hashtable filters)
        //{
        //    List<DTO.ProductionItem> result = new List<DTO.ProductionItem>();
        //    //get companyID
        //    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        //    int? companyID = fw_factory.GetCompanyID(userId);
        //    using (ReceivingNoteEntities context = this.CreateContext())
        //    {


        //        string searchQuery = filters["searchQuery"].ToString();
        //        string workOrderIDs = filters["workOrderIDs"].ToString();
        //        int? workCenterID = null;
        //        if (filters["workCenterID"] != null)
        //        {
        //            workCenterID = Convert.ToInt32(filters["workCenterID"]);
        //        }
        //        bool? isInit = Convert.ToBoolean(filters["isInit"]);
        //        var dbItems = context.ReceivingNoteMng_function_ReceivingProductionItemFromTeamToWarehouse(companyID, searchQuery, workOrderIDs, workCenterID, isInit).ToList();
        //        return AutoMapper.Mapper.Map<List<ReceivingNoteMng_ProductionItem_View>, List<DTO.ProductionItem>>(dbItems);
        //    }
        //}

        //public List<DTO.ProductionItem> ReceivingProductionItemFromPOToWarehouse(int userId, Hashtable filters)
        //{
        //    List<DTO.ProductionItem> result = new List<DTO.ProductionItem>();
        //    //get companyID
        //    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        //    int? companyID = fw_factory.GetCompanyID(userId);
        //    using (ReceivingNoteEntities context = this.CreateContext())
        //    {
        //        string searchQuery = filters["searchQuery"].ToString();
        //        string workOrderIDs = filters["workOrderIDs"].ToString();
        //        int? workCenterID = null;
        //        if (filters["workCenterID"] != null)
        //        {
        //            workCenterID = Convert.ToInt32(filters["workCenterID"]);
        //        }
        //        bool? isInit = Convert.ToBoolean(filters["isInit"]);
        //        var dbItems = context.ReceivingNoteMng_function_ReceivingProductionItemFromPOToWarehouse(companyID, searchQuery, workOrderIDs, workCenterID, isInit).ToList();
        //        return converter.DB2DTO_ProductionItem(dbItems);
        //    }
        //}

        public string GetReceivingNotePrintout(int receivingNoteID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReceivingNoteMng_function_GetReceivingNotePrintout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ReceivingNoteID", receivingNoteID);
                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.Fill(ds);
                return Library.Helper.CreateReceiptPrintout(ds.Tables["Receipt"], ds.Tables["ReceiptDetail"], "ReceivingNotePrintout.rdlc");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }

        public DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                int? companyID = null;
                string receivingNoteUD = null;
                string receivingNoteDate = null;
                string purchasingOrderNo = null;
                string workCenterNM = null;
                string fromProductionTeamNM = null;

                string workOrderUD = null;
                string modelUD = null;
                string modelNM = null;
                string productArticleCode = null;
                string productDescription = null;

                string description = null;
                string updatorName = null;
                string updatedDate = null;
                int? receivingNoteTypeID = null;
                string workCenterAndDeliverName = null;
                string fromProductionTeamAndDeliverAddress = null;

                string fromReceivingNoteDate = null;
                string toReceivingNoteDate = null;
                int? statusTypeID = null;
                string fromUpdatedDate = null;
                string toUpdatedDate = null;
                int? workCenterID = null;
                int? fromProductionTeamID = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("receivingNoteUD") && !string.IsNullOrEmpty(filters["receivingNoteUD"].ToString()))
                {
                    receivingNoteUD = filters["receivingNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receivingNoteDate") && !string.IsNullOrEmpty(filters["receivingNoteDate"].ToString()))
                {
                    receivingNoteDate = filters["receivingNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchasingOrderNo") && !string.IsNullOrEmpty(filters["purchasingOrderNo"].ToString()))
                {
                    purchasingOrderNo = filters["purchasingOrderNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workCenterNM") && !string.IsNullOrEmpty(filters["workCenterNM"].ToString()))
                {
                    workCenterNM = filters["workCenterNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamNM") && !string.IsNullOrEmpty(filters["fromProductionTeamNM"].ToString()))
                {
                    fromProductionTeamNM = filters["fromProductionTeamNM"].ToString().Replace("'", "''");
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
                    updatedDate = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receivingNoteTypeID") && filters["receivingNoteTypeID"] != null)
                {
                    receivingNoteTypeID = Convert.ToInt32(filters["receivingNoteTypeID"]);
                }
                if (filters.ContainsKey("workCenterAndDeliverName") && !string.IsNullOrEmpty(filters["workCenterAndDeliverName"].ToString()))
                {
                    workCenterAndDeliverName = filters["workCenterAndDeliverName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamAndDeliverAddress") && !string.IsNullOrEmpty(filters["fromProductionTeamAndDeliverAddress"].ToString()))
                {
                    fromProductionTeamAndDeliverAddress = filters["fromProductionTeamAndDeliverAddress"].ToString().Replace("'", "''");
                }

                //search detail
                if (filters.ContainsKey("fromReceivingNoteDate") && !string.IsNullOrEmpty(filters["fromReceivingNoteDate"].ToString()))
                {
                    fromReceivingNoteDate = filters["fromReceivingNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toReceivingNoteDate") && !string.IsNullOrEmpty(filters["toReceivingNoteDate"].ToString()))
                {
                    toReceivingNoteDate = filters["toReceivingNoteDate"].ToString().Replace("'", "''");
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

                using (ReceivingNoteEntities context = CreateContext())
                {
                    totalRows = context.ReceivingNoteMng_function_SearchReceivingNote(companyID, orderBy, orderDirection, receivingNoteUD, receivingNoteDate, purchasingOrderNo, workCenterNM, fromProductionTeamNM, workOrderUD, modelUD, modelNM, productArticleCode, productDescription, description, updatorName, updatedDate, receivingNoteTypeID, workCenterAndDeliverName, fromProductionTeamAndDeliverAddress, fromReceivingNoteDate, toReceivingNoteDate, statusTypeID, fromUpdatedDate, toUpdatedDate, workCenterID, fromProductionTeamID).Count();
                    var result = context.ReceivingNoteMng_function_SearchReceivingNote(companyID, orderBy, orderDirection, receivingNoteUD, receivingNoteDate, purchasingOrderNo, workCenterNM, fromProductionTeamNM, workOrderUD, modelUD, modelNM, productArticleCode, productDescription, description, updatorName, updatedDate, receivingNoteTypeID, workCenterAndDeliverName, fromProductionTeamAndDeliverAddress, fromReceivingNoteDate, toReceivingNoteDate, statusTypeID, fromUpdatedDate, toUpdatedDate, workCenterID, fromProductionTeamID);
                    searchFormData.Data = converter.DB2DTO_ReceivingNoteSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    //Add Status
                    searchFormData.StatusTypes = new List<DTO.StatusTypeDTO>();
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 1, StatusTypeNM = "From Production" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 2, StatusTypeNM = "From Purchasing" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 3, StatusTypeNM = "From Other" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 4, StatusTypeNM = "From Outsource" });
                    searchFormData.StatusTypes.Add(new DTO.StatusTypeDTO() { StatusTypeID = 5, StatusTypeNM = "From Transfer Warehouse" });

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
                                item.StatusTypeNM = "From Purchasing";
                            }
                            else
                            {
                                if (item.StatusTypeID == 3)
                                {
                                    item.StatusTypeNM = "From Other";
                                }
                                else
                                {
                                    if(item.StatusTypeID == 4)
                                    {
                                        item.StatusTypeNM = "From Outsource";
                                    }
                                    else
                                    {
                                        item.StatusTypeNM = "From Transfer Warehouse";
                                    }
                                }
                            }
                        }
                    }

                    int hasReceivingNoteType = 0;
                    hasReceivingNoteType = searchFormData.Data.Where(o => o.ReceivingNoteTypeID != null).Count();
                    if (hasReceivingNoteType == 0)
                    {
                        //load workorder
                        if (searchFormData.Data.Count > 0)
                        {
                            int? numberWO = searchFormData.Data.Where(o => !string.IsNullOrEmpty(o.WorkOrderIDs)).Count();
                            if (numberWO != 0 || numberWO != null)
                            {
                                string x = searchFormData.Data.Where(o => !string.IsNullOrEmpty(o.WorkOrderIDs)).Select(s => s.WorkOrderIDs).Aggregate((i, j) => i + "," + j);
                                string[] woIDs = x.Split(',');
                                List<int> woIDi = new List<int>();
                                woIDi = woIDs.Select(int.Parse).ToList();
                                var allWorkOrders = context.ReceivingNoteMng_WorkOrder_SearchView.Where(o => woIDi.Contains(o.WorkOrderID.Value)).ToList();
                                foreach (var item in searchFormData.Data)
                                {
                                    item.WorkOrderSearchDTOs = new List<DTO.WorkOrderSearchDTO>();
                                    if (item.WorkOrderIDs != null)
                                    {
                                        List<int> _woID = item.WorkOrderIDs.Split(',').Select(int.Parse).ToList();
                                        var _wo = allWorkOrders.Where(o => _woID.Contains(o.WorkOrderID.Value)).ToList();
                                        foreach (var woItem in _wo)
                                        {
                                            if (woItem.ReceivingNoteID.Value == item.ReceivingNoteID)
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

        //private List<DTO.BOMDTO> GetBom(int? workOrderID, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    List<DTO.BOMDTO> data = new List<DTO.BOMDTO>();
        //    try
        //    {
        //        using (ReceivingNoteEntities context = CreateContext())
        //        {
        //            data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_BOM_View>, List<DTO.BOMDTO>>(context.ReceivingNoteMng_BOM_View.Where(o => o.WorkOrderID == workOrderID).ToList());
        //            return data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.GetInnerException(ex).Message;
        //        return new List<DTO.BOMDTO>();
        //    }
        //}

        /// <summary>
        /// Export excel manage QRCode in Factory warehouse.
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public string GetExportBarcode(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            string fileName = string.Empty;

            try
            {
                ReceivingNoteReportData ds2 = new ReceivingNoteReportData();
                SqlDataAdapter adap = new SqlDataAdapter();

                adap.SelectCommand = new SqlCommand("ReportMng_ReceivingNoteDefault_function_PrintHangTag", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@productionItemIDs", filters["productionItemID"].ToString().Trim());
                adap.SelectCommand.Parameters.AddWithValue("@qntBarcodes", filters["qntBarcode"].ToString().Trim());

                adap.TableMappings.Add("Table", "ReportMng_SampleOrder_HangTag_View");
                adap.Fill(ds2);

                ds2.AcceptChanges();

                foreach (var item in ds2.ReportMng_SampleOrder_HangTag_View.Where(o => !o.IsProductionItemUDNull()))
                {
                    item.BarCode = Library.Helper.QRCodeImageFile(item.ProductionItemUD);
                }

                fileName = Library.Helper.CreateReportFileWithEPPlus(ds2, "ReceivingNoteDefault_HangTag");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }

        public DTO.ReceivingNotePrintoutDTO GetReceivingNotePrintoutHTML(int ReceivingNoteID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReceivingNotePrintoutDTO ReceivingNotePrintout = new DTO.ReceivingNotePrintoutDTO();
            ReceivingNotePrintout.ReceivingNoteDetailPrintoutDTOs = new List<DTO.ReceivingNoteDetailPrintoutDTO>();
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReceivingNoteMng_function_GetReceivingNotePrintout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ReceivingNoteID", ReceivingNoteID);
                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.Fill(ds);

                //read to delivery note
                var receivingNote = ds.Receipt.FirstOrDefault();
                ReceivingNotePrintout.ReceiptNo = receivingNote.ReceiptNo;
                ReceivingNotePrintout.ReceiptDate = receivingNote.ReceiptDate;
                ReceivingNotePrintout.DeliverName = receivingNote.DeliverName;
                ReceivingNotePrintout.DeliverAddress = receivingNote.DeliverAddress;
                ReceivingNotePrintout.ReceiverName = receivingNote.ReceiverName;
                ReceivingNotePrintout.ReceiverAddress = receivingNote.ReceiverAddress;
                ReceivingNotePrintout.StockName = receivingNote.StockName;
                ReceivingNotePrintout.StockAddress = receivingNote.StockAddress;
                ReceivingNotePrintout.Reason = receivingNote.Reason;
                ReceivingNotePrintout.DayReceipt = receivingNote.DayReceipt;
                ReceivingNotePrintout.MonthReceipt = receivingNote.MonthReceipt;
                ReceivingNotePrintout.YearReceipt = receivingNote.YearReceipt;

                //read delivery note detail
                DTO.ReceivingNoteDetailPrintoutDTO receivingNoteDetail;
                foreach (var item in ds.ReceiptDetail)
                {
                    receivingNoteDetail = new DTO.ReceivingNoteDetailPrintoutDTO();
                    ReceivingNotePrintout.ReceivingNoteDetailPrintoutDTOs.Add(receivingNoteDetail);

                    receivingNoteDetail.ProductionItemNM = item.FactoryMaterialNM;
                    receivingNoteDetail.ProductionItemUD = item.FactoryMaterialUD;
                    receivingNoteDetail.UnitNM = item.UnitNM;
                    receivingNoteDetail.Quantity = item.Quantity;
                    receivingNoteDetail.Price = item.Price;
                    receivingNoteDetail.Amount = item.Amount;
                    receivingNoteDetail.Remark = item.Remark;
                    receivingNoteDetail.ClientID = item.ClientID;
                    receivingNoteDetail.WorkOrderUD = item.WorkOrderUD;
                    receivingNoteDetail.Weight = item.Weight;
                }
                return ReceivingNotePrintout;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return ReceivingNotePrintout;
            }
        }

        public List<DTO.SubSupplier> GetSubSupplier(string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SubSupplier> data = new List<DTO.SubSupplier>();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_SubSupplier(context.SupportMng_SubSupplierQuickSearch_View.Where(o => o.Value.Contains(searchQuery) || o.Label.Contains(searchQuery)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public bool DeleteWithPermission(int id, int createdBy, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReceivingNoteEntities context = CreateContext())
                {
                    var dbItem = context.ReceivingNote.FirstOrDefault(o => o.ReceivingNoteID == id);

                    if (dbItem == null)
                    {
                        // return message notification.
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Can not find Data !" };
                        return false;
                    }

                    if (createdBy != 0)
                    {
                        if (dbItem.CreatedBy.Value == userId)
                        {
                            // execute delete id.
                            context.ReceivingNote.Remove(dbItem);
                            context.SaveChanges();
                            return true;
                        }

                        if (dbItem.CreatedBy.Value != userId)
                        {
                            // execute delete id.
                            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Only the created has the right to delete" };
                            return false;
                        }
                    }
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

        public List<DTO.PurchaseOrderDTO> GetPurchaseOrder(int userId, string purchaseOrderUD, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PurchaseOrderDTO> data = new List<DTO.PurchaseOrderDTO>();
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (ReceivingNoteEntities context = CreateContext())
                {
                    var x = context.ReceivingNoteMng_PurchaseOrder_View.Where(o => o.CompanyID == companyID && o.PurchaseOrderUD.Contains(purchaseOrderUD)).ToList();
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_PurchaseOrder_View>, List<DTO.PurchaseOrderDTO>>(x);
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.PurchaseOrderDTO>();
            }
        }

        public List<DTO.ProductionItem> GetProductionItem(string searchQuery, int? branchID, string receivingNoteDate, string workOrderIDs, int? workCenterID, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };

            List<DTO.ProductionItem> data = new List<DTO.ProductionItem>();

            try
            {
                // Pre data param to get data ProductionItem
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    var dbItem = context.ReceivingNoteMng_function_GetProductionItem(receivingNoteDate, searchQuery, companyID, null, workOrderIDs, workCenterID).Where(o => o.BranchID == branchID);
                    var dbItemWithBranch = dbItem.ToList();
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_function_GetProductionItem_Result>, List<DTO.ProductionItem>>(dbItemWithBranch);
                    foreach (var item in data)
                    {
                        var unitItem = context.ReceivingNoteMng_Function_ProductionItemUnitByValidDate(receivingNoteDate.ConvertStringToDateTime(), item.ProductionItemID).ToList();
                        item.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(unitItem));
                    }
                    // Pre data return front-end
                    foreach (var item in data)
                    {
                        if (item != null && item.FrameWeight.HasValue && item.FrameWeight.Value != 0)
                        {
                            item.IsHasFrameWeight = true;
                        }
                        else
                        {
                            item.IsHasFrameWeight = false;
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

        public List<DTO.BOMDTO> GetBOM(int userID, int branchID, string workOrderIDs, string receivingNoteDate, int? workCenterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.BOMDTO> data = new List<DTO.BOMDTO>();
            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_BOM_View>, List<DTO.BOMDTO>>(context.ReceivingNoteMng_function_GetBom(workOrderIDs, branchID, receivingNoteDate).ToList());

                    // Get production item unit(sub) include (HeSoQuiDoi)
                    foreach (var item in data)
                    {
                        if (item != null && item.ProductionItemID != null)
                        {
                            //set again warehouse under WorkOrder begin 'A' default warehouse: FINISHED PRODUCT and 'B' default warehouse: PAL2-FINISHED-PRODUCT for workcenter = packaging
                            if(workCenterID != null && workCenterID == 11)
                            {
                                if (item.WorkOrderUD.Substring(0, 1) == "A")
                                {
                                    item.DefaultFactoryWarehouseID = 46;
                                }
                                else
                                {
                                    if (item.WorkOrderUD.Substring(0, 1) == "B")
                                    {
                                        item.DefaultFactoryWarehouseID = 68;
                                    }
                                }
                            }

                            // Set value for input(not input) FrameWeight
                            if (item != null && item.FrameWeight.HasValue && item.FrameWeight.Value != 0)
                            {
                                item.IsHasFrameWeight = true;
                            }
                            else
                            {
                                item.IsHasFrameWeight = false;
                            }

                            item.ProductionItemUnits = AutoMapper.Mapper.Map<List<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(context.ReceivingNoteMng_Function_ProductionItemUnitByValidDate(receivingNoteDate.ConvertStringToDateTime(), item.ProductionItemID).ToList());
                        }
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

        public string GetReceivingNoteExportToExcelList(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ListReceivingNoteDataObject ds = new ListReceivingNoteDataObject();

            //try to get data
            try
            {
                int? companyID = null;
                string receivingNoteUD = null;
                string receivingNoteDate = null;
                string purchasingOrderNo = null;
                string workCenterNM = null;
                string fromProductionTeamNM = null;

                string workOrderUD = null;
                string modelUD = null;
                string modelNM = null;
                string productArticleCode = null;
                string productDescription = null;

                string description = null;
                string updatorName = null;
                string updatedDate = null;
                int? receivingNoteTypeID = null;
                string workCenterAndDeliverName = null;
                string fromProductionTeamAndDeliverAddress = null;

                string fromReceivingNoteDate = null;
                string toReceivingNoteDate = null;
                int? statusTypeID = null;
                string statusTypeNM = null;
                string fromUpdatedDate = null;
                string toUpdatedDate = null;
                int? workCenterID = null;
                int? fromProductionTeamID = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("receivingNoteUD") && !string.IsNullOrEmpty(filters["receivingNoteUD"].ToString()))
                {
                    receivingNoteUD = filters["receivingNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receivingNoteDate") && !string.IsNullOrEmpty(filters["receivingNoteDate"].ToString()))
                {
                    receivingNoteDate = filters["receivingNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchasingOrderNo") && !string.IsNullOrEmpty(filters["purchasingOrderNo"].ToString()))
                {
                    purchasingOrderNo = filters["purchasingOrderNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workCenterNM") && !string.IsNullOrEmpty(filters["workCenterNM"].ToString()))
                {
                    workCenterNM = filters["workCenterNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamNM") && !string.IsNullOrEmpty(filters["fromProductionTeamNM"].ToString()))
                {
                    fromProductionTeamNM = filters["fromProductionTeamNM"].ToString().Replace("'", "''");
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
                    updatedDate = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receivingNoteTypeID") && filters["receivingNoteTypeID"] != null)
                {
                    receivingNoteTypeID = Convert.ToInt32(filters["receivingNoteTypeID"]);
                }
                if (filters.ContainsKey("workCenterAndDeliverName") && !string.IsNullOrEmpty(filters["workCenterAndDeliverName"].ToString()))
                {
                    workCenterAndDeliverName = filters["workCenterAndDeliverName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromProductionTeamAndDeliverAddress") && !string.IsNullOrEmpty(filters["fromProductionTeamAndDeliverAddress"].ToString()))
                {
                    fromProductionTeamAndDeliverAddress = filters["fromProductionTeamAndDeliverAddress"].ToString().Replace("'", "''");
                }

                //search detail
                if (filters.ContainsKey("fromReceivingNoteDate") && !string.IsNullOrEmpty(filters["fromReceivingNoteDate"].ToString()))
                {
                    fromReceivingNoteDate = filters["fromReceivingNoteDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("toReceivingNoteDate") && !string.IsNullOrEmpty(filters["toReceivingNoteDate"].ToString()))
                {
                    toReceivingNoteDate = filters["toReceivingNoteDate"].ToString().Replace("'", "''");
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
                adap.SelectCommand = new SqlCommand("ReceivingNoteMng_function_ExportToExcelListReceivingNote", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.SelectCommand.Parameters.AddWithValue("@ReceivingNoteUD", receivingNoteUD);
                adap.SelectCommand.Parameters.AddWithValue("@ReceivingNoteDate", receivingNoteDate);
                adap.SelectCommand.Parameters.AddWithValue("@PurchasingOrderNo", purchasingOrderNo);
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterNM", workCenterNM);
                adap.SelectCommand.Parameters.AddWithValue("@FromProductionTeamNM", fromProductionTeamNM);
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderUD", workOrderUD);
                adap.SelectCommand.Parameters.AddWithValue("@ModelUD", modelUD);
                adap.SelectCommand.Parameters.AddWithValue("@ModelNM", modelNM);
                adap.SelectCommand.Parameters.AddWithValue("@ProductArticleCode", productArticleCode);
                adap.SelectCommand.Parameters.AddWithValue("@ProductDescription", productDescription);
                adap.SelectCommand.Parameters.AddWithValue("@Description", description);
                adap.SelectCommand.Parameters.AddWithValue("@UpdatorName", updatorName);
                adap.SelectCommand.Parameters.AddWithValue("@UpdatedDate", updatedDate);
                adap.SelectCommand.Parameters.AddWithValue("@ReceivingNoteTypeID", receivingNoteTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterAndDeliverName", workCenterAndDeliverName);
                adap.SelectCommand.Parameters.AddWithValue("@FromProductionTeamAndDeliverAddress ", fromProductionTeamAndDeliverAddress);
                adap.SelectCommand.Parameters.AddWithValue("@FromReceivingNoteDate", fromReceivingNoteDate);
                adap.SelectCommand.Parameters.AddWithValue("@ToReceivingNoteDate", toReceivingNoteDate);
                adap.SelectCommand.Parameters.AddWithValue("@StatusTypeID", statusTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@StatusTypeNM", statusTypeNM);
                adap.SelectCommand.Parameters.AddWithValue("@FromUpdatedDate", fromUpdatedDate);
                adap.SelectCommand.Parameters.AddWithValue("@ToUpdatedDate", toUpdatedDate);
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterID", workCenterID);
                adap.SelectCommand.Parameters.AddWithValue("@FromProductionTeamID", fromProductionTeamID);
                adap.TableMappings.Add("Table", "ExportToExcelReceivingNote");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListReceivingNote");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                return string.Empty;
            }
        }

        public List<DTO.WorkOrderItemDTO> GetWorkOrderItems(int userID, int productionItemID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.WorkOrderItemDTO> data = new List<DTO.WorkOrderItemDTO>();

            try
            {
                using (ReceivingNoteEntities context = CreateContext())
                {
                    List<ReceivingNoteMng_WorkOrderItem_View> dbItem = context.ReceivingNoteMng_WorkOrderItem_View.Where(o => o.ProductionItemID == productionItemID).ToList();

                    if (dbItem == null || dbItem.Count() == 0)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "No search result data!";
                        return data;
                    }

                    data = converter.DB2DTO_WorkOrderItem(dbItem);
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public object GetPurchaseOrderDetail(int purchaseOrderID, int branchID, string receivingNoteDate, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.PurchaseOrderDetailDTO> data = new List<DTO.PurchaseOrderDetailDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ReceivingNoteMng_PurchaseOrderDetail_View.Where(o => o.PurchaseOrderID == purchaseOrderID && o.BranchID == branchID);
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_PurchaseOrderDetail_View>, List<DTO.PurchaseOrderDetailDTO>>(dbItem.ToList());

                    foreach (var item in data)
                    {
                        //Get SubUnit Data
                        var unitItem = context.ReceivingNoteMng_Function_ProductionItemUnitByValidDate(receivingNoteDate.ConvertStringToDateTime(), item.ProductionItemID).ToList();
                        if (item.UnitID != null)
                        {
                            //Main Unit
                            var dataUnit = unitItem.FirstOrDefault(o => o.UnitID == item.UnitID);
                            item.ConversionFactorMainUnit = dataUnit.ConversionFactor;
                        }
                        //Mapping data 2 SubUnit
                        item.productionItemUnits.AddRange(AutoMapper.Mapper.Map<List<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(unitItem));
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

        public object SetContentDetail(int productionItemID, int factoryWarehouseID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.SetContentDetailDTO data = new DTO.SetContentDetailDTO();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ReceivingNoteMng_function_SetContentDetail(productionItemID, factoryWarehouseID).FirstOrDefault();
                    data = AutoMapper.Mapper.Map<ReceivingNoteMng_SetContentDetail_View, DTO.SetContentDetailDTO>(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetBOMProductionItem(int userID, string productionItemUD, string workOrderIDs, int? workCenterID, int? branchID, string receivingNoteDate, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.BOMProductionDTO> data = new List<DTO.BOMProductionDTO>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    var dbItem = context.ReceivingNoteMng_function_GetBOMProductionItem(receivingNoteDate, productionItemUD, companyID, branchID, workOrderIDs, workCenterID);
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_BOMProduction_View>, List<DTO.BOMProductionDTO>>(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
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

        public void SendEmailNotification(DTO.ReceivingNoteDTO receivingNote, string notificationGroup, string baseUrl, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            bool isInvalid = false;

            try
            {
                foreach (var item in receivingNote.ReceivingNoteDetailDTOs)
                {
                    if (item != null)
                    {
                        if (item.Quantity.Value > item.OrderQnt.Value)
                        {
                            isInvalid = true;
                            break;
                        }
                    }
                }

                if (isInvalid)
                {
                    string emailSubject = "[Tilsoft] - Receiving Note - Receiving quantity larger than order quantity";
                    string emailBody = baseUrl + "ReceivingNote/PO2WarehouseWithoutWorkOrder/" + receivingNote.ReceivingNoteID;
                    string sendTo = null;

                    using (var context = CreateContext())
                    {
                        var notificationGroupMember = context.ReceivingNoteMng_function_NotificationGroupMember(notificationGroup).ToList();
                        foreach (var item in notificationGroupMember)
                        {
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(sendTo))
                                {
                                    sendTo += ", ";
                                }

                                sendTo += item.Email;
                            }
                        }

                        EmailNotificationMessage dbItem = new EmailNotificationMessage();
                        context.EmailNotificationMessage.Add(dbItem);

                        dbItem.EmailSubject = emailSubject;
                        dbItem.EmailBody = emailBody;
                        dbItem.SendTo = sendTo;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
        }

        public bool UpdateFactoryProductionMonitiring(int userID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            try
            {
                using (var context = CreateContext())
                {
                    string startDate = "02/09/2019";
                    string endDate = "05/10/2019";
                    DateTime? valDateStart = startDate.ConvertStringToDateTime();
                    DateTime? valDateEnd = endDate.ConvertStringToDateTime();
                    List<DTO.ReceivingNoteDTO> lisDTOReceivingNote = new List<DTO.ReceivingNoteDTO>();
                    lisDTOReceivingNote = AutoMapper.Mapper.Map<List<ReceivingNoteMng_ReceivingNote_View>, List<DTO.ReceivingNoteDTO>>(context.ReceivingNoteMng_ReceivingNote_View.Where(o => o.ReceivingNoteDate > valDateStart && o.ReceivingNoteDate <= valDateEnd && o.WorkCenterID == 11 && o.StatusTypeID == 1).ToList());
                    //Update Component # Piece for FactoryProductionStatus
                    #region Update FactoryOrderDetail for FactoryProductionStatus
                    for (int z = 0; z < lisDTOReceivingNote.Count; z++)
                    {
                        var dtoReceivingNote = lisDTOReceivingNote[z];
                        //Piece
                        foreach (var item in dtoReceivingNote.ReceivingNoteDetailDTOs.ToList())
                        {
                            decimal qty = 0;
                            if (item.OldQuantity == null)
                            {
                                if (item.Quantity != null)
                                {
                                    qty = (int)item.Quantity;
                                }
                            }
                            else
                            {
                                if (item.Quantity != null)
                                {
                                    if (item.OldQuantity != null)
                                    {
                                        qty = (int)item.Quantity - (int)item.OldQuantity;
                                    }
                                    else
                                    {
                                        qty = (int)item.Quantity;
                                    }

                                }
                                else
                                {
                                    if (item.OldQuantity != null)
                                    {
                                        qty = 0 - (int)item.OldQuantity;
                                    }
                                }
                            }
                            //1 WorkOrder have 1 WorkOrderDetail. Confirm from Mr Hanh
                            var modelSetWO = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.WorkOrderID == item.WorkOrderID).FirstOrDefault();
                            var modelUDSetWO = modelSetWO.ArticleCode.Substring(0, 4).ToString();
                            var listModelSet = context.ReceivingNoteMng_ListModel_Set_View.Where(o => o.ModelUD == modelUDSetWO).FirstOrDefault();
                            if (listModelSet == null) // Type != Set
                            {
                                var dbWOItem = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.ArticleCode == item.ProductionItemUD && o.WorkOrderID == item.WorkOrderID).ToList();
                                int? weekNo = null;
                                var season = GetSeason(dtoReceivingNote.ReceivingNoteDate);
                                foreach (var subItem in dbWOItem.ToList())
                                {
                                    var seasonDate = supportFactory.GetWeekInSeason(season).ToList();
                                    for (int i = 0; i < seasonDate.Count; i++)
                                    {
                                        var seasonItem = seasonDate[i];
                                        DateTime? valStartDate = seasonItem.StartDate.ConvertStringToDateTime();
                                        DateTime? valEndDate = seasonItem.EndDate.ConvertStringToDateTime();
                                        if (dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() >= valStartDate && dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() <= valEndDate)
                                        {
                                            weekNo = seasonItem.WeekNo;
                                            break;
                                        }
                                    }
                                    context.ReceivingNoteMng_function_UpdateFactoryProductionStatus(subItem.FactoryOrderDetailID, subItem.FactoryOrderID, subItem.FactoryID, season, qty, weekNo);
                                }
                            }
                        }

                        //Set
                        var list_ReceivingNoteDetail = dtoReceivingNote.ReceivingNoteDetailDTOs.OrderBy(o => o.WorkOrderID).ToList();
                        int? workOrderID = null;
                        for (int i = 0; i < list_ReceivingNoteDetail.Count; i++)
                        {
                            var item = list_ReceivingNoteDetail[i];
                            if (item.WorkOrderID != workOrderID)
                            {
                                workOrderID = item.WorkOrderID;
                                var modelUD = item.ProductionItemUD.Substring(0, 4).ToString();

                                //1 WorkOrder have 1 WorkOrderDetail. Confirm from Mr Hanh
                                var modelSetWO = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.WorkOrderID == item.WorkOrderID).FirstOrDefault();
                                var modelUDSetWO = modelSetWO.ArticleCode.Substring(0, 4).ToString();
                                var listModelSet = context.ReceivingNoteMng_ListModel_Set_View.Where(o => o.ModelUD == modelUDSetWO).FirstOrDefault();
                                if (listModelSet != null)
                                {
                                    var setModelUD = listModelSet.ModelUD;

                                    // lay qnt nho nhat trong từng WorkOrder để làm qnt cho Model = set
                                    int qty_SET = GetQnt_ModelSet_ReceivingNoteDetail(item, list_ReceivingNoteDetail);

                                    //
                                    var dbWOItem = context.ReceivingNoteMng_ListWorkOrder_FactoryOrderDetail_View.Where(o => o.ArticleCode.Contains(setModelUD) && o.WorkOrderID == item.WorkOrderID).ToList();
                                    int? weekNo = null;
                                    var season = GetSeason(dtoReceivingNote.ReceivingNoteDate);
                                    foreach (var subItem in dbWOItem.ToList())
                                    {
                                        var seasonDate = supportFactory.GetWeekInSeason(season).ToList();
                                        for (int j = 0; j < seasonDate.Count; j++)
                                        {
                                            var seasonItem = seasonDate[j];
                                            DateTime? valStartDate = seasonItem.StartDate.ConvertStringToDateTime();
                                            DateTime? valEndDate = seasonItem.EndDate.ConvertStringToDateTime();
                                            if (dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() >= valStartDate && dtoReceivingNote.ReceivingNoteDate.ConvertStringToDateTime() <= valEndDate)
                                            {
                                                weekNo = seasonItem.WeekNo;
                                                break;
                                            }
                                        }
                                        context.ReceivingNoteMng_function_UpdateFactoryProductionStatus(subItem.FactoryOrderDetailID, subItem.FactoryOrderID, subItem.FactoryID, season, qty_SET, weekNo);
                                    }

                                }
                            }
                        }
                    }
                    #endregion
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

        private int GetQnt_ModelSet_ReceivingNoteDetail(DTO.ReceivingNoteDetailDTO item, List<DTO.ReceivingNoteDetailDTO> list_ReceivingNoteDetail)
        {
            int QtyModel = 0;
            using (ReceivingNoteEntities context = CreateContext())
            {
                var listModelSet = context.ReceivingNoteMng_ListModel_Set_View.ToList();
                if (item.OldQuantity == null)
                {
                    if(item.Quantity != null)
                    {
                        QtyModel = (int)item.Quantity;
                    }
                }
                else
                {
                    if(item.Quantity != null)
                    {
                        if(item.OldQuantity != null)
                        {
                            QtyModel = (int)(item.Quantity - item.OldQuantity);
                        }
                        else
                        {
                            QtyModel = QtyModel = (int)(item.Quantity);
                        }

                    }
                    else
                    {
                        if(item.OldQuantity != null)
                        {
                            QtyModel = (int)(0 - item.OldQuantity);
                        }
                    }                    
                }
                for (int i = 0; i < list_ReceivingNoteDetail.Count; i++)
                {
                    var sItem = list_ReceivingNoteDetail[i];
                    
                    if (sItem.WorkOrderID == item.WorkOrderID)
                    {
                        var modelUD = sItem.ProductionItemUD.Substring(0, 4).ToString();
                        var listModelSet_1 = listModelSet.Where(o => o.PieceModelUD == modelUD).FirstOrDefault();
                        int qty = 0;
                        if(listModelSet_1.Quantity != null)
                        {
                            if (sItem.OldQuantity == null)
                            {
                                if(sItem.Quantity != null)
                                {
                                    qty = (int)sItem.Quantity / (int)listModelSet_1.Quantity;
                                }
                                else
                                {
                                    qty = 0;
                                }
                            }
                            else
                            {
                                if (sItem.Quantity != null)
                                {
                                    qty = (int)(sItem.Quantity - sItem.OldQuantity) / (int)listModelSet_1.Quantity;
                                }
                                else
                                {
                                    qty = 0;
                                }
                            }
                        }
                        else
                        {
                            qty = 0;
                        }

                        if (qty < QtyModel)
                        {
                            QtyModel = qty;
                        }
                    }
                }
            }
            
            return QtyModel;
        }
        private string GetSeason(string receivingNoteDate)
        {
            int year = int.Parse(receivingNoteDate.Substring(6, 4));
            string yearDate = "01/09/" + year.ToString();
            DateTime? startDate = yearDate.ConvertStringToDateTime();
            DateTime? receivingDate = receivingNoteDate.ConvertStringToDateTime();
            string result = "";
            if(receivingDate >= startDate)
            {
                int year1 = year + 1;
                result = year.ToString() + "/" + year1.ToString();
            }
            else
            {
                int year1 = year - 1;
                result = year1.ToString() + "/" + year.ToString();
            }
            return result;
        }

        public List<DTO.FactorySaleOrderDTO> GetFactorySaleOrder(string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactorySaleOrderDTO> data = new List<DTO.FactorySaleOrderDTO>();
            try
            {
                using (ReceivingNoteEntities context = CreateContext())
                {
                    List<ReceivingNoteMng_FactorySaleOrder_View> item;
                    item = context.ReceivingNoteMng_FactorySaleOrder_View.Where(o => o.FactorySaleOrderUD.Contains(searchQuery)).ToList();
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_FactorySaleOrder_View>, List<DTO.FactorySaleOrderDTO>>(item);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public List<DTO.FactorySaleOrderDetailDTO> GetFactorySaleOrderDetail(int factorySaleOrderID, string receivingNoteDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactorySaleOrderDetailDTO> data = new List<DTO.FactorySaleOrderDetailDTO>();
            try
            {
                using (ReceivingNoteEntities context = CreateContext())
                {
                    List<ReceivingNoteMng_FactorySaleOrderDetail_View> item;
                    var datax = context.ReceivingNoteMng_FactorySaleOrderDetail_View.Where(o => o.FactorySaleOrderID == factorySaleOrderID);
                    item = datax.ToList();
                    data = AutoMapper.Mapper.Map<List<ReceivingNoteMng_FactorySaleOrderDetail_View>, List<DTO.FactorySaleOrderDetailDTO>>(item);
                    foreach (var itemUnit in data)
                    {
                        var unitItem = context.ReceivingNoteMng_Function_ProductionItemUnitByValidDate(receivingNoteDate.ConvertStringToDateTime(), itemUnit.ProductionItemID).ToList();
                        itemUnit.ProductionItemUnits.AddRange(AutoMapper.Mapper.Map<List<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result>, List<DTO.ProductionItemUnit>>(unitItem));
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
    }
}
