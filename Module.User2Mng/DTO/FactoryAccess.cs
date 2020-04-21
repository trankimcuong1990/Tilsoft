using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng.DTO
{
    public class FactoryAccess
    {
        public int UserFactoryAccessID { get; set; }
        public int FactoryID { get; set; }
        public bool CanAccess { get; set; }
        public bool CanReceiveNotification { get; set; }
        public string FactoryUD { get; set; }
    }
}
