using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class InspectionType
    {
        public int? ConstantEntryID { get; set; }
        public int? InspectionTypeID { get; set; }
        public string InspectionTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
