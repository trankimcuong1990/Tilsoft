using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class PurchaseInvoiceStatusDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> PurchaseInvoiceStatusID { get; set; }
        public string PurchaseInvoiceStatusNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
