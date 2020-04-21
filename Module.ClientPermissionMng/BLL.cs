using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPermissionMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        public DTO.SearchData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }
        public DTO.EditData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }
        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return this.factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return this.factory.DeleteData(id, out notification);
        }
        public List<DTO.Employee> QuickSearchEmployee(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {           
            return factory.QuickSearchEmployee(filters, out notification);
        } public List<DTO.ModuleDTO> QuickSearchModule(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {           
            return factory.QuickSearchModule(filters, out notification);
        }
    }
}
