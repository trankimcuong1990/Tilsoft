using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DTO
{
    public class ReceivingNoteDTO
    {
        public int? ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public string ReceivingNoteDate { get; set; }
        public string PurchasingOrderNo { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public int? FromProductionTeamID { get; set; }
        public int? ToFactoryWarehouseID { get; set; }
        public int? WorkOrderID { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string WorkOrderUD { get; set; }
        public string WorkOrderDescription { get; set; }
        public string OPSequenceDetailNM { get; set; }
        public string OPSequenceNM { get; set; }
        public int? OPSequenceID { get; set; }
        public string ViewName { get; set; }
        public string WorkOrderIDs { get; set; }

        public string RelatedDocumentNo { get; set; }
        public int? ReceivingNoteTypeID { get; set; }
        public List<ReceivingNoteDetailDTO> ReceivingNoteDetailDTOs { get; set; }

        public int? WorkCenterID { get; set; }
    }
}

