using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceExtDetailOverview
    {
        [Display(Name = "ECommercialInvoiceExtDetailID")]
        public int? ECommercialInvoiceExtDetailID { get; set; }

        [Display(Name = "ECommercialInvoiceID")]
        public int? ECommercialInvoiceID { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "TotalAmount")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "ContainerType")]
        public string ContainerType { get; set; }

        [Display(Name = "SealNo")]
        public string SealNo { get; set; }
    }
}
