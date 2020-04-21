namespace Module.ShowroomTransferMng.DTO
{
    public class ShowroomTransferDetailDTO
    {
        public int ShowroomTransferDetailID { get; set; }
        public int? ShowroomTransferID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Quantity { get; set; }
        public int? QntBarcode { get; set; }
        public int? FactoryWarehousePalletID { get; set; }
        public int? FactoryWarehouseToPalletID { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string Remark { get; set; }
    }
}
