using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportShipmentAmount
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportShipmentAmount.DataFactory factory = new DAL.ReportShipmentAmount.DataFactory();

        public string GetShipmentAmountNL(string Season, out Library.DTO.Notification notification)
        {
            return factory.GetShipmentAmountNL(Season, out notification);
        }

        public string GetShipmentAmountVN(string Season, out Library.DTO.Notification notification)
        {
            return factory.GetShipmentAmountVN(Season, out notification);
        }

        public IEnumerable<DTO.Support.Season> GetSeasons(out Library.DTO.Notification notification)
        {
            return factory.GetSeasons(out notification);
        }
    }
}
