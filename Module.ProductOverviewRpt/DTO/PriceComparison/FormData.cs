using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductOverviewRpt.DTO.PriceComparison
{
    public class FormData
    {
        public ProductOptionDetailDTO Data { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.QuotationStatus> QuotationStatuses { get; set; }
    }
}
