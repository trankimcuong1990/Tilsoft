using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class WorkCenterDTO
    {
        public int KeyID { get; set; }
        public int? WorkCenterID { get; set; }
        public string WorkCenterNM { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
