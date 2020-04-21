//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PLCMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class PLCMng_PLC_View
    {
        public PLCMng_PLC_View()
        {
            this.PLCMng_PLCImage_View = new HashSet<PLCMng_PLCImage_View>();
        }
    
        public int PLCID { get; set; }
        public Nullable<int> BookingID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<int> OfferLineSparepartID { get; set; }
        public string CushionBatchCode { get; set; }
        public string QCReportFile { get; set; }
        public string AssemplyInstructionFile { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public string RatingComment { get; set; }
        public Nullable<int> RatedBy { get; set; }
        public Nullable<System.DateTime> RatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string CachedClientUD { get; set; }
        public string CachedContanierNo { get; set; }
        public string CachedLoadingPlanUD { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string RatorName { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public string CachedProformaInvoiceNo { get; set; }
    
        public virtual ICollection<PLCMng_PLCImage_View> PLCMng_PLCImage_View { get; set; }
    }
}