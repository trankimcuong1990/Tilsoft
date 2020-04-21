using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PurchasingInvoiceMng
{
    public class SearchFormData
    {
        public List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> Data { get; set; }
        public List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSearch> AllData { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalFactoryAmount { get; set; }
        public int? UserOfficeID { get; set; }
    }
}
