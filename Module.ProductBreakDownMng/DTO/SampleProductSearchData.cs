using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class SampleProductSearchData
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }

        public int? ModelID { get; set; }

        public string ModelUD { get; set; }

        public string ModelNM { get; set; }

        public int? FactoryID { get; set; }

        public string FactoryUD { get; set; }

        public string FactoryName { get; set; }
    }
}
