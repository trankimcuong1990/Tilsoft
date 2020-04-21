using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DTO
{
    public class DetailOfLiabilitiesByInvoiceNoDto
    {
        public int? PaymentNoteID { get; set; }
        public string PaymentNoteNo { get; set; }
        public string PostingDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public decimal? AmountByCurrency { get; set; }
        public int PrimaryID { get; set; }
        public string Currency { get; set; }
    }
}
