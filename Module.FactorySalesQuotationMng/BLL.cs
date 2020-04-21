using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng
{
    internal class BLL
    {
        private DAL.FactorySalesQuotationMngFactory factory = new DAL.FactorySalesQuotationMngFactory();
        private Framework.BLL fwBLL = new Framework.BLL();        

        public BLL()
        {
            factory = new DAL.FactorySalesQuotationMngFactory();
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of Production Item");

            // query data
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(iRequesterID,filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.FactorySaleQuotationData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            //fwBLL.WriteLog( 0, "get as Production Item Group" + id.ToString());

            // query data
            return factory.GetData(userId, id, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete as Production Item Group" + id.ToString());

            // query data
            return factory.DeleteData( iRequesterID, id, out notification);

        }
        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update Production Item Group" + id.ToString());

            // query data
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);

        }
        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }
        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();           
        }
        public object GetFilterQuotation(int iRequesterID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetFilterQuotation(iRequesterID, searchQuery, out notification);
        }
        public object GetFilterRawMaterial(int iRequesterID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetFilterRawMaterial(iRequesterID, searchQuery, out notification);
        }
        public object GetFilterSaleEmployee(int iRequesterID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetFilterSaleEmployee(iRequesterID, searchQuery, out notification);
        }
        public object GetFilterProductItem(int iRequesterID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetFilterProductItem(iRequesterID, searchQuery, out notification);
        }
        public object GetFilterClientcontact(int iRequesterID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetFilterClientcontact(iRequesterID, searchQuery, out notification);
        }
        public bool Cancel(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Cancel(userId, id, ref dtoItem, out notification);
        }
    }
}
