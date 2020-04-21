using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote.DTO
{
    public class PurchasingCreditNoteExtendDetail
    {
        public int? PurchasingCreditNoteExtendDetailID { get; set; }
        public int? PurchasingCreditNoteID { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
    }
}
