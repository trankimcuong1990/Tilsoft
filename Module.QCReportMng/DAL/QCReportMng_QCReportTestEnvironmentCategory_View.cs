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
    
    public partial class QCReportMng_QCReportTestEnvironmentCategory_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QCReportMng_QCReportTestEnvironmentCategory_View()
        {
            this.QCReportMng_QCReportTestEnvironmentItem_View = new HashSet<QCReportMng_QCReportTestEnvironmentItem_View>();
        }
    
        public int QCReportTestEnvironmentCategoryID { get; set; }
        public string Description { get; set; }
        public Nullable<int> RowIndex { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QCReportMng_QCReportTestEnvironmentItem_View> QCReportMng_QCReportTestEnvironmentItem_View { get; set; }
    }
}
