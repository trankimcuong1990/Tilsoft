using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class POFromInvoiceDTO
    {
        public long keyID { get; set; }
        public Nullable<int> PurchaseInvoiceID { get; set; }
        public Nullable<int> PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string ETA { get; set; }
        public string PurchaseOrderDate { get; set; }
        public Nullable<decimal> PaymentByPO { get; set; }
        public Nullable<decimal> TotalPurchaseOrderAmount { get; set; }
        public Nullable<decimal> RemainPurchaseOrderAmount { get; set; }
        public Nullable<decimal> TotalPayDepositAmount { get; set; }
        public Nullable<decimal> TotalDepositAmount { get; set; }
        public Nullable<decimal> RemainDepositAmount { get; set; }
    }
}
