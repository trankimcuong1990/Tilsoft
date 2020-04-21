using Library.DTO;
using System.Collections;

namespace Module.OPSequence
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL bll = new Framework.BLL();

        public DTO.SearchData Search(int userId, Hashtable filter, int pageSize, int pageIndex,string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.Search(userId, filter, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditData Get(int userId, int id, out Notification notification)
        {
            return factory.Get(id, out notification);
        }

        public bool Update(int userId, int id, ref object dto, out Notification notification)
        {
            return factory.Update(userId, id, ref dto, out notification);
        }

        public bool Delete(int userId, int id, out Notification notification)
        {
            return factory.Delete(id, out notification);
        }
    }
}
