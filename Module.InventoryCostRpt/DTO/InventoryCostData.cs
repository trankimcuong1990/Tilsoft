namespace Module.InventoryCostRpt.DTO
{
    public class InventoryCostData
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? TotalBeginning { get; set; }
        public decimal? TotalReceiving { get; set; }
        public decimal? TotalDelivering { get; set; }
        public decimal? TotalEndingInventory { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public long PrimaryID { get; set; }
        public decimal? Price { get; set; }
        public decimal? Value { get; set; }

        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
    }
}
