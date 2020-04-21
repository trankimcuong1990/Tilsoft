using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class ClientPaymentDetail
    {
        public int ClientPaymentDetailID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public string PaidDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DeductedAmount { get; set; }
        public string Remark { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string SaleOrderDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? ToBePaidAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public bool IsDone { get; set; }
        public List<ClientPaymentDeduction> ClientPaymentDeductions { get; set; }
    }
}
