using Library;
using Library.DTO;
using Module.OfferToClientMng.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Module.OfferToClientMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private OfferToClientMngEntities CreateContext()
        {
            return new OfferToClientMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OfferToClientMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            SearchFormData result = new SearchFormData();
            result.OfferSearchResultDTOs = new List<OfferSearchResultDTO>();
            //try to get data
            try
            {
                using (OfferToClientMngEntities context = CreateContext())
                {
                    string offerUD = null;
                    string season = null;
                    string clientUD = null;
                    string clientNM = null;
                    string paymentTermNM = null;
                    string deliveryTermNM = null;
                    string currency = null;
                    string forwarderNM = null;
                    string podNM = null;
                    string articleCode = null;
                    string description = null;
                    int? v4id = null;
                    int? offerStatus = null;
                    bool? isPotential = null;
                    int? saleID = null;


                    if (filters.ContainsKey("OfferUD") && !string.IsNullOrEmpty(filters["OfferUD"].ToString()))
                    {
                        offerUD = filters["OfferUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        season = filters["Season"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        clientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                    {
                        clientNM = filters["ClientNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("PaymentTermNM") && !string.IsNullOrEmpty(filters["PaymentTermNM"].ToString()))
                    {
                        paymentTermNM = filters["PaymentTermNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("DeliveryTermNM") && !string.IsNullOrEmpty(filters["DeliveryTermNM"].ToString()))
                    {
                        deliveryTermNM = filters["DeliveryTermNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Currency") && !string.IsNullOrEmpty(filters["Currency"].ToString()))
                    {
                        currency = filters["Currency"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ForwarderNM") && !string.IsNullOrEmpty(filters["ForwarderNM"].ToString()))
                    {
                        forwarderNM = filters["ForwarderNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("PODNM") && !string.IsNullOrEmpty(filters["PODNM"].ToString()))
                    {
                        podNM = filters["PODNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        description = filters["Description"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("V4ID") && !string.IsNullOrEmpty(filters["V4ID"].ToString()))
                    {
                        v4id = Convert.ToInt32(filters["V4ID"].ToString().Replace("'", "''"));
                    }

                    if (filters.ContainsKey("OfferStatus") && filters["OfferStatus"] != null && !string.IsNullOrEmpty(filters["OfferStatus"].ToString()))
                    {
                        offerStatus = Convert.ToInt32(filters["OfferStatus"].ToString().Replace("'", "''"));
                    }

                    if (filters.ContainsKey("IsPotential") && filters["IsPotential"] != null && !string.IsNullOrEmpty(filters["IsPotential"].ToString()))
                    {
                        isPotential = filters["IsPotential"].ToString().ConvertStringToBool();
                    }

                    if (filters.ContainsKey("SaleID") && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
                    {
                        saleID = Convert.ToInt32(filters["SaleID"].ToString().Replace("'", "''"));
                    }
                    
                    var results = context.OfferToClientMng_function_SearchOffer(offerUD, season, clientUD, clientNM, paymentTermNM, deliveryTermNM, currency, forwarderNM, podNM, articleCode, description, v4id, offerStatus, isPotential, saleID, orderBy, orderDirection).ToList();
                    totalRows = results.Count();

                    result.OfferSearchResultDTOs = converter.DB2DTO_OfferSearchResultList(results.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    return result;
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
                return result;
            }
        }

        //public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        //{
        //    throw new NotImplementedException();

        //    //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    //DTO.EditFormData data = new DTO.EditFormData();
        //    //data.Data = new DTO.SampleOrder();
        //    //data.Data.SampleOrderStatusID = 1;
        //    //data.Data.SampleOrderStatusNM = "PENDING";
        //    //data.Data.SampleProducts = new List<DTO.SampleProduct>();
        //    //data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

        //    //data.Seasons = new List<Support.DTO.Season>();
        //    //data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
        //    //data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
        //    //data.Users = new List<Support.DTO.User>();
        //    //data.Factories = new List<Support.DTO.Factory>();
        //    //data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
        //    //data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
        //    //data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


        //    ////try to get data
        //    //try
        //    //{
        //    //    using (Sample2MngEntities context = CreateContext())
        //    //    {
        //    //        if (id > 0)
        //    //        {
        //    //            data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
        //    //                .Include("Sample2Mng_SampleProduct_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
        //    //                .Include("Sample2Mng_SampleMonitor_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
        //    //                .FirstOrDefault(o => o.SampleOrderID == id));
        //    //        }

        //    //        data.Seasons = supportFactory.GetSeason().ToList();
        //    //        data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
        //    //        data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
        //    //        data.Users = supportFactory.GetUsers().ToList();
        //    //        data.Factories = supportFactory.GetFactory().ToList();
        //    //        data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
        //    //        data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
        //    //        data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
        //    //        data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification.Type = Library.DTO.NotificationType.Error;
        //    //    notification.Message = ex.Message;
        //    //}

        //    //return data;
        //}

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            OfferDTO inputParam = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<OfferDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            
            try
            {
                using (OfferToClientMngEntities context = CreateContext())
                {
                    Offer dbItem = null;
                    OfferStatus dbOfferStatus = null;
                    if (id == 0) 
                    {
                        dbItem = new Offer();
                        context.Offer.Add(dbItem);

                        //SET OFFER STATUS
                        dbOfferStatus = new OfferStatus();
                        dbOfferStatus.TrackingStatusID = DALBase.Helper.CONFIRMED;
                        dbOfferStatus.StatusDate = DateTime.Now;
                        dbOfferStatus.UpdatedBy = userId;
                        dbOfferStatus.IsCurrentStatus = true;
                        dbItem.OfferStatus.Add(dbOfferStatus);

                        //SET TRACKING INFO
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;

                        inputParam.CreatedBy = userId;
                        inputParam.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                        inputParam.OfferVersion = 1;
                        inputParam.OfferUD = context.OfferToClientMng_function_GenerateOfferCode(inputParam.OfferID, inputParam.Season, inputParam.SaleID, inputParam.ClientID).FirstOrDefault();

                    }
                    else
                    {
                        //get db item
                        dbItem = context.Offer.FirstOrDefault(o => o.OfferID == id);
                        if (dbItem.ClientID != inputParam.ClientID || dbItem.SaleID != inputParam.SaleID || dbItem.Season != inputParam.Season)
                        {
                            inputParam.OfferUD = context.OfferToClientMng_function_GenerateOfferCode(inputParam.OfferID, inputParam.Season, inputParam.SaleID, inputParam.ClientID).FirstOrDefault();
                        }
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Offer not found!";
                        return false;
                    }
                    else
                    {
                        //// check concurrency
                        //if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(inputParam.ConcurrencyFlag_String)))
                        //{
                        //    throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        //}

                        ////offer status
                        //if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CHANGE_OFFER_ITEM_OPTION))
                        //{
                        //    if (dbOfferStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbOfferStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                        //    {
                        //        throw new Exception("Offer was confirmed, you can not change. In case you have to revise offer before save change.");
                        //    }
                        //}
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        //read dto to db
                        converter.DTO2DB_Offer(inputParam, ref dbItem, userId);

                        // check if sale price has changed                     
                        foreach (var entry in context.ChangeTracker.Entries().Where(o => o.State == EntityState.Modified || o.State == EntityState.Added))
                        {
                            if (entry.Entity is OfferLine)
                            {
                                bool hasChanged = false;

                                if (entry.State == EntityState.Modified)
                                {
                                    var originalValue = entry.OriginalValues["UnitPrice"];
                                    var currentValue = entry.CurrentValues["UnitPrice"];

                                    if (originalValue == null && currentValue != null)
                                    {
                                        hasChanged = true;
                                    }
                                    else if (originalValue != null && currentValue == null)
                                    {
                                        hasChanged = true;
                                    }
                                    else if (originalValue != null && currentValue != null)
                                    {
                                        if (Convert.ToDecimal(originalValue) != Convert.ToDecimal(currentValue))
                                        {
                                            hasChanged = true;
                                        }
                                    }
                                }
                                else
                                {
                                    hasChanged = true;
                                }

                                if (hasChanged)
                                {
                                    OfferLineSalePriceHistory dbHistory = new OfferLineSalePriceHistory();
                                    dbHistory.OfferLine = (OfferLine)entry.Entity;
                                    dbHistory.SalePrice = dbHistory.OfferLine.UnitPrice;
                                    dbHistory.UpdatedBy = userId;
                                    dbHistory.UpdatedDate = DateTime.Now;
                                    context.OfferLineSalePriceHistory.Add(dbHistory);
                                }
                            }
                        }
                        
                        //delete offerline is null
                        context.OfferLine.Local.Where(o => o.Offer == null).ToList().ForEach(o => context.OfferLine.Remove(o));

                        //delete offerline sparepart is null
                        context.OfferLineSparepart.Local.Where(o => o.Offer == null).ToList().ForEach(o => context.OfferLineSparepart.Remove(o));

                        //save data
                        context.SaveChanges();

                        //// add item to quotation if needed
                        //context.FW_Quotation_function_AddFactoryOrderItem(dbItem.OfferID, null, null); // table lockx and also check if item is available on sql server side

                        //reload data
                        // description: load all data in dto at this stage is a waste, so only new id needed
                        //dtoItem = GetData(dbItem.OfferID, out notification);
                        if (id > 0)
                        {
                            dtoItem = new OfferDTO { OfferID = id };
                        }
                        else
                        {
                            dtoItem = new OfferDTO { OfferID = dbItem.OfferID };
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return false;
        }
       
        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //public bool Confirm(int userId, int id, out Library.DTO.Notification notification)
        //{            
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been confirmed success" };
        //    try
        //    {
        //        using (OfferToClientMngEntities context = CreateContext())
        //        {
        //            Offer offer = context.Offer.Where(s => s.OfferID == id).FirstOrDefault();                    
                   
        //            if (id == 0)
        //            {
        //                notification = new Library.DTO.Notification() { Message = "You have to save data before confirm offer", Type = Library.DTO.NotificationType.Warning };
        //                return false;
        //            }

        //            OfferStatus dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == id && o.IsCurrentStatus == true).FirstOrDefault();
        //            if (dbOfferStatus.TrackingStatusID != DALBase.Helper.REVISED && dbOfferStatus.TrackingStatusID != DALBase.Helper.CREATED)
        //            {
        //                notification = new Library.DTO.Notification() { Message = "Offer must be in REVISED/CREATED status before confirm", Type = Library.DTO.NotificationType.Warning };
        //                return false;
        //            }                  

        //            //set tracking status
        //            int TrackingStatusID = (dbOfferStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);

        //            SetOfferStatus(id, TrackingStatusID, userId);

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
        //        {
        //            notification.DetailMessage.Add(ex.InnerException.Message);
        //        }
        //        return false;
        //    }
        //}
        //public bool Revise(int userId, int id, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been confirmed success" };
        //    try
        //    {
        //        using (OfferToClientMngEntities context = CreateContext())
        //        {
        //            Offer offer = context.Offer.Where(s => s.OfferID == id).FirstOrDefault();

        //            if (id == 0)
        //            {
        //                notification = new Library.DTO.Notification() { Message = "You have to save data before confirm offer", Type = Library.DTO.NotificationType.Warning };
        //                return false;
        //            }
        //            //check sale order status
        //            if (context.OfferToClientMng_SaleOrderStatus_View.Where(o => o.OfferID == id && (o.TrackingStatusID == DALBase.Helper.CONFIRMED || o.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)).Count() > 0)
        //            {
        //                //throw new Exception("All PI of this offer were confirmed. You can not revise offer. In this case you need to contact with user who make PI to revise PI before revise  offer.");
        //                notification = new Library.DTO.Notification() { Message = "All PI of this offer were confirmed. You can not revise offer. In this case you need to contact with user who make PI to revise PI before revise  offer.", Type = Library.DTO.NotificationType.Warning };
        //                return false;
        //            }

        //            OfferStatus dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == id && o.IsCurrentStatus == true).FirstOrDefault();
        //            if (dbOfferStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbOfferStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
        //            {
        //                notification = new Library.DTO.Notification() { Message = "Offer must be in CONFIRMED status before revise", Type = Library.DTO.NotificationType.Warning };
        //                return false;
        //            }

        //            //set tracking status     
        //            SetOfferStatus(id, DALBase.Helper.REVISED, userId);

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
        //        {
        //            notification.DetailMessage.Add(ex.InnerException.Message);
        //        }
        //        return false;
        //    }
        //}
        public DTO.EditFormData GetDataByClient(int clientID,string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (OfferToClientMngEntities context = CreateContext())
                {
                    EditFormData data = new EditFormData();
                    data.OfferDTO = new OfferDTO();

                    OfferToClientMng_OfferSeason_View dbItem;
                    dbItem = context.OfferToClientMng_OfferSeason_View
                        .FirstOrDefault(o => o.Season == season && o.ClientID == clientID);
                    if (dbItem == null)
                    {
                        dbItem = new OfferToClientMng_OfferSeason_View();
                        dbItem.Remark = "Standard paper wrapped packaging for wicker items" + Environment.NewLine + "Standard box packaging for wooden items" + Environment.NewLine + "Maximum of 3 mix items per container";
                    }
                    data.OfferDTO = converter.DB2DTO_Offer(dbItem);
                    data.OfferDTO.OfferLineDTOs = new List<OfferLineDTO>();
                    data.OfferDTO.OfferLineSparepartDTOs = new List<OfferLineSparepartDTO>();
                    data.OfferDTO.OfferLineSampleProductDTOs = new List<OfferLineSampleProductDTO>();
                    data.ExchangeRateDTOs = new List<ExchangeRateDTO>();
                    data.ExchangeRateDTOs = converter.DB2DTO_ExchangeRate(context.OfferToClientMng_function_getExchangeRate().ToList());
                    data.PaymentTermDTOs = converter.DB2DTO_PaymentTermDTO(context.OfferToClientMng_PaymentTerm.ToList());
                    data.InterestPercentDTOs = converter.DB2DTO_InterestPercentDTO(context.OfferToClientMng_InterestPercent_View.ToList());
                    data.DeliveryTermDTOs = converter.DB2DTO_DeliveryTermDTO(context.OfferToClientMng_DeliveryTerm.ToList());
                    data.ForwarderDTOs = converter.DB2DTO_ForwarderDTO(context.OfferToClientMng_Forwarder_View.ToList());
                    data.PutInProductionTermDTOs = converter.DB2DTO_PutInProductionTermDTO(context.OfferToClientMng_PutInProductionTerm_View.ToList());
                    data.CurrencyDTOs = GetCurrency().ToList();
                    data.VATPercentDTOs = GetVATPercent().ToList();
                    data.LabelingTypeDTOs = GetLabelingType().ToList();
                    data.PackagingTypeDTOs = GetPackagingType().ToList();
                    data.ClientEstimatedAdditionalCostDTOs = converter.DB2DTO_ClientEstimatedAdditionalCostDTO(context.OfferToClientMng_ClientEstimatedAdditionalCost_View.Where(o => o.ClientID == data.OfferDTO.ClientID).ToList());
                    data.OfferDTO.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
                    data.OfferDTO.OfferVersion = 1;
                    data.OfferDTO.OfferDateFormated = DateTime.Now.ToString("dd/MM/yyyy");
                    OfferToClientMng_ClientEstimatedAdditionalCost_View offerToClientMng_ClientEstimatedAdditionalCost_View = context.OfferToClientMng_ClientEstimatedAdditionalCost_View.Where(s => s.ClientID == clientID && s.Season == season).FirstOrDefault();
                    if (offerToClientMng_ClientEstimatedAdditionalCost_View != null)
                        data.OfferDTO.DefaultCommissionPercent = offerToClientMng_ClientEstimatedAdditionalCost_View.DefaultCommissionPercent;

                    Client client = context.Client.Where(s => s.ClientID == clientID).FirstOrDefault();
                    data.OfferDTO.PaymentTermID = client.PaymentTermID;
                    data.OfferDTO.DeliveryTermID = client.DeliveryTermID;

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
                return new EditFormData();
            }
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (OfferToClientMngEntities context = CreateContext())
                {
                    EditFormData data = new EditFormData();
                    data.OfferDTO = new OfferDTO();
                    data.OfferDTO.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
                    data.OfferDTO.OfferVersion = 1;
                    data.OfferDTO.OfferDateFormated = DateTime.Now.ToString("dd/MM/yyyy");

                    if (id > 0)
                    {
                        OfferToClientMng_Offer_View dbItem;
                        dbItem = context.OfferToClientMng_Offer_View
                            .Include("OfferToClientMng_OfferLine_View")
                            .FirstOrDefault(o => o.OfferID == id);
                        if (dbItem == null)
                        {
                            dbItem = new OfferToClientMng_Offer_View();
                            dbItem.Remark = "Standard paper wrapped packaging for wicker items" + Environment.NewLine + "Standard box packaging for wooden items" + Environment.NewLine + "Maximum of 3 mix items per container";
                        }
                        data.OfferDTO = converter.DB2DTO_Offer(dbItem);                    
                        data.ExchangeRateDTOs = new List<ExchangeRateDTO>();
                        data.ExchangeRateDTOs = converter.DB2DTO_ExchangeRate(context.OfferToClientMng_function_getExchangeRate().ToList());
                        data.PaymentTermDTOs = converter.DB2DTO_PaymentTermDTO(context.OfferToClientMng_PaymentTerm.ToList());
                        data.InterestPercentDTOs = converter.DB2DTO_InterestPercentDTO(context.OfferToClientMng_InterestPercent_View.ToList());
                        data.DeliveryTermDTOs = converter.DB2DTO_DeliveryTermDTO(context.OfferToClientMng_DeliveryTerm.ToList());
                        data.ForwarderDTOs = converter.DB2DTO_ForwarderDTO(context.OfferToClientMng_Forwarder_View.ToList());
                        data.PutInProductionTermDTOs = converter.DB2DTO_PutInProductionTermDTO(context.OfferToClientMng_PutInProductionTerm_View.ToList());
                        data.CurrencyDTOs = GetCurrency().ToList();
                        data.VATPercentDTOs = GetVATPercent().ToList();
                        data.LabelingTypeDTOs = GetLabelingType().ToList();
                        data.PackagingTypeDTOs = GetPackagingType().ToList();                       
                        data.ClientEstimatedAdditionalCostDTOs = converter.DB2DTO_ClientEstimatedAdditionalCostDTO(context.OfferToClientMng_ClientEstimatedAdditionalCost_View.Where(o => o.ClientID == data.OfferDTO.ClientID).ToList());
                    }                   
                   
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
                return new EditFormData();
            }
        }

        //
        // CUSTOM FUNCTION HERE
        //
        public bool DeleteOffer(int deletedBy, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been deleted" };
            try
            {
                using (OfferToClientMngEntities context = CreateContext())
                {
                    //OfferStatus dbStatus = context.OfferStatus.Where(o => o.OfferID == id && o.IsCurrentStatus == true).FirstOrDefault();
                    //if (dbStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                    //{
                    //    throw new Exception("Offer was confirmed. You can not delete");
                    //}

                    //if (dbStatus.TrackingStatusID == DALBase.Helper.REVISED)
                    //{
                    //    throw new Exception("Offer was revised. You can not delete");
                    //}
                    int hasPI = context.SaleOrder.Where(o => o.OfferID == id).Count();

                    if (hasPI == 0)
                    {
                        context.OfferToClientMng_function_DeleteOffer(id, deletedBy);
                        return true;
                    }
                    else
                    {
                        //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "Offer has created PI, you can not delete" };
                        notification.Type = Library.DTO.NotificationType.Warning;
                        notification.Message = "Offer has created PI, you can not delete";
                        return false;
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
        public SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            SupportFormData data = new SupportFormData();
            data.Seasons = new List<SeasonDTO>();
            data.OfferSeasonTypes = new List<OfferSeasonTypeDTO>();
            data.Clients = new List<ClientDTO>();
            
            try
            {
                using (var context = CreateContext())
                {                    
                    data.Seasons = GetSeasons().ToList();
                    data.OfferSeasonTypes = converter.DB2DTO_OfferSeasonType(context.OfferToClientMng_OfferSeasonType_View.ToList());
                    data.Clients = converter.DB2DTO_Client(context.OfferToClientMng_Client_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public OfferSeasonDetailSearchFormData QuickSearchOfferLine(string articleCode, string description, int clientID, string season, string currency, int offerLineType, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferSeasonDetailSearchFormData data = new OfferSeasonDetailSearchFormData();
            int totalRows = 0;
            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.OfferToClientMng_function_QuickSearchOfferLine(articleCode, description, clientID, season, currency, offerLineType, orderBy, orderDirection).Count();
                    var result = context.OfferToClientMng_function_QuickSearchOfferLine(articleCode, description, clientID, season, currency, offerLineType, orderBy, orderDirection).ToList();
                    data.Data = converter.DB2DTO_OfferSeasonDetail(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        private IEnumerable<SeasonDTO> GetSeasons()
        {
            List<SeasonDTO> data = new List<SeasonDTO>();
            for (int i = 2005; i <= DateTime.Now.Year + 1; i++)
            {
                data.Add(new SeasonDTO() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            var result = data.OrderByDescending(f => f.SeasonValue);

            return result;
        }        
        private IEnumerable<CurrencyDTO> GetCurrency()
        {
            List<CurrencyDTO> result = new List<CurrencyDTO>();
            result.Add(new CurrencyDTO() { CurrencyValue = "USD", CurrencyText = "USD" });
            result.Add(new CurrencyDTO() { CurrencyValue = "EUR", CurrencyText = "EUR" });
            return result;
        }
        private List<VATPercentDTO> GetVATPercent()
        {
            List<VATPercentDTO> VATPercents = new List<VATPercentDTO>();

            VATPercents.Add(new VATPercentDTO { VATPercentValue = 0, VATPercentText = "0%" });
            VATPercents.Add(new VATPercentDTO { VATPercentValue = 21, VATPercentText = "21%" });
            return VATPercents;
        }
        private IEnumerable<LabelingTypeDTO> GetLabelingType()
        {
            List<LabelingTypeDTO> result = new List<LabelingTypeDTO>();
            result.Add(new LabelingTypeDTO() { LabelingTypeValue = "ST", LabelingTypeText = "STANDARD" });
            result.Add(new LabelingTypeDTO() { LabelingTypeValue = "SP", LabelingTypeText = "SPECIAL" });
            return result;
        }

        private IEnumerable<PackagingTypeDTO> GetPackagingType()
        {
            List<PackagingTypeDTO> result = new List<PackagingTypeDTO>();
            result.Add(new PackagingTypeDTO() { PackagingTypeValue = "ST", PackagingTypeText = "STANDARD" });
            result.Add(new PackagingTypeDTO() { PackagingTypeValue = "SP", PackagingTypeText = "SPECIAL" });
            return result;
        }

        public string GetPrintDataOffer5(int OfferID, bool IsSendEmail, bool IsGetFullSizeImage, int imageOption, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferToClientMngDataSet ds = new OfferToClientMngDataSet();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferToClientMng_function_GetPrintData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", OfferID);
                adap.SelectCommand.Parameters.AddWithValue("@IsSendEmail", IsSendEmail);
                adap.SelectCommand.Parameters.AddWithValue("@ImageOption", imageOption);

                adap.TableMappings.Add("Table", "ParamTable");
                adap.TableMappings.Add("Table1", "PrintHeader");
                adap.TableMappings.Add("Table2", "PrintDetail");
                adap.TableMappings.Add("Table3", "ModelPieceReport");
                adap.Fill(ds);

                //logo image
                ds.PrintHeader.FirstOrDefault().LogoImage = FrameworkSetting.Setting.MediaThumbnailUrl + ds.PrintHeader.FirstOrDefault().LogoImage;

                //set image url
                foreach (var item in ds.PrintDetail)
                {
                    if (IsGetFullSizeImage)
                    {
                        item.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.FileLocation_FullSize;
                        item.FileGardenLocation = FrameworkSetting.Setting.MediaFullSizeUrl + item.FileGardenLocation_FullSize;
                    }
                    else
                    {
                        item.FileLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileLocation;
                        item.FileGardenLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileGardenLocation;
                    }
                }

                //DALBase.Helper.DevCreateReportXMLSource(ds, "Offer5");
                // generate xml file
                string reportFile = "";
                Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);
                switch (companyID)
                {
                    case 13:
                        reportFile = "Offer5_OrangePine";
                        break;
                    default:
                        reportFile = "Offer5";
                        break;
                }

                // remove table from dataset
                ds.Tables.Remove("OfferMng_OfferPrintoutPP_View");
                ds.Tables.Remove("OfferMng_OfferPrintoutPP_Detail_View");
                ds.Tables.Remove("OfferMng_OfferPrintoutPP_PieceDetail_View");
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, reportFile);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return string.Empty;
            }
        }

        public string GetPrintDataOfferPP2013(int OfferID, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferToClientMngDataSet ds = new OfferToClientMngDataSet();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferToClientMng_function_GetPPPrintData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", OfferID);

                adap.TableMappings.Add("Table", "OfferMng_OfferPrintoutPP_View");
                adap.TableMappings.Add("Table1", "OfferMng_OfferPrintoutPP_Detail_View");
                adap.TableMappings.Add("Table2", "OfferMng_OfferPrintoutPP_PieceDetail_View");
                adap.Fill(ds);

                //
                // TO DO LIST
                //
                return (new Office2013Helper.PowerpointController()).ExportOffer(ds);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }

        public string GetExportNewVersion(int offerID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            OfferToClientMng2DataSet ds = new OfferToClientMng2DataSet();

            string pathFile = string.Empty;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferToClientMng_function_GetPrint1448Data", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@offerID", offerID);

                adap.TableMappings.Add("Table", "OfferData");
                adap.TableMappings.Add("Table1", "OfferDetailData");
                adap.TableMappings.Add("Table2", "OfferSubDetailData");
                adap.Fill(ds);

                if (ds.OfferData != null)
                {
                    if (!string.IsNullOrEmpty(ds.OfferData.FirstOrDefault().ThumbnailLocation))
                    {
                        if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + ds.OfferData.FirstOrDefault().ThumbnailLocation))
                        {
                            ds.OfferData.FirstOrDefault().ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + ds.OfferData.FirstOrDefault().ThumbnailLocation;
                        }
                        else
                        {
                            ds.OfferData.FirstOrDefault().ThumbnailLocation = "NONE";
                        }
                    }
                }

                if (ds.OfferDetailData != null)
                {
                    foreach (var item in ds.OfferDetailData)
                    {
                        if (item != null)
                        {
                            if (!string.IsNullOrEmpty(item.ThumbnailLocation))
                            {
                                if (System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation))
                                {
                                    item.ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + item.ThumbnailLocation;
                                }
                                else
                                {
                                    item.ThumbnailLocation = "NONE";
                                }
                            }
                        }
                    }
                }

                pathFile = Library.Helper.CreateReportFileWithEPPlus2(ds, "OfferReport_1448");

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return pathFile;
        }

        public string GetExcelFOBItemOnlyReportData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferToClientMng3DataSet ds = new OfferToClientMng3DataSet();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferToClientMng__function_FOBItemOnlyReportView", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", id);
                adap.TableMappings.Add("Table", "FOBItemOnly");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "Offer_FOBItemOnly");
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
        public bool UpdateClientInfomation(int offerID, out Library.DTO.Notification notification)
        {           
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (OfferToClientMngEntities context = CreateContext())
                {                   
                    if (offerID > 0)
                    {
                        OfferToClientMng_Offer_View dbItem;
                        dbItem = context.OfferToClientMng_Offer_View
                            .Include("OfferToClientMng_OfferLine_View")
                            .Include("OfferToClientMng_OfferLineSparepart_View")
                            .Include("OfferToClientMng_OfferLineSampleProduct_View")
                            .FirstOrDefault(o => o.OfferID == offerID);
                        // Product
                        if (dbItem.OfferToClientMng_OfferLine_View != null)
                        {
                            foreach (var item in dbItem.OfferToClientMng_OfferLine_View)
                            {
                                var lstSaleOrder = context.SaleOrderDetail.Where(s => s.OfferLineID == item.OfferLineID).ToList();
                                foreach (var itemSaleOrderDetail in lstSaleOrder)
                                {
                                    var entity = context.SaleOrderDetail.Where(s => s.SaleOrderDetailID == itemSaleOrderDetail.SaleOrderDetailID).FirstOrDefault();
                                    entity.ClientArticleCode = string.IsNullOrEmpty(item.ClientArticleCode) ? entity.ClientArticleCode : item.ClientArticleCode;
                                    entity.ClientArticleName = string.IsNullOrEmpty(item.ClientArticleName) ? entity.ClientArticleName : item.ClientArticleName;
                                    entity.ClientEANCode = string.IsNullOrEmpty(item.ClientEANCode) ? entity.ClientEANCode : item.ClientEANCode;

                                    context.SaveChanges();
                                }
                            }
                        }
                        // Saprepart
                        if(dbItem.OfferToClientMng_OfferLineSparepart_View != null)
                        {
                            foreach (var item in dbItem.OfferToClientMng_OfferLineSparepart_View)
                            {
                                var lstSaleOrder = context.SaleOrderDetailSparepart.Where(s => s.OfferLineSparePartID == item.OfferLineSparePartID).ToList();
                                foreach (var itemSaleOrderDetailSparepart in lstSaleOrder)
                                {
                                    var entity = context.SaleOrderDetailSparepart.Where(s => s.SaleOrderDetailSparepartID == itemSaleOrderDetailSparepart.SaleOrderDetailSparepartID).FirstOrDefault();
                                    entity.ClientArticleCode = string.IsNullOrEmpty(item.ClientArticleCode) ? entity.ClientArticleCode : item.ClientArticleCode;
                                    entity.ClientArticleName = string.IsNullOrEmpty(item.ClientArticleName) ? entity.ClientArticleName : item.ClientArticleName;
                                    entity.ClientEANCode = string.IsNullOrEmpty(item.ClientEANCode) ? entity.ClientEANCode : item.ClientEANCode;

                                    context.SaveChanges();
                                }
                            }
                        }
                        // Sample
                        if (dbItem.OfferToClientMng_OfferLineSampleProduct_View != null)
                        {
                            foreach (var item in dbItem.OfferToClientMng_OfferLineSampleProduct_View)
                            {
                                var lstSaleOrder = context.SaleOrderDetailSample.Where(s => s.OfferLineSampleProductID == item.OfferLineSampleProductID).ToList();
                                foreach (var itemSaleOrderDetailSample in lstSaleOrder)
                                {
                                    var entity = context.SaleOrderDetailSample.Where(s => s.SaleOrderDetailSampleID == itemSaleOrderDetailSample.SaleOrderDetailSampleID).FirstOrDefault();
                                    entity.ClientArticleCode = string.IsNullOrEmpty(item.ClientArticleCode) ? entity.ClientArticleCode : item.ClientArticleCode;
                                    entity.ClientArticleName = string.IsNullOrEmpty(item.ClientArticleName) ? entity.ClientArticleName : item.ClientArticleName;
                                    entity.ClientEANCode = string.IsNullOrEmpty(item.ClientEANCode) ? entity.ClientEANCode : item.ClientEANCode;

                                    context.SaveChanges();
                                }
                            }
                        }

                        return true;
                    }
                    else
                    {
                        notification.Message = "Offer not found!";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return false;
        }

    }
}
