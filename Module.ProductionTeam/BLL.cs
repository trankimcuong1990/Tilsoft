using Library.DTO;

namespace Module.ProductionTeam
{
    internal class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.EditData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public DTO.SearchData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return this.factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return this.factory.DeleteData(id, out notification);
        }
    }
}
