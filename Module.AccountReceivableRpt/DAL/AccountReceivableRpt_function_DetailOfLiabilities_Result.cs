//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AccountReceivableRpt.DAL
{
    using System;
    
    public partial class AccountReceivableRpt_function_DetailOfLiabilities_Result
    {
        public Nullable<long> KeyID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public Nullable<int> FactorySaleInvoiceID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> ToTalAmount { get; set; }
        public Nullable<System.DateTime> LoadingDate { get; set; }
        public Nullable<decimal> BeginningBalance { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public string Currency { get; set; }
        public string KeyRawMaterialNM { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public Nullable<int> DueDay { get; set; }
        public int DueColorID { get; set; }
        public string DueColorUD { get; set; }
        public string DueColorNM { get; set; }
        public Nullable<int> ToDue { get; set; }
        public string DueColorDate { get; set; }
    }
}
