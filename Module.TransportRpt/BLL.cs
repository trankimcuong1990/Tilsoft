using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public string GetReportData(out Library.DTO.Notification notification)
        {
            return factory.GetReportData(out notification);
        }
    }
}
