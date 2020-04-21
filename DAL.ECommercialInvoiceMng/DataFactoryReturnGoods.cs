using System;
using System.Linq;

namespace DAL.ECommercialInvoiceMng
{
    public class DataFactoryReturnGoods
    {
        private DataConverter converter = new DataConverter();
        private ReturnGoodsEntities CreateContext()
        {
            return new ReturnGoodsEntities(DALBase.Helper.CreateEntityConnectionString("ReturnGoodsModel"));
        }

        public DTO.ECommercialInvoiceMng.ReturnGoods GetReturnGoods(int eCommercialInvoiceID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReturnGoodsEntities context = CreateContext())
                {
                    DTO.ECommercialInvoiceMng.ReturnGoods data = new DTO.ECommercialInvoiceMng.ReturnGoods();
                    data.ReturnProducts = converter.DB2DTO_ReturnProduct(context.ECommercialInvoiceMng_ReturnProduct_View.Where(o => o.ECommercialInvoiceID == eCommercialInvoiceID).ToList());
                    int i = 1;
                    foreach (var item in data.ReturnProducts)
                    {
                        item.RowIndex = i;
                        if (!item.ProductSetEANCodeID.HasValue) {
                            item.ProductSetEANCodeID = -i;
                            item.ProductEANCode = "NONE";
                        }
                        if (!item.ProductColliID.HasValue) {
                            item.ProductColliID = -i;
                            item.ColliEANCode = "NONE";
                        }
                        i++;
                    }
                    data.ReturnSpareparts = converter.DB2DTO_ReturnSparepart(context.ECommercialInvoiceMng_ReturnSparepart_View.Where(o =>o.ECommercialInvoiceID == eCommercialInvoiceID).ToList());
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
                return new DTO.ECommercialInvoiceMng.ReturnGoods();
            }
        }

