using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardFinanceOverviewData
    {
        public int FactoryPayment2DetailID { get; set; }
        public int? FactoryPayment2ID { get; set; }
        public string ReceiptNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? DPDeductedAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
    }
}
