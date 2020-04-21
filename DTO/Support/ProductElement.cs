using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ProductElement
    {
        public int ConstantEntryID { get; set; }
        public int? ProductElementID { get; set; }
        public string ProductElementNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}

