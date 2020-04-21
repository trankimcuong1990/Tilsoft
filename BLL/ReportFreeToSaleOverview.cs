using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace BLL
{
    public class ReportFreeToSaleOverview
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportFreeToSale.DataFactory factory = new DAL.ReportFreeToSale.DataFactory();

        public DTO.ReportFreeToSale.SearchFormData GetFreeToSaleSearch(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetFreeToSaleSearch(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetFreeToSaleOverview(bool isIncludeImage, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetFreeToSaleOverview(isIncludeImage, out notification);
        }

        public string GetFreeToSaleSelected(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetFreeToSaleSelected(filters, out notification);
        }
    }
}
