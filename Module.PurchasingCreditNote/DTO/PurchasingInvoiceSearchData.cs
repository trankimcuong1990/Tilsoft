using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote.DTO
{
    public class PurchasingInvoiceSearchData
    {
        public List<PurchasingInvoice> Data { get; set; }
        public int? TotalRows { get; set; }
    }
}
