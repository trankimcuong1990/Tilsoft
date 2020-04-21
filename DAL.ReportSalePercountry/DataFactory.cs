using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.ReportSalePerCountry
{
    public class DataFactory
    {
        public string GetReportData(string Season, int SaleID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_action_GetSalePerCountry", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@SaleID", SaleID);

                adap.TableMappings.Add("Table",  "Expected");
                adap.TableMappings.Add("Table1", "ExpectedCompareWithProfomasConfirmed");
                adap.TableMappings.Add("Table2", "ParamTable");
                adap.Fill(ds);

                // dev
                DALBase.Helper.DevCreateReportXMLSource(ds, "SalePerCountry");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "SalePerCountry");
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



        public DTO.ReportSalePerCountry.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory factory = new Support.DataFactory();

            //try to get data
            try
            {
                DTO.ReportSalePerCountry.SupportDataContainer dtoItem = new DTO.ReportSalePerCountry.SupportDataContainer();
                dtoItem.Seasons = factory.GetSeason().ToList();
                dtoItem.Salers = factory.GetSaler().ToList();
                return dtoItem;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new DTO.ReportSalePerCountry.SupportDataContainer();
            }
        }

    }
}
