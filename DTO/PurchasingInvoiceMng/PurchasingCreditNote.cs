using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PurchasingInvoiceMng
{
    public class PurchasingCreditNote
    {
        public int PurchasingCreditNoteID { get; set; }
        public int? PurchasingInvoiceID { get; set; }
        public string CreditNoteNo { get; set; }
        public string CreditNoteDate { get; set; }
        public string Remark { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
    }
}
