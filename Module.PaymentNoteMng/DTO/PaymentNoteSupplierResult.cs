using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class PaymentNoteSupplierResult
    {
        public int PaymentNoteSupplierID { get; set; }
        public int? PaymentNoteID { get; set; }
        public int? PurchaseOrderID { get; set; }
        public decimal? DepositAmount { get; set; }
        public string Remark { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string PurchaseOrderDate { get; set; }
        public decimal? TotalDepositAmount { get; set; }
        public decimal? TotalPaymentDepositAmount { get; set; }
        public decimal? RemainDepositAmount { get; set; }
        public decimal? TotalPurchaseOrderAmount { get; set; }
        public string SupplierPaymentTermNM { get; set; }
    }
}
