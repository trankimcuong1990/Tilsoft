using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardPurchasingInvoiceData
    {
        public int PurchasingInvoiceID { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Season { get; set; }
        public string AVTStatus { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasURLLink { get; set; }
    }
}
