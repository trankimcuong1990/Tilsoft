using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class QuickSearchSampleProductDTO
    {
        public int OptionID { get; set; }
        public string OptionUD { get; set; }
        public string OptionNM { get; set; }

        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
    }
}
