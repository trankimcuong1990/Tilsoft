namespace Module.InventoryRpt.DTO
{
    public class FactoryWarehouseDTO
    {
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsDefault { get; set; }
        public int? BranchID { get; set; }
        public bool? IsSelected { get; set; }
    }
}
