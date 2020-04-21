using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionScheduleRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.ReportData GetProductionSchedule(int userId, System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetProductionSchedule(userId, filters, out notification);
        }

        public int FinishComponent(int userId, System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.FinishComponent(userId, filters, out notification);
        }


    }
}
