//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientLPMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientLP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientLP()
        {
            this.ClientLPNotification = new HashSet<ClientLPNotification>();
        }
    
        public int ClientLPID { get; set; }
        public int ClientID { get; set; }
        public int LPManagingTeamID { get; set; }
        public Nullable<bool> IsAutoUpdateSimilarLP { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientLPNotification> ClientLPNotification { get; set; }
    }
}