using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class DefaultPrice_PurchaseQuotationDTO
    {
        public int PurchaseQuotationID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}
