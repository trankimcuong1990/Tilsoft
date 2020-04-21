using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class ProductionItemDto
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public decimal? RequestingQnt { get; set; }
        public List<WorkOrderDto> WorkOrderDTOs { get; set; }
    }
}
