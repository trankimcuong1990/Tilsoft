using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterExchangeRateMng
{
    public class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.SearchForm GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public DTO.EditForm GetData(int userId, int id, out Library.DTO.Notification notification){
            return factory.GetData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id,  ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public object UpdateIndexData(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            return factory.UpdateIndexData(userId, dtoItems, out notification);
        }
    }
}
