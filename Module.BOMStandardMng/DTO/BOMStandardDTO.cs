using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class BOMStandardDTO
    {
        public int? BOMStandardID { get; set; }
        public int? ProductionProcessID { get; set; }        
        public int? ProductionItemID { get; set; }
        public int? ParentBOMStandardID { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public string Description { get; set; }
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
        public int? UnitID { get; set; }
        public decimal? QtyByUnit { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleArticleDescription { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public string UnitNM { get; set; }        
        public decimal? ConversionFactor { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string WorkCenterNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
        public int? OPSequenceID { get; set; }
        public string OPSequenceNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? ModelID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public decimal? IssuedQnt { get; set; }
        public decimal? Price { get; set; }
        public bool? CheckBreakdown { get; set; }

        public List<BOMStandardDTO> BOMStandardDTOs { get; set; }
        public List<ProductionItemUnitDTO> ProductionItemUnitDTOs { get; set; }

        public BOMStandardDTO()
        {
            BOMStandardDTOs = new List<BOMStandardDTO>();
            ProductionItemUnitDTOs = new List<ProductionItemUnitDTO>();
        }
    }
}
