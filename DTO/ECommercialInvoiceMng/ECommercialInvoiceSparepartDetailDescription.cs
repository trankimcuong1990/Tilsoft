using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
   public class ECommercialInvoiceSparepartDetailDescription
    {
        [Display(Name = "ECommercialInvoiceSparepartDetailDescriptionID")]
        public int? ECommercialInvoiceSparepartDetailDescriptionID { get; set; }

        [Display(Name = "ECommercialInvoiceDetailID")]
        public int? ECommercialInvoiceSparepartDetailID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RowIndex")]
        public int? RowIndex { get; set; }
    }
}
