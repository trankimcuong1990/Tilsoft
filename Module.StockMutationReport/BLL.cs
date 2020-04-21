using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockMutationReport
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        public string GetReportData(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(filters, out notification);
        }
    }
}
