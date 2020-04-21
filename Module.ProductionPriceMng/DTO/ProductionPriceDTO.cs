using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng.DTO
{
    public class ProductionPriceDTO
    {
        public ProductionPriceDTO()
        {
            ProductionPriceDetailDTOs = new List<ProductionPriceDetailDTO>();
        }
        public int? ProductionPriceID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedBy { get; set; }
        public string LockedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? CompanyID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string LockerName { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public List<ProductionPriceDetailDTO> ProductionPriceDetailDTOs { get; set; }
        public int? UpdateTypeID { get; set; }
    }
}
