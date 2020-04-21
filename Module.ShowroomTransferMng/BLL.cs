using System.Collections;

namespace Module.ShowroomTransferMng
{
    internal class BLL
    {
        public readonly DAL.DataFactory dataFactory = new DAL.DataFactory();

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return dataFactory.GetInitData(userId, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return dataFactory.GetData(id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return dataFactory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return dataFactory.DeleteData(id, out notification);
        }
    }
}
