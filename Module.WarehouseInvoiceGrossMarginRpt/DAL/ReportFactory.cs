using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseInvoiceGrossMarginRpt.DAL
{
    public class ReportFactory
    {
        public string ExportExcel(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");

            string fileName = null;
            WarehouseInvoiceGrossMarginReportObject ds = new WarehouseInvoiceGrossMarginReportObject();

            try
            {
                string filterWithSeason = filters.ContainsKey("season") && filters["season"] != null && !string.IsNullOrEmpty(filters["season"].ToString().Trim()) ? filters["season"].ToString() : null;
                string filtersWithCode = filters.ContainsKey("commercialSaleInvoiceNr") && filters["commercialSaleInvoiceNr"] != null && !string.IsNullOrEmpty(filters["commercialSaleInvoiceNr"].ToString().Trim()) ? filters["commercialSaleInvoiceNr"].ToString() : null;
                string filtersWithDate = null;
                string filtersWithDateTo = null;
                DateTime? tmpDate = null;
                if (filters.ContainsKey("commercialSaleInvoiceDate") && filters["commercialSaleInvoiceDate"] != null && !string.IsNullOrEmpty(filters["commercialSaleInvoiceDate"].ToString().Trim()))
                {
                    tmpDate = Library.Helper.ConvertStringToDateTime(filters["commercialSaleInvoiceDate"].ToString(), cInfo);
                    if (tmpDate.HasValue)
                    {
                        filtersWithDate = tmpDate.Value.ToString("yyyy-MM-dd");
                    }
                }
                if (filters.ContainsKey("commercialSaleInvoiceDateTo") && filters["commercialSaleInvoiceDateTo"] != null && !string.IsNullOrEmpty(filters["commercialSaleInvoiceDateTo"].ToString().Trim()))
                {
                    tmpDate = Library.Helper.ConvertStringToDateTime(filters["commercialSaleInvoiceDateTo"].ToString(), cInfo);
                    if (tmpDate.HasValue)
                    {
                        filtersWithDateTo = tmpDate.Value.ToString("yyyy-MM-dd");
                    }
                }

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("WarehouseInvoiceGrossMarginRpt_function_GrossMarginExportExcel", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@filterWithSeason", filterWithSeason);
                adap.SelectCommand.Parameters.AddWithValue("@filterWithCode", filtersWithCode);
                adap.SelectCommand.Parameters.AddWithValue("@filterWithDate", filtersWithDate);
                adap.SelectCommand.Parameters.AddWithValue("@filterWithDateTo", filtersWithDateTo);
                adap.SelectCommand.Parameters.AddWithValue("@sortedBy", DBNull.Value);
                adap.SelectCommand.Parameters.AddWithValue("@sortedDirection", DBNull.Value);

                adap.TableMappings.Add("Table", "GrossMarginData");
                adap.Fill(ds);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "WarehouseInvoiceGrossMarginRpt");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }
    }
}
