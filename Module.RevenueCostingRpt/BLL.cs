using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RevenueCostingRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.InitForm GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public string GetExcelReportData(int userId, int? factoryRawMaterialID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(userId, factoryRawMaterialID, startDate, endDate, out notification);
        }
    }
}
