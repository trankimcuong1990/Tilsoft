using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionCosting.DTO
{
    public class TotalReceivingNote
    {
        public decimal? TotalQnt { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? WorkOrderID { get; set; }
        public long PrimaryKey { get; set; }
    }
}
