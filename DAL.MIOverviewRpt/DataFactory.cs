using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MIOverviewRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MIOverviewRptEntities CreateContext()
        {
            return new MIOverviewRptEntities(DALBase.Helper.CreateEntityConnectionString("MIOverviewRptModel"));
        }

        public DTO.MIOverviewRpt.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MIOverviewRpt.ReportData data = new DTO.MIOverviewRpt.ReportData();
            data.DDCs = new List<DTO.MIOverviewRpt.DDC>();
            data.Productions = new List<DTO.MIOverviewRpt.Production>();
            data.Proformas = new List<DTO.MIOverviewRpt.Proforma>();
            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;
            string nextSeason = Library.Helper.GetNextSeason(season);

            //try to get data
            try
            {
                using (MIOverviewRptEntities context = CreateContext())
                {
                    data.DDCs = converter.DB2DTO_DDCList(context.MIOverviewRpt_function_getDDC(season).ToList());
                    data.Proformas = converter.DB2DTO_ProformaList(context.MIOverviewRpt_function_getProforma(season).ToList());
                    data.Productions = converter.DB2DTO_ProductionList(context.MIOverviewRpt_Production_View.Where(o => o.Season == season).ToList());
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));
                }
                using (MIOverviewRptEntities context2 = CreateContext())
                {
                    data.DDCNextSeasons = converter.DB2DTO_DDCList(context2.MIOverviewRpt_function_getDDC(nextSeason).ToList());                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
