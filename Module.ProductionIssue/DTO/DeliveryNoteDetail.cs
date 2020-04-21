using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue.DTO
{
    public class DeliveryNoteDetail
    {
        public int DeliveryNoteDetailID { get; set; }
        public int? DeliveryNoteID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Qty { get; set; }
        public int? FactoryWarehousePalletID { get; set; }
        public string Description { get; set; }
        public int? FromFactoryWarehouseID { get; set; }
        public int? BOMID { get; set; }

        public List<DTO.HistoryDeliveryNote> HistoryDeliveryNotes { get; set; }
    }
}
