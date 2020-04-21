using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FSCRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "Get init data support");
            return factory.GetInitData(out notification);
        }

        public string ExportFSCReport(int iRequesterID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "Export file FSC report");
            return factory.ExportFSCReport(startDate, endDate, out notification);
        }
    }
}
