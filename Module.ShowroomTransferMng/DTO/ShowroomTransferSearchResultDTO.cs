namespace Module.ShowroomTransferMng.DTO
{
    public class ShowroomTransferSearchResultDTO
    {
        public int ShowroomTransferID { get; set; }
        public string ShowroomTransferUD { get; set; }
        public string ShowroomTransferDate { get; set; }
        public int? FromWarehouseID { get; set; }
        public int? ToWarehouseID { get; set; }
        public string Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string FromWarehouseUD { get; set; }
        public string FromWarehouseNM { get; set; }

        public string ToWarehouseUD { get; set; }
        public string ToWarehouseNM { get; set; }

        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
    }
}
