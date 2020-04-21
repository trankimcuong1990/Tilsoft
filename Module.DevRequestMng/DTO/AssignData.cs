using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DTO
{
    public class AssignData
    {
        public int AssignedTo { get; set; }
        public bool IsPersonInCharge { get; set; }
        public string Comment { get; set; }
    }
}
