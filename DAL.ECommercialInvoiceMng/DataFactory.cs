using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DALBase;
namespace DAL.ECommercialInvoiceMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult, DTO.ECommercialInvoiceMng.ECommercialInvoice>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        
        private ECommercialInvoiceMngEntities CreateContext()
        {
            return new ECommercialInvoiceMngEntities(DALBase.Helper.CreateEntityConnectionString("ECommercialInvoiceMngModel"));
        }

        public override IEnumerable<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.ECommercialInvoiceMng.ECommercialInvoice GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    ECommercialInvoiceMng_ECommercialInvoice_View dbItem;
                    dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_View
                        .Include("ECommercialInvoiceMng_ECommercialInvoiceDetail_View")
                        .Include("ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View")
                        .Include("ECommercialInvoiceMng_ECommercialInvoiceDescription_View")
                        .Include("ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View")
                        .Include("ECommercialInvoiceMng_Booking_View")
                        .FirstOrDefault(o => o.ECommercialInvoiceID == id);
                    return converter.DB2DTO_ECommercialInvoice(dbItem);

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.ECommercialInvoice();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Deleted Success" };
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    ECommercialInvoice dbItem = context.ECommercialInvoice.FirstOrDefault(o => o.ECommercialInvoiceID == id);

                    if (dbItem == null)
                    {
                        throw new Exception("invoice not found!");
                    }

                    if (dbItem.IsConfirmed.HasValue)
                    {
                        throw new Exception("Invoice was cofirmed. You can not delete");
                    }

                    context.ECommercialInvoice.Remove(dbItem);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int isCreate = 0;
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    ECommercialInvoice dbItem = null;
                    if (id == 0)
                    {
                        //validate user who can make new invoice in old season
                        string currentSeason = Library.Helper.GetCurrentSeason();
                        if (DateTime.Now.Month <= 9)
                        {
                            // season in invoice need to be 1 month late
                            currentSeason = (DateTime.Now.Year - 1).ToString() + "/" + DateTime.Now.Year.ToString();
                        }
                        if (dtoItem.Season != currentSeason)
                        {
                            if (!fwFactory.HasSpecialPermission(dtoItem.UpdatedBy.Value, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CI_OLD_SEASON))
                            {
                                throw new Exception("You don't have permission to create new invoice in season " + dtoItem.Season);
                            }
                        }

                        //generate invoice no.
                        if (string.IsNullOrEmpty(dtoItem.InvoiceNo))
                        {
                            int? companyID = dtoItem.CompanyID;
                            if (companyID == 4) // 4: Eurofar
                            {
                                int? typeOfInvoice = dtoItem.TypeOfInvoice;
                                int? parentTypeOfInvoice = dtoItem.ParentTypeOfInvoice;
                                string Season = dtoItem.Season;
                                string Season_Year = Season.Substring(0, 4); //2018
                                string InvoiceNoTemplate = Season_Year.Substring(2, 2) + "42"; //1842
                                string ContainerInvoiceNoTemplate = Season_Year.Substring(2, 2) + "45"; //1845

                                if (typeOfInvoice == DALBase.Helper.FOB_INVOICE || parentTypeOfInvoice == DALBase.Helper.FOB_INVOICE)
                                {
                                    var invoices = context.ECommercialInvoice.Where(o => o.CompanyID == companyID && o.Season == Season && o.InvoiceNo.Substring(0, 4) == Season_Year && (o.TypeOfInvoice == DALBase.Helper.FOB_INVOICE || o.TypeOfInvoice == DALBase.Helper.CREDITNOTE_INVOICE)).OrderByDescending(o => o.InvoiceNo);
                                    if (invoices.ToList().Count() == 0)
                                    {
                                        dtoItem.InvoiceNo = Season_Year + "0001";
                                    }
                                    else
                                    {
                                        var x = invoices.FirstOrDefault();
                                        int iNo = 0;
                                        iNo = Convert.ToInt32(x.InvoiceNo.Substring(4, 4)) + 1;
                                        dtoItem.InvoiceNo = Season_Year + iNo.ToString().PadLeft(4, '0');
                                    }
                                }
                                else if (typeOfInvoice == DALBase.Helper.WAREHOUSE_INVOICE || typeOfInvoice == DALBase.Helper.OTHER_INVOICE || parentTypeOfInvoice == DALBase.Helper.WAREHOUSE_INVOICE || parentTypeOfInvoice == DALBase.Helper.OTHER_INVOICE)
                                {
                                    var invoices = context.ECommercialInvoice.Where(o => o.CompanyID == companyID
                                                                                            && o.InvoiceNo.Substring(0, 4) == InvoiceNoTemplate
                                                                                            && (o.TypeOfInvoice == DALBase.Helper.WAREHOUSE_INVOICE || o.TypeOfInvoice == DALBase.Helper.OTHER_INVOICE || o.TypeOfInvoice == DALBase.Helper.CREDITNOTE_INVOICE)).OrderByDescending(o => o.InvoiceNo);
                                    if (invoices.ToList().Count() == 0)
                                    {
                                        dtoItem.InvoiceNo = InvoiceNoTemplate + "0001";
                                    }
                                    else
                                    {
                                        var s = invoices.FirstOrDefault();
                                        int iNo = 0;
                                        iNo = Convert.ToInt32(s.InvoiceNo) + 1;
                                        dtoItem.InvoiceNo = iNo.ToString();
                                    }
                                }
                                else if (typeOfInvoice == DALBase.Helper.CONTAINER_TRANSPORT || parentTypeOfInvoice == DALBase.Helper.CONTAINER_TRANSPORT)
                                {
                                    var invoices = context.ECommercialInvoice.Where(o => o.CompanyID == companyID && o.InvoiceNo.Substring(0, 4) == ContainerInvoiceNoTemplate
                                                                                                                    && (o.TypeOfInvoice == DALBase.Helper.CONTAINER_TRANSPORT || o.TypeOfInvoice == DALBase.Helper.CREDITNOTE_INVOICE)).OrderByDescending(o => o.InvoiceNo);
                                    if (invoices.ToList().Count() == 0)
                                    {
                                        dtoItem.InvoiceNo = ContainerInvoiceNoTemplate + "0001";
                                    }
                                    else
                                    {
                                        var s = invoices.FirstOrDefault();
                                        int iNo = 0;
                                        iNo = Convert.ToInt32(s.InvoiceNo) + 1;
                                        dtoItem.InvoiceNo = iNo.ToString();
                                    }
                                }
                            }
                            else if (companyID == 13) // 13: Orange Pie
                            {
                                int? typeOfInvoice = dtoItem.TypeOfInvoice;
                                int year = DateTime.Now.Year;
                                if (DateTime.Now.Month >= 10)
                                {
                                    year = DateTime.Now.Year + 1;
                                }
                                if (typeOfInvoice == DALBase.Helper.CONTAINER_TRANSPORT)
                                {
                                    string invoice_pattern_container = "P" + year.ToString() + "5";
                                    var invoices_orangepie_container = context.ECommercialInvoice.Where(o => o.CompanyID == companyID && o.InvoiceNo.Substring(0, 6) == invoice_pattern_container).OrderByDescending(o => o.InvoiceNo);
                                    if (invoices_orangepie_container.ToList().Count() == 0)
                                    {
                                        dtoItem.InvoiceNo = invoice_pattern_container + "001";
                                    }
                                    else
                                    {
                                        var p = invoices_orangepie_container.FirstOrDefault();
                                        int iNo = Convert.ToInt32(p.InvoiceNo.Substring(6, 3)) + 1;
                                        dtoItem.InvoiceNo = p.InvoiceNo.Substring(0, 6) + iNo.ToString().PadLeft(3, '0');
                                    }
                                }
                                else
                                {
                                    string invoice_pattern = "P" + year.ToString() + "6";
                                    var invoices_orangepie = context.ECommercialInvoice.Where(o => o.CompanyID == companyID && o.InvoiceNo.Substring(0, 6) == invoice_pattern).OrderByDescending(o => o.InvoiceNo);
                                    if (invoices_orangepie.ToList().Count() == 0)
                                    {
                                        dtoItem.InvoiceNo = invoice_pattern + "001";
                                    }
                                    else
                                    {
                                        var p = invoices_orangepie.FirstOrDefault();
                                        int iNo = Convert.ToInt32(p.InvoiceNo.Substring(6, 3)) + 1;
                                        dtoItem.InvoiceNo = p.InvoiceNo.Substring(0, 6) + iNo.ToString().PadLeft(3, '0');
                                    }
                                }
                            }
                        }
                        dbItem = new ECommercialInvoice();
                        context.ECommercialInvoice.Add(dbItem);
                        isCreate = 1;
                    }
                    else
                    {
                        dbItem = context.ECommercialInvoice.FirstOrDefault(o => o.ECommercialInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        if (dtoItem.TypeOfInvoice == DALBase.Helper.CREDITNOTE_INVOICE && string.IsNullOrEmpty(dtoItem.Remark))
                        {
                            throw new Exception("You need fill-in remark (the reason credit note) for invoice with type is CREDIT NOTE");
                        }
                        //convert data to db
                        converter.DTO2DB_ECommercialInvoice(dtoItem, ref dbItem);

                        //remove orphan item
                        context.ECommercialInvoiceDescription.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.ECommercialInvoiceDescription.Remove(o));
                        context.ECommercialInvoiceExtDetail.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.ECommercialInvoiceExtDetail.Remove(o));
                        context.ECommercialInvoiceSparepartDetail.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.ECommercialInvoiceSparepartDetail.Remove(o));
                        context.ECommercialInvoiceDetailDescription.Local.Where(o => o.ECommercialInvoiceDetail == null).ToList().ForEach(o => context.ECommercialInvoiceDetailDescription.Remove(o));
                        context.ECommercialInvoiceDetail.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.ECommercialInvoiceDetail.Remove(o));
                        context.WarehouseInvoiceProductDetail.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.WarehouseInvoiceProductDetail.Remove(o));
                        context.WarehouseInvoiceSparepartDetail.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.WarehouseInvoiceSparepartDetail.Remove(o));
                        context.ECommercialInvoiceContainerTransport.Local.Where(o => o.ECommercialInvoice == null).ToList().ForEach(o => context.ECommercialInvoiceContainerTransport.Remove(o));
                        context.ECommercialInvoiceSparepartDetailDescription.Local.Where(o => o.ECommercialInvoiceSparepartDetail == null).ToList().ForEach(o => context.ECommercialInvoiceSparepartDetailDescription.Remove(o));

                        //recal amount
                        RecalAmount(ref dbItem);

                        //save data
                        context.SaveChanges();

                        //
                        // Create notification
                        //
                        string emailSubjectInvoice = "";
                        string emailBodyInvoice = "";
                        if (isCreate == 1) {
                            emailSubjectInvoice = "[Commercial Invoice] - " + dbItem.InvoiceNo + " - Created";
                            emailBodyInvoice = "Commercial Invoice " + dbItem.InvoiceNo + " has updated. </br> Click <a target='blank' href='http://app.tilsoft.bg/ECommercialInvoice/Edit/" + dbItem.ECommercialInvoiceID + "'><strong>here</strong></a> to go to Commercial Invoice or copy this link <strong> http://app.tilsoft.bg/ECommercialInvoice/Edit/" + dbItem.ECommercialInvoiceID + "</strong> to browser.";
                        }
                        else
                        {
                            emailSubjectInvoice = "[Commercial Invoice] - " + dbItem.InvoiceNo + " - Updated";
                            emailBodyInvoice = "Commercial Invoice " + dbItem.InvoiceNo + " has updated. </br> Click <a target='blank' href='http://app.tilsoft.bg/ECommercialInvoice/Edit/" + dbItem.ECommercialInvoiceID + "'><strong>here</strong></a> to go to Commercial Invoice or copy this link <strong> http://app.tilsoft.bg/ECommercialInvoice/Edit/" + dbItem.ECommercialInvoiceID + "</strong> to browser.";
                        }
                        
                        if (dtoItem.CompanyID == 13)
                        {
                            SendNotification(emailSubjectInvoice, emailBodyInvoice, "mingming@orangepine.com.sg;supplychain@orangepine.com.sg;hien.tran@anvietthinh.vn");
                            // add to NotificationMessage table
                            List<int> listUser = new List<int>();
                            listUser.Add(137);
                            listUser.Add(349);
                            listUser.Add(379);
                            foreach(var list in listUser)
                            {
                                NotificationMessage notifications = new NotificationMessage();
                                notifications.UserID = list;
                                notifications.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_ACCOUNTING;
                                notifications.NotificationMessageTitle = emailSubjectInvoice;
                                notifications.NotificationMessageContent = emailBodyInvoice;
                                context.NotificationMessage.Add(notifications);
                            }
                            
                            if (dbItem.ClientID == 519203)
                            {
                                SendNotification(emailSubjectInvoice, emailBodyInvoice, "nicole.dinh@anvietthinh.vn;hung.vien@anvietthinh.vn");
                                // add to NotificationMessage table
                                List<int> listUser2 = new List<int>();
                                listUser2.Add(243);
                                listUser2.Add(251);
                                foreach(var list2 in listUser2)
                                {
                                    NotificationMessage notificationd = new NotificationMessage();
                                    notificationd.UserID = list2;
                                    notificationd.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_ACCOUNTING;
                                    notificationd.NotificationMessageTitle = emailSubjectInvoice;
                                    notificationd.NotificationMessageContent = emailBodyInvoice;
                                    context.NotificationMessage.Add(notificationd);
                                }
                                
                            }
                        }

                        //save data
                        context.SaveChanges();

                        //
                        // generate factory order info caching
                        //
                        context.FactoryOrderInfoCacheMng_function_BuildCacheForCommercialInvoice(dbItem.ECommercialInvoiceID);

                        //reload data
                        dtoItem = GetData(dbItem.ECommercialInvoiceID, out notification);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public DTO.ECommercialInvoiceMng.DataContainer GetDataContainer(int id, int? typeOfInvoice, int? internalCompanyID, int? clientID, int? parentID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    DTO.ECommercialInvoiceMng.DataContainer dtoItem = new DTO.ECommercialInvoiceMng.DataContainer();

                    // added by themy
                    dtoItem.ContainerTransports = new List<DTO.ECommercialInvoiceMng.ContainerTransport>();
                    dtoItem.ClientOfferCostItems = new List<DTO.ECommercialInvoiceMng.ClientOfferCostItem>();
                    // end

                    if (id > 0)
                    {
                        ECommercialInvoiceMng_ECommercialInvoice_View dbItem;
                        dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_View
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceDetail_View.ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_View")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceDescription_View")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_View.ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_View")
                                .Include("ECommercialInvoiceMng_Booking_View")
                                .Include("ECommercialInvoiceMng_CreditNote_View")
                                .Include("ECommercialInvoiceMng_WarehouseInvoiceProductDetail_View")
                                .Include("ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_View")
                                .Include("ECommercialInvoiceMng_WarehouseImport_View")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View")
                                .FirstOrDefault(o => o.ECommercialInvoiceID == id);
                        DTO.ECommercialInvoiceMng.ECommercialInvoice ECommercialInvoiceDTOItem = converter.DB2DTO_ECommercialInvoice(dbItem);
                        dtoItem.ECommercialInvoiceData = ECommercialInvoiceDTOItem;

                        if (dtoItem.ECommercialInvoiceData.BookingID.HasValue)
                        {
                            //get list container
                            var containerItems = context.ECommercialInvoiceMng_ContainerTransport_View.Where(o => o.BookingID == dtoItem.ECommercialInvoiceData.BookingID);
                            dtoItem.ContainerTransports = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ContainerTransport_View>, List<DTO.ECommercialInvoiceMng.ContainerTransport>>(containerItems.ToList());

                            //get list client offer cost
                            List<ECommercialInvoiceMng_ClientOfferCostItem_View> clientOffer = context.ECommercialInvoiceMng_function_GetClientOfferCostItem(dtoItem.ECommercialInvoiceData.ClientID, dtoItem.ECommercialInvoiceData.BookingID).ToList();
                            dtoItem.ClientOfferCostItems = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ClientOfferCostItem_View>, List<DTO.ECommercialInvoiceMng.ClientOfferCostItem>>(clientOffer);
                        }
                    }
                    else
                    {
                        if (typeOfInvoice == DALBase.Helper.CREDITNOTE_INVOICE)
                        {
                            ECommercialInvoiceMng_ECommercialInvoice_View dbItem;
                            dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_View
                                    .Include("ECommercialInvoiceMng_ECommercialInvoiceDetail_View.ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_View")
                                    .Include("ECommercialInvoiceMng_ECommercialInvoiceExtDetail_View")
                                    .Include("ECommercialInvoiceMng_ECommercialInvoiceDescription_View")
                                    .Include("ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View")
                                    .Include("ECommercialInvoiceMng_Booking_View")
                                    .Include("ECommercialInvoiceMng_CreditNote_View")
                                    .Include("ECommercialInvoiceMng_WarehouseInvoiceProductDetail_View")
                                    .Include("ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_View")
                                    .Include("ECommercialInvoiceMng_WarehouseImport_View")
                                    .Include("ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_View")
                                    .FirstOrDefault(o => o.ECommercialInvoiceID == parentID);
                            DTO.ECommercialInvoiceMng.ECommercialInvoice ECommercialInvoiceDTOItem = converter.DB2DTO_ECommercialInvoice(dbItem);
                            dtoItem.ECommercialInvoiceData = ECommercialInvoiceDTOItem;

                            //assign type of invoice
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceID = null;
                            dtoItem.ECommercialInvoiceData.InvoiceNo = "";
                            dtoItem.ECommercialInvoiceData.TypeOfInvoice = DALBase.Helper.CREDITNOTE_INVOICE;
                            dtoItem.ECommercialInvoiceData.TypeOfInvoiceText = GetInvoiceType().Where(o => o.InvoiceTypeValue == typeOfInvoice).FirstOrDefault().InvoiceTypeText;
                            dtoItem.ECommercialInvoiceData.ParentID = parentID;
                            dtoItem.ECommercialInvoiceData.ParentTypeOfInvoice = dbItem.TypeOfInvoice;
                            dtoItem.ECommercialInvoiceData.IsConfirmed = null;
                            dtoItem.ECommercialInvoiceData.ConfirmedBy = null;
                            dtoItem.ECommercialInvoiceData.ConfirmedDate = null;
                            dtoItem.ECommercialInvoiceData.CreatedBy = null;
                            dtoItem.ECommercialInvoiceData.CreatedDate = null;
                            dtoItem.ECommercialInvoiceData.UpdatedBy = null;
                            dtoItem.ECommercialInvoiceData.UpdatedDate = null;

                            //make negative all item
                            foreach (var item in dtoItem.ECommercialInvoiceData.ECommercialInvoiceDescriptions.Where(o => o.TotalAmount != 0))
                            {
                                item.TotalAmount = -item.TotalAmount;
                                item.ECommercialInvoiceDescriptionID = -item.ECommercialInvoiceDescriptionID;
                            }
                            foreach (var item in dtoItem.ECommercialInvoiceData.ECommercialInvoiceDetails)
                            {
                                item.Quantity = -item.Quantity;
                                item.ECommercialInvoiceDetailID = -item.ECommercialInvoiceDetailID;
                            }
                            foreach (var item in dtoItem.ECommercialInvoiceData.ECommercialInvoiceSparepartDetails)
                            {
                                item.Quantity = -item.Quantity;
                                item.ECommercialInvoiceSparepartDetailID = -item.ECommercialInvoiceSparepartDetailID;
                            }
                            foreach (var item in dtoItem.ECommercialInvoiceData.ECommercialInvoiceExtDetails)
                            {
                                item.TotalAmount = -item.TotalAmount;
                                item.ECommercialInvoiceExtDetailID = -item.ECommercialInvoiceExtDetailID;
                            }

                            //warehouse
                            foreach (var item in dtoItem.ECommercialInvoiceData.WarehouseInvoiceProductDetails)
                            {
                                item.Quantity = -item.Quantity;
                                item.WarehouseInvoiceProductDetailID = -item.WarehouseInvoiceProductDetailID;
                            }
                            foreach (var item in dtoItem.ECommercialInvoiceData.WarehouseInvoiceSparepartDetails)
                            {
                                item.Quantity = -item.Quantity;
                                item.WarehouseInvoiceSparepartDetailID = -item.WarehouseInvoiceSparepartDetailID;
                            }

                            //container transport
                            foreach (var item in dtoItem.ECommercialInvoiceData.ECommercialInvoiceContainerTransports)
                            {
                                item.TotalAmount = -item.TotalAmount;
                                item.ECommercialInvoiceContainerTransportID = -item.ECommercialInvoiceContainerTransportID;
                            }
                        }
                        else
                        {
                            dtoItem.ECommercialInvoiceData = new DTO.ECommercialInvoiceMng.ECommercialInvoice();
                            dtoItem.ECommercialInvoiceData.Season = Library.OMSHelper.Helper.GetCurrentSeason();
                            dtoItem.ECommercialInvoiceData.TypeOfInvoice = typeOfInvoice; // 1 : FOB INVOICE, 2 : WAREHOUSE INVOICE, 3: OTHER INVOICE, 4: CREDIT NOTE INVOICE
                            dtoItem.ECommercialInvoiceData.TypeOfInvoiceText = GetInvoiceType().Where(o => o.InvoiceTypeValue == typeOfInvoice).FirstOrDefault().InvoiceTypeText;
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceDetails = new List<DTO.ECommercialInvoiceMng.ECommercialInvoiceDetail>();
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceDescriptions = new List<DTO.ECommercialInvoiceMng.ECommercialInvoiceDescription>();
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceExtDetails = new List<DTO.ECommercialInvoiceMng.ECommercialInvoiceExtDetail>();
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceSparepartDetails = new List<DTO.ECommercialInvoiceMng.ECommercialInvoiceSparepartDetail>();
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceSampleDetails = new List<DTO.ECommercialInvoiceMng.ECommercialInvoiceSampleDetail>();
                            dtoItem.ECommercialInvoiceData.Bookings = new List<DTO.ECommercialInvoiceMng.Booking>();
                            dtoItem.ECommercialInvoiceData.WarehouseInvoiceProductDetails = new List<DTO.ECommercialInvoiceMng.WarehouseInvoiceProductDetail>();
                            dtoItem.ECommercialInvoiceData.WarehouseInvoiceSparepartDetails = new List<DTO.ECommercialInvoiceMng.WarehouseInvoiceSparepartDetail>();
                            dtoItem.ECommercialInvoiceData.ECommercialInvoiceContainerTransports = new List<DTO.ECommercialInvoiceMng.ECommercialInvoiceContainerTransport>();

                            ECommercialInvoiceMng_Client_View dbClient = context.ECommercialInvoiceMng_Client_View.Where(o => o.ClientID == clientID).FirstOrDefault();
                            dtoItem.ECommercialInvoiceData.ClientID = clientID;
                            dtoItem.ECommercialInvoiceData.ClientUD = dbClient.ClientUD;
                            dtoItem.ECommercialInvoiceData.ClientNM = dbClient.ClientNM;
                            dtoItem.ECommercialInvoiceData.ClientAddress = dbClient.ClientAddress;
                            dtoItem.ECommercialInvoiceData.VATNo = dbClient.VATNo;
                            dtoItem.ECommercialInvoiceData.Telephone = dbClient.Telephone;
                            dtoItem.ECommercialInvoiceData.Fax = dbClient.Fax;

                            dtoItem.ECommercialInvoiceData.DeliveryTermID = dbClient.DeliveryTermID;
                            dtoItem.ECommercialInvoiceData.DeliveryTypeNM = dbClient.DeliveryTermNM;
                            dtoItem.ECommercialInvoiceData.DeliveryTerm = dbClient.DeliveryTermNM;
                            dtoItem.ECommercialInvoiceData.PaymentTermID = dbClient.PaymentTermID;
                            dtoItem.ECommercialInvoiceData.PaymentTypeNM = dbClient.PaymentTermNM;
                            dtoItem.ECommercialInvoiceData.PaymentTerm = dbClient.PaymentTermNM;

                            dtoItem.ECommercialInvoiceData.CompanyID = internalCompanyID; // assign company id

                            // added by themy
                            if (dtoItem.ECommercialInvoiceData.BookingID.HasValue)
                            {
                                //get list container
                                var containerItems = context.ECommercialInvoiceMng_ContainerTransport_View.Where(o => o.BookingID == dtoItem.ECommercialInvoiceData.BookingID);
                                dtoItem.ContainerTransports = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ContainerTransport_View>, List<DTO.ECommercialInvoiceMng.ContainerTransport>>(containerItems.ToList());

                                //get list client offer cost
                                List<ECommercialInvoiceMng_ClientOfferCostItem_View> clientOffer = context.ECommercialInvoiceMng_function_GetClientOfferCostItem(dtoItem.ECommercialInvoiceData.ClientID, dtoItem.ECommercialInvoiceData.BookingID).ToList();
                                dtoItem.ClientOfferCostItems = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ClientOfferCostItem_View>, List<DTO.ECommercialInvoiceMng.ClientOfferCostItem>>(clientOffer);
                            }
                            // end
                        }
                    }

                    // get support data
                    dtoItem.DeliveryTerms = converter.DB2DTO_DeliveryTerm(context.ECommercialInvoiceMng_DeliveryTerm.ToList());
                    dtoItem.PaymentTerms = converter.DB2DTO_PaymentTerm(context.ECommercialInvoiceMng_PaymentTerm.ToList());
                    dtoItem.TurnOverLedgerAccounts = converter.DB2DTO_TurnOverLedgerAccount(context.ECommercialInvoiceMng_TurnOverLedgerAccount_View.ToList());
                    dtoItem.VATPercents = new DAL.Support.DataFactory().GetVATPercent();
                    dtoItem.Currency = new DAL.Support.DataFactory().GetCurrency().ToList();
                    dtoItem.CostTypes = GetCostTypes().ToList();
                    dtoItem.Seasons = new DAL.Support.DataFactory().GetSeason().ToList();
                    dtoItem.ReportTemplates = new DAL.Support.DataFactory().GetReportTemplate().ToList();
                    dtoItem.ClientCostItems = converter.DB2DTO_ClientCostItem(context.ECommercialInvoiceMng_ClientCostItem_View.ToList());

                    //
                    // rem code by Themy - this section must be applied only when invoice type is not credit note
                    //
                    //if (dtoItem.ECommercialInvoiceData.BookingID.HasValue)
                    //{
                    //    //get list container
                    //    var containerItems = context.ECommercialInvoiceMng_ContainerTransport_View.Where(o => o.BookingID == dtoItem.ECommercialInvoiceData.BookingID);
                    //    dtoItem.ContainerTransports = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ContainerTransport_View>, List<DTO.ECommercialInvoiceMng.ContainerTransport>>(containerItems.ToList());

                    //    //get list client offer cost
                    //    List<ECommercialInvoiceMng_ClientOfferCostItem_View> clientOffer = context.ECommercialInvoiceMng_function_GetClientOfferCostItem(dtoItem.ECommercialInvoiceData.ClientID, dtoItem.ECommercialInvoiceData.BookingID).ToList();
                    //    dtoItem.ClientOfferCostItems = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ClientOfferCostItem_View>, List<DTO.ECommercialInvoiceMng.ClientOfferCostItem>>(clientOffer);
                    //}
                    // end

                    // Get defautl report template
                    if (dtoItem.ECommercialInvoiceData.DefaultCiReport == null)
                    {
                        var defaultRpt = dtoItem.ReportTemplates.FirstOrDefault(x => x.ReportType == "CI" && x.IsDefault == true).ReportTemplateNM;
                        dtoItem.ECommercialInvoiceData.DefaultCiReport = defaultRpt;
                    }

                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.DataContainer();
            }
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.PurchasingInvoice> GetPurchasingInvoice(int clientID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    totalRows = 0;
                    List<int?> pIDs = context.ECommercialInvoiceMng_PurchasingInvoiceDetail_View.Where(o => o.ClientID == clientID).Select(s => s.PurchasingInvoiceID).ToList();
                    pIDs.AddRange(context.ECommercialInvoiceMng_PurchasingInvoiceSparepartDetail_View.Where(o => o.ClientID == clientID).Select(s => s.PurchasingInvoiceID).ToList());
                    pIDs.AddRange(context.ECommercialInvoiceMng_PurchasingInvoiceSampleDetail_View.Where(o => o.ClientID == clientID).Select(s => s.PurchasingInvoiceID).ToList());
                    var result = context.ECommercialInvoiceMng_PurchasingInvoice_View.Where(o => pIDs.Contains(o.PurchasingInvoiceID));
                        //.Include("ECommercialInvoiceMng_PurchasingInvoiceDetail_View")
                        //.Include("ECommercialInvoiceMng_PurchasingInvoiceSparepartDetail_View")
                        //.Where(o => pIDs.Contains(o.PurchasingInvoiceID));
                    return converter.DB2DTO_PurchasingInvoice(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ECommercialInvoiceMng.PurchasingInvoice>();
            }
        }

        //private IEnumerable<DTO.ECommercialInvoiceMng.VATRate> GetVATRates()
        //{
        //    List<DTO.ECommercialInvoiceMng.VATRate> VATRates = new List<DTO.ECommercialInvoiceMng.VATRate>();

        //    VATRates.Add(new DTO.ECommercialInvoiceMng.VATRate { RateValue = 0, RateText = "0%"});
        //    VATRates.Add(new DTO.ECommercialInvoiceMng.VATRate { RateValue = 21, RateText = "21%" });

        //    return VATRates;
        //}

        private IEnumerable<DTO.ECommercialInvoiceMng.CostType> GetCostTypes()
        {
            List<DTO.ECommercialInvoiceMng.CostType> CostTypes = new List<DTO.ECommercialInvoiceMng.CostType>();

            CostTypes.Add(new DTO.ECommercialInvoiceMng.CostType { CostTypeValue = DALBase.Helper.COST_TYPE_DISCOUNT, CostTypeText = "DISCOUNT" });
            CostTypes.Add(new DTO.ECommercialInvoiceMng.CostType { CostTypeValue = DALBase.Helper.COST_TYPE_SEA_FREIGHT, CostTypeText = "SEA FREIGHT" });
            CostTypes.Add(new DTO.ECommercialInvoiceMng.CostType { CostTypeValue = DALBase.Helper.COST_TYPE_TRUCKING, CostTypeText = "TRUCKING" });
            CostTypes.Add(new DTO.ECommercialInvoiceMng.CostType { CostTypeValue = DALBase.Helper.COST_TYPE_OTHER, CostTypeText = "OTHER" });
            CostTypes.Add(new DTO.ECommercialInvoiceMng.CostType { CostTypeValue = DALBase.Helper.COST_TYPE_WAREHOUSE, CostTypeText = "WAREHOUSE" });

            return CostTypes;
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout GetInvoicePrintoutData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    ECommercialInvoice_ReportView dbItem;
                    dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_ReportView
                        .Include("ECommercialInvoiceDetail_ReportView.ECommercialInvoiceDetailDescription_ReportView")
                        .Include("ECommercialInvoiceExtDetail_ReportView")
                        .Include("ECommercialInvoiceDescription_ReportView")
                        .Include("Container_ReportView")
                        .Include("ECommercialInvoiceSparepartDetail_ReportView.ECommercialInvoiceSparepartDetailDescription_ReportView")
                        .Include("ECommercialInvoiceMng_ECommercialInvoiceSampleDetail_ReportView.ECommercialInvoiceMng_ECommercialInvoiceSampleDetailDescription_ReportView")
                        .Include("ECommercialInvoiceContainerTransport_ReportView")
                        .FirstOrDefault(o => o.ECommercialInvoiceID == id);

                    // Get defautl report template
                    if (dbItem.ReportName == null)
                    {
                        var defaultRpt = context.ReportTemplate.FirstOrDefault(x => x.ReportType == "CI" && x.IsDefault == true).ReportTemplateNM;
                        dbItem.ReportName = defaultRpt;
                    }

                    if (dbItem.TypeOfInvoice == DALBase.Helper.FOB_INVOICE || dbItem.ParentTypeOfInvoice == DALBase.Helper.FOB_INVOICE)
                    {
                        return converter.DB2DTO_Printout(dbItem);
                    }
                    else if (dbItem.TypeOfInvoice == DALBase.Helper.WAREHOUSE_INVOICE || dbItem.ParentTypeOfInvoice == DALBase.Helper.WAREHOUSE_INVOICE)
                    {
                        return converter.DB2DTO_Printout_WarehouseInvoice(dbItem);
                    }
                    else if (dbItem.TypeOfInvoice == DALBase.Helper.OTHER_INVOICE || dbItem.ParentTypeOfInvoice == DALBase.Helper.OTHER_INVOICE)
                    {
                        return converter.DB2DTO_Printout_Other(dbItem);
                    }
                    else if (dbItem.TypeOfInvoice == DALBase.Helper.CONTAINER_TRANSPORT || (dbItem.TypeOfInvoice == DALBase.Helper.CREDITNOTE_INVOICE && dbItem.ParentTypeOfInvoice == DALBase.Helper.CONTAINER_TRANSPORT))
                    {
                        return converter.DB2DTO_Printout_TransportContainer(dbItem);
                    }
                    return new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.InvoiceContainerPrintout();
            }
        }

        private void RecalAmount(ref ECommercialInvoice dbItem)
        {
            decimal? net_amount = 0;
            decimal? vat_amount = 0;

            decimal? discount_and_warehouse_cost = 0;
            decimal? remaining_cost = 0;

            foreach (ECommercialInvoiceDetail dbDetail in dbItem.ECommercialInvoiceDetail)
            {
                net_amount += (dbDetail.UnitPrice == null ? 0 : dbDetail.UnitPrice) * (dbDetail.Quantity == null ? 0 : dbDetail.Quantity);
            }

            foreach (ECommercialInvoiceSparepartDetail dbSparepart in dbItem.ECommercialInvoiceSparepartDetail)
            {
                net_amount += (dbSparepart.UnitPrice == null ? 0 : dbSparepart.UnitPrice) * (dbSparepart.Quantity == null ? 0 : dbSparepart.Quantity);
            }

            foreach (ECommercialInvoiceSampleDetail dbSample in dbItem.ECommercialInvoiceSampleDetail)
            {
                net_amount += (dbSample.UnitPrice == null ? 0 : dbSample.UnitPrice) * (dbSample.Quantity == null ? 0 : dbSample.Quantity);
            }

            foreach (ECommercialInvoiceExtDetail dbExtDetail in dbItem.ECommercialInvoiceExtDetail) // table ECommercialInvoiceExtDetail was use in cas type of invoice is OTHER
            {
                net_amount += (dbExtDetail.TotalAmount == null ? 0 : dbExtDetail.TotalAmount);
            }

            foreach (var item in dbItem.WarehouseInvoiceProductDetail)
            {
                net_amount += (item.UnitPrice == null ? 0 : item.UnitPrice) * (item.Quantity == null ? 0 : item.Quantity);
            }

            foreach (var item in dbItem.WarehouseInvoiceSparepartDetail)
            {
                net_amount += (item.UnitPrice == null ? 0 : item.UnitPrice) * (item.Quantity == null ? 0 : item.Quantity);
            }

            foreach (var item in dbItem.ECommercialInvoiceContainerTransport)
            {
                net_amount += (!item.TotalAmount.HasValue ? 0 : item.TotalAmount.Value);
            }

            foreach (ECommercialInvoiceDescription dbDescrip in dbItem.ECommercialInvoiceDescription)
            {
                if (dbDescrip.CostType == DALBase.Helper.COST_TYPE_DISCOUNT || dbDescrip.CostType == DALBase.Helper.COST_TYPE_WAREHOUSE)
                {
                    discount_and_warehouse_cost += (dbDescrip.TotalAmount == null ? 0 : dbDescrip.TotalAmount);
                }
                else
                {
                    remaining_cost += (dbDescrip.TotalAmount == null ? 0 : dbDescrip.TotalAmount);
                }
            }
            net_amount = net_amount + discount_and_warehouse_cost;
            vat_amount = net_amount * (dbItem.VATRate == null ? 0 : dbItem.VATRate) / 100;

            dbItem.NetAmount = net_amount;
            dbItem.VATAmount = vat_amount;
            dbItem.TotalAmount = (net_amount + vat_amount + remaining_cost);
        }

        public DTO.ECommercialInvoiceMng.PackingListContainerPrintout GetPackingListPrintoutData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    ECommercialInvoice_ReportView dbItem;
                    dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_ReportView
                        .Include("PackingListDetail_ReportView.ECommercialInvoiceDetailDescription_ReportView")
                        .Include("PackingListDetailExtend_ReportView")
                        .Include("ECommercialInvoiceDescription_ReportView")
                        .Include("Container_ReportView")
                        .FirstOrDefault(o => o.ECommercialInvoiceID == id);
                    return converter.DB2DTO_PackingListPrintout(dbItem);

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.PackingListContainerPrintout();
            }
        }

        public List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout> GetPackingListPrintoutData_PerCont(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout> dataItem = new List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout>();
            //try to get data
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    List<string> ContNos = new List<string>();
                    List <DTO.ECommercialInvoiceMng.OrderNoDTO> OrderNoDTOs = new List<DTO.ECommercialInvoiceMng.OrderNoDTO>();
                    
                    var Conts = context.ECommercialInvoiceMng_PackingListDetail_ReportView.Where(o=>o.ECommercialInvoiceID == id).Select(o=>new { o.ContainerNo }).GroupBy(o=>o.ContainerNo).ToList();
                    foreach(var item in Conts)
                    {
                        ContNos.Add(item.Key);
                    }

                    var OrderNos = context.ECommercialInvoiceMng_PackingListDetail_ReportView.Where(o => o.ECommercialInvoiceID == id).Select(o => new { o.ProformaInvoiceNo, o.ContainerNo}).GroupBy(o => new { o.ProformaInvoiceNo, o.ContainerNo }).ToList();
                    foreach (var item in OrderNos)
                    {
                        DTO.ECommercialInvoiceMng.OrderNoDTO OrderNoDTO = new DTO.ECommercialInvoiceMng.OrderNoDTO();
                        OrderNoDTO.ProformaInvoiceNo = item.Key.ProformaInvoiceNo;
                        OrderNoDTO.ContainerNo = item.Key.ContainerNo;
                        OrderNoDTOs.Add(OrderNoDTO);
                    }

                    ECommercialInvoice_ReportView dbItem;
                    dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_ReportView
                        .Include("PackingListDetail_ReportView.ECommercialInvoiceDetailDescription_ReportView")
                        .Include("PackingListDetailExtend_ReportView")
                        .Include("ECommercialInvoiceDescription_ReportView")
                        .Include("Container_ReportView")
                        .FirstOrDefault(o => o.ECommercialInvoiceID == id);
                    return converter.DB2DTO_PackingListPrintout_PerCont(dbItem, ContNos, OrderNoDTOs);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ECommercialInvoiceMng.PackingListContainerPrintout>();
            }
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.WarehousePickingList> GetWarehousePickingList(int clientID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    var result = context.ECommercialInvoiceMng_WarehousePickingList_View
                        .Include("ECommercialInvoiceMng_WarehousePickingListDetail_View")
                        .Where(o => o.ClientID == clientID);
                    return converter.DB2DTO_WarehousePickingList(result.ToList());

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ECommercialInvoiceMng.WarehousePickingList>();
            }
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.InvoiceType> GetInvoiceType()
        {
            List<DTO.ECommercialInvoiceMng.InvoiceType> InvoiceTypes = new List<DTO.ECommercialInvoiceMng.InvoiceType>();
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = DALBase.Helper.FOB_INVOICE, InvoiceTypeText = "FOB" });
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = DALBase.Helper.WAREHOUSE_INVOICE, InvoiceTypeText = "WAREHOUSE" });
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = DALBase.Helper.OTHER_INVOICE, InvoiceTypeText = "OTHER" });
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = DALBase.Helper.CREDITNOTE_INVOICE, InvoiceTypeText = "CREDIT NOTE" });
            InvoiceTypes.Add(new DTO.ECommercialInvoiceMng.InvoiceType { InvoiceTypeValue = DALBase.Helper.CONTAINER_TRANSPORT, InvoiceTypeText = "CONTAINER TRANSPORT" });
            return InvoiceTypes;
        }

        public bool ConfirmInvoice(int eCommercialInvoiceID, bool isConfirmed, int confirmedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = isConfirmed ? "Invoice have been confirmed success" : "Invoice have been un-confirmed success" };
            List<DTO.ECommercialInvoiceMng.WarehouseInvoice> dataEmailNotification = new List<DTO.ECommercialInvoiceMng.WarehouseInvoice>();
            ECommercialInvoice dbItem;
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    dbItem = context.ECommercialInvoice.Where(o => o.ECommercialInvoiceID == eCommercialInvoiceID).FirstOrDefault();
                    dbItem.IsConfirmed = isConfirmed;

                    dbItem.ConfirmedBy = confirmedBy;
                    dbItem.ConfirmedDate = DateTime.Now;
                    context.SaveChanges();
                    dataEmailNotification = converter.DB2DTO_WarehouseInvoice(context.ECommercialInvoiceMng_WarehouseInvoice_View.Where(s => s.ECommercialInvoiceID == eCommercialInvoiceID && s.IsConfirmed == true && s.TypeOfInvoice == 2).ToList());
                }
                //create content mail
                string bodyHasValue = "";
                string bodyNotValue = "";

                //create content body of table
                string tablebodyValue = "";
                string tablebodyNotValue = "";

                //value of much total has price
                int? totalQnt = 0;
                decimal? totalPurchasingPrice = 0;
                decimal? totalPurchasingAmount = 0;
                decimal? totalUnitPrice = 0;
                decimal? totalSaleAmount = 0;
                decimal? totalGrossMargin = 0;
                decimal? totalGrossMarginPercent = 0;

                //value of much total not price
                int? totalQntNotValue = 0;
                decimal? totalPurchasingPriceNotValue = 0;
                decimal? totalPurchasingAmountNotValue = 0;
                decimal? totalUnitPriceNotValue = 0;
                decimal? totalSaleAmountNotValue = 0;
                decimal? totalGrossMarginNotValue = 0;
                decimal? totalGrossMarginPercentNotValue = 0;

                for (int i = 0; i < dataEmailNotification.Count; i++)
                {
                    var list = dataEmailNotification[i];
                    int price = Convert.ToInt32(list.PurchasingPrice);
                    if (list.UnitPrice == null)
                    {
                        list.UnitPrice = 0;
                    }
                    decimal? GrossMarginPercent = 0;
                    if (Convert.ToInt32(list.SaleAmount) == 0 || list.SaleAmount == null)
                    {
                        GrossMarginPercent = 0;
                    }
                    else
                    {
                        GrossMarginPercent = (list.GrossMargin / list.SaleAmount) * 100;
                        GrossMarginPercent = Math.Round(Convert.ToDecimal(GrossMarginPercent), 2);
                    }
                    if (price > 0 && list.PurchasingPrice != null)
                    {
                        //bodyHasValue += "Purchasing price: " + list.PurchasingPrice + "; Purchasing amount: " + list.PurchasingAmount + "; Gross margin: " + list.GrossMargin + "; Gross margin (%): " + GrossMarginPercent;
                        tablebodyValue += BodyTableSendMail(list, GrossMarginPercent);
                        totalQnt += list.Quantity;
                        totalPurchasingPrice += list.PurchasingPrice;
                        totalPurchasingAmount += list.PurchasingAmount;
                        totalUnitPrice += list.UnitPrice;
                        totalSaleAmount += list.SaleAmount;
                        totalGrossMargin += list.GrossMargin;
                        //totalGrossMarginPercent += GrossMarginPercent;
                    }
                    if (price <= 0 || list.PurchasingPrice == null)
                    {
                        //bodyNotValue += "Purchasing price: " + list.PurchasingPrice + "; Purchasing amount: " + list.PurchasingAmount + "; Gross margin: " + list.GrossMargin + "; Gross margin (%): " + GrossMarginPercent;
                        tablebodyNotValue += BodyTableSendMail(list, GrossMarginPercent);
                        totalQntNotValue += list.Quantity;
                        totalPurchasingPriceNotValue += list.PurchasingPrice;
                        totalPurchasingAmountNotValue += list.PurchasingAmount;
                        totalUnitPriceNotValue += list.UnitPrice;
                        totalSaleAmountNotValue += list.SaleAmount;
                        totalGrossMarginNotValue += list.GrossMargin;
                        //totalGrossMarginPercentNotValue += GrossMarginPercent;
                    }
                    if (i == (dataEmailNotification.Count - 1))
                    {
                        string totalSaleAmountFormat = "0,00";
                        string totalGrossMarginPercentFormat = "0,00%";
                        string totalGrossMarginFormat = "0,00";
                        string totalPurchasingPriceFormat = "0,00";
                        string totalPurchasingAmountFormat = "0,00";
                        string totalUnitPriceFormat = "0,00";
                        if (tablebodyValue != "")
                        {
                            if (totalSaleAmount == 0)
                            {
                                totalGrossMarginPercent = 0;
                            }
                            else
                            {
                                totalGrossMarginPercent = (totalGrossMargin / totalSaleAmount) * 100;
                                totalGrossMarginPercentFormat = String.Format("{0:#,##0.00}", totalGrossMarginPercent) + "%";
                            }
                            totalGrossMarginFormat = String.Format("{0:#,##0.00}", totalGrossMargin ?? 0);
                            totalSaleAmountFormat = String.Format("{0:#,##0.00}", totalSaleAmount ?? 0);
                            totalPurchasingPriceFormat = String.Format("{0:#,##0.00}", totalPurchasingPrice ?? 0);
                            totalPurchasingAmountFormat = String.Format("{0:#,##0.00}", totalPurchasingAmount ?? 0);
                            totalUnitPriceFormat = String.Format("{0:#,##0.00}", totalUnitPrice ?? 0);
                            bodyHasValue += HeaderTableSendMail(totalQnt, totalPurchasingPriceFormat, totalPurchasingAmountFormat, totalUnitPriceFormat, totalSaleAmountFormat, totalGrossMarginFormat, totalGrossMarginPercentFormat) + tablebodyValue + "</table>";
                        }
                        string totalSaleAmountNotValueFormat = "0,00";
                        string totalGrossMarginPercentNotValueFormat = "0,00%";
                        string totalGrossMarginNotValueFormat = "0,00";
                        string totalPurchasingPriceNotValueFormat = "0,00";
                        string totalPurchasingAmountNotValueFormat = "0,00";
                        string totalUnitPriceNotValueFormat = "0,00";
                        if (tablebodyNotValue != "")
                        {
                            if (totalSaleAmountNotValue == 0)
                            {
                                totalGrossMarginPercentNotValue = 0;
                            }
                            else
                            {
                                totalGrossMarginPercentNotValue = (totalGrossMarginNotValue / totalSaleAmountNotValue) * 100;
                                totalGrossMarginPercentNotValueFormat = String.Format("{0:#,##0.00}", totalGrossMarginPercentNotValue) + "%";
                            }
                            totalGrossMarginNotValueFormat = String.Format("{0:#,##0.00}", totalGrossMarginNotValue ?? 0);
                            totalSaleAmountNotValueFormat = String.Format("{0:#,##0.00}", totalSaleAmountNotValue ?? 0);
                            totalPurchasingPriceNotValueFormat = String.Format("{0:#,##0.00}", totalPurchasingPriceNotValue ?? 0);
                            totalPurchasingAmountNotValueFormat = String.Format("{0:#,##0.00}", totalPurchasingAmountNotValue ?? 0);
                            totalUnitPriceNotValueFormat = String.Format("{0:#,##0.00}", totalUnitPriceNotValue ?? 0);
                            bodyNotValue += HeaderTableSendMail(totalQntNotValue, totalPurchasingPriceNotValueFormat, totalPurchasingAmountNotValueFormat, totalUnitPriceNotValueFormat, totalSaleAmountNotValueFormat, totalGrossMarginNotValueFormat, totalGrossMarginPercentNotValueFormat) + tablebodyNotValue + "</table>";
                        }
                    }
                }
                string emailSubjectHasValue = "Warehouse Invoice: Purchasing invoice is " + dbItem.InvoiceNo + ". The list Purchasing price is available";
                string emailSubjectNotValue = "Warehouse Invoice: The list Purchasing invoice is " + dbItem.InvoiceNo + ". Purchasing price is not available";
                if (bodyHasValue != "")
                {
                    SendNotification(emailSubjectHasValue, bodyHasValue, "Iron@eurofar.nl;joop@eurofar.nl");
                    // add notification support
                    List<int> listUser2 = new List<int>();
                    listUser2.Add(26);
                    listUser2.Add(69);
                    foreach (var list2 in listUser2)
                    {
                        using (ECommercialInvoiceMngEntities context = CreateContext())
                        {
                            NotificationMessage notificationd = new NotificationMessage();
                            notificationd.UserID = list2;
                            notificationd.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_ACCOUNTING;
                            notificationd.NotificationMessageTitle = emailSubjectHasValue;
                            notificationd.NotificationMessageContent = bodyHasValue;
                            context.NotificationMessage.Add(notificationd);
                        }
                    }
                }
                if (bodyNotValue != "")
                {
                    SendNotification(emailSubjectNotValue, bodyNotValue, "joop@eurofar.nl");
                    // add notification support
                    List<int> listUser2 = new List<int>();                    
                    listUser2.Add(69);
                    foreach (var list2 in listUser2)
                    {
                        using (ECommercialInvoiceMngEntities context = CreateContext())
                        {
                            NotificationMessage notificationd = new NotificationMessage();
                            notificationd.UserID = list2;
                            notificationd.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_ACCOUNTING;
                            notificationd.NotificationMessageTitle = emailSubjectHasValue;
                            notificationd.NotificationMessageContent = bodyHasValue;
                            context.NotificationMessage.Add(notificationd);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice> GetInitFobInvoice(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ClientUD = string.Empty;
            string ClientNM = string.Empty;

            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();

            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.ECommercialInvoiceMng_function_SearchInitFobInvoice(orderBy, orderDirection, ClientUD, ClientNM).Count();
                    var result = context.ECommercialInvoiceMng_function_SearchInitFobInvoice(orderBy, orderDirection, ClientUD, ClientNM);

                    IEnumerable<DTO.ECommercialInvoiceMng.InitFobInvoice> data = converter.DB2DTO_InitFobInvoice(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ECommercialInvoiceMng.InitFobInvoice>();
            }
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo> GetInitWarehouseInvoice(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ClientUD = string.Empty;
            string ClientNM = string.Empty;

            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();

            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.ECommercialInvoiceMng_function_SearchInitWarehouseInvoice(orderBy, orderDirection, ClientUD, ClientNM).Count();
                    var result = context.ECommercialInvoiceMng_function_SearchInitWarehouseInvoice(orderBy, orderDirection, ClientUD, ClientNM);

                    IEnumerable<DTO.ECommercialInvoiceMng.InitWarehouseInfo> data = converter.DB2DTO_InitWarehouseInfos(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ECommercialInvoiceMng.InitWarehouseInfo>();
            }
        }

        public IEnumerable<DTO.ECommercialInvoiceMng.Client> GetClient(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ClientUD = string.Empty;
            string ClientNM = string.Empty;

            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();

            //try to get data
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.ECommercialInvoiceMng_function_SearchClient(orderBy, orderDirection, ClientUD, ClientNM).Count();
                    var result = context.ECommercialInvoiceMng_function_SearchClient(orderBy, orderDirection, ClientUD, ClientNM);

                    IEnumerable<DTO.ECommercialInvoiceMng.Client> data = converter.DB2DTO_Client(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new List<DTO.ECommercialInvoiceMng.Client>();
            }
        }

        public string GetReportExportExcelInvoice(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ECommercialInvoiceMng_function_GetReportExportExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);

                adap.TableMappings.Add("Table", "ECommercialInvoiceMng_ReportExportExcel_View");
                adap.TableMappings.Add("Table1", "ECommercialInvoiceMng_SubInfo_View");
                adap.Fill(ds);

                foreach (var item in ds.ECommercialInvoiceMng_ReportExportExcel_View)
                {
                    item.FactoryInvoiceNo = "";
                    item.BLNo = "";
                    item.ProformaInvoiceNo = "";
                    item.PurchasingInvoiceNo = "";
                    foreach (var dbItem in ds.ECommercialInvoiceMng_SubInfo_View.Where(o => o.ECommercialInvoiceID == item.ECommercialInvoiceID))
                    {
                        if (!dbItem.IsFactoryInvoiceNoNull() && !item.FactoryInvoiceNo.Contains(dbItem.FactoryInvoiceNo)) item.FactoryInvoiceNo = item.FactoryInvoiceNo + dbItem.FactoryInvoiceNo + ", ";
                        if (!dbItem.IsBLNoNull() && !item.BLNo.Contains(dbItem.BLNo)) item.BLNo = item.BLNo + dbItem.BLNo + ", ";
                        if (!dbItem.IsProformaInvoiceNoNull() && !item.ProformaInvoiceNo.Contains(dbItem.ProformaInvoiceNo)) item.ProformaInvoiceNo = item.ProformaInvoiceNo + dbItem.ProformaInvoiceNo + ", ";
                        if (!dbItem.IsPurchasingInvoiceNoNull() && !item.PurchasingInvoiceNo.Contains(dbItem.PurchasingInvoiceNo)) item.PurchasingInvoiceNo = item.PurchasingInvoiceNo + dbItem.PurchasingInvoiceNo + ", ";
                    }
                    if (item.FactoryInvoiceNo.Length >= 2) item.FactoryInvoiceNo = item.FactoryInvoiceNo.Substring(0, item.FactoryInvoiceNo.Length - 2);
                    if (item.BLNo.Length >= 2) item.BLNo = item.BLNo.Substring(0, item.BLNo.Length - 2);
                    if (item.ProformaInvoiceNo.Length >= 2) item.ProformaInvoiceNo = item.ProformaInvoiceNo.Substring(0, item.ProformaInvoiceNo.Length - 2);
                    if (item.PurchasingInvoiceNo.Length >= 2) item.PurchasingInvoiceNo = item.PurchasingInvoiceNo.Substring(0, item.PurchasingInvoiceNo.Length - 2);
                }

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ExportECommercialInvoice");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "ExportECommercialInvoice");
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

        public bool SetDonePayment(int eCommercialInvoiceID, bool isDonePayment, out DTO.ECommercialInvoiceMng.ECommercialInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update Success" };
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    ECommercialInvoice dbItem = context.ECommercialInvoice.FirstOrDefault(o => o.ECommercialInvoiceID == eCommercialInvoiceID);
                    if (dbItem == null)
                    {
                        throw new Exception("invoice not found!");
                    }
                    dbItem.IsDonePayment = isDonePayment;
                    context.SaveChanges();
                    dtoItem = GetData(eCommercialInvoiceID, out notification);

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                dtoItem = new DTO.ECommercialInvoiceMng.ECommercialInvoice();
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }

        public string ExportExactOnlineSoftware(string ecommercialInvoiceIds, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                if (string.IsNullOrEmpty(ecommercialInvoiceIds))
                {
                    throw new Exception("You need select invoice before export");
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ECommercialInvoiceMng_function_GetExportExactOnline", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ECommercialInvoiceIds", ecommercialInvoiceIds);

                adap.TableMappings.Add("Table", "ECommercialInvoiceMng_ExportExactOnline_View");
                adap.TableMappings.Add("Table1", "ECommercialInvoiceMng_SubInfo_View");
                adap.Fill(ds);

                foreach (var item in ds.ECommercialInvoiceMng_ExportExactOnline_View)
                {
                    item.BLNo = "";
                    string ProformaInvoiceNo = "";
                    foreach (var dbItem in ds.ECommercialInvoiceMng_SubInfo_View.Where(o => o.ECommercialInvoiceID == item.ECommercialInvoiceID))
                    {
                        if (!dbItem.IsBLNoNull() && !item.BLNo.Contains(dbItem.BLNo)) item.BLNo = item.BLNo + dbItem.BLNo + " / ";
                        if (!dbItem.IsProformaInvoiceNoNull() && !ProformaInvoiceNo.Contains(dbItem.ProformaInvoiceNo)) ProformaInvoiceNo = ProformaInvoiceNo + dbItem.ProformaInvoiceNo + " / ";
                    }
                    if (item.BLNo.Length >= 2) item.BLNo = item.BLNo.Substring(0, item.BLNo.Length - 2);
                    if (ProformaInvoiceNo.Length >= 2)
                    {
                        ProformaInvoiceNo = ProformaInvoiceNo.Substring(0, ProformaInvoiceNo.Length - 2);
                        item.DefinitionHeader = item.DefinitionHeader + " - " + ProformaInvoiceNo;
                        item.Definition = item.Definition + " - " + ProformaInvoiceNo;
                    }
                }

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ExportExactOnline");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "ExportExactOnline");
                //return Library.Helper.CreateCOMReportFile(ds, "ExportExactOnline");
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

        public DTO.ECommercialInvoiceMng.SearchFormData SearchECommercialInvoice(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ECommercialInvoiceMng.SearchFormData searchFormData = new DTO.ECommercialInvoiceMng.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string InvoiceNo = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            string BLNo = string.Empty;
            string OrderNo = string.Empty;
            string ClientInvoiceNo = string.Empty;
            int? TypeOfInvoice = 0;
            string ContainerNo = string.Empty;
            string CMRNo = string.Empty;
            string ETA = string.Empty;
            string ETA2 = string.Empty;
            string ETD = string.Empty;
            string Season = string.Empty;
            int? InternalCompanyID = 0;
            
            //try to get data
            try
            {
                if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
                if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
                if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
                if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
                if (filters.ContainsKey("OrderNo")) OrderNo = filters["OrderNo"].ToString();
                if (filters.ContainsKey("ClientInvoiceNo")) ClientInvoiceNo = filters["ClientInvoiceNo"].ToString();
                if (filters.ContainsKey("TypeOfInvoice")) TypeOfInvoice = Convert.ToInt32(filters["TypeOfInvoice"]);
                if (filters.ContainsKey("ContainerNo")) ContainerNo = filters["ContainerNo"].ToString();
                if (filters.ContainsKey("CMRNo")) CMRNo = filters["CMRNo"].ToString();
                if (filters.ContainsKey("ETA")) ETA = filters["ETA"].ToString();
                if (filters.ContainsKey("ETA2")) ETA2 = filters["ETA2"].ToString();
                if (filters.ContainsKey("ETD")) ETD = filters["ETD"].ToString();
                if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
                if (filters.ContainsKey("InternalCompanyID")) InternalCompanyID = Convert.ToInt32(filters["InternalCompanyID"]);

                using (ECommercialInvoiceMngEntities context = CreateContext())
                {                    
                    var result = context.ECommercialInvoiceMng_function_SearchECommercialInvoice(orderBy, orderDirection, InvoiceNo, ClientUD, ClientNM, BLNo, OrderNo, ClientInvoiceNo, TypeOfInvoice, ContainerNo, CMRNo, ETA, ETA2, ETD, Season, InternalCompanyID);
                    searchFormData.Data = converter.DB2DTO_ECommercialInvoiceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    var x = context.ECommercialInvoiceMng_function_SearchECommercialInvoice(orderBy, orderDirection, InvoiceNo, ClientUD, ClientNM, BLNo, OrderNo, ClientInvoiceNo, TypeOfInvoice, ContainerNo, CMRNo, ETA, ETA2, ETD, Season, InternalCompanyID).ToList();
                    totalRows = x.Count();
                    searchFormData.SumNetAmountEUR = x.Sum(o => (o.NetAmountEUR.HasValue ? o.NetAmountEUR : 0));
                    searchFormData.SumVATAmountEUR = x.Sum(o => (o.VATAmountEUR.HasValue ? o.VATAmountEUR : 0));
                    searchFormData.SumTotalAmountEUR = x.Sum(o => (o.TotalAmountEUR.HasValue ? o.TotalAmountEUR : 0));

                    //assign sub info
                    List<int> ecommericalInvoiceIds = searchFormData.Data.Select(o => o.ECommercialInvoiceID).ToList();
                    var subInfos = context.ECommercialInvoiceMng_SubInfo_View.Where(o => ecommericalInvoiceIds.Contains(o.ECommercialInvoiceID.Value)).ToList();
                    foreach (var item in searchFormData.Data)
                    {
                        item.FactoryInvoiceNo = "";
                        item.BLNo = "";
                        item.ProformaInvoiceNo = "";
                        item.PurchasingInvoiceNo = "";
                        foreach (var subInfo in subInfos.Where(o => o.ECommercialInvoiceID == item.ECommercialInvoiceID))
                        {
                            if (subInfo.FactoryInvoiceNo != null && !item.FactoryInvoiceNo.Contains(subInfo.FactoryInvoiceNo)) item.FactoryInvoiceNo = item.FactoryInvoiceNo + subInfo.FactoryInvoiceNo + ", ";
                            if (subInfo.BLNo != null && !item.BLNo.Contains(subInfo.BLNo)) item.BLNo = item.BLNo + subInfo.BLNo + ", ";
                            if (subInfo.ProformaInvoiceNo != null && !item.ProformaInvoiceNo.Contains(subInfo.ProformaInvoiceNo)) item.ProformaInvoiceNo = item.ProformaInvoiceNo + subInfo.ProformaInvoiceNo + ", ";
                            if (subInfo.PurchasingInvoiceNo != null && !item.PurchasingInvoiceNo.Contains(subInfo.PurchasingInvoiceNo)) item.PurchasingInvoiceNo = item.PurchasingInvoiceNo + subInfo.PurchasingInvoiceNo + ", ";
                        }
                        if (item.FactoryInvoiceNo.Length >= 2) item.FactoryInvoiceNo = item.FactoryInvoiceNo.Substring(0, item.FactoryInvoiceNo.Length - 2);
                        if (item.BLNo.Length >= 2) item.BLNo = item.BLNo.Substring(0, item.BLNo.Length - 2);
                        if (item.ProformaInvoiceNo.Length >= 2) item.ProformaInvoiceNo = item.ProformaInvoiceNo.Substring(0, item.ProformaInvoiceNo.Length - 2);
                        if (item.PurchasingInvoiceNo.Length >= 2) item.PurchasingInvoiceNo = item.PurchasingInvoiceNo.Substring(0, item.PurchasingInvoiceNo.Length - 2);
                    }

                    searchFormData.Seasons = new DAL.Support.DataFactory().GetSeason().ToList();
                    searchFormData.InvoiceTypes = GetInvoiceType().ToList();
                    searchFormData.InternalCompany3s = new DAL.Support.DataFactory().GetInternalCompany3().ToList();

                    return searchFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.SearchFormData();
            }
        }

        public DTO.ECommercialInvoiceMng.InitData GetInitData()
        {
            DTO.ECommercialInvoiceMng.InitData data = new DTO.ECommercialInvoiceMng.InitData();
            data.InvoiceTypes = this.GetInvoiceType().ToList();
            data.InternalCompany3s = new DAL.Support.DataFactory().GetInternalCompany3().ToList();
            return data;
        }

        public string GetInvoicePrintoutInExcel(int eCommercialInvoiceID, string reportName, int invoiceType, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            InvoicePrintoutInExcelDataObject ds = new InvoicePrintoutInExcelDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ECommercialInvoiceMng_function_GetInvoicePrintoutInExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ECommercialInvoiceID", eCommercialInvoiceID);

                adap.TableMappings.Add("Table", "ECommercialInvoice");
                adap.TableMappings.Add("Table1", "Booking");
                adap.TableMappings.Add("Table2", "Container");
                adap.TableMappings.Add("Table3", "Goods");
                adap.TableMappings.Add("Table4", "GoodsDescription");
                adap.TableMappings.Add("Table5", "BottomDescription");
                adap.TableMappings.Add("Table6", "ContainerTransport");
                adap.TableMappings.Add("Table7", "ExtDetail");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, reportName);

                //if (invoiceType == DALBase.Helper.CONTAINER_TRANSPORT)
                //{
                //    return Library.Helper.CreateReportFileWithEPPlus2(ds, reportName + "_Transport");
                //}
                //else
                //{
                //    return Library.Helper.CreateReportFileWithEPPlus2(ds, reportName);
                //}

            }
            //catch (ConstraintException cEx)
            //{
            //    DataRow[] rowErrors = this.YourDataSet.YourDataTable.GetErrors();

            //    System.Diagnostics.Debug.WriteLine("YourDataTable Errors:"
            //        + rowErrors.Length);

            //    for (int i = 0; i < rowErrors.Length; i++)
            //    {
            //        System.Diagnostics.Debug.WriteLine(rowErrors[i].RowError);

            //        foreach (DataColumn col in rowErrors[i].GetColumnsInError())
            //        {
            //            System.Diagnostics.Debug.WriteLine(col.ColumnName
            //                + ":" + rowErrors[i].GetColumnError(col));
            //        }
            //    }
            //}
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

        public string GetPEUackingListPrintoutData(int eCommercialInvoiceID, string reportName, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PackingListPrintoutInExcelDataObject ds = new PackingListPrintoutInExcelDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ECommercialInvoiceMng_function_GetPackingListPrintoutInExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ECommercialInvoiceID", eCommercialInvoiceID);

                adap.TableMappings.Add("Table", "ECommercialInvoice_ReportView");
                adap.TableMappings.Add("Table1", "PackingListDetail_ReportView");
                adap.TableMappings.Add("Table2", "ECommercialInvoiceDetailDescription_ReportView");
                adap.TableMappings.Add("Table3", "PackingListDetailExtend_ReportView");
                adap.TableMappings.Add("Table4", "ECommercialInvoiceDescription_ReportView");
                adap.TableMappings.Add("Table5", "Container_ReportView");
                adap.TableMappings.Add("Table6", "PackingListSparepartDetail_ReportView");
                adap.TableMappings.Add("Table7", "PackingListSampleDetail_ReportView");
                adap.Fill(ds);

                PackingListPrintoutInExcelDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                PackingListPrintoutInExcelDataObject.ECommercialInvoice_ReportViewRow drOrigin = ds.ECommercialInvoice_ReportView.FirstOrDefault();
                PackingListPrintoutInExcelDataObject.PackingListDetail_ReportViewDataTable dtPackingListDetail = ds.PackingListDetail_ReportView;
                PackingListPrintoutInExcelDataObject.PackingListDetailExtend_ReportViewDataTable dtPackingListDetailExtend = ds.PackingListDetailExtend_ReportView;
                PackingListPrintoutInExcelDataObject.PackingListSparepartDetail_ReportViewDataTable dtPackingListDetailSparepart = ds.PackingListSparepartDetail_ReportView;
                PackingListPrintoutInExcelDataObject.PackingListSampleDetail_ReportViewDataTable dtPackingListDetailSample = ds.PackingListSampleDetail_ReportView;
                drPackingList.ClientNM = drOrigin.IsClientNMNull() ? "" : drOrigin.ClientNM;
                drPackingList.Address = drOrigin.IsAddressNull() ? "" : drOrigin.Address;
                drPackingList.InvoiceDate = drOrigin.IsInvoiceDateNull() ? "" : drOrigin.InvoiceDate;
                drPackingList.OrderNo = drOrigin.IsOrderNoNull() ? "" : drOrigin.OrderNo;
                drPackingList.BLNo = drOrigin.IsBLNoNull() ? "" : drOrigin.BLNo;
                drPackingList.Reference = drOrigin.IsReferenceNull() ? "" : drOrigin.Reference;
                drPackingList.DeliveryTermNM = drOrigin.IsDeliveryTermNMNull() ? "" : drOrigin.DeliveryTermNM;
                drPackingList.TotalCBMs = dtPackingListDetail.Sum(o => o.CBMs) + dtPackingListDetailExtend.Sum(o => o.CBMs) + dtPackingListDetailSparepart.Sum(o => o.CBMs) + dtPackingListDetailSample.Sum(o=>o.CBMs);
                drPackingList.TotalKGs = dtPackingListDetail.Sum(o => o.KGs) + dtPackingListDetailExtend.Sum(o => o.KGs) + dtPackingListDetailSparepart.Sum(o => o.KGs) + dtPackingListDetailSample.Sum(o => o.KGs);
                drPackingList.TotalNettWeight = dtPackingListDetail.Sum(o => o.NettWeight) + dtPackingListDetailExtend.Sum(o => o.NettWeight) + dtPackingListDetailSparepart.Sum(o => o.NettWeight) + dtPackingListDetailSample.Sum(o => o.NettWeight);
                drPackingList.TotalPKGs = dtPackingListDetail.Sum(o => o.PKGs) + dtPackingListDetailExtend.Sum(o => o.PKGs) + dtPackingListDetailSparepart.Sum(o => o.PKGs) + dtPackingListDetailSample.Sum(o => o.PKGs);
                drPackingList.TotalQuantityShipped = dtPackingListDetail.Sum(o => o.QuantityShipped) + dtPackingListDetailExtend.Sum(o => o.QuantityShipped) + dtPackingListDetailSparepart.Sum(o => o.QuantityShipped) + dtPackingListDetailSample.Sum(o=>o.QuantityShipped);
                ds.PackingList.AddPackingListRow(drPackingList);
                //parse detail data
                PackingListPrintoutInExcelDataObject.PackingListDetailRow drPackingListDetail;

                //CREATE TOP DESCRIPTION
                foreach (var dbDescription in ds.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "TOP").OrderBy(o => o.RowIndex))
                {
                    drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = dbDescription.IsDescriptionNull() ? "" : dbDescription.Description;
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                }
                //Create BL Row
                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                drPackingListDetail.Description = "BILL OF LADING NO.:" + drPackingList.BLNo;
                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                //Create Blank Row
                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                drPackingListDetail.Description = "";
                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                foreach (var dbCont in ds.Container_ReportView)
                {
                    drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = dbCont.IsContInfoNull() ? "" : dbCont.ContInfo;
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                    foreach (var dbPackingListDetail in ds.PackingListDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                    {
                        //CREATE PRODUCT ROW
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.ArticleCode = dbPackingListDetail.IsArticleCodeNull() ? "" : dbPackingListDetail.ArticleCode;
                        drPackingListDetail.Description = dbPackingListDetail.IsDescriptionNull() ? "" : dbPackingListDetail.Description;
                        drPackingListDetail.Unit = dbPackingListDetail.IsUnitNull() ? "" : dbPackingListDetail.Unit;
                        drPackingListDetail.QuantityShipped = dbPackingListDetail.IsQuantityShippedNull() ? 0 : dbPackingListDetail.QuantityShipped;
                        drPackingListDetail.PKGs = dbPackingListDetail.IsPKGsNull() ? 0 : dbPackingListDetail.PKGs;
                        drPackingListDetail.NettWeight = dbPackingListDetail.IsNettWeightNull() ? 0 : dbPackingListDetail.NettWeight;
                        drPackingListDetail.KGs = dbPackingListDetail.IsKGsNull() ? 0 : dbPackingListDetail.KGs;
                        drPackingListDetail.CBMs = dbPackingListDetail.IsCBMsNull() ? 0 : dbPackingListDetail.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        //CREATE ITEM DESCRIPTION ROW
                        foreach (var dbDetailDescription in ds.ECommercialInvoiceDetailDescription_ReportView.Where(o=>o.ECommercialInvoiceDetailID == dbPackingListDetail.ECommercialInvoiceDetailID).OrderBy(o => o.RowIndex))
                        {
                            drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                            drPackingListDetail.Description = dbDetailDescription.IsDescriptionNull() ? "" : dbDetailDescription.Description;
                            ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        }
                    }

                    foreach(var dbSparepart in ds.PackingListSparepartDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                    {
                        //CREATE SPAREPART ROW
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.ArticleCode = dbSparepart.IsArticleCodeNull() ? "" : dbSparepart.ArticleCode;
                        drPackingListDetail.Description = dbSparepart.IsDescriptionNull() ? "" : dbSparepart.Description;
                        drPackingListDetail.Unit = dbSparepart.IsUnitNull() ? "" : dbSparepart.Unit;
                        drPackingListDetail.QuantityShipped = dbSparepart.IsQuantityShippedNull() ? 0 : dbSparepart.QuantityShipped;
                        drPackingListDetail.PKGs = dbSparepart.IsPKGsNull() ? 0 : dbSparepart.PKGs;
                        drPackingListDetail.NettWeight = dbSparepart.IsNettWeightNull() ? 0 : dbSparepart.NettWeight;
                        drPackingListDetail.KGs = dbSparepart.IsKGsNull() ? 0 : dbSparepart.KGs;
                        drPackingListDetail.CBMs = dbSparepart.IsCBMsNull() ? 0 : dbSparepart.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }

                    foreach(var dbSample in ds.PackingListSampleDetail_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                    {
                        //CREATE SAMPLE ROW
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.ArticleCode = dbSample.IsArticleCodeNull() ? "" : dbSample.ArticleCode;
                        drPackingListDetail.Description = dbSample.IsDescriptionNull() ? "" : dbSample.Description;
                        drPackingListDetail.Unit = dbSample.IsUnitNull() ? "" : dbSample.Unit;
                        drPackingListDetail.QuantityShipped = dbSample.IsQuantityShippedNull() ? 0 : dbSample.QuantityShipped;
                        drPackingListDetail.PKGs = dbSample.IsPKGsNull() ? 0 : dbSample.PKGs;
                        drPackingListDetail.NettWeight = dbSample.IsNettWeightNull() ? 0 : dbSample.NettWeight;
                        drPackingListDetail.KGs = dbSample.IsKGsNull() ? 0 : dbSample.KGs;
                        drPackingListDetail.CBMs = dbSample.IsCBMsNull() ? 0 : dbSample.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }

                    foreach (var dbPackingListDetailExt in ds.PackingListDetailExtend_ReportView.Where(o => o.ContainerNo == dbCont.ContainerNo && o.ContainerType == dbCont.ContainerType && o.SealNo == dbCont.SealNo))
                    {
                        //CREATE ITEM ROW
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.ArticleCode = dbPackingListDetailExt.IsArticleCodeNull() ? "" : dbPackingListDetailExt.ArticleCode;
                        drPackingListDetail.Description = dbPackingListDetailExt.IsDescriptionNull() ? "" : dbPackingListDetailExt.Description;
                        drPackingListDetail.Unit = dbPackingListDetailExt.IsUnitNull() ? "" : dbPackingListDetailExt.Unit;
                        drPackingListDetail.QuantityShipped = dbPackingListDetailExt.IsQuantityShippedNull() ? 0 : dbPackingListDetailExt.QuantityShipped;
                        drPackingListDetail.PKGs = dbPackingListDetailExt.IsPKGsNull() ? 0 : dbPackingListDetailExt.PKGs;
                        drPackingListDetail.NettWeight = dbPackingListDetailExt.IsNettWeightNull() ? 0 : dbPackingListDetailExt.NettWeight;
                        drPackingListDetail.KGs = dbPackingListDetailExt.IsKGsNull() ? 0 : dbPackingListDetailExt.KGs;
                        drPackingListDetail.CBMs = dbPackingListDetailExt.IsCBMsNull() ? 0 : dbPackingListDetailExt.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    }
                }
                //Create Blank Row
                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                drPackingListDetail.Description = "";
                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                //CREATE BOTTOM DESCRIPTION
                foreach (var dbDescription in ds.ECommercialInvoiceDescription_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o => o.RowIndex))
                {
                    drPackingListDetail = drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = dbDescription.Description;
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                }

                // generate xml file
                string reportFile = Library.Helper.CreateReportFiles(ds, "ECommercialInvoice_PackingList_EU");
                return reportFile = reportFile + ".xlsm";
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public string GetEUPackingListPrintoutData_PerCont(int eCommercialInvoiceID, string reportName, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            PackingListPrintoutInExcelDataObject ds = new PackingListPrintoutInExcelDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ECommercialInvoiceMng_function_GetPackingListPrintoutInExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ECommercialInvoiceID", eCommercialInvoiceID);

                adap.TableMappings.Add("Table", "ECommercialInvoice_ReportView");
                adap.TableMappings.Add("Table1", "PackingListDetail_ReportView");
                adap.TableMappings.Add("Table2", "ECommercialInvoiceDetailDescription_ReportView");
                adap.TableMappings.Add("Table3", "PackingListDetailExtend_ReportView");
                adap.TableMappings.Add("Table4", "ECommercialInvoiceDescription_ReportView");
                adap.TableMappings.Add("Table5", "Container_ReportView");
                adap.TableMappings.Add("Table6", "PackingListSparepartDetail_ReportView");
                adap.TableMappings.Add("Table7", "PackingListSampleDetail_ReportView");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ECommercialInvoice_PackingList_EU_PerCont");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public List<DTO.ECommercialInvoiceMng.BillTransport> GetBillTransport(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    List<DTO.ECommercialInvoiceMng.BillTransport> bookings = new List<DTO.ECommercialInvoiceMng.BillTransport>();
                    string searchQuery = filters["searchQuery"].ToString();
                    int? clientID = Convert.ToInt32(filters["clientID"]);
                    var x = context.ECommercialInvoiceMng_BillTransport_View.Include("ECommercialInvoiceMng_ContainerTransport_View").Where(o => o.ClientID == clientID && o.BLNo.Contains(searchQuery)).ToList();
                    bookings = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_BillTransport_View>, List<DTO.ECommercialInvoiceMng.BillTransport>>(x);
                    foreach (var item in bookings)
                    {
                        List<ECommercialInvoiceMng_ClientOfferCostItem_View> clientOffer = context.ECommercialInvoiceMng_function_GetClientOfferCostItem(item.ClientID, item.BookingID).ToList();
                        item.ClientOfferCostItems = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ClientOfferCostItem_View>, List<DTO.ECommercialInvoiceMng.ClientOfferCostItem>>(clientOffer);
                    }
                    return bookings;
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
                return new List<DTO.ECommercialInvoiceMng.BillTransport>();
            }
        }

        public DTO.ECommercialInvoiceMng.DataOverview GetDataOverview(int id, int? typeOfInvoice, int? internalCompanyID, int? clientID, int? parentID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ECommercialInvoiceMngEntities context = CreateContext())
                {
                    DTO.ECommercialInvoiceMng.DataOverview dtoItem = new DTO.ECommercialInvoiceMng.DataOverview();

                    if (id > 0)
                    {
                        ECommercialInvoiceMng_ECommercialInvoice_OverView dbItem;
                        dbItem = context.ECommercialInvoiceMng_ECommercialInvoice_OverView
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView.ECommercialInvoiceMng_ECommercialInvoiceDetailDescription_OverView")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView")
                                .Include("ECommercialInvoiceMng_Booking_OverView")
                                .Include("ECommercialInvoiceMng_CreditNote_OverView")
                                .Include("ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView")
                                .Include("ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView")
                                .Include("ECommercialInvoiceMng_WarehouseImport_OverView")
                                .Include("ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView")
                                .FirstOrDefault(o => o.ECommercialInvoiceID == id);
                        DTO.ECommercialInvoiceMng.ECommercialInvoiceOverview ECommercialInvoiceDTOItem = converter.DB2DTO_ECommercialInvoiceOverview(dbItem);
                        dtoItem.ECommercialInvoiceData = ECommercialInvoiceDTOItem;
                    }

                    // get support data
                    //dtoItem.DeliveryTerms = converter.DB2DTO_DeliveryTerm(context.ECommercialInvoiceMng_DeliveryTerm.ToList());
                    //dtoItem.PaymentTerms = converter.DB2DTO_PaymentTerm(context.ECommercialInvoiceMng_PaymentTerm.ToList());
                    //dtoItem.TurnOverLedgerAccounts = converter.DB2DTO_TurnOverLedgerAccount(context.ECommercialInvoiceMng_TurnOverLedgerAccount_View.ToList());
                    //dtoItem.VATPercents = new DAL.Support.DataFactory().GetVATPercent();
                    //dtoItem.Currency = new DAL.Support.DataFactory().GetCurrency().ToList();
                    //dtoItem.CostTypes = GetCostTypes().ToList();
                    //dtoItem.Seasons = new DAL.Support.DataFactory().GetSeason().ToList();
                    dtoItem.ReportTemplates = new DAL.Support.DataFactory().GetReportTemplate().ToList();
                    dtoItem.ClientCostItems = converter.DB2DTO_ClientCostItem(context.ECommercialInvoiceMng_ClientCostItem_View.ToList());
                    if (dtoItem.ECommercialInvoiceData.BookingID.HasValue)
                    {
                        //get list container
                        var containerItems = context.ECommercialInvoiceMng_ContainerTransport_View.Where(o => o.BookingID == dtoItem.ECommercialInvoiceData.BookingID);
                        dtoItem.ContainerTransports = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ContainerTransport_View>, List<DTO.ECommercialInvoiceMng.ContainerTransport>>(containerItems.ToList());

                        //get list client offer cost
                        List<ECommercialInvoiceMng_ClientOfferCostItem_View> clientOffer = context.ECommercialInvoiceMng_function_GetClientOfferCostItem(dtoItem.ECommercialInvoiceData.ClientID, dtoItem.ECommercialInvoiceData.BookingID).ToList();
                        dtoItem.ClientOfferCostItems = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ClientOfferCostItem_View>, List<DTO.ECommercialInvoiceMng.ClientOfferCostItem>>(clientOffer);
                    }

                    // Get defautl report template
                    if (dtoItem.ECommercialInvoiceData.DefaultCiReport == null)
                    {
                        var defaultRpt = dtoItem.ReportTemplates.FirstOrDefault(x => x.ReportType == "CI" && x.IsDefault == true).ReportTemplateNM;
                        dtoItem.ECommercialInvoiceData.DefaultCiReport = defaultRpt;
                    }

                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ECommercialInvoiceMng.DataOverview();
            }
        }

        private void SendNotification(string emailSubject, string emailBody, string sendToEmail)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (ECommercialInvoiceMngEntities context = CreateContext())
                {

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

        private string HeaderTableSendMail(int? totalQnt, string totalPurchasingPrice, string totalPurchasingAmount, string totalUnitPrice, string totalSaleAmount, string totalGrossMargin, string totalGrossMarginPercent)
        {
            string bodyHasValue = "";
            bodyHasValue = "<table border='1' style='width: 2500px'>";
            bodyHasValue += "<thead>";
            bodyHasValue += "<tr style='background-color:yellow'>";
            bodyHasValue += "<th style='width: 70px'>COMMERCIAL SALE INVOICE NR.</th>";
            bodyHasValue += "<th style='width: 70px'>COMMERCIAL SALE INVOICE DATE</th>";
            bodyHasValue += "<th style='width: 70px'>SALE INVOICE CURRENCY</th>";
            bodyHasValue += "<th style='width: 100px'>CUSTOMER CODE</th>";
            bodyHasValue += "<th style='width: 120px'>CUSTOMER NAME</th>";
            bodyHasValue += "<th style='width: 150px'>ART. CODE</th>";
            bodyHasValue += "<th style='width: 400px'>DESCRIPTION</th>";
            bodyHasValue += "<th style='width: 60px'>QUANTITY</th>";
            bodyHasValue += "<th style='width: 100px'>STANDARD COST PRICE PER UNIT</th>";
            bodyHasValue += "<th style='width: 100px'>STANDARD COST PRICE AMOUNT</th>";
            bodyHasValue += "<th style='width: 100px'>SALE PRICE PER UNIT</th>";
            bodyHasValue += "<th style='width: 100px'>SALE PRICE AMOUNT</th>";
            bodyHasValue += "<th style='width: 100px'>GROSS MARGIN AMOUNT</th>";
            bodyHasValue += "<th style='width: 100px'>GROSS MARGIN %</th>";
            bodyHasValue += "</tr>";
            bodyHasValue += "<tr  style='background-color:cyan'>";
            bodyHasValue += "<th colspan='7' style='text-align: right; width: 980px'>Sub Total</th>";
            bodyHasValue += "<th style='text-align: right; width: 60px'>" + totalQnt + "</th>";
            bodyHasValue += "<th style='text-align: right; width: 100px'>" + totalPurchasingPrice + "</th>";
            bodyHasValue += "<th style='text-align: right; width: 100px'>" + totalPurchasingAmount + "</th>";
            bodyHasValue += "<th style='text-align: right; width: 100px'>" + totalUnitPrice + "</th>";
            bodyHasValue += "<th style='text-align: right; width: 100px'>" + totalSaleAmount + "</th>";
            bodyHasValue += "<th style='text-align: right; width: 100px'>" + totalGrossMargin + "</th>";
            bodyHasValue += "<th style='text-align: right; width: 100px'>" + totalGrossMarginPercent + "</th>";
            bodyHasValue += "</tr>";
            bodyHasValue += "</thead>";
            return bodyHasValue;
        }

        private string BodyTableSendMail(DTO.ECommercialInvoiceMng.WarehouseInvoice list, decimal? GrossMarginPercent)
        {
            string bodyHasValue = "";
            bodyHasValue += "<tbody>";
            bodyHasValue += "<tr>";
            bodyHasValue = bodyHasValue + "<td style='text-align: center; width: 70px'>" + list.InvoiceNo + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: center; width: 70px'>" + list.ECInvoiceDate + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: center; width: 70px'>" + list.Currency + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: center; width: 70px'>" + list.ClientUD + "</td>";
            bodyHasValue = bodyHasValue + "<td style='width: 120px'>" + list.ClientNM + "</td>";
            bodyHasValue = bodyHasValue + "<td style='width: 150px'>" + list.ArticleCode + "</td>";
            bodyHasValue = bodyHasValue + "<td style='width: 400px'>" + list.Description + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 60px'>" + list.Quantity + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 100px'>" + String.Format("{0:#,##0.00}", list.PurchasingPrice ?? 0) + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 100px'>" + String.Format("{0:#,##0.00}", list.PurchasingAmount ?? 0) + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 100px'>" + String.Format("{0:#,##0.00}", list.UnitPrice ?? 0) + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 100px'>" + String.Format("{0:#,##0.00}", list.SaleAmount ?? 0) + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 100px'>" + String.Format("{0:#,##0.00}", list.GrossMargin ?? 0) + "</td>";
            bodyHasValue = bodyHasValue + "<td style='text-align: right; width: 100px'>" + String.Format("{0:#,##0.00}", GrossMarginPercent ?? 0) + "%" + "</td>";
            bodyHasValue += "</tr>";
            bodyHasValue += "</tbody>";
            return bodyHasValue;
        }

        public string GetOrangePiePrintout(int ecommercialInvoiceID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OrangePiePrintoutDataObject ds = new OrangePiePrintoutDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ECommercialInvoiceMng_function_GetOrangePiePrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ECommercialInvoiceID", ecommercialInvoiceID);

                adap.TableMappings.Add("Table", "OrangePie_PackingList");
                adap.TableMappings.Add("Table1", "OrangePie_Container");
                adap.TableMappings.Add("Table2", "OrangePie_Goods");
                adap.TableMappings.Add("Table3", "OrangePie_GoodsDescription");
                adap.Fill(ds);

                //if (ds.OrangePie_PackingList.Count == 0)
                //{
                //    notification.Type = Library.DTO.NotificationType.Error;
                //    notification.Message = "There isn't any PackingList";                  
                //    return string.Empty;
                //}

                string ClientOrderNos = "";
                string CustomerOrderNos = "";
                foreach (var item in ds.OrangePie_Goods)
                {
                    if (!string.IsNullOrEmpty(item.ProformaInvoiceNo) && !ClientOrderNos.Contains(item.ProformaInvoiceNo))
                    {
                        ClientOrderNos += item.ProformaInvoiceNo + ", ";
                    }
                    if (!string.IsNullOrEmpty(item.ClientOrderNumber) && !CustomerOrderNos.Contains(item.ClientOrderNumber))
                    {
                        CustomerOrderNos += item.ClientOrderNumber + ", ";
                    }
                }
                OrangePiePrintoutDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                OrangePiePrintoutDataObject.OrangePie_PackingListRow drOrigin = ds.OrangePie_PackingList.FirstOrDefault();
                drPackingList.SupplierNM = drOrigin.IsSupplierNMNull() ? "" : drOrigin.SupplierNM;
                drPackingList.Address = drOrigin.IsAddressNull() ? "" : drOrigin.Address;
                drPackingList.ConsigneeInfo = drOrigin.IsConsigneeInfoNull() ? "" : drOrigin.ConsigneeInfo;
                drPackingList.NotifyParty = drOrigin.IsNotifyPartyNull() ? "" : drOrigin.NotifyParty;
                drPackingList.PackingListUD = "";
                drPackingList.PackingListDate = drOrigin.IsPackingListDateNull() ? "" : drOrigin.PackingListDate;
                drPackingList.ClientOrderNos = drOrigin.IsClientOrderNosNull() ? "" : drOrigin.ClientOrderNos;
                drPackingList.CustomerOrderNos = drOrigin.IsCustomerOrderNosNull() ? "" : drOrigin.CustomerOrderNos;
                drPackingList.ForwarderNM = drOrigin.IsForwarderNMNull() ? "" : drOrigin.ForwarderNM;
                drPackingList.ShipedDate = drOrigin.IsShipedDateNull() ? "" : drOrigin.ShipedDate;
                drPackingList.POLName = drOrigin.IsPOLNameNull() ? "" : drOrigin.POLName;
                drPackingList.PODName = drOrigin.IsPODNameNull() ? "" : drOrigin.PODName;
                drPackingList.Vessel = drOrigin.IsVesselNull() ? "" : drOrigin.Vessel;
                drPackingList.BLNo = drOrigin.IsBLNoNull() ? "" : drOrigin.BLNo;
                drPackingList.TotalQuantityShipped = drOrigin.IsTotalQuantityShippedNull() ? 0 : drOrigin.TotalQuantityShipped;
                drPackingList.TotalPKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalPKGs;
                drPackingList.TotalNettWeight = drOrigin.IsTotalNettWeightNull() ? 0 : drOrigin.TotalNettWeight;
                drPackingList.TotalKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalKGs;
                drPackingList.TotalCBMs = drOrigin.IsTotalCBMsNull() ? 0 : drOrigin.TotalCBMs;
                ds.PackingList.AddPackingListRow(drPackingList);

                //parse detail data
                OrangePiePrintoutDataObject.PackingListDetailRow drPackingListDetail;

                foreach (var container in ds.OrangePie_Container)
                {
                    drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                    drPackingListDetail.Description = "1x" + (container.IsContainerTypeNMNull() ? "" : container.ContainerTypeNM) + " CONTAINER / CONT.NO: " + (container.IsContainerNoNull() ? "" : container.ContainerNo) + " / SEAL NO: " + (container.IsSealNoNull() ? "" : container.SealNo);
                    drPackingListDetail.Flag = "x";
                    ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                    foreach (var product in ds.OrangePie_Goods.Where(o => o.ContainerNo == container.ContainerNo))
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.ArticleCode = product.IsArticleCodeNull() ? "" : product.ArticleCode;
                        drPackingListDetail.Description = product.IsDescriptionNull() ? "" : product.Description;
                        drPackingListDetail.QuantityShipped = product.IsQuantityShippedNull() ? 0 : product.QuantityShipped;
                        drPackingListDetail.PKGs = product.IsPKGsNull() ? 0 : product.PKGs;
                        drPackingListDetail.NettWeight = product.IsNettWeightNull() ? 0 : product.NettWeight;
                        drPackingListDetail.KGs = product.IsKGsNull() ? 0 : product.KGs;
                        drPackingListDetail.CBMs = product.IsCBMsNull() ? 0 : product.CBMs;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        foreach(var productDescription in ds.OrangePie_GoodsDescription.Where(o=>o.GoodsID == product.GoodsID).OrderBy(o=>o.RowIndex))
                        {
                            drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                            drPackingListDetail.Description = productDescription.IsDescriptionNull() ? "" : productDescription.Description;
                            ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        }
                    }
                }

                // generate xml file
                string reportFile = Library.Helper.CreateReportFiles(ds, "ECommercialInvoice_PackingList_OrangePine");
                return reportFile = reportFile + ".xlsm";
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        #region Get Purchasing Quantity
        public List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing> GetPurchasingQnt(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing> data = new List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing>();
            
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ECommercialInvoiceMng_ECommercialInvoicePurchasing_View.Where(o => o.ECommercialInvoiceID == id);
                    data = AutoMapper.Mapper.Map<List<ECommercialInvoiceMng_ECommercialInvoicePurchasing_View>, List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing>>(dbItem.ToList());
                }
                return data;
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
                return new List<DTO.ECommercialInvoiceMng.ECommercialInvoicePurchasing>();
            }
        }
        #endregion

        public List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> BizBloqsImportData(List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> bizBloqsInvoice, int? userId, out Library.DTO.Notification notification) {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Import Success !!!" };
            List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> data = new List<DTO.ECommercialInvoiceMng.BizBloqsInvoice>();
            List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> validInvoice = new List<DTO.ECommercialInvoiceMng.BizBloqsInvoice>();
            List<DTO.ECommercialInvoiceMng.BizBloqsInvoice> inValidInvoice = new List<DTO.ECommercialInvoiceMng.BizBloqsInvoice>();
            try
            {
                using (var context = CreateContext())
                {
                    ECommercialInvoice dbInvoice;
                    WarehouseInvoiceProductDetail dbInvoiceDetail;
                    foreach (var invoiceItem in bizBloqsInvoice.Where(o => o.IsSelected.HasValue && o.IsSelected.Value))
                    {
                        dbInvoice = new ECommercialInvoice();
                        context.ECommercialInvoice.Add(dbInvoice);
                        dbInvoice.InvoiceNo = invoiceItem.InvoiceNo;
                        dbInvoice.TypeOfInvoice = DALBase.Helper.WAREHOUSE_INVOICE;
                        dbInvoice.InvoiceDate = invoiceItem.InvoiceDate.ConvertStringToDateTime();
                        dbInvoice.Season = Library.Helper.GetCurrentSeason();
                        dbInvoice.CreatedDate = DateTime.Now;
                        dbInvoice.UpdatedDate = DateTime.Now;
                        dbInvoice.CreatedBy = userId;
                        dbInvoice.UpdatedBy = userId;
                        var client = context.Client.Where(o => o.ClientUD == invoiceItem.ClientUD).FirstOrDefault();
                        if (client == null)
                        {                            
                            inValidInvoice.Add(new DTO.ECommercialInvoiceMng.BizBloqsInvoice() { InvoiceNo = invoiceItem.InvoiceNo, Remark = "Client is not exist " + invoiceItem.ClientUD});                            
                        }
                        else {
                            dbInvoice.ClientID = client.ClientID;
                            validInvoice.Add(new DTO.ECommercialInvoiceMng.BizBloqsInvoice() { InvoiceNo = invoiceItem.InvoiceNo });
                        }
                        var saleOrder = context.SaleOrder.Where(o => o.BizBloqOrderNo == invoiceItem.OrderNo).FirstOrDefault();
                        if (saleOrder == null)
                        {
                            inValidInvoice.Add(new DTO.ECommercialInvoiceMng.BizBloqsInvoice() { InvoiceNo = invoiceItem.InvoiceNo, Remark = "OrderNo is not exist " + invoiceItem.OrderNo });
                        }
                        else {
                            dbInvoice.BizBloqOrderNo = invoiceItem.InvoiceNo;
                            validInvoice.Add(new DTO.ECommercialInvoiceMng.BizBloqsInvoice() { InvoiceNo = invoiceItem.InvoiceNo });
                        }
                        dbInvoice.PaymentTerm = invoiceItem.PaymentTerm;
                        dbInvoice.DeliveryTerm = invoiceItem.DeliveryTerm;

                        foreach (var lineItem in invoiceItem.InvoiceLines)
                        {
                            dbInvoiceDetail = new WarehouseInvoiceProductDetail();
                            dbInvoice.WarehouseInvoiceProductDetail.Add(dbInvoiceDetail);
                            var product = context.Product.Where(o => o.ArticleCode == lineItem.ArticleCode).FirstOrDefault();
                            if (product == null)
                            {
                                inValidInvoice.Add(new DTO.ECommercialInvoiceMng.BizBloqsInvoice() { InvoiceNo = invoiceItem.InvoiceNo, Remark = "Invoice has item does not exist: " + lineItem.ArticleCode});
                            }
                            else {
                                dbInvoiceDetail.ProductID = product.ProductID;
                            }                            
                            dbInvoiceDetail.Quantity = lineItem.Quantity;
                            dbInvoiceDetail.UnitPrice = lineItem.UnitPrice;
                            dbInvoice.AccountNo = Convert.ToInt32(lineItem.AccountNo);
                            dbInvoice.VATRate = lineItem.VATPercent;
                            dbInvoice.Currency = lineItem.Currency;
                        }                        
                    }
                    if (inValidInvoice.Count() > 0)
                    {
                        data = inValidInvoice;
                        throw new Exception("There are some invoice are invalid (item code in invoice line does not exist)");
                    }
                    else
                    {
                        data = validInvoice;
                        context.SaveChanges();
                    }
                }
                return data;
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
    }
}