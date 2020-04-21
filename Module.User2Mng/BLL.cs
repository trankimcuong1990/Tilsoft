using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteUser(iRequesterID, id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public int InitAccount(int iRequesterID, DTO.NewAccountData dtoItem, out Library.DTO.Notification notification)
        {
            return factory.InitAccount(iRequesterID, dtoItem, out notification);
        }

        public bool UpdateAccount(int iRequesterID, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateAccount(iRequesterID, id, dtoItem, out notification);
        }

        public bool UpdateEmployee(int iRequesterID, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateEmployee(iRequesterID, id, dtoItem, out notification);
        }

        public bool UpdatePermission(int iRequesterID, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdatePermission(iRequesterID, id, dtoItem, out notification);
        }

        public bool UpdateFactoryAccess(int iRequesterID, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateFactoryAccess(iRequesterID, id, dtoItem, out notification);
        }

        public bool ForceChangePassword(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.ForceChangePassword(iRequesterID, id, out notification);
        }

        public bool RestoreUser(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.RestoreUser(userId, id, out notification);
        }

        public bool GetAPIKey(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetAPIKey(userId, id, out notification);
        }
    }
}
