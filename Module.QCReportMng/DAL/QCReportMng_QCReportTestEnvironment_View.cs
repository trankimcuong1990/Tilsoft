//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QCReportMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QCReportMng_QCReportTestEnvironment_View
    {
        public int QCReportTestEnvironmentID { get; set; }
        public Nullable<int> QCTestReportID { get; set; }
        public Nullable<int> QCReportTestEnvironmentItemID { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public string QCReportTestEnvironmentItemNM { get; set; }
        public Nullable<int> QCReportTestEnvironmentCategoryID { get; set; }
        public string QCReportTestEnvironmentCategoryNM { get; set; }
    
        public virtual QCReportMng_QCReport_View2 QCReportMng_QCReport_View2 { get; set; }
    }
}
