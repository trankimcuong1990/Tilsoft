using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceDetailDescriptionOverview
    {
        [Display(Name = "ECommercialInvoiceDetailDescriptionID")]
        public int? ECommercialInvoiceDetailDescriptionID { get; set; }

        [Display(Name = "ECommercialInvoiceDetailID")]
        public int? ECommercialInvoiceDetailID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RowIndex")]
        public int? RowIndex { get; set; }
    }
}
