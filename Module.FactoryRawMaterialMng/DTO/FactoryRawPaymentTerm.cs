using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawPaymentTerm
    {
        public int FactoryRawMaterialPaymentTermID { get; set; }
        public int FactoryRawMaterialID { get; set; }
        public int? SupplierPaymentTermID { get; set; }
        public string SupplierPaymentTermNM { get; set; }
    }
}
