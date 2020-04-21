namespace Module.ProductionItem.DTO
{
    public class FactoryWarehouse
    {
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }

        public int? CompanyID { get; set; }

        // Add new properties: BranchID.
        public int? BranchID { get; set; }

        public bool? IsDefault { get; set; }
    }
}
