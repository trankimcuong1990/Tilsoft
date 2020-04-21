using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class ProductionItemSubUnitDTO
    {
        public ProductionItemSubUnitDTO()
        {
            productionItemUnitHistorys = new List<ProductionItemUnitHistoryDTO>();
        }
        public int ProductionItemUnitID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string Remark { get; set; }
        public string UnitUD { get; set; }
        public string UnitNM { get; set; }
        public string ValidFrom { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string Creator { get; set; }

        public List<DTO.ProductionItemUnitHistoryDTO> productionItemUnitHistorys { get; set; }
    }
}
