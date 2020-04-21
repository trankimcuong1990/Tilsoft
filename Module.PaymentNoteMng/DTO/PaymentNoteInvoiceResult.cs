using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class PaymentNoteInvoiceResult
    {
        public int PaymentNoteInvoiceID { get; set; }
        public Nullable<int> PaymentNoteID { get; set; }
        public Nullable<int> PurchaseInvoiceID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public string InvoiceNo { get; set; }
        public string PurchaseInvoiceDate { get; set; }
        public string Currency { get; set; }
        public string PurchaseInvoiceUD { get; set; }
        public Nullable<decimal> TotalPurchaseInvoice { get; set; }
        public Nullable<decimal> TotalPayment { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public Nullable<decimal> TotalRealDeposit { get; set; }

        public List<DTO.PaymentNotePODepositResult> paymentNotePODepositResults { get; set; }
    }
}
