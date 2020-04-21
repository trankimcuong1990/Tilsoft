using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShowroomRpt.DTO
{
    public class SearchDataDto
    {
        public List<DTO.ShowroomDataDto> Data { get; set; }
        public List<DTO.ShowroomDataDto> Data1 { get; set; }
        public decimal? TotalQuantity { get; set; }

        public List<Support.DTO.FactoryWarehouseDto> factory { get; set; }

        public List<ProductionItemWithDescription> ProductionItemWithDescriptions { get; set; }
    }
}
