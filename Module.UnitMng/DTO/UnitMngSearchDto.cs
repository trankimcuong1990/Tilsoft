using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UnitMng.DTO
{
   public class UnitMngSearchDto
    {
        public int UnitID { get; set; }
        public string UnitUD { get; set; }
        public string UnitNM { get; set; }
        public Nullable<int> UnitTypeID { get; set; }
    }
}
