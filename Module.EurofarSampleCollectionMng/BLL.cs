using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarSampleCollectionMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        public DTO.SearchFormData GetDataWithFilter(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            return factory.Delete(id, out notification);
        }
        public string ExportExcel(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcel(filters, out notification);
        }
    }
}
