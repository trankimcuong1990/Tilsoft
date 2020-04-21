using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class PaymentNoteOtherResult
    {
        public int? PaymentNoteOtherID { get; set; }
        public int? PaymentnoteID { get; set; }
        public int? NoteItemID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public decimal? Amount { get; set; }
        public string Remark { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
    }
}
