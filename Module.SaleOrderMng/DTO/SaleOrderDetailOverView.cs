using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class SaleOrderDetailOverView
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

        public string CreatedDateFormated
        {
            get { return (CreatedDate.HasValue ? CreatedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string UpdatedDateFormated
        {
            get { return (UpdatedDate.HasValue ? UpdatedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public List<SaleOrderDetailExtendOverView> SaleOrderDetailExtends { get; set; }

        public int? ProductID { get; set; }
        public int? PhysicalQnt { get; set; }
        public int? OnSeaQnt { get; set; }
        public int? TobeShippedQnt { get; set; }
        public int? ReservationQnt { get; set; }
        public int? FTSQnt { get; set; }
        public int? ProductStatusID { get; set; }
        public int? V4ID { get; set; }

        [Display(Name = "IsDPReceived")]
        public bool IsDPReceived { get; set; }

        [Display(Name = "IsLCReceived")]
        public bool IsLCReceived { get; set; }

        [Display(Name = "IsNAReceived")]
        public bool IsNAReceived { get; set; }
        public int? RowIndex { get; set; }
    }
}
