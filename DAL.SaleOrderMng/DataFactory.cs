using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using DALBase;
namespace DAL.SaleOrderMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.SaleOrderMng.OfferSearch, DTO.SaleOrderMng.SaleOrder>
    {
        private string tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        public DataFactory(string tempFolder)
        {
            this.tempFolder = tempFolder;
        }
        
        private SaleOrderMngEntities CreateContext()
        {
            var mContext = new SaleOrderMngEntities(DALBase.Helper.CreateEntityConnectionString("SaleOrderMngModel"));
            mContext.Database.CommandTimeout = 300;
            return mContext;
        }

        public override IEnumerable<DTO.SaleOrderMng.OfferSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string OfferUD = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            int SaleID = 0;
            string ProformaInvoiceNo = string.Empty;
            string OrderType = string.Empty;
            string V4PINo = string.Empty;
            string ArticleCode = string.Empty;
            string Description = string.Empty;
            string ClientArticleCode = string.Empty;
            string ClientArticleName = string.Empty;
            string ClientOrderNumber = string.Empty;
            string ClientEANCode = string.Empty;

            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("OfferUD")) OfferUD = filters["OfferUD"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("SaleID") && filters["SaleID"] != null) SaleID = Convert.ToInt32(filters["SaleID"]);
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("OrderType") && filters["OrderType"] != null) OrderType = filters["OrderType"].ToString();
            if (filters.ContainsKey("V4PINo")) V4PINo = filters["V4PINo"].ToString();

            if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description")) Description = filters["Description"].ToString();
            if (filters.ContainsKey("ClientArticleCode")) ClientArticleCode = filters["ClientArticleCode"].ToString();
            if (filters.ContainsKey("ClientArticleName")) ClientArticleName = filters["ClientArticleName"].ToString();
            if (filters.ContainsKey("ClientOrderNumber")) ClientOrderNumber = filters["ClientOrderNumber"].ToString();
            if (filters.ContainsKey("ClientEANCode")) ClientEANCode = filters["ClientEANCode"].ToString();
            //try to get data
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    totalRows = context.SaleOrderMng_function_SearchOffer(  orderBy,
                                                                            orderDirection,
                                                                            Season,
                                                                            OfferUD,
                                                                            ClientUD,
                                                                            ClientNM,
                                                                            SaleID,
                                                                            ProformaInvoiceNo,
                                                                            OrderType,
                                                                            V4PINo,
                                                                            ArticleCode,
                                                                            Description,
                                                                            ClientArticleCode,
                                                                            ClientArticleName,
                                                                            ClientOrderNumber,
                                                                            ClientEANCode
                                                                            ).Count();
                    var result = context.SaleOrderMng_function_SearchOffer( orderBy,
                                                                            orderDirection,
                                                                            Season,
                                                                            OfferUD,
                                                                            ClientUD,
                                                                            ClientNM,
                                                                            SaleID,
                                                                            ProformaInvoiceNo,
                                                                            OrderType,
                                                                            V4PINo,
                                                                            ArticleCode,
                                                                            Description,
                                                                            ClientArticleCode,
                                                                            ClientArticleName,
                                                                            ClientOrderNumber,
                                                                            ClientEANCode
                                                                            );
                    
                    return converter.DB2DTO_OfferSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.SaleOrderMng.OfferSearch>();
            }
        }

        public override DTO.SaleOrderMng.SaleOrder GetData(int id, out Library.DTO.Notification notification)
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
                return new DTO.SaleOrderMng.SaleOrder();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            return true;
        }

        public override bool UpdateData(int id, ref DTO.SaleOrderMng.SaleOrder dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrder dbItem = null;
                    SaleOrderStatus dbStatus = null;
                    if (id == 0)
                    {
                        if (!dtoItem.OfferID.HasValue)
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
                        dbStatus.TrackingStatusID = DALBase.Helper.CREATED;
                        dbStatus.StatusDate = DateTime.Now;
                        dbStatus.UpdatedBy = dtoItem.UpdatedBy;
                        dbStatus.IsCurrentStatus = true;
                        dbItem.SaleOrderStatus.Add(dbStatus);

                        //SET PI No
                        dtoItem.ProformaInvoiceNo = context.SaleOrderMng_function_GeneratePINo(dtoItem.UpdatedBy, dtoItem.OfferID).FirstOrDefault();

                        //SET TRACKING INFO
                        dtoItem.CreatedBy = dtoItem.UpdatedBy;
                        dtoItem.CreatedDate = DateTime.Now;
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
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        if (id == 0)
                        {
                            int? offerTrackingStatusID = GetOfferTrackingStatus(dtoItem.OfferID, out notification);
                            if (!(offerTrackingStatusID == 3 || offerTrackingStatusID == 5))
                            {
                                throw new Exception("Offer should be confirmed before create new PI");
                            }
                        }

                        // check who update payment field
                        string oldPaymentDate = (dbItem.PaymentDate == null ? "" : dbItem.PaymentDate.Value.ToString("dd/MM/yyyy"));
                        string newPaymentDate = (dtoItem.paymentDateFormated == null ? "" : dtoItem.paymentDateFormated);
                        if (dtoItem.PaymentStatusID != dbItem.PaymentStatusID || dtoItem.PaymentRemark != dbItem.PaymentRemark || newPaymentDate != oldPaymentDate)
                        {
                            dbItem.PaymentUpdatedBy = dtoItem.UpdatedBy;
                            dbItem.PaymentUpdatedDate = DateTime.Now;
                        }

                        //check invoice data in season 2015/2016
                        DateTime? startDate = new DateTime(Convert.ToInt32(dtoItem.Season.Substring(0, 4)), 10, 1);
                        DateTime? endDate = new DateTime(Convert.ToInt32(dtoItem.Season.Substring(5, 4)), 9, 30);
                        DateTime? invoiceDate = dtoItem.InvoiceDateFormated.ConvertStringToDateTime();
                        if (invoiceDate < startDate || invoiceDate > endDate)
                        {
                            throw new Exception("Invoice Date must be between " + startDate.Value.ToString("dd/MM/yyyy") + " and " + endDate.Value.ToString("dd/MM/yyyy") + " because the season is " + dtoItem.Season);
                        }

                        //Check quantity of sample
                        if (dtoItem.TrackingStatusID != 5)
                        {
                            if (dtoItem.SaleOrderDetailSamples.Count() > 0)
                            {
                                if (dtoItem.OrderType == "FACTORY")
                                {
                                    List<int> removeIndex = new List<int>();
                                    var offerID = dtoItem.OfferID;
                                    var OfferLineSamples = context.SaleOrderMng_OfferLineSample_View.Where(o => o.OfferID == offerID).ToList();
                                    foreach (var item in dtoItem.SaleOrderDetailSamples)
                                    {
                                        var offerLineSampleItem = OfferLineSamples.Where(o => o.OfferLineSampleProductID == item.OfferLineSampleProductID).ToList();
                                        if (offerLineSampleItem.Count > 0)
                                        {
                                            if (item.Quantity > offerLineSampleItem[0].OfferSeasonDetailQuantity)
                                            {
                                                notification.Type = Library.DTO.NotificationType.Warning;
                                                notification.Message = "Quantity of sample <b>" + item.ArticleCode + "</b> can't greater than quantity on Offer Season (Quantity must less than or equal " + offerLineSampleItem[0].OfferSeasonDetailQuantity + ")";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            removeIndex.Add(dtoItem.SaleOrderDetailSamples.IndexOf(item));
                                        }
                                    }
                                    if (removeIndex.Count() > 0)
                                    {
                                        foreach(var item in removeIndex)
                                        {
                                            dtoItem.SaleOrderDetailSamples.RemoveAt(item);
                                        }
                                    }
                                }
                            }
                        }

                            //read dto to db
                        converter.DTO2DB_SaleOrder(dtoItem, ref dbItem);

                        //remove orphan item
                        context.SaleOrderDetailExtend.Local.Where(o => o.SaleOrderDetail == null).ToList().ForEach(o => context.SaleOrderDetailExtend.Remove(o));
                        context.SaleOrderDetail.Local.Where(o => o.SaleOrder == null).ToList().ForEach(o => context.SaleOrderDetail.Remove(o));
                        context.SaleOrderExtend.Local.Where(o => o.SaleOrder == null).ToList().ForEach(o => context.SaleOrderExtend.Remove(o));
                        context.SaleOrderDetailSparepart.Local.Where(o => o.SaleOrder == null).ToList().ForEach(o => context.SaleOrderDetailSparepart.Remove(o));
                        
                        //save data
                        context.SaveChanges();

                        //// add item to quotation if needed
                        //context.FW_Quotation_function_AddFactoryOrderItem(null, dbItem.SaleOrderID, null); // table lockx and also check if item is available on sql server side

                        //reload data
                        //dtoItem = GetData(dbItem.SaleOrderID, out notification);
                        if (id > 0)
                        {
                            dtoItem = new DTO.SaleOrderMng.SaleOrder { SaleOrderID = id };
                        }
                        else
                        {
                            dtoItem = new DTO.SaleOrderMng.SaleOrder { SaleOrderID = dbItem.SaleOrderID };
                        }

                        //create reservation
                        //if (dtoItem.OrderType == "WAREHOUSE")
                        //{
                        //    context.SaleOrderMng_function_CreateReservation(dtoItem.SaleOrderID, dtoItem.UpdatedBy);
                        //}
                        // dtoItem = GetData(id, out notification);
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

        public bool DeletePI(int id, int deletedBy ,out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "PI has been deleted" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrderStatus dbStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == id && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                    {
                        throw new Exception("PI was confirmed. You can not delete");
                    }

                    if (dbStatus.TrackingStatusID == DALBase.Helper.REVISED)
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

        public DTO.SaleOrderMng.DataContainer GetDataContainer(int id, int offerID, string orderType, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new DAL.Support.DataFactory();
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    DTO.SaleOrderMng.DataContainer dtoItem = new DTO.SaleOrderMng.DataContainer();

                    if (id > 0)
                    {
                        SaleOrderMng_SaleOrder_View dbItem;
                        dbItem = context.SaleOrderMng_SaleOrder_View
                                .Include("SaleOrderMng_SaleOrderDetail_View.SaleOrderMng_SaleOrderDetailExtend_View")
                                .Include("SaleOrderMng_SaleOrderExtend_View")
                                .Include("SaleOrderMng_SaleOrderDetailSparepart_View")
                                .Include("SaleOrderMng_SaleOrderDetailSample_View")
                                .Include("SaleOrderMng_SaleOrderProductReturn_View")
                                .Include("SaleOrderMng_SaleOrderSparepartReturn_View")
                                .Include("SaleOrderMng_WarehouseImport_View")
                                .FirstOrDefault(o => o.SaleOrderID == id);
                        DTO.SaleOrderMng.SaleOrder SaleOrderDTOItem = converter.DB2DTO_SaleOrder(dbItem);
                        dtoItem.SaleOrderData = SaleOrderDTOItem;
                    }
                    else
                    {
                        SaleOrderMng_Offer_View dbOffer = context.SaleOrderMng_Offer_View
                                                    //.Include("SaleOrderMng_OfferLine_View")
                                                    //.Include("SaleOrderMng_OfferLineSparepart_View")
                                                    .FirstOrDefault(o => o.OfferID == offerID);

                        // remove in-approrpiated items
                        //if (!string.IsNullOrEmpty(orderType))
                        //{                            
                        //    if (orderType == "WAREHOUSE")
                        //    {
                        //        // remove fob item
                        //        dbOffer.SaleOrderMng_OfferLine_View.Where(o => o.OfferItemTypeID == 1).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLine_View.Remove(o));
                        //    }
                        //    else
                        //    {
                        //        // remove warehouse item
                        //        dbOffer.SaleOrderMng_OfferLine_View.Where(o => o.OfferItemTypeID == 2 || (o.OfferItemTypeID == 1 && string.IsNullOrEmpty(o.ApproverName))).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLine_View.Remove(o));
                        //    }
                        //}

                        if (!String.IsNullOrEmpty(orderType) && (orderType == "WAREHOUSE" || orderType == "FACTORY"))
                        {
                            if (orderType == "WAREHOUSE")
                            {
                                // remove factory item
                                dbOffer.SaleOrderMng_OfferLine_View.Where(o => o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLine_View.Remove(o));
                                //dbOffer.SaleOrderMng_OTCSparepart_View.Where(o => o.OfferSeasonTypeID == 4 || o.OfferSeasonTypeID == 5).ToList().ForEach(o => dbOffer.SaleOrderMng_OTCSparepart_View.Remove(o));
                            }
                            else
                            {
                                //remove warehouse item
                                dbOffer.SaleOrderMng_OfferLine_View.Where(o => o.OfferSeasonTypeID == 1 || o.OfferSeasonTypeID == null).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLine_View.Remove(o));
                                dbOffer.SaleOrderMng_OfferLineSparepart_View.Where(o => o.OfferSeasonTypeID == 1 || o.OfferSeasonTypeID == null).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLineSparepart_View.Remove(o));
                            }
                        }
                        else
                        {
                            dbOffer.SaleOrderMng_OfferLine_View.Clear();
                            dbOffer.SaleOrderMng_OfferLineSparepart_View.Clear();
                            orderType = null;
                        }

                        dbOffer.SaleOrderMng_OfferLine_View.Where(o => !o.Quantity.HasValue || o.Quantity == 0).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLine_View.Remove(o));
                        dbOffer.SaleOrderMng_OfferLineSparepart_View.Where(o => !o.Quantity.HasValue || o.Quantity == 0).ToList().ForEach(o => dbOffer.SaleOrderMng_OfferLineSparepart_View.Remove(o));

                        dtoItem.SaleOrderData = new DTO.SaleOrderMng.SaleOrder();
                        dtoItem.SaleOrderData.SaleOrderExtends = new List<DTO.SaleOrderMng.SaleOrderExtend>();
                        dtoItem.SaleOrderData.SaleOrderDetails = new List<DTO.SaleOrderMng.SaleOrderDetail>();
                        dtoItem.SaleOrderData.SaleOrderDetailSpareparts = new List<DTO.SaleOrderMng.SaleOrderDetailSparepart>();
                        dtoItem.SaleOrderData.SaleOrderDetailSamples = new List<DTO.SaleOrderMng.SaleOrderDetailSample>();
                        dtoItem.SaleOrderData.SaleOrderProductReturns = new List<DTO.SaleOrderMng.SaleOrderProductReturn>();
                        dtoItem.SaleOrderData.SaleOrderSparepartReturns = new List<DTO.SaleOrderMng.SaleOrderSparepartReturn>();

                        //SET INIT VALUE
                        dtoItem.SaleOrderData.OfferID = offerID;
                        dtoItem.SaleOrderData.OfferUD = dbOffer.OfferUD;
                        dtoItem.SaleOrderData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
                        dtoItem.SaleOrderData.PaymentTermID = dbOffer.PaymentTermID;
                        dtoItem.SaleOrderData.DeliveryTermID = dbOffer.DeliveryTermID;
                        dtoItem.SaleOrderData.PaymentTypeNM = dbOffer.PaymentTypeNM;
                        dtoItem.SaleOrderData.DeliveryTypeNM = dbOffer.DeliveryTypeNM;
                        //dtoItem.SaleOrderData.Season = DALBase.Helper.GetCurrentSeason();
                        dtoItem.SaleOrderData.Season = dbOffer.Season;
                        dtoItem.SaleOrderData.Conditions = "ALL PRICES INCLUDE 2% DAMAGE RISK";
                        dtoItem.SaleOrderData.Currency = dbOffer.Currency;
                        dtoItem.SaleOrderData.VATPercent = dbOffer.VAT;
                        dtoItem.SaleOrderData.IsViewDeliveryDateOnPrintout = true;
                        dtoItem.SaleOrderData.IsViewLDSOnPrintout = true;
                        dtoItem.SaleOrderData.IsViewQuantityContOnPrintout = true;
                        dtoItem.SaleOrderData.OrderType = orderType;
                        dtoItem.SaleOrderData.ClientID = dbOffer.ClientID;
                        int i = -1;
                        foreach (SaleOrderMng_OfferLine_View lineItem in dbOffer.SaleOrderMng_OfferLine_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderMng.SaleOrderDetail dtoDetail = new DTO.SaleOrderMng.SaleOrderDetail();

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
                            dtoDetail.RowIndex = lineItem.RowIndex;
                            dtoDetail.CommissionPercentOTC = lineItem.CommissionPercent;
                            dtoDetail.CommissionPercent = lineItem.CommissionPercent;


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

                            dtoDetail.SaleOrderDetailExtends = new List<DTO.SaleOrderMng.SaleOrderDetailExtend>();

                            dtoItem.SaleOrderData.SaleOrderDetails.Add(dtoDetail);
                            i = i - 1;
                        }

                        i = -1;
                        foreach (var item in dbOffer.SaleOrderMng_OfferLineSparepart_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderMng.SaleOrderDetailSparepart dtoSparepart = new DTO.SaleOrderMng.SaleOrderDetailSparepart();
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
                            dtoSparepart.RowIndex = item.RowIndex;
                            dtoSparepart.CommissionPercentOTC = item.CommissionPercent;
                            dtoSparepart.CommissionPercent = item.CommissionPercent;

                            dtoItem.SaleOrderData.SaleOrderDetailSpareparts.Add(dtoSparepart);

                            i = i - 1;
                        }

                        i = -1;
                        foreach (var item in dbOffer.SaleOrderMng_OfferLineSample_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderMng.SaleOrderDetailSample dtoSample = new DTO.SaleOrderMng.SaleOrderDetailSample();
                            dtoSample.SaleOrderDetailSampleID = i;
                            dtoSample.OfferLineSampleProductID = item.OfferLineSampleProductID;
                            dtoSample.ArticleCode = item.ArticleCode;
                            dtoSample.Description = item.Description;
                            dtoSample.Quantity = item.OfferSeasonDetailQuantity;
                            dtoSample.SalePrice = item.SalePrice;
                            dtoSample.RowIndex = item.RowIndex;
                            dtoSample.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(item.FileLocation) ? item.FileLocation : "no-image.jpg");
                            dtoSample.ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(item.ThumbnailLocation) ? item.ThumbnailLocation : "no-image.jpg");

                            dtoItem.SaleOrderData.SaleOrderDetailSamples.Add(dtoSample);
                        }
                    }
                    //get support data
                    dtoItem.DeliveryTerms = support_factory.GetDeliveryTerm().ToList();
                    dtoItem.PaymentTerms = support_factory.GetPaymentTerm().ToList();
                    dtoItem.Seasons = support_factory.GetSeason().ToList();
                    dtoItem.OrderTypes = GetOrderType().ToList();
                    dtoItem.ProductStatuses = support_factory.GetProductStatus().ToList();
                    dtoItem.CostTypes = GetCostTypes().ToList();
                    dtoItem.PaymentStatuses = GetPaymentStatus().ToList();
                    dtoItem.VATPercents = support_factory.GetVATPercent();
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
                return new DTO.SaleOrderMng.DataContainer();
            }
        }

        public DTO.SaleOrderMng.PIFormContainerDTO SearchSaleOrders(int offerID,out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new DAL.Support.DataFactory();
            totalRows = 0;
            DTO.SaleOrderMng.PIFormContainerDTO Data = new DTO.SaleOrderMng.PIFormContainerDTO();
            Data.Data = new List<DTO.SaleOrderMng.SaleOrderSearch>();
            Data.OrderTypes = new List<DTO.SaleOrderMng.OrderType>();

            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    totalRows = context.SaleOrderMng_SaleOrderSearch_View.Where(o => o.OfferID == offerID).Count();
                    Data.Data = converter.DB2DTO_SaleOrderSearch(context.SaleOrderMng_SaleOrderSearch_View.Where(o => o.OfferID == offerID).ToList());
                    Data.OrderTypes = GetOrderType().ToList();
                    Data.LinkInfos = getClientIDByOfferID(offerID);
                    Data.Seasons = support_factory.GetSeason().ToList();
                    Data.saleOrderStatusDTOs = converter.DB2DTO_SaleOrderStatus(context.List_TrackingStatus.ToList());
                    return Data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return Data;
        }

        public IEnumerable<DTO.SaleOrderMng.SaleOrderDetailSearch> SearchSaleOrderDetails(int saleOrderID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            //try to get data
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    totalRows = context.SaleOrderMng_SaleOrderDetailSearch_View.Where(o => o.SaleOrderID == saleOrderID).Count();
                    var result = context.SaleOrderMng_SaleOrderDetailSearch_View.Where(o => o.SaleOrderID == saleOrderID);
                    return converter.DB2DTO_SaleOrderDetailSearch(result.ToList());
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
                return new List<DTO.SaleOrderMng.SaleOrderDetailSearch>();
            }
        }

        private string GeneratePINo()
        {
            using (SaleOrderMngEntities context = CreateContext())
            {
                var orders = context.SaleOrder.Where(o =>(o.IsDeleted == null || o.IsDeleted == false) && o.ProformaInvoiceNo.Substring(0, 1) == "3").OrderByDescending(o => o.ProformaInvoiceNo);
                
                if (orders.ToList().Count() == 0)
                {
                    return "30000";
                }
                else
                {
                    var x = orders.FirstOrDefault();
                    int iPI = Convert.ToInt32(x.ProformaInvoiceNo) + 1;
                    return iPI.ToString();
                }
            }
        }

        private void SetPIStatus(int saleOrderID,int trackingStatusID ,int setStatusBy)
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

        public bool Confirm(int saleOrderID, ref DTO.SaleOrderMng.SaleOrder dtoItem, int setStatusBy, bool isConfirmWithoutSigned, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "PI has been confirmed success" };
            try
            {
                //SAVE DATA
                Library.DTO.Notification save_nofication = new Library.DTO.Notification{ Type = Library.DTO.NotificationType.Success};
                if (!UpdateData(saleOrderID, ref dtoItem, out save_nofication))
                {
                    notification = save_nofication;
                    return false;
                }
                
                //VALIDATE && CONFIRM
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrder dbSaleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                    OfferStatus dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == dbSaleOrder.OfferID && o.IsCurrentStatus == true).FirstOrDefault();
                    
                    if (dbOfferStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbOfferStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
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
                    if (dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.REVISED && dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.CREATED)
                    {
                        notification = new Library.DTO.Notification() { Message = "PI must be in REVISED/CREATED status before confirm", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }
                    //set tracking status
                    int TrackingStatusID = (dbSaleOrderStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);
                    SetPIStatus(saleOrderID, TrackingStatusID, setStatusBy);

                    //frozend PI printout data
                    context.SaleOrderMng_function_FrozenSaleOrderPrintoutData(saleOrderID);
                    //get back data
                    dtoItem.TrackingStatusNM = (dbSaleOrderStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.TEXT_STATUS_CONFIRMED : DALBase.Helper.TEXT_STATUS_REVISION_CONFIRMED);
                    dtoItem.TrackingStatusID = TrackingStatusID;

                    //// add item to quotation if needed
                    //context.FW_Quotation_function_AddFactoryOrderItem(null, saleOrderID, null); // table lockx and also check if item is available on sql server side

                    //New LDS Client type: Original
                    List<LDSClient> ldsList = context.LDSClient.Where(o => o.SaleOrderID == saleOrderID).ToList();
                    LDSClient lds = new LDSClient();
                    context.LDSClient.Add(lds);
                    if (ldsList.Count > 0)
                    {
                        var teamBestScores =
                            from player in ldsList
                            group player by player.LDSTypeID into playerGroup
                            select new
                            {
                                LDSTypeID = playerGroup.Key,
                                Version = playerGroup.Max(x => x.Version),
                            };
                        int? version = null;
                        foreach (var item in teamBestScores.ToList())
                        {
                            if(item.LDSTypeID == 1)
                            {
                                version = item.Version;
                            }
                        }
                        lds.SaleOrderID = saleOrderID;
                        lds.LDSTypeID = 1;
                        lds.Version = version + 1;
                        lds.LDS = dbSaleOrder.LDS;
                        lds.UpdatedBy = setStatusBy;
                        lds.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        lds.SaleOrderID = saleOrderID;
                        lds.LDSTypeID = 1;
                        lds.Version = 1;
                        lds.LDS = dbSaleOrder.LDS;
                        lds.UpdatedBy = setStatusBy;
                        lds.UpdatedDate = DateTime.Now;
                    }
                    
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

        public bool Revise(int saleOrderID, ref DTO.SaleOrderMng.SaleOrder dtoItem, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "PI has been revised success" };
            try
            {
                //SAVE DATA
                Library.DTO.Notification save_nofication = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
                if (!UpdateData(saleOrderID, ref dtoItem, out save_nofication))
                {
                    notification = save_nofication;
                    return false;
                }

                //VALIDATE && CONFIRM
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrderStatus dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                    if (dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                    {
                        notification = new Library.DTO.Notification() { Message = "PI must be in CONFIRMED status before revise", Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    SetPIStatus(saleOrderID, DALBase.Helper.REVISED, setStatusBy);
                    dtoItem.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_REVISED;
                    dtoItem.TrackingStatusID = DALBase.Helper.REVISED;

                    // add item to quotation if needed
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

        public DTO.SaleOrderMng.DataSearchContainer SearchDataContainer(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SaleOrderMng.DataSearchContainer dtoSearch = new DTO.SaleOrderMng.DataSearchContainer();
            DAL.Support.DataFactory dal_support = new Support.DataFactory();
            dtoSearch.OfferSearchs = GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification).ToList();
            dtoSearch.Seasons = dal_support.GetSeason().ToList();
            dtoSearch.Salers = dal_support.GetSaler().ToList();
            dtoSearch.OrderTypes = GetOrderType().ToList();

            return dtoSearch;
        }

        public DTO.SaleOrderMng.PIContainerPrintout GetPrintoutData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    SaleOrder_ReportView dbItem;
                    dbItem = context.SaleOrderMng_SaleOrder_ReportView
                        .Include("SaleOrderDetail_ReportView.SaleOrderDetailExtend_ReportView")
                        .Include("SaleOrderDetailSparepart_ReportView")
                        .Include("SaleOrderDetailSample_ReportView")
                        .Include("SaleOrderExtend_ReportView")
                        .FirstOrDefault(o => o.SaleOrderID == id);
                    return converter.DB2DTO_Printout(dbItem);
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
                return new DTO.SaleOrderMng.PIContainerPrintout();
            }


            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };   
            //MagentoDataFactory mFactory = new MagentoDataFactory();
            //DTO.SaleOrderMng.MagentoOrder order = new DTO.SaleOrderMng.MagentoOrder();
            //order.MagentoOrderDetails = new List<DTO.SaleOrderMng.MagentoOrderDetail>();


            //order.ClientUD = "000500";
            //order.Currency = "EUR";
            //order.UserID = 34;

            //DTO.SaleOrderMng.MagentoOrderDetail orderDetail = new DTO.SaleOrderMng.MagentoOrderDetail();
            //orderDetail.ArticleCode = "A3120105010400700005410A00";
            //orderDetail.Quantity = 10;
            //orderDetail.UnitPrice = 12;
            //order.MagentoOrderDetails.Add(orderDetail);

            //orderDetail = new DTO.SaleOrderMng.MagentoOrderDetail();
            //orderDetail.ArticleCode = "A3120105010401000005421A00";
            //orderDetail.Quantity = 20;
            //orderDetail.UnitPrice = 25;
            //order.MagentoOrderDetails.Add(orderDetail);

            //mFactory.CreateSaleOrderFromMangentoOrder(order, out notification);
            //return null;

        }

        private IEnumerable<DTO.SaleOrderMng.OrderType> GetOrderType()
        {
            List<DTO.SaleOrderMng.OrderType> OrderTypes = new List<DTO.SaleOrderMng.OrderType>();

            OrderTypes.Add(new DTO.SaleOrderMng.OrderType { OrderTypeValue = "FACTORY", OrderTypeText = "FACTORY" });
            //OrderTypes.Add(new DTO.SaleOrderMng.OrderType { OrderTypeValue = "FOB_WAREHOUSE", OrderTypeText = "FOB-WAREHOUSE" });
            OrderTypes.Add(new DTO.SaleOrderMng.OrderType { OrderTypeValue = "WAREHOUSE", OrderTypeText = "WAREHOUSE" });

            return OrderTypes;
        }

        private DTO.SaleOrderMng.LinkInfo getClientIDByOfferID(int offerID)
        {
            DTO.SaleOrderMng.LinkInfo linkInfo = new DTO.SaleOrderMng.LinkInfo();
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {

                    linkInfo = converter.DB2DTO_LinkInfo(context.SaleOrderMng_GetClientIDByOfferID_View.Where(o =>o.OfferID == offerID).FirstOrDefault());
                    return linkInfo;
                }
            }
            catch(Exception ex)
            {
                
            }
            return linkInfo;
        }

        public string GetReportOrderOverview(string Season,string OrderType,out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SaleOrderMng_fucntion_GetReportOrderOverview", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@OrderType", OrderType);
                adap.TableMappings.Add("Table", "SaleOrderMng_OrderOverview_ReportView");
                adap.TableMappings.Add("Table1", "SaleOrderMng_PIOverview_ReportView");
                adap.Fill(ds);
                ds.AcceptChanges();

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "OrderOverview");
                //return string.Empty;
                return DALBase.Helper.CreateReportFiles(ds, "OrderOverview");
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

        public bool ValidateStockQuantity(int saleOrderID, DTO.SaleOrderMng.SaleOrder dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Validate Stock Quantity Success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    //check product status
                    if (dtoItem.OrderType == "WAREHOUSE" || dtoItem.OrderType == "FOB_WAREHOUSE")
                    {
                        foreach (var item in dtoItem.SaleOrderDetails)
                        {
                            if (!item.ProductStatusID.HasValue)
                            {
                                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You have to fill-in product status for product : " + item.ArticleCode + " - " + item.Description };
                                return false;
                            }
                        }
                        foreach (var item in dtoItem.SaleOrderDetailSpareparts)
                        {
                            if (!item.ProductStatusID.HasValue)
                            {
                                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You have to fill-in product status for product : " + item.ArticleCode + " - " + item.Description };
                                return false;
                            }
                        }
                    }
                    
                    //checkk quantity
                    if (dtoItem.OrderType == "WAREHOUSE")
                    {
                        if (saleOrderID == 0)
                        {
                            int? ftsQnt = 0;
                            SaleOrderMng_StockOverview_View dbStock;
                            foreach (var item in dtoItem.SaleOrderDetails)
                            {
                                dbStock = context.SaleOrderMng_StockOverview_View.Where(o => o.ProductID == item.ProductID && o.ProductStatusID == item.ProductStatusID).FirstOrDefault();
                                ftsQnt = (dbStock == null ? 0 : (dbStock.FTSQnt.HasValue ? dbStock.FTSQnt.Value : 0));
                                if (ftsQnt < (item.OrderQnt.HasValue? item.OrderQnt.Value : 0))
                                {
                                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "Can not save. Do not enought quantity to reserve for product : " + item.ArticleCode + " - " + item.Description };
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //get previous ordertype of order
                            string orderType = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault().OrderType;    
                            int? needCompareQnt = 0;
                            int? ftsQnt = 0;

                            SaleOrderMng_SaleOrderDetail_View dbDetail;
                            SaleOrderMng_StockOverview_View dbStock;
                            foreach (var item in dtoItem.SaleOrderDetails)
                            {
                                //get current free to sale
                                dbStock = context.SaleOrderMng_StockOverview_View.Where(o => o.ProductID == item.ProductID && o.ProductStatusID == item.ProductStatusID).FirstOrDefault();
                                ftsQnt = (dbStock == null ? 0 : (dbStock.FTSQnt.HasValue ? dbStock.FTSQnt.Value : 0));
                                if (item.SaleOrderDetailID > 0)
                                {
                                    dbDetail = context.SaleOrderMng_SaleOrderDetail_View.Where(o => o.SaleOrderID == saleOrderID && o.ProductID == item.ProductID && o.ProductStatusID == item.ProductStatusID).FirstOrDefault();
                                    if (orderType == "WAREHOUSE") // this case to resolve user edit order with type WAREHOUSE 
                                    {
                                        needCompareQnt = (item.OrderQnt.HasValue?item.OrderQnt.Value:0) - (dbDetail != null && dbDetail.OrderQnt.HasValue ? dbDetail.OrderQnt.Value : 0);
                                    }
                                    else //this case to resolve case user edit order with type difference with WAREHOUSE then edit to order with type WAREHOUSE
                                    {
                                        needCompareQnt = (item.OrderQnt.HasValue?item.OrderQnt.Value:0);
                                    }
                                    //compare with stock
                                    if (ftsQnt < needCompareQnt)
                                    {
                                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "Can not save. Do not enought quantity to reserve for product : " + item.ArticleCode + " - " + item.Description };
                                        return false;
                                    }
                                }
                                else // this case to resolve case user add product to order
                                {                                                                        
                                    if (ftsQnt < (item.OrderQnt.HasValue ? item.OrderQnt.Value : 0))
                                    {
                                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "Can not save. Do not enought quantity to reserve for product : " + item.ArticleCode + " - " + item.Description };
                                        return false;
                                    }
                                }
                            }
                        }
                    }
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

        //public bool ValidateStockQuantity(int saleOrderID, DTO.SaleOrderMng.SaleOrder dtoItem, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Validate Stock Quantity Success" };
        //    return true;
        //}

        public IEnumerable<int> MultiConfirm(List<int> saleOrderIDs, int setStatusBy, bool isConfirmWithoutSigned, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                List<string> ErrorMessages = new List<string>();
                List<int> SuccessSaleOrderIDs = new List<int>();
                List<string> SuccessPINos = new List<string>();

                if (saleOrderIDs.Count() == 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You need select PIs to confirm" };
                    return SuccessSaleOrderIDs;
                }
                //VALIDATE && CONFIRM
                SaleOrder dbSaleOrder;
                OfferStatus dbOfferStatus;
                using (SaleOrderMngEntities context = CreateContext())
                {
                    for (int i = 0; i < saleOrderIDs.Count(); i++)
                    {
                        int saleOrderID = saleOrderIDs[i];
                        bool isValid = true;
                        dbSaleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        dbOfferStatus = context.OfferStatus.Where(o => o.OfferID == dbSaleOrder.OfferID && o.IsCurrentStatus == true).FirstOrDefault();

                        SaleOrderStatus dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID && o.IsCurrentStatus == true).FirstOrDefault();
                        if (dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.REVISED && dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.CREATED)
                        {
                            ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " : PI must be in REVISED/CREATED status before confirm</li>");
                            isValid = false;
                        }
                        
                        if (dbOfferStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbOfferStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                        {
                            ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " : Offer must be in CONFIRMED status before confirm PI</li>");
                            isValid = false;
                        }
                        else if (isConfirmWithoutSigned)
                        {
                            if (string.IsNullOrEmpty(dbSaleOrder.PIReceivedRemark))
                            {
                                ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " : PI must be filled-in [Signed PI Remark] before confirm</li>");
                                isValid = false;
                            }
                        }
                        else
                        {
                            if (dbSaleOrder.IsPIReceived == null || dbSaleOrder.IsPIReceived == false)
                            {
                                ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " : PI must be checked [Signed PI Received] before confirm</li>");
                                isValid = false;
                            }
                        }

                        if (isValid)
                        {
                            //set tracking status
                            int TrackingStatusID = (dbSaleOrderStatus.TrackingStatusID == DALBase.Helper.CREATED ? DALBase.Helper.CONFIRMED : DALBase.Helper.REVISION_CONFIRMED);
                            SetPIStatus(saleOrderID, TrackingStatusID, setStatusBy);

                            //frozend PI printout data
                            context.SaleOrderMng_function_FrozenSaleOrderPrintoutData(saleOrderID);
                            //get back data
                            SuccessSaleOrderIDs.Add(saleOrderID);
                            SuccessPINos.Add(dbSaleOrder.ProformaInvoiceNo);
                        }
                    }
                }

                string message = "<ul>";
                string successPINo = "";

                for (int i = 0; i < SuccessPINos.Count(); i++)
                {
                    successPINo += SuccessPINos[i] + ", ";
                }
                if (!string.IsNullOrEmpty(successPINo)) {
                    successPINo = successPINo.Substring(0, successPINo.Length - 2);
                    message += "<li>" + successPINo + " : Confirmed Successfull</li>";
                }

                for (int i = 0; i < ErrorMessages.Count(); i++)
                {
                    message += ErrorMessages[i];
                }
                message += "</ul>";

                
                if (ErrorMessages.Count() > 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = message };
                }
                else
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
                }
                return SuccessSaleOrderIDs;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return null;
            }
        }

        public IEnumerable<int> MultiRevise(List<int> saleOrderIDs, int setStatusBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                List<string> ErrorMessages = new List<string>();
                List<int> SuccessSaleOrderIDs = new List<int>();
                List<string> SuccessPINos = new List<string>();

                if (saleOrderIDs.Count() == 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You need select PIs to revise" };
                    return SuccessSaleOrderIDs;
                }
                //VALIDATE && REVISE
                using (SaleOrderMngEntities context = CreateContext())
                {
                    for (int i = 0; i < saleOrderIDs.Count(); i++)
                    {
                        int saleOrderID = saleOrderIDs[i];
                        bool isValid = true;
                        SaleOrder dbSaleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        SaleOrderStatus dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID && o.IsCurrentStatus == true).FirstOrDefault();

                        if (dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.CONFIRMED && dbSaleOrderStatus.TrackingStatusID != DALBase.Helper.REVISION_CONFIRMED)
                        {
                            ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " : PI must be in CONFIRMED status before revise</li>");
                            isValid = false;
                        }
                        if (isValid)
                        {
                            SetPIStatus(saleOrderID, DALBase.Helper.REVISED, setStatusBy);
                            SuccessSaleOrderIDs.Add(saleOrderID);
                            SuccessPINos.Add(dbSaleOrder.ProformaInvoiceNo);
                        }
                    }
                }

                string message = "<ul>";
                string successPINo = "";

                for (int i = 0; i < SuccessPINos.Count(); i++)
                {
                    successPINo += SuccessPINos[i] + ", ";
                }
                if (!string.IsNullOrEmpty(successPINo))
                {
                    successPINo = successPINo.Substring(0, successPINo.Length - 2);
                    message += "<li>" + successPINo + " : Revised Successfull</li>";
                }

                for (int i = 0; i < ErrorMessages.Count(); i++)
                {
                    message += ErrorMessages[i];
                }
                message += "</ul>";


                if (ErrorMessages.Count() > 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = message };
                }
                else
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
                }
                return SuccessSaleOrderIDs;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return null;
            }
        }

        public IEnumerable<int> MultiDelete(List<int> saleOrderIDs, int deletedBy, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                List<string> ErrorMessages = new List<string>();
                List<int> SuccessFactoryOrderIDs = new List<int>();
                List<string> SuccessFactoryOrderCodes = new List<string>();

                if (saleOrderIDs.Count() == 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = "You need select factory orders to delete" };
                    return SuccessFactoryOrderIDs;
                }
                //VALIDATE && CONFIRM
                SaleOrder dbSaleOrder;
                SaleOrderStatus dbSaleOrderStatus;
                using (SaleOrderMngEntities context = CreateContext())
                {
                    for (int i = 0; i < saleOrderIDs.Count(); i++)
                    {
                        int saleOrderID = saleOrderIDs[i];
                        bool isValid = true;
                        dbSaleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        dbSaleOrderStatus = context.SaleOrderStatus.Where(o => o.SaleOrderID == saleOrderID && o.IsCurrentStatus == true).FirstOrDefault();

                        if (dbSaleOrderStatus.TrackingStatusID == DALBase.Helper.CONFIRMED || dbSaleOrderStatus.TrackingStatusID == DALBase.Helper.REVISION_CONFIRMED)
                        {
                            ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " : Proforma Invoice was confirmed. You can not delete</li>");
                            isValid = false;
                        }

                        if (dbSaleOrderStatus.TrackingStatusID == DALBase.Helper.REVISED)
                        {
                            ErrorMessages.Add("<li>" + dbSaleOrder.ProformaInvoiceNo + " :  Proforma Invoice was revised. You can not delete</li>");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            context.SaleOrderMng_function_DeleteSaleOrder(saleOrderID, deletedBy);
                            SuccessFactoryOrderIDs.Add(saleOrderID);
                            SuccessFactoryOrderCodes.Add(dbSaleOrder.ProformaInvoiceNo);
                        }
                    }
                }

                string message = "<ul>";
                string successCode = "";

                for (int i = 0; i < SuccessFactoryOrderCodes.Count(); i++)
                {
                    successCode += SuccessFactoryOrderCodes[i] + ", ";
                }
                if (!string.IsNullOrEmpty(successCode))
                {
                    successCode = successCode.Substring(0, successCode.Length - 2);
                    message += "<li>" + successCode + " : Deleted Successfull</li>";
                }

                for (int i = 0; i < ErrorMessages.Count(); i++)
                {
                    message += ErrorMessages[i];
                }
                message += "</ul>";


                if (ErrorMessages.Count() > 0)
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Warning, Message = message };
                }
                else
                {
                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = message };
                }
                return SuccessFactoryOrderIDs;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return null;
            }
        }

        private IEnumerable<DTO.SaleOrderMng.CostType> GetCostTypes()
        {
            List<DTO.SaleOrderMng.CostType> CostTypes = new List<DTO.SaleOrderMng.CostType>();

            CostTypes.Add(new DTO.SaleOrderMng.CostType { CostTypeValue = "D", CostTypeText = "DISCOUNT" });
            CostTypes.Add(new DTO.SaleOrderMng.CostType { CostTypeValue = "T", CostTypeText = "TRANSPORTATION" });
            CostTypes.Add(new DTO.SaleOrderMng.CostType { CostTypeValue = "O", CostTypeText = "OTHER" });

            return CostTypes;
        }

        public IEnumerable<DTO.SaleOrderMng.PaymentStatus> GetPaymentStatus()
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
                return new List<DTO.SaleOrderMng.PaymentStatus>();
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

        public bool UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer, out DTO.SaleOrderMng.SaleOrder dtoSaleOrder, out Library.DTO.Notification notification)
        {
            Module.Framework.DAL.DataFactory framework_factory = new Module.Framework.DAL.DataFactory();
            dtoSaleOrder = new DTO.SaleOrderMng.SaleOrder();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Upload success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    string newPointer = framework_factory.CreateNoneImageFilePointer(this.tempFolder, newFile, oldPointer);
                    if (saleOrderID > 0)
                    {
                        var saleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        saleOrder.SignedPIFile = newPointer;
                        context.SaveChanges();
                        //get return data
                        dtoSaleOrder = GetData(saleOrderID, out notification);
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

        public bool UploadClientPOFile(int saleOrderID, string newFile, string oldPointer, out DTO.SaleOrderMng.SaleOrder dtoSaleOrder, out Library.DTO.Notification notification)
        {
            Module.Framework.DAL.DataFactory framework_factory = new Module.Framework.DAL.DataFactory();
            dtoSaleOrder = new DTO.SaleOrderMng.SaleOrder();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Upload success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    string newPointer = framework_factory.CreateNoneImageFilePointer(this.tempFolder, newFile, oldPointer);
                    if (saleOrderID > 0)
                    {
                        var saleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        saleOrder.ClientPOFile = newPointer;
                        context.SaveChanges();
                        //get return data
                        dtoSaleOrder = GetData(saleOrderID, out notification);
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
        public bool UploadLCFile(int saleOrderID, string newFile, string oldPointer, out DTO.SaleOrderMng.SaleOrder dtoSaleOrder, out Library.DTO.Notification notification)
        {
            Module.Framework.DAL.DataFactory framework_factory = new Module.Framework.DAL.DataFactory();
            dtoSaleOrder = new DTO.SaleOrderMng.SaleOrder();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Upload success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    string newPointer = framework_factory.CreateNoneImageFilePointer(this.tempFolder, newFile, oldPointer);
                    if (saleOrderID > 0)
                    {
                        var saleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        saleOrder.LCAttachFile = newPointer;
                        context.SaveChanges();
                        //get return data
                        dtoSaleOrder = GetData(saleOrderID, out notification);
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
        public DTO.SaleOrderMng.DataContainerOverview GetViewData(int id, int offerID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory support_factory = new DAL.Support.DataFactory();
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    DTO.SaleOrderMng.DataContainerOverview dtoItem = new DTO.SaleOrderMng.DataContainerOverview();

                    if (id > 0)
                    {
                        SaleOrderMng_SaleOrder_OverView dbItem;
                        dbItem = context.SaleOrderMng_SaleOrder_OverView
                                .Include("SaleOrderMng_SaleOrderDetail_OverView.SaleOrderMng_SaleOrderDetailExtend_OverView")
                                .Include("SaleOrderMng_SaleOrderExtend_OverView")
                                .Include("SaleOrderMng_SaleOrderDetailSparepart_OverView")
                                .Include("SaleOrderMng_WarehouseImport_OverView")
                                .FirstOrDefault(o => o.SaleOrderID == id);
                        DTO.SaleOrderMng.SaleOrderOverview SaleOrderDTOItem = converter.DB2DTO_SaleOrderOverView(dbItem);
                        dtoItem.SaleOrderData = SaleOrderDTOItem;
                        dtoItem.SaleOrderData.SumColFactoryOrder = 0;
                    }
                    else
                    {
                        SaleOrderMng_Offer_View dbOffer = context.SaleOrderMng_Offer_View
                                                    .Include("SaleOrderMng_OfferLine_View")
                                                    .Include("SaleOrderMng_OfferLineSparepart_View")
                                                    .FirstOrDefault(o => o.OfferID == offerID);

                        dtoItem.SaleOrderData = new DTO.SaleOrderMng.SaleOrderOverview();
                        dtoItem.SaleOrderData.SaleOrderExtends = new List<DTO.SaleOrderMng.SaleOrderExtendOverView>();
                        dtoItem.SaleOrderData.SaleOrderDetails = new List<DTO.SaleOrderMng.SaleOrderDetailOverView>();
                        dtoItem.SaleOrderData.SaleOrderDetailSpareparts = new List<DTO.SaleOrderMng.SaleOrderDetailSparepartOverView>();
                       

                        //SET INIT VALUE
                        dtoItem.SaleOrderData.OfferID = offerID;
                        dtoItem.SaleOrderData.OfferUD = dbOffer.OfferUD;
                        dtoItem.SaleOrderData.TrackingStatusNM = DALBase.Helper.TEXT_STATUS_CREATED;
                        dtoItem.SaleOrderData.PaymentTermID = dbOffer.PaymentTermID;
                        dtoItem.SaleOrderData.DeliveryTermID = dbOffer.DeliveryTermID;
                        dtoItem.SaleOrderData.PaymentTypeNM = dbOffer.PaymentTypeNM;
                        dtoItem.SaleOrderData.DeliveryTypeNM = dbOffer.DeliveryTypeNM;
                        dtoItem.SaleOrderData.Season = DALBase.Helper.GetCurrentSeason();
                        dtoItem.SaleOrderData.Conditions = "ALL PRICES INCLUDE 2% DAMAGE RISK";
                        dtoItem.SaleOrderData.Currency = dbOffer.Currency;
                        dtoItem.SaleOrderData.VATPercent = dbOffer.VAT;
                        dtoItem.SaleOrderData.IsViewDeliveryDateOnPrintout = true;
                        dtoItem.SaleOrderData.IsViewLDSOnPrintout = true;
                        dtoItem.SaleOrderData.IsViewQuantityContOnPrintout = true;
                        dtoItem.SaleOrderData.SumColFactoryOrder = 0;
                        int i = -1;
                        foreach (SaleOrderMng_OfferLine_View lineItem in dbOffer.SaleOrderMng_OfferLine_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderMng.SaleOrderDetailOverView dtoDetail = new DTO.SaleOrderMng.SaleOrderDetailOverView();

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

                            dtoDetail.SaleOrderDetailExtends = new List<DTO.SaleOrderMng.SaleOrderDetailExtendOverView>();

                            dtoItem.SaleOrderData.SaleOrderDetails.Add(dtoDetail);
                            i = i - 1;
                        }

                        i = -1;
                        foreach (var item in dbOffer.SaleOrderMng_OfferLineSparepart_View.OrderBy(o => o.RowIndex))
                        {
                            DTO.SaleOrderMng.SaleOrderDetailSparepartOverView dtoSparepart = new DTO.SaleOrderMng.SaleOrderDetailSparepartOverView();
                            dtoSparepart.SaleOrderDetailSparepartID = i;
                            dtoSparepart.OfferLineSparepartID = item.OfferLineSparepartID;
                            dtoSparepart.ArticleCode = item.ArticleCode;
                            dtoSparepart.Description = item.Description;
                            //dtoSparepart.OrderQnt = item.Quantity;
                            dtoSparepart.UnitPrice = item.UnitPrice;
                            dtoSparepart.ProductStatusID = 1;

                            dtoItem.SaleOrderData.SaleOrderDetailSpareparts.Add(dtoSparepart);

                            i = i - 1;
                        }

                    }

                    if(dtoItem.SaleOrderData.Season != null && dtoItem.SaleOrderData.ClientID != null)
                    {
                        dtoItem.FactoryOrderDetailOverViews = converter.DB2DTO_FactoryOrderDetailOverView(context.SaleOrderMng_FactoryOrderDetail_OverView.Where(o => o.Season == dtoItem.SaleOrderData.Season && o.ClientID == dtoItem.SaleOrderData.ClientID).OrderByDescending(item => item.LDS).ToList());
                        if(dtoItem.SaleOrderData.SaleOrderDetails.Count > 0)
                        {
                            var maxFactoryOrder = dtoItem.SaleOrderData.SaleOrderDetails.OrderByDescending(item => item.TotalFactoryOrderDetail).First();
                            dtoItem.SaleOrderData.SumColFactoryOrder = maxFactoryOrder.TotalFactoryOrderDetail;
                            foreach (var item in dtoItem.SaleOrderData.SaleOrderDetails.ToList())
                            {
                                var count = dtoItem.FactoryOrderDetailOverViews.Where(o => o.SaleOrderDetailID == item.SaleOrderDetailID).Count();
                                if (count < maxFactoryOrder.TotalFactoryOrderDetail)
                                {
                                    for (int i = count; i < maxFactoryOrder.TotalFactoryOrderDetail; i++)
                                    {
                                        DTO.SaleOrderMng.FactoryOrderDetailOverView factory = new DTO.SaleOrderMng.FactoryOrderDetailOverView();
                                        factory.SaleOrderDetailID = item.SaleOrderDetailID;
                                        factory.ShippedQnt = 0;
                                        dtoItem.FactoryOrderDetailOverViews.Add(factory);
                                    }
                                }
                            }
                        }  
                    }
                    else
                    {
                        dtoItem.FactoryOrderDetailOverViews = new List<DTO.SaleOrderMng.FactoryOrderDetailOverView>();
                    }
                    dtoItem.SaleOrderData.SumColFactoryOrder = dtoItem.SaleOrderData.SumColFactoryOrder * 350;

                    //Support
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
                return new DTO.SaleOrderMng.DataContainerOverview();
            }
        }

        public List<DTO.SaleOrderMng.SaleOrderDetail> GetOfferLine(int offerId, string orderType, string searchArt, out Library.DTO.Notification notification)
        {
            List<DTO.SaleOrderMng.SaleOrderDetail> data = new List<DTO.SaleOrderMng.SaleOrderDetail>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success};
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    if(searchArt != null && searchArt != "")
                    {
                        if (orderType == "WAREHOUSE")
                        {
                            data = converter.DB2DTO_OfferLine(context.SaleOrderMng_OfferLine_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1 && o.ArticleCode.Contains(searchArt)).ToList());
                        }
                        else
                        {
                            data = converter.DB2DTO_OfferLine(context.SaleOrderMng_OfferLine_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3) && o.ArticleCode.Contains(searchArt)).ToList());
                        }
                    }
                    else
                    {
                        if (orderType == "WAREHOUSE")
                        {
                            data = converter.DB2DTO_OfferLine(context.SaleOrderMng_OfferLine_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1).ToList());
                        }
                        else
                        {
                            data = converter.DB2DTO_OfferLine(context.SaleOrderMng_OfferLine_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 2 || o.OfferSeasonTypeID == 3)).ToList());
                        }
                    }

                    foreach (var item in data)
                    {
                        item.ProductStatusID = 1;
                        item.UnitPrice = item.OfferSalePrice;
                        item.SaleOrderDetailExtends = new List<DTO.SaleOrderMng.SaleOrderDetailExtend>();
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

        public List<DTO.SaleOrderMng.SaleOrderDetailSparepart> GetOfferLineSparepart(int offerId, string orderType, string searchArtSparepart, out Library.DTO.Notification notification)
        {
            List<DTO.SaleOrderMng.SaleOrderDetailSparepart> data = new List<DTO.SaleOrderMng.SaleOrderDetailSparepart>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    if(searchArtSparepart != null && searchArtSparepart != "")
                    {
                        if (orderType == "WAREHOUSE")
                        {
                            data = converter.DB2DTO_OfferLineSparepart(context.SaleOrderMng_OfferLineSparepart_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1 && o.ArticleCode.Contains(searchArtSparepart)).ToList());
                        }
                        else
                        {
                            data = converter.DB2DTO_OfferLineSparepart(context.SaleOrderMng_OfferLineSparepart_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 4 || o.OfferSeasonTypeID == 5) && o.ArticleCode.Contains(searchArtSparepart)).ToList());
                        }
                    }
                    else
                    {
                        if (orderType == "WAREHOUSE")
                        {
                            data = converter.DB2DTO_OfferLineSparepart(context.SaleOrderMng_OfferLineSparepart_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1).ToList());
                        }
                        else
                        {
                            data = converter.DB2DTO_OfferLineSparepart(context.SaleOrderMng_OfferLineSparepart_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 4 || o.OfferSeasonTypeID == 5)).ToList());
                        }
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

        public List<DTO.SaleOrderMng.SaleOrderDetailSample> GetOfferLineSample(int offerId, string orderType, string searchSample, out Library.DTO.Notification notification)
        {
            List<DTO.SaleOrderMng.SaleOrderDetailSample> data = new List<DTO.SaleOrderMng.SaleOrderDetailSample>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    if (searchSample != null && searchSample != "")
                    {
                        if (orderType == "WAREHOUSE")
                        {
                            data = converter.DB2DTO_OfferLineSample(context.SaleOrderMng_OfferLineSample_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1 && o.ArticleCode.Contains(searchSample)).ToList());
                        }
                        else
                        {
                            data = converter.DB2DTO_OfferLineSample(context.SaleOrderMng_OfferLineSample_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 6 || o.OfferSeasonTypeID == 7) && o.ArticleCode.Contains(searchSample)).ToList());
                        }
                    }
                    else
                    {
                        if (orderType == "WAREHOUSE")
                        {
                            data = converter.DB2DTO_OfferLineSample(context.SaleOrderMng_OfferLineSample_View.Where(o => o.OfferID == offerId && o.OfferSeasonTypeID == 1).ToList());
                        }
                        else
                        {
                            data = converter.DB2DTO_OfferLineSample(context.SaleOrderMng_OfferLineSample_View.Where(o => o.OfferID == offerId && (o.OfferSeasonTypeID == 6 || o.OfferSeasonTypeID == 7)).ToList());
                        }
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

        public List<DTO.SaleOrderMng.BizBloqsInvoice> BizBloqsImportData(List<DTO.SaleOrderMng.BizBloqsInvoice> bizBloqsInvoice, int? userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Import Success !!!" };
            List<DTO.SaleOrderMng.BizBloqsInvoice> data = new List<DTO.SaleOrderMng.BizBloqsInvoice>();
            List<DTO.SaleOrderMng.BizBloqsInvoice> validInvoice = new List<DTO.SaleOrderMng.BizBloqsInvoice>();
            List<DTO.SaleOrderMng.BizBloqsInvoice> inValidInvoice = new List<DTO.SaleOrderMng.BizBloqsInvoice>();
            try
            {
                using (var context = CreateContext())
                {
                    OfferSeason dbOfferSeason;
                    OfferSeasonDetail dbOfferSeasonDetail;
                    Offer dbOffer;
                    OfferLine dbOfferLine;
                    SaleOrder dbSaleOrder;
                    SaleOrderDetail dbSaleOrderDetail;
                    string season = Library.Helper.GetCurrentSeason();
                    
                    foreach (var invoiceItem in bizBloqsInvoice.Where(o => o.IsSelected.HasValue && o.IsSelected.Value))
                    {
                        //check bizBloqs order no
                        var saleOrder = context.SaleOrder.Where(o => o.BizBloqOrderNo == invoiceItem.OrderNumber).FirstOrDefault();
                        if (saleOrder != null)
                        {
                            inValidInvoice.Add(new DTO.SaleOrderMng.BizBloqsInvoice() { OrderNumber = invoiceItem.OrderNumber, Remark = "Order is exist " + invoiceItem.OrderNumber });
                            continue;
                        }

                        //check client
                        var client = context.Client.Where(o => o.ClientUD == invoiceItem.ClientUD).FirstOrDefault();
                        if (client == null)
                        {
                            inValidInvoice.Add(new DTO.SaleOrderMng.BizBloqsInvoice() { OrderNumber = invoiceItem.OrderNumber, Remark = "Client is not exist " + invoiceItem.ClientUD });
                            continue;
                        }

                        validInvoice.Add(new DTO.SaleOrderMng.BizBloqsInvoice() { OrderNumber = invoiceItem.OrderNumber });
                        //create offerSeason
                        dbOfferSeason = context.OfferSeason.Where(o => o.OfferSeasonTypeID == 1 && o.Season == season && o.ClientID == client.ClientID).FirstOrDefault();
                        if (dbOfferSeason == null)
                        {
                            dbOfferSeason = new OfferSeason();
                            context.OfferSeason.Add(dbOfferSeason);

                            dbOfferSeason.OfferSeasonTypeID = 1; //warehouse
                            dbOfferSeason.Season = season;
                            dbOfferSeason.ClientID = client.ClientID;
                            dbOfferSeason.Currency = invoiceItem.Currency;
                            dbOfferSeason.VAT = invoiceItem.VATPercentage;
                            dbOfferSeason.CreatedBy = userId;
                            dbOfferSeason.CreatedDate = DateTime.Now;
                        }

                        //create Offer
                        dbOffer = new Offer();
                        context.Offer.Add(dbOffer);

                        dbOffer.Season = season;
                        dbOffer.ClientID = client.ClientID;
                        dbOffer.SaleID = client.SaleID;
                        dbOffer.Currency = invoiceItem.Currency;
                        dbOffer.VAT = invoiceItem.VATPercentage;
                        dbOffer.CreatedBy = userId;
                        dbOffer.CreatedDate = DateTime.Now;
                        dbOffer.OfferDate = DateTime.Now;

                        //create SaleOrder
                        dbSaleOrder = new SaleOrder();
                        dbOffer.SaleOrder.Add(dbSaleOrder);

                        dbSaleOrder.BizBloqOrderNo = invoiceItem.OrderNumber;
                        dbSaleOrder.ProformaInvoiceNo = context.SaleOrderMng_function_GeneratePINo(-1, -1).FirstOrDefault();
                        dbSaleOrder.Season = season;
                        dbSaleOrder.InvoiceDate = invoiceItem.OrderDate.ConvertStringToDateTime();
                        dbSaleOrder.CreatedBy = userId;
                        dbSaleOrder.CreatedDate = DateTime.Now;
                        dbSaleOrder.UpdatedBy = userId;
                        dbSaleOrder.UpdatedDate = DateTime.Now;

                        //
                        //create OfferSeasonDetail, OfferLine, SaleOrderDetail
                        //
                        foreach (var lineItem in invoiceItem.InvoiceLines)
                        {
                            //create OfferSeasonDetail
                            dbOfferSeasonDetail = new OfferSeasonDetail();
                            dbOfferSeason.OfferSeasonDetail.Add(dbOfferSeasonDetail);

                            dbOfferSeasonDetail.Quantity = lineItem.Quantity;
                            dbOfferSeasonDetail.SalePrice = lineItem.UnitPrice;
                            dbOfferSeasonDetail.IsClientSelected = true;
                            dbOfferSeasonDetail.ItemStatus = 5; //approved
                            dbOfferSeasonDetail.CreatedBy = userId;
                            dbOfferSeasonDetail.CreatedDate = DateTime.Now;
                            dbOfferSeasonDetail.UpdatedBy = userId;
                            dbOfferSeasonDetail.UpdatedDate = DateTime.Now;
                            var product = context.Product.Where(o => o.ArticleCode == lineItem.ArticleCode).FirstOrDefault();
                            if (product == null)
                            {
                                inValidInvoice.Add(new DTO.SaleOrderMng.BizBloqsInvoice() { OrderNumber = invoiceItem.OrderNumber, Remark = "Order has item does not exist: " + lineItem.ArticleCode });
                            }
                            else
                            {
                                dbOfferSeasonDetail.ArticleCode = product.ArticleCode;
                                dbOfferSeasonDetail.Description = product.Description;
                                dbOfferSeasonDetail.ProductID = product.ProductID;
                                dbOfferSeasonDetail.ModelID = product.ModelID;
                                dbOfferSeasonDetail.MaterialID = product.MaterialID;
                                dbOfferSeasonDetail.MaterialTypeID = product.MaterialTypeID;
                                dbOfferSeasonDetail.MaterialColorID = product.MaterialColorID;
                                dbOfferSeasonDetail.FrameMaterialID = product.FrameMaterialID;
                                dbOfferSeasonDetail.FrameMaterialColorID = product.FrameMaterialColorID;
                                dbOfferSeasonDetail.SubMaterialID = product.SubMaterialID;
                                dbOfferSeasonDetail.SubMaterialColorID = product.SubMaterialColorID;
                                dbOfferSeasonDetail.BackCushionID = product.BackCushionID;
                                dbOfferSeasonDetail.SeatCushionID = product.SeatCushionID;
                                dbOfferSeasonDetail.CushionColorID = product.CushionColorID;
                                dbOfferSeasonDetail.FSCTypeID = product.FSCTypeID;
                                dbOfferSeasonDetail.FSCPercentID = product.FSCPercentID;
                            }

                            //create OfferLine
                            dbOfferLine = new OfferLine();
                            dbOfferSeasonDetail.OfferLine.Add(dbOfferLine);
                            dbOffer.OfferLine.Add(dbOfferLine);

                            dbOfferLine.Quantity = lineItem.Quantity;
                            dbOfferLine.UnitPrice = lineItem.UnitPrice;

                            //create SaleOrderDetail
                            dbSaleOrderDetail = new SaleOrderDetail();
                            dbSaleOrder.SaleOrderDetail.Add(dbSaleOrderDetail);
                            dbOfferLine.SaleOrderDetail.Add(dbSaleOrderDetail);

                            dbSaleOrderDetail.OrderQnt = lineItem.Quantity;
                            dbSaleOrderDetail.UnitPrice = lineItem.UnitPrice;
                            dbSaleOrderDetail.ProductStatusID = 1;
                        }
                    }
                    if (inValidInvoice.Count() > 0)
                    {
                        data = inValidInvoice;
                        throw new Exception("There are some order are invalid. Please read detail");
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
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return data;
            }
        }

        public bool SaveLDSClient(int saleOrderID, string ldsDate, int userID, string remark, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Success" };
            try
            {
                using (SaleOrderMngEntities context = CreateContext())
                {
                    LDSClient lds = new LDSClient();
                    context.LDSClient.Add(lds);
                    lds.SaleOrderID = saleOrderID;
                    lds.LDSTypeID = 0;
                    lds.Version = 0;
                    lds.LDS = ldsDate.ConvertStringToDateTime();
                    lds.UpdatedBy = userID;
                    lds.UpdatedDate = DateTime.Now;
                    lds.Remark = remark;

                    SaleOrder sale = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                    sale.LDS = ldsDate.ConvertStringToDateTime();
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
    }
}
