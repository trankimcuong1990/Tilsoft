using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO.EurofarStock
{
    public class Customer
    {
        public int id { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public List<Attribute> custom_attributes { get; set; }
    }
}
