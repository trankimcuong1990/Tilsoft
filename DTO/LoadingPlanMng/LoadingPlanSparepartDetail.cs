﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.LoadingPlanMng
{
    public class LoadingPlanSparepartDetail
    {
        public int LoadingPlanSparepartDetailID { get; set; }

        [Display(Name = "FactoryOrderSparepartDetailID")]
        public int? FactoryOrderSparepartDetailID { get; set; }

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
        public int? OfferLineSparePartID { get; set; }

        public int? PLCID { get; set; }
        public bool IsCreatedPLC { get; set; }
        public decimal? OrderSparepartQnt40HC { get; set; }
        public string Unit { get; set; }
        public int? PKGs { get; set; }
        public decimal? NettWeight { get; set; }
        public decimal? KGs { get; set; }
        public decimal? CBMs { get; set; }
        public string HSCode { get; set; }
    }
}