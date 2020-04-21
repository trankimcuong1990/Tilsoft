using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MISaleManagementRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MISaleManagementRptEntities CreateContext()
        {
            return new MISaleManagementRptEntities(DALBase.Helper.CreateEntityConnectionString("MISaleManagementRptModel"));
        }

        public DTO.MISaleManagementRpt.ReportData GetReportData(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleManagementRpt.ReportData data = new DTO.MISaleManagementRpt.ReportData();
            try
            {
                using (MISaleManagementRptEntities context = CreateContext())
                {
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));

                    data.AllSaleProformaByMonths = converter.DB2DTO_AllSaleProformaByMonthList(context.MISaleManagementRpt_function_getAllSaleProfomaByMonth(season).ToList());
                    data.ConfirmedSaleProformaByMonths = converter.DB2DTO_ConfirmedSaleProformaByMonthList(context.MISaleManagementRpt_function_getConfirmedSaleProfomaByMonth(season).ToList());
                    //data.DDCProformaBySales = converter.DB2DTO_DDCProformaBySaleList(context.MISaleManagementRpt_function_getDDCProformaBySale(season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }


        /// <summary>
        /// This function like function GetReportData below, but it is special case
        /// It only use for module Delta Overview
        /// </summary>
        /// <param name="season"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.MISaleManagementRpt.ReportData GetReportData_ForDeltaOverview(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleManagementRpt.ReportData data = new DTO.MISaleManagementRpt.ReportData();
            try
            {
                using (MISaleManagementRptEntities context = CreateContext())
                {
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));

                    //data.AllSaleProformaByMonths = converter.DB2DTO_AllSaleProformaByMonthList(context.MISaleManagementRpt_function_getAllSaleProfomaByMonth(season).ToList());
                    //data.ConfirmedSaleProformaByMonths = converter.DB2DTO_ConfirmedSaleProformaByMonthList(context.MISaleManagementRpt_function_getConfirmedSaleProfomaByMonth(season).ToList());
                    data.DDCProformaBySales = converter.DB2DTO_DDCProformaBySaleList(context.MISaleManagementRpt_function_getDDCProformaBySale(season).ToList());
                }                
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }
    }
}
