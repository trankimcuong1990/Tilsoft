using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;

namespace Module.StandardCostPriceOverviewRpt
{
   public class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();
        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);

        }

        public string ExportExcel(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcel(userId, filters, out notification);
        }

        public object GetData(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetData(filters, out notification);
        }

        public object SetDefaultFactory(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.SetDefaultFactory(userId, filters, out notification);
        }
        public bool UpdateProductPrice(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateProductPrice(userId, dtoItem, out notification);
        }
    }
}
