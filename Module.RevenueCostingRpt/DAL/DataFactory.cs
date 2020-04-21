using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RevenueCostingRpt.DAL
{
    public class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private RevenueCostingRptEntities CreateContext()
        {
            return new RevenueCostingRptEntities(Library.Helper.CreateEntityConnectionString("DAL.RevenueCostingRptModel"));
        }

        public DTO.InitForm GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitForm data = new DTO.InitForm();
            data.SupplierDTOs = new List<DTO.SupplierDTO>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.SupplierDTOs = converter.DB2DTO_GetSubSupplier(context.Supplier.Where(o=>o.SupplierID== 336037).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(int userId, int? factoryRawMaterialID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(userId);
            decimal exchangeRate = 1;
            try
            {
                using (RevenueCostingRptEntities context = CreateContext())
                {
                    exchangeRate = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault().ExchangeRate.Value;
                }
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("RevenueCostingRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SupplierID", factoryRawMaterialID);
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);
                adap.TableMappings.Add("Table", "RevenueCosting_Report");
                adap.Fill(ds);
                for(int i=0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ReportDataObject.OtherDataRow dateRange = ds.OtherData.NewOtherDataRow();
                    dateRange.StartDate = startDate;
                    dateRange.EndDate = endDate;
                    dateRange.ExchangeRate = exchangeRate;
                    ds.OtherData.AddOtherDataRow(dateRange);
                }
                
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "RevenueCostingRpt");
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
