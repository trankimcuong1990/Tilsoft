using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ShowroomAreaByPhysicalQnt
    {
        public object KeyID { get; set; }
        public int? ShowroomItemID { get; set; }
        public int? ShowroomAreaID { get; set; }
        public string ShowroomAreaUD { get; set; }
        public int Quantity { get; set; }
    }
}
