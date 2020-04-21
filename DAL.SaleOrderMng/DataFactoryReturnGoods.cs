using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SaleOrderMng
{
    public  class DataFactoryReturnGoods
    {
        private DataConverter converter = new DataConverter();
        private ReturnGoodsFromSaleOrderEntities CreateContext()
        {
            return new ReturnGoodsFromSaleOrderEntities(DALBase.Helper.CreateEntityConnectionString("ReturnGoodsFromSaleOrder"));
        }

        public IEnumerable<DTO.SaleOrderMng.LoadingPlan2> GetLoadingPlan(int saleOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReturnGoodsFromSaleOrderEntities context = CreateContext())
                {
                    List<int?> loadingPlanIDs = context.SaleOrderMng_LoadingPlanDetail2_View.Where(o => o.SaleOrderID == saleOrderID).Select(x => x.LoadingPlanID).ToList();
                    loadingPlanIDs.AddRange(context.SaleOrderMng_LoadingPlanSparepartDetail2_View.Where(o => o.SaleOrderID == saleOrderID).Select(x => x.LoadingPlanID).ToList());
                    loadingPlanIDs = loadingPlanIDs.Distinct().ToList();

                    var result = context.SaleOrderMng_LoadingPlan2_View.Include("SaleOrderMng_LoadingPlanDetail2_View").Include("SaleOrderMng_LoadingPlanSparepartDetail2_View").Where(o => loadingPlanIDs.Contains(o.LoadingPlanID));
                    return converter.DB2DTO_LoadingPlan2(result.ToList());
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
                return new List<DTO.SaleOrderMng.LoadingPlan2>();
            }
        }

        public bool CreateReturnData(List<DTO.SaleOrderMng.LoadingPlan2> dtoReturns, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success , Message = "You have been returned goods success" };
            try
            {
                using (ReturnGoodsFromSaleOrderEntities context = CreateContext())
                {
                    WarehouseImport dbWHImport = new WarehouseImport();
                    dbWHImport.ImportTypeID = 3; //RETURN TYPE
                    dbWHImport.ImportedDate = DateTime.Now;
                    dbWHImport.Season = DALBase.Helper.GetCurrentSeason();

                    WarehouseImportAreaDetail dbWHArea;

                    foreach (var dtoReturn in dtoReturns)
                    {
                        foreach (var dtoProduct in dtoReturn.LoadingPlanDetail2s.Where(o => o.ReturnQnt.HasValue && o.ReturnQnt > 0))
                        {
                            WarehouseImportProductDetail dbProduct = new WarehouseImportProductDetail();
                            dbProduct.ProductID = dtoProduct.ProductID;
                            dbProduct.ProductStatusID = dtoProduct.ProductStatusID;
                            dbProduct.Quantity = dtoProduct.ReturnQnt;
                            dbProduct.LoadingPlanDetailID = dtoProduct.LoadingPlanDetailID;
                            dbWHImport.WarehouseImportProductDetail.Add(dbProduct);

                            //distribute to warehouse area
                            dbWHArea = new WarehouseImportAreaDetail();
                            dbWHArea.WarehouseAreaID = 1;
                            dbWHArea.Quantity = dtoProduct.ReturnQnt;
                            dbProduct.WarehouseImportAreaDetail.Add(dbWHArea);
                        }
                        foreach (var dtoSparepart in dtoReturn.LoadingPlanSparepartDetail2s.Where(o => o.ReturnQnt.HasValue && o.ReturnQnt > 0))
                        {
                            WarehouseImportSparepartDetail dbSparepart = new WarehouseImportSparepartDetail();
                            dbSparepart.SparepartID = dtoSparepart.SparepartID;
                            dbSparepart.ProductStatusID = dtoSparepart.ProductStatusID;
                            dbSparepart.Quantity = dtoSparepart.ReturnQnt;
                            dbSparepart.LoadingPlanSparepartDetailID = dtoSparepart.LoadingPlanSparepartDetailID;
                            dbWHImport.WarehouseImportSparepartDetail.Add(dbSparepart);

                            //distribute to warehouse area
                            dbWHArea = new WarehouseImportAreaDetail();
                            dbWHArea.WarehouseAreaID = 1;
                            dbWHArea.Quantity = dtoSparepart.ReturnQnt;
                            dbSparepart.WarehouseImportAreaDetail.Add(dbWHArea);
                        }
                    }
                    
                    context.WarehouseImport.Add(dbWHImport);
                    context.SaveChanges();
                    context.WarehouseImportMng_function_GenerateReceipNo(dbWHImport.WarehouseImportID, dbWHImport.Season);
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
