using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SaleOrderMng
{
    public  class MagentoDataFactory
    {
        private DataConverter converter = new DataConverter();
        private MagentoSaleOrderEntities CreateContext()
        {
            return new MagentoSaleOrderEntities(DALBase.Helper.CreateEntityConnectionString("MagentoSaleOrderModel"));
        }

        public int? CreateSaleOrderFromMangentoOrder(InnerDTO.MagentoOrder magentoOrder, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (MagentoSaleOrderEntities context = CreateContext())
                {
                    //get client info
                    var client = context.Client.Where(o => o.ClientUD == magentoOrder.ClientUD).FirstOrDefault();

                    //get userID
                    int? saleID = client.SaleID;
                    int? userID = null;
                    var sale = context.Sale.Where(o => o.SaleID == saleID).FirstOrDefault();
                    if (sale != null)
                    {
                        userID = sale.UserID;
                    }
                    if (!userID.HasValue)
                    {
                        userID = magentoOrder.UserID;
                    }

                    //create offer
                    Offer_Magento dbOffer = new Offer_Magento();
                    context.Offer.Add(dbOffer);
                    dbOffer.ClientID = client.ClientID;
                    dbOffer.SaleID = client.SaleID;
                    dbOffer.Season = Library.Helper.GetCurrentSeason();
                    dbOffer.OfferVersion = 1;
                    dbOffer.Currency = magentoOrder.Currency;
                    dbOffer.CreatedDate = DateTime.Now;
                    dbOffer.CreatedBy = userID;
                    dbOffer.OfferUD = context.OfferMng_function_GenerateOfferCode_Magento(null, dbOffer.Season, dbOffer.SaleID, client.ClientID).FirstOrDefault();

                    //create offer status
                    OfferStatus_Magento dbOfferStatus = new OfferStatus_Magento();
                    dbOffer.OfferStatus.Add(dbOfferStatus);
                    dbOfferStatus.TrackingStatusID = DALBase.Helper.CREATED;
                    dbOfferStatus.StatusDate = DateTime.Now;
                    dbOfferStatus.UpdatedBy = userID;
                    dbOfferStatus.IsCurrentStatus = true;
                    
                    //create saleOrder
                    SaleOrder_Magento dbSaleOrder = new SaleOrder_Magento();
                    dbOffer.SaleOrder.Add(dbSaleOrder);
                    dbSaleOrder.Season = Library.Helper.GetCurrentSeason();
                    dbSaleOrder.InvoiceDate = DateTime.Now;
                    dbSaleOrder.OrderType = "WAREHOUSE";
                    dbSaleOrder.ProformaInvoiceNo = context.SaleOrderMng_function_GeneratePINo_Magento(userID).FirstOrDefault();
                    dbSaleOrder.CreatedBy = userID;
                    dbSaleOrder.CreatedDate = DateTime.Now;
                    dbSaleOrder.Remark = magentoOrder.MagentoOrderUD;

                    //create saleOrder status
                    SaleOrderStatus_Magento dbSaleOrderStatus = new SaleOrderStatus_Magento();
                    dbSaleOrder.SaleOrderStatus.Add(dbSaleOrderStatus);
                    dbSaleOrderStatus.TrackingStatusID = DALBase.Helper.CREATED;
                    dbSaleOrderStatus.StatusDate = DateTime.Now;
                    dbSaleOrderStatus.UpdatedBy = userID;
                    dbSaleOrderStatus.IsCurrentStatus = true;

                    //create product for offeLine & saleOrderDetail
                    OfferLine_Magento dbOfferLine = new OfferLine_Magento();
                    SaleOrderDetail_Magento dbSaleOrderDetail = new SaleOrderDetail_Magento();
                    Product_Magento dbProduct = new Product_Magento();
                    foreach (var item in magentoOrder.MagentoOrderDetails)
                    {
                        //get product info
                        dbProduct = context.Product.Where(o => o.ArticleCode == item.ArticleCode).FirstOrDefault();

                        //create offer line
                        dbOfferLine = new OfferLine_Magento();
                        dbOffer.OfferLine.Add(dbOfferLine);
                        dbOfferLine.ArticleCode = dbProduct.ArticleCode;
                        dbOfferLine.Description = dbProduct.Description;
                        dbOfferLine.ModelID = dbProduct.ModelID;
                        dbOfferLine.MaterialID = dbProduct.MaterialID;
                        dbOfferLine.MaterialTypeID = dbProduct.MaterialTypeID;
                        dbOfferLine.MaterialColorID = dbProduct.MaterialColorID;
                        dbOfferLine.FrameMaterialID = dbProduct.FrameMaterialID;
                        dbOfferLine.FrameMaterialColorID = dbProduct.FrameMaterialColorID;
                        dbOfferLine.SubMaterialID = dbProduct.SubMaterialID;
                        dbOfferLine.SubMaterialColorID = dbProduct.SubMaterialColorID;
                        dbOfferLine.SeatCushionID = dbProduct.SeatCushionID;
                        dbOfferLine.BackCushionID = dbProduct.BackCushionID;
                        dbOfferLine.CushionColorID = dbProduct.CushionColorID;
                        dbOfferLine.FSCTypeID = dbProduct.FSCTypeID;
                        dbOfferLine.FSCPercentID = dbProduct.FSCPercentID;
                        dbOfferLine.Quantity = item.Quantity;
                        dbOfferLine.UnitPrice = item.UnitPrice;

                        //create saleOrderDetail
                        dbSaleOrderDetail = new SaleOrderDetail_Magento();
                        dbSaleOrder.SaleOrderDetail.Add(dbSaleOrderDetail);
                        dbSaleOrderDetail.OrderQnt = item.Quantity;
                        dbSaleOrderDetail.UnitPrice = item.UnitPrice;
                        dbSaleOrderDetail.ProductStatusID = 1;
                        dbOfferLine.SaleOrderDetail.Add(dbSaleOrderDetail);
                    }
                    context.Offer.Add(dbOffer);
                    context.SaveChanges();
                    return dbOffer.SaleOrder.FirstOrDefault().SaleOrderID;
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
                return null;
            }
        }

    }
}
