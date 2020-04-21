using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShowroomRpt.DTO
{
    public class ReceivingNote
    {
        public long PrimarykeyID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ToFactoryWarehouseID { get; set; }
        public int? ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
    }
}
