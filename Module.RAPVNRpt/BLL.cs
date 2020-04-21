using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RAPVNRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public string GetExcelReportData(bool isRAPEU, string season, int? factoryID, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(isRAPEU, season, factoryID, iRequesterID, out notification);
        }
        public object GetDataWithFilters(Hashtable input, int userId, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(input, userId, out totalRows,  out notification);
        }
        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(userId, out notification);
        }
    }
}
