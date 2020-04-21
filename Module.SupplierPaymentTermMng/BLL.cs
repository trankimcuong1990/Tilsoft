using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierPaymentTermMng
{
    public class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();
        public DTO.SearchForm GetDataWithFilter(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public DTO.EditForm GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {

            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int id, out Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }
    }
}
