using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class PaymentNoteSearchResult
    {
        public int? PaymentNoteID { get; set; }
        public int? PaymentNoteTypeID { get; set; }
        public string PaymentNoteNo { get; set; }
        public string PaymentNoteDate { get; set; }
        public string PostingDate { get; set; }
        public string Currency { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public int? PaymentTypeID { get; set; }
        public string BankAcc { get; set; }
        public string ReceiverName { get; set; }
        public string AttachedFile { get; set; }
        public int? StatusID { get; set; }
        public string Remark { get; set; }
        public int? CreateBy { get; set; }
        public string CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string StatusNM { get; set; }
        public string Creator { get; set; }
        public string Updater { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string PaymentNoteTypeNM { get; set; }
    }
}
