using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.SaleOrderMng.DTO;
using Library;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Module.SaleOrderMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private string tempFolder;
        public DataFactory() { }

        public DataFactory(string tempFolder)
        {
            this.tempFolder = tempFolder;
        }

        private SaleOrderMngEntities CreateContext()
        {
            return new SaleOrderMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SaleOrderMngModel"));
        }
        
        public DTO.SearchFormData GetDataWithFilters(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.SaleOrderMngSearchResult>();
            totalRows = 0;

            string Season = null;
            string ClientUD = null;
            string ClientNM = null;
            string PINo = null;

            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
            {
                ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PINo") && !string.IsNullOrEmpty(filters["PINo"].ToString()))
            {
                PINo = filters["PINo"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    totalRows = context.SaleOrderMng_function_SearchSaleOrder(orderBy, orderDirection, Season, ClientUD, ClientNM, PINo).Count();
                    var result = context.SaleOrderMng_function_SearchSaleOrder(orderBy, orderDirection, Season, ClientUD, ClientNM, PINo);
                    data.Data = converter.DB2DTO_SaleOrderResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.DataContainer GetDataContainer(int id, int offerID, string orderType, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    DTO.DataContainer dtoItem = new DTO.DataContainer();

                    if (id > 0)
                    {
                        SaleOrderMng_SaleOrder_View dbItem;
                        dbItem = context.SaleOrderMng_SaleOrder_View
                                .Include("SaleOrderMng_SaleOrderDetail_View.SaleOrderMng_SaleOrderDetailExtend_View")
                                .Include("SaleOrderMng_SaleOrderExtend_View")
                                .Include("SaleOrderMng_SaleOrderDetailSparepart_View")
                                .Include("SaleOrderMng_SaleOrderProductReturn_View")
                                .Include("SaleOrderMng_SaleOrderSparepartReturn_View")
                                .Include("SaleOrderMng_WarehouseImport_View")
                                .FirstOrDefault(o => o.SaleOrderID == id);
                        DTO.SaleOrder SaleOrderDTOItem = converter.DB2DTO_SaleOrder(dbItem);
                        dtoItem.SaleOrderData = SaleOrderDTOItem;
                    }
                    else
                    {
                        SaleOrderMng_Offer_View dbOffer = context.SaleOrderMng_Offer_View
                                                    //.Include("SaleOrderMng_OfferLine_View")
                                                    //.Include("SaleOrderMng_OfferLineSparepart_View")
                                                    .FirstOrDefault(o => o.OfferID == offerID);

                        // remove in-approrpiated items
                        if (!String.IsNullOrEmpty(orderType) && (orderType == "WAREHOUSE" || orderType == "FACTORY"))
                        {
                            if (orderType == "WAREHOUSE")
                            {
                                // remove factory item
                                dbOffer.SaleOrderMng_OTC_View.Where(o => o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3).ToList().ForEach(o => dbOffer.SaleOrderMng_OTC_View.Remove(o));
                                //dbOffer.SaleOrderMng_OTCSparepart_View.Where(o => o.OfferSeasonTypeID == 4 || o.OfferSeasonTypeID == 5).ToList().ForEach(o => dbOffer.SaleOrderMng_OTCSparepart_View.Remove(o));
                            }
                            else
                            {
                                //remove warehouse item
                                dbOffer.SaleOrderMng_OTC_View.Where(o => o.OfferSeasonTypeID == 1 || o.OfferSeasonTypeID == null).ToList().ForEach(o => dbOffer.SaleOrderMng_OTC_View.Remove(o));
                                dbOffer.SaleOrderMng_OTCSparepart_View.Where(o => o.OfferSeasonTypeID == 1 || o.OfferSeasonTypeID == null).ToList().ForEach(o => dbOffer.SaleOrderMng_OTCSparepart_View.Remove(o));
                            }
                        }
                        else
                        {
                            dbOffer.SaleOrderMng_OTC_View.Clear();
                            dbOffer.SaleOrderMng_OTCSparepart_View.Clear();
                            orderType = null ;
                        }
                        dbOffer.SaleOrderMng_OTC_View.Where(o => !o.Quantity.HasValue || o.Quantity == 0).ToList().ForEach(o => dbOffer.SaleOrderMng_OTC_View.Remove(o));
                        dbOffer.SaleOrderMng_OTCSparepart_View.Where(o => !o.Quantity.HasValue || o.Quantity == 0).ToList().ForEach(o => dbOffer.SaleOrderMng_OTCSparepart_View.Remove(o));

                        dtoItem.SaleOrderData = new DTO.SaleOrder();
                        dtoItem.SaleOrderData.SaleOrderExtends = new List<DTO.SaleOrderExtend>();
                        dtoItem.SaleOrderData.SaleOrderDetails = new List<DTO.SaleOrderDetail>();
                        dtoItem.SaleOrderData.SaleOrderDetailSpareparts = new List<DTO.SaleOrderDetailSparepart>();
                        dtoItem.SaleOrderData.SaleOrderProductReturns = new List<DTO.SaleOrderProductReturn>();
                        dtoItem.SaleOrderData.SaleOrderSparepartReturns = new List<DTO.SaleOrderSparepartReturn>();

                        //SET INIT VALUE
                        dtoItem.SaleOrderData.OfferID = offerID;
                        dtoItem.SaleOrderData.OfferUD = dbOffer.OfferUD;
                        dtoItem.SaleOrderData.Season = dbOffer.Season;
                        dtoItem.SaleOrderData.TrackingStatusNM = Library.Helper.TEXT_STATUS_CREATED;
                        dtoItem.SaleOrderData.PaymentTermID = dbOffer.PaymentTermID;
                        dtoItem.SaleOrderData.DeliveryTermID = dbOffer.DeliveryTermID;
                        dtoItem.SaleOrderData.PaymentTypeNM = dbOffer.PaymentTypeNM;
                        dtoItem.SaleOrderData.DeliveryTypeNM = dbOffer.DeliveryTypeNM;
                        //dtoItem.SaleOrderData.Season = Library.Helper.GetCurrentSeason();
                        dtoItem.SaleOrderData.Conditions = "ALL PRICES INCLUDE 2% DAMAGE RISK";
                        dtoItem.SaleOrderData.Currency = dbOffer.Currency;
                        dtoItem.SaleOrderData.VATPercent = dbOffer.VAT;
                        dtoItem.SaleOrderData.IsViewDeliveryDateOnPrintout = true;
                        dtoItem.SaleOrderData.IsViewLDSOnPrintout = true;
                        dtoItem.SaleOrderData.IsViewQuantityContOnPrintout = true;
                        dtoItem.SaleOrderData.OrderType = orderType;
                        int i = -1;
                        foreach (SaleOrderMng_OTC_View lineItem in dbOffer.SaleOrderMng_OTC_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderDetail dtoDetail = new DTO.SaleOrderDetail();

                            dtoDetail.SaleOrderDetailID = i;
                            dtoDetail.OfferLineID = lineItem.OfferLineID;
                            dtoDetail.ArticleCode = lineItem.ArticleCode;
                            dtoDetail.Description = lineItem.Description;
                            //dtoDetail.OrderQnt = lineItem.Quantity;
                            dtoDetail.UnitPrice = lineItem.FinalPrice;
                            dtoDetail.ProductID = lineItem.ProductID;
                            dtoDetail.Quantity = lineItem.Quantity; //offer quantity

                            dtoDetail.PhysicalQnt = lineItem.PhysicalQnt;
                            dtoDetail.OnSeaQnt = lineItem.OnSeaQnt;
                            dtoDetail.TobeShippedQnt = lineItem.TobeShippedQnt;
                            dtoDetail.ReservationQnt = lineItem.ReservationQnt;
                            dtoDetail.FTSQnt = lineItem.FTSQnt;
                            dtoDetail.ProductStatusID = 1;

                            dtoDetail.ClientArticleCode = lineItem.ClientArticleCode;
                            dtoDetail.ClientArticleName = lineItem.ClientArticleName;
                            dtoDetail.ClientOrderNumber = lineItem.ClientOrderNumber;
                            dtoDetail.ClientEANCode = lineItem.ClientEANCode;


                            // themy
                            dtoDetail.OfferItemTypeID = lineItem.OfferItemTypeID;
                            dtoDetail.OfferItemTypeNM = lineItem.OfferItemTypeNM;
                            dtoDetail.ApproverName = lineItem.ApproverName;
                            dtoDetail.ApprovedDate = lineItem.ApprovedDate.HasValue ? lineItem.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : null;
                            dtoDetail.PlaningPurchasingPrice = lineItem.PlaningPurchasingPrice;
                            dtoDetail.PlaningPurchasingPriceFactoryID = lineItem.PlaningPurchasingPriceFactoryID;
                            dtoDetail.PlaningPurchasingPriceSourceID = lineItem.PlaningPurchasingPriceSourceID;
                            dtoDetail.IsPlaningPurchasingPriceSelected = lineItem.IsPlaningPurchasingPriceSelected;
                            dtoDetail.PlaningPurchasingPriceSelectedBy = lineItem.PlaningPurchasingPriceSelectedBy;
                            dtoDetail.PlaningPurchasingPriceSelectedDate = lineItem.PlaningPurchasingPriceSelectedDate.HasValue ? lineItem.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy HH:mm") : null;
                            dtoDetail.PlaningPurchasingPriceSourceNM = lineItem.PlaningPurchasingPriceSourceNM;
                            dtoDetail.PlanningPriceSelectorName = lineItem.PlanningPriceSelectorName;
                            dtoDetail.UnitPrice = lineItem.OfferSalePrice;

                            dtoDetail.SaleOrderDetailExtends = new List<DTO.SaleOrderDetailExtend>();

                            dtoItem.SaleOrderData.SaleOrderDetails.Add(dtoDetail);
                            i = i - 1;
                        }

                        i = -1;
                        foreach (var item in dbOffer.SaleOrderMng_OTCSparepart_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderDetailSparepart dtoSparepart = new DTO.SaleOrderDetailSparepart();
                            dtoSparepart.SaleOrderDetailSparepartID = i;
                            dtoSparepart.OfferLineSparepartID = item.OfferLineSparepartID;
                            dtoSparepart.ArticleCode = item.ArticleCode;
                            dtoSparepart.Description = item.Description;
                            //dtoSparepart.OrderQnt = item.Quantity;
                            dtoSparepart.UnitPrice = item.UnitPrice;
                            dtoSparepart.ProductStatusID = 1;
                            dtoSparepart.ClientArticleCode = item.ClientArticleCode;
                            dtoSparepart.ClientArticleName = item.ClientArticleName;
                            dtoSparepart.ClientEANCode = item.ClientEANCode;

                            dtoItem.SaleOrderData.SaleOrderDetailSpareparts.Add(dtoSparepart);

                            i = i - 1;
                        }
                    }
                    //get support data
                    dtoItem.DeliveryTerms = support_factory.GetDeliveryTerm().ToList();
                    dtoItem.PaymentTerms = support_factory.GetPaymentTerm().ToList();
                    dtoItem.Seasons = support_factory.GetSeason().ToList();
                    dtoItem.OrderTypes = GetOrderType().ToList();
                    dtoItem.ProductStatuses = GetProductStatus().ToList();
                    dtoItem.CostTypes = GetCostTypes().ToList();
                    dtoItem.PaymentStatuses = GetPaymentStatus().ToList();
                    dtoItem.VATPercents = GetVATPercent().ToList();
                    dtoItem.ReportTemplates = support_factory.GetReportTemplate().ToList();

                    // Get defautl report template
                    if (dtoItem.SaleOrderData.DefaultPiReport == null)
                    {
                        var defaultRpt = dtoItem.ReportTemplates.FirstOrDefault(x => x.ReportType == "PI" && x.IsDefault == true).ReportTemplateNM;
                        dtoItem.SaleOrderData.DefaultPiReport = defaultRpt;
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
                return new DTO.DataContainer();
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SaleOrder GetBackData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrderMng_SaleOrder_View dbItem;
                    dbItem = context.SaleOrderMng_SaleOrder_View.FirstOrDefault(o => o.SaleOrderID == id);
                    return converter.DB2DTO_SaleOrder(dbItem);

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return new DTO.SaleOrder();
            }
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            DTO.SaleOrder dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SaleOrder>();
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrder dbItem = null;
                    SaleOrderStatus dbStatus = null;
                    if (id == 0)
                    {
                        if (!dtoItems.OfferID.HasValue)
                        {
                            throw new Exception("Invalid offer!");
                        }
                        //else
                        //{
                        //    int offerID = dtoItem.OfferID.Value;
                        //    var dbOffer = context.SaleOrderMng_ApprovedOffer_View.FirstOrDefault(o => o.OfferID == offerID);
                        //    if (dbOffer == null)
                        //    {
                        //        throw new Exception("Offer has not been approved! Please contact your manager");
                        //    }
                        //}

                        dbItem = new SaleOrder();
                        context.SaleOrder.Add(dbItem);

                        //SET PI STATUS
                        dbStatus = new SaleOrderStatus();
                        dbStatus.TrackingStatusID = Library.Helper.CREATED;
                        dbStatus.StatusDate = DateTime.Now;
                        dbStatus.UpdatedBy = userId;
                        dbStatus.IsCurrentStatus = true;
                        dbItem.SaleOrderStatus.Add(dbStatus);

                        //SET PI No
                        dtoItems.ProformaInvoiceNo = context.SaleOrderMng_function_GeneratePINo(dtoItems.UpdatedBy, dtoItems.OfferID).FirstOrDefault();

                        //SET TRACKING INFO
                        dtoItems.CreatedBy = userId;
                        dtoItems.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.SaleOrder.FirstOrDefault(o => o.SaleOrderID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "PI not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItems.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        if (id == 0)
                        {
                            int? offerTrackingStatusID = GetOfferTrackingStatus(dtoItems.OfferID, out notification);
                            if (!(offerTrackingStatusID == 3 || offerTrackingStatusID == 5))
                            {
                                throw new Exception("Offer should be confirmed before create new PI");
                            }
                        }

                        // check who update payment field
                        string oldPaymentDate = (dbItem.PaymentDate == null ? "" : dbItem.PaymentDate.Value.ToString("dd/MM/yyyy"));
                        string newPaymentDate = (dtoItems.paymentDateFormated == null ? "" : dtoItems.paymentDateFormated);
                        if (dtoItems.PaymentStatusID != dbItem.PaymentStatusID || dtoItems.PaymentRemark != dbItem.PaymentRemark || newPaymentDate != oldPaymentDate)
                        {
                            dbItem.PaymentUpdatedBy = userId;
                            dbItem.PaymentUpdatedDate = DateTime.Now;
                        }

                        //check invoice data in season 2015/2016
                        DateTime? startDate = new DateTime(Convert.ToInt32(dtoItems.Season.Substring(0, 4)), 10, 1);
                        DateTime? endDate = new DateTime(Convert.ToInt32(dtoItems.Season.Substring(5, 4)), 9, 30);
                        DateTime? invoiceDate = dtoItems.InvoiceDateFormated.ConvertStringToDateTime();
                        if (invoiceDate < startDate || invoiceDate > endDate)
                        {
                            throw new Exception("Invoice Date must be between " + startDate.Value.ToString("dd/MM/yyyy") + " and " + endDate.Value.ToString("dd/MM/yyyy") + " because the season is " + dtoItems.Season);
                        }
                        //read dto to db
                        converter.DTO2DB_SaleOrder(dtoItems, ref dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //remove orphan item
                        context.SaleOrderDetailExtend.Local.Where(o => o.SaleOrderDetail == null).ToList().ForEach(o => context.SaleOrderDetailExtend.Remove(o));
                        context.SaleOrderDetail.Local.Where(o => o.SaleOrder == null).ToList().ForEach(o => context.SaleOrderDetail.Remove(o));
                        context.SaleOrderExtend.Local.Where(o => o.SaleOrder == null).ToList().ForEach(o => context.SaleOrderExtend.Remove(o));
                        context.SaleOrderDetailSparepart.Local.Where(o => o.SaleOrder == null).ToList().ForEach(o => context.SaleOrderDetailSparepart.Remove(o));

                        //save data
                        context.SaveChanges();

                        //// add item to quotation if needed
                        //context.FW_Quotation_function_AddFactoryOrderItem(null, dbItem.SaleOrderID, null); // table lockx and also check if item is available on sql server side
                        int OfferID = Convert.ToInt32(dbItem.OfferID);
                        //reload data
                        dtoItem = GetDataContainer(dbItem.SaleOrderID, OfferID, dbItem.OrderType, out notification);
                        //if (id > 0)
                        //{
                        //    dtoItem = new DTO.SaleOrder { SaleOrderID = id };
                        //}
                        //else
                        //{
                        //    dtoItem = new DTO.SaleOrder { SaleOrderID = dbItem.SaleOrderID };
                        //}

                        //create reservation
                        //if (dtoItem.OrderType == "WAREHOUSE")
                        //{
                        //    context.SaleOrderMng_function_CreateReservation(dtoItem.SaleOrderID, dtoItem.UpdatedBy);
                        //}
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }
        }

        public bool DeletePI(int id, int deletedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "PI has been deleted" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrderStatus dbStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == id && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbStatus.TrackingStatusID == Library.Helper.CONFIRMED || dbStatus.TrackingStatusID == Library.Helper.REVISION_CONFIRMED)
                    {
                        throw new Exception("PI was confirmed. You can not delete");
                    }

                    if (dbStatus.TrackingStatusID == Library.Helper.REVISED)
                    {
                        throw new Exception("PI was revised. You can not delete");
                    }

                    context.SaleOrderMng_function_DeleteSaleOrder(id, deletedBy);
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

        public int? GetOfferTrackingStatus(int? offerID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int? trackingStatusID = null;
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    //var dbOffer = context.SaleOrderMng_ApprovedOffer_View.FirstOrDefault(o => o.OfferID == offerID);
                    //if (dbOffer == null)
                    //{
                    //    return -1; // not manager approved
                    //}
                    trackingStatusID = context.OfferStatus.Where(o => o.OfferID == offerID && o.IsCurrentStatus == true).FirstOrDefault().TrackingStatusID;
                    return trackingStatusID;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return null;
            }
        }

        private IEnumerable<DTO.OrderType> GetOrderType()
        {
            List<DTO.OrderType> OrderTypes = new List<DTO.OrderType>();

            OrderTypes.Add(new DTO.OrderType { OrderTypeValue = "FACTORY", OrderTypeText = "FACTORY" });
            OrderTypes.Add(new DTO.OrderType { OrderTypeValue = "FOB_WAREHOUSE", OrderTypeText = "FOB-WAREHOUSE" });
            OrderTypes.Add(new DTO.OrderType { OrderTypeValue = "WAREHOUSE", OrderTypeText = "WAREHOUSE" });

            return OrderTypes;
        }

        private IEnumerable<DTO.CostType> GetCostTypes()
        {
            List<DTO.CostType> CostTypes = new List<DTO.CostType>();

            CostTypes.Add(new DTO.CostType { CostTypeValue = "D", CostTypeText = "DISCOUNT" });
            CostTypes.Add(new DTO.CostType { CostTypeValue = "T", CostTypeText = "TRANSPORTATION" });
            CostTypes.Add(new DTO.CostType { CostTypeValue = "O", CostTypeText = "OTHER" });

            return CostTypes;
        }

        private IEnumerable<DTO.ProductStatus> GetProductStatus()
        {
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductStatus(context.SupportMng_ProductStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.ProductStatus>();
            }
        }

        private IEnumerable<DTO.VATPercent> GetVATPercent()
        {
            List<DTO.VATPercent> VATPercents = new List<DTO.VATPercent>();

            VATPercents.Add(new DTO.VATPercent { VATPercentValue = 0, VATPercentText = "0%" });
            VATPercents.Add(new DTO.VATPercent { VATPercentValue = 21, VATPercentText = "21%" });
            return VATPercents;
        }

        public IEnumerable<DTO.PaymentStatus> GetPaymentStatus()
        {
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentStatus(context.List_PaymentStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.PaymentStatus>();
            }
        }

        public List<DTO.SaleOrderDetailOTC> GetOfferLine(int offerId, string orderType, out Library.DTO.Notification notification)
        {
            List<DTO.SaleOrderDetailOTC> data = new List<DTO.SaleOrderDetailOTC>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    if (orderType == "WAREHOUSE")
                    {
                        data = converter.DB2DTO_OfferLineOTC(context.SaleOrderMng_OTC_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1).ToList());
                    }
                    else
                    {
                        data = converter.DB2DTO_OfferLineOTC(context.SaleOrderMng_OTC_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3)).ToList());
                    }

                    foreach (var item in data)
                    {
                        item.ProductStatusID = 1;
                        item.UnitPrice = item.OfferSalePrice;
                        item.SaleOrderDetailExtends = new List<DTO.SaleOrderDetailExtend>();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.SaleOrderDetailOTCSparepart> GetOfferLineSparepart(int offerId, string orderType, out Library.DTO.Notification notification)
        {
            List<DTO.SaleOrderDetailOTCSparepart> data = new List<DTO.SaleOrderDetailOTCSparepart>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    if (orderType == "WAREHOUSE")
                    {
                        data = converter.DB2DTO_OfferLineOTCSparepart(context.SaleOrderMng_OTCSparepart_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1).ToList());
                    }
                    else
                    {
                        data = converter.DB2DTO_OfferLineOTCSparepart(context.SaleOrderMng_OTCSparepart_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 4 || o.OfferSeasonTypeID == 5)).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public bool Confirm(int saleOrderID, ref object dtoItem, int setStatusBy, bool isConfirmWithoutSigned, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "PI has been confirmed success" };
            DTO.SaleOrder dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SaleOrder>();
            try
            {
                //SAVE DATA
                Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
                if (!UpdateData(setStatusBy, saleOrderID, ref dtoItem, out save_nofication))
                {
                    notification = save_nofication;
                    return false;
                }

                //VALIDATE && CONFIRM
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrder dbSaleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                    OfferStatus dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == dbSaleOrder.OfferID && o.IsCurrentStatus == true).FirstOrDefault();

                    if (dbOfferStatus.TrackingStatusID != Library.Helper.CONFIRMED && dbOfferStatus.TrackingStatusID != Library.Helper.REVISION_CONFIRMED)
                    {
                        notification = new Library.DTO.Notification() { Message = "Offer must be in CONFIRMED status before confirm PI", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }
                    else if (isConfirmWithoutSigned)
                    {
                        if (string.IsNullOrEmpty(dbSaleOrder.PIReceivedRemark))
                        {
                            notification = new Library.DTO.Notification() { Message = "PI must be filled-in [Signed PI Remark] before confirm", Type = Library.DTO.NotificationType.Warning };
                            return false;
                        }
                    }
                    else
                    {
                        if (dbSaleOrder.IsPIReceived == null || dbSaleOrder.IsPIReceived == false)
                        {
                            notification = new Library.DTO.Notification() { Message = "PI must be check [Signed PI Received] before confirm", Type = Library.DTO.NotificationType.Warning };
                            return false;
                        }
                    }

                    SaleOrderStatus dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbSaleOrderStatus.TrackingStatusID != Library.Helper.REVISED && dbSaleOrderStatus.TrackingStatusID != Library.Helper.CREATED)
                    {
                        notification = new Library.DTO.Notification() { Message = "PI must be in REVISED/CREATED status before confirm", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }
                    //set tracking status
                    int TrackingStatusID = (dbSaleOrderStatus.TrackingStatusID == Library.Helper.CREATED ? Library.Helper.CONFIRMED : Library.Helper.REVISION_CONFIRMED);
                    SetPIStatus(saleOrderID, TrackingStatusID, setStatusBy);

                    //frozend PI printout data
                    context.SaleOrderMng_function_FrozenSaleOrderPrintoutData(saleOrderID);
                    //get back data
                    dtoItems.TrackingStatusNM = (dbSaleOrderStatus.TrackingStatusID == Library.Helper.CREATED ? Library.Helper.TEXT_STATUS_CONFIRMED : Library.Helper.TEXT_STATUS_REVISION_CONFIRMED);
                    dtoItems.TrackingStatusID = TrackingStatusID;

                    //// add item to quotation if needed
                    //context.FW_Quotation_function_AddFactoryOrderItem(null, saleOrderID, null); // table lockx and also check if item is available on sql server side

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

        public bool Revise(int saleOrderID, ref object dtoItem, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "PI has been revised success" };
            DTO.SaleOrder dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SaleOrder>();
            try
            {
                //SAVE DATA
                Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
                if (!UpdateData(setStatusBy, saleOrderID, ref dtoItem, out save_nofication))
                {
                    notification = save_nofication;
                    return false;
                }

                //VALIDATE && CONFIRM
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrderStatus dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbSaleOrderStatus.TrackingStatusID != Library.Helper.CONFIRMED && dbSaleOrderStatus.TrackingStatusID != Library.Helper.REVISION_CONFIRMED)
                    {
                        notification = new Library.DTO.Notification() { Message = "PI must be in CONFIRMED status before revise", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    SetPIStatus(saleOrderID, Library.Helper.REVISED, setStatusBy);
                    dtoItems.TrackingStatusNM = Library.Helper.TEXT_STATUS_REVISED;
                    dtoItems.TrackingStatusID = Library.Helper.REVISED;

                    // add item to quotation if needed
                    context.FW_Quotation_function_AddFactoryOrderItem(null, saleOrderID, null); // table lockx and also check if item is available on sql server side

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

        private void SetPIStatus(int saleOrderID, int trackingStatusID, int setStatusBy)
        {
            using (SaleOrderMngEntities context = CreateContext())
            {
                foreach (SaleOrderStatus item in context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID))
                {
                    item.IsCurrentStatus = false;
                }

                SaleOrderStatus dbStatus = new SaleOrderStatus();
                dbStatus.SaleOrderID = saleOrderID;
                dbStatus.TrackingStatusID = trackingStatusID;
                dbStatus.StatusDate = DateTime.Now;
                dbStatus.UpdatedBy = setStatusBy;
                dbStatus.IsCurrentStatus = true;
                context.SaleOrderStatus.Add(dbStatus);

                context.SaveChanges();

                //// add item to quotation if needed
                //context.FW_Quotation_function_AddFactoryOrderItem(null, saleOrderID, null); // table lockx and also check if item is available on sql server side
            }
        }

        public DTO.SaleOrder UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer, string tempFolder, out Library.DTO.Notification notification)
        {
            Module.Framework.DAL.DataFactory framework_factory = new Module.Framework.DAL.DataFactory();
            DTO.SaleOrder dtoSaleOrder = new DTO.SaleOrder();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Upload success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    string newPointer = framework_factory.CreateNoneImageFilePointer(tempFolder, newFile, oldPointer);
                    if (saleOrderID > 0)
                    {
                        var saleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        saleOrder.SignedPIFile = newPointer;
                        context.SaveChanges();
                        //get return data
                        dtoSaleOrder = GetBackData(saleOrderID, out notification);
                    }

                }
                return dtoSaleOrder;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return dtoSaleOrder;
            }
        }

        public DTO.SaleOrder UploadClientPOFile(int saleOrderID, string newFile, string oldPointer, string tempFolder, out Library.DTO.Notification notification)
        {
            Module.Framework.DAL.DataFactory framework_factory = new Module.Framework.DAL.DataFactory();
            DTO.SaleOrder dtoSaleOrder = new DTO.SaleOrder();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Upload success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    string newPointer = framework_factory.CreateNoneImageFilePointer(tempFolder, newFile, oldPointer);
                    if (saleOrderID > 0)
                    {
                        var saleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        saleOrder.ClientPOFile = newPointer;
                        context.SaveChanges();
                        //get return data
                        dtoSaleOrder = GetBackData(saleOrderID, out notification);
                    }
                }
                return dtoSaleOrder;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return dtoSaleOrder;
            }
        }

        public string GetPrintoutData (int saleOrderID, string reportName, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrderMng_SaleOrder_ReportView dbItem;
                    dbItem = context.SaleOrderMng_SaleOrder_ReportView
                        .Include("SaleOrderMng_SaleOrderDetail_ReportView.SaleOrderMng_SaleOrderDetailExtend_ReportView")
                        .Include("SaleOrderMng_SaleOrderDetailSparepart_ReportView")
                        .Include("SaleOrderMng_SaleOrderExtend_ReportView")
                        .FirstOrDefault(o => o.SaleOrderID == saleOrderID);
                    var dtoPrintout = converter.DB2DTO_Printout(dbItem);



                    //CREATE PRINTOUT
                    if (reportName == null)
                    {
                        reportName = dtoPrintout.ReportName;
                    }

                    //int? companyID = fwBll.GetCompanyID(ControllerContext.GetAuthUserId());
                    //switch (companyID)
                    //{
                    //    case 13:
                    //        reportName = reportName + "_OrangePine.rdlc";
                    //        break;
                    //    default:
                    //        reportName = reportName + ".rdlc";
                    //        break;
                    //}
                    reportName = reportName + ".rdlc";
                    Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                    lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

                    Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
                    rsInvoice.Name = "Order";
                    rsInvoice.Value = dtoPrintout.PIPrintouts;
                    lr.DataSources.Add(rsInvoice);

                    Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
                    rsInvoiceDetail.Name = "OrderDetail";
                    rsInvoiceDetail.Value = dtoPrintout.PIDetailPrintouts;
                    lr.DataSources.Add(rsInvoiceDetail);

                    string printoutFileName = TilsoftService.Helper.PrintoutHelper.BuildPrintoutFile(lr, "PDF");

                    //
                    // author: TheMy
                    // description: add disclaimer (using iTextSharp)
                    //
                    string finalFile = System.Guid.NewGuid().ToString().Replace("-", "") + ".pdf";
                    FileInfo fInfo = new FileInfo(FrameworkSetting.Setting.AbsoluteReportFolder + reportName);
                    if (File.Exists(FrameworkSetting.Setting.AbsoluteReportFolder + fInfo.Name.Replace(fInfo.Extension, "") + "_bottom.pdf"))
                    {
                        try
                        {
                            Document document = new Document();
                            PdfCopy writer = new PdfCopy(document, new FileStream(FrameworkSetting.Setting.AbsoluteReportTmpFolder + finalFile, FileMode.Create));
                            if (writer == null)
                            {
                                throw new Exception("Can not create Pdf object");
                            }
                            document.Open();
                            writer.AddDocument(new PdfReader(FrameworkSetting.Setting.AbsoluteReportTmpFolder + printoutFileName));
                            writer.AddDocument(new PdfReader(FrameworkSetting.Setting.AbsoluteReportFolder + fInfo.Name.Replace(fInfo.Extension, "") + "_bottom.pdf"));
                            writer.Close();
                            document.Close();
                        }
                        catch (Exception ex)
                        {
                            //Library.DTO.notification.Type = Library.DTO.NotificationType.Error;
                            //notification.Message = "Adding general condition failed: " + ex.Message;
                            finalFile = printoutFileName;
                        }
                    }
                    else
                    {
                        finalFile = printoutFileName;
                    }

                    return finalFile;
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
                return "";
            }
        }

    }
}
