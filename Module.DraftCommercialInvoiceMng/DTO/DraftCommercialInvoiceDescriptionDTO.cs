using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class DraftCommercialInvoiceDescriptionDTO
    {
        public int DraftCommercialInvoiceDescriptionID { get; set; }
        public int? DraftCommercialInvoiceID { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public int? RowIndex { get; set; }
    }
}
