using System.Collections.Generic;

namespace Module.ShowroomTransferMng.DTO
{
    public class ShowroomTransferDTO
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

        public List<ShowroomTransferDetailDTO> ShowroomTransferDetails { get; set; }

        public ShowroomTransferDTO()
        {
            ShowroomTransferDetails = new List<ShowroomTransferDetailDTO>();
        }
    }
}
