using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class DeliveryNoteDTO
    {
        public int? DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string DeliveryNoteDate { get; set; }
        public int? FromProductionTeamID { get; set; }
        public int? ToProductionTeamID { get; set; }
        public string SaleOrderNo { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ViewName { get; set; }
        public int? WorkCenterID { get; set; }
        public string WorkOrderIDs { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        //warehouse field
        public int? FactoryID { get; set; }
        public int? ClientID { get; set; }
        public string RelatedDocumentNo { get; set; }
        public int? DeliveryNoteTypeID { get; set; }

        public string UpdatorName { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }

        public List<DeliveryNoteDetailDTO> DeliveryNoteDetailDTOs { get; set; }

        public int? SubSupplierID { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public string FactorySaleOrderUD { get; set; }

        public bool? IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ApproverName { get; set; }
        public int? ResetBy { get; set; }
        public string ResetDate { get; set; }
        public string ReseterName { get; set; }

        public int? BranchID { get; set; }
        public int? StatusTypeID { get; set; }
        public string StatusTypeNM { get; set; }

        public string WorkCenterNM { get; set; }
        public string ToProductionTeamNM { get; set; }

        public int? PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }

        public string SubSupplierNM { get; set; }
        public int? TransportationServiceID { get; set; }
        public string TransportationServiceNM { get; set; }

        public int? ParentID { get; set; }
        public int? SemiReceiptID { get; set; }
        public string SemiReceiptUD { get; set; }
    }
}
