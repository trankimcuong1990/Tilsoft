using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFVATRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public string GetExcelReportData(int iRequesterID, string from, string to, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get VAT Overview to excel");

            // query data
            return factory.GetExcelReportData(from, to, out notification);
        }
        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
    }
}
