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
    
    public partial class FactoryOrderMng_QCReport_View
    {
        public int QCReportID { get; set; }
        public string QCReportUD { get; set; }
        public string QCReportNM { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string FriendlyName { get; set; }
    }
}