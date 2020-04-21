using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DTO
{
    public class FactoryInvoiceSearchResult
    {
        public int? FactoryInvoiceID { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public decimal? SubTotalAmount { get; set; }
        public decimal? DeductedAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public int? SupplierID { get; set; }
        public string FileLocation { get; set; }
    }
}