        public int CreateReturGoods(DTO.ECommercialInvoiceMng.ReturnGoods dtoReturn, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success , Message = "You have been returned goods success" };
            try
            {
                using (ReturnGoodsEntities context = CreateContext())
                {
                    //check data
                    var x = dtoReturn.ReturnProducts.Where(o => o.IsSelected.HasValue && o.IsSelected.Value);
                    if (x == null || x.Count() == 0)
                    {
                        throw new Exception("There are no ean code were seleted. Please select ean code");
                    }

                    var s = dtoReturn.ReturnProducts.Where(o => o.ProductColliID.HasValue && o.ProductColliID.Value < 0);
                    if (s.Count() > 0)
                    {
                        throw new Exception("All product must be assign Colli EAN Code before add to warehouse. To assign Colli EAN Code you click on each article code.");
                    }
                    
                    //create import receipt
                    WarehouseImport dbWHImport = new WarehouseImport();
                    dbWHImport.ImportTypeID = 3; //RETURN TYPE
                    dbWHImport.ImportedDate = DateTime.Now;
                    dbWHImport.Season = DALBase.Helper.GetCurrentSeason();

                    WarehouseImportAreaDetail dbWHArea;
                    WarehouseImportColliDetail dbColli;

                    var product = from item in dtoReturn.ReturnProducts.Where(o => o.IsSelected.HasValue && o.IsSelected.Value && o.ProductSetEANCodeID.HasValue && o.ProductSetEANCodeID.Value > 0 && o.ProductColliID.HasValue && o.ProductColliID.Value>0 && o.ReturnQnt.HasValue && o.ReturnQnt > 0)
                                            group item by new { item.ProductID,item.ECommercialInvoiceDetailID, item.ProductStatusID, item.ReturnQnt } into g
                                  select new { g.Key.ProductID, g.Key.ECommercialInvoiceDetailID, g.Key.ProductStatusID, g.Key.ReturnQnt };

                    foreach (var item in product)
                    {
                        WarehouseImportProductDetail dbProduct = new WarehouseImportProductDetail();
                        dbProduct.ProductID = item.ProductID;
                        dbProduct.ProductStatusID = item.ProductStatusID;
                        dbProduct.Quantity = item.ReturnQnt;
                        dbProduct.ECommercialInvoiceDetailID = item.ECommercialInvoiceDetailID;
                        dbWHImport.WarehouseImportProductDetail.Add(dbProduct);

                        //distribute to warehouse area
                        dbWHArea = new WarehouseImportAreaDetail();
                        dbWHArea.WarehouseAreaID = 1;
                        dbWHArea.Quantity = item.ReturnQnt;
                        dbProduct.WarehouseImportAreaDetail.Add(dbWHArea);

                        //create colli
                        foreach (var cItem in dtoReturn.ReturnProducts.Where(o => o.IsSelected.HasValue && o.IsSelected.Value && o.ProductSetEANCodeID.HasValue && o.ProductSetEANCodeID.Value > 0 && o.ProductColliID.HasValue && o.ProductColliID.Value > 0 && o.ProductID == item.ProductID && o.ReturnQnt.HasValue && o.ReturnQnt > 0))
                        {
                            dbColli = new WarehouseImportColliDetail();
                            dbColli.Quantity = item.ReturnQnt;
                            dbColli.ProductColliID = cItem.ProductColliID;
                            dbProduct.WarehouseImportColliDetail.Add(dbColli);
                        }
                    }

                    //foreach (var dtoProduct in dtoReturn.ReturnProducts.Where(o =>o.ReturnQnt.HasValue && o.ReturnQnt.Value > 0))
                    //{
                    //    WarehouseImportProductDetail dbProduct = new WarehouseImportProductDetail();
                    //    dbProduct.ProductID = dtoProduct.ProductID;
                    //    dbProduct.ProductStatusID = dtoProduct.ProductStatusID;
                    //    dbProduct.Quantity = dtoProduct.ReturnQnt;
                    //    dbProduct.ECommercialInvoiceDetailID = dtoProduct.ECommercialInvoiceDetailID;
                    //    dbWHImport.WarehouseImportProductDetail.Add(dbProduct);

                    //    //distribute to warehouse area
                    //    dbWHArea = new WarehouseImportAreaDetail();
                    //    dbWHArea.WarehouseAreaID = 1;
                    //    dbWHArea.Quantity = dtoProduct.ReturnQnt;
                    //    dbProduct.WarehouseImportAreaDetail.Add(dbWHArea);
                    //}

                    //foreach (var dtoSparepart in dtoReturn.ReturnSpareparts.Where(o =>o.ReturnQnt.HasValue && o.ReturnQnt.Value > 0))
                    //{
                    //    WarehouseImportSparepartDetail dbSparepart = new WarehouseImportSparepartDetail();
                    //    dbSparepart.SparepartID = dtoSparepart.SparepartID;
                    //    dbSparepart.ProductStatusID = dtoSparepart.ProductStatusID;
                    //    dbSparepart.Quantity = dtoSparepart.ReturnQnt;
                    //    dbSparepart.ECommercialInvoiceSparepartDetailID = dtoSparepart.ECommercialInvoiceSparepartDetailID;
                    //    dbWHImport.WarehouseImportSparepartDetail.Add(dbSparepart);

                    //    //distribute to warehouse area
                    //    dbWHArea = new WarehouseImportAreaDetail();
                    //    dbWHArea.WarehouseAreaID = 1;
                    //    dbWHArea.Quantity = dtoSparepart.ReturnQnt;
                    //    dbSparepart.WarehouseImportAreaDetail.Add(dbWHArea);
                    //}

                    context.WarehouseImport.Add(dbWHImport);
                    context.SaveChanges();
                    context.WarehouseImportMng_function_GenerateReceipNo(dbWHImport.WarehouseImportID, dbWHImport.Season);
                    return dbWHImport.WarehouseImportID;
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
                return -1;
            }
        }
    }
}
