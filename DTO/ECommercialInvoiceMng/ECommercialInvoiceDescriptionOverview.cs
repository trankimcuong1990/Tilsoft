using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
      public class ECommercialInvoiceDescriptionOverview
    {
        [Display(Name = "ECommercialInvoiceDescriptionID")]
        public int? ECommercialInvoiceDescriptionID { get; set; }

        [Display(Name = "ECommercialInvoiceID")]
        public int? ECommercialInvoiceID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "RowIndex")]
        public int? RowIndex { get; set; }

        [Display(Name = "TotalAmount")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "CostType")]
        public string CostType { get; set; }

        [Display(Name = "CostTypeNM")]
        public string CostTypeNM { get; set; }
    }
}
