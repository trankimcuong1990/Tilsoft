using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace DAL.BackSaleOrderMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.BackSaleOrderMng.SaleOrderSearch, DTO.BackSaleOrderMng.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        
        private BackSaleOrderMngEntities CreateContext()
        {
            return new BackSaleOrderMngEntities(DALBase.Helper.CreateEntityConnectionString("BackSaleOrderMng"));
        }

        public override IEnumerable<DTO.BackSaleOrderMng.SaleOrderSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            int SaleID = 0;
            string ProformaInvoiceNo = string.Empty;
            string OrderType = string.Empty;
            string ArticleCode = string.Empty;
            string Description = string.Empty;
            string ClientArticleCode = string.Empty;
            string ClientArticleName = string.Empty;
            string ClientOrderNumber = string.Empty;
            string ClientEANCode = string.Empty;

            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("SaleID") && filters["SaleID"] != null) SaleID = Convert.ToInt32(filters["SaleID"]);
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("OrderType") && filters["OrderType"] != null) OrderType = filters["OrderType"].ToString();
            if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description")) Description = filters["Description"].ToString();
            if (filters.ContainsKey("ClientArticleCode")) ClientArticleCode = filters["ClientArticleCode"].ToString();
            if (filters.ContainsKey("ClientArticleName")) ClientArticleName = filters["ClientArticleName"].ToString();
            if (filters.ContainsKey("ClientOrderNumber")) ClientOrderNumber = filters["ClientOrderNumber"].ToString();
            if (filters.ContainsKey("ClientEANCode")) ClientEANCode = filters["ClientEANCode"].ToString();
            try
            {
                using (BackSaleOrderMngEntities context = CreateContext())
                {
                    totalRows = context.BackSaleOrderMng_function_SearchSaleOrder(orderBy,
                                                                            orderDirection,
                                                                            Season,
                                                                            ClientUD,
                                                                            ClientNM,
                                                                            SaleID,
                                                                            ProformaInvoiceNo,
                                                                            OrderType,
                                                                            ArticleCode,
                                                                            Description,
                                                                            ClientArticleCode,
                                                                            ClientArticleName,
                                                                            ClientOrderNumber,
                                                                            ClientEANCode
                                                                            ).Count();
                    var result = context.BackSaleOrderMng_function_SearchSaleOrder(orderBy,
                                                                            orderDirection,
                                                                            Season,
                                                                            ClientUD,
                                                                            ClientNM,
                                                                            SaleID,
                                                                            ProformaInvoiceNo,
                                                                            OrderType,
                                                                            ArticleCode,
                                                                            Description,
                                                                            ClientArticleCode,
                                                                            ClientArticleName,
                                                                            ClientOrderNumber,
                                                                            ClientEANCode
                                                                            );

                    return converter.DB2DTO_SaleOrderSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return  new List<DTO.BackSaleOrderMng.SaleOrderSearch>();
            }
        }

        public override DTO.BackSaleOrderMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Get success" };
            try
            {
                using (BackSaleOrderMngEntities context = CreateContext())
                {
                    DTO.BackSaleOrderMng.EditFormData data = new DTO.BackSaleOrderMng.EditFormData();
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_BackOrder(context.BackSaleOrderMng_BackOrder_View.Include("BackSaleOrderMng_BackOrderDetail_View").Where(o => o.BackOrderID == id).FirstOrDefault());
                    }
                    else
                    {
                        data.Data = new DTO.BackSaleOrderMng.BackOrder();
                    }
                    DAL.Support.DataFactory support_factory = new Support.DataFactory();
                    data.Seasons = support_factory.GetSeason().ToList();
                    data.Salers = support_factory.GetSaler().ToList();
                    data.PaymentTerms = support_factory.GetPaymentTerm().ToList();
                    data.DeliveryTerms = support_factory.GetDeliveryTerm().ToList();
                    data.VATPercent = support_factory.GetVATPercent();
                    data.Currency = support_factory.GetCurrency().ToList();
                    data.SaleOrderTypes = support_factory.GetSaleOrderType();
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
                return new DTO.BackSaleOrderMng.EditFormData();
            }            
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool UpdateData(int id, ref DTO.BackSaleOrderMng.EditFormData dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            return true;
        }

        public DTO.BackSaleOrderMng.SearchFilterData GetSearchFilter()
        {
            DTO.BackSaleOrderMng.SearchFilterData filterData = new DTO.BackSaleOrderMng.SearchFilterData();
            Support.DataFactory support_factory = new Support.DataFactory();
            filterData.Seasons = support_factory.GetSeason().ToList();
            filterData.Salers = support_factory.GetSaler().ToList();
            return filterData;
        }

        public IEnumerable<DTO.BackSaleOrderMng.GoodsRemaining> GetGoodsRemaining(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            int SaleID = 0;
            string ProformaInvoiceNo = string.Empty;
            string OrderType = string.Empty;
            string ArticleCode = string.Empty;
            string Description = string.Empty;
            string ClientArticleCode = string.Empty;
            string ClientArticleName = string.Empty;
            string ClientOrderNumber = string.Empty;
            string ClientEANCode = string.Empty;

            if (filters.ContainsKey("Season") && filters["Season"] != null) Season = filters["Season"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("SaleID") && filters["SaleID"] != null) SaleID = Convert.ToInt32(filters["SaleID"]);
            if (filters.ContainsKey("ProformaInvoiceNo")) ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString();
            if (filters.ContainsKey("OrderType") && filters["OrderType"] != null) OrderType = filters["OrderType"].ToString();
            if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description")) Description = filters["Description"].ToString();
            if (filters.ContainsKey("ClientArticleCode")) ClientArticleCode = filters["ClientArticleCode"].ToString();
            if (filters.ContainsKey("ClientArticleName")) ClientArticleName = filters["ClientArticleName"].ToString();
            if (filters.ContainsKey("ClientOrderNumber")) ClientOrderNumber = filters["ClientOrderNumber"].ToString();
            if (filters.ContainsKey("ClientEANCode")) ClientEANCode = filters["ClientEANCode"].ToString();
            try
            {
                using (BackSaleOrderMngEntities context = CreateContext())
                {
                    totalRows = context.BackSaleOrderMng_function_SearchGoodsRemaining( orderBy,
                                                                                        orderDirection,
                                                                                        Season,
                                                                                        ClientUD,
                                                                                        ClientNM,
                                                                                        SaleID,
                                                                                        ProformaInvoiceNo,
                                                                                        OrderType,
                                                                                        ArticleCode,
                                                                                        Description,
                                                                                        ClientArticleCode,
                                                                                        ClientArticleName,
                                                                                        ClientOrderNumber,
                                                                                        ClientEANCode
                                                                            ).Count();
                    var result = context.BackSaleOrderMng_function_SearchGoodsRemaining(orderBy,
                                                                                        orderDirection,
                                                                                        Season,
                                                                                        ClientUD,
                                                                                        ClientNM,
                                                                                        SaleID,
                                                                                        ProformaInvoiceNo,
                                                                                        OrderType,
                                                                                        ArticleCode,
                                                                                        Description,
                                                                                        ClientArticleCode,
                                                                                        ClientArticleName,
                                                                                        ClientOrderNumber,
                                                                                        ClientEANCode
                                                                                        );

                    return converter.DB2DTO_GoodsRemaining(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.BackSaleOrderMng.GoodsRemaining>();
            }
        }

        private DTO.BackSaleOrderMng.BackOrder GetBackOrder(int id)
        {
            DTO.BackSaleOrderMng.BackOrder data = new DTO.BackSaleOrderMng.BackOrder();
            using (BackSaleOrderMngEntities contex = CreateContext())
            {
                data = converter.DB2DTO_BackOrder(contex.BackSaleOrderMng_BackOrder_View.Include("BackSaleOrderMng_BackOrderDetail_View").Where(o => o.BackOrderID == id).FirstOrDefault());
            }
            return data;       
        }

        public bool UpdateBackOrder(int id,ref DTO.BackSaleOrderMng.BackOrder dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BackSaleOrderMngEntities context = CreateContext())
                {
                    //validate qnt inputed
                    foreach (var item in dtoItem.BackOrderDetails)
                    {
                        int? remainQnt = 0;
                        int? checkQnt = (item.BackOrderDetailID > 0 ? item.BackQnt - item.tempBackQnt : item.BackQnt);
                        if (item.OriginSaleOrderDetailID != null) //product
                        {
                            remainQnt = context.BackSaleOrderMng_GoodsRemaining_View.Where(o => o.DetailID == item.OriginSaleOrderDetailID && o.GoodsType == "PRODUCT").FirstOrDefault().RemainQnt;
                        }
                        else if (item.OriginSaleOrderDetailSparepartID != null) //sparepart
                        {
                            remainQnt = context.BackSaleOrderMng_GoodsRemaining_View.Where(o => o.DetailID == item.OriginSaleOrderDetailSparepartID && o.GoodsType == "SPAREPART").FirstOrDefault().RemainQnt;
                        }
                        if (checkQnt > remainQnt)
                        {
                            throw new Exception("Quantity(" + item.BackQnt +") must be less than quantity remaining : " + item.ArticleCode + " - " + item.Description);
                        }
                    }
                    //begin update 
                    Offer dbOffer = new Offer();
                    OfferStatus dbOfferStatus = new OfferStatus();
                    SaleOrder dbSaleOrder = new SaleOrder();
                    SaleOrderStatus dbSaleOrderStatus = new SaleOrderStatus();
                    BackOrder dbBackOrder = new BackOrder();

                    if (id > 0)
                    {
                        int? offerID = dtoItem.OfferID;
                        int? saleOrderID = dtoItem.SaleOrderID;
                        dbOffer = context.Offer.Where(o => o.OfferID == offerID).FirstOrDefault();
                        dbSaleOrder = context.SaleOrder.Where(o => o.SaleOrderID == saleOrderID).FirstOrDefault();
                        dbBackOrder = context.BackOrder.Where(o => o.BackOrderID == id).FirstOrDefault();
                    }
                    else
                    {
                        //create offer status
                        dbOfferStatus.TrackingStatusID = DALBase.Helper.CREATED;
                        dbOfferStatus.StatusDate = DateTime.Now;
                        dbOfferStatus.UpdatedBy = dtoItem.UpdatedBy;
                        dbOfferStatus.IsCurrentStatus = true;

                        //create sale order satatus
                        dbSaleOrderStatus.TrackingStatusID = DALBase.Helper.CREATED;
                        dbSaleOrderStatus.StatusDate = DateTime.Now;
                        dbSaleOrderStatus.UpdatedBy = dtoItem.UpdatedBy;
                        dbSaleOrderStatus.IsCurrentStatus = true;
                        
                        dbOffer.OfferStatus.Add(dbOfferStatus);
                        dbOffer.SaleOrder.Add(dbSaleOrder);
                        dbSaleOrder.SaleOrderStatus.Add(dbSaleOrderStatus);
                        dbSaleOrder.BackOrder.Add(dbBackOrder);

                        dbOffer.OfferUD = context.OfferMng_function_GenerateOfferCode(null, dbOffer.Season, dbOffer.SaleID, dbOffer.ClientID).FirstOrDefault();
                        dbSaleOrder.ProformaInvoiceNo = context.SaleOrderMng_function_GeneratePINo(dtoItem.UpdatedBy).FirstOrDefault();

                        context.Offer.Add(dbOffer);
                    }

                    dbOffer.ClientID = dtoItem.ClientID;
                    dbOffer.SaleID = dtoItem.SaleID;
                    dbOffer.Season = dtoItem.Season;
                    dbOffer.OfferVersion = 1;
                    dbOffer.Currency = dtoItem.Currency;
                    dbOffer.IsBackOffer = true;
                    dbOffer.CreatedBy = dtoItem.UpdatedBy;
                    dbOffer.CreatedDate = DateTime.Now;
                    

                    //create saleorder
                    dbSaleOrder.Season = dtoItem.Season;
                    dbSaleOrder.InvoiceDate = DateTime.Now;
                    dbSaleOrder.OrderType = "FOB_WAREHOUSE";
                    dbSaleOrder.PaymentTermID = dtoItem.PaymentTermID;
                    dbSaleOrder.DeliveryTermID = dtoItem.DeliveryTermID;
                    dbSaleOrder.VATPercent = dtoItem.VATPercent;
                    dbSaleOrder.CreatedBy = dtoItem.UpdatedBy;
                    dbSaleOrder.CreatedDate = DateTime.Now;
                    
                    //create back order
                    dbBackOrder.Season = dtoItem.Season;
                    dbBackOrder.ClientID = dtoItem.ClientID;
                    dbBackOrder.SaleID = dtoItem.SaleID;
                    dbBackOrder.PaymentTermID = dtoItem.PaymentTermID;
                    dbBackOrder.DeliveryTermID = dtoItem.DeliveryTermID;
                    dbBackOrder.VATPercent = dtoItem.VATPercent;
                    dbBackOrder.Currency = dtoItem.Currency;
                    dbBackOrder.OrderType = dtoItem.OrderType;
                    dbBackOrder.UpdatedBy = dtoItem.UpdatedBy;
                    dbBackOrder.UpdatedDate = DateTime.Now;
                    
                    //create offerline
                    var selected_products = from p in dtoItem.BackOrderDetails.Where(o => o.OriginSaleOrderDetailID != null)
                                            group p by new
                                            {
                                                p.ModelID,
                                                p.MaterialID,
                                                p.MaterialTypeID,
                                                p.MaterialColorID,
                                                p.FrameMaterialID,
                                                p.FrameMaterialColorID,
                                                p.SubMaterialID,
                                                p.SubMaterialColorID,
                                                p.SeatCushionID,
                                                p.BackCushionID,
                                                p.CushionColorID,
                                                p.FSCTypeID,
                                                p.FSCPercentID
                                            } into g
                                            select new
                                            {
                                                g.Key.ModelID,
                                                g.Key.MaterialID,
                                                g.Key.MaterialTypeID,
                                                g.Key.MaterialColorID,
                                                g.Key.FrameMaterialID,
                                                g.Key.FrameMaterialColorID,
                                                g.Key.SubMaterialID,
                                                g.Key.SubMaterialColorID,
                                                g.Key.SeatCushionID,
                                                g.Key.BackCushionID,
                                                g.Key.CushionColorID,
                                                g.Key.FSCTypeID,
                                                g.Key.FSCPercentID,
                                                TotalBackQnt = g.Sum(x => x.BackQnt)
                                            };
                    OfferLine dbOfferLine;
                    SaleOrderDetail dbSaleOrderDetail;
                    BackOrderDetail dbBackOrderDetail;
                    foreach (var item in selected_products)
                    {
                        var dtoProduct = dtoItem.BackOrderDetails.Where(o => o.OriginSaleOrderDetailID !=null &&
                                                                            o.ModelID == item.ModelID && 
                                                                            o.MaterialID == item.MaterialID && o.MaterialTypeID == item.MaterialTypeID && o.MaterialColorID == item.MaterialColorID &&
                                                                            o.FrameMaterialID == item.FrameMaterialID && o.FrameMaterialColorID == item.FrameMaterialColorID &&
                                                                            o.SubMaterialID == item.SubMaterialID && o.SubMaterialColorID == item.SubMaterialColorID &&
                                                                            o.SeatCushionID == item.SeatCushionID && o.BackCushionID == item.BackCushionID && o.CushionColorID == item.CushionColorID &&
                                                                            o.FSCTypeID == item.FSCTypeID && o.FSCPercentID == item.FSCPercentID
                                                                       );
                        dbOfferLine = context.OfferLine.Where(o => o.Offer.OfferID == dbOffer.OfferID && o.ModelID == item.ModelID & o.MaterialID == item.MaterialID && o.MaterialTypeID == item.MaterialTypeID && o.MaterialColorID == item.MaterialColorID &&
                                                                            o.FrameMaterialID == item.FrameMaterialID && o.FrameMaterialColorID == item.FrameMaterialColorID &&
                                                                            o.SubMaterialID == item.SubMaterialID && o.SubMaterialColorID == item.SubMaterialColorID &&
                                                                            o.SeatCushionID == item.SeatCushionID && o.BackCushionID == item.BackCushionID && o.CushionColorID == item.CushionColorID &&
                                                                            o.FSCTypeID == item.FSCTypeID && o.FSCPercentID == item.FSCPercentID).FirstOrDefault();
                        if (dbOfferLine != null)
                        {
                            dbOfferLine.Quantity = item.TotalBackQnt;
                            dbSaleOrderDetail = context.SaleOrderDetail.Where(o => o.SaleOrder.SaleOrderID == dbSaleOrder.SaleOrderID  && o.OfferLine.OfferLineID == dbOfferLine.OfferLineID).FirstOrDefault();
                            dbSaleOrderDetail.OrderQnt = item.TotalBackQnt;
                        }
                        else {
                            dbOfferLine = new OfferLine();
                            dbOfferLine.ArticleCode = dtoProduct.FirstOrDefault().ArticleCode;
                            dbOfferLine.Description = dtoProduct.FirstOrDefault().Description;
                            dbOfferLine.ModelID = item.ModelID;
                            dbOfferLine.MaterialID = item.MaterialID;
                            dbOfferLine.MaterialTypeID = item.MaterialTypeID;
                            dbOfferLine.MaterialColorID = item.MaterialColorID;
                            dbOfferLine.FrameMaterialID = item.FrameMaterialID;
                            dbOfferLine.FrameMaterialColorID = item.FrameMaterialColorID;
                            dbOfferLine.SubMaterialID = item.SubMaterialID;
                            dbOfferLine.SubMaterialColorID = item.SubMaterialColorID;
                            dbOfferLine.SeatCushionID = item.SeatCushionID;
                            dbOfferLine.BackCushionID = item.BackCushionID;
                            dbOfferLine.CushionColorID = item.CushionColorID;
                            dbOfferLine.FSCTypeID = item.FSCTypeID;
                            dbOfferLine.FSCPercentID = item.FSCPercentID;
                            dbOfferLine.Quantity = item.TotalBackQnt;
                            dbOffer.OfferLine.Add(dbOfferLine);

                            //create saleorderdetail
                            dbSaleOrderDetail = new SaleOrderDetail();
                            dbSaleOrderDetail.OrderQnt = item.TotalBackQnt;
                            dbSaleOrderDetail.ProductStatusID = 1;
                            dbSaleOrder.SaleOrderDetail.Add(dbSaleOrderDetail);
                            dbOfferLine.SaleOrderDetail.Add(dbSaleOrderDetail);
                        }
                        //create back sale order
                        foreach (var p in dtoProduct)
                        {
                            if (p.BackOrderDetailID < 0)
                            {
                                dbBackOrderDetail = new BackOrderDetail();
                                dbBackOrderDetail.OriginQnt = p.OrderQnt;
                                dbBackOrderDetail.BackQnt = p.BackQnt;
                                dbBackOrderDetail.OriginSaleOrderDetail = context.SaleOrderDetail.Where(o => o.SaleOrderDetailID == p.OriginSaleOrderDetailID).FirstOrDefault();
                                dbBackOrderDetail.BackSaleOrderDetail = dbSaleOrderDetail;
                                dbBackOrder.BackOrderDetail.Add(dbBackOrderDetail);
                            }
                            else {
                                dbBackOrderDetail = context.BackOrderDetail.Where(o => o.BackOrderDetailID == p.BackOrderDetailID).FirstOrDefault();
                                dbBackOrderDetail.BackQnt = p.BackQnt;
                            }
                            
                        }
                    }

                    //create offerline sparepart 
                    var selected_spareparts = from p in dtoItem.BackOrderDetails.Where(o => o.OriginSaleOrderDetailSparepartID != null)
                                            group p by new
                                            {
                                                p.ModelID,
                                                p.PartID,
                                            } into g
                                            select new
                                            {
                                                g.Key.ModelID,
                                                g.Key.PartID,
                                                TotalBackQnt = g.Sum(x => x.BackQnt)
                                            };
                    OfferLineSparepart dbOfferLineSparepart;
                    SaleOrderDetailSparepart dbSaleOrderDetailSparepart;
                    foreach (var item in selected_spareparts)
                    {
                        var dtoSparepart = dtoItem.BackOrderDetails.Where(o => o.OriginSaleOrderDetailSparepartID != null &&
                                                                            o.ModelID == item.ModelID &&
                                                                            o.PartID == item.PartID 
                                                                       );
                        dbOfferLineSparepart = context.OfferLineSparepart.Where(o => o.Offer.OfferID == dbOffer.OfferID && o.ModelID == item.ModelID & o.PartID == item.PartID).FirstOrDefault();
                        if (dbOfferLineSparepart != null)
                        {
                            dbOfferLineSparepart.Quantity = item.TotalBackQnt;
                            dbSaleOrderDetailSparepart = context.SaleOrderDetailSparepart.Where(o => o.SaleOrder.SaleOrderID == dbSaleOrder.SaleOrderID && o.OfferLineSparepart.OfferLineSparePartID == dbOfferLineSparepart.OfferLineSparePartID).FirstOrDefault();
                            dbSaleOrderDetailSparepart.OrderQnt = item.TotalBackQnt;
                        }
                        else
                        {
                            dbOfferLineSparepart = new OfferLineSparepart();
                            dbOfferLineSparepart.ArticleCode = dtoSparepart.FirstOrDefault().ArticleCode;
                            dbOfferLineSparepart.Description = dtoSparepart.FirstOrDefault().Description;
                            dbOfferLineSparepart.ModelID = item.ModelID;
                            dbOfferLineSparepart.PartID = item.PartID;
                            dbOffer.OfferLineSparepart.Add(dbOfferLineSparepart);

                            //create saleorderdetail sparepart
                            dbSaleOrderDetailSparepart = new SaleOrderDetailSparepart();
                            dbSaleOrderDetailSparepart.OrderQnt = item.TotalBackQnt;
                            dbSaleOrderDetailSparepart.ProductStatusID = 1;
                            dbSaleOrder.SaleOrderDetailSparepart.Add(dbSaleOrderDetailSparepart);
                            dbOfferLineSparepart.SaleOrderDetailSparepart.Add(dbSaleOrderDetailSparepart);
                        }
                        //create back sale order
                        foreach (var s in dtoSparepart)
                        {
                            if (s.BackOrderDetailID < 0)
                            {
                                dbBackOrderDetail = new BackOrderDetail();
                                dbBackOrderDetail.OriginQnt = s.OrderQnt;
                                dbBackOrderDetail.BackQnt = s.BackQnt;
                                dbBackOrderDetail.OriginSaleOrderDetailSparepart = context.SaleOrderDetailSparepart.Where(o => o.SaleOrderDetailSparepartID == s.OriginSaleOrderDetailSparepartID).FirstOrDefault();
                                dbBackOrderDetail.BackSaleOrderDetailSparepart = dbSaleOrderDetailSparepart;
                                dbBackOrder.BackOrderDetail.Add(dbBackOrderDetail);
                            }
                            else
                            {
                                dbBackOrderDetail = context.BackOrderDetail.Where(o => o.BackOrderDetailID == s.BackOrderDetailID).FirstOrDefault();
                                dbBackOrderDetail.BackQnt = s.BackQnt;
                            }
                        }
                    }

                    //save data
                    context.SaveChanges();
                    
                    //return data
                    dtoItem = this.GetBackOrder(dbBackOrder.BackOrderID);
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
    }
}
