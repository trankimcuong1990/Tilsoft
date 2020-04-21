using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ReceivingNoteDTO
    {
        public int? ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public string ReceivingNoteDate { get; set; }
        public string WorkOrderIDs { get; set; }
        public int? WorkCenterID { get; set; }
        public string PurchasingOrderNo { get; set; }
        public int? FromProductionTeamID { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ViewName { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public  int? PurchaseOrderID { get; set; }
        //warehouse field
        public string RelatedDocumentNo { get; set; }
        public int? ReceivingNoteTypeID { get; set; }
        
        public string UpdatorName { get; set; }
        public decimal? TotalQntWithPurchaseOrder { get; set; }
        public string PurchaseOrderUD { get; set; }

        public bool? IsConfirm { get; set; }
        public int? ConfirmBy { get; set; }
        public string ConfirmDate { get; set; }
        public string ConfirmedName { get; set; }
        public int? ResetBy { get; set; }
        public string ResetDate { get; set; }
        public string ReseterName { get; set; }

        public List<ReceivingNoteDetailDTO> ReceivingNoteDetailDTOs { get; set; }

        public int? StatusTypeID { get; set; }
        public string StatusTypeNM { get; set; }

        public string WorkCenterNM { get; set; }

        public string FromProductionTeamNM { get; set; }
        public int? BranchID { get; set; }

        public decimal? FrameWeight { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsHasFrameWeight { get; set; }
        public string BaseUrl { get; set; }
        public int? TransportationServiceID { get; set; }
        public string TransportationServiceNM { get; set; }

        public int? FactorySaleOrderID { get; set; }
        public string FactorySaleOrderUD { get; set; }
        public int? ParentID { get; set; }
        public int? SemiReceiptID { get; set; }
        public string SemiReceiptUD { get; set; }
    }
}

