using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteClientResult
    {
        public int? ReceiptNoteClientID { get; set; }
        public int? ReceiptNoteID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public decimal? Amount { get; set; }
        public string Remark { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
    }
}
