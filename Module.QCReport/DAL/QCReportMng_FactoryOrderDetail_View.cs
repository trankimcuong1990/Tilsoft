//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QCReport.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QCReportMng_FactoryOrderDetail_View
    {
        public int FactoryOrderDetailID { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
    }
}
