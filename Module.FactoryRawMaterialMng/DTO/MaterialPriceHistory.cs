using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
     public class MaterialPriceHistory
    {
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeUD { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int UnitID { get; set; }
        public int StatusID { get; set; }
        public string UnitNM { get; set; }
        public int MaterialHistoryID { get; set; }
        public int? MaterialsPriceID { get; set; }
        public decimal Price { get; set; }
        public string ValidFrom { get; set; }
        public decimal Qty { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string AttachFileHistory { get; set; }
        public string RemarkHistory { get; set; }
        public string AttachFileURL { get; set; }
        public string AttachFileFriendlyName { get; set; }
    }
}
