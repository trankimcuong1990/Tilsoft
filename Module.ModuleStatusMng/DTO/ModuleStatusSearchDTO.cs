using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ModuleStatusMng.DTO
{
    public class ModuleStatusSearchDTO
    {
        public int ModuleStatusID { get; set; }
        public int? ModuleID { get; set; }
        public int? StatusID { get; set; }
        public string StatusUD { get; set; }
        public string StatusNM { get; set; }

        public string DisplayText { get; set; }
        public bool? IsActived { get; set; }
    }
}
