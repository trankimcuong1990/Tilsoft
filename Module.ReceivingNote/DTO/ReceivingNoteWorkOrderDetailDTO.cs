using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ReceivingNoteWorkOrderDetailDTO
    {
        public int ReceivingNoteWorkOrderDetailID { get; set; }
        public int? ReceivingNoteDetailID { get; set; }
        public int? WorkOrderID { get; set; }
        public decimal? ReceivingQnt { get; set; }
        public string WorkOrderUD { get; set; }
        public string FinishDate { get; set; }
        public decimal? TotalNorm { get; set; }
    }
}
