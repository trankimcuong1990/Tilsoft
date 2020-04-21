using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DTO
{
    public class FactoryCreditNoteDetail
    {
        public int FactoryCreditNoteDetailID { get; set; }
        public int? FactoryInvoiceID { get; set; }
        public decimal? CreditAmount { get; set; }
        public string Remark { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? InvoiceAmount { get; set; }
    }
}
