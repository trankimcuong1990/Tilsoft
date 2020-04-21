using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.SummaryOutWardRpt.DTO;

namespace Module.SummaryOutWardRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private SummaryOutWardRptEntities CreateContext()
        {
            return new SummaryOutWardRptEntities(Library.Helper.CreateEntityConnectionString("DAL.SummaryOutWardRptModel"));
        }
        public string GetExcelReportData(int userId, int? month, int? year, string workOrderStatusNM, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(userId);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SummaryOutWardRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.SelectCommand.Parameters.AddWithValue("@Month", month);
                adap.SelectCommand.Parameters.AddWithValue("@Year", year);
                adap.SelectCommand.Parameters.AddWithValue("@Status", workOrderStatusNM);
                adap.TableMappings.Add("Table", "SummaryOutWardReportDataView");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "SummaryOutWardRpt");
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
        public DTO.SearchForm GetDataWithFilters(int userId, int? month, int? year, string workOrderStatusNM, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();
            data.SummaryOutWardReportDatas = new List<DTO.SummaryOutWardReportData>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    var results = context.SummaryOutWardRpt_function_GetReportData(companyID, month, year, workOrderStatusNM);
                    data.SummaryOutWardReportDatas = converter.DB2DTO_GetSummaryOutWardReportData(results.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditForm GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
