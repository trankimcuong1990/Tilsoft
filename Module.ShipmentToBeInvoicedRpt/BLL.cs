using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShipmentToBeInvoicedRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public object GetSupportSeasons(int userID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, " Get support season! ");

            return factory.GetSupportSeasons(out notification);
        }

        public string ExportExcel(int userID, string season, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, " Get export excel! ");

            return factory.ExportExcel(season, out notification);
        }
    }
}
