using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryOrderMng
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

        [Display(Name = "ProductStatusID")]
        public int? ProductStatusID { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public bool IsSelected { get; set; }

    }
}