using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class WarehouseTransportMng : Lib.BLLBase<DTO.WarehouseTransportMng.WarehouseTransportSearch, DTO.WarehouseTransportMng.WarehouseTransport>
    {
        private DAL.WarehouseTransportMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public WarehouseTransportMng()
        {
            factory = new DAL.WarehouseTransportMng.DataFactory();
        }
        
        public override IEnumerable<DTO.WarehouseTransportMng.WarehouseTransportSearch> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of warehouse transport");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.WarehouseTransportMng.WarehouseTransport GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get WarehouseTransport " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete WarehouseTransport " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.WarehouseTransportMng.WarehouseTransport dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update WarehouseTransport " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

               
        public DTO.WarehouseTransportMng.EditSupportList GetEditSupportData()
        {
            return factory.GetEditSupportData();
        }
        
        public DTO.WarehouseTransportMng.WarehouseTransport GetEditData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get WarehouseTransport " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public List<DTO.WarehouseTransportMng.PhysicalStock> QuickSearchProduct(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
       
    }
}
