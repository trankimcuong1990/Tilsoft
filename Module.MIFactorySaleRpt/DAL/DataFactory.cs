using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIFactorySaleRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private MIFactorySaleRptEntities CreateContext()
        {
            return new MIFactorySaleRptEntities(Library.Helper.CreateEntityConnectionString("DAL.MIFactorySaleRptModel"));
        }

        public DTO.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            data.FactorySales = new List<DTO.FactorySale>();
            data.Season = season;
            //try to get data
            try
            {
                using (MIFactorySaleRptEntities context = CreateContext())
                {
                    data.FactorySales = converter.DB2DTO_FactorySale(context.MIFactorySaleRpt_function_getFactorySale(season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
            }
            return data;
        }
    }
}
