using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DTO
{
    public class ReceivingNoteDetailDTO
    {
        public int? ReceivingNoteDetailID { get; set; }
        public int? ReceivingNoteID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal Quantity { get; set; }
        public int? FactoryWarehousePalletID { get; set; }
        public string Description { get; set; }
        public int? ToFactoryWarehouseID { get; set; }
        public int? BOMID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public decimal? BOMQnt { get; set; }
        public int? QNTBarCode { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
    }
}
