namespace Module.InventoryRpt.DTO
{
    public class InventoryDetailData
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }

        public int? DocumentNoteID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? DocumentNoteTypeID { get; set; }

        public string WorkOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string DocumentNoteUD { get; set; }
        public string DocumentNoteDate { get; set; }
        public string DocumentNoteDescription { get; set; }
        public string ViewName { get; set; }

        public int? OrderQnt { get; set; }

        public decimal? ReceivedQnt { get; set; }
        public decimal? DeliveredQnt { get; set; }

        public int? ProductionItemID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int? ProductionTeamID { get; set; }
    }
}
