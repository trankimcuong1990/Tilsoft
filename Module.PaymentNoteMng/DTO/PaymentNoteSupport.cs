using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class PaymentNoteSupport
    {
        public PaymentNoteSupport()
        {
            paymentSupportTypes = new List<PaymentSupportType>();
            paymentNoteSupportItemTypes = new List<PaymentNoteSupportItemType>();
            paymentNoteSupportTypes = new List<PaymentNoteSupportType>();
        }

        public List<DTO.PaymentNoteSupportItemType> paymentNoteSupportItemTypes { get; set; }
        public List<DTO.PaymentNoteSupportType> paymentNoteSupportTypes { get; set; }
        public List<DTO.PaymentNoteBankAccount> paymentNoteBankAccounts { get; set; }
        public List<DTO.PaymentSupportType> paymentSupportTypes { get; set; }
    }
    public class PaymentNoteSupportSerachPurchaseInvoice
    {
        public int? PurchaseInvoiceID { get; set; }
        public string PurchaseInvoiceUD { get; set; }
        public string InvoiceNo { get; set; }
        public string PurchaseInvoiceDate { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public decimal? TotalPurchaseInvoice { get; set; }
        public decimal? Remain { get; set; }
        public decimal? TotalPayment { get; set; }
        public string Currency { get; set; }
        public decimal? TotalRealDeposit { get; set; }
        public string Remark { get; set; }
    }
    public class PaymentNoteSupportItemType
    {
        public object KeyID { get; set; }
        public int? PaymentNoteItemTypeID { get; set; }
        public string PaymentNoteItemTypeNM { get; set; }
    }

    public class PaymentNoteSupportType
    {
        public object KeyID { get; set; }
        public int? PaymentTypeID { get; set; }
        public string PaymentTypeNM { get; set; }
    }
    public class PaymentNoteBankAccount
    {
        public int SupplierBankID { get; set; }
        public string BankCode { get; set; }
        public string BankNM { get; set; }
        public string AccountNM { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string Infor { get; set; }
    }

    public class PaymentSupportType
    {
        public object keyID { get; set; }
        public int? PaymentNoteTypeID { get; set; }
        public string PaymentNoteTypeNM { get; set; }
    }

    public class PaymentSupportSearchSupplier
    {
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
    }

    public class PaymentSupportStatus
    {
        public int? StatusID { get; set; }
        public string StatusNM { get; set; }
    }

    public class MasterExchangeRatePayment
    {
        public int? MasterExchangeRateID { get; set; }
        public string ValidDate { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
    }

    public class PaymentNoteSupportSearchPO {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> PurchaseOrderStatusID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string RequiredDocuments { get; set; }
        public string PaymentDocuments { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string ETA { get; set; }
        public Nullable<decimal> PaymentByPO { get; set; }
        public Nullable<decimal> TotalPurchaseOrderAmount { get; set; }
        public Nullable<decimal> RemainPurchaseOrderAmount { get; set; }
        public Nullable<decimal> TotalPayDepositAmount { get; set; }
        public Nullable<decimal> TotalDepositAmount { get; set; }
        public Nullable<decimal> RemainDepositAmount { get; set; }
        public string SupplierPaymentTermNM { get; set; }
        public decimal? TotalRealDeposit { get; set; }
    }
}
