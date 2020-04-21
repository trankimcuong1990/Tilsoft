using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProduct
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get price list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete FactoryFinishedProduct" + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update FactoryFinishedProduct" + id.ToString());
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public object GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get FactoryFinishedProduct" + id.ToString());
            return factory.GetData(id, out notification);
        }

        public int CreateFactoryFinishedProductPrice(int iRequesterID, int factoryFinishedProductID, object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update component price");
            return factory.CreateFactoryFinishedProductPrice(factoryFinishedProductID, dtoItem, out notification);
        }

        public bool DeleteFactoryFinishedProductPrice(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete component price" + id.ToString());
            return factory.DeleteFactoryFinishedProductPrice(id, out notification);
        }


    }
}
