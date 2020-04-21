using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.FactoryWarehouse
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get Factory Warehouse list");
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete Factory Warehouse" + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update actory Warehouse" + id.ToString());
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public object GetData(int iRequesterID, int id, int branchID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get actory Warehouse" + id.ToString());
            return factory.GetData1(iRequesterID, id, branchID, out notification);
        }
        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public DTO.CapacityData GetCapacityData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetCapacityData(id, out notification);
        }

        public DTO.CapacityDetailData GetCapacityDetailData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetCapacityDetailData(id, out notification);
        }
    }
}
