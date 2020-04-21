using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO.EurofarStock.DataContract.Post
{
    public class productContractDetailForUpdate
    {
        public string sku { get; set; }
        public int attribute_set_id { get; set; }
        public decimal price { get; set; }
        public int status { get; set; }
        public int visibility { get; set; }
        public string type_id { get; set; }
        public List<productContractCustomAttribute> custom_attributes { get; set; }
    }
}
