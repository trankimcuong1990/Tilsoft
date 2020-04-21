using System.Collections;
using Library.DTO;

namespace Module.OPSequence
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        #region Generate functions and methods

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.Delete(userId, id, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return bll.Get(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.Search(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.Update(userId, id, ref dtoItem, out notification);
        }

        #endregion
    }
}
