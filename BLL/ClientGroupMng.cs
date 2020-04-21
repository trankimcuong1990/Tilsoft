using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class ClientGroupMng : Lib.BLLBase<DTO.ClientGroupMng.ClientGroupSearchResult, DTO.ClientGroupMng.ClientGroup>
    {
        private DAL.ClientGroupMng.DataFactory factory = new DAL.ClientGroupMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.ClientGroupMng.ClientGroupSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of client groups");

            // query data
            return factory.GetDataWithFilter(new Hashtable(), pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ClientGroupMng.ClientGroup GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client group " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete client group " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ClientGroupMng.ClientGroup dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update client group " + id.ToString());

            // query data
            return factory.UpdateData(id, ref dtoItem, out notification);
        }
    }
}
