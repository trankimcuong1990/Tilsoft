using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class InitFormData
    {
        public InitFormData()
        {
            SupportItemGroup = new List<SupportItemGroupData>();
            SupportSupplier = new List<SupportSupplierData>();
        }

        public List<SupportItemGroupData> SupportItemGroup { get; set; }

        public List<SupportSupplierData> SupportSupplier { get; set; }
    }
}
