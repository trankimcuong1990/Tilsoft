using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt
{
    internal class BLL
    {
        private DAL.DataFactory dataFactory = new DAL.DataFactory();

        public object GetInitForm(int iRequesterID, out Library.DTO.Notification notification)
        {
            return dataFactory.GetInitForm(iRequesterID, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
    }
}
