using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng.DTO
{
    public class SupportFormData
    {
        public List<QuotationStatusDTO> QuotationStatusDTOs { get; set; }
        public List<DTO.AVTSupportBreakdownCategoryDTO> avtSupportBreakdownCategoryDTOs { get; set; }
        public decimal? Exchange { get; set; }
    }
}
