using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class SaleOrderDetailSparepart
    {
        [Display(Name = "SaleOrderDetailSparepartID")]
        public int? SaleOrderDetailSparepartID { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "OfferLineSparepartID")]
        public int? OfferLineSparepartID { get; set; }

        [Display(Name = "OrderQnt")]
        public int? OrderQnt { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int? ProductStatusID { get; set; }
        public int? V4ID { get; set; }
        public int? RowIndex { get; set; }

        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientEANCode { get; set; }

        public bool IsSelected { get; set; }
        public bool IsFactoryOrderCreated { get; set; }
    }
}
