using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderCostMng.DTO
{
    public class BOMDTO
    {
        public int? BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ParentBOMID { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public string Description { get; set; }
        public int? RevisionNumber { get; set; }
        public bool? IsActive { get; set; }
        public int? CompanyID { get; set; }
        public int? DisplayIndex { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public string DeletedDate { get; set; }
        public decimal? Quantity { get; set; }
        public int? ProductionTeamID { get; set; }
        public decimal? OperatingTime { get; set; }
        public decimal? DefaultCost { get; set; }
        public decimal? WastagePercent { get; set; }
        public int? UnitID { get; set; }
        public decimal? QtyByUnit { get; set; }


        //workorder info
        public string WorkOrderUD { get; set; }
        public string WorkOrderDescription { get; set; }
        public int? WorkOrderQnt { get; set; }
        public int? OPSequenceID { get; set; }
        public string OPSequenceNM { get; set; }
        public string ClientUD { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public decimal? WorkOrderWastagePercent { get; set; }
        public int? ProductID { get; set; }

        //production item info
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string OPSequenceDetailNM { get; set; }
        public string Unit { get; set; }
        public string ProductFullSizeImageUrl { get; set; }
        public string ProductThumbnailImageUrl { get; set; }
        public bool? IsCreatingNewVersionOfBOM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? ProductionItemTypeDisplayIndex { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? WorkCenterID { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public bool? IsExceptionAtConfirmFrameStatus { get; set; }
        public string UnitNM { get; set; }
        public decimal? ConversionFactor { get; set; }

        //ui info
        public bool? IsEditing { get; set; }
        public bool? VisibleCRUD { get; set; }

        public List<BOMDTO> BOMDTOs { get; set; }
    }
}
