using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class PaymentNoteEditResult
    {
        public PaymentNoteEditResult()
        {
            paymentNoteSupplierResults = new List<PaymentNoteSupplierResult>();
            paymentNoteOtherResults = new List<PaymentNoteOtherResult>();
            paymentNoteInvoiceResults = new List<PaymentNoteInvoiceResult>();
        }
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
        public string Creator { get; set; }
        public string Updater { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }

        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; }

        public decimal? ExchangeRate { get; set; }
        public decimal? TotalByCurrency { get; set; }

        public decimal? DepositRemain { get; set; }
        public Nullable<int> SupplierBankID { get; set; }

        public List<DTO.PaymentNoteInvoiceResult> paymentNoteInvoiceResults { get; set; }
        public List<DTO.PaymentNoteOtherResult> paymentNoteOtherResults { get; set; }
        public List<DTO.PaymentNoteSupplierResult> paymentNoteSupplierResults { get; set; }
    }
}
