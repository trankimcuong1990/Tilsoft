using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PackingListMng
{
    public class PackingListDetail
    {
        [Display(Name = "PackingListDetailID")]
        public int? PackingListDetailID { get; set; }

        [Display(Name = "PackingListID")]
        public int? PackingListID { get; set; }

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

        [Display(Name = "PurchasingInvoiceDetailID")]
        public int? PurchasingInvoiceDetailID { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "OrderNo")]
        public string OrderNo { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

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

    }
}