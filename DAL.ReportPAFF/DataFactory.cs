using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAL.ReportPAFF
{
    public class DataFactory
    {
        public string GetReportPAFF(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Get PAFF report success" };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_action_GetPAFF", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);

                adap.TableMappings.Add("Table", "PAFF_Sum");
                adap.TableMappings.Add("Table1", "PAFF_Chart");
                adap.Fill(ds);

                // dev
                DALBase.Helper.DevCreateReportXMLSource(ds, "PAFF");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "PAFF");
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

        public List<DTO.Support.Season> GetSeasons()
        {
            return new DAL.Support.DataFactory().GetSeason().ToList();
        }
    }
}
