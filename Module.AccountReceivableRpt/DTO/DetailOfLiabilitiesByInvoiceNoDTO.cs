using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountReceivableRpt.DTO
{
    public class DetailOfLiabilitiesByInvoiceNoDto
    {
        public int? ReceiptNoteID { get; set; }
        public string ReceiptNoteNo { get; set; }
        public string PostingDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public decimal? AmountByCurrency { get; set; }
        public int PrimaryID { get; set; }
    }
}
