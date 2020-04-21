using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO.EurofarStock
{
    public class Order
    {        
        public int entity_id { get; set; }
        public string increment_id { get; set; }
        public int customer_id { get; set; }
        public string customer_firstname { get; set; }
        public string customer_lastname { get; set; }
        public string customer_email { get; set; }
        public List<OrderItem> items { get; set; }
    }
}
