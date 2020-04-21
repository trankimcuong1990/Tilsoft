using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class OfferLineSparepart
    {
        [Display(Name = "OfferLineSparepartID")]
        public int? OfferLineSparepartID { get; set; }

        [Display(Name = "OfferID")]
        public int? OfferID { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }
        public int? RowIndex { get; set; }

    }
}