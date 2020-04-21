namespace Module.InventoryCostRpt.DTO
{
    using System.Collections.Generic;

    public class InitFormData
    {
        public InitFormData()
        {
            FactoryWarehouses = new List<FactoryWarehouseData>();
            ProductionItemTypes = new List<ProductionItemTypeData>();
        }

        public List<FactoryWarehouseData> FactoryWarehouses { get; set; }
        public List<ProductionItemTypeData> ProductionItemTypes { get; set; }
    }
}
