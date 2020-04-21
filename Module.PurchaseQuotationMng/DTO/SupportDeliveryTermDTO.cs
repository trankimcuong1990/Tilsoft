using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class SupportDeliveryTermDTO
    {
        public int ConstantEntryID { get; set; }

        public int? PurchaseDeliveryTermID { get; set; }

        public string PurchaseDeliveryTermNM { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
