using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DAL
{
    public class DataFactorySaleOrderReturn
    {
        private DataConverter converter = new DataConverter();
        private SaleOrderReturnEntities CreateContext()
        {
            return new SaleOrderReturnEntities(Library.Helper.CreateEntityConnectionString("SaleOrderReturnModel"));
        }

        public IEnumerable<DTO.LoadingPlan> GetLoadingPlan(int saleOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SaleOrderReturnEntities context = CreateContext())
                {
                    List<int?> loadingPlanIDs = context.SaleOrderMng_LoadingPlanDetail_View.Where(o => o.SaleOrderID == saleOrderID).Select(x => x.LoadingPlanID).ToList();
                    loadingPlanIDs.AddRange(context.SaleOrderMng_LoadingPlanSparepartDetail_View.Where(o => o.SaleOrderID == saleOrderID).Select(x => x.LoadingPlanID).ToList());
                    loadingPlanIDs = loadingPlanIDs.Distinct().ToList();

                    var result = context.SaleOrderMng_LoadingPlan_View.Include("SaleOrderMng_LoadingPlanDetail_View").Include("SaleOrderMng_LoadingPlanSparepartDetail_View").Where(o => loadingPlanIDs.Contains(o.LoadingPlanID));
                    return converter.DB2DTO_LoadingPlan(result.ToList());
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
                return new List<DTO.LoadingPlan>();
            }
        }

        /// <summary>
        /// Hien thoi khong su dung ham nay (entities dang bi loi, sau nay se bo)
        /// </summary>
        /// <param name="dtoItem"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool CreateReturnData(object dtoItems, out Library.DTO.Notification notification)
        {
            DTO.SaleOrder dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoItems).ToObject<DTO.SaleOrder>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "You have been returned goods success" };

            if (dtoItem.SaleOrderID <= 0)
            {
                throw new Exception("You need save PI before return data");
            }

            try
            {
                using (SaleOrderReturnEntities context = CreateContext())
                {
                    //check quantity return that user entered
                    foreach (var item in dtoItem.SaleOrderProductReturns.Where(o => o.SaleOrderProductReturnID < 0))
                    {
                        var dbRemain = context.SaleOrderMng_LoadingPlanDetail_View.Where(o => o.LoadingPlanDetailID == item.LoadingPlanDetailID).FirstOrDefault();
                        if (dbRemain != null && item.OrderQnt > dbRemain.RemaingReturnQnt)
                        {
                            throw new Exception("Quantity of product: " + item.ArticleCode + "(" + item.Description + ") must be less than quantity of return remaining");
                        }
                    }

                    foreach (var item in dtoItem.SaleOrderSparepartReturns.Where(o => o.SaleOrderSparepartReturnID < 0))
                    {
                        var dbRemain = context.SaleOrderMng_LoadingPlanSparepartDetail_View.Where(o => o.LoadingPlanSparepartDetailID == item.LoadingPlanSparepartDetailID).FirstOrDefault();
                        if (dbRemain != null && item.OrderQnt > dbRemain.RemaingReturnQnt)
                        {
                            throw new Exception("Quantity of product: " + item.ArticleCode + "(" + item.Description + ") must be less than quantity of return remaining");
                        }
                    }

                    //get reuturn index
                    int? returnIdex = 1;
                    int? returnIdexSparepart = 1;

                    if (dtoItem.SaleOrderProductReturns.Where(o => o.SaleOrderProductReturnID > 0).Count() > 0)
                    {
                        returnIdex = dtoItem.SaleOrderProductReturns.OrderByDescending(o => o.ReturnIndex).FirstOrDefault().ReturnIndex + 1;
                    }
                    if (dtoItem.SaleOrderSparepartReturns.Where(o => o.SaleOrderSparepartReturnID > 0).Count() > 0)
                    {
                        returnIdexSparepart = dtoItem.SaleOrderSparepartReturns.OrderByDescending(o => o.ReturnIndex).FirstOrDefault().ReturnIndex + 1;
                    }

                    //create offer and fobwarehouse saleorder to prepare to import data to warehouse
                    Offer_Return dbOffer = new Offer_Return();
                    dbOffer.ClientID = 70560; //EUROFAR
                    dbOffer.SaleID = 23;//IRON
                    dbOffer.Season = dtoItem.Season;
                    dbOffer.OfferVersion = 1;
                    dbOffer.Currency = dtoItem.Currency;
                    dbOffer.OfferUD = context.OfferMng_function_GenerateOfferCode(null, dbOffer.Season, dbOffer.SaleID, dbOffer.ClientID).FirstOrDefault();

                    OfferStatus_Return dbOfferStatus = new OfferStatus_Return();
                    dbOfferStatus.TrackingStatusID = Library.Helper.CREATED;
                    dbOfferStatus.StatusDate = DateTime.Now;
                    dbOfferStatus.UpdatedBy = dtoItem.UpdatedBy;
                    dbOfferStatus.IsCurrentStatus = true;
                    dbOffer.OfferStatus.Add(dbOfferStatus);

                    SaleOrder_Return dbSaleOrder = new SaleOrder_Return();
                    dbSaleOrder.Season = dtoItem.Season;
                    dbSaleOrder.InvoiceDate = DateTime.Now;
                    dbSaleOrder.OrderType = "FOB_WAREHOUSE";
                    dbSaleOrder.ProformaInvoiceNo = context.SaleOrderMng_function_GeneratePINo(371,dtoItem.UpdatedBy).FirstOrDefault();
                    dbSaleOrder.CreatedBy = dtoItem.UpdatedBy;
                    dbSaleOrder.CreatedDate = DateTime.Now;
                    dbSaleOrder.OriginSaleOrderID = dtoItem.SaleOrderID;
                    dbOffer.SaleOrder.Add(dbSaleOrder);

                    SaleOrderStatus_Return dbSaleOrderStatus = new SaleOrderStatus_Return();
                    dbSaleOrderStatus.TrackingStatusID = Library.Helper.CREATED;
                    dbSaleOrderStatus.StatusDate = DateTime.Now;
                    dbSaleOrderStatus.UpdatedBy = dtoItem.UpdatedBy;
                    dbSaleOrderStatus.IsCurrentStatus = true;
                    dbSaleOrder.SaleOrderStatus.Add(dbSaleOrderStatus);

                    WarehouseImport dbWarehouseImport = new WarehouseImport();
                    dbWarehouseImport.ReceiptNo = context.SaleOrderMng_funtions_GenerateWarehouseImportReceiptNo(dtoItem.Season).FirstOrDefault();
                    dbWarehouseImport.ImportedDate = DateTime.Now;
                    dbWarehouseImport.IsConfirmed = true;
                    dbWarehouseImport.UpdatedBy = dtoItem.UpdatedBy;
                    dbWarehouseImport.UpdatedDate = DateTime.Now;
                    dbWarehouseImport.ConfirmedBy = dtoItem.UpdatedBy;
                    dbWarehouseImport.ConfirmedDate = DateTime.Now;
                    dbWarehouseImport.ImportTypeID = 2;
                    dbWarehouseImport.Season = dtoItem.Season;

                    //create offerline
                    var selected_products = from p in dtoItem.SaleOrderProductReturns.Where(o => o.SaleOrderProductReturnID < 0)
                                            group p by new
                                            {
                                                p.ProductID,
                                                p.ArticleCode,
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
                                                g.Key.ProductID,
                                                g.Key.ArticleCode,
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
                                                TotalQnt = g.Sum(x => x.OrderQnt)
                                            };
                    OfferLine_Return dbOfferLine = new OfferLine_Return();
                    SaleOrderDetail_Return dbSaleOrderDetail = new SaleOrderDetail_Return();
                    SaleOrderProductReturn dbProductReturn = new SaleOrderProductReturn();
                    WarehouseImportProductDetail dbWarehouseImportProduct = new WarehouseImportProductDetail();
                    foreach (var item in selected_products)
                    {
                        dbOfferLine = new OfferLine_Return();
                        dbOfferLine.ArticleCode = item.ArticleCode;
                        dbOfferLine.Description = dtoItem.SaleOrderProductReturns.Where(o => o.ProductID == item.ProductID).FirstOrDefault().Description;
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
                        dbOfferLine.Quantity = item.TotalQnt;
                        dbOffer.OfferLine.Add(dbOfferLine);

                        //create saleOrderDetail
                        dbSaleOrderDetail = new SaleOrderDetail_Return();
                        dbSaleOrderDetail.OrderQnt = item.TotalQnt;
                        dbSaleOrderDetail.ProductStatusID = 1;
                        dbOfferLine.SaleOrderDetail.Add(dbSaleOrderDetail);
                        dbSaleOrder.SaleOrderDetail.Add(dbSaleOrderDetail);

                        //create saleorder product return
                        foreach (var item_return in dtoItem.SaleOrderProductReturns.Where(o => o.SaleOrderProductReturnID < 0 && o.ProductID == item.ProductID))
                        {
                            dbProductReturn = new SaleOrderProductReturn();
                            dbProductReturn.LoadingPlanDetailID = item_return.LoadingPlanDetailID;
                            dbProductReturn.OrderQnt = item_return.OrderQnt;
                            dbProductReturn.ReturnIndex = returnIdex;
                            dbProductReturn.CreatedDate = DateTime.Now;
                            dbSaleOrderDetail.SaleOrderProductReturn.Add(dbProductReturn);
                        }

                        //create fob warehouse import
                        var loadingplans = from p in dtoItem.SaleOrderProductReturns.Where(o => o.SaleOrderProductReturnID < 0 && o.ProductID == item.ProductID)
                                           group p by new { p.ProductID, p.LoadingPlanID } into g
                                           select new { g.Key.ProductID, g.Key.LoadingPlanID, TotalQnt = g.Sum(x => x.OrderQnt) };

                        foreach (var item_loadingplan in loadingplans)
                        {
                            dbWarehouseImportProduct = new WarehouseImportProductDetail();
                            dbWarehouseImportProduct.ProductID = item_loadingplan.ProductID;
                            dbWarehouseImportProduct.Quantity = item_loadingplan.TotalQnt;
                            dbWarehouseImportProduct.ProductStatusID = 1;
                            //dbWarehouseImportProduct.LoadingPlanID = item_loadingplan.LoadingPlanID;

                            dbWarehouseImportProduct.WarehouseImport = dbWarehouseImport;
                            //dbSaleOrderDetail.WarehouseImportProductDetail.Add(dbWarehouseImportProduct);
                        }


                    }

                    //create offerlinesparepart
                    var selected_spareparts = from s in dtoItem.SaleOrderSparepartReturns.Where(o => o.SaleOrderSparepartReturnID < 0)
                                              group s by new { s.SparepartID, s.ArticleCode, s.ModelID, s.PartID } into g
                                              select new { g.Key.SparepartID, g.Key.ArticleCode, g.Key.ModelID, g.Key.PartID, TotalQnt = g.Sum(o => o.OrderQnt) };

                    OfferLineSparepart_Return dbOfferLineSparepart = new OfferLineSparepart_Return();
                    SaleOrderDetailSparepart_Return dbSaleOrderDetailSparepart = new SaleOrderDetailSparepart_Return();
                    SaleOrderSparepartReturn dbSparepartReturn = new SaleOrderSparepartReturn();
                    WarehouseImportSparepartDetail dbWarehouseImportSparepart = new WarehouseImportSparepartDetail();

                    foreach (var item in selected_spareparts)
                    {
                        dbOfferLineSparepart = new OfferLineSparepart_Return();
                        dbOfferLineSparepart.ArticleCode = item.ArticleCode;
                        dbOfferLineSparepart.Description = dtoItem.SaleOrderSparepartReturns.Where(o => o.SparepartID == item.SparepartID).FirstOrDefault().Description;
                        dbOfferLineSparepart.ModelID = item.ModelID;
                        dbOfferLineSparepart.PartID = item.PartID;
                        dbOfferLine.Quantity = item.TotalQnt;
                        dbOffer.OfferLineSparepart.Add(dbOfferLineSparepart);

                        //create SaleOrderDetailSparepart
                        dbSaleOrderDetailSparepart = new SaleOrderDetailSparepart_Return();
                        dbSaleOrderDetailSparepart.OrderQnt = item.TotalQnt;
                        dbSaleOrderDetailSparepart.ProductStatusID = 1;

                        dbSaleOrderDetailSparepart.SaleOrder = dbSaleOrder;
                        dbOfferLineSparepart.SaleOrderDetailSparepart.Add(dbSaleOrderDetailSparepart);

                        //create sparepart return
                        foreach (var item_return in dtoItem.SaleOrderSparepartReturns.Where(o => o.SaleOrderSparepartReturnID < 0 && o.SparepartID == item.SparepartID))
                        {
                            dbSparepartReturn = new SaleOrderSparepartReturn();
                            dbSparepartReturn.LoadingPlanSparepartDetailID = item_return.LoadingPlanSparepartDetailID;
                            dbSparepartReturn.OrderQnt = item_return.OrderQnt;
                            dbSparepartReturn.ReturnIndex = returnIdexSparepart;
                            dbSparepartReturn.CreatedDate = DateTime.Now;
                            dbSaleOrderDetailSparepart.SaleOrderSparepartReturn.Add(dbSparepartReturn);
                        }

                        //create fob warehouse import sparepart
                        var loadingplans = from p in dtoItem.SaleOrderSparepartReturns.Where(o => o.SaleOrderSparepartReturnID < 0 && o.SparepartID == item.SparepartID)
                                           group p by new { p.SparepartID, p.LoadingPlanID } into g
                                           select new { g.Key.SparepartID, g.Key.LoadingPlanID, TotalQnt = g.Sum(x => x.OrderQnt) };

                        foreach (var item_loadingplan in loadingplans)
                        {
                            dbWarehouseImportSparepart = new WarehouseImportSparepartDetail();
                            dbWarehouseImportSparepart.SparepartID = item_loadingplan.SparepartID;
                            dbWarehouseImportSparepart.Quantity = item_loadingplan.TotalQnt;
                            dbWarehouseImportSparepart.ProductStatusID = 1;
                            //dbWarehouseImportSparepart.LoadingPlanID = item_loadingplan.LoadingPlanID;

                            dbWarehouseImportSparepart.WarehouseImport = dbWarehouseImport;
                            //dbSaleOrderDetailSparepart.WarehouseImportSparepartDetail.Add(dbWarehouseImportSparepart);
                        }
                    }
                    //save data
                    context.Offer.Add(dbOffer);
                    context.SaveChanges();
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
    }
}
