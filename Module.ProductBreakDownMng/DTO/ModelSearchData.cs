using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class ModelSearchData
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }

        public int? FactoryID { get; set; }

        public string FactoryUD { get; set; }

        public string FactoryName { get; set; }
    }
}
