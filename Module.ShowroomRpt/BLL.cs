using System.Collections;

namespace Module.ShowroomRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.SearchDataDto GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            //filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public string ExportExcel(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcel(userId, filters, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(userId, out notification);
        }

        public string ExportExcelWithoutImage(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcelWithoutImage(userId, filters, out notification);
        }
    }
}
