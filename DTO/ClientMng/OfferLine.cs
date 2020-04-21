using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class OfferLine
    {
        public object KeyID { get; set; }
        public int? OfferID { get; set; }
        public int? GoodsID { get; set; }
        public string GoodsType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? SalePriceCalculated { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public decimal? OrderQntIn40HC { get; set; }
        public decimal? FinalPrice { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Currency { get; set; }
    }
}
