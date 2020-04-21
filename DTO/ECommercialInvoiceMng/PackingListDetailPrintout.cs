using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class PackingListDetailPrintout
    {
        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "QuantityShipped")]
        public int? QuantityShipped { get; set; }

        [Display(Name = "PKGs")]
        public int? PKGs { get; set; }

        [Display(Name = "NettWeight")]
        public decimal? NettWeight { get; set; }

        [Display(Name = "KGs")]
        public decimal? KGs { get; set; }

        [Display(Name = "CBMs")]
        public decimal? CBMs { get; set; }
                
    }
}
