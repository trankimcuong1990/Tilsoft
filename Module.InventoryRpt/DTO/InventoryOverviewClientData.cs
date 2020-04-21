using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.InventoryRpt.DTO
{
    public class InventoryOverviewClientData
    {
        public int PrimaryID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ProductionItemUD { get; set; }
    }
}
