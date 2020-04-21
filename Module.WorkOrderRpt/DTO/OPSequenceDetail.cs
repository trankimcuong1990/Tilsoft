using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class OPSequenceDetail
    {
        public int OPSequenceDetailID { get; set; }
        public string OPSequenceDetailNM { get; set; }
        public int SequenceIndexNumber { get; set; }
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
    }
}
