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
    
    public partial class QCReportImage
    {
        public int QCReportImageID { get; set; }
        public Nullable<int> QCReportID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ImageTitle { get; set; }
    
        public virtual QCReport QCReport { get; set; }
    }
}
