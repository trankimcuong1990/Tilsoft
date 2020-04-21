using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.InventoryRpt.DTO
{
    public class InventoryDTO
    {
        public List<InventoryData> Inventory { get; set; }       
        public List<InventoryDetailData> InventoryDetail { get; set; }

        public InventoryDTO()
        {
            Inventory = new List<InventoryData>();            
            InventoryDetail = new List<InventoryDetailData>();
        }
    }
}
