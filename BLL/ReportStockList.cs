using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace BLL
{
    public class ReportStockList 
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportStockList.DataFactory factory = new DAL.ReportStockList.DataFactory();

        public string GetReportStockList(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report stock list");

            // query data
            return factory.GetStockListReport(filters, out notification);
        }

        public DTO.ReportStockList.SearchFormData GetStockListSearch(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetStockListSearch(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool ActiveFreeToSaleProduct(int productID, bool? isActiveFreeToSale, out Library.DTO.Notification notification)
        {
            return factory.ActiveFreeToSaleProduct(productID, isActiveFreeToSale, out notification);
        }

        public bool SetFreeToSaleCategory(int productID, int? freeToSaleCategoryID, out Library.DTO.Notification notification)
        {
            return factory.SetFreeToSaleCategory(productID, freeToSaleCategoryID, out notification);
        }

        public List<DTO.ReportStockList.StockReservation> GetStockReservation(int? goodsID, string productType, int? productStatusID, out Library.DTO.Notification notification)
        {
            return factory.GetStockReservation(goodsID, productType, productStatusID, out notification);
        }

        public bool MatchedImageProduct(int productID, bool? isMatchedImage, out Library.DTO.Notification notification)
        {
            return factory.MatchedImageProduct(productID, isMatchedImage, out notification);
        }

        public DTO.ReportStockList.StockListDetail GetStockListDetail(string keyID, out Library.DTO.Notification notification)
        {
            return factory.GetStockListDetail(keyID, out notification);
        }
        
    }
}
