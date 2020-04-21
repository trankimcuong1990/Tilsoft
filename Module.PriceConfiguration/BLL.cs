using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL bll = new Framework.BLL();

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }

        public bool DeleteData(int userId, int id, string season, out Notification notification)
        {
            return factory.DeleteData(id, season, out notification);
        }

        public DTO.EditData GetData(int userId, int id, out Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public DTO.SearchData GetDataWithFilter(int userId, Hashtable filter, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(filter, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public DTO.EditData GetData(int userId, int id, string season, out Notification notification)
        {
            return factory.GetData(id, season, out notification);
        }
    }
}
