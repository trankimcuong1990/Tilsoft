using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class PaymentTermDTO
    {
        public int FactoryRawMaterialPaymentTermID { get; set; }
        public int FactoryRawMaterialID { get; set; }
        public string SupplierPaymentTermNM { get; set; }
        public int? SupplierPaymentTermID { get; set; }
    }
}
