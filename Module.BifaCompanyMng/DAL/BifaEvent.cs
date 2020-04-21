//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BifaCompanyMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BifaEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BifaEvent()
        {
            this.BifaContactHistory = new HashSet<BifaContactHistory>();
            this.BifaEventFile = new HashSet<BifaEventFile>();
            this.BifaEventParticipant = new HashSet<BifaEventParticipant>();
        }
    
        public int BifaEventID { get; set; }
        public string BifaEventUD { get; set; }
        public string BifaEventNM { get; set; }
        public Nullable<int> BifaCompanyID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual BifaCompany BifaCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BifaContactHistory> BifaContactHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BifaEventFile> BifaEventFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BifaEventParticipant> BifaEventParticipant { get; set; }
    }
}
