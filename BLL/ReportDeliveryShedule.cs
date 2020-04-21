
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportDeliveryShedule
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportDeliverySheduleMng.DataFactory factory = new DAL.ReportDeliverySheduleMng.DataFactory();

        public string GetReportDeliveryShedule(string ClientNM, string ETA, string ContainerNo, string Season, int SaleID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report Delivery Shedule");

            // query data
            return factory.GetReportDeliveryShedule(ClientNM, ETA, ContainerNo, Season, SaleID,  out notification);
        }

        public DTO.ReportDeliveryShedule.SupportDataContainer GetFilters(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData  (out notification);
        }
           
    }
}