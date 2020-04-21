using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class PaymentTermDTO
    {
        public int PaymentTermID { get; set; }
        public string PaymentTermNM { get; set; }
        public int? PaymentTypeID { get; set; }
        public string PaymentTypeNM { get; set; }
    }
}
