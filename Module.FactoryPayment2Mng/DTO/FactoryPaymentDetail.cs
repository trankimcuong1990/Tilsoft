using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng.DTO
{
    public class FactoryPaymentDetail
    {
        public int FactoryPayment2DetailID { get; set; }
        public int? FactoryInvoiceID { get; set; }
        public decimal? DPDeductedAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public string Remark { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? RemainAmount { get; set; }
    }
}
