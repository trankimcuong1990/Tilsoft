using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class BOMStandardViewDTO
    {
        public int BOMStandardID { get; set; }
        public int? ParentBOMStandardID { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal? QtyByUnit { get; set; }
        public string UnitNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string WorkCenterNM { get; set; }
        public string OPSequenceNM { get; set; }
        public decimal? Price { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ProductionItemID { get; set; }

        public List<BOMStandardViewDTO> BOMStandardViewDTOs { get; set; }
    }
}
