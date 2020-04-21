using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public DTO.SearchFormData GetDataWithFilter(int iRequestID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, "Get draft packing list.");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "Delete data width " + id.ToString());
            return factory.Delete(id, out notification);
        }

        public List<DTO.InitInfo> GetInitInfos(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetInitInfos(filters, out notification);
        }

        public DTO.DataContainer GetData(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetData(filters, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public string getDraftPackingListPrintOut(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get draft paking list printout");
            return factory.getDraftPackingListPrintOut(id, out notification);
        }
    }
}
