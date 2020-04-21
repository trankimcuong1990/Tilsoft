using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory production status list");
            filters["userId"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete factory production status" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update factory production status" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int? factoryID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory production status" + id.ToString());

            return factory.GetData(iRequesterID,id, factoryID,season, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.ProductionOverviewData GetProductionOverview(int iRequesterID, int factoryID, string season, bool isGetSupportData, out Notification notification)
        {
            return factory.GetProductionOverview(iRequesterID, factoryID,season,isGetSupportData, out notification);
        }

        public List<DTO.OrderDetail> GetOrderDetail(int iRequesterID, int factoryOrderID, string excludeFactoryOrderDetailIDs, out Notification notification)
        {
            return factory.GetOrderDetail(factoryOrderID, excludeFactoryOrderDetailIDs, out notification);
        }


    }
}
