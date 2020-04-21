using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class ProductionItemType
    {
        public int ConstantEntryID { get; set; }
        public int ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
