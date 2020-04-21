using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientConditionRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private ClientConditionRptEntities CreateContext()
        {
            return new ClientConditionRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ClientConditionRptModel"));
        }

        public DTO.ReportData GetHtmlReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            data.CheckLists = new List<DTO.CheckList>();

            //try to get data
            try
            {
                using (ClientConditionRptEntities context = CreateContext())
                {
                    data.CheckLists = converter.DB2DTO_CheckList(context.ClientConditionRpt_CheckListHtml_View.Where(o => o.Season == season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientConditionRpt_function_getExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.Fill(ds.ClientConditionRpt_CheckListExcel_View);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.Season = Season;
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "ClientCondition");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "ClientCondition");
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

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();

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
    }
}
