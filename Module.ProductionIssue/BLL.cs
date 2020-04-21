using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public object GetInit(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInit(iRequesterID, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public object GetWorkOrder(int iRequesterID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetWorkOrder(iRequesterID, searchQuery, out notification);
        }

        public object GetHistoryDeliveryNote(int iRequesterID, int workOrderID, int workCenterID, int productionItemID, out Library.DTO.Notification notification)
        {
            return factory.GetHistoryDeliveryNote(iRequesterID, workOrderID, workCenterID, productionItemID, out notification);
        }

        public object UpdateData(int userID, Hashtable filters, out Library.DTO.Notification notification) {
            return factory.UpdateData(userID, filters, out notification);
        }
    }
}
