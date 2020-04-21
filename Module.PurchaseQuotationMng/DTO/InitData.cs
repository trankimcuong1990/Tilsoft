using System.Collections.Generic;

namespace Module.PurchaseQuotationMng.DTO
{
    public class InitData
    {
        public List<MaterialGroup> MaterialGroups { get; set; }
        public List<SubSupplier> Suppliers { get; set; }

        public InitData()
        {
            MaterialGroups = new List<MaterialGroup>();
            Suppliers = new List<SubSupplier>();
        }
    }
}
