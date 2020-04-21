using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.ReportRAPEU
{
    public class DataFactory
    {
        public string GetReportRAPEU(bool IsRAPEU,string Season,int FactoryID, int UserID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_function_GetRAPEU", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryID", FactoryID);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                adap.SelectCommand.Parameters.AddWithValue("@IsRapEU", IsRAPEU);

                adap.TableMappings.Add("Table", "Header");
                adap.TableMappings.Add("Table1", "Report_RAPEU_Order_View");
                adap.TableMappings.Add("Table2", "Report_RAPEU_Shipped_View");
                adap.TableMappings.Add("Table3", "Report_RAPEU_Loaded_View");
                adap.TableMappings.Add("Table4", "SupplierSCMResponsible");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "RAPEU");

                // generate xml file
                //return DALBase.Helper.CreateReportFiles(ds, IsRAPEU ? "RAPEU" : "RAPVN");
                return Library.Helper.CreateReportFileWithEPPlus2(ds, IsRAPEU ? "RAPEU" : "RAPVN");
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

        

        public DTO.ReportRAPEU.SupportDataContainer GetSupportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory factory = new Support.DataFactory();

            //try to get data
            try
            {
                DTO.ReportRAPEU.SupportDataContainer dtoItem = new DTO.ReportRAPEU.SupportDataContainer();
                dtoItem.Seasons = factory.GetSeason().ToList();
                dtoItem.Factories = factory.GetFactory().ToList();
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
                return new DTO.ReportRAPEU.SupportDataContainer();
            }
        }

    }
}
