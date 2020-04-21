using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseInvoiceGrossMarginRpt.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();

        private WarehouseInvoiceGrossMarginRptEntities CreateContext()
        {
            return new WarehouseInvoiceGrossMarginRptEntities(Library.Helper.CreateEntityConnectionString("DAL.WarehouseInvoiceGrossMarginRptModel"));
        }

        public object GetDataWithFilter(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");

            totalRows = 0;

            List<DTO.GrossMarginData> data = new List<DTO.GrossMarginData>();

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

                using (WarehouseInvoiceGrossMarginRptEntities context = CreateContext())
                {
                    totalRows = context.WarehouseInvoiceGrossMarginRpt_function_GrossMarginSearchResult(filterWithSeason, filtersWithCode, filtersWithDate, filtersWithDateTo, orderBy, orderDirection).Count();
                    data = converter.DB2DTO_GrossMarginDataSearchResult(context.WarehouseInvoiceGrossMarginRpt_function_GrossMarginSearchResult(filterWithSeason, filtersWithCode, filtersWithDate, filtersWithDateTo, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetInitData(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormData data = new DTO.InitFormData();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.SupportSeason = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
