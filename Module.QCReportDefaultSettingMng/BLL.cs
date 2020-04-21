using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportDefaultSettingMng
{
    internal class BLL
    {
        DAL.DataFactory dataFactory = new DAL.DataFactory();
        Framework.BLL fwBLL = new Framework.BLL();

        public object GetData(int iRequesterID, int id, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "Get data width " + id.ToString());
            return dataFactory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object objItem, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "Update data width " + id.ToString());
            return dataFactory.UpdateData(iRequesterID, id, ref objItem, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "Delete data width " + id.ToString());
            return dataFactory.DeleteData(id, out notification);
        }
        public DTO.SearchFormData GetDataWithFilter(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Notification notification)
        {
            return dataFactory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }
    }
}
