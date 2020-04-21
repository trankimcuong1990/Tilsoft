using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.POLMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetData(id, out notification);
        }

        public bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int id, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // query data
            return factory.UpdateData(id, ref dtoItem, out notification);
        }
    }
}
