using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportPlcProducts
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportPlcProducts.DataFactory factory = new DAL.ReportPlcProducts.DataFactory();

        public string GetReportPlcProducts(int plcID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report PlcProducts");

            // query data
            return factory.GetReportPlcProducts( plcID , iRequesterID, out notification);
        }

        public DTO.ReportPlcProducts.SupportDataContainer GetFilters(out Library.DTO.Notification notification)
        {
            return factory.GetSupportData(out notification);
        }
    }
}
