//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QCReportDefaultSettingMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QCReportDefaultSettingMng_QCReportDefaultSetting_View
    {
        public int QCReportDefaultSettingID { get; set; }
        public string QCReportSection { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<bool> IsRetrievableFromModel { get; set; }
        public Nullable<int> ModelConnectorCodeID { get; set; }
    }
}
