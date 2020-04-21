using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class PackagingType
    {
        public int? ConstantEntryID { get; set; }
        public int? PackagingTypeID { get; set; }
        public string PackagingTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
