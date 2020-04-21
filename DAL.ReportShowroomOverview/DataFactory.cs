using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
namespace DAL.ReportShowroomOverview
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();

        private ReportShowroomOverviewEntities CreateContext()
        {
            return new ReportShowroomOverviewEntities(DALBase.Helper.CreateEntityConnectionString("ReportShowroomOverviewModel"));
        }

        public List<DTO.ReportShowroomOverview.ShowroomArea> GetShowroomOverview(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get showroom overview success" };
            try
            {
                string articleCode = string.Empty;
                string description = string.Empty;
                int? showroomAreaID = null;

                if (filters.ContainsKey("articleCode") && filters["articleCode"] != null) articleCode = filters["articleCode"].ToString();
                if (filters.ContainsKey("description") && filters["description"] != null) description = filters["description"].ToString();
                if (filters.ContainsKey("showroomAreaID") && filters["showroomAreaID"] != null) showroomAreaID = Convert.ToInt32(filters["showroomAreaID"].ToString());

                string sqlClause = "";
                string whereClause = "";
                string orderBy = "ShowroomAreaUD";
                string orderDirection = "ASC";

                using (ReportShowroomOverviewEntities context = CreateContext())
                {
                    //Showroom Overview
                    IEnumerable<ReportMng_ShowroomOverview_View> dbShowroomOverview;
                    sqlClause = "SELECT * FROM ReportMng_ShowroomOverview_View as sr";
                    whereClause = "";
                    if (!string.IsNullOrEmpty(articleCode))
                    {
                        whereClause += " AND sr.ArticleCode LIKE '%" + articleCode + "%'";
                    }
                    if (!string.IsNullOrEmpty(description))
                    {
                        whereClause += " AND sr.Description LIKE '%" + description + "%'";
                    }
                    if (showroomAreaID.HasValue)
                    {
                        whereClause += " AND sr.ShowroomAreaID = " + showroomAreaID;
                    }
                    string orderClause = " ORDER BY sr." + (string.IsNullOrEmpty(orderBy) ? "ShowroomAreaUD" : orderBy) + " " + (string.IsNullOrEmpty(orderDirection) ? "ASC" : orderDirection);

                    if (!string.IsNullOrEmpty(whereClause))
                        dbShowroomOverview = context.Database.SqlQuery<ReportMng_ShowroomOverview_View>(sqlClause + " WHERE " + whereClause.Substring(5, whereClause.Length - 5) + orderClause);
                    else
                        dbShowroomOverview = context.Database.SqlQuery<ReportMng_ShowroomOverview_View>(sqlClause + orderClause);

                    //read to dto showroom overview
                    List<DTO.ReportShowroomOverview.ShowroomOverview> showroomOverviews = converter.DB2DTO_ShowroomOverview(dbShowroomOverview.ToList());
                    List<int?> areaIDs = dbShowroomOverview.Select(o => o.ShowroomAreaID).ToList();

                    //read to dto area
                    var dbArea = context.ReportMng_ShowroomOverview_ShowroomArea_View.Where(o => areaIDs.Contains(o.ShowroomAreaID));
                    List<DTO.ReportShowroomOverview.ShowroomArea> areas = converter.DB2DTO_ShowroomArea(dbArea.ToList().OrderBy(o =>o.DisplayIndex).ToList());

                    //read to dto area image
                    var dbAreaImage = context.ReportMng_ShowroomOverview_ShowroomAreaImage_View.Where(o => areaIDs.Contains(o.ShowroomAreaID));
                    List<DTO.ReportShowroomOverview.ShowroomAreaImage> areaImages = converter.DB2DTO_ShowroomAreaImage(dbAreaImage.ToList());

                    //
                    foreach (var area in areas)
                    {
                        area.ShowroomOverviews = converter.DTO2DTO_ShowroomOverview(showroomOverviews.Where(o => o.ShowroomAreaID == area.ShowroomAreaID).ToList());
                        area.ShowroomAreaImages = converter.DTO2DTO_ShowroomAreaImage(areaImages.Where(o =>o.ShowroomAreaID == area.ShowroomAreaID).ToList());
                    }
                    return areas;
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
                return new List<DTO.ReportShowroomOverview.ShowroomArea>();
            }
        }

        public string PrintHangTag(string keyIDs,out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Export success" };
            try
            {
                ReportDataObject ds = new ReportDataObject();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_ShowroomOverview_function_PrintHangTag", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@KeyIDs", keyIDs);
                adap.TableMappings.Add("Table", "ReportMng_ShowroomOverview_HangTag_View");
                adap.TableMappings.Add("Table1", "ReportMng_ShowroomOverview_HangTag_Cushion_View");
                adap.TableMappings.Add("Table2", "ReportMng_ShowroomOverview_HangTag_Pieces_View");
                adap.Fill(ds);
                ds.AcceptChanges();

                foreach (var item in ds.ReportMng_ShowroomOverview_HangTag_View)
                {
                    if (!item.IsModelImageNull())
                    {
                        item.ModelImage = FrameworkSetting.Setting.ThumbnailUrl + item.ModelImage;
                    }
                    else
                    {
                        item.ModelImage = "NONE";
                    }

                    if (!item.IsProductIDNull())
                    {
                        item.QRCodeProductImage = FrameworkSetting.Setting.ReportTempUrl + Library.Helper.QRCodeImageFile("http://app.tilsoft.bg/Product/Edit/" + item.ProductID.ToString());
                    }
                    else
                    {
                        item.QRCodeProductImage = "NONE";
                    }

                    if (!item.IsShowroomItemIDNull())
                    {
                        item.QRCodeShowroomItemImage = FrameworkSetting.Setting.ReportTempUrl + Library.Helper.QRCodeImageFile("http://app.tilsoft.bg/ShowroomItem/Edit/" + item.ShowroomItemID.ToString());
                    }
                    else
                    {
                        item.QRCodeShowroomItemImage = "NONE";
                    }

                    //item.ShowroomItemBarCode = BarCode.BarcodeConverter39.StringToBarcode(item.ShowroomArt);
                    //item.ShowroomAreaBarCode = BarCode.BarcodeConverter39.StringToBarcode(item.ShowroomAreaUD);
                }

                foreach (var item in ds.ReportMng_ShowroomOverview_HangTag_Pieces_View)
                {
                    if (!item.IsPieceThumbImageNull())
                    {
                        item.PieceThumbImage = FrameworkSetting.Setting.ThumbnailUrl + item.PieceThumbImage;
                    }
                    else
                    {
                        item.PieceThumbImage = "NONE";
                    }
                }
                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "HangTag_Set");
                //return string.Empty;

                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "HangTag_Set");
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

        public string PrintShowroomOverview(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Export success" };
            try
            {
                ReportDataObject ds = new ReportDataObject();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_ShowroomOverview_function_PrintOverview", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.TableMappings.Add("Table", "ReportMng_ShowroomOverview_View");
                adap.Fill(ds);
                ds.AcceptChanges();

                foreach (var item in ds.ReportMng_ShowroomOverview_View)
                {
                    if (!item.IsShowroomItemThumbnailImageNull())
                    {
                        item.ShowroomItemThumbnailImage = FrameworkSetting.Setting.ThumbnailUrl + item.ShowroomItemThumbnailImage;
                    }
                    else
                    {
                        item.ShowroomItemThumbnailImage = "NONE";
                    }

                    if (!item.IsProductThumbnailImageNull())
                    {
                        item.ProductThumbnailImage = FrameworkSetting.Setting.ThumbnailUrl + item.ProductThumbnailImage;
                    }
                    else
                    {
                        item.ProductThumbnailImage = "NONE";

                    }
                }
                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ShowroomOverview");
                //return string.Empty;

                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "ShowroomOverview");
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

    }
}
