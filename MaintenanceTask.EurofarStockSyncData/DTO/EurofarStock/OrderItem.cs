using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO.EurofarStock
{
    public class OrderItem
    {
        public string sku { get; set; }
        public int qty_ordered { get; set; }
        public decimal price { get; set; }
        public decimal price_incl_tax { get; set; }
        public decimal tax_percent { get; set; }
    }
}
