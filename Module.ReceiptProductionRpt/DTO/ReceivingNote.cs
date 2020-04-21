using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DTO
{
    public class ReceivingNote
    {
        public int ReceivingNoteID { get; set; }
        public int? FromProductionTeamID { get; set; }
        public int? WorkOrderID { get; set; }
        public string ViewName { get; set; }
        public int? OPSequenceDetailID { get; set; }

        public List<ReceivingNoteDetail> ReceivingNoteDetails { get; set; }
    }
}
