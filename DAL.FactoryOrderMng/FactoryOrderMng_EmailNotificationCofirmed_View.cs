//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryOrderMng_EmailNotificationCofirmed_View
    {
        public int FactoryOrderID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public Nullable<int> NumberOfPiece { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ProductTypeNM { get; set; }
        public Nullable<int> TrackingStatusID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public long PrimaryKey { get; set; }
    }
}