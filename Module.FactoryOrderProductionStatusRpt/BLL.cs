using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderProductionStatusRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.ReportData GetFactoryOrderProductionStatusRpt(int userId, System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetFactoryOrderProductionStatusRpt(userId, filters, out notification);
        }
    }
}
