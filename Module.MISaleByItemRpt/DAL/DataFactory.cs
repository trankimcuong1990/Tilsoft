using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private MISaleByItemRptEntities CreateContext()
        {
            return new MISaleByItemRptEntities(Library.Helper.CreateEntityConnectionString("DAL.MISaleByItemRptModel"));
        }

        public DTO.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            try
            {
                using (MISaleByItemRptEntities context = CreateContext())
                {
                    data.Top20ByCategories = converter.DB2DTO_Top20ByCategoryList(context.MISaleByItemRpt_Top20ByCategory_View.Where(o => o.Season == season).ToList());
                    data.Top30s = converter.DB2DTO_Top30List(context.MISaleByItemRpt_Top30_View.Where(o => o.Season == season).OrderByDescending(o =>o.EURAmount).Take(30).ToList());
                    data.ItemByClients = converter.DB2DTO_ItemByClient(context.MISaleByItemRpt_ItemByClient_View.Where(o => o.Season == season).ToList());
                    data.CommercialInvoiceByCategories = converter.DB2DTO_CommercialInvoiceByCategories(context.MISaleByItemRpt_CommercialInvoiceByCategories_View.Where(o => o.Season == season).ToList());

                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }

        public DTO.ReportData GetReportData_ForDeltaOverview(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            try
            {
                using (MISaleByItemRptEntities context = CreateContext())
                {                    
                    data.CommercialInvoiceByCategories = converter.DB2DTO_CommercialInvoiceByCategories(context.MISaleByItemRpt_CommercialInvoiceByCategories_View.Where(o => o.Season == season).ToList());                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.PiConfirmedByItemCategory> GetPiConfirmedByItemCategory(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PiConfirmedByItemCategory> data = new List<DTO.PiConfirmedByItemCategory>();
            try
            {
                using (MISaleByItemRptEntities context = CreateContext())
                {
                    var dbData = context.MISaleByItemRpt_function_GetPiConfirmedByItemCategory(season).ToList();
                    data = AutoMapper.Mapper.Map<List<MISaleByItemRpt_function_GetPiConfirmedByItemCategory_Result>,List<DTO.PiConfirmedByItemCategory>>(dbData);
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
