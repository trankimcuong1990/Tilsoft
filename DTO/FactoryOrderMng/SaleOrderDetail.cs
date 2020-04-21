using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryOrderMng
{
    public class SaleOrderDetail
    {
        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "OfferLineID")]
        public int? OfferLineID { get; set; }

        [Display(Name = "OrderQnt")]
        public int? OrderQnt { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "ClientArticleCode")]
        public string ClientArticleCode { get; set; }

        [Display(Name = "ClientArticleName")]
        public string ClientArticleName { get; set; }

        [Display(Name = "ClientOrderNumber")]
        public string ClientOrderNumber { get; set; }

        [Display(Name = "ClientEANCode")]
        public string ClientEANCode { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }


        public string ApproverName { get; set; }
        public string ApprovedDate { get; set; }
        public bool IsSelected { get; set; }
    }
}