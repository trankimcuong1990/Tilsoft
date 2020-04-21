using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng.DTO
{
    public class ProductionPriceSearchDTO
    {
        public int? ProductionPriceID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public bool? IsLocked { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? CompanyID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public List<DTO.ProductionPriceDetailSearchDTO> ProductionPriceDetailSearchDTOs { get; set; }
    }
}
