using System.Collections.Generic;

namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteEditResult
    {
        public ReceiptNoteEditResult()
        {
            receiptNoteOtherResults = new List<ReceiptNoteOtherResult>();
            receiptNoteInvoiceResults = new List<ReceiptNoteInvoiceResult>();
            receiptNoteClientResults = new List<ReceiptNoteClientResult>();
            receiptNoteSaleInvoiceResults = new List<ReceiptNoteSaleInvoiceResult>();
        }

        public int? ReceiptNoteID { get; set; }
        public int? ReceiptNoteTypeID { get; set; }
        public string ReceiptNoteNo { get; set; }
        public string ReceiptNoteDate { get; set; }
        public string PostingDate { get; set; }
        public string Currency { get; set; }
        public int? SupplierID { get; set; }
        public int? ReceiveTypeID { get; set; }
        public string BankAcc { get; set; }
        public string ReceiverName { get; set; }
        public int? StatusID { get; set; }
        public string AttachedFile { get; set; }
        public string Remark { get; set; }
        public string FileLocation { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public string Creator { get; set; }
        public string Updater { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }

        public string FriendlyName { get; set; }
        public string ThumbnailLocation { get; set; }

        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? TotalByCurrency { get; set; }
        public int? SupplierBankID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public bool? IsCheckEmployeeOther { get; set; }

        //Declare list detail
        public List<DTO.ReceiptNoteClientResult> receiptNoteClientResults { get; set; }
        public List<DTO.ReceiptNoteInvoiceResult> receiptNoteInvoiceResults { get; set; }
        public List<DTO.ReceiptNoteOtherResult> receiptNoteOtherResults { get; set; }
        public List<DTO.ReceiptNoteSaleInvoiceResult> receiptNoteSaleInvoiceResults { get; set; }
    }
}
