using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt.DTO
{
    public class DeliveryNote
    {
        public int DeliveryNoteDetailID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string DeliveryNoteDate { get; set; }
        public decimal? Qty { get; set; }
        public int? ProductionItemID { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public int? DeliveryNoteID { get; set; }
        public string Description { get; set; }
    }
}
