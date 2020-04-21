using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class WarehouseTransferDTO
    {
        public int WarehouseTransferID { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }

        public int? FromFactoryWarehouseID { get; set; }
        public int? ToFactoryWarehouseID { get; set; }
        public int? CompanyID { get; set; }
        public List<WarehouseTransferProductDTO> WarehouseTransferProductDTOs { get; set; }

        public int? FromBranchID { get; set; }

        public string FromBranchUD { get; set; }

        public string FromBranchNM { get; set; }

        public int? ToBranchID { get; set; }

        public string ToBranchUD { get; set; }

        public string ToBranchNM { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public List<WarehouseTransferDetailDTO> WarehouseTransferDetails { get; set; }

        public bool? IsConfirmReceiving { get; set; }

        public bool? IsConfirmDelivering { get; set; }
        public string WarehouseTransferType { get; set; }
    }
}
