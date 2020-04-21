using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockOverviewRpt.DTO
{
    public class ProductType
    {
        public int ConstantEntryID { get; set; }
        public int? ProductTypeID { get; set; }
        public string ProductTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
