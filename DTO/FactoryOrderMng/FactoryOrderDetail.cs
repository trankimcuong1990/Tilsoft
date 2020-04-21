using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryOrderMng
{
    public class FactoryOrderDetail
    {
        [Display(Name = "FactoryOrderDetailID")]
        public int? FactoryOrderDetailID { get; set; }

        [Display(Name = "FactoryOrderID")]
        public int? FactoryOrderID { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "OrderQnt")]
        public int? OrderQnt { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "SaleOrderQnt")]
        public int? SaleOrderQnt { get; set; }
        public int? V4ID { get; set; }

        [Display(Name = "TotalQnt")]
        public int? TotalQnt { get; set; }

        [Display(Name = "RemainQnt")]
        public int? RemainQnt { get; set; }

        public int? LoadedQnt { get; set; }

        public List<FactoryOrderColliDetail> FactoryOrderColliDetails { get; set; }

        public string ApproverName { get; set; }
        public string ApprovedDate { get; set; }
        public bool IsSelected { get; set; }
        public int? OfferSeasonTypeID { get; set; }
        public int? QAQCID { get; set; }
    }
}