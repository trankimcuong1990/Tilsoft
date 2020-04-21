using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
namespace DAL.ReportStockList
{
    public class DataFactory
    {
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private DataConverter converter = new DataConverter();
        
        private ReportStockListEntities CreateContext()
        {
            return new ReportStockListEntities(DALBase.Helper.CreateEntityConnectionString("ReportStockListModel"));
        }

        public  DTO.ReportStockList.SearchFormData GetStockListSearch(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ReportStockList.SearchFormData searchFormData = new DTO.ReportStockList.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            try
            {
                string productStatusIDs = null;
                bool? isMatchedImage = null;
                bool? isHaveImage = null;
                string articleCode = null;
                string description = null;
                int? qnt40HC = null;
                int? physicalQnt = null;
                decimal? physicalQntIn40HC = null;
                string wareHouseAreaIDs = null;
                string qntPerWarehouseArea = null;
                int? onSeaQnt = null;
                int? tobeShippedQnt = null;
                int? reservationQnt = null;
                int? ftsQnt = null;
                bool? isActiveFreeToSale = null;
                string eanCode = null;
                string freeToSaleCategoryIDs = null;
                
                using (ReportStockListEntities context = CreateContext())
                {
                    if (filters.ContainsKey("productStatusIDs") && !string.IsNullOrEmpty(filters["productStatusIDs"].ToString()))
                    {
                        productStatusIDs = filters["productStatusIDs"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("isMatchedImage") && filters["isMatchedImage"] != null && !string.IsNullOrEmpty(filters["isMatchedImage"].ToString()))
                    {
                        switch (filters["isMatchedImage"].ToString().ToLower())
                        {
                            case "true":
                                isMatchedImage = true;
                                break;
                            case "false":
                                isMatchedImage = false;
                                break;
                            default:
                                isMatchedImage = null;
                                break;
                        }
                    }
                    if (filters.ContainsKey("isHaveImage") && filters["isHaveImage"] != null && !string.IsNullOrEmpty(filters["isHaveImage"].ToString()))
                    {
                        switch (filters["isHaveImage"].ToString().ToLower())
                        {
                            case "true":
                                isHaveImage = true;
                                break;
                            case "false":
                                isHaveImage = false;
                                break;
                            default:
                                isHaveImage = null;
                                break;
                        }
                    }
                    if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
                    {
                        articleCode = filters["articleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                    {
                        description = filters["description"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("qnt40HC") && filters["qnt40HC"] != null && !string.IsNullOrEmpty(filters["qnt40HC"].ToString()))
                    {
                        qnt40HC = Convert.ToInt32(filters["qnt40HC"].ToString());
                    }
                    if (filters.ContainsKey("physicalQnt") && filters["physicalQnt"] != null && !string.IsNullOrEmpty(filters["physicalQnt"].ToString()))
                    {
                        physicalQnt = Convert.ToInt32(filters["physicalQnt"].ToString());
                    }
                    if (filters.ContainsKey("physicalQntIn40HC") && filters["physicalQntIn40HC"] != null && !string.IsNullOrEmpty(filters["physicalQntIn40HC"].ToString()))
                    {
                        physicalQntIn40HC = Convert.ToDecimal(filters["physicalQntIn40HC"].ToString());
                    }
                    if (filters.ContainsKey("wareHouseAreaIDs") && !string.IsNullOrEmpty(filters["wareHouseAreaIDs"].ToString()))
                    {
                        wareHouseAreaIDs = filters["wareHouseAreaIDs"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("qntPerWarehouseArea") && !string.IsNullOrEmpty(filters["qntPerWarehouseArea"].ToString()))
                    {
                        qntPerWarehouseArea = filters["qntPerWarehouseArea"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("onSeaQnt") && filters["onSeaQnt"] != null && !string.IsNullOrEmpty(filters["onSeaQnt"].ToString()))
                    {
                        onSeaQnt = Convert.ToInt32(filters["onSeaQnt"].ToString());
                    }
                    if (filters.ContainsKey("tobeShippedQnt") && filters["tobeShippedQnt"] != null && !string.IsNullOrEmpty(filters["tobeShippedQnt"].ToString()))
                    {
                        tobeShippedQnt = Convert.ToInt32(filters["tobeShippedQnt"].ToString());
                    }
                    if (filters.ContainsKey("reservationQnt") && filters["reservationQnt"] != null && !string.IsNullOrEmpty(filters["reservationQnt"].ToString()))
                    {
                        reservationQnt = Convert.ToInt32(filters["reservationQnt"].ToString());
                    }
                    if (filters.ContainsKey("ftsQnt") && filters["ftsQnt"] != null && !string.IsNullOrEmpty(filters["ftsQnt"].ToString()))
                    {
                        ftsQnt = Convert.ToInt32(filters["ftsQnt"].ToString());
                    }
                    if (filters.ContainsKey("isActiveFreeToSale") && filters["isActiveFreeToSale"] != null && !string.IsNullOrEmpty(filters["isActiveFreeToSale"].ToString()))
                    {
                        switch (filters["isActiveFreeToSale"].ToString().ToLower())
                        {
                            case "true":
                                isActiveFreeToSale = true;
                                break;
                            case "false":
                                isActiveFreeToSale = false;
                                break;
                            default:
                                isActiveFreeToSale = null;
                                break;
                        }
                    }

                    if (filters.ContainsKey("eanCode") && !string.IsNullOrEmpty(filters["eanCode"].ToString()))
                    {
                        eanCode = filters["eanCode"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("freeToSaleCategoryIDs") && !string.IsNullOrEmpty(filters["freeToSaleCategoryIDs"].ToString()))
                    {
                        freeToSaleCategoryIDs = filters["freeToSaleCategoryIDs"].ToString().Replace("'", "''");
                    }
                    
                    //cal grand total
                    var s = context.ReportMng_StockList_View.ToList();
                    searchFormData.TotalPhysicalQnt = s.Sum(o => (o.PhysicalQnt.HasValue ? o.PhysicalQnt.Value : 0));
                    searchFormData.TotalPhysicalQntIn40HC = s.Sum(o => (o.PhysicalQntIn40HC.HasValue ? o.PhysicalQntIn40HC.Value : 0));
                    searchFormData.TotalOnSeaQnt = s.Sum(o => (o.OnSeaQnt.HasValue ? o.OnSeaQnt.Value : 0));
                    searchFormData.TotalTobeShippedQnt = s.Sum(o => (o.TobeShippedQnt.HasValue ? o.TobeShippedQnt.Value : 0));
                    searchFormData.TotalReservationQnt = s.Sum(o => (o.ReservationQnt.HasValue ? o.ReservationQnt.Value : 0));
                    searchFormData.TotalFTSQnt = s.Sum(o => (o.FTSQnt.HasValue ? o.FTSQnt.Value : 0));

                    //cal sub total
                    var x = context.ReportMng_action_GetSearchStockList(orderBy, orderDirection
                                                                            , productStatusIDs
                                                                            , isMatchedImage
                                                                            , isHaveImage
                                                                            , articleCode
                                                                            , description
                                                                            , qnt40HC
                                                                            , physicalQnt
                                                                            , physicalQntIn40HC
                                                                            , wareHouseAreaIDs
                                                                            , qntPerWarehouseArea
                                                                            , onSeaQnt
                                                                            , tobeShippedQnt
                                                                            , reservationQnt
                                                                            , ftsQnt
                                                                            , isActiveFreeToSale
                                                                            , eanCode
                                                                            , freeToSaleCategoryIDs).ToList();

                    searchFormData.SubTotalPhysicalQnt = x.Sum(o => (o.PhysicalQnt.HasValue ? o.PhysicalQnt.Value : 0));
                    searchFormData.SubTotalPhysicalQntIn40HC = x.Sum(o => (o.PhysicalQntIn40HC.HasValue ? o.PhysicalQntIn40HC.Value : 0));
                    searchFormData.SubTotalOnSeaQnt = x.Sum(o => (o.OnSeaQnt.HasValue ? o.OnSeaQnt.Value : 0));
                    searchFormData.SubTotalTobeShippedQnt = x.Sum(o => (o.TobeShippedQnt.HasValue ? o.TobeShippedQnt.Value : 0));
                    searchFormData.SubTotalReservationQnt = x.Sum(o => (o.ReservationQnt.HasValue ? o.ReservationQnt.Value : 0));
                    searchFormData.SubTotalFTSQnt = x.Sum(o => (o.FTSQnt.HasValue ? o.FTSQnt.Value : 0));

                    //cal total row
                    totalRows = x.Count();
                    //search result
                    var result = context.ReportMng_action_GetSearchStockList(orderBy, orderDirection
                                                                            , productStatusIDs
                                                                            , isMatchedImage
                                                                            , isHaveImage
                                                                            , articleCode
                                                                            , description
                                                                            , qnt40HC
                                                                            , physicalQnt
                                                                            , physicalQntIn40HC
                                                                            , wareHouseAreaIDs
                                                                            , qntPerWarehouseArea
                                                                            , onSeaQnt
                                                                            , tobeShippedQnt
                                                                            , reservationQnt
                                                                            , ftsQnt
                                                                            , isActiveFreeToSale
                                                                            , eanCode
                                                                            , freeToSaleCategoryIDs);
                                                                         
                    searchFormData.Data = converter.DB2DTO_StockListSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //get list eancode 
                    List<int?> productIDs = searchFormData.Data.Where(o => o.ProductType == "PRODUCT").Select(o => o.GoodsID).ToList();
                    searchFormData.ProductSetEANCodes = converter.DB2DTO_ProductSetEANCode(context.ReportMng_StockList_ProductSetEANCode_View.Where(o => productIDs.Contains(o.ProductID)).ToList());

                    //assign location for every product
                    List<string> keyIDs = searchFormData.Data.Select(o =>o.KeyID).ToList();
                    var stockLocation = context.ReportMng_StockList_PhysicalStockByWarehouseArea_View.Where(o => keyIDs.Contains(o.KeyID)).ToList();
                    
                    foreach (var item in searchFormData.Data)
                    {
                        item.WarehouseAreaUD = "";
                        item.QntPerWarehouseArea = "";
                        foreach (var sItem in stockLocation.Where(o => o.KeyID == item.KeyID))
                        {
                            item.WarehouseAreaUD += sItem.WarehouseAreaUD + " / ";
                            item.QntPerWarehouseArea += sItem.PhysicalQnt.ToString() + " / ";
                        }
                        if (item.WarehouseAreaUD.Length > 2)
                        {
                            item.WarehouseAreaUD = item.WarehouseAreaUD.Substring(0, item.WarehouseAreaUD.Length - 2);
                        }
                        if (item.QntPerWarehouseArea.Length > 2)
                        {
                            item.QntPerWarehouseArea = item.QntPerWarehouseArea.Substring(0, item.QntPerWarehouseArea.Length - 2);
                        }
                    }
                }

                //get support list
                DAL.Support.DataFactory support_factory  = new Support.DataFactory();
                searchFormData.FreeToSaleCategories = support_factory.GetFreeToSaleCategory();
                searchFormData.ProductStatuses = support_factory.GetProductStatus().ToList();
                searchFormData.WarehouseAreas = support_factory.GetAllWarehouseArea().ToList(); ;
                return searchFormData;
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
                return searchFormData;
            }
        }

        public string GetStockListReport(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get report success" };
            try
            {
                string articleCode = string.Empty;
                string description = string.Empty;
                string productStatusIDs = string.Empty;
                string wareHouseAreaIDs = string.Empty;
                bool isIncludeImage = false;

                if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
                if (filters.ContainsKey("description")) description = filters["description"].ToString();
                if (filters.ContainsKey("productStatusIDs")) productStatusIDs = filters["productStatusIDs"].ToString();
                if (filters.ContainsKey("wareHouseAreaIDs")) wareHouseAreaIDs = filters["wareHouseAreaIDs"].ToString();
                if (filters.ContainsKey("isIncludeImage") && filters["isIncludeImage"] != null) isIncludeImage = Convert.ToBoolean(filters["isIncludeImage"]);
                
                
                ReportDataObject ds = new ReportDataObject();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_action_GetStockList", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@ArticleCode", articleCode);
                adap.SelectCommand.Parameters.AddWithValue("@Description", description);
                adap.SelectCommand.Parameters.AddWithValue("@ProductStatusIDs", productStatusIDs);
                adap.SelectCommand.Parameters.AddWithValue("@WareHouseAreaIDs", wareHouseAreaIDs);

                adap.TableMappings.Add("Table", "ReportMng_StockList_View");
                adap.TableMappings.Add("Table1", "ReportMng_StockList_PhysicalStockByWarehouseArea_View");
                adap.TableMappings.Add("Table2", "ReportMng_StockList_ProductSetEANCode_View");
                adap.Fill(ds);
                ds.AcceptChanges();

                //create param table
                ReportDataObject.ParamTableRow pRow = ds.ParamTable.NewParamTableRow();
                pRow.IsIncludeImage = isIncludeImage;
                ds.ParamTable.AddParamTableRow(pRow);

                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "StockList");
                //return string.Empty;

                //assign location for every product
                foreach (var item in ds.ReportMng_StockList_View)
                {
                    //assign area
                    item.WarehouseAreaUD = "";
                    item.QntPerWarehouseArea = "";
                    foreach (var sItem in ds.ReportMng_StockList_PhysicalStockByWarehouseArea_View.Where(o => o.KeyID == item.KeyID))
                    {
                        item.WarehouseAreaUD += sItem.WarehouseAreaUD + " / ";
                        item.QntPerWarehouseArea += sItem.PhysicalQnt.ToString() + " / ";
                    }
                    if (item.WarehouseAreaUD.Length > 2)
                    {
                        item.WarehouseAreaUD = item.WarehouseAreaUD.Substring(0, item.WarehouseAreaUD.Length - 2);
                    }
                    if (item.QntPerWarehouseArea.Length > 2)
                    {
                        item.QntPerWarehouseArea = item.QntPerWarehouseArea.Substring(0, item.QntPerWarehouseArea.Length - 2);
                    }
                    //asign image
                    if (isIncludeImage && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.SelectedThumbnailImage))
                    {
                        item.SelectedThumbnailImage = FrameworkSetting.Setting.ThumbnailUrl + item.SelectedThumbnailImage;
                    }
                    else
                    {
                        item.SelectedThumbnailImage = "NONE";
                    }

                    //assign EAN Code
                    item.EANCode = "";
                    foreach (var eItem in ds.ReportMng_StockList_ProductSetEANCode_View.Where(o =>o.ProductID==item.GoodsID && item.ProductType=="PRODUCT"))
                    {
                        item.EANCode += eItem.EANCode + " / ";
                    }
                    if (item.EANCode.Length > 2)
                    {
                        item.EANCode = item.EANCode.Substring(0, item.EANCode.Length - 2);
                    }
                }
                    
                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "StockList");
                return result;
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
                return string.Empty; 
            }
        }

        public bool ActiveFreeToSaleProduct(int productID, bool? isActiveFreeToSale, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReportStockListEntities context = CreateContext())
                {
                    var product = context.Product.Where(o => o.ProductID == productID).FirstOrDefault();
                    if (product != null)
                    {
                        product.IsActiveFreeToSale = isActiveFreeToSale;
                        context.SaveChanges();
                    }
                    else {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Could not find product";
                        return false;
                    }
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

        public bool SetFreeToSaleCategory(int productID, int? freeToSaleCategoryID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReportStockListEntities context = CreateContext())
                {
                    var product = context.Product.Where(o => o.ProductID == productID).FirstOrDefault();
                    if (product != null)
                    {
                        product.FreeToSaleCategoryID = freeToSaleCategoryID;
                        context.SaveChanges();
                    }
                    else
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Could not find product";
                        return false;
                    }
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

        public List<DTO.ReportStockList.StockReservation> GetStockReservation(int? GoodsID, string ProductType, int? ProductStatusID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReportStockListEntities context = CreateContext())
                {
                    return converter.DB2DTO_StockReservation(context.ReportMng_StockList_Reservation_View.Where(o =>o.GoodsID == GoodsID && o.ProductType == ProductType && o.ProductStatusID == ProductStatusID).ToList());
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
                return new List<DTO.ReportStockList.StockReservation>();
            }
        }

        public bool MatchedImageProduct(int productID, bool? isMatchedImage, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ReportStockListEntities context = CreateContext())
                {
                    var product = context.Product.Where(o => o.ProductID == productID).FirstOrDefault();
                    if (product != null)
                    {
                        product.IsMatchedImage = isMatchedImage;
                        context.SaveChanges();
                    }
                    else
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Could not find product";
                        return false;
                    }
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

        public DTO.ReportStockList.StockListDetail GetStockListDetail(string keyID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportStockList.StockListDetail data = new DTO.ReportStockList.StockListDetail();
            try
            {
                using (ReportStockListEntities context = CreateContext())
                {
                    data.SaleOrderDetails = converter.DB2DTO_SaleOrderDetail(context.ReportMng_StockList_SaleOrderDetail_View.Where(o =>o.KeyID == keyID).ToList());
                    data.WarehouseImportDetails = converter.DB2DTO_WarehouseImportDetail(context.ReportMng_StockList_WarehouseImportDetail_View.Where(o =>o.KeyID == keyID).ToList());
                    data.WarehousePickingListDetails = converter.DB2DTO_WarehousePickingListDetail(context.ReportMng_StockList_WarehousePickingListDetail_View.Where(o => o.KeyID == keyID).ToList());
                    data.LoadingPlanDetails = converter.DB2DTO_LoadingPlanDetail(context.ReportMng_StockList_LoadingPlanDetail_View.Where(o => o.KeyID == keyID).ToList());
                }
                return data;
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
                return new DTO.ReportStockList.StockListDetail();
            }
        }
    }
}
