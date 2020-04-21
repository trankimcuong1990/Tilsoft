using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WEXStockOverviewRpt.DTO
{
    public class SearchFormData
    {
        public List<DTO.ProductSearchResultDTO> Data { get; set; }
        public List<DTO.ProductEcommerceSpecPieceDetailDTO> ProductEcommerceSpecPieceDetailDTOs { get; set; }
    }
}
