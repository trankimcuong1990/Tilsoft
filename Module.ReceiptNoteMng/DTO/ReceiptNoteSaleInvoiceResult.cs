using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteSaleInvoiceResult
    {
        public int ReceiptNoteSaleInvoiceID { get; set; }
        public Nullable<int> ReceiptNoteID { get; set; }
        public Nullable<int> FactorySaleInvoiceID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> AmountByCurrency { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public Nullable<decimal> SaleInvoiceAmount { get; set; }
        public Nullable<decimal> TotalReceipt { get; set; }
        public Nullable<decimal> Remain { get; set; }
    }
}
