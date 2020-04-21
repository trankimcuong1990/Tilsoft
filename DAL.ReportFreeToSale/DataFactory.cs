using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace DAL.ReportFreeToSale
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        
        private ReportFTSEntities CreateContext()
        {
            return new ReportFTSEntities(DALBase.Helper.CreateEntityConnectionString("ReportFTSModel"));
        }
                
        public DTO.ReportFreeToSale.SearchFormData GetFreeToSaleSearch(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ReportFreeToSale.SearchFormData searchFormData = new DTO.ReportFreeToSale.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ArticleCode = string.Empty;
            string Description = string.Empty;
            string MaterialNM = string.Empty;
            string MaterialTypeNM = string.Empty;
            string MaterialColorNM = string.Empty;
            string CushionNM = string.Empty;

            if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description")) Description = filters["Description"].ToString();
            if (filters.ContainsKey("MaterialNM")) MaterialNM = filters["MaterialNM"].ToString();
            if (filters.ContainsKey("MaterialTypeNM")) MaterialTypeNM = filters["MaterialTypeNM"].ToString();
            if (filters.ContainsKey("MaterialColorNM")) MaterialColorNM = filters["MaterialColorNM"].ToString();
            if (filters.ContainsKey("CushionNM")) CushionNM = filters["CushionNM"].ToString();

            try
            {
                using (ReportFTSEntities context = CreateContext())
                {
                    totalRows = context.ReportMng_action_GetSearchFreeToSale(orderBy, orderDirection, ArticleCode, Description, MaterialNM, MaterialTypeNM, MaterialColorNM, CushionNM).Count();

                    //total fields
                    var s = context.ReportMng_FreeToSaleOverview_View.ToList();
                    searchFormData.TotalFTSQnt = s.Sum(o => (o.FTSQnt.HasValue?o.FTSQnt.Value:0));
                    searchFormData.TotalFTSQntIn40HC = s.Sum(o => (o.FTSQntIn40HC.HasValue ? o.FTSQntIn40HC.Value : 0));

                    //search result
                    var result = context.ReportMng_action_GetSearchFreeToSale(orderBy, orderDirection, ArticleCode, Description, MaterialNM, MaterialTypeNM, MaterialColorNM, CushionNM);
                    searchFormData.Data = converter.DB2DTO_FreeToSaleSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
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

        public string GetFreeToSaleOverview(bool IsIncludeImage, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get FTS report success" };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_action_GetFreeToSale", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@IsIncludeImage", IsIncludeImage);

                adap.TableMappings.Add("Table", "ParamTable");
                adap.TableMappings.Add("Table1", "ReportMng_FreeToSaleOverview_View");
                adap.Fill(ds);

                foreach (var item in ds.ReportMng_FreeToSaleOverview_View)
                {
                    if (System.IO.File.Exists(FrameworkSetting.Setting.MediaFullSizeUrl + item.SelectedThumbnailImage))
                    {
                        item.SelectedThumbnailImage = FrameworkSetting.Setting.MediaThumbnailUrl + item.SelectedThumbnailImage;
                    }
                    else
                    {
                        item.SelectedThumbnailImage = "NONE";
                    }
                }
                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ReportFreeToSaleOverview");

                // generate xml file
                string reportName = DALBase.Helper.CreateReportFiles(ds, "ReportFreeToSaleOverview");
                if (string.IsNullOrEmpty(reportName))
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = "Can not create report";
                    return string.Empty;
                }
                return reportName;
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

        public string GetFreeToSaleSelected(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get report success" };
            try
            {
                string KeyIDs = "";
                int? ClientID = null;
                if (filters.ContainsKey("KeyIDs") && filters["KeyIDs"] != null)
                {
                    KeyIDs = filters["KeyIDs"].ToString();
                }
                if (filters.ContainsKey("ClientID") && filters["ClientID"] != null)
                {
                    ClientID = Convert.ToInt32(filters["ClientID"]);
                }

                ReportDataObject ds = new ReportDataObject();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_action_GetFreeToSaleSelected", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@KeyIDs", KeyIDs);
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", ClientID);
                adap.TableMappings.Add("Table", "ReportMng_FreeToSaleOverview_View");
                adap.TableMappings.Add("Table1", "Client");
                adap.Fill(ds);
                ds.AcceptChanges();


                foreach (var item in ds.Client)
                {
                    if (!item.IsLogoImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.LogoImage))
                    {
                        item.LogoImage = FrameworkSetting.Setting.ThumbnailUrl + item.LogoImage;
                    }
                    else
                    {
                        item.LogoImage = "NONE";
                    }
                }
               

                //appy image
                foreach (var item in ds.ReportMng_FreeToSaleOverview_View)
                {
                    if (!item.IsSelectedThumbnailImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.SelectedThumbnailImage))
                    {
                        item.SelectedThumbnailImage = FrameworkSetting.Setting.ThumbnailUrl + item.SelectedThumbnailImage;
                    }
                    else
                    {
                        item.SelectedThumbnailImage = "NONE";
                    }
                    
                    if (!item.IsSelectedFullImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteFileFolder + item.SelectedFullImage))
                    {
                        item.SelectedFullImage = FrameworkSetting.Setting.FullSizeUrl + item.SelectedFullImage;
                    }
                    else
                    {
                        item.SelectedFullImage = "NONE";
                    }

                    if (!item.IsProductGardenFullImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteFileFolder + item.ProductGardenFullImage))
                    {
                        item.ProductGardenFullImage = FrameworkSetting.Setting.FullSizeUrl + item.ProductGardenFullImage;
                    }
                    else
                    {
                        item.ProductGardenFullImage = "NONE";
                    }

                    if (!item.IsCushionImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.CushionImage))
                    {
                        item.CushionImage = FrameworkSetting.Setting.ThumbnailUrl + item.CushionImage;
                    }
                    else
                    {
                        item.CushionImage = "NONE";
                    }
                    if (!item.IsMaterialImageNull() && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.MaterialImage))
                    {
                        item.MaterialImage = FrameworkSetting.Setting.ThumbnailUrl + item.MaterialImage;
                    }
                    else
                    {
                        item.MaterialImage = "NONE";
                    }
                }

                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ReportFreeToSaleSelected");
                //return string.Empty;

                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "ReportFreeToSaleSelected");

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
    }
}
