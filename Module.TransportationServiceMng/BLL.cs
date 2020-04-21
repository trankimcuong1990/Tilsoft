using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Module.TransportationServiceMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        public DTO.SearchFormData GetDataWithFilter(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {

            return factory.GetEditData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Update(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            return factory.Delete(id, out notification);
        }
    }
}
