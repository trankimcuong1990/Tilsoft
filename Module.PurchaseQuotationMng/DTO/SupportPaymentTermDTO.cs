using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class SupportPaymentTermDTO
    {
        public int ConstantEntryID { get; set; }

        public int? PurchasePaymentTermID { get; set; }

        public string PurchasePaymentTermNM { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
