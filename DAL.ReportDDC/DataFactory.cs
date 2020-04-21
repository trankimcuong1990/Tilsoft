using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.ReportDDC
{
    public class DataFactory
    {
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private DataConverter converter = new DataConverter();
        private DDCReportEntities CreateContext()
        {
            return new DDCReportEntities(DALBase.Helper.CreateEntityConnectionString("DDCReportModel"));
        }

        public string GetReportData(int userID, string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DDCRpt_function_GetData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                adap.TableMappings.Add("Table", "DDCRpt_ClientInfo_View");
                adap.TableMappings.Add("Table1", "DDCRpt_Sale_View");
                adap.TableMappings.Add("Table2", "Report_Setting_View");
                adap.Fill(ds);

                // change string to numeric value
                //foreach (ReportDataObject.Report_Setting_ViewRow sRow in ds.Report_Setting_View)
                //{
                //    if (!sRow.IsSettingValueNull())
                //    {
                //        sRow.SettingValue = sRow.SettingValue.Replace(".", ",");
                //    }
                //}

                // add report param
                ReportDataObject.ReportParamRow pRow = ds.ReportParam.NewReportParamRow();
                pRow.Season = Season;
                pRow.PreSeason = Library.OMSHelper.Helper.GetPrevSeason(Season);
                ds.ReportParam.AddReportParamRow(pRow);
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "DDCReport", new List<string>() { "DDCRpt_ClientInfo_View", "DDCRpt_Sale_View", "Report_Setting_View", "ReportParam" });

                // generate xml file
                //return DALBase.Helper.CreateReportFiles(ds, "DDCReport");
                //return Library.Helper.CreateCOMReportFileImportDataOnly(ds, "DDCReport");

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

        public DTO.ReportDDC.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportDDC.InitFormData data = new DTO.ReportDDC.InitFormData();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.DDCReport.ReportData GetReportData_HTML(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DDCReport.ReportData data = new DTO.DDCReport.ReportData();
            data.ClientInfos = new List<DTO.DDCReport.ClientInfo>();
            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;
            data.ExchangeRateLastSeason  = 0;
            string lastSeason = Library.Helper.GetPreviousSeason(season);
            try
            {
                using (DDCReportEntities context = CreateContext())
                {
                    
                    data.ClientInfos = converter.DB2DTO_ClientInfo(context.DDCRpt_function_GetData(season, userID).ToList());
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));
                    data.Seasons = supportFactory.GetSeason().ToList();
                    data.ExchangeRateLastSeason = Convert.ToDecimal(supportFactory.GetSettingValue(lastSeason, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
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
    }
}
