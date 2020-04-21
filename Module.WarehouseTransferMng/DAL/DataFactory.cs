using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Module.WarehouseTransferMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private WarehouseTransferMngEntities CreateContext()
        {
            return new WarehouseTransferMngEntities(Library.Helper.CreateEntityConnectionString("DAL.WarehouseTransferMngModel"));
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
                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    var dbItem = context.WarehouseTransfer.Where(o => o.WarehouseTransferID == id).FirstOrDefault();
                    context.WarehouseTransfer.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = iEx.Message;
                return false;
            }

        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    WarehouseTransferMng_WarehouseTransfer_View dbItem;
                    dbItem = context.WarehouseTransferMng_WarehouseTransfer_View.FirstOrDefault(o => o.WarehouseTransferID == id);
                    editFormData.Data = converter.DB2DTO_WarehouseTransfer(dbItem);
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = iEx.Message;
                return editFormData;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.WarehouseTransferDTO dtoWarehouseTransfer = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.WarehouseTransferDTO>();

            try
            {
                // Get company
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userId);

                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    WarehouseTransfer dbWarehouseTransfer;

                    if (id == 0)
                    {
                        dbWarehouseTransfer = new WarehouseTransfer();
                        context.WarehouseTransfer.Add(dbWarehouseTransfer);
                    }
                    else
                    {
                        dbWarehouseTransfer = context.WarehouseTransfer.FirstOrDefault(o => o.WarehouseTransferID == id);
                    }

                    if (dbWarehouseTransfer == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find WarehouseTransfer!";

                        return false;
                    }

                    converter.DTO2DB_WarehouseTransfer(userId, companyID, dtoWarehouseTransfer, ref dbWarehouseTransfer);

                    // Remove local WarehouseTransferDetail
                    context.WarehouseTransferDetail.Local.Where(o => o.WarehouseTransfer == null).ToList().ForEach(o => context.WarehouseTransferDetail.Remove(o));
                    context.SaveChanges();

                    // Generate code WarehouseTransfer
                    if (id == 0)
                    {
                        context.WarehouseTransferMng_function_GenerateReceiptCode(dbWarehouseTransfer.WarehouseTransferID, dbWarehouseTransfer.CompanyID, dbWarehouseTransfer.ReceiptDate.Value.Year, dbWarehouseTransfer.ReceiptDate.Value.Month);
                    }

                    // Get data WarehouseTransfer to reload
                    dtoItem = GetData(userId,dbWarehouseTransfer.WarehouseTransferID, out notification).Data;
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
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        WarehouseTransferMng_WarehouseTransfer_View dbItem;
                        dbItem = context.WarehouseTransferMng_WarehouseTransfer_View.FirstOrDefault(o => o.WarehouseTransferID == id);
                        editFormData.Data = converter.DB2DTO_WarehouseTransfer(dbItem);
                        editFormData.Data.WarehouseTransferProductDTOs = new List<DTO.WarehouseTransferProductDTO>();

                        //get item from 
                        List<WarehouseTransferMng_WarehouseTransferProduct_View> transferItem = context.WarehouseTransferMng_WarehouseTransferProduct_View.Where(o => o.WarehouseTransferID == id).ToList();
                        DTO.WarehouseTransferProductDTO dtoTransferItem;
                        foreach (var item in transferItem)
                        {
                            dtoTransferItem = new DTO.WarehouseTransferProductDTO();
                            dtoTransferItem.WarehouseTransferProductID = item.WarehouseTransferProductID;
                            dtoTransferItem.ProductionItemID = item.ProductionItemID;
                            dtoTransferItem.Quantity = item.Quantity;
                            dtoTransferItem.QNTBarCode = item.QNTBarCode;
                            dtoTransferItem.ProductionItemUD = item.ProductionItemUD;
                            dtoTransferItem.ProductionItemNM = item.ProductionItemNM;
                            dtoTransferItem.ProductionItemTypeNM = item.ProductionItemTypeNM;

                            editFormData.Data.WarehouseTransferProductDTOs.Add(dtoTransferItem);
                        }
                    }
                    else
                    {
                        editFormData.Data = new DTO.WarehouseTransferDTO();
                        editFormData.Data.WarehouseTransferProductDTOs = new List<DTO.WarehouseTransferProductDTO>();
                        editFormData.Data.WarehouseTransferDetails = new List<DTO.WarehouseTransferDetailDTO>();
                        editFormData.Data.ReceiptDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
                //get support list
                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                editFormData.FactoryWarehouses = support_factory.GetFactoryWarehouse(companyID);
                return editFormData;
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = iEx.Message;
                return editFormData;
            }
        }

        public DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                //int? companyID = null;
                string receiptNo = null;
                string deliveryNoteUD = null;
                string receivingNoteUD = null;
                string receiptDate = null;
                int? fromFactoryWarehouseID=null;
                int? toFactoryWarehouseID = null;
                string warehouseTransferType = null;

                //Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("receiptNo") && !string.IsNullOrEmpty(filters["receiptNo"].ToString()))
                {
                    receiptNo = filters["receiptNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("deliveryNoteUD") && !string.IsNullOrEmpty(filters["deliveryNoteUD"].ToString()))
                {
                    deliveryNoteUD = filters["deliveryNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receivingNoteUD") && !string.IsNullOrEmpty(filters["receivingNoteUD"].ToString()))
                {
                    receivingNoteUD = filters["receivingNoteUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("receiptDate") && !string.IsNullOrEmpty(filters["receiptDate"].ToString()))
                {
                    receiptDate = filters["receiptDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("fromFactoryWarehouseID") && filters["fromFactoryWarehouseID"] != null)
                {
                    fromFactoryWarehouseID = Convert.ToInt32(filters["fromFactoryWarehouseID"]);
                }
                if (filters.ContainsKey("toFactoryWarehouseID") && filters["toFactoryWarehouseID"] != null)
                {
                    toFactoryWarehouseID = Convert.ToInt32(filters["toFactoryWarehouseID"]);
                }

                //if (filters.ContainsKey("fromFactoryWarehouseNM") && !string.IsNullOrEmpty(filters["fromFactoryWarehouseNM"].ToString()))
                //{
                //    receivingNoteUD = filters["fromFactoryWarehouseNM"].ToString().Replace("'", "''");
                //}
                //if (filters.ContainsKey("toFactoryWarehouseNM") && !string.IsNullOrEmpty(filters["toFactoryWarehouseNM"].ToString()))
                //{
                //    receiptDate = filters["toFactoryWarehouseNM"].ToString().Replace("'", "''");
                //}

                if (filters.ContainsKey("warehouseTransferType") && !string.IsNullOrEmpty(filters["warehouseTransferType"].ToString()))
                {
                    warehouseTransferType = filters["warehouseTransferType"].ToString().Replace("'", "''");
                }

                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    totalRows = context.WarehouseTransferMngMng_function_SearchWarehouseTransfer(/*companyID,*/ orderBy, orderDirection, receiptNo, deliveryNoteUD, receivingNoteUD, receiptDate, fromFactoryWarehouseID, toFactoryWarehouseID, warehouseTransferType).Count();
                    var result = context.WarehouseTransferMngMng_function_SearchWarehouseTransfer(/*companyID,*/ orderBy, orderDirection, receiptNo, deliveryNoteUD, receivingNoteUD, receiptDate, fromFactoryWarehouseID, toFactoryWarehouseID, warehouseTransferType).ToList();
                    string transferredType = "TRANSFERRED";
                    string sentType = "SENT";
                    foreach (var item in result)
                    {
                        if (item.WarehouseTransferType == "0")
                        {
                            item.WarehouseTransferType = sentType;
                        }
                        if (item.WarehouseTransferType == "1")
                        {
                            item.WarehouseTransferType = transferredType;
                        }
                    }

                    searchFormData.Data = converter.DB2DTO_WarehouseTransferSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchFormData.FactoryWarehouses = converter.DB2DTO_FactoryWarehouseDTO(context.WarehouseTransferMng_FactoryWarehouse_View.ToList());
                }
               
                //support list
                //Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                //searchFormData.FactoryWarehouses = support_factory.GetFactoryWarehouse(companyID);

                return searchFormData;
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = iEx.Message;
                return searchFormData;
            }
        }

        public DTO.InitFormDTO GetInitForm(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormDTO data = new DTO.InitFormDTO();

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    data.Branches = AutoMapper.Mapper.Map<List<WarehouseTransferMng_Branch_View>, List<DTO.BranchDTO>>(context.WarehouseTransferMng_Branch_View.Where(o => o.CompanyID == companyID).ToList());
                    data.FactoryWarehouses = AutoMapper.Mapper.Map<List<WarehouseTransferMng_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.WarehouseTransferMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object ConfirmReceiving(int userID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                // Update confirm receiving in WarehouseTransfer
                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    WarehouseTransfer dbWarehouseTransfer = context.WarehouseTransfer.FirstOrDefault(o => o.WarehouseTransferID == id);

                    if (dbWarehouseTransfer == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find WarehouseTransfer!";

                        return null;
                    }

                    dbWarehouseTransfer.IsConfirmReceiving = true;
                    dbWarehouseTransfer.UpdatedBy = userID;
                    dbWarehouseTransfer.UpdatedDate = DateTime.Now;

                    // Create receiving note new with WarehouseTransferID.
                    ReceivingNote dbReceivingNote = new ReceivingNote();
                    context.ReceivingNote.Add(dbReceivingNote);

                    dbReceivingNote.ReceivingNoteDate = DateTime.Now;
                    dbReceivingNote.CompanyID = companyID;
                    dbReceivingNote.WarehouseTransferID = dbWarehouseTransfer.WarehouseTransferID;
                    dbReceivingNote.ViewName = "Team2Warehouse";
                    dbReceivingNote.CreatedBy = userID;
                    dbReceivingNote.CreatedDate = DateTime.Now;
                    dbReceivingNote.UpdatedBy = userID;
                    dbReceivingNote.UpdatedDate = DateTime.Now;
                    dbReceivingNote.IsConfirm = true;
                    dbReceivingNote.ConfirmBy = userID;
                    dbReceivingNote.ConfirmDate = DateTime.Now;
                    dbReceivingNote.Description = dbWarehouseTransfer.ReceiptNo;
                    dbReceivingNote.StatusTypeID = 5; //From Transfer Warehouse

                    // Update receiving note detail with warehouse transfer detail
                    foreach (WarehouseTransferDetail dbWarehouseTransferDetail in dbWarehouseTransfer.WarehouseTransferDetail.ToList())
                    {
                        ReceivingNoteDetail dbReceivingNoteDetail = new ReceivingNoteDetail();
                        dbReceivingNote.ReceivingNoteDetail.Add(dbReceivingNoteDetail);

                        // Update value important
                        dbReceivingNoteDetail.ProductionItemID = dbWarehouseTransferDetail.ProductionItemID;
                        dbReceivingNoteDetail.Quantity = dbWarehouseTransferDetail.DeliveriedQnt;
                        dbReceivingNoteDetail.QtyByUnit = dbWarehouseTransferDetail.DeliveriedQnt;
                        dbReceivingNoteDetail.ToFactoryWarehouseID = dbWarehouseTransferDetail.ToFactoryWarehouseID;
                        dbReceivingNoteDetail.UnitID = dbWarehouseTransferDetail.UnitID;
                    }

                    context.SaveChanges();

                    // Create receiving note code
                    context.ReceivingNoteMng_function_GenerateReceivingNoteUD(dbReceivingNote.ReceivingNoteID, companyID, dbReceivingNote.ReceivingNoteDate.Value.Year, dbReceivingNote.ReceivingNoteDate.Value.Month);
                }

                // Get warehouse transfer after confirm receiving note
                return GetData(id, out notification);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return null;
            }
        }

        public object ConfirmDelivering(int userID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                // Update confirm receiving in WarehouseTransfer
                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    WarehouseTransfer dbWarehouseTransfer = context.WarehouseTransfer.FirstOrDefault(o => o.WarehouseTransferID == id);

                    if (dbWarehouseTransfer == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find WarehouseTransfer!";

                        return null;
                    }

                    dbWarehouseTransfer.IsConfirmDelivering = true;
                    dbWarehouseTransfer.UpdatedBy = userID;
                    dbWarehouseTransfer.UpdatedDate = DateTime.Now;

                    // Create receiving note new with WarehouseTransferID.
                    DeliveryNote dbDeliveryNote = new DeliveryNote();
                    context.DeliveryNote.Add(dbDeliveryNote);

                    dbDeliveryNote.DeliveryNoteDate = DateTime.Now;
                    dbDeliveryNote.CompanyID = companyID;
                    dbDeliveryNote.WarehouseTransferID = dbWarehouseTransfer.WarehouseTransferID;
                    dbDeliveryNote.ViewName = "SaleOrderWithoutWorkOrder";
                    dbDeliveryNote.CreatedBy = userID;
                    dbDeliveryNote.CreatedDate = DateTime.Now;
                    dbDeliveryNote.UpdatedBy = userID;
                    dbDeliveryNote.UpdatedDate = DateTime.Now;
                    dbDeliveryNote.IsApproved = true;
                    dbDeliveryNote.ApprovedBy = userID;
                    dbDeliveryNote.ApprovedDate = DateTime.Now;
                    dbDeliveryNote.Description = dbWarehouseTransfer.ReceiptNo;
                    dbDeliveryNote.StatusTypeID = 7; //From Transfer Warehouse

                    // Update receiving note detail with warehouse transfer detail
                    foreach (WarehouseTransferDetail dbWarehouseTransferDetail in dbWarehouseTransfer.WarehouseTransferDetail.ToList())
                    {
                        DeliveryNoteDetail dbDeliveryNoteDetail = new DeliveryNoteDetail();
                        dbDeliveryNote.DeliveryNoteDetail.Add(dbDeliveryNoteDetail);

                        // Update value important
                        dbDeliveryNoteDetail.ProductionItemID = dbWarehouseTransferDetail.ProductionItemID;
                        dbDeliveryNoteDetail.Qty = dbWarehouseTransferDetail.ReceivedQnt;
                        dbDeliveryNoteDetail.QtyByUnit = dbWarehouseTransferDetail.ReceivedQnt;
                        dbDeliveryNoteDetail.FromFactoryWarehouseID = dbWarehouseTransferDetail.FromFactoryWarehouseID;
                        dbDeliveryNoteDetail.UnitID = dbWarehouseTransferDetail.UnitID;
                    }

                    context.SaveChanges();

                    // Create delivery note code
                    context.DeliveryNoteMng_function_GenerateDeliveryNoteUD(dbDeliveryNote.DeliveryNoteID, companyID, dbDeliveryNote.DeliveryNoteDate.Value.Year, dbDeliveryNote.DeliveryNoteDate.Value.Month);
                }

                // Get warehouse transfer after confirm receiving note
                return GetData(id, out notification);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return null;
            }
        }

        public object QuickSearchProductionItem(int userID, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.QuickSearchProductionItemDTO data = new DTO.QuickSearchProductionItemDTO();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                string searchString = (filters.ContainsKey("searchString") && filters["searchString"] != null && !string.IsNullOrEmpty(filters["searchString"].ToString()) ? filters["searchString"].ToString() : null);
                int? fromBranch = (filters.ContainsKey("fromBranch") && filters["fromBranch"] != null && !string.IsNullOrEmpty(filters["fromBranch"].ToString()) ? (int?)Convert.ToInt32(filters["fromBranch"].ToString()) : null);
                int? toBranch = (filters.ContainsKey("toBranch") && filters["toBranch"] != null && !string.IsNullOrEmpty(filters["toBranch"].ToString()) ? (int?)Convert.ToInt32(filters["toBranch"].ToString()) : null);

                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    var dbProductionItem = context.WarehouseTransferMng_function_ProductionItemQuickSearchResult(searchString, fromBranch, toBranch, companyID);
                    data.ProductionItems = converter.DB2DTO_ProductionItem(dbProductionItem.ToList());
                    //data.ProductionItemUnits = converter.DB2DTO_ProductionItemUnit(context.WarehouseTransferMng_ProductionItemUnit_View.Where(o => o.ProductionItemUD.Contains(searchString) || o.ProductionItemNM.Contains(searchString)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.WarehouseTransferPrintoutDTO GetWarehouseTransferPrintoutHTML(int warehouseTransferID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.WarehouseTransferPrintoutDTO warehouseTransferPrintout = new DTO.WarehouseTransferPrintoutDTO();
            warehouseTransferPrintout.WarehouseTransferDetailPrintoutDTOs = new List<DTO.WarehouseTransferDetailPrintoutDTO>();
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehouseTransferMng_function_GetWarehouseTransferPrintout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WarehouseTransferID", warehouseTransferID);
                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.TableMappings.Add("Table2", "ReceiptQtyDetail");
                adap.Fill(ds);

                //read to delivery note
                var deliveryNote = ds.Receipt.FirstOrDefault();
                warehouseTransferPrintout.ReceiptNo = deliveryNote.ReceiptNo;
                warehouseTransferPrintout.ReceiptDate = deliveryNote.ReceiptDate;
                warehouseTransferPrintout.DeliverName = deliveryNote.DeliverName;
                warehouseTransferPrintout.DeliverAddress = deliveryNote.DeliverAddress;
                warehouseTransferPrintout.ReceiverName = deliveryNote.ReceiverName;
                warehouseTransferPrintout.ReceiverAddress = deliveryNote.ReceiverAddress;
                warehouseTransferPrintout.StockName = deliveryNote.StockName;
                warehouseTransferPrintout.StockAddress = deliveryNote.StockAddress;
                warehouseTransferPrintout.Reason = deliveryNote.Reason;
                warehouseTransferPrintout.DayReceipt = deliveryNote.DayReceipt;
                warehouseTransferPrintout.MonthReceipt = deliveryNote.MonthReceipt;
                warehouseTransferPrintout.YearReceipt = deliveryNote.YearReceipt;
                try
                {
                    warehouseTransferPrintout.DeliveryNoteUD = deliveryNote.DeliveryNoteUD;
                }
                catch (Exception ex)
                {
                    warehouseTransferPrintout.DeliveryNoteUD = "";
                }
                try
                {
                    warehouseTransferPrintout.ReceivingNoteUD = deliveryNote.ReceivingNoteUD;
                }
                catch (Exception ex)
                {
                    warehouseTransferPrintout.ReceivingNoteUD = "";
                }

                //read delivery note detail
                DTO.WarehouseTransferDetailPrintoutDTO warehouseTransferDetail;
                foreach (var itemqty in ds.ReceiptQtyDetail)
                {
                    warehouseTransferDetail = new DTO.WarehouseTransferDetailPrintoutDTO();
                    warehouseTransferPrintout.WarehouseTransferDetailPrintoutDTOs.Add(warehouseTransferDetail);

                    foreach (var item in ds.ReceiptDetail)
                    {
                        if (itemqty.ProductionItemID == item.ProductionItemID)
                        {
                            warehouseTransferDetail.ProductionItemNM = item.FactoryMaterialNM;
                            warehouseTransferDetail.ProductionItemUD = item.FactoryMaterialUD;
                            warehouseTransferDetail.UnitNM = item.UnitNM;
                            warehouseTransferDetail.Quantity = itemqty.Quantity;
                            warehouseTransferDetail.Price = itemqty.Price;
                            warehouseTransferDetail.Amount = itemqty.Amount;
                            warehouseTransferDetail.Description = item.Description;
                            warehouseTransferDetail.ProductionItemID = item.ProductionItemID;
                        }
                    }
                }
                return warehouseTransferPrintout;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return warehouseTransferPrintout;
            }
        }

        public List<DTO.StockQntFromWarehouse> GetStockQntFromWarehouse(int FromFactoryWarehouseID, int ProductionItemID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            List<DTO.StockQntFromWarehouse> data = new List<DTO.StockQntFromWarehouse>();
            try
            {
                using (WarehouseTransferMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_StockQntFromWarehouse(context.WarehouseTransferMng_GetStockQnt_View.Where(o => o.FactoryWarehouseID == FromFactoryWarehouseID && o.ProductionItemID == ProductionItemID).ToList());
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
