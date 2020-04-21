using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
namespace DAL.WarehouseTransportMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.WarehouseTransportMng.WarehouseTransportSearch, DTO.WarehouseTransportMng.WarehouseTransport>
    {
        private DataConverter converter = new DataConverter();

        private WarehouseTransportMngEntities CreateContext()
        {
            return new WarehouseTransportMngEntities(DALBase.Helper.CreateEntityConnectionString("WarehouseTransportMngModel"));
        }

        public DataFactory() { }

        public override IEnumerable<DTO.WarehouseTransportMng.WarehouseTransportSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ReceiptNo = string.Empty;
            string Remark = string.Empty;

            if (filters.ContainsKey("ReceiptNo") && filters["ReceiptNo"] != null) ReceiptNo = filters["ReceiptNo"].ToString();
            if (filters.ContainsKey("Remark") && filters["Remark"] != null) Remark = filters["Remark"].ToString();
            try
            {
                using (WarehouseTransportMngEntities context = CreateContext())
                {
                    totalRows = context.WarehouseTransportMng_function_SearchWarehouseTransport(orderBy, orderDirection, ReceiptNo, Remark).Count();
                    var result = context.WarehouseTransportMng_function_SearchWarehouseTransport(orderBy, orderDirection, ReceiptNo, Remark);
                    return converter.DB2DTO_WarehouseTransportSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.WarehouseTransportMng.WarehouseTransportSearch>();
            }
        }

        public override DTO.WarehouseTransportMng.WarehouseTransport GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseTransportMngEntities context = CreateContext())
                {
                    DTO.WarehouseTransportMng.WarehouseTransport dtoItem = new DTO.WarehouseTransportMng.WarehouseTransport();

                    if (id > 0)
                    {
                        WarehouseTransportMng_WarehouseTransport_View dbItem;
                        dbItem = context.WarehouseTransportMng_WarehouseTransport_View
                            .Include("WarehouseTransportMng_WarehouseTransportProductDetail_View")
                            .Include("WarehouseTransportMng_WarehouseTransportSparepartDetail_View")
                            .FirstOrDefault(o => o.WarehouseTransportID == id);

                        dtoItem = converter.DB2DTO_WarehouseTransport(dbItem);
                    }
                    else
                    {
                        dtoItem.Season = DALBase.Helper.GetCurrentSeason();
                        dtoItem.WarehouseTransportProductDetails = new List<DTO.WarehouseTransportMng.WarehouseTransportProductDetail>();
                        dtoItem.WarehouseTransportSparepartDetails = new List<DTO.WarehouseTransportMng.WarehouseTransportSparepartDetail>();
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
                return new DTO.WarehouseTransportMng.WarehouseTransport();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseTransportMngEntities context = CreateContext())
                {
                    WarehouseTransport dbItem = context.WarehouseTransport.FirstOrDefault(o => o.WarehouseTransportID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "receipt not found!";
                        return false;
                    }
                    else
                    {
                        context.WarehouseTransport.Remove(dbItem);
                        context.SaveChanges();
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

        public override bool UpdateData(int id, ref DTO.WarehouseTransportMng.WarehouseTransport dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseTransportMngEntities context = CreateContext())
                {
                    WarehouseTransport dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WarehouseTransport();
                        context.WarehouseTransport.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WarehouseTransport.FirstOrDefault(o => o.WarehouseTransportID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "receipt not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        //validate transfer qnt
                        ValidateTransferQuantity(dtoItem);

                        //convert dto to db
                        converter.DTO2DB_WarehouseTransport(dtoItem, ref dbItem);

                        context.WarehouseTransportProductDetail.Local.Where(o => o.WarehouseTransport == null).ToList().ForEach(o => context.WarehouseTransportProductDetail.Remove(o));
                        context.WarehouseTransportSparepartDetail.Local.Where(o => o.WarehouseTransport == null).ToList().ForEach(o => context.WarehouseTransportSparepartDetail.Remove(o));
                        context.SaveChanges();

                        if (id == 0) { context.WarehouseTransportMng_function_GenerateReceiptNo(dbItem.WarehouseTransportID, dbItem.Season); } //update ReceiptNo

                        //Get return data
                        dtoItem = GetData(dbItem.WarehouseTransportID, out notification);
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

        public DTO.WarehouseTransportMng.EditSupportList GetEditSupportData()
        {
            DAL.Support.DataFactory factory = new Support.DataFactory();
            DTO.WarehouseTransportMng.EditSupportList dtoSupport = new DTO.WarehouseTransportMng.EditSupportList();
            dtoSupport.Users = factory.GetUser().ToList();
            dtoSupport.FromWarehouseAreas = GetPhysicalStockByWarehouseArea();
            dtoSupport.ToWarehouseAreas = factory.GetAllWarehouseArea().ToList();
            dtoSupport.Seasons = factory.GetSeason().ToList();
            
            return dtoSupport;
        }

        //public DTO.WarehouseTransportMng.WarehouseTransport GetEditData(int id, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    try
        //    {
        //        using (WarehouseTransportMngEntities context = CreateContext())
        //        {
        //            DTO.WarehouseTransportMng.WarehouseTransport dtoItem = new DTO.WarehouseTransportMng.WarehouseTransport();

        //            if (id > 0)
        //            {
        //                WarehouseTransportMng_WarehouseTransport_View dbItem;
        //                dbItem = context.WarehouseTransportMng_WarehouseTransport_View
        //                    .Include("PurchasingInvoiceMng_PurchasingInvoiceDetail_View")
        //                    .Include("PurchasingInvoiceMng_PurchasingInvoiceSparepartDetail_View")
        //                    .FirstOrDefault(o => o.WarehouseTransportID == id);

        //                dtoItem = converter.DB2DTO_WarehouseTransport(dbItem);
        //            }
        //            else
        //            {
                        
        //            }
        //            return dtoItem;
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
        //        return new DTO.WarehouseTransportMng.WarehouseTransport();
        //    }
        //}

        public List<DTO.WarehouseTransportMng.PhysicalStock> QuickSearchProduct(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehouseTransportMng.PhysicalStock> data = new List<DTO.WarehouseTransportMng.PhysicalStock>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                using (WarehouseTransportMngEntities context = CreateContext())
                {
                    string productType = null;
                    string searchQuery = null;

                    productType = filters["productType"].ToString().Replace("'", "''");
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    totalRows = context.WarehouseTransportMng_action_QuickSearchProduct(orderBy, orderDirection,productType, searchQuery).Count();
                    var result = context.WarehouseTransportMng_action_QuickSearchProduct(orderBy, orderDirection, productType, searchQuery);

                    data = converter.DB2DTO_PhysicalStock(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }
            return data;
        }

        private List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea> GetPhysicalStockByWarehouseArea()
        {
            List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea> data = new List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea>();
            try
            {
                using (WarehouseTransportMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_PhysicalStockByWarehouseArea(context.WarehouseTransportMng_PhysicalStockByWarehouseArea_View.ToList());
                }
            }
            catch (Exception ex)
            {
                return new List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea>();
            }
            return data;
        }

        private bool ValidateTransferQuantity(DTO.WarehouseTransportMng.WarehouseTransport dtoItem)
        {
            int? stockQnt = 0;
            int? inputQnt = 0;
            using (WarehouseTransportMngEntities context = CreateContext())
            {
                //validate product qnt
                foreach (var item in dtoItem.WarehouseTransportProductDetails)
                {
                    inputQnt = item.Quantity;
                    if (item.WarehouseTransportProductDetailID > 0)
                    {
                        inputQnt = item.Quantity - context.WarehouseTransportProductDetail.Where(o => o.WarehouseTransportProductDetailID == item.WarehouseTransportProductDetailID).FirstOrDefault().Quantity;
                    }
                    var dbCurrentProduct = context.WarehouseTransportMng_PhysicalStockByWarehouseArea_View.Where(o => o.GoodsID == item.ProductID && o.ProductStatusID == item.ProductStatusID && o.ProductType == "PRODUCT" && o.WarehouseAreaID == item.FromWarehouseAreaID).FirstOrDefault();
                    stockQnt = dbCurrentProduct == null ? 0 : dbCurrentProduct.PhysicalQnt;
                    if (inputQnt > stockQnt)
                    {
                        throw new Exception("Quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of product " + item.ArticleCode + " (" + item.Description + " ) in area " + item.FromWarehouseAreaUD + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                    }
                }
                //validate sparepart qnt
                foreach (var item in dtoItem.WarehouseTransportSparepartDetails)
                {
                    inputQnt = item.Quantity;
                    if (item.WarehouseTransportSparepartDetailID > 0)
                    {
                        inputQnt = item.Quantity - context.WarehouseTransportProductDetail.Where(o => o.WarehouseTransportProductDetailID == item.WarehouseTransportSparepartDetailID).FirstOrDefault().Quantity;
                    }
                    var dbCurrentProduct = context.WarehouseTransportMng_PhysicalStockByWarehouseArea_View.Where(o => o.GoodsID == item.SparepartID && o.ProductStatusID == item.ProductStatusID && o.ProductType == "SPAREPART" && o.WarehouseAreaID == item.FromWarehouseAreaID).FirstOrDefault();
                    stockQnt = dbCurrentProduct == null ? 0 : dbCurrentProduct.PhysicalQnt;
                    if (inputQnt > stockQnt)
                    {
                        throw new Exception("Quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of sparepart " + item.ArticleCode + " (" + item.Description + " ) in area " + item.FromWarehouseAreaUD + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                    }
                }
            }
            return true;
        }
    
    }
}
