using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng
{
    internal class BLL
    {
        private DAL.DataFactory dataFactory = new DAL.DataFactory();
        private Framework.BLL bll = new Framework.BLL();

        public object GetData(int iRequesterID, int id, out Notification notification)
        {
            return dataFactory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dto, out Notification notification)
        {
            return dataFactory.UpdateData(iRequesterID, id, ref dto, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            return dataFactory.DeleteData(id, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dto, out Notification notification)
        {
            return dataFactory.Approve(iRequesterID, id, ref dto, out notification);
        }

        public object GetInitData(int iRequesterID, out Notification notification)
        {
            return dataFactory.GetInitData(out notification);
        }

        public object GetDataWithFilter(int iRequesterID, Hashtable filter, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return dataFactory.GetDataWithFilter(filter, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        //============================================================================//
        public string ExportCostInvoice2(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            bll.WriteLog(iRequesterID, 0, "Export file Cost Invoice report");
            return dataFactory.ExportCostInvoice2(season, out notification);
        }
        //===========================================================================//
    }
}
