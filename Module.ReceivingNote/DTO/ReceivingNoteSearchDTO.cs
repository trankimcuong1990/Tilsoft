using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ReceivingNoteSearchDTO
    {
        public int? ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public string ReceivingNoteDate { get; set; }
        public string PurchasingOrderNo { get; set; }
        public string WorkCenterNM { get; set; }
        public string FromProductionTeamNM { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ViewName { get; set; }
        public int? ReceivingNoteTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? CreatedBy { get; set; }
        public List<WorkOrderSearchDTO> WorkOrderSearchDTOs { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public int? WorkCenterID { get; set; }
        public int? FromProductionTeamID { get; set; }

        public bool? IsConfirm { get; set; }
        public int? ConfirmBy { get; set; }
        public string ConfirmDate { get; set; }
        public string ConfirmedName { get; set; }
        public string WorkOrderIDs { get; set; }
        public int? StatusTypeID { get; set; }
        public string StatusTypeNM { get; set; }
    }


    public class WorkOrderSearchDTO
    {
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }        
    }

}
