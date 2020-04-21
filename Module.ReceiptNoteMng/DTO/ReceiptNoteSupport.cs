using System;
using System.Collections.Generic;

namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteSupport
    {
        public ReceiptNoteSupport()
        {
            receiveSupportTypes = new List<ReceiveSupportType>();
            receiptNoteSupportItemTypes = new List<ReceiptNoteSupportItemType>();
            receiptNoteSupportTypes = new List<ReceiptNoteSupportType>();
        }

        public List<DTO.ReceiptNoteSupportItemType> receiptNoteSupportItemTypes { get; set; }
        public List<DTO.ReceiptNoteSupportType> receiptNoteSupportTypes { get; set; }
        public List<DTO.ReceiveSupportType> receiveSupportTypes { get; set; }
        public List<DTO.ReceiptNoteBankAccount> receiptNoteBankAccounts { get; set; }
    }
    public class ReceiptNoteBankAccount
    {
        public int SupplierBankID { get; set; }
        public string BankNM { get; set; }
        public string AccountNM { get; set; }
        public string BankCode { get; set; }
        public string Remark { get; set; }
        public int? SupplierID { get; set; }
        public string BankInfor { get; set; }
    }

    public class ReceiptNoteSupportSerachPurchasingInvoice
    {
        public int? PurchasingInvoiceID { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }      
        public decimal? TotalAmount { get; set; }
        public decimal? TotalReceipt { get; set; }
        public decimal? InvoiceAmount { get; set; }     
        public decimal? RemainQnt { get; set; }
       
    }
    public class ReceiptNoteSupportItemType
    {
        public object KeyID { get; set; }
        public int? ReceiptNoteItemTypeID { get; set; }
        public string ReceiptNoteItemTypeNM { get; set; }
    }

    public class ReceiptNoteSupportType
    {
        public object KeyID { get; set; }
        public int? ReceiptNoteTypeID { get; set; }
        public string ReceiptNoteTypeNM { get; set; }
    }

    public class ReceiveSupportType
    {
        public object keyID { get; set; }
        public int? ReceiveTypeID { get; set; }
        public string ReceiveTypeNM { get; set; }
    }

    public class ReceiptSupportSearchClient
    {
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientShortNM { get; set; }
    }

    public class ReceiptSupportStatus
    {
        public int? StatusID { get; set; }
        public string StatusNM { get; set; }
    }

    public class MasterExchangeRateReceipt
    {
        public int? MasterExchangeRateID { get; set; }
        public string ValidDate { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
    public class ReceiptNoteSupportSearchFactorySaleInvoice
    {
        public int FactorySaleInvoiceID { get; set; }
        public string DocCode { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalReceipt { get; set; }
        public Nullable<decimal> RemainQnt { get; set; }

    }
}
