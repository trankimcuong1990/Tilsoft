using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private Booking2MngEntities CreateContext()
        {
            return new Booking2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.Booking2MngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.BookingSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (Booking2MngEntities context = CreateContext())
                {
                    string BookingUD = null;
                    string Season = null;
                    string BLNo = null;
                    string ContainerNo = null;
                    string ClientUD = null;
                    int UserID = -1;
                    string ConfirmationNo = null;
                    string ProformaInvoiceNo = null;
                    if (filters.ContainsKey("BookingUD") && !string.IsNullOrEmpty(filters["BookingUD"].ToString()))
                    {
                        BookingUD = filters["BookingUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
                    {
                        BLNo = filters["BLNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ContainerNo") && !string.IsNullOrEmpty(filters["ContainerNo"].ToString()))
                    {
                        ContainerNo = filters["ContainerNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    if (filters.ContainsKey("ConfirmationNo") && !string.IsNullOrEmpty(filters["ConfirmationNo"].ToString()))
                    {
                        ConfirmationNo = filters["ConfirmationNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }
                    totalRows = context.Booking2Mng_function_SearchBooking(BookingUD, Season, BLNo, ContainerNo, ClientUD, UserID, ConfirmationNo, ProformaInvoiceNo, orderBy, orderDirection).Count();
                    var result = context.Booking2Mng_function_SearchBooking(BookingUD, Season, BLNo, ContainerNo, ClientUD, UserID, ConfirmationNo, ProformaInvoiceNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_BookingSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Booking dtoBooking = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Booking>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Booking2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected supplier data");
                    }
                    if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected booking data");
                    }

                    Booking dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Booking();
                        context.Booking.Add(dbItem);
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Booking not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoBooking.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2DB(dtoBooking, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");

                        //remove orphan
                        context.BookingPlanFactoryOrderDetail.Local.Where(o => o.Booking == null).ToList().ForEach(o => context.BookingPlanFactoryOrderDetail.Remove(o));
                        context.BookingPlanFactoryOrderSampleDetail.Local.Where(o => o.Booking == null).ToList().ForEach(o => context.BookingPlanFactoryOrderSampleDetail.Remove(o));
                        context.BookingPlanFactoryOrderSparepartDetail.Local.Where(o => o.Booking == null).ToList().ForEach(o => context.BookingPlanFactoryOrderSparepartDetail.Remove(o));

                        if (!dbItem.IsConfirmed.HasValue || !dbItem.IsConfirmed.Value)
                        {
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;
                        }

                        // generate booking number
                        if (id <= 0)
                        {
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM Booking WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    dbItem.BookingUD = context.Booking2Mng_function_GenerateBookingRef(dbItem.SupplierID, dbItem.Season, dbItem.ClientID).FirstOrDefault();

                                    context.SaveChanges();
                                    string emailSubject = "Booking" + dtoBooking.BookingUD + " has been created";
                                    fwFactory.SendEmailNotificationBasedOn("Booking2Mng", dbItem.BookingID, emailSubject, 2);

                                    // Get information notification with status
                                    var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == "Booking2Mng" && o.StatusID == 2).ToList();
                                    foreach (var status in groupStatuses)
                                    {
                                        // Create email body
                                        var EmailBody = FrameworkSetting.Setting.FrontendUrl + status.URLLink + "/Edit/" + dbItem.BookingID.ToString();

                                        if (!string.IsNullOrEmpty(status.EmailTemplate))
                                        {
                                            EmailBody = EmailBody + Environment.NewLine + status.EmailTemplate + dbItem.BookingID.ToString();
                                        }
                                        var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == status.NotificationGroupID);
                                        foreach (var user in dbGroupMember)
                                        {
                                            // add to NotificationMessage table
                                            NotificationMessage notification1 = new NotificationMessage();
                                            notification1.UserID = user.UserID;
                                            notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_LOGISTICS;
                                            notification1.NotificationMessageTitle = emailSubject;
                                            notification1.NotificationMessageContent = EmailBody;
                                            context.NotificationMessage.Add(notification1);
                                            
                                        }
                                    }
                                    

                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    scope.Commit();
                                }
                            }
                        }
                        else
                        {
                            context.SaveChanges();                                                                                 
                        }

                        //
                        // generate factory order caching
                        //
                        context.FactoryOrderInfoCacheMng_function_BuildCacheForBooking(dbItem.BookingID);
                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.BookingID, -1, -1, string.Empty, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Booking dtoBooking = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Booking>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                int? companyID = fwFactory.GetCompanyID(userId);
                if (companyID != 3)
                {
                    throw new Exception("Change your company to AVT !!!");
                }

                string currentSeason = Library.Helper.GetCurrentSeason();

                // check permission
                if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected booking data");
                }

                using (Booking2MngEntities context = CreateContext())
                {
                    Booking dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Booking not found!");
                    }

                    // check if already confirm
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Booking is already confirmed!");
                    }

                    // validate before approve
                    if (string.IsNullOrEmpty(dbItem.BLNo))
                    {
                        throw new Exception("B/L number is required!");
                    }
                    if (dbItem.ETA == null)
                    {
                        throw new Exception("ETA is required!");
                    }
                    if (dbItem.ETA2 == null)
                    {
                        throw new Exception("ETA2 is required!");
                    }
                    if (dbItem.ShipedDate == null)
                    {
                        throw new Exception("Shipped Date is required!");
                    }

                    //check all loadingplan is confirmed?
                    var dbLoadingPlancheck = context.Booking2Mng_LoadingPlan_View.Where(o => o.BookingID == dbItem.BookingID).ToList();
                    if (dbLoadingPlancheck.Count == 0)
                    {
                        throw new Exception("Plase create loading plan before confirm !!!");
                    }

                    var loadingPlanNotConfirmed = dbLoadingPlancheck.Where(o => !o.IsConfirmed).ToList();
                    if (loadingPlanNotConfirmed.Count > 0)
                    {
                        throw new Exception("Plase confirm all loadingplan before confirm booking !!!");
                    }

                    //
                    //auto create purchasing invoice for all loading plan
                    //
                    PurchasingInvoice purchasingInvoice;
                    PurchasingInvoiceDetail purchasingInvoiceDetail;
                    PurchasingInvoiceSparepartDetail purchasingInvoiceSparepartDetail;
                    PurchasingInvoiceSampleDetail purchasingInvoiceSampleDetail;

                    PackingList packingList;
                    PackingListDetail packingListDetail;
                    PackingListSparepartDetail packingListSparepartDetail;
                    PackingListSampleDetail packingListSampleDetail;

                    //get all loading plan base on booking
                    var dbBooking2New = context.Booking2Mng_BookingForPurChasingInvoice_View
                                                                            .Include("Booking2Mng_LoadingPlanDetailForPurChasingInvoice_View")
                                                                            .Include("Booking2Mng_LoadingPlanSparepartDetailForPurChasingInvoice_View")
                                                                            .Include("Booking2Mng_LoadingPlanSampleDetailForPurChasingInvoice_View")
                                                                            .FirstOrDefault(o => o.BookingID == dbItem.BookingID);

                    purchasingInvoice = context.PurchasingInvoice.Where(o => o.BookingID == dbItem.BookingID).FirstOrDefault();
                    if (purchasingInvoice == null)
                    {
                        //create purchasing invoice
                        purchasingInvoice = new PurchasingInvoice();
                        context.PurchasingInvoice.Add(purchasingInvoice);

                        //create purchasing invoice info
                        purchasingInvoice.InvoiceType = 1;
                        purchasingInvoice.SupplierID = dbBooking2New.SupplierID;
                        purchasingInvoice.BookingID = dbBooking2New.BookingID;
                        purchasingInvoice.Season = dbBooking2New.Season;
                        purchasingInvoice.InvoiceDate = DateTime.Now;
                        purchasingInvoice.SupplierID = dbBooking2New.SupplierID;
                        purchasingInvoice.CreatedBy = userId;
                        purchasingInvoice.CreatedDate = DateTime.Now;

                        //create packing list
                        packingList = new PackingList();
                        purchasingInvoice.PackingList.Add(packingList);

                        //packingList.PackingListUD = dbPurchasingInvoice.InvoiceNo;
                        packingList.PackingListDate = DateTime.Now;
                        packingList.CreatedBy = userId;
                        packingList.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        packingList = purchasingInvoice.PackingList.FirstOrDefault();
                    }
                    //create product for purchasing invoice
                    foreach (var itemLoadingPlanDetail in dbBooking2New.Booking2Mng_LoadingPlanDetailForPurChasingInvoice_View.ToList())
                    {
                        //purchasing invoice
                        purchasingInvoiceDetail = purchasingInvoice.PurchasingInvoiceDetail.Where(o => o.LoadingPlanDetailID == itemLoadingPlanDetail.LoadingPlanDetailID).FirstOrDefault();
                        if (purchasingInvoiceDetail == null)
                        {
                            purchasingInvoiceDetail = new PurchasingInvoiceDetail();
                            purchasingInvoice.PurchasingInvoiceDetail.Add(purchasingInvoiceDetail);

                            packingListDetail = new PackingListDetail();
                            purchasingInvoiceDetail.PackingListDetail.Add(packingListDetail);
                            packingList.PackingListDetail.Add(packingListDetail);
                        }
                        purchasingInvoiceDetail.LoadingPlanDetailID = itemLoadingPlanDetail.LoadingPlanDetailID;
                        purchasingInvoiceDetail.Quantity = itemLoadingPlanDetail.Quantity;
                        purchasingInvoiceDetail.UnitPrice = itemLoadingPlanDetail.QuotationPurchasingPrice;
                        purchasingInvoiceDetail.FactoryPrice = itemLoadingPlanDetail.QuotationPurchasingPrice;

                        //packing list
                        packingListDetail = purchasingInvoiceDetail.PackingListDetail.FirstOrDefault();

                        packingListDetail.QuantityShipped = itemLoadingPlanDetail.Quantity;
                        packingListDetail.PKGs = itemLoadingPlanDetail.PKGs;
                        packingListDetail.NettWeight = itemLoadingPlanDetail.NettWeight;
                        packingListDetail.KGs = itemLoadingPlanDetail.KGs;
                        packingListDetail.CBMs = itemLoadingPlanDetail.CBMs;

                    }

                    //create sparepart for purchasing invoice
                    foreach (var itemLoadingPlanSparepartDetail in dbBooking2New.Booking2Mng_LoadingPlanSparepartDetailForPurChasingInvoice_View.ToList())
                    {
                        purchasingInvoiceSparepartDetail = purchasingInvoice.PurchasingInvoiceSparepartDetail.Where(o => o.LoadingPlanSparepartDetailID == itemLoadingPlanSparepartDetail.LoadingPlanSparepartDetailID).FirstOrDefault();
                        if (purchasingInvoiceSparepartDetail == null)
                        {
                            purchasingInvoiceSparepartDetail = new PurchasingInvoiceSparepartDetail();
                            purchasingInvoice.PurchasingInvoiceSparepartDetail.Add(purchasingInvoiceSparepartDetail);

                            packingListSparepartDetail = new PackingListSparepartDetail();
                            purchasingInvoiceSparepartDetail.PackingListSparepartDetail.Add(packingListSparepartDetail);
                            packingList.PackingListSparepartDetail.Add(packingListSparepartDetail);
                        }
                        purchasingInvoiceSparepartDetail.LoadingPlanSparepartDetailID = itemLoadingPlanSparepartDetail.LoadingPlanSparepartDetailID;
                        purchasingInvoiceSparepartDetail.Quantity = itemLoadingPlanSparepartDetail.Quantity;
                        purchasingInvoiceSparepartDetail.UnitPrice = itemLoadingPlanSparepartDetail.QuotationPurchasingPrice;
                        purchasingInvoiceSparepartDetail.FactoryPrice = itemLoadingPlanSparepartDetail.QuotationPurchasingPrice;

                        //packing list
                        packingListSparepartDetail = purchasingInvoiceSparepartDetail.PackingListSparepartDetail.FirstOrDefault();

                        packingListSparepartDetail.QuantityShipped = itemLoadingPlanSparepartDetail.Quantity;
                        packingListSparepartDetail.PKGs = itemLoadingPlanSparepartDetail.PKGs;
                        packingListSparepartDetail.NettWeight = itemLoadingPlanSparepartDetail.NettWeight;
                        packingListSparepartDetail.KGs = itemLoadingPlanSparepartDetail.KGs;
                        packingListSparepartDetail.CBMs = itemLoadingPlanSparepartDetail.CBMs;
                    }

                    //create sample Product for purchasing invoice
                    foreach (var itemLoadingsample in dbBooking2New.Booking2Mng_LoadingPlanSampleDetailForPurChasingInvoice_View.ToList())
                    {
                        purchasingInvoiceSampleDetail = purchasingInvoice.PurchasingInvoiceSampleDetail.Where(o => o.LoadingPlanSampleDetailID == itemLoadingsample.LoadingPlanSampleDetailID).FirstOrDefault();
                        if (purchasingInvoiceSampleDetail == null)
                        {
                            purchasingInvoiceSampleDetail = new PurchasingInvoiceSampleDetail();
                            purchasingInvoice.PurchasingInvoiceSampleDetail.Add(purchasingInvoiceSampleDetail);

                            packingListSampleDetail = new PackingListSampleDetail();
                            purchasingInvoiceSampleDetail.PackingListSampleDetail.Add(packingListSampleDetail);
                            packingList.PackingListSampleDetail.Add(packingListSampleDetail);
                        }
                        purchasingInvoiceSampleDetail.LoadingPlanSampleDetailID = itemLoadingsample.LoadingPlanSampleDetailID;
                        purchasingInvoiceSampleDetail.Quantity = itemLoadingsample.Quantity;
                        purchasingInvoiceSampleDetail.UnitPrice = itemLoadingsample.QuotationPurchasingPrice;
                        purchasingInvoiceSampleDetail.FactoryPrice = itemLoadingsample.QuotationPurchasingPrice;

                        //packing list
                        packingListSampleDetail = purchasingInvoiceSampleDetail.PackingListSampleDetail.FirstOrDefault();

                        packingListSampleDetail.QuantityShipped = itemLoadingsample.Quantity;
                        packingListSampleDetail.PKGs = itemLoadingsample.PKGs;
                        packingListSampleDetail.NettWeight = itemLoadingsample.NettWeight;
                        packingListSampleDetail.KGs = itemLoadingsample.KGs;
                        packingListSampleDetail.CBMs = itemLoadingsample.CBMs;
                    }
                    
                    //
                    //confirm booking
                    //
                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = userId;

                    //Create InvoiceNo
                    context.SaveChanges();
                    context.PurchasingInvoiceMng_function_GenerateInvoiceNo(purchasingInvoice.PurchasingInvoiceID);
                    dtoItem = GetData(userId, dbItem.BookingID, -1, -1, string.Empty, out notification).Data;                    

                    string emailSubject = string.Format("Purchasing Invoice created / Bill# " + dbItem.BLNo);
                    fwFactory.SendEmailNotificationBasedOn("Booking2Mng", purchasingInvoice.PurchasingInvoiceID, emailSubject, 1);

                    var listemail = context.Factory2Mng_PersonInCharge_View.Where(o => o.SupplierID == dtoBooking.SupplierID && o.ResponsibleFor == "DOCUMENTS/INVOICES");
                    var emailBodySpecial = "Please check: http://app.tilsoft.bg/PurchasingInvoice/Edit/" + purchasingInvoice.PurchasingInvoiceID;
                    if (listemail != null)
                    {
                        foreach (var item in listemail)
                        {
                            EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                            dbEmail.EmailBody = emailBodySpecial;
                            dbEmail.EmailSubject = emailSubject;
                            dbEmail.SendTo = item.Email;
                            context.EmailNotificationMessage.Add(dbEmail);

                        }
                        context.SaveChanges();
                    }
                    // Get information notification with status
                    var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == "Booking2Mng" && o.StatusID == 1).ToList();
                    foreach (var status in groupStatuses)
                    {
                        // Create email body
                        var EmailBody = FrameworkSetting.Setting.FrontendUrl + status.URLLink + "/Edit/" + purchasingInvoice.PurchasingInvoiceID.ToString();

                        if (!string.IsNullOrEmpty(status.EmailTemplate))
                        {
                            EmailBody = EmailBody + Environment.NewLine + status.EmailTemplate + purchasingInvoice.PurchasingInvoiceID.ToString();
                        }
                        var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == status.NotificationGroupID);
                        foreach (var user in dbGroupMember)
                        {
                            // add to NotificationMessage table
                            NotificationMessage notification1 = new NotificationMessage();
                            notification1.UserID = user.UserID;
                            notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_LOGISTICS;
                            notification1.NotificationMessageTitle = emailSubject;
                            notification1.NotificationMessageContent = EmailBody;
                            context.NotificationMessage.Add(notification1);
                            
                        }
                    }
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Booking dtoBooking = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Booking>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected booking data");
                }

                using (Booking2MngEntities context = CreateContext())
                {
                    Booking dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Booking not found!");
                    }
                    else if (dbItem.IsConfirmed == true)
                    {
                        var dbCheckPurchasingInvoice = context.Booking2Mng_PurchasingInvoiceCheckStatus_View.Where(o => o.BookingID == dbItem.BookingID).FirstOrDefault();
                        if (dbCheckPurchasingInvoice != null) throw new Exception("Plase Check Purchasing Invoice. It has been confirmed !!!");
                        var dbCheckEcoomercialInvoice = context.Booking2Mng_ECommercialInvoiceCheckStatus_View.Where(o => o.BookingID == dbItem.BookingID).FirstOrDefault();
                        if (dbCheckPurchasingInvoice != null) throw new Exception("Plase Check ECommercial Invoice. Once or more has been confirmed !!!");

                        dbItem.IsConfirmed = null;
                        dbItem.ConfirmedDate = null;
                        dbItem.ConfirmedBy = null;

                        dbItem.IsReset = true;
                        dbItem.ResetBy = userId;
                        dbItem.ResetDate = DateTime.Now;

                        context.SaveChanges();
                    }
                    else
                    {
                        dbItem.IsConfirmAllLoading = null;
                        dbItem.ConfirmAllLoadingBy = null;
                        dbItem.ConfirmAllLoadingDate = null;

                        dbItem.IsReset = true;
                        dbItem.ResetBy = userId;
                        dbItem.ResetDate = DateTime.Now;

                        context.SaveChanges();
                    }
                    dtoItem = GetData(userId, dbItem.BookingID, -1, -1, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Suppliers = new List<Support.DTO.Supplier>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.Suppliers = supportFactory.GetSupplier(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int supplierID, int clientID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Booking();
            data.Data.LoadingPlans = new List<DTO.LoadingPlan>();
            data.DeliveryTerms = new List<Support.DTO.DeliveryTerm>();
            data.MovementTerms = new List<Support.DTO.MovementTerm>();
            data.OceanFreights = new List<Support.DTO.StringCollectionItem>();
            data.PODs = new List<Support.DTO.POD>();
            data.POLs = new List<Support.DTO.POL>();
            data.Forwarders = new List<Support.DTO.Forwarder>();

            //try to get data
            try
            {
                // check if user can update this information 
                if (id <= 0 && fwFactory.CheckSupplierPermission(userId, supplierID) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected supplier data");
                }
                if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected booking data");
                }

                using (Booking2MngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        data.Data.Season = season;
                        data.Data.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                        data.Data.ClientID = clientID;
                        data.Data.SupplierID = supplierID;
                        data.Data.ClientUD = supportFactory.GetClient().FirstOrDefault(o => o.ClientID == clientID).ClientUD;
                        Support.DTO.Supplier dtoSupplier = supportFactory.GetSupplierInfo(supplierID);
                        if (dtoSupplier != null)
                        {
                            data.Data.ShipperInfo = dtoSupplier.SupplierNM + Environment.NewLine + dtoSupplier.Address;
                            data.Data.SupplierNM = dtoSupplier.SupplierNM;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_Booking(context.Booking2Mng_Booking_View.Include("Booking2Mng_LoadingPlan_View").FirstOrDefault(o => o.BookingID == id));
                    }
                }

                data.DeliveryTerms = supportFactory.GetDeliveryTerm();
                data.MovementTerms = supportFactory.GetMovementTerm();
                data.OceanFreights = supportFactory.GetOceanFreight();
                data.PODs = supportFactory.GetPOD();
                data.POLs = supportFactory.GetPOL();
                data.Forwarders = supportFactory.GetForwarder();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected booking data");
                }

                using (Booking2MngEntities context = CreateContext())
                {
                    // check if can delete
                    Booking dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Booking not found");
                    }
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not delete booking: it's already been confirmed!");
                    }

                    // everything ok, delete
                    // remove attached file
                    if (!string.IsNullOrEmpty(dbItem.BLFile))
                    {
                        fwFactory.RemoveImageFile(dbItem.BLFile);
                    }
                    context.Booking.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public bool ConfirmETA(int userId, int id, int ETAType, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Booking dtoBooking = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Booking>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Booking2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected booking data");
                    }

                    Booking dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Booking();
                        context.Booking.Add(dbItem);
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Booking not found!";
                        return false;
                    }
                    else
                    {
                        System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("nl-NL");
                        DateTime tmpDate;
                        if (ETAType == 1)
                        {
                            if ((!dbItem.IsETAConfirmed.HasValue || !dbItem.IsETAConfirmed.Value) && !string.IsNullOrEmpty(dtoBooking.ETA))
                            {
                                if (DateTime.TryParse(dtoBooking.ETA, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                                {
                                    dbItem.ETA = tmpDate;
                                }
                                dbItem.IsETAConfirmed = true;
                                dbItem.ETAConfirmedBy = userId;
                                dbItem.ETAConfirmedDate = DateTime.Now;                                
                            }
                            else
                            {
                                throw new Exception("ETA already confirmed!");
                            }
                        }
                        else if (ETAType == 2)
                        {
                            if ((!dbItem.IsETA2Confirmed.HasValue || !dbItem.IsETA2Confirmed.Value) && !string.IsNullOrEmpty(dtoBooking.ETA2))
                            {
                                if (DateTime.TryParse(dtoBooking.ETA2, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                                {
                                    dbItem.ETA2 = tmpDate;
                                }
                                dbItem.IsETA2Confirmed = true;
                                dbItem.ETA2ConfirmedBy = userId;
                                dbItem.ETA2ConfirmedDate = DateTime.Now;
                            }
                            else
                            {
                                throw new Exception("ETA already confirmed!");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid ETA type!");
                        }

                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.BookingID, -1, -1, string.Empty, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public string GetExcelReportData(int bookingID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Booking2Mng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@BookingID", bookingID);

                adap.TableMappings.Add("Table", "BookingMng_Booking_ReportView");
                adap.Fill(ds);

                //generate schema
                //DALBase.Helper.DevCreateReportXMLSource(ds, "Booking");

                //generate xml file
                return Library.Helper.CreateReportFiles(ds, "Booking");
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return string.Empty;
            }
        }

        public List<DTO.ShippingInstruction> GetSetDefault(int clientID, out Notification notification)
        {
            List<DTO.ShippingInstruction> shippingInstructions = new List<DTO.ShippingInstruction>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    shippingInstructions = converter.DB2DTO_ShippingInstructions(context.Booking2Mng_ShippingInstruction_View.Where(o => o.ClientID == clientID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return shippingInstructions;
        }

        public bool ConfirmAllLoading(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.Booking dtoBooking = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Booking>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (Booking2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected booking data");
                    }

                    Booking dbItem = null;
                    if (id == 0)
                    {
                        throw new Exception("Plase Create Booking First !!!");
                    }
                    else
                    {
                        dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Booking not found!";
                        return false;
                    }
                    else
                    {
                        var dbLoadingPlancheck = context.Booking2Mng_LoadingPlan_View.Where(o => o.BookingID == dbItem.BookingID).ToList();
                        if (dbLoadingPlancheck.Count == 0)
                        {
                            throw new Exception("Plase create loading plan before confirm !!!");
                        }

                        var loadingPlanNotConfirmed = dbLoadingPlancheck.Where(o => !o.IsConfirmed).ToList();
                        if (loadingPlanNotConfirmed.Count > 0)
                        {
                            throw new Exception("Plase confirm all loadingplan before confirm booking !!!");
                        }
                        dbItem.IsConfirmAllLoading = true;
                        dbItem.ConfirmAllLoadingBy = userId;
                        dbItem.ConfirmAllLoadingDate = DateTime.Now;
                        

                        if (dbItem.IsConfirmAllLoading == true)
                        {
                            var emailSubject1 = "Booking " + dtoBooking.BookingUD + " has been confirmed by Factory " + dtoBooking.SupplierNM;
                            fwFactory.SendEmailNotificationBasedOn("Booking2Mng", dtoBooking.BookingID, emailSubject1, 3);

                            // Get information notification with status
                            var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == "Booking2Mng" && o.StatusID == 3).ToList();
                            foreach (var status in groupStatuses)
                            {
                                // Create email body
                                var EmailBody = FrameworkSetting.Setting.FrontendUrl + status.URLLink + "/Edit/" + dtoBooking.BookingID.ToString();

                                if (!string.IsNullOrEmpty(status.EmailTemplate))
                                {
                                    EmailBody = EmailBody + Environment.NewLine + status.EmailTemplate + dtoBooking.BookingID.ToString();
                                }
                                var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == status.NotificationGroupID);
                                foreach (var user in dbGroupMember)
                                {
                                    // add to NotificationMessage table
                                    NotificationMessage notification1 = new NotificationMessage();
                                    notification1.UserID = user.UserID;
                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_LOGISTICS;
                                    notification1.NotificationMessageTitle = emailSubject1;
                                    notification1.NotificationMessageContent = EmailBody;
                                    context.NotificationMessage.Add(notification1);
                                    
                                }
                            }
                        }
                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.BookingID, -1, -1, string.Empty, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public string GetLoadingPlanReportData(int bookingID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            CustomDocDataObject ds = new CustomDocDataObject();
            try
            {
                //int userOfficeID = fwFactory.GetUserOffice(iRequesterID);
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Booking2Mng_function_GetContainerReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@BookingID", bookingID);

                adap.TableMappings.Add("Table", "Booking2Mng_BookingLoadingPlan_ReportView");
                adap.TableMappings.Add("Table1", "Booking2Mng_LoadingPlanDetail_ReportView");
                adap.TableMappings.Add("Table2", "Booking2Mng_LoadingPlanSparepartDetail_ReportView");
                adap.TableMappings.Add("Table3", "Booking2Mng_LoadingPlanContainer_ReportView");
                adap.TableMappings.Add("Table4", "Booking2Mng_LoadingPlanSampleDetail_ReportView");
                adap.Fill(ds);

                CustomDocDataObject.BookingRow drBooking = ds.Booking.NewBookingRow();
                CustomDocDataObject.Booking2Mng_BookingLoadingPlan_ReportViewRow drOriginBooking = ds.Booking2Mng_BookingLoadingPlan_ReportView.FirstOrDefault();

                drBooking.BLNo = drOriginBooking.IsBLNoNull() ? "" : drOriginBooking.BLNo;
                drBooking.POLName = drOriginBooking.IsPOLNameNull() ? "" : drOriginBooking.POLName;
                drBooking.PODName = drOriginBooking.IsPODNameNull() ? "" : drOriginBooking.PODName;
                drBooking.TotalQuantity = drOriginBooking.IsTotalQuantityNull() ? 0 : drOriginBooking.TotalQuantity;
                drBooking.TotalAmount = drOriginBooking.IsTotalAmountNull() ? 0 : drOriginBooking.TotalAmount;
                drBooking.SupplierNM = drOriginBooking.IsSupplierNMNull() ? "" : drOriginBooking.SupplierNM;
                drBooking.Address = drOriginBooking.IsAddressNull() ? "" : drOriginBooking.Address;
                drBooking.BankName = drOriginBooking.IsBankNameNull() ? "" : drOriginBooking.BankName;
                drBooking.BankAddress = drOriginBooking.IsBankAddressNull() ? "" : drOriginBooking.BankAddress;
                drBooking.BankSwiftCode = drOriginBooking.IsBankSwiftCodeNull() ? "" : drOriginBooking.BankSwiftCode;
                drBooking.BankAccountNo = drOriginBooking.IsBankNameNull() ? "" : drOriginBooking.BankName;
                drBooking.IsBuyingOrangePie = drOriginBooking.IsIsBuyingOrangePieNull() ? 0 : drOriginBooking.IsBuyingOrangePie;
                drBooking.ShippedDate = drOriginBooking.IsShippedDateNull() ? "" : drOriginBooking.ShippedDate;
                ds.Booking.AddBookingRow(drBooking);

                //parse detail data
                CustomDocDataObject.BookingDetailRow drBookingDetail;
                foreach (var loadingplan in ds.Booking2Mng_LoadingPlanContainer_ReportView)
                {
                    drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                    drBookingDetail.Description = loadingplan.ContainerInfo;
                    ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                    foreach (var product in ds.Booking2Mng_LoadingPlanDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                        drBookingDetail.Order_Client = product.ProformaInvoiceNo + " / " + product.ClientUD;
                        drBookingDetail.ArticleCode = product.ArticleCode;
                        drBookingDetail.Description = product.Description;
                        drBookingDetail.Quantity = product.IsQuantityNull() ? 0 : product.Quantity;
                        drBookingDetail.HSCode = product.IsHSCodeNull() ? "" : product.HSCode;
                        drBookingDetail.UnitPrice = product.IsUnitPriceNull() ? 0 : product.UnitPrice;
                        drBookingDetail.Amount = drBookingDetail.UnitPrice * drBookingDetail.Quantity;
                        ds.BookingDetail.AddBookingDetailRow(drBookingDetail);

                        //client artcode, client artName
                        if (!string.IsNullOrEmpty(product.ClientArticleCode))
                        {
                            drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                            drBookingDetail.HSCode = "CLIENT ART CODE:";
                            drBookingDetail.Description = product.ClientArticleCode;
                            ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                        }
                        if (!string.IsNullOrEmpty(product.ClientArticleName))
                        {
                            drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                            drBookingDetail.HSCode = "CLIENT ART NAME:";
                            drBookingDetail.Description = product.ClientArticleName;
                            ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                        }

                    }
                    foreach (var sparepart in ds.Booking2Mng_LoadingPlanSparepartDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                        drBookingDetail.Order_Client = sparepart.ProformaInvoiceNo + " / " + sparepart.ClientUD;
                        drBookingDetail.ArticleCode = sparepart.ArticleCode;
                        drBookingDetail.Description = sparepart.Description;
                        drBookingDetail.Quantity = sparepart.IsQuantityNull() ? 0 : sparepart.Quantity;
                        drBookingDetail.HSCode = sparepart.IsHSCodeNull() ? "" : sparepart.HSCode;
                        drBookingDetail.UnitPrice = sparepart.IsUnitPriceNull() ? 0 : sparepart.UnitPrice;
                        drBookingDetail.Amount = drBookingDetail.UnitPrice * drBookingDetail.Quantity;
                        ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                        
                        //client artcode, client artName
                        if (!string.IsNullOrEmpty(sparepart.ClientArticleCode))
                        {
                            drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                            drBookingDetail.HSCode = "CLIENT ART CODE:";
                            drBookingDetail.Description = sparepart.ClientArticleCode;
                            ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                        }
                        if (!string.IsNullOrEmpty(sparepart.ClientArticleName))
                        {
                            drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                            drBookingDetail.HSCode = "CLIENT ART NAME:";
                            drBookingDetail.Description = sparepart.ClientArticleName;
                            ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                        }
                    }

                    foreach (var sampleproduct in ds.Booking2Mng_LoadingPlanSampleDetail_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                    {
                        drBookingDetail = ds.BookingDetail.NewBookingDetailRow();
                        drBookingDetail.Order_Client = sampleproduct.ProformaInvoiceNo + " / " + sampleproduct.ClientUD;
                        drBookingDetail.ArticleCode = sampleproduct.ArticleCode;
                        drBookingDetail.Description = sampleproduct.Description;
                        drBookingDetail.Quantity = sampleproduct.IsQuantityNull() ? 0 : sampleproduct.Quantity;
                        drBookingDetail.HSCode = sampleproduct.IsHSCodeNull() ? "" : sampleproduct.HSCode;
                        drBookingDetail.UnitPrice = sampleproduct.IsUnitPriceNull() ? 0 : sampleproduct.UnitPrice;
                        drBookingDetail.Amount = drBookingDetail.UnitPrice * drBookingDetail.Quantity;
                        ds.BookingDetail.AddBookingDetailRow(drBookingDetail);
                    }
                }
                string reportName = "BookingLoadingPlan";
                return Library.Helper.CreateReportFiles(ds, reportName);
            }
            catch (Exception ex)  {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return string.Empty;
            }
        }

        public string GetLoadingPlanReportData_PL(int bookingID, int reportOption, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            if (reportOption == 1)
            {
                CustomDocOrangePinePLDataObject ds = new CustomDocOrangePinePLDataObject();

                try
                {
                    SqlDataAdapter adap = new SqlDataAdapter();
                    adap.SelectCommand = new SqlCommand("Booking2Mng_function_GetOrangePinePackingList_Printout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@BookingID", bookingID);

                    adap.TableMappings.Add("Table", "OrangePine_PackingList_ReportView");
                    adap.TableMappings.Add("Table1", "OrangePine_Container_ReportView");
                    adap.TableMappings.Add("Table2", "OrangePine_Goods_ReportView");
                    adap.Fill(ds);

                    string ClientOrderNos = "";
                    string CustomerOrderNos = "";
                    foreach (var item in ds.OrangePine_Goods_ReportView)
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
                    ds.OrangePine_PackingList_ReportView.FirstOrDefault().ClientOrderNos = ClientOrderNos;
                    ds.OrangePine_PackingList_ReportView.FirstOrDefault().CustomerOrderNos = CustomerOrderNos;

                    CustomDocOrangePinePLDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                    CustomDocOrangePinePLDataObject.OrangePine_PackingList_ReportViewRow drOrigin = ds.OrangePine_PackingList_ReportView.FirstOrDefault();
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
                    CustomDocOrangePinePLDataObject.PackingListDetailRow drPackingListDetail;

                    foreach (var container in ds.OrangePine_Container_ReportView)
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Description = "1x" + (container.IsContainerTypeNMNull() ? "" : container.ContainerTypeNM) + " CONTAINER / CONT.NO: " + (container.IsContainerNoNull() ? "" : container.ContainerNo) + " / SEAL NO: " + (container.IsSealNoNull() ? "" : container.SealNo);
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                        foreach (var product in ds.OrangePine_Goods_ReportView.Where(o => o.ContainerNo == container.ContainerNo))
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

                            //client artcode, client artName
                            if (!string.IsNullOrEmpty(product.ClientArticleCode))
                            {
                                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                                drPackingListDetail.Description = "CLIENT ART CODE: " + product.ClientArticleCode;
                                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                            }
                            if (!string.IsNullOrEmpty(product.ClientArticleName))
                            {
                                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                                drPackingListDetail.Description = "CLIENT ART NAME: " + product.ClientArticleName;
                                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                            }
                        }
                    }

                    // generate xml file
                    string reportFile = Library.Helper.CreateReportFiles(ds, "Booking_PackingList_OrangePine");
                    return reportFile = reportFile + ".xlsm";
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
            if (reportOption == 2)
            {
                CustomDocPLDataObject ds = new CustomDocPLDataObject();

                try
                {
                    SqlDataAdapter adap = new SqlDataAdapter();
                    adap.SelectCommand = new SqlCommand("Booking2Mng_function_GetPackingList_Printout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@BookingID", bookingID);

                    adap.TableMappings.Add("Table", "Booking2Mng_PackingList_ReportView");
                    adap.TableMappings.Add("Table1", "Booking2Mng_Container_ReportView");
                    adap.TableMappings.Add("Table2", "Booking2Mng_Goods_ReportView");
                    adap.Fill(ds);

                    //parese invoice
                    CustomDocPLDataObject.PackingListRow drPackingList = ds.PackingList.NewPackingListRow();
                    CustomDocPLDataObject.Booking2Mng_PackingList_ReportViewRow drOrigin = ds.Booking2Mng_PackingList_ReportView.FirstOrDefault();

                    drPackingList.InvoiceNo = drOrigin.IsInvoiceNoNull() ? "" : drOrigin.InvoiceNo;
                    drPackingList.BLNo = drOrigin.IsBLNoNull() ? "" : drOrigin.BLNo;
                    drPackingList.POLName = drOrigin.IsPOLNameNull() ? "" : drOrigin.POLName;
                    drPackingList.PODName = drOrigin.IsPODNameNull() ? "" : drOrigin.PODName;
                    drPackingList.TotalQuantityShipped = drOrigin.IsTotalQuantityShippedNull() ? 0 : drOrigin.TotalQuantityShipped;
                    drPackingList.TotalPKGs = drOrigin.IsTotalPKGsNull() ? 0 : drOrigin.TotalPKGs;
                    drPackingList.TotalNettWeight = drOrigin.IsTotalNettWeightNull() ? 0 : drOrigin.TotalNettWeight;
                    drPackingList.TotalKGs = drOrigin.IsTotalKGsNull() ? 0 : drOrigin.TotalKGs;
                    drPackingList.TotalCBMs = drOrigin.IsTotalCBMsNull() ? 0 : drOrigin.TotalCBMs;
                    drPackingList.ShippedDate = drOrigin.IsShipedDateNull() ? "" : drOrigin.ShipedDate.ToString("dd/MM/yyyy");
                    drPackingList.SupplierNM = drOrigin.IsSupplierNMNull() ? "" : drOrigin.SupplierNM;
                    drPackingList.Address = drOrigin.IsAddressNull() ? "" : drOrigin.Address;
                    ds.PackingList.AddPackingListRow(drPackingList);

                    //parse detail data
                    CustomDocPLDataObject.PackingListDetailRow drPackingListDetail;
                    foreach (var loadingplan in ds.Booking2Mng_Container_ReportView)
                    {
                        drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                        drPackingListDetail.Description = loadingplan.IsContainerInfoNull() ? "" : loadingplan.ContainerInfo;
                        ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                        foreach (var product in ds.Booking2Mng_Goods_ReportView.Where(o => o.ContainerInfo == loadingplan.ContainerInfo))
                        {
                            drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                            drPackingListDetail.Order_Client = (product.IsProformaInvoiceNoNull() ? "" : product.ProformaInvoiceNo) + " / " + (product.IsClientUDNull() ? "" : product.ClientUD);
                            drPackingListDetail.ArticleCode = product.IsArticleCodeNull() ? "" : product.ArticleCode;
                            drPackingListDetail.Description = product.IsDescriptionNull() ? "" : product.Description;
                            drPackingListDetail.QuantityShipped = product.IsQuantityShippedNull() ? 0 : product.QuantityShipped;
                            drPackingListDetail.PKGs = product.IsPKGsNull() ? 0 : product.PKGs;
                            drPackingListDetail.NettWeight = product.IsNettWeightNull() ? 0 : product.NettWeight;
                            drPackingListDetail.KGs = product.IsKGsNull() ? 0 : product.KGs;
                            drPackingListDetail.CBMs = product.IsCBMsNull() ? 0 : product.CBMs;
                            drPackingListDetail.HSCode = product.IsHSCodeNull() ? "" : product.HSCode;
                            ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);

                            //client artcode, client artName
                            if (!string.IsNullOrEmpty(product.ClientArticleCode))
                            {
                                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                                drPackingListDetail.HSCode = "CLIENT ART CODE:";
                                drPackingListDetail.Description = product.ClientArticleCode;
                                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                            }
                            if (!string.IsNullOrEmpty(product.ClientArticleName))
                            {
                                drPackingListDetail = ds.PackingListDetail.NewPackingListDetailRow();
                                drPackingListDetail.HSCode = "CLIENT ART NAME:";
                                drPackingListDetail.Description = product.ClientArticleName;
                                ds.PackingListDetail.AddPackingListDetailRow(drPackingListDetail);
                            }
                        }
                    }
                    // generate xml file
                    string reportFile = Library.Helper.CreateReportFiles(ds, "Booking_PackingList");
                    return reportFile = reportFile + ".xlsm";

                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        //List FactoryOrder
        public DTO.ListFactoryOrderDTO GetFactoryOrder(int? clientID, string season, string pi, int? bookingID, out Notification notification)
        {
            DTO.ListFactoryOrderDTO listFactoryOrder = new DTO.ListFactoryOrderDTO();
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    listFactoryOrder.FactoryOrderDTOs = converter.DB2DTO_FactoryOrder(context.Booking2Mng_function_ListFactoryOrder(clientID, season, pi, bookingID).ToList());
                    listFactoryOrder.FactoryOrderDetailDTOs = converter.DB2DTO_FactoryOrderDetail(context.Booking2Mng_function_ListFactoryOrderDetail(clientID, season, pi, bookingID).ToList());
                    listFactoryOrder.FactoryOrderSampleDetailDTOs = converter.DB2DTO_FactoryOrderSampleDetail(context.Booking2Mng_function_ListFactoryOrderSampleDetail(clientID, season, pi, bookingID).ToList());
                    listFactoryOrder.FactoryOrderSparepartDetailDTOs = converter.DB2DTO_FactoryOrderSparepartDetail(context.Booking2Mng_function_ListFactoryOrderSparepartDetail(clientID, season, pi, bookingID).ToList());
                    foreach (var item in listFactoryOrder.FactoryOrderDTOs.ToList())
                    {
                        int? total = 0;
                        foreach (var sDetail in listFactoryOrder.FactoryOrderDetailDTOs.ToList())
                        {
                            if(sDetail.FactoryOrderID == item.FactoryOrderID)
                            {
                                total += sDetail.PlanQnt;
                            }
                        }
                        foreach (var sDetailSample in listFactoryOrder.FactoryOrderSampleDetailDTOs.ToList())
                        {
                            if (sDetailSample.FactoryOrderID == item.FactoryOrderID)
                            {
                                total += sDetailSample.PlanQnt;
                            }
                        }
                        foreach (var sDetailSparepart in listFactoryOrder.FactoryOrderSparepartDetailDTOs.ToList())
                        {
                            if (sDetailSparepart.FactoryOrderID == item.FactoryOrderID)
                            {
                                total += sDetailSparepart.PlanQnt;
                            }
                        }

                        item.OrderQnt = total;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return listFactoryOrder;
        }

        
    }
}
