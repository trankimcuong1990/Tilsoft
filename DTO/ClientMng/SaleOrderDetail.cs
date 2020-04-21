using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class SaleOrderDetail
    {
        public object KeyID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? GoodsID { get; set; }
        public string GoodsType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientEANCode { get; set; }
        public int? OfferQnt { get; set; }
        public string Currency { get; set; }
    }
}
