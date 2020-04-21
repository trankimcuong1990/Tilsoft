namespace Module.OutsourceRpt.DTO
{
    public class OutsourceProductionItemDTO
    {
        public int WorkOrderID { get; set; }
        public int ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public decimal? NormQnt { get; set; }
        public decimal? ReceivingQnt { get; set; }
        public decimal? DeliveringQnt { get; set; }
    }
}
