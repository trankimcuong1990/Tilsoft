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
    
    public partial class QCReportMng_QCReportDefect_View2
    {
        public int QCReportDefectID { get; set; }
        public Nullable<int> QCReportID { get; set; }
        public string Description { get; set; }
        public Nullable<int> Critical { get; set; }
        public Nullable<decimal> Major { get; set; }
        public Nullable<decimal> Minor { get; set; }
        public string Remark { get; set; }
    
        public virtual QCReportMng_QCReport_View2 QCReportMng_QCReport_View2 { get; set; }
    }
}
