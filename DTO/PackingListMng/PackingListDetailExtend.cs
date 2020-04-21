using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PackingListMng
{
    public class PackingListDetailExtend
    {
        [Display(Name = "PackingListDetailExtendID")]
        public int? PackingListDetailExtendID { get; set; }

        [Display(Name = "PackingListID")]
        public int? PackingListID { get; set; }

        [Display(Name = "ECommercialInvoiceID")]
        public int? ECommercialInvoiceID { get; set; }

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

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "SealNo")]
        public string SealNo { get; set; }

        [Display(Name = "ContainerType")]
        public string ContainerType { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

    }
}