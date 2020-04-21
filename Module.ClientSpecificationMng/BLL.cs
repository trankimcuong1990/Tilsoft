using Library.Base;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.ClientSpecificationMng.DAL;

namespace Module.ClientSpecificationMng
{
    internal class BLL
    {
        private DataFactory factory = new DataFactory();

        public object GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userID, int id, out Notification notification)
        {
            return factory.GetData(userID, id, out notification);
        }

        public bool UpdateData(int userID, int id, ref object dtoItems, out Notification notification)
        {
            return factory.UpdateData(userID, id, ref dtoItems, out notification);
        }

        public object PostComment(int userID, object dtoItems, out Notification notification)
        {
            return factory.PostComment(userID, dtoItems, out notification);
        }

        public int? DeleteComment(int userID, int id, out Notification notification)
        {
            return factory.DeleteComment(userID, id, out notification);
        }

        public object GetStandardFile(int userID, int typeID, int clientID, out Notification notification)
        {
            return factory.GetStandardFile(typeID, clientID, out notification);
        }
    }
}
