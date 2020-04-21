using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WEXStockOverviewRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string ExportExcel(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcel(userId, filters, out notification);
        }

        public bool UpdateSellingPrice(int userID, object dtoItems, out Library.DTO.Notification notification)
        {
            return factory.UpdateSellingPrice(userID, dtoItems, out notification);
        }

        public bool UpdatePurchasingPrice(int userID, object dtoItems, out Library.DTO.Notification notification)
        {
            return factory.UpdatePurchasingPrice(userID, dtoItems, out notification);
        }

        public bool UpdateObsolete(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            return factory.UpdateObsolete(userId, dtoItems, out notification);
        }

        public bool UpdateValueObsolescence(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            return factory.UpdateValueObsolescence(userId, dtoItems, out notification);
        }

        public bool UpdateProductPrice(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateProductPrice(userId, dtoItem, out notification);
        }

        public bool UpdateEnableStatus(int userId, object dtoJson, out Library.DTO.Notification notification)
        {
            return factory.UpdateEnableStatus(userId, dtoJson, out notification);
        }
    }
}
