//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LoadingPlanMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoadingPlanMng_LoadingPlanSparepartDetail_OverView
    {
        public int LoadingPlanSparepartDetailID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Remark { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public int OfferLineSparePartID { get; set; }
        public Nullable<int> PLCID { get; set; }
        public Nullable<bool> IsCreatedPLC { get; set; }
        public decimal OrderSparepartQnt40HC { get; set; }
    
        public virtual LoadingPlanMng_LoadingPlan_OverView LoadingPlanMng_LoadingPlan_OverView { get; set; }
    }
}
