//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DevRequestMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DevRequestMng_DevRequestHistory_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DevRequestMng_DevRequestHistory_View()
        {
            this.DevRequestMng_DevRequestCommentAttachedFile_View = new HashSet<DevRequestMng_DevRequestCommentAttachedFile_View>();
        }
    
        public int DevRequestHistoryID { get; set; }
        public Nullable<int> DevRequestID { get; set; }
        public Nullable<int> DevRequestHistoryActionID { get; set; }
        public string ActionDescription { get; set; }
        public string Comment { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ActionUpdatorName { get; set; }
        public string DevRequestHistoryActionNM { get; set; }
    
        public virtual DevRequestMng_DevRequest_View DevRequestMng_DevRequest_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DevRequestMng_DevRequestCommentAttachedFile_View> DevRequestMng_DevRequestCommentAttachedFile_View { get; set; }
    }
}
