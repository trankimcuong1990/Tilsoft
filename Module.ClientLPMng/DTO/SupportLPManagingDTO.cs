using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng.DTO
{
    public class SupportLPManagingDTO
    {
        public int ConstantEntryID { get; set; }
        public int? LPManagingTeamID { get; set; }
        public string LPManagingTeamNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
