using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DTO
{
    public class PurchaseInvoiceDTO
    {
        public int? PurchaseInvoiceID { get; set; }
        public string PurchaseInvoiceUD { get; set; }
        public string InvoiceNo { get; set; }
        public int PurchaseInvoiceTypeID { get; set; }
        public string PurchaseInvoiceDate { get; set; }
        public int FactoryRawMaterialID { get; set; }
        public int PurchaseInvoiceStatusID { get; set; }
        public string Currency { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string PurchaseInvoiceStatusNM { get; set; }
        public decimal TotalAmount { get; set; }
        public bool isSelected { get; set; }
        public string DueDay { get; set; }
        public int? KeyRawMaterialID { get; set; }
        public string keyRawMaterialNM { get; set; }
        public decimal? TotalPayment { get; set; }
        public decimal? TotalRemain { get; set; }
        public string Remark { get; set; }

        public decimal? PlanPayment { get; set; }
    }
}
