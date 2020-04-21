using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO.EurofarStock
{
    public class Item
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int attribute_set_id { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }
        public string visibility { get; set; }
        public string type_id { get; set; }
        public string weight { get; set; }
        public List<Attribute> custom_attributes { get; set; }
    }
}
