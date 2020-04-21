using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class ItemNotBOMDTO
    {
        public long KeyID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public Nullable<decimal> DeliveryQty { get; set; }
        public Nullable<decimal> ReceivingQty { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string UnitNM { get; set; }
        public string WorkCenterNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
    }
}
