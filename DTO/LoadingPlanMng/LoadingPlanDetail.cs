using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
    public class LoadingPlanDetail
    {
        public int LoadingPlanDetailID { get; set; }

        [Display(Name = "FactoryOrderDetailID")]
        public int? FactoryOrderDetailID { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "LDS")]
        public string LDS { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Qnt40HC")]
        public int? Qnt40HC { get; set; }

        [Display(Name = "OrderQnt")]
        public int? OrderQnt { get; set; }

        public decimal? QuotationSalePrice { get; set; }

        public int? FactoryID { get; set; }
        public int? OfferLineID { get; set; }

        public int? PLCID { get; set; }
        public bool IsCreatedPLC { get; set; }
        public decimal? OrderProductQnt40HC { get; set; }

        public string ImageFile { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public int? ProductID { get; set; }

        public int? RemainQnt { get; set; }
        public string Unit { get; set; }
        public int? PKGs { get; set; }
        public decimal? NettWeight { get; set; }
        public decimal? KGs { get; set; }
        public decimal? CBMs { get; set; }
        public string HSCode { get; set; }
    }
}