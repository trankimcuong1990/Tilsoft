using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class InitDataDTO
    {
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<DTO.QuickSearchSampleProductDTO> Data { get; set; }
    }
}
