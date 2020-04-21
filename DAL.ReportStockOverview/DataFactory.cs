using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace DAL.ReportStockOverview
{
    public class DataFactory 
    {
        private DataConverter converter = new DataConverter();
        private ReportStockOverviewEntities CreateContext()
        {
            return new ReportStockOverviewEntities(DALBase.Helper.CreateEntityConnectionString("ReportStockOverviewModel"));
        }
        public string GetStockOverview(Hashtable filters,out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get report success" };
            try
            {
                ReportDataObject ds = new ReportDataObject();

                string season = string.Empty;
                int month = DateTime.Now.Month;
                bool isIncludeImage = false;
                bool isIncludeDailyImageProduct = false;

                if (filters.ContainsKey("season") && filters["season"] != null) season = filters["season"].ToString();
                if (filters.ContainsKey("month") && filters["month"] != null) month = Convert.ToInt32(filters["month"]);
                if (filters.ContainsKey("isIncludeImage") && filters["isIncludeImage"] != null) isIncludeImage = Convert.ToBoolean(filters["isIncludeImage"]);
                if (filters.ContainsKey("isIncludeDailyImageProduct") && filters["isIncludeDailyImageProduct"] != null) isIncludeDailyImageProduct = Convert.ToBoolean(filters["isIncludeDailyImageProduct"]);


                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_function_GetStockOverview", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.SelectCommand.Parameters.AddWithValue("@Month", month);
                adap.SelectCommand.Parameters.AddWithValue("@IsIncludeImage", isIncludeImage);
                adap.SelectCommand.Parameters.AddWithValue("@IsIncludeDailyImageProduct", isIncludeDailyImageProduct);

                adap.TableMappings.Add("Table",  "ParamTable");
                adap.TableMappings.Add("Table1", "ReportMng_StockOverview_View");
                adap.TableMappings.Add("Table2", "ReportMng_StockReservation_View");
                adap.TableMappings.Add("Table3", "ReportMng_StockReservationProduct_View");
                adap.TableMappings.Add("Table4", "ReportMng_StockClient_View");
                adap.TableMappings.Add("Table5", "ReportMng_StockOverview_PhysicalStock_View");
                adap.TableMappings.Add("Table6", "ReportMng_StockOverview_ProductImportRemark_View");

                adap.TableMappings.Add("Table7", "DailyInMonth");
                adap.TableMappings.Add("Table8", "ReportMng_StockDaily_Imported_View");
                adap.TableMappings.Add("Table9", "ReportMng_StockDaily_Picked_View");
                adap.TableMappings.Add("Table10", "ReportMng_StockDaily_Product_View");

                adap.Fill(ds);

                // encode string for barcode
                foreach (ReportDataObject.ReportMng_StockOverview_ViewRow row in ds.ReportMng_StockOverview_View)
                {
                    //if (!row.IsArticleCodeNull())
                    //{
                    //    row.BarCode = BarCode.BarcodeConverter128.StringToBarcode(row.ArticleCode);
                    //}

                    if (!row.IsKeyIDNull())
                    {
                        //add location 
                        row.WarehouseAreaUD = "";
                        row.QntPerWarehouseArea = "";
                        foreach (var item in ds.ReportMng_StockOverview_PhysicalStock_View.Where(o => !o.IsKeyIDNull() && !o.IsWarehouseAreaUDNull() && o.KeyID == row.KeyID))
                        {
                            row.WarehouseAreaUD += item.WarehouseAreaUD + " / ";
                            row.QntPerWarehouseArea += item.PhysicalQnt.ToString() + " / ";
                        }

                        //add remark
                        string remark = "";
                        foreach (var item in ds.ReportMng_StockOverview_ProductImportRemark_View.Where(o => !o.IsKeyIDNull() && o.KeyID == row.KeyID))
                        {
                            remark += item.Remark + "\n";
                        }
                        if (!string.IsNullOrEmpty(remark)) row.Description = row.Description + "\n\nREMARK: " + remark;
                    }

                    if (isIncludeImage && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + row.ImageFile))
                    {
                        row.ImageFile = FrameworkSetting.Setting.ThumbnailUrl + row.ImageFile;
                    }
                    else
                    {
                        row.ImageFile = "NONE";
                    }
                }

                foreach (ReportDataObject.ReportMng_StockReservationProduct_ViewRow row in ds.ReportMng_StockReservationProduct_View)
                {
                    //if (!row.IsArticleCodeNull())
                    //{
                    //    row.BarCode = BarCode.BarcodeConverter128.StringToBarcode(row.ArticleCode);
                    //}
                    if (isIncludeImage && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + row.ImageFile))
                    {
                        row.ImageFile = FrameworkSetting.Setting.ThumbnailUrl + row.ImageFile;
                    }
                    else
                    {
                        row.ImageFile = "NONE";
                    }
                }

                foreach (var row in ds.ReportMng_StockDaily_Product_View)
                {
                    //if (!row.IsArticleCodeNull())
                    //{
                    //    row.Barcode = BarCode.BarcodeConverter128.StringToBarcode(row.ArticleCode);
                    //}
                    if (isIncludeDailyImageProduct && !row.IsSelectedThumbnailImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + row.SelectedThumbnailImage))
                    {
                        row.SelectedThumbnailImage = FrameworkSetting.Setting.ThumbnailUrl + row.SelectedThumbnailImage;
                    }
                    else
                    {
                        row.SelectedThumbnailImage = "NONE";
                    }
                }

                ds.AcceptChanges();

                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "StockOverview");
                //return string.Empty;

                // generate xml file
                string result = string.Empty;
                //if (includeImage)
                //{
                //    result = DALBase.Helper.CreateReportFiles(ds, "ReportStockOverviewWithImage");
                //}
                //else
                //{
                //    result = DALBase.Helper.CreateReportFiles(ds, "ReportStockOverview");
                //}

                result = DALBase.Helper.CreateReportFiles(ds, "ReportStockOverview");

                if (string.IsNullOrEmpty(result))
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = "Can not create report";
                    return string.Empty;
                }
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
        public string GetStockOverviewWithDetail(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                ReportDataObject ds = new ReportDataObject();

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_function_GetStockOverviewWithDetail", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.TableMappings.Add("Table", "ReportMng_StockOverview_View");
                adap.TableMappings.Add("Table1", "ReportMng_StockReservation_View");
                adap.TableMappings.Add("Table2", "ReportMng_StockReservationProduct_View");
                adap.TableMappings.Add("Table3", "ReportMng_StockClient_View");

                //adap.TableMappings.Add("Table4", "ReportMng_ImportedOverview_View");
                //adap.TableMappings.Add("Table5", "ReportMng_OrderOverview_View");
                //adap.TableMappings.Add("Table6", "ReportMng_ReservationOverview_View");
                //adap.TableMappings.Add("Table7", "ReportMng_PickingListOverview_View");
                //adap.TableMappings.Add("Table8", "ReportMng_ExportedOverview_View");

                adap.Fill(ds);

                // encode string for barcode
                //foreach (ReportDataObject.ReportMng_StockOverview_ViewRow row in ds.ReportMng_StockOverview_View)
                //{
                //    if (row.IsBarCodeNull())
                //    {
                //        row.BarCode = BarCode.BarcodeConverter128.StringToBarcode(row.ArticleCode);
                //    }
                //}

                //foreach (ReportDataObject.ReportMng_StockReservationProduct_ViewRow row in ds.ReportMng_StockReservationProduct_View)
                //{
                //    if (row.IsBarCodeNull())
                //    {
                //        row.BarCode = BarCode.BarcodeConverter128.StringToBarcode(row.ArticleCode);
                //    }
                //}

                ds.AcceptChanges();

                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "StockOverview");
                //return string.Empty;

                // generate xml file
                string result = string.Empty;
                result = DALBase.Helper.CreateReportFiles(ds, "ReportStockOverviewWithDetail");

                if (string.IsNullOrEmpty(result))
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    return string.Empty;
                }
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
        public List<DTO.ReportStockOverview.StockOverview> GetStockOverview_HTMLView(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get stock overview success" };
            try
            {
                string articleCode = string.Empty;
                string description = string.Empty;

                if (filters.ContainsKey("articleCode") && filters["articleCode"] != null) articleCode = filters["articleCode"].ToString();
                if (filters.ContainsKey("description") && filters["description"] != null) description = filters["description"].ToString();
               
                using (ReportStockOverviewEntities context = CreateContext())
                {
                    List<DTO.ReportStockOverview.StockOverview> stockOverviewData = converter.DB2DTO_StockOverview(context.ReportMng_StockOverview_View.Where(o =>o.ArticleCode.Contains(articleCode) && o.Description.Contains(description)).ToList());
                    List<DTO.ReportStockOverview.PhysicalStockByArea> physicalStockByAreaData = converter.DB2DTO_PhysicalStockByArea(context.ReportMng_StockOverview_PhysicalStock_View.Where(o => o.ArticleCode.Contains(articleCode) && o.Description.Contains(description)).ToList());
                    List<DTO.ReportStockOverview.ProductImportRemark> importRemarkData = converter.DB2DTO_ImportRemark(context.ReportMng_StockOverview_ProductImportRemark_View.Where(o => o.ArticleCode.Contains(articleCode) && o.Description.Contains(description)).ToList());
                    foreach (var overviewItem in stockOverviewData)
                    {
                        overviewItem.WarehouseAreaUD = "";
                        overviewItem.QntPerWarehouseArea = "";
                        foreach (var physicalItem in physicalStockByAreaData.Where(o => o.KeyID == overviewItem.KeyID))
                        {
                            overviewItem.WarehouseAreaUD += physicalItem.WarehouseAreaUD + " / ";
                            overviewItem.QntPerWarehouseArea += physicalItem.PhysicalQnt.ToString() + " / ";
                        }
                        if (overviewItem.WarehouseAreaUD.Length > 2)
                        {
                            overviewItem.WarehouseAreaUD = overviewItem.WarehouseAreaUD.Substring(0, overviewItem.WarehouseAreaUD.Length - 2);
                        }
                        if (overviewItem.QntPerWarehouseArea.Length > 2)
                        {
                            overviewItem.QntPerWarehouseArea = overviewItem.QntPerWarehouseArea.Substring(0, overviewItem.QntPerWarehouseArea.Length - 2);
                        }
                        //add remark
                        string remark = "";
                        foreach (var remarkItem in importRemarkData.Where(o => o.KeyID == overviewItem.KeyID))
                        {
                            remark += remarkItem.Remark + "\n";
                        }
                        if (!string.IsNullOrEmpty(remark)) overviewItem.Description = overviewItem.Description + "<br/>REMARK: " + remark;
                    }
                    return stockOverviewData;
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
                return new List<DTO.ReportStockOverview.StockOverview>();
            }
        }
    }
}
