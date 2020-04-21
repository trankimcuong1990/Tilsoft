using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace BLL
{
    public class ReportStockOverview
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportStockOverview.DataFactory factory = new DAL.ReportStockOverview.DataFactory();

        public string GetStockOverview(Hashtable filters, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetStockOverview(filters,out notification);
        }
        public string GetStockOverviewWithDetail(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetStockOverviewWithDetail(out notification);
        }
        public List<DTO.ReportStockOverview.StockOverview> GetStockOverview_HTMLView(Hashtable filters, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetStockOverview_HTMLView(filters, out notification);
        }
    }
}
