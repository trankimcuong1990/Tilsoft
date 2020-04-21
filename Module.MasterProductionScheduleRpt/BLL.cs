using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterProductionScheduleRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.ReportData GetMasterProductionScheduleRpt(int userId, System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetMasterProductionScheduleRpt(userId, filters, out notification);
        }
    }
}
