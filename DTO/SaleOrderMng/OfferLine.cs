using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class OfferLine
    {
        [Display(Name = "OfferLineID")]
        public int? OfferLineID { get; set; }

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

        public int? ProductID { get; set; }
        public int? PhysicalQnt { get; set; }
        public int? OnSeaQnt { get; set; }
        public int? TobeShippedQnt { get; set; }
        public int? ReservationQnt { get; set; }
        public int? FTSQnt { get; set; }

        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public int? RowIndex { get; set; }

    }
}