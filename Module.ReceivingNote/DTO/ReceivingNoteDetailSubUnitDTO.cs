using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ReceivingNoteDetailSubUnitDTO
    {
        public int ReceivingNoteDetailSubUnitID { get; set; }
        public int? ReceivingNoteDetailID { get; set; }
        public int? UnitID { get; set; }
        public decimal? Quantity { get; set; }
        public string Remark { get; set; }
        public string UnitNM { get; set; }
    }
}
