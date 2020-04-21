using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.VanPhatMng.DTO
{
    public class DeliveryNoteDetailSearch
    {
        public int? DeliveryNoteID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ComponentMaterialNM { get; set; }
        public string MaterialMaterialNM { get; set; }
        public string ComponentFrameMaterial { get; set; }
        public string MaterialFrameMaterial { get; set; }
        public string UnitNM { get; set; }
        public decimal? ComponentQty { get; set; }
        public decimal? MaterialQty { get; set; }
        public string Description { get; set; }
        public int? PrimaryProduct { get; set; }
    }
}
