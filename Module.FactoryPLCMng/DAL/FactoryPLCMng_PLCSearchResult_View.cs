//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryPLCMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryPLCMng_PLCSearchResult_View
    {
        public int PLCID { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string Rating { get; set; }
        public Nullable<int> RatedBy { get; set; }
        public string RatorName1 { get; set; }
        public string RatorName2 { get; set; }
        public Nullable<System.DateTime> RatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName1 { get; set; }
        public string UpdatorName2 { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public string ConfirmerName1 { get; set; }
        public string ConfirmerName2 { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public int TotalLoadingPlanDetail { get; set; }
    }
}
