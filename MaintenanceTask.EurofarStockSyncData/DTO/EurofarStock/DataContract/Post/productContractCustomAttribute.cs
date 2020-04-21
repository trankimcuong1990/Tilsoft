using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData.DTO.EurofarStock.DataContract.Post
{
    public class productContractCustomAttribute
    {
        public string attribute_code { get; set; }
        public object value { get; set; }
    }
}
