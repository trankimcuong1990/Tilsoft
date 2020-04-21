using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DTO
{
    public class ReceivingNoteDetail
    {
        public int ReceivingNoteDetailID { get; set; }
        public int? ReceivingNoteID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Quantity { get; set; }
        public int? ToFactoryWarehouseID { get; set; }
        public int? BOMID { get; set; }
    }
}
