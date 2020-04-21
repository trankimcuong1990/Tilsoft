using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DTO
{
    public class ModuleStatusDTO
    {
        public int ModuleStatusID { get; set; }
        public int? ModuleID { get; set; }
        public int? StatusID { get; set; }
        public string StatusUD { get; set; }
        public string StatusNM { get; set; }
        public bool? IsSelected { get; set; }
    }
}
