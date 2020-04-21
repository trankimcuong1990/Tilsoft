using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.OMSHelper;
using System.IO;
using DALBase;
using System.Collections;
using Library;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DAL.OfferMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.OfferMng.OfferSearchResult, DTO.OfferMng.Offer>
    {
        private DataConverter converter = new DataConverter();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory()
        {
        }

        public DataFactory(string tempFolder)
        {
            _tempFolder = tempFolder + @"\";
        }

        private OfferMngEntities CreateContext()
        {
            OfferMngEntities context = new OfferMngEntities(DALBase.Helper.CreateEntityConnectionString("OfferMngModel"));
            context.Database.CommandTimeout = 1200;
            return context;
        }

        public override IEnumerable<DTO.OfferMng.OfferSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (OfferMngEntities context = CreateContext())
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
                    bool? IsNeedApproval = null;
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

                    if (filters.ContainsKey("IsNeedApproval") && filters["IsNeedApproval"] != null && !string.IsNullOrEmpty(filters["IsNeedApproval"].ToString()))
                    {
                        IsNeedApproval = filters["IsNeedApproval"].ToString().ConvertStringToBool();
                    }

                    if (filters.ContainsKey("SaleID") && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
                    {
                        saleID = Convert.ToInt32(filters["SaleID"].ToString().Replace("'", "''"));
                    }

                    //totalRows = context.OfferMng_function_SearchOffer(offerUD, season, clientUD, clientNM, paymentTermNM, deliveryTermNM, currency, forwarderNM, podNM, articleCode, description, v4id, offerStatus, isPotential, orderBy, orderDirection).Count();
                    var result = context.OfferMng_function_SearchOffer(offerUD, season, clientUD, clientNM, paymentTermNM, deliveryTermNM, currency, forwarderNM, podNM, articleCode, description, v4id, offerStatus, isPotential, saleID, IsNeedApproval, orderBy, orderDirection).ToList();
                    totalRows = result.Count();

                    return converter.DB2DTO_OfferSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.OfferMng.OfferSearchResult>();
            }
        }

        public override DTO.OfferMng.Offer GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    OfferMng_Offer_View dbItem;
                    dbItem = context.OfferMng_Offer_View
                        .Include("OfferMng_OfferLine_View")
                        .FirstOrDefault(o => o.OfferID == id);

                    if (id == 0)
                    {
                        dbItem.Remark = "Standard paper wrapped packaging for wicker items" + Environment.NewLine + "Standard box packaging for wooden items" + Environment.NewLine + "Maximum of 3 mix items per container";
                    }

                    return converter.DB2DTO_Offer(dbItem);
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
                return new DTO.OfferMng.Offer();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int id, ref DTO.OfferMng.Offer dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.OfferMng.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory factory = new Support.DataFactory();

            //try to get data
            try
            {
                DTO.OfferMng.SupportDataContainer dtoItem = new DTO.OfferMng.SupportDataContainer();
                dtoItem.Seasons = factory.GetSeason().ToList();
                using (OfferMngEntities context = CreateContext())
                {
                    dtoItem.offerStatusDTOs = converter.DB2DTO_OfferStatus(context.List_TrackingStatus.ToList());
                    dtoItem.Sales = factory.GetSaler().Where(o => o.UserID > 0).ToList();
                }
                return dtoItem;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.OfferMng.SupportDataContainer();
            }
        }

        private IEnumerable<DTO.OfferMng.RatePercent> GetCommision()
        {
            List<DTO.OfferMng.RatePercent> Commissions = new List<DTO.OfferMng.RatePercent>();

            Commissions.Add(new DTO.OfferMng.RatePercent { RatePercentValue = 8, RatePercentText = "8%" });
            Commissions.Add(new DTO.OfferMng.RatePercent { RatePercentValue = 15, RatePercentText = "15%" });

            return Commissions;
        }

        public DTO.OfferMng.DataContainer GetDataContainer(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferMng.DataContainer dtoItem = new DTO.OfferMng.DataContainer();

            //try to get data
            try
            {
                DAL.Support.DataFactory factory = new Support.DataFactory();
                List<DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO> listFactory = new List<DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO>();                
                List<FactoryMng_FactoryPermission_View> ListFactory = new List<FactoryMng_FactoryPermission_View>();
                dtoItem.SupportData = new DTO.OfferMng.SupportDataContainer();
                dtoItem.SupportData.ExchangeRateDTOs = new List<DTO.OfferMng.ExchangeRateDTO>();

                using (OfferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        OfferMng_Offer_View dbItem = context.OfferMng_function_GetOffer(id).FirstOrDefault();

                        //OfferMng_Offer_View dbItem = context.OfferMng_function_GetOffer(id).FirstOrDefault();
                        //OfferMng_Offer_View dbItem = context.OfferMng_function_GetOffer(id).FirstOrDefault();
                        //dbItem = context.OfferMng_Offer_View
                        //    .Include("OfferMng_OfferLine_View")
                        //    .Include("OfferMng_OfferLine_View.OfferMng_OfferLinePriceOption_View")
                        //    .Include("OfferMng_OfferLineSparepart_View")
                        //    .Include("OfferMng_OfferLineSampleProduct_View")
                        //    .FirstOrDefault(o => o.OfferID == id)
                        //    ;

                        dtoItem.OfferData = converter.DB2DTO_Offer(dbItem);
                    }
                    else
                    {
                        dtoItem.OfferData = new DTO.OfferMng.Offer { IsPotential = true };
                        dtoItem.OfferData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
                        dtoItem.OfferData.OfferVersion = 1;
                        dtoItem.OfferData.Season = Library.Helper.GetCurrentSeason();
                        dtoItem.OfferData.OfferDateFormated = DateTime.Now.ToString("dd/MM/yyy");
                        dtoItem.OfferData.Currency = "USD";

                        dtoItem.OfferData.OfferLines = new List<DTO.OfferMng.OfferLine>();
                        dtoItem.OfferData.OfferLineSpareparts = new List<DTO.OfferMng.OfferLineSparepart>();
                        dtoItem.OfferData.OfferLineSampleProductDTOs = new List<DTO.OfferMng.OfferLineSampleProductDTO>();

                        dtoItem.OfferData.Remark = "Standard paper wrapped packaging for wicker items" + Environment.NewLine + "Standard box packaging for wooden items" + Environment.NewLine + "Maximum of 3 mix items per container";
                    }

                    //purchasing price source
                    dtoItem.SupportData.PlaningPurchasingPriceSourceDTOs = converter.DB2DTO_PlaningPurchasingPriceSourceDTO(context.SupportMng_PlaningPurchasingPriceSource_View.OrderBy(o => o.DisplayOrder).ToList());

                    //exchange rate
                    dtoItem.SupportData.ExchangeRateDTOs = converter.DB2DTO_ExchangeRateDTO(context.OfferMng_function_getExchangeRate().ToList());

                    //est client cost
                    var estimatedAdditionalCost = context.OfferMng_ClientEstimatedAdditionalCost_View.Where(o => o.ClientID == dtoItem.OfferData.ClientID).ToList();
                    dtoItem.SupportData.ClientEstimatedAdditionalCostDTOs = AutoMapper.Mapper.Map<List<OfferMng_ClientEstimatedAdditionalCost_View>, List<DTO.OfferMng.ClientEstimatedAdditionalCostDTO>>(estimatedAdditionalCost);

                    // special permission
                    Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                    dtoItem.OfferData.IsActiveApproveOffer = fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER);
                }

                //GET SUPPORT DATA                
                dtoItem.SupportData.Seasons = factory.GetSeason().ToList();
                dtoItem.SupportData.Sales = factory.GetSaler().ToList();
                dtoItem.SupportData.PaymentTerms = factory.GetPaymentTerm().ToList();
                dtoItem.SupportData.DeliveryTerms = factory.GetDeliveryTerm().ToList();
                dtoItem.SupportData.PODs = factory.GetPOD().ToList();
                dtoItem.SupportData.Currency = factory.GetCurrency().ToList();
                dtoItem.SupportData.VATPercents = factory.GetVATPercent();
                dtoItem.SupportData.LabelingTypes = factory.GetLabelingType().ToList();
                dtoItem.SupportData.PackagingTypes = factory.GetPackagingType().ToList();
                dtoItem.SupportData.Forwarders = factory.GetForwarder().ToList();
                dtoItem.SupportData.PutInProductionTerms = factory.GetPutInProductionTerm().ToList();
                dtoItem.FactoryDTOs = GetFactoryPermission(userId).OrderBy(o=>o.FactoryUD).ToList();

                //OFFER LINE SUPPORT
                dtoItem.SupportData.POLs = factory.GetPOL().ToList();
                dtoItem.SupportData.CommissionPercents = GetCommision().ToList();
                //dtoItem.SupportData.PackagingMethods = factory.GetPackagingMethod().ToList();
                //dtoItem.SupportData.ManufacturerCountrys = factory.GetManufacturerCountry().ToList();
                //dtoItem.SupportData.FrameMaterials = factory.GetFrameMaterial().ToList();
                dtoItem.SupportData.ModelStatuses = factory.GetModelStatus().ToList();
                dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -1, ModelStatusNM = "TOTAL" });
                dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -2, ModelStatusNM = "WAREHOUSE" });
                dtoItem.SupportData.ProductElements = factory.GetProductElement();

                return dtoItem;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return dtoItem;
        }

        public List<DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO> GetFactoryPermission(int userId)
        {
            OfferMngEntities context = CreateContext();
            var result = context.FactoryMng_Function_GetFactoryPermission(userID: userId).ToList();
            List<DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO> dtoListFactory = new List<DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO>();
           
            foreach (var item in result)
            {
                var item2 = new DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO();
                item2.FactoryName = item.FactoryName;
                item2.FactoryUD = item.FactoryUD;
                item2.FactoryID = item.FactoryID;
                dtoListFactory.Add(item2);
            }
            return dtoListFactory;
        }

        public IEnumerable<DTO.OfferMng.OfferLine> SearchOfferLines(int offerID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    totalRows = context.OfferMng_OfferLine_View.Where(o => o.OfferID == offerID).Count();
                    var result = context.OfferMng_OfferLine_View.Where(o => o.OfferID == offerID);
                    return converter.DB2DTO_OfferLine(result.ToList());
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
                return new List<DTO.OfferMng.OfferLine>();
            }
        }

        private void SetOfferStatus(int? offerID, int trackingStatusID, int setStatusBy)
        {
            using (OfferMngEntities context = CreateContext())
            {
                foreach (OfferStatus item in context.OfferStatus.Where(o => o.OfferID == offerID))
                {
                    item.IsCurrentStatus = false;
                }

                OfferStatus dbStatus = new OfferStatus();
                dbStatus.OfferID = offerID;
                dbStatus.TrackingStatusID = trackingStatusID;
                dbStatus.StatusDate = DateTime.Now;
                dbStatus.UpdatedBy = setStatusBy;
                dbStatus.IsCurrentStatus = true;
                context.OfferStatus.Add(dbStatus);

                context.SaveChanges();

                // add item to quotation if needed
                //context.FW_Quotation_function_AddFactoryOrderItem(offerID, null, null); // table lockx and also check if item is available on sql server side
            }
        }

        public bool Confirm(int offerID, int actionType, ref DTO.OfferMng.Offer dtoItem, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been confirmed success" };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    foreach (var item in dtoItem.OfferLines)
                    {
                        if (item.ModelID == null || item.MaterialID == null || item.MaterialTypeID == null || item.MaterialColorID == null
                            || item.FrameMaterialID == null || item.FrameMaterialColorID == null
                            || item.SubMaterialID == null || item.SubMaterialColorID == null
                            || item.SeatCushionID == null || item.BackCushionID == null || item.CushionColorID == null
                            || item.FSCTypeID == null || item.FSCPercentID == null)
                        {
                            notification = new Library.DTO.Notification() { Message = "Product : " + item.ArticleCode + " - " + item.Description + "<b> must be fill-in all attribute before confirm</b>", Type = Library.DTO.NotificationType.Warning };
                            return false;
                        }
                    }

                    foreach (var item in dtoItem.OfferLineSpareparts)
                    {
                        if (item.ModelID == null || item.PartID == null)
                        {
                            notification = new Library.DTO.Notification() { Message = "Sparepart : " + item.ArticleCode + " - " + item.Description + "<b> must be fill-in all attribute before confirm</b>", Type = Library.DTO.NotificationType.Warning };
                            return false;
                        }
                    }
                    //validate data
                    if (offerID == 0 || actionType == 0 || actionType == 2 || actionType == 3) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        notification = new Library.DTO.Notification() { Message = "You have to save data before confirm offer", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    OfferStatus dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == offerID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbOfferStatus.TrackingStatusID != DALBase.Helper.REVISED && dbOfferStatus.TrackingStatusID != DALBase.Helper.CREATED)
                    {
                        notification = new Library.DTO.Notification() { Message = "Offer must be in REVISED/CREATED status before confirm", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    //save data
                    Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
                    if (!UpdateData(offerID, actionType, setStatusBy, ref dtoItem, out save_nofication))
                    {
                        notification = save_nofication;
                        return false;
                    }

                    //set tracking status
                    int TrackingStatusID = (dbOfferStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);
                    SetOfferStatus(offerID, TrackingStatusID, setStatusBy);
                    dtoItem.TrackingStatusNM = (dbOfferStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.TEXT_STATUS_CONFIRMED : DALBase.Helper.TEXT_STATUS_REVISION_CONFIRMED);
                    dtoItem.TrackingStatusID = TrackingStatusID;

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

        public bool Revise(int offerID, int actionType, ref DTO.OfferMng.Offer dtoItem, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been revised success" };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    //validate
                    if (offerID == 0 || actionType == 0 || actionType == 2 || actionType == 3) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        notification = new Library.DTO.Notification() { Message = "You have to save data before revise offer", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    //check sale order status
                    if (context.OfferMng_function_GetConfirmedSaleOrder(offerID).Count() > 0)
                    {
                        throw new Exception("At least 1 P/I of this offer were confirmed. You can not revise offer. In this case you need to contact with user who make PI to revise PI before revise offer.");
                    }

                    OfferStatus dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == offerID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbOfferStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbOfferStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                    {
                        notification = new Library.DTO.Notification() { Message = "Offer must be in CONFIRMED status before revise", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    //set tracking status
                    SetOfferStatus(offerID, DALBase.Helper.REVISED, setStatusBy);
                    dtoItem.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_REVISED;
                    dtoItem.TrackingStatusID = DALBase.Helper.REVISED;

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

        public bool UpdateData(int id, int actionType, int userId, ref DTO.OfferMng.Offer dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    Offer dbItem = null;
                    OfferStatus dbOfferStatus = null;

                    if (actionType == 0 || actionType == 2 || actionType == 3) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        dbItem = new Offer();
                        context.Offer.Add(dbItem);

                        //SET OFFER STATUS
                        dbOfferStatus = new OfferStatus();
                        dbOfferStatus.TrackingStatusID = DALBase.Helper.CREATED;
                        dbOfferStatus.StatusDate = DateTime.Now;
                        dbOfferStatus.UpdatedBy = dtoItem.UpdatedBy;
                        dbOfferStatus.IsCurrentStatus = true;
                        dbItem.OfferStatus.Add(dbOfferStatus);

                        //SET TRACKING INFO
                        //dbItem.CreatedBy = dtoItem.UpdatedBy;
                        //dbItem.CreatedDate = DateTime.Now;

                        //dtoItem.CreatedBy = dtoItem.UpdatedBy;
                        //dtoItem.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                        switch (actionType)
                        {
                            case 0: //NEW OFFER
                                dtoItem.OfferVersion = 1;
                                dtoItem.OfferUD = context.OfferMng_function_GenerateOfferCode(dtoItem.OfferID, dtoItem.Season, dtoItem.SaleID, dtoItem.ClientID).FirstOrDefault();
                                break;
                            case 2: // COPY OFFER
                                dtoItem.OfferVersion = 1;
                                dtoItem.OfferUD = context.OfferMng_function_GenerateOfferCode(dtoItem.OfferID, dtoItem.Season, dtoItem.SaleID, dtoItem.ClientID).FirstOrDefault();
                                break;
                            case 3: //NEW VERSION OFFER
                                dtoItem.OfferVersion = dtoItem.OfferVersion + 1;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (id > 0)
                    {
                        //get db item
                        dbItem = context.Offer.FirstOrDefault(o => o.OfferID == id);
                        if (dbItem.ClientID != dtoItem.ClientID || dbItem.SaleID != dtoItem.SaleID || dbItem.Season != dtoItem.Season)
                        {
                            dtoItem.OfferUD = context.OfferMng_function_GenerateOfferCode(dtoItem.OfferID, dtoItem.Season, dtoItem.SaleID, dtoItem.ClientID).FirstOrDefault();
                        }

                        //get db offer status
                        dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == id && o.IsCurrentStatus == true).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Offer not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //offer status
                        //if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CHANGE_OFFER_ITEM_OPTION))
                        //{
                        //    if (dbOfferStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbOfferStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                        //    {
                        //        throw new Exception("Offer was confirmed, you can not change. In case you have to revise offer before save change.");
                        //    }
                        //}
                        //dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        //dbItem.UpdatedDate = DateTime.Now;

                        //read dto to db
                        converter.DTO2DB_Offer(dtoItem, ref dbItem, actionType, userId);

                        if (id == 0)
                        {
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                        }

                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        // check if sale price has changed
                        string objectName = "";
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

                        // auto set quantity = 1
                        //dbItem.OfferLine.Where(o => !o.Quantity.HasValue || o.Quantity == 0).ToList().ForEach(o => o.Quantity = 1);                        

                        //create article and description
                        foreach (var item in dbItem.OfferLine)
                        {
                            item.ArticleCode = context.OfferMng_function_GenerateArticleCode(item.ModelID, item.FrameMaterialID, item.FrameMaterialColorID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.SubMaterialID, item.SubMaterialColorID, item.SeatCushionID, item.BackCushionID, item.CushionColorID, item.ManufacturerCountryID, item.FSCTypeID, item.FSCPercentID).FirstOrDefault();
                            item.Description = context.OfferMng_function_GenerateDescription(item.ModelID, item.FrameMaterialID, item.FrameMaterialColorID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.SubMaterialID, item.SubMaterialColorID, item.SeatCushionID, item.BackCushionID, item.CushionColorID, item.CushionRemark, item.FSCTypeID, item.FSCPercentID).FirstOrDefault();
                        }

                        //create article and description for sparepart
                        //dbItem.OfferLineSparepart.Where(o => !o.Quantity.HasValue || o.Quantity == 0).ToList().ForEach(o => o.Quantity = 1);
                        foreach (var item in dbItem.OfferLineSparepart)
                        {
                            item.ArticleCode = context.OfferMng_function_GenerateSparepartArticleCode(item.ModelID, item.PartID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.CushionID, item.CushionColorID, item.CountryID).FirstOrDefault();
                            item.Description = context.OfferMng_function_GenerateSparepartDescription(item.ModelID, item.PartID, item.MaterialID, item.MaterialTypeID, item.MaterialColorID, item.CushionID, item.CushionColorID).FirstOrDefault();
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
                            dtoItem = new DTO.OfferMng.Offer { OfferID = id };
                        }
                        else
                        {
                            dtoItem = new DTO.OfferMng.Offer { OfferID = dbItem.OfferID };
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

        public bool DeleteOffer(int id, int deletedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been deleted" };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    OfferStatus dbStatus = context.OfferStatus.Where(o => o.OfferID == id && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                    {
                        throw new Exception("Offer was confirmed. You can not delete");
                    }

                    if (dbStatus.TrackingStatusID == DALBase.Helper.REVISED)
                    {
                        throw new Exception("Offer was revised. You can not delete");
                    }

                    context.OfferMng_function_DeleteOffer(id, deletedBy);
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

        public DTO.OfferMng.OfferLineEdit GetOfferLineEdit(int offerLineID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory supportFactory = new Support.DataFactory();
            //try to get data
            try
            {
                DTO.OfferMng.OfferLineEdit data = new DTO.OfferMng.OfferLineEdit();
                if (offerLineID > 0)
                {
                    using (OfferMngEntities context = CreateContext())
                    {
                        OfferMng_OfferLine_EditView dbItem = context.OfferMng_OfferLine_EditView.Where(o => o.OfferLineID == offerLineID).FirstOrDefault();
                        data = converter.DB2DTO_OfferLineEdit(dbItem);
                    }
                }
                else
                {
                    data.MaterialThumbnailLocation = "no-image.jpg";
                    data.FrameMaterialThumbnailLocation = "no-image.jpg";
                    data.SubMaterialColorThumbnailLocation = "no-image.jpg";
                    data.CushionColorThumbnailLocation = "no-image.jpg";
                    data.BackCushionThumbnailLocation = "no-image.jpg";
                    data.SeatCushionThumbnailLocation = "no-image.jpg";
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.OfferMng.OfferLineEdit();
            }
        }

        public List<DTO.Support.ModelSparepart> GetModelSparepart(int modelID)
        {
            DAL.Support.DataFactory supportFactory = new Support.DataFactory();
            return supportFactory.GetModelSparepart(modelID).ToList();
        }

        public bool UploadOfferScanFile(int offerID, bool offerScanFileHasChange, string newOfferScanFile, string offerScanFile, out DTO.OfferMng.Offer dtoOffer, out Library.DTO.Notification notification)
        {
            dtoOffer = new DTO.OfferMng.Offer();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = (!string.IsNullOrEmpty(newOfferScanFile) ? "Upload offer scan file success" : "Remove offer scan file success") };
            try
            {
                //file processing
                Library.FileHelper.FileManager fileMng = new Library.FileHelper.FileManager(FrameworkSetting.Setting.AbsoluteFileFolder);
                string fileNeedDeleted = string.Empty;
                string thumbnailFileNeedDeleted = string.Empty;

                if (offerScanFileHasChange)
                {
                    //check to delete file does not exist in database
                    if (!string.IsNullOrEmpty(offerScanFile))
                    {
                        fwFactory.GetDBFileLocation(offerScanFile, out fileNeedDeleted, out thumbnailFileNeedDeleted);
                        if (!string.IsNullOrEmpty(fileNeedDeleted))
                        {
                            try
                            {
                                fileMng.DeleteFile(fileNeedDeleted);
                            }
                            catch { }
                        }
                    }
                    if (string.IsNullOrEmpty(newOfferScanFile))
                    {
                        // remove file registration in File table
                        fwFactory.RemoveFile(offerScanFile);
                        // reset file in table Contract
                        offerScanFile = string.Empty;
                    }
                    else
                    {
                        string outDBFileLocation = "";
                        string outFileFullPath = "";
                        string outFilePointer = "";
                        // copy new file
                        fileMng.StoreFile(_tempFolder + newOfferScanFile, out outDBFileLocation, out outFileFullPath);

                        if (File.Exists(outFileFullPath))
                        {
                            FileInfo info = new FileInfo(outFileFullPath);
                            // insert/update file registration in database
                            fwFactory.UpdateFile(offerScanFile, newOfferScanFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);
                            // set file database pointer
                            offerScanFile = outFilePointer;
                        }
                    }

                    //Update back new file to offer
                    using (OfferMngEntities context = CreateContext())
                    {
                        Offer dbItem = context.Offer.Where(o => o.OfferID == offerID).FirstOrDefault();
                        dbItem.OfferScanFile = offerScanFile;
                        context.SaveChanges();
                        dtoOffer = GetData(offerID, out notification);
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

        public DTO.OfferMng.Model GetModelInfo(int modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory supportFactory = new Support.DataFactory();
            try
            {
                DTO.OfferMng.Model data = new DTO.OfferMng.Model();
                using (OfferMngEntities context = CreateContext())
                {
                    OfferMng_Model_View dbItem = context.OfferMng_Model_View.Where(o => o.ModelID == modelID).FirstOrDefault();
                    data = converter.DB2DTO_Model(dbItem);
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
                return new DTO.OfferMng.Model();
            }
        }

        public DTO.OfferMng.DataContainerOverView GetViewData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                DAL.Support.DataFactory factory = new Support.DataFactory();
                DTO.OfferMng.DataContainerOverView dtoItem = new DTO.OfferMng.DataContainerOverView();
                using (OfferMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        OfferMng_Offer_OverView dbItem;
                        dbItem = context.OfferMng_Offer_OverView
                            .Include("OfferMng_OfferLine_OverView.OfferMng_OfferLinePriceOption_OverView")
                            .Include("OfferMng_OfferLineSparepart_OverView")
                            .FirstOrDefault(o => o.OfferID == id)
                            ;
                        dtoItem.OfferData = converter.DB2DTO_OfferOverView(dbItem);
                    }
                    else
                    {
                        dtoItem.OfferData = new DTO.OfferMng.OfferOverView();
                        dtoItem.OfferData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
                        dtoItem.OfferData.OfferVersion = 1;
                        dtoItem.OfferData.Season = Library.Helper.GetCurrentSeason();
                        dtoItem.OfferData.OfferDateFormated = DateTime.Now.ToString("dd/MM/yyy");
                        dtoItem.OfferData.Currency = "USD";

                        dtoItem.OfferData.OfferLines = new List<DTO.OfferMng.OfferlineOverView>();
                        dtoItem.OfferData.OfferLineSpareparts = new List<DTO.OfferMng.OfferLineSparepartOverView>();
                    }
                }

                //OFFER LINE SUPPORT
                //dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -1, ModelStatusNM = "TOTAL" });
                //dtoItem.SupportData.ModelStatuses.Add(new DTO.Support.ModelStatus { ModelStatusID = -2, ModelStatusNM = "WAREHOUSE" });
                return dtoItem;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.OfferMng.DataContainerOverView();
            }
        }

        public IEnumerable<DTO.OfferMng.SampleProductSearchResultDTO> QuickSearchSampleProduct(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.OfferMng.SampleProductSearchResultDTO> data = new List<DTO.OfferMng.SampleProductSearchResultDTO>();

            //try to get data
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    string SampleProductUD = null;
                    string ArticleDescription = null;
                    string SampleOrderUD = null;
                    int ClientID = 0;

                    if (filters.ContainsKey("SearchQuery") && !string.IsNullOrEmpty(filters["SearchQuery"].ToString()))
                    {
                        SampleProductUD = filters["SearchQuery"].ToString().Replace("'", "''");
                        ArticleDescription = SampleProductUD;
                        SampleOrderUD = SampleProductUD;
                    }

                    if (filters.ContainsKey("ClientID"))
                    {
                        ClientID = Convert.ToInt32(filters["ClientID"]);
                    }

                    var result = context.OfferMng_function_SearchSampleProduct(SampleProductUD, ArticleDescription, SampleOrderUD, ClientID, orderBy, orderDirection).ToList();
                    totalRows = result.Count();
                    data = converter.DB2DTO_SampleProductSearchResultDTO(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceDTO> GetPlaningPurchasingPrice(int userId, int offerLineID, out Library.DTO.Notification notification)
        {
            List<DTO.OfferMng.PlaningPurchasingPriceDTO> Data = new List<DTO.OfferMng.PlaningPurchasingPriceDTO>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    Data = converter.DB2DTO_PlaningPurchasingPriceDTO(context.OfferMng_function_GetOfferLinePlaningPurchasingPrice(offerLineID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return Data;
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceDTO> GetPlaningPurchasingPrice(int userId, object param, out Library.DTO.Notification notification)
        {
            DTO.OfferMng.PlanningPurchasingPriceParamDTO inputParam = ((Newtonsoft.Json.Linq.JObject)param).ToObject<DTO.OfferMng.PlanningPurchasingPriceParamDTO>();
            List<DTO.OfferMng.PlaningPurchasingPriceDTO> Data = new List<DTO.OfferMng.PlaningPurchasingPriceDTO>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    Data = converter.DB2DTO_PlaningPurchasingPriceDTO(
                        context.OfferMng_function_GetPlaningPurchasingPrice(
                            inputParam.ClientID,
                            inputParam.ModelID,
                            inputParam.MaterialID,
                            inputParam.MaterialTypeID,
                            inputParam.MaterialColorID,
                            inputParam.FrameMaterialID,
                            inputParam.FrameMaterialColorID,
                            inputParam.SubMaterialID,
                            inputParam.SubMaterialColorID,
                            inputParam.BackCushionID,
                            inputParam.SeatCushionID,
                            inputParam.CushionColorID,
                            inputParam.FSCTypeID,
                            inputParam.FSCPercentID,
                            inputParam.PackagingMethodID,
                            inputParam.ClientSpecialPackagingMethodID,
                            inputParam.Season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return Data;
        }

        public DTO.OfferMng.PriceHistory GetOfferLineSalePriceHistory(int userId,string season, int offerLineID, out Library.DTO.Notification notification)
        {
            //List<DTO.OfferMng.OfferLineSalePriceHistoryDTO> Data = new List<DTO.OfferMng.OfferLineSalePriceHistoryDTO>();
            DTO.OfferMng.PriceHistory Data = new DTO.OfferMng.PriceHistory();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OfferMngEntities context = CreateContext())
                {
                    Data.offerLineSalePriceHistoryDTOs = converter.DB2DTO_OfferLineSalePriceHistoryDTO(context.OfferMng_OfferLineSalePriceHistory_View.Where(o=>o.OfferLineID == offerLineID).OrderByDescending(o=>o.UpdatedDate).ToList());
                    var priceLastSeason = context.OfferMng_function_GetOfferLineSalePriceHistoryLastSeason(season, offerLineID).ToList();
                    Data.offerLineSalePriceHistoryLastSeasonDTOs = converter.DB2DTO_OfferLineSalePriceHistoryLastSeasonDTO(priceLastSeason);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return Data;
        }

        public bool ManagerApprove(int id, int userId,  out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Offer has been revised success" };
            try
            {
                if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER))
                {
                    throw new Exception("Unauthorized!");
                }

                using (OfferMngEntities context = CreateContext())
                {
                    var dbOffer = context.Offer.FirstOrDefault(o => o.OfferID == id);
                    if (dbOffer == null)
                    {
                        throw new Exception("Offer not found!");
                    }
                    if (dbOffer.IsApproved.HasValue && dbOffer.IsApproved.Value)
                    {
                        throw new Exception("Offer already been approved!");
                    }
                    dbOffer.IsApproved = true;
                    dbOffer.ApprovedBy = userId;
                    dbOffer.ApprovedDate = DateTime.Now;
                    context.SaveChanges();

                    // send email notification
                    var dbOffer2 = context.OfferMng_Offer_View.FirstOrDefault(o => o.OfferID == id);
                    if (dbOffer2 != null)
                    {
                        string emailBody = "Offer: " + dbOffer2.OfferUD + " has been approved by " + dbOffer2.ApproverName + " on " + dbOffer2.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm");
                        string emailSubject = "Offer: " + dbOffer2.OfferUD + " has been approved by " + dbOffer2.ApproverName + " on " + dbOffer2.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm");
                        string sendToEmail = string.Empty;
                        foreach (var emailItem in context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "OfferAppr"))
                        {
                            sendToEmail += (!string.IsNullOrEmpty(sendToEmail) ? ";" : "") + emailItem.Email1;

                            // add to NotificationMessage table
                            NotificationMessage notification1 = new NotificationMessage();
                            notification1.UserID = emailItem.UserID;
                            notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                            notification1.NotificationMessageTitle = emailSubject;
                            notification1.NotificationMessageContent = emailBody;
                            context.NotificationMessage.Add(notification1);
                        }
                        if (!string.IsNullOrEmpty(emailBody) && !string.IsNullOrEmpty(emailSubject) && !string.IsNullOrEmpty(sendToEmail))
                        {
                            context.EmailNotificationMessage.Add(new EmailNotificationMessage { EmailBody = emailBody, EmailSubject = emailSubject, SendTo = sendToEmail });
                            context.SaveChanges();
                        }
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }
        }

        public bool ApproveItem(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER))
                {
                    throw new Exception("Unauthorized!");
                }

                using (OfferMngEntities context = CreateContext())
                {
                    OfferLine dbOfferLine = context.OfferLine.FirstOrDefault(o => o.OfferLineID == id);
                    if (dbOfferLine == null)
                        throw new Exception("Item not found!");
                    if (!dbOfferLine.Quantity.HasValue)
                        throw new Exception("Invalid order quantity for approval!");
                    if(!dbOfferLine.UnitPrice.HasValue)
                        throw new Exception("Invalid sales price for approval!");
                    if(!dbOfferLine.PlaningPurchasingPrice.HasValue)
                        throw new Exception("Invalid factory price for approval!");
                    dbOfferLine.IsApproved = true;
                    dbOfferLine.ApprovedBy = userId;
                    dbOfferLine.ApprovedDate = DateTime.Now;
                    context.SaveChanges();

                    //Send email to sale assistant
                    var dbOfferLine2 = context.OfferMng_OfferLine_View.FirstOrDefault(o => o.OfferLineID == id);
                    var dbOffer = context.OfferMng_Offer_View.FirstOrDefault(o => o.OfferID == dbOfferLine2.OfferID);
                    var dbClient = context.Client.FirstOrDefault(o => o.ClientID == dbOffer.ClientID);
                    var dbEmployee = context.Employee.FirstOrDefault(o => o.UserID == dbClient.Sale2ID);

                    var OfferUrl = "";
                    OfferUrl = "http://app.tilsoft.bg/Offer/Edit/" + dbOffer.OfferID + "/1";

                    if (dbOfferLine2 != null)
                    {
                        string emailBody = "Item: [" + dbOfferLine2.ArticleCode + "] of Offer [" + dbOffer.OfferUD + "] has been approved by " + dbOfferLine2.ApproverName + " on " + dbOfferLine2.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm");
                        emailBody = emailBody + Environment.NewLine;
                        emailBody = emailBody + "Link to Offer: <a target='blank' href=" + OfferUrl + ">"+ OfferUrl + "</a> </br>";
                        string emailSubject = "Item: " + dbOfferLine2.ArticleCode + " has been approved by " + dbOfferLine2.ApproverName + " on " + dbOfferLine2.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm");
                        string sendToEmail = dbEmployee.Email1.ToString();

                        // add to NotificationMessage table
                        NotificationMessage notification1 = new NotificationMessage();
                        notification1.UserID = dbEmployee.UserID;
                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notification1.NotificationMessageTitle = emailSubject;
                        notification1.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification1);

                        if (!string.IsNullOrEmpty(emailBody) && !string.IsNullOrEmpty(emailSubject) && !string.IsNullOrEmpty(sendToEmail))
                        {
                            context.EmailNotificationMessage.Add(new EmailNotificationMessage { EmailBody = emailBody, EmailSubject = emailSubject, SendTo = sendToEmail });
                            context.SaveChanges();
                        }
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

        public bool UnApproveItem(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER))
                {
                    throw new Exception("Unauthorized!");
                }

                using (OfferMngEntities context = CreateContext())
                {
                    OfferLine dbOfferLine = context.OfferLine.FirstOrDefault(o => o.OfferLineID == id);
                    if (dbOfferLine == null) throw new Exception("Item not found!");
                    dbOfferLine.IsApproved = false;
                    dbOfferLine.ApprovedBy = null;
                    dbOfferLine.ApprovedDate = null;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return false;
        }

        public bool ApproveAllItem(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER))
                {
                    throw new Exception("Unauthorized!");
                }

                using (OfferMngEntities context = CreateContext())
                {
                    Offer dbOffer = context.Offer.FirstOrDefault(o => o.OfferID == id);
                    if (dbOffer == null) throw new Exception("Offer not found!");
                    foreach (OfferLine dbOfferLine in dbOffer.OfferLine.Where(
                        o=>o.OfferItemTypeID == 1 
                        && o.Quantity.HasValue && o.Quantity.Value >= 0 
                        && o.UnitPrice.HasValue && o.UnitPrice.Value > 0
                        && o.PlaningPurchasingPrice.HasValue && o.PlaningPurchasingPrice.Value > 0
                        && (!o.IsApproved.HasValue || !o.IsApproved.Value)
                        )
                    )
                    {
                        dbOfferLine.IsApproved = true;
                        dbOfferLine.ApprovedBy = userId;
                        dbOfferLine.ApprovedDate = DateTime.Now;
                    }
                    context.SaveChanges();

                    //Send email to sale assistant
                    var dbClient = context.Client.FirstOrDefault(o => o.ClientID == dbOffer.ClientID);
                    var dbEmployee = context.Employee.FirstOrDefault(o => o.UserID == dbClient.Sale2ID);
                    var dbEmployee2 = context.Employee.FirstOrDefault(o => o.UserID == userId);

                    var OfferUrl = "";
                    OfferUrl = "http://app.tilsoft.bg/Offer/Edit/" + dbOffer.OfferID.ToString() + "/1";

                    if (dbOffer != null)
                    {
                        string emailBody = "All valid items of offer : [" + dbOffer.OfferUD + "] has been approved by " + dbEmployee2.EmployeeNM + " on " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        emailBody = emailBody + Environment.NewLine;
                        emailBody = emailBody + "Link to Offer: <a target='blank' href=" + OfferUrl + ">" + OfferUrl + "</a> </br>";
                        string emailSubject = "All valid items of offer : [" + dbOffer.OfferUD + "] has been approved by " + dbEmployee2.EmployeeNM + " on " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        string sendToEmail = dbEmployee.Email1.ToString();
                        // add to NotificationMessage table
                        NotificationMessage notification1 = new NotificationMessage();
                        notification1.UserID = dbEmployee.UserID;
                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notification1.NotificationMessageTitle = emailSubject;
                        notification1.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification1);

                        if (!string.IsNullOrEmpty(emailBody) && !string.IsNullOrEmpty(emailSubject) && !string.IsNullOrEmpty(sendToEmail))
                        {
                            context.EmailNotificationMessage.Add(new EmailNotificationMessage { EmailBody = emailBody, EmailSubject = emailSubject, SendTo = sendToEmail });
                            context.SaveChanges();
                        }
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

        public bool SendEmailNotificationByApproveOfferPermission(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                bool? isApproveOffer = fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER);

                if (!isApproveOffer.HasValue || !isApproveOffer.Value)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = "You don't have special permission to work this function!";
                    return false;
                }

                using (var context = CreateContext())
                {
                    var dbItem = context.OfferMng_Offer_View.FirstOrDefault(o => o.OfferID == id);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Offer is not existed!";
                        return false;
                    }

                    if (!dbItem.IsActiveApprovalOffer.HasValue || !dbItem.IsActiveApprovalOffer.Value)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Offer is approved or Offer have not Offer item (FOB) full condition(quantity <> 0, sale price not null, purchasing price not null)!";
                        return false;
                    }

                    // Send email notification
                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    context.EmailNotificationMessage.Add(dbEmail);

                    // Set value
                    dbEmail.EmailSubject = "Offer " + dbItem.OfferUD + " need your approval, please check";
                    dbEmail.EmailBody = "Link:" + Environment.NewLine + FrameworkSetting.Setting.FrontendUrl + "Offer/Edit/" + dbItem.OfferID + "/" + dbItem.OfferVersion;
                    dbEmail.SendTo = "";

                    // Set list email receive notification
                    var dbSendTo = context.OfferMng_ModuleSpecialPermissionAccess_View.Where(o => Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_APPROVE_OFFER.Equals(o.ModuleSpecialPermissionUD));

                    foreach (var item in dbSendTo.ToList())
                    {
                        if (!string.IsNullOrEmpty(dbEmail.SendTo))
                        {
                            dbEmail.SendTo = dbEmail.SendTo + ",";
                        }

                        dbEmail.SendTo = dbEmail.SendTo + item.EmailNotitication;

                        // add to NotificationMessage table
                        NotificationMessage notification1 = new NotificationMessage();
                        notification1.UserID = item.UserID;
                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_SALES;
                        notification1.NotificationMessageTitle = dbEmail.EmailSubject;
                        notification1.NotificationMessageContent = dbEmail.EmailBody;
                        context.NotificationMessage.Add(notification1);
                    }

                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public string ExportOfferDetailToExcel(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            OfferReportDataObject ds = new OfferReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("OfferMng_function_ExportDetailToExcel", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@OfferID", id);
                adap.Fill(ds.OfferMng_function_ExportDetailToExcel);
                return Library.Helper.CreateReportFileWithEPPlus(ds, "OfferDetail", new List<string> { "OfferMng_function_ExportDetailToExcel" });
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return string.Empty;
            }
        }
    }
}
