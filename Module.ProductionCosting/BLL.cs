using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionCosting
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();             
        public string GetExcelReportData(int userId, int? workOrderID, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(userId, workOrderID, out notification);
        }
        public object GetDataWithFilters(int iRequesterID, int? workOrderID, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(iRequesterID, workOrderID, out notification);
        }
        public DTO.EditForm GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get work order" + id.ToString());

            return factory.GetData(id, out notification);
        }
    }
}
