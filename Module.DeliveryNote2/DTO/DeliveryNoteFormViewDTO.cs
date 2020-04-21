using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
   public class DeliveryNoteFormViewDTO
    {
        public int? DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string DeliveryNoteDate { get; set; }
        public string FromTeam { get; set; }
        public string ToTeam { get; set; }
        public string SaleOrderNo { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ViewName { get; set; }
        public string WorkCenterNM { get; set; }
        public string WorkOrderIDs { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? ClientID { get; set; }
        public int? FactoryID { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string UpdatorName { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }

        public List<DTO.DeliveryNoteDetailFormViewDTO> deliveryNoteDetailFormViewDTOs { get; set; }
    }
}
