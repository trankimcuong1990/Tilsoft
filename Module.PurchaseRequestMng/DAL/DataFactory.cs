using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;
using Library.DTO;

namespace Module.PurchaseRequestMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        public DataFactory() { }

        private PurchaseRequestMngEntities CreateContext()
        {
            return new PurchaseRequestMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchaseRequestMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseRequest.FirstOrDefault(o => o.PurchaseRequestID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    dbItem.IsApproved = true;
                    dbItem.ApprovedBy = userId;
                    dbItem.ApprovedDate = DateTime.Now;

                    // Auto change status is confirmed
                    dbItem.PurchaseRequestStatusID = 4; // CONFIRMED
                    context.SaveChanges();

                    // send mail to Employee on ProductionItemGroup
                    // loop data to get ProductionItemID
                    
                    string url = "http://app.tilsoft.bg/" + "PurchaseRequestMng/Edit/" + dbItem.PurchaseRequestID.ToString();
                    string emailSubject = dbItem.PurchaseRequestUD + " - " + dbItem.Remark, emailBody = "Truy cap den yeu cau mua hang:" + "<a href = '" + url + "'></a> ";
                    foreach (var item in dbItem.PurchaseRequestDetail.ToArray())
                    {
                        int? productionItemID = item.ProductionItemID;
                        SendNotification(productionItemID, emailSubject, emailBody);
                    }

                    dtoItem = GetData(dbItem.PurchaseRequestID, out notification).Data;
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return true;
        }

        private void SendNotification(int? productionItemID, string emailSubject, string emailBody)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseRequestMng_GetEmailNotification_View.Where(o => o.ProductionItemID == productionItemID);

                    foreach (var item in dbItem.ToArray())
                    {
                        if (!string.IsNullOrEmpty(item.Email))
                        {
                            emailAddress.Add(item.Email);
                        }

                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = item.UserID;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PURCHASING;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }
                    string sendToEmail = string.Empty;
                    foreach (string eAddress in emailAddress)
                    {
                        sendToEmail += string.IsNullOrEmpty(sendToEmail) ? eAddress : ";" + eAddress;

                    }
                    if (sendToEmail != null)
                    {
                        EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                        dbEmail.EmailBody = emailBody;
                        dbEmail.EmailSubject = emailSubject;
                        dbEmail.SendTo = sendToEmail;
                        context.EmailNotificationMessage.Add(dbEmail);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    var dbItem = context.PurchaseRequest.Where(o => o.PurchaseRequestID == id).FirstOrDefault();
                    context.PurchaseRequest.Remove(dbItem);
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    PurchaseRequestMng_PurchaseRequest_View dbItem;
                    dbItem = context.PurchaseRequestMng_PurchaseRequest_View.Include("PurchaseRequestMng_PurchaseRequestDetail_View.PurchaseRequestMng_PurchaseRequestWorkOrderDetail_View").FirstOrDefault(o => o.PurchaseRequestID == id);
                    editFormData.Data = converter.DB2DTO_PurchaseRequest(dbItem);
                    return editFormData;
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchaseRequestDTO dtoPurchaseRequest = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PurchaseRequestDTO>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    PurchaseRequest dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new PurchaseRequest();
                        context.PurchaseRequest.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PurchaseRequest.Where(o => o.PurchaseRequestID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_PurchaseRequest(dtoPurchaseRequest, ref dbItem);
                        dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        if (id == 0)
                        {
                            dbItem.PurchaseRequestUD = context.PurchaseRequestMng_function_GeneratePurchaseRequestUD().FirstOrDefault();
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                        }

                        //remove orphan
                        context.PurchaseRequestWorkOrderDetail.Local.Where(o => o.PurchaseRequestDetail == null).ToList().ForEach(o => context.PurchaseRequestWorkOrderDetail.Remove(o));
                        context.PurchaseRequestDetail.Local.Where(o => o.PurchaseRequest == null).ToList().ForEach(o => context.PurchaseRequestDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.PurchaseRequestID, out notification).Data;
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

        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            editFormData.ClientByWorkOrders = new List<DTO.ClientByWorkOrderDTO>();
            editFormData.ProformaInvoiceByWorkOrders = new List<DTO.ProformaInvoiceNoByWorkOrderDTO>();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        PurchaseRequestMng_PurchaseRequest_View dbItem;
                        dbItem = context.PurchaseRequestMng_PurchaseRequest_View.Include("PurchaseRequestMng_PurchaseRequestDetail_View.PurchaseRequestMng_PurchaseRequestWorkOrderDetail_View").Where(o => o.CompanyID == companyID).FirstOrDefault(o => o.PurchaseRequestID == id);
                        editFormData.Data = converter.DB2DTO_PurchaseRequest(dbItem);
                        editFormData.Data.PurchaseRequestDetailDTOs = editFormData.Data.PurchaseRequestDetailDTOs.Where(o => o.BranchID == Library.Helper.ConvertStringToInt(param["branchID"].ToString())).ToList();
                    }
                    else
                    {
                        //initialize data
                        editFormData.Data = new DTO.PurchaseRequestDTO();
                        editFormData.Data.PurchaseRequestDetailDTOs = new List<DTO.PurchaseRequestDetailDTO>();
                        editFormData.Data.PurchaseRequestDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.RequestedDate = DateTime.Now.ToString("dd/MM/yyyy");
                        editFormData.Data.WorkOrderIDs = param["workOrderIDs"].ToString();

                        if (param.ContainsKey("workOrderIDs") && param["workOrderIDs"] != null && !string.IsNullOrEmpty(param["workOrderIDs"].ToString()))
                        {
                            var productionItemByWorkOrders = context.PurchaseRequestMng_function_GetProductionItemByWorkOrder(param["workOrderIDs"].ToString(), Library.Helper.ConvertStringToInt(param["branchID"].ToString()));

                            int i = 0;
                            foreach (var item in productionItemByWorkOrders.ToList())
                            {
                                i = i - 1;
                                if (item != null/* && item.ProductionItemTypeID != 3*/)
                                {
                                    DTO.PurchaseRequestDetailDTO purchaseRequestDetail = new DTO.PurchaseRequestDetailDTO();
                                    purchaseRequestDetail.PurchaseRequestDetailID = i;
                                    purchaseRequestDetail.ProductionItemID = item.ProductionItemID;
                                    purchaseRequestDetail.ProductionItemUD = item.ProductionItemUD;
                                    purchaseRequestDetail.ProductionItemNM = item.ProductionItemNM;
                                    purchaseRequestDetail.RequestQnt = item.RequestQnt;
                                    purchaseRequestDetail.OrderQnt = item.OrderQnt;
                                    purchaseRequestDetail.StockQnt = item.StockQnt;
                                    purchaseRequestDetail.IsApproved = item.IsApproved;
                                    purchaseRequestDetail.UnitNM = item.UnitNM;
                                    purchaseRequestDetail.WorkOrderID = item.WorkOrderID;
                                    purchaseRequestDetail.WorkOrderUD = item.WorkOrderUD;
                                    purchaseRequestDetail.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                    purchaseRequestDetail.ProductionItemGroupID = item.ProductionItemGroupID;
                                    purchaseRequestDetail.ETA = editFormData.Data.ETA;

                                    editFormData.Data.PurchaseRequestDetailDTOs.Add(purchaseRequestDetail);
                                }
                            }
                        }
                    }

                    string workOrderIDs = editFormData.Data.WorkOrderIDs;

                    // Client
                    var dbClient = context.PurchaseRequestMng_function_GetClientByWorkOrder(workOrderIDs).ToList();
                    foreach (var item in dbClient)
                    {
                        DTO.ClientByWorkOrderDTO client = new DTO.ClientByWorkOrderDTO();
                        client.ClientID = item.ClientID;
                        client.ClientUD = item.ClientUD;

                        editFormData.ClientByWorkOrders.Add(client);
                    }

                    // ProformaInvoice
                    var dbSaleOrder = context.PurchaseRequestMng_function_GetProformaInvoiceByWorkOrder(workOrderIDs).ToList();
                    foreach (var item in dbSaleOrder)
                    {
                        DTO.ProformaInvoiceNoByWorkOrderDTO saleOrder = new DTO.ProformaInvoiceNoByWorkOrderDTO();
                        saleOrder.SaleOrderID = item.SaleOrderID;
                        saleOrder.ProformaInvoiceNo = item.ProformaInvoiceNo;

                        editFormData.ProformaInvoiceByWorkOrders.Add(saleOrder);
                    }

                    // Set default request status always open
                    if (editFormData.Data.PurchaseRequestStatusID == null)
                    {
                        editFormData.Data.PurchaseRequestStatusID = 1;
                    }

                    return editFormData;
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
                int? companyID = null;
                string purchaseRequestUD = null;
                string purchaseRequestDate = null;
                int? purchaseRequestStatusID = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("purchaseRequestUD") && !string.IsNullOrEmpty(filters["purchaseRequestUD"].ToString()))
                {
                    purchaseRequestUD = filters["purchaseRequestUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseRequestDate") && !string.IsNullOrEmpty(filters["purchaseRequestDate"].ToString()))
                {
                    purchaseRequestDate = filters["purchaseRequestDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("purchaseRequestStatusID") && filters["purchaseRequestStatusID"] != null)
                {
                    purchaseRequestStatusID = Convert.ToInt32(filters["purchaseRequestStatusID"]);
                }

                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    totalRows = context.PurchaseRequestMng_function_SearchPurchaseRequest(orderBy, orderDirection, companyID, purchaseRequestUD, purchaseRequestDate, purchaseRequestStatusID).Count();
                    var result = context.PurchaseRequestMng_function_SearchPurchaseRequest(orderBy, orderDirection, companyID, purchaseRequestUD, purchaseRequestDate, purchaseRequestStatusID);
                    searchFormData.Data = converter.DB2DTO_PurchaseRequestSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public List<DTO.ProductionItemDTO> GetRequestingItem(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProductionItemDTO> data = new List<DTO.ProductionItemDTO>();
            DTO.ProductionItemDTO productionItemDTO;
            DTO.WorkOrderDTO workOrderDTO;
            try
            {
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    var requestingItem = context.PurchaseRequestMng_RequestingItem_View.ToList();
                    var productionItems = from item in requestingItem
                                          group item by new { item.ProductionItemID, item.ProductionItemUD, item.ProductionItemNM, item.Unit, item.SuggestedFactoryRawMaterialID } into g
                                          select new { g.Key.ProductionItemID, g.Key.ProductionItemUD, g.Key.ProductionItemNM, g.Key.Unit, g.Key.SuggestedFactoryRawMaterialID, RequestingQnt = g.Sum(o => o.RequestingQnt) };

                    foreach (var item in productionItems)
                    {
                        productionItemDTO = new DTO.ProductionItemDTO();
                        productionItemDTO.WorkOrderDTOs = new List<DTO.WorkOrderDTO>();
                        data.Add(productionItemDTO);

                        productionItemDTO.ProductionItemID = item.ProductionItemID;
                        productionItemDTO.ProductionItemUD = item.ProductionItemUD;
                        productionItemDTO.ProductionItemNM = item.ProductionItemNM;
                        productionItemDTO.Unit = item.Unit;
                        productionItemDTO.SuggestedFactoryRawMaterialID = item.SuggestedFactoryRawMaterialID;
                        productionItemDTO.RequestQnt = item.RequestingQnt;

                        foreach (var wItem in requestingItem.Where(o => o.ProductionItemID == item.ProductionItemID))
                        {
                            workOrderDTO = new DTO.WorkOrderDTO();
                            productionItemDTO.WorkOrderDTOs.Add(workOrderDTO);

                            workOrderDTO.WorkOrderID = wItem.WorkOrderID;
                            workOrderDTO.WorkOrderUD = wItem.WorkOrderUD;
                            workOrderDTO.ProformaInvoiceNo = wItem.ProformaInvoiceNo;
                            workOrderDTO.ClientUD = wItem.ClientUD;
                            workOrderDTO.RequestingQnt = wItem.RequestingQnt;
                        }
                    }
                    return data;
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
                return data;
            }
        }

        public DTO.SearchFormFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormFilterData data = new DTO.SearchFormFilterData();
            try
            {
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    var x = context.SupportMng_PurchaseRequestStatus_View.ToList();
                    data.PurchaseRequestStatusDTOs = AutoMapper.Mapper.Map<List<SupportMng_PurchaseRequestStatus_View>, List<DTO.PurchaseRequestStatusDTO>>(x);
                    return data;
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
                return data;
            }
        }

        public bool ApprovePrice(int userId, int purchaseRequestDetailID, int purchaseQuotationDetailID, decimal? approvedQnt, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Approve price success !" };
            try
            {
                using (PurchaseRequestMngEntities context = CreateContext())
                {
                    if (!approvedQnt.HasValue || approvedQnt == 0)
                    {
                        throw new Exception("Please select quantity to approve");
                    }

                    var approvePrice = context.PurchaseRequestDetailApproval.Where(o => o.PurchaseRequestDetailID == purchaseRequestDetailID && o.PurchaseQuotationDetailID == purchaseQuotationDetailID).FirstOrDefault();
                    if (approvePrice != null)
                    {
                        throw new Exception("Request has been arppoved. You do not need approved again.");
                    }

                    PurchaseRequestDetailApproval dbApproval = new PurchaseRequestDetailApproval();
                    dbApproval.PurchaseRequestDetailID = purchaseRequestDetailID;
                    dbApproval.PurchaseQuotationDetailID = purchaseQuotationDetailID;
                    dbApproval.ApprovedQnt = approvedQnt;
                    dbApproval.ApprovedBy = userId;
                    dbApproval.ApprovedDate = DateTime.Now;
                    context.PurchaseRequestDetailApproval.Add(dbApproval);
                    context.SaveChanges();
                    return true;
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

        public DTO.InitFormDTO GetInitData(int userID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.InitFormDTO data = new DTO.InitFormDTO();

            try
            {
                using (var context = CreateContext())
                {
                    data.PurchaseRequestStatuses = AutoMapper.Mapper.Map<List<SupportMng_PurchaseRequestStatus_View>, List<DTO.PurchaseRequestStatusDTO>>(context.SupportMng_PurchaseRequestStatus_View.ToList());
                    data.ProductionItemGroups = AutoMapper.Mapper.Map<List<PurchaseRequestMng_ProductionItemGroup_View>, List<DTO.ProductionItemGroupDTO>>(context.PurchaseRequestMng_ProductionItemGroup_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetProductionItemBaseOn(int userID, int? branchID, string workOrderIDs, string searchItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.ProductionItemDTO> data = new List<DTO.ProductionItemDTO>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    if (string.IsNullOrEmpty(workOrderIDs)) // Free item
                    {
                        data = converter.DB2DTO_ProductionItem(context.PurchaseRequestMng_function_GetProductionItem(companyID, branchID, searchItem).ToList());
                    }
                    else // WorkOrder item
                    {
                        var dbItem = context.PurchaseRequestMng_function_GetProductionItemByWorkOrder2(companyID, workOrderIDs, branchID, searchItem).ToList();

                        int? productionItemID = null;
                        DTO.ProductionItemDTO dtoItem = null;

                        foreach (var item in dbItem)
                        {
                            if (item != null/* && item.ProductionItemTypeID != 3*/) // Not get items depend on type is piece
                            {
                                if (productionItemID == null || productionItemID != item.ProductionItemID)
                                {
                                    dtoItem = new DTO.ProductionItemDTO();
                                    dtoItem.ProductionItemID = item.ProductionItemID;
                                    dtoItem.ProductionItemUD = item.ProductionItemUD;
                                    dtoItem.ProductionItemNM = item.ProductionItemNM;
                                    dtoItem.UnitNM = item.UnitNM;
                                    dtoItem.ETA = null;
                                    dtoItem.IsApproved = item.IsApproved;
                                    dtoItem.OrderQnt = item.OrderQnt;
                                    dtoItem.StockQnt = item.StockQnt;
                                    dtoItem.ProductionItemGroupID = item.ProductionItemGroupID;
                                    dtoItem.WorkOrderDTOs = new List<DTO.WorkOrderDTO>();

                                    data.Add(dtoItem);

                                    productionItemID = item.ProductionItemID;
                                }

                                DTO.WorkOrderDTO dtoWorkOrder = new DTO.WorkOrderDTO();
                                dtoWorkOrder.WorkOrderID = item.WorkOrderID;
                                dtoWorkOrder.WorkOrderUD = item.WorkOrderUD;
                                dtoWorkOrder.ProformaInvoiceNo = item.ProformaInvoiceNo;
                                dtoWorkOrder.RequestingQnt = item.RequestQnt;

                                dtoItem.WorkOrderDTOs.Add(dtoWorkOrder);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object ReloadItemIsSetGroup(int userID, int? branchID, string productionItemIDs, string workOrderIDs, string stickGroupdIDs, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.PurchaseRequestDetailDTO> data = new List<DTO.PurchaseRequestDetailDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseRequestMng_function_ReloadItemByMaterialGroup(branchID, productionItemIDs, workOrderIDs, stickGroupdIDs).ToList();

                    int i = 0;
                    foreach (var item in dbItem)
                    {
                        i = i - 1;
                        if (item != null/* && item.ProductionItemTypeID == 2*/)
                        {
                            DTO.PurchaseRequestDetailDTO dtoItem = new DTO.PurchaseRequestDetailDTO();
                            dtoItem.PurchaseRequestDetailID = i;
                            dtoItem.ProductionItemID = item.ProductionItemID;
                            dtoItem.ProductionItemUD = item.ProductionItemUD;
                            dtoItem.ProductionItemNM = item.ProductionItemNM;
                            dtoItem.RequestQnt = item.RequestQnt;
                            dtoItem.OrderQnt = item.OrderQnt;
                            dtoItem.StockQnt = item.StockQnt;
                            dtoItem.IsApproved = item.IsApproved;
                            dtoItem.UnitNM = item.UnitNM;
                            dtoItem.WorkOrderID = item.WorkOrderID;
                            dtoItem.WorkOrderUD = item.WorkOrderUD;
                            dtoItem.ProformaInvoiceNo = item.ProformaInvoiceNo;
                            dtoItem.ProductionItemGroupID = item.ProductionItemGroupID;
                            dtoItem.ETA = DateTime.Now.ToString("dd/MM/yyyy");

                            data.Add(dtoItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
