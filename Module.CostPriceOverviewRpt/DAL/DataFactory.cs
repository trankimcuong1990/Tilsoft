using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostPriceOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public string GetExcelReportData(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("CostPriceOverviewRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.TableMappings.Add("Table", "CostPriceOverviewRpt_Overview_View");
                adap.TableMappings.Add("Table1", "CostPriceOverviewRpt_MarginCustomer_View");
                adap.TableMappings.Add("Table2", "ReportHeader");
                adap.TableMappings.Add("Table3", "CostPriceOverviewRpt_SaleManager_View");
                adap.TableMappings.Add("Table4", "CostPriceOverviewRpt_Supplier_View");                
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus(ds, "CostPriceOverview", new List<string>() { "CostPriceOverviewRpt_Overview_View", "CostPriceOverviewRpt_MarginCustomer_View", "CostPriceOverviewRpt_SaleManager_View", "ReportHeader", "CostPriceOverviewRpt_Supplier_View" });
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
    }
}
