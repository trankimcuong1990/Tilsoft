//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QAQCMng.DAL
{
    using System;
    
    public partial class QAQCMng_function_SearchQAQC_Result
    {
        public int QAQCID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusNM { get; set; }
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> SampleQNT { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public Nullable<int> ItemStandardID { get; set; }
        public string ItemStandardNM { get; set; }
        public Nullable<int> InspectionOptionID { get; set; }
        public string InspectionOptionNM { get; set; }
        public Nullable<int> TestSamplingOptionID { get; set; }
        public string TestSamplingOptionNM { get; set; }
        public Nullable<int> ShippedByFactoryID { get; set; }
        public string ShippedByFactoryNM { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int FactoryOrderDetailID { get; set; }
        public string ApprovalNM { get; set; }
        public string ClientNM { get; set; }
        public string LocationNM { get; set; }
        public Nullable<bool> IsRemove { get; set; }
    }
}