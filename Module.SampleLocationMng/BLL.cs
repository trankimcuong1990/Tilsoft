using Library.DTO;
using Module.SampleLocationMng.DAL;
using Module.SampleLocationMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleLocationMng
{
    internal class BLL
    {
        protected DataFactory dataFactory = new DataFactory();
        protected Framework.BLL bLL = new Framework.BLL();

        public SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            bLL.WriteLog(userID, 0, "Get data with filters.");

            return dataFactory.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public EditFormData GetData(int userID, int id, out Notification notification)
        {
            bLL.WriteLog(userID, 0, "Get data with product id.");

            return dataFactory.GetData(userID, id, out notification);
        }

        public DTO.EditFormData GetData(int userID, Hashtable filters, out Notification notification)
        {
            return dataFactory.GetData(userID, filters, out notification);
        }

        public bool UpdateData(int userID, int id, ref object dtoItem, out Notification notification)
        {
            bLL.WriteLog(userID, id, "Update data.");

            return dataFactory.UpdateData(userID, id, ref dtoItem, out notification);
        }

        public List<DTO.SampleProductLocationSearchResultData> UpdateData(int userID, Hashtable filters, out Notification notification)
        {
            return dataFactory.UpdateData(userID, filters, out notification);
        }
    }
}
