using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class WarehouseTransferSearchDTO
    {
        public int? WarehouseTransferID { get; set; }
        public string ReceiptNo { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string ReceivingNoteUD { get; set; }
        public string ReceiptDate { get; set; }
        public string FromFactoryWarehouseNM { get; set; }
        public string ToFactoryWarehouseNM { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string Description { get; set; }
        public int FromFactoryWarehouseID { get; set; }
        public int ToFactoryWarehouseID { get; set; }
        public int? DeliveryNoteID { get; set; }
        public int? ReceivingNoteID { get; set; }
        public int? CompanyID { get; set; }
        public string DeliveryName { get; set; }
        public string ReceivingName { get; set; }
        public bool? DeliveryApproved { get; set; }
        public bool? ReceivingConfirm { get; set; }
        public string WarehouseTransferType { get; set; }
        public List<ShowFactoryhouseNM> showFactoryhouseNMs { get; set; }
    }
}
