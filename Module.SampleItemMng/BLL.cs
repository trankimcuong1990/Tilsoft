using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Module.SampleItemMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormDataDTO GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.SearchFilterDataDTO GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }
        public DTO.EditFormDataDTO GetData(int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }    
        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "update sample item");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }
        public bool UpdateProgressData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {          
            fwBLL.WriteLog(iRequesterID, id, "update progress");
            return factory.UpdateProgressData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteProgress(int iRequesterID, int id, out Library.DTO.Notification notification)
        {          
            fwBLL.WriteLog(iRequesterID, id, "delete progress");
            return factory.DeleteProgress(id, out notification);
        }
        public bool UpdateQARemarkData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateQARemarkData(iRequesterID, id, ref dtoItem, out notification);
        }
        public bool DeleteQARemark(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteQARemark(id, out notification);
        }

        public bool UpdateProductStatus(int iRequesterID, int id, int statusId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update product status");
            return factory.UpdateProductStatus(iRequesterID, id, statusId, out notification);
        }
        public bool UpdateProductStatus2(int iRequesterID, int id, int statusId, string file, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update product status");
            return factory.UpdateProductStatus2(iRequesterID, id, statusId, file, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.DeleteData(id, out notification);
        }
        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.SupportFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return factory.GetInitData(out notification);
        }
    }
}
