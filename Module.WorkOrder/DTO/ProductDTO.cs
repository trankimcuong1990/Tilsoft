using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class ProductDTO
    {
        public int? ProductID { get; set; }
        public int? ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
