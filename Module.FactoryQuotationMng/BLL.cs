using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        { 
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory quotation list");

            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public DTO.PreviewFormData GetPreviewData(int iRequesterID, int id, out Library.DTO.Notification notification)
        { 
            return factory.GetPreviewData(id, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // check authentication
            bool canAccess = factory.CheckAuthentication(id, iRequesterID, out notification);
            if (canAccess)
            {
                // keep log entry
                fwBLL.WriteLog(iRequesterID, id, "get factory quotation");

                return factory.GetData(iRequesterID, id, out notification);
            }
            else
            {
                return new DTO.EditFormData();
            }
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // check authentication
            bool canAccess = factory.CheckAuthentication(id, userId, out notification);
            if (canAccess)
            {
                // keep log entry
                fwBLL.WriteLog(userId, id, "update factory quotation");

                return factory.UpdateData(userId, id, ref dtoItem, out notification);
            }
            else
            {
                return false;
            }
        }
    }
}
