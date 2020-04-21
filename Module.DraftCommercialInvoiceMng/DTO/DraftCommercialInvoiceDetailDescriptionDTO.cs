using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class DraftCommercialInvoiceDetailDescriptionDTO
    {
        public int DraftCommercialInvoiceDetailDescriptionID { get; set; }
        public int? DraftCommercialInvoiceDetailID { get; set; }
        public string Description { get; set; }
        public int? RowIndex { get; set; }
    }
}
