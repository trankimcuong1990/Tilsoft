using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.IncommingCashFlowPlanningRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public string GetExcelReportData(int userId, string Season, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(userId, Season, out notification);
        }
    }
}
