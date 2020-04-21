using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory norm list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete factory norm" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update factory norm" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory norm" + id.ToString());

            return factory.GetData(id, out notification);
        }

        public int CreateNewFactoryFinishedProduct(int iRequesterID, string code, string name, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create new factory finished product");

            return factory.CreateNewFactoryFinishedProduct(code, name, out notification);
        }


        public int CreateComponentNorm(int iRequesterID, int factoryNormID, DTO.FactoryFinishedProductNorm dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create new factory component norm");

            return factory.CreateComponentNorm(factoryNormID, dtoItem, out notification);
        }

        public int CreateSubComponentNorm(int iRequesterID, int parentFactoryFinishedProductNormID, DTO.FactoryFinishedProductNorm dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create new factory sub component norm");

            return factory.CreateSubComponentNorm(parentFactoryFinishedProductNormID, dtoItem, out notification);
        }

        public int CreateFactoryMaterialNorm(int iRequesterID, int factoryFinishedProductNormID, DTO.FactoryMaterialNorm dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "create new factory material norm");

            return factory.CreateFactoryMaterialNorm(factoryFinishedProductNormID, dtoItem, out notification);
        }

        public int DeleteFinishedProductNorm(int iRequesterID, int id ,out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete component norm");

            return factory.DeleteFinishedProductNorm(id, out notification);
        }

        public int DeleteMaterialNorm(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete material norm");

            return factory.DeleteMaterialNorm(id, out notification);
        }
    }
}
