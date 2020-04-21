using Library;
using Library.DTO;
using Module.PurchaseOrderMng.DTO;
using Module.Support.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.PurchaseOrderMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchData, DTO.EditData>
    {
        private DataConverter converter = new DataConverter();

        private PurchaseOrderMngEntities CreateContext()
        {
            return new PurchaseOrderMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchaseOrderMngModel"));
        }

        public DTO.SearchData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchData searchFormData = new DTO.SearchData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                //int? companyID = null;
                string season = null;
                string purchaseOrderUD = null;
                string purchaseOrderDate = null;
                string factoryRawMaterialShortNM = null;
                string purchaseRequestUD = null;
                string eTA = null;
                string currency = null;
                string remark = null;
                int? poStatus = null;
                string purchaseOrderIDs = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //companyID = fw_factory.GetCompanyID(userId);
                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseOrderUD") && !string.IsNullOrEmpty(filters["purchaseOrderUD"].ToString()))
                {
                    purchaseOrderUD = filters["purchaseOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseOrderDate") && !string.IsNullOrEmpty(filters["purchaseOrderDate"].ToString()))
                {
                    purchaseOrderDate = filters["purchaseOrderDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("factoryRawMaterialShortNM") && !string.IsNullOrEmpty(filters["factoryRawMaterialShortNM"].ToString()))
                {
                    factoryRawMaterialShortNM = filters["factoryRawMaterialShortNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseRequestUD") && !string.IsNullOrEmpty(filters["purchaseRequestUD"].ToString()))
                {
                    purchaseRequestUD = filters["purchaseRequestUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("currency") && !string.IsNullOrEmpty(filters["currency"].ToString()))
                {
                    currency = filters["currency"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("eTA") && !string.IsNullOrEmpty(filters["eTA"].ToString()))
                {
                    eTA = filters["eTA"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("remark") && !string.IsNullOrEmpty(filters["remark"].ToString()))
                {
                    remark = filters["remark"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("status") && filters["status"] != null && !string.IsNullOrEmpty(filters["status"].ToString()))
                {
                    poStatus = Convert.ToInt32(filters["status"].ToString());
                }
                if (filters.ContainsKey("purchaseOrderIDs") && !string.IsNullOrEmpty(filters["purchaseOrderIDs"].ToString()))
                {
                    purchaseOrderIDs = filters["purchaseOrderIDs"].ToString().Replace("'", "''");
                }
                DateTime? valpurchaseOrderDate = purchaseOrderDate.ConvertStringToDateTime();
                DateTime? valETA = eTA.ConvertStringToDateTime();
                using (PurchaseOrderMngEntities context = CreateContext())
                {
                    Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                    totalRows = context.PurchaseOrderMng_function_SearchPurchaseOrder(season, purchaseOrderUD, valpurchaseOrderDate, factoryRawMaterialShortNM, purchaseRequestUD, valETA, currency, orderBy, orderDirection, remark, poStatus, purchaseOrderIDs).Count();
                    searchFormData.Data = converter.DB2DTO_PurchaseOrderSearch(context.PurchaseOrderMng_function_SearchPurchaseOrder(season, purchaseOrderUD, valpurchaseOrderDate, factoryRawMaterialShortNM, purchaseRequestUD, valETA, currency, orderBy, orderDirection, remark, poStatus, purchaseOrderIDs).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    searchFormData.PurchaseOrderStatus = supportFactory.GetConstantEntries(EntryGroupType.PurchaseOrderStatus);
                    searchFormData.Seasons = supportFactory.GetSeason();
                }
                return searchFormData;
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
                return searchFormData;
            }
        }

        public DTO.EditData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditData data = new EditData();
            data.Data = new PurchaseOrderDto();
            data.Data.PurchaseOrderDetailDTOs = new List<PurchaseOrderDetailDto>();

            data.SupplierPaymentTermDtos = new List<SupplierPaymentTermDto>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_PurchaseOrder(context.PurchaseOrderMng_PurchaseOrder_View.FirstOrDefault(o => o.PurchaseOrderID == id));
                        data.SupplierPaymentTermDtos = converter.DB2DTO_SupplierPaymentTerm(context.PurchaseOrderMng_SupplierPaymentTerm_View.Where(s => s.FactoryRawMaterialID == data.Data.FactoryRawMaterialID).ToList());
                    }
                    if (id <= 0)
                    {
                        data.Data.RequiredDocuments = "Hóa đơn + phiếu giao hàng";
                        data.Data.PaymentDocuments = "Hóa đơn + phiếu giao hàng";
                        data.Data.PurchaseOrderStatusID = 1; //Open
                        data.SupplierPaymentTermDtos = converter.DB2DTO_SupplierPaymentTerm(context.PurchaseOrderMng_SupplierPaymentTerm_View.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
            }

            return data;
        }

        public List<DTO.SupportPurchaseRequestDto> GetPurchaseRequest(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;
                int searchString2 = (filters.ContainsKey("searchQuery2") && filters["searchQuery2"] != null && !string.IsNullOrEmpty(filters["searchQuery2"].ToString().Replace("'", "''"))) ? Convert.ToInt32(filters["searchQuery2"].ToString()) : 0;

                using (var context = CreateContext())
                {
                    return converter.DB2DTO_GetPurchaseRequest(context.PurchaseOrderMng_function_PurchaseRequest(searchString, searchString2).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return null;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseOrder.FirstOrDefault(o => o.PurchaseOrderID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.PurchaseOrderStatusID = 2;
                    dbItem.ApprovedBy = userId;
                    dbItem.ApprovedDate = DateTime.Now;

                    context.SaveChanges();

                    // Auto update unit price in receiving note
                    UpdateUnitPriceReceivingNoteDetail(dbItem.PurchaseOrderID, out notification);

                    dtoItem = GetData(dbItem.PurchaseOrderID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchaseOrderMngEntities context = CreateContext())
                {
                    var dbItem = context.PurchaseOrder.Where(o => o.PurchaseOrderID == id).FirstOrDefault();
                    context.PurchaseOrder.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
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
                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            DTO.EditData editFormData = new DTO.EditData();
            notification = new Notification();
            try
            {
                using (PurchaseOrderMngEntities context = CreateContext())
                {
                    PurchaseOrderMng_PurchaseOrder_View dbItem;
                    dbItem = context.PurchaseOrderMng_PurchaseOrder_View.FirstOrDefault(o => o.PurchaseOrderID == id);
                    editFormData.Data = converter.DB2DTO_PurchaseOrder(dbItem);
                }
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

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchaseOrderDto dtoPurchaseOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PurchaseOrderDto>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (PurchaseOrderMngEntities context = CreateContext())
                {
                    PurchaseOrder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new PurchaseOrder();
                        context.PurchaseOrder.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PurchaseOrder.Where(o => o.PurchaseOrderID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoPurchaseOrder.File_HasChange.HasValue && dtoPurchaseOrder.File_HasChange.Value)
                        {
                            dtoPurchaseOrder.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoPurchaseOrder.File_NewFile, dtoPurchaseOrder.AttachedFile, dtoPurchaseOrder.FriendlyName);
                        }

                        //
                        //convert dto to db
                        converter.DTO2DB_PurchaseOrder(dtoPurchaseOrder, ref dbItem);
                        dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        if (id == 0)
                        {
                            //dbItem.PurchaseOrderUD = context.PurchaseOrderMng_function_GeneratePurchaseOrderUD(dtoPurchaseOrder.FactoryRawMaterialUD, dtoPurchaseOrder.PurchaseOrderDate, dtoPurchaseOrder.FactoryRawMaterialID).FirstOrDefault();
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                            if (dtoPurchaseOrder.PurchaseOrderStatusID != 4)
                            {
                                dbItem.PurchaseOrderStatusID = 1;
                            }
                        }

                        // loop dto item detail
                        foreach (var item in dtoPurchaseOrder.PurchaseOrderDetailDTOs)
                        {
                            if (item.IsChangePrice == true)
                            {
                                PurchaseOrderDetailPriceHistory dbItemPriceHistory = new PurchaseOrderDetailPriceHistory();
                                context.PurchaseOrderDetailPriceHistory.Add(dbItemPriceHistory);
                                dbItemPriceHistory.PurchaseOrderDetailID = (int)item.PurchaseOrderDetailID;
                                dbItemPriceHistory.UnitPrice = (decimal)item.UnitPrice;
                                dbItemPriceHistory.ApprovedBy = userId;
                                dbItemPriceHistory.ApprovedDate = DateTime.Now;
                            }
                        }
                        //remove orphan

                        context.PurchaseOrderDetail.Local.Where(o => o.PurchaseOrder == null).ToList().ForEach(o => context.PurchaseOrderDetail.Remove(o));
                        context.PurchaseOrderWorkOrderDetail.Local.Where(o => o.PurchaseOrderDetail == null).ToList().ForEach(o => context.PurchaseOrderWorkOrderDetail.Remove(o));
                        context.PurchaseOrderDetailReceivingPlan.Local.Where(o => o.PurchaseOrderDetail == null).ToList().ForEach(o => context.PurchaseOrderDetailReceivingPlan.Remove(o));
                        context.PurchaseOrderDetailPriceHistory.Local.Where(o => o.PurchaseOrderDetail == null).ToList().ForEach(o => context.PurchaseOrderDetailPriceHistory.Remove(o));
                        //save data
                        context.SaveChanges();

                        // Generate PurchaseOrderUD.
                        if (id == 0)
                        {
                            context.PurchaseOrderMng_function_GeneratePurchaseOrderUD(dbItem.Season, dbItem.FactoryRawMaterialID, dbItem.PurchaseOrderID);
                        }
                        else
                        {
                            // Auto update unit price in receiving note
                            UpdateUnitPriceReceivingNoteDetail(dbItem.PurchaseOrderID, out notification);
                        }

                        //get return data
                        dtoItem = GetData(userId, dbItem.PurchaseOrderID, out notification).Data;

                        if (id == 0)
                        {
                            string emailSubject = string.Format("[Tilsoft] - Purchasing Order - {0} - {1}", dbItem.PurchaseOrderUD, dtoPurchaseOrder.FactoryRawMaterialShortNM);
                            string emailBody = string.Format("Please approve PO: {0} - {1} - {2}", dbItem.PurchaseOrderUD, dtoPurchaseOrder.FactoryRawMaterialShortNM, "http://app.tilsoft.bg/PurchaseOrderMng/Edit/" + dbItem.PurchaseOrderID);
                            SendNotification(emailSubject, emailBody);
                        }

                        return true;
                    }
                }
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
                return false;
            }
        }

        public DTO.PurchaseRequestData GetPurchaseOrderDetailByPurchaseRequestID(Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.PurchaseRequestData data = new PurchaseRequestData();
            data.PurchaseRequestDetailApprovals = new List<DTO.PurchaseRequestDetailApproval>();

            try
            {
                int PurchaseRequestID = (filters.Contains("PurchaseRequestID") && filters["PurchaseRequestID"] != null) ? Convert.ToInt32(filters["PurchaseRequestID"]) : 0;
                int FactoryRawMaterialID = (filters.Contains("FactoryRawMaterialID") && filters["FactoryRawMaterialID"] != null) ? Convert.ToInt32(filters["FactoryRawMaterialID"]) : 0;
                string PurchaseOrderDate = (filters.Contains("purchaseOrderDate") && filters["purchaseOrderDate"] != null) ? filters["purchaseOrderDate"].ToString() : null;

                var PODate = PurchaseOrderDate.ConvertStringToDateTime();

                using (var context = CreateContext())
                {
                    data.PurchaseRequestDetailApprovals = converter.DB2DTO_PurchaseRequestDetailApproval(context.PurchaseOrderMng_PurchaseRequestDetailApproval_View.Where(o => o.PurchaseRequestID == PurchaseRequestID && o.FactoryRawMaterialID == FactoryRawMaterialID && o.ValidFrom <= PODate && o.ValidTo >= PODate).ToList());
                    data.typePQAndPR = 1; //ListPurchaseRequest
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }

            return data;
        }

        public DTO.InitFormData GetInitData(int userId, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.InitFormData data = new InitFormData();
            data.SubSupplier = new List<FactoryRawMaterialDto>();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Seasons = supportFactory.GetSeason();

                using (var context = CreateContext())
                {
                    data.SubSupplier = converter.DB2DTO_GetFactoryRawMaterial(context.PurchaseOrderMng_FactoryRawMaterial_View.ToList());
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return data;
        }

        public DTO.EditData GetSupplierPaymentTerm(int factoryRawMaterialID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.EditData data = new EditData();
            data.SupplierPaymentTermDtos = new List<SupplierPaymentTermDto>();

            try
            {

                using (var context = CreateContext())
                {
                    data.SupplierPaymentTermDtos = converter.DB2DTO_SupplierPaymentTerm(context.PurchaseOrderMng_SupplierPaymentTerm_View.Where(s => s.FactoryRawMaterialID == factoryRawMaterialID).ToList());
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return data;
        }

        public string GetExcelReportData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PurchaseOrderDataObject ds = new PurchaseOrderDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchaseOrderMng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchaseOrderID", id);
                adap.TableMappings.Add("Table", "PurchaseOrderReportData");
                adap.TableMappings.Add("Table1", "PurchaseOrderDetailReportData");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "PurchaseOrder");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public DTO.PurchaseQuotationData GetPurchaseQuotationDetail(int FactoryRawMaterialID, DateTime purchaseOrderDate, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            DTO.PurchaseQuotationData data = new PurchaseQuotationData();
            data.purchaseQuotationAndSupplierDtos = new List<DTO.PurchaseQuotationAndSupplierDto>();
            try
            {
                using (var context = CreateContext())
                {
                    var quotation = context.PurchaseOrderMng_function_GetListMaterial_PurchaseQuotation_Supplier(FactoryRawMaterialID, purchaseOrderDate).ToList();
                    data.purchaseQuotationAndSupplierDtos = AutoMapper.Mapper.Map<List<PurchaseOrderMng_PurchaseQuotationAndSupplier_View>, List<PurchaseQuotationAndSupplierDto>>(quotation);
                    data.typePQAndPR = 2; //ListPurchaseQuotation and Supplier
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public DTO.PurchaseOrderPrintOutDto GetHTMLReportData(int PurchaseOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchaseOrderPrintOutDto PurchaseOrderPrintOut = new DTO.PurchaseOrderPrintOutDto();
            PurchaseOrderPrintOut.PurchaseOrderDetailPrintOutDtos = new List<DTO.PurchaseOrderDetailPrintOutDto>();
            PurchaseOrderDataObject ds = new PurchaseOrderDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchaseOrderMng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);
                adap.TableMappings.Add("Table", "PurchaseOrderReportData");
                adap.TableMappings.Add("Table1", "PurchaseOrderDetailReportData");
                adap.Fill(ds);

                //read to delivery note
                var purchaseOrder = ds.PurchaseOrderReportData.FirstOrDefault();
                PurchaseOrderPrintOut.PurchaseOrderID = purchaseOrder.PurchaseOrderID;
                PurchaseOrderPrintOut.PurchaseOrderUD = purchaseOrder.PurchaseOrderUD;
                PurchaseOrderPrintOut.PurchaseOrderDate = purchaseOrder.PurchaseOrderDate;
                PurchaseOrderPrintOut.FactoryRawMaterialOfficialNM = purchaseOrder.FactoryRawMaterialOfficialNM;
                PurchaseOrderPrintOut.FactoryRawMaterialShortNM = purchaseOrder.FactoryRawMaterialShortNM;
                PurchaseOrderPrintOut.Address = purchaseOrder.Address;
                PurchaseOrderPrintOut.Fax = purchaseOrder.Fax;
                PurchaseOrderPrintOut.Phone = purchaseOrder.Phone;
                PurchaseOrderPrintOut.ContactPerson = purchaseOrder.ContactPerson;

                PurchaseOrderPrintOut.RequiredDocuments = purchaseOrder.RequiredDocuments;
                PurchaseOrderPrintOut.PaymentDocuments = purchaseOrder.PaymentDocuments;
                PurchaseOrderPrintOut.SupplierPaymentTermID = purchaseOrder.SupplierPaymentTermID;
                PurchaseOrderPrintOut.SupplierPaymentTermNM = purchaseOrder.SupplierPaymentTermNM;
                PurchaseOrderPrintOut.Currency = purchaseOrder.Currency;
                PurchaseOrderPrintOut.VAT = purchaseOrder.VAT;

                //read delivery note detail
                DTO.PurchaseOrderDetailPrintOutDto purchaseOrderDetail;
                foreach (var item in ds.PurchaseOrderDetailReportData)
                {
                    purchaseOrderDetail = new DTO.PurchaseOrderDetailPrintOutDto();
                    PurchaseOrderPrintOut.PurchaseOrderDetailPrintOutDtos.Add(purchaseOrderDetail);

                    purchaseOrderDetail.ProductionItemNM = item.ProductionItemNM;
                    purchaseOrderDetail.ProductionItemUD = item.ProductionItemUD;
                    purchaseOrderDetail.ClientUD = item.ClientUD;
                    purchaseOrderDetail.UnitNM = item.UnitNM;
                    purchaseOrderDetail.OrderQnt = item.OrderQnt;
                    purchaseOrderDetail.Amount = item.Amount;
                    purchaseOrderDetail.Remark = item.Remark;
                    purchaseOrderDetail.UnitPrice = item.UnitPrice;
                    purchaseOrderDetail.ThumbnailLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.ThumbnailLocation;
                    purchaseOrderDetail.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.FileLocation;
                    purchaseOrderDetail.ProductImage = item.ProductImage;
                    purchaseOrderDetail.ETA = item.ETA;
                    //purchaseOrderDetail.WorkOrderUD = item.WorkOrderUD;
                    purchaseOrderDetail.ID = item.ID;
                }
                return PurchaseOrderPrintOut;
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
                return PurchaseOrderPrintOut;
            }
        }

        public string GetPurchaseOrderPrintout(int purchaseOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PurchaseOrderPDF ds = new PurchaseOrderPDF();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchaseOrderMng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PurchaseOrderID", purchaseOrderID);
                adap.TableMappings.Add("Table", "PurchaseOrder");
                adap.TableMappings.Add("Table1", "PurchaseOrderDetail");
                adap.Fill(ds);

                ds.Tables["PurchaseOrder"].Rows[0]["SignatureFileLocation"] = FrameworkSetting.Setting.MediaFullSizeUrl + ds.Tables["PurchaseOrder"].Rows[0]["SignatureFileLocation"];

                for (int intCount = 0; intCount < ds.Tables["PurchaseOrderDetail"].Rows.Count; intCount++)
                {
                    string myNumber = Convert.ToString(ds.Tables["PurchaseOrderDetail"].Rows[intCount]["OrderQnt"]);
                    string[] xxString = myNumber.Split('.');
                    ds.Tables["PurchaseOrderDetail"].Rows[intCount]["HeaderQty"] = xxString[0];
                    ds.Tables["PurchaseOrderDetail"].Rows[intCount]["FooterQty"] = xxString[1];

                    ds.Tables["PurchaseOrderDetail"].Rows[intCount]["FileLocation"] = FrameworkSetting.Setting.MediaFullSizeUrl + ds.Tables["PurchaseOrderDetail"].Rows[intCount]["FileLocation"];
                }

                ds.Tables["PurchaseOrderDetail"].AcceptChanges();

                return Library.Helper.CreateReceiptPrintout(ds.Tables["PurchaseOrder"], ds.Tables["PurchaseOrderDetail"], "PurchaseOrderPDF.rdlc");
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
                return string.Empty;
            }
        }

        public bool Cancel(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseOrder.FirstOrDefault(o => o.PurchaseOrderID == id);
                    var listReceivingNote = context.PurchaseOrderMng_ReceivingNote_View.ToList();
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    for (int i = 0; i < listReceivingNote.Count; i++)
                    {
                        if (id == listReceivingNote[i].PurchaseOrderID)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = " You can not cancel. This PO was created at Receiving Note!";
                            return false;
                        }
                    }
                    dbItem.PurchaseOrderStatusID = 3;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.PurchaseOrderID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public bool Finish(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseOrder.FirstOrDefault(o => o.PurchaseOrderID == id);
                    var listReceivingNote = context.PurchaseOrderMng_ReceivingNote_View.ToList();
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    //for (int i = 0; i < listReceivingNote.Count; i++)
                    //{
                    //    if (id == listReceivingNote[i].PurchaseOrderID)
                    //    {
                    //        notification.Type = NotificationType.Error;
                    //        notification.Message = " You can not cancel. This PO was created at Receiving Note!";
                    //        return false;
                    //    }
                    //}
                    dbItem.PurchaseOrderStatusID = 4;

                    context.SaveChanges();

                    // Auto update unit price in receiving note
                    UpdateUnitPriceReceivingNoteDetail(dbItem.PurchaseOrderID, out notification);

                    dtoItem = GetData(dbItem.PurchaseOrderID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public bool Revise(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseOrder.FirstOrDefault(o => o.PurchaseOrderID == id);
                    var listReceivingNote = context.PurchaseOrderMng_ReceivingNote_View.ToList();
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    //for (int i = 0; i < listReceivingNote.Count; i++)
                    //{
                    //    if (id == listReceivingNote[i].PurchaseOrderID)
                    //    {
                    //        notification.Type = NotificationType.Error;
                    //        notification.Message = " You can not cancel. This PO was created at Receiving Note!";
                    //        return false;
                    //    }
                    //}
                    dbItem.PurchaseOrderStatusID = 2;

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.PurchaseOrderID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        //Send Notification
        private void SendNotification(string emailSubject, string emailBody)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (var context = CreateContext())
                {
                    // Custome email group
                    var dbNotificationEmails = context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "POG");
                    foreach (var dbNotificationEmail in dbNotificationEmails.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbNotificationEmail.Email1) && !emailAddress.Contains(dbNotificationEmail.Email1))
                        {
                            emailAddress.Add(dbNotificationEmail.Email1);
                        }

                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = dbNotificationEmail.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PURCHASING;
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

        /// <summary>
        /// Update unit price in receiving note
        /// </summary>
        /// <param name="purchaseOrderID"></param>
        /// <param name="notification"></param>
        public void UpdateUnitPriceReceivingNoteDetail(int purchaseOrderID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbPurchaseOrderDetail = context.PurchaseOrderDetail.Where(o => o.PurchaseOrderID == purchaseOrderID).ToList();
                    bool isUpdated = false;

                    foreach (var purchaseOrderDetail in dbPurchaseOrderDetail)
                    {
                        var dbReceivingNoteDetail = context.PurchaseOrderMng_ReceivingNoteByPurchaseOrder_View.Where(o => o.PurchaseOrderID == purchaseOrderID && o.ProductionItemID == purchaseOrderDetail.ProductionItemID).ToList();

                        if (dbReceivingNoteDetail != null)
                        {
                            foreach (var item in dbReceivingNoteDetail.ToList())
                            {
                                var dbItem = context.ReceivingNoteDetail.FirstOrDefault(o => o.ReceivingNoteDetailID == item.ReceivingNoteDetailID);
                                dbItem.UnitPrice = purchaseOrderDetail.UnitPrice;
                            }

                            isUpdated = true;
                        }
                    }

                    if (isUpdated)
                    {
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
    }
}
