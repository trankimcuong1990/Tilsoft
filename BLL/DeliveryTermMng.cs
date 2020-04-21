using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class DeliveryTermMng : Lib.BLLBase<DTO.DeliveryTermMng.DeliveryTermSearchResult, DTO.DeliveryTermMng.DeliveryTerm>
    {
        private DAL.DeliveryTermMng.DataFactory factory = new DAL.DeliveryTermMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.DeliveryTermMng.DeliveryTermSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of deliveries");
            return factory.GetDataWithFilter(new Hashtable(), pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.DeliveryTermMng.DeliveryTerm GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get delivery term " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete delivery term " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.DeliveryTermMng.DeliveryTerm dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update delivery term " + id.ToString());
            return factory.UpdateData(id, ref dtoItem, out notification);
        }
    }
}
