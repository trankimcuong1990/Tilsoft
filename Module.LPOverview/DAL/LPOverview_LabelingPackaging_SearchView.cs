//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LPOverview.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LPOverview_LabelingPackaging_SearchView
    {
        public int LabelingPackagingID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ClientOrderNumber { get; set; }
        public string SaleNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string LPStatusNM { get; set; }
        public string VNRepName { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string NameApproval { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> DateDifff { get; set; }
    }
}
