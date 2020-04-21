namespace Module.ReceivingNote.DTO
{
    public class SetContentDetailDTO
    {
        public int ProductionItemID { get; set; }

        public int? FactoryWarehouseID { get; set; }

        public decimal? StockQnt { get; set; }

        public decimal? TotalReceive { get; set; }
    }
}
