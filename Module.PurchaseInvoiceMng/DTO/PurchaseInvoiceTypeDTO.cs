using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class PurchaseInvoiceTypeDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> PurchaseInvoiceTypeID { get; set; }
        public string PurchaseInvoiceTypeNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
