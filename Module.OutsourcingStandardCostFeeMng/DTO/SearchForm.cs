using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class SearchForm
    {
        public List<DTO.OutsourcingModel> outsourcingModels { get; set; }

        public DTO.SupportDTO support { get; set; }
    }
}
