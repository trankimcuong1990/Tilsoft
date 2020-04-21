using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class MaterialsPrice
    {
        public int MaterialsPriceID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal Price { get; set; }
        public string ValidFrom { get; set; }
        public int UnitID { get; set; }
        public decimal Qty { get; set; }
        public int StatusID { get; set; }
        public string Remark { get; set; }
        public string OldRemark { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemNMTemp { get; set; }
        public string ProductionItemTypeUD { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public bool? IsChange { get; set; }
        public decimal OldPrice { get; set; }
        public string OldValidFrom { get; set; }
        public decimal OldQty { get; set; }
        public int OldStatusID { get; set; }
        public string AttachFile { get; set; }
        public string OldAttachFile { get; set; }
        public string AttachFileURL { get; set; }
        public string AttachFileFriendlyName { get; set; }
        public bool AttachFileHasChange { get; set; }
        public string NewAttachFile { get; set; }
        public List<MaterialPriceHistory> materialPriceHistories { get; set; }
        public List<Unit> Units { get; set; }
    }
}
