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
    
    public partial class ClientLPMng_ClientLPMng_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientLPMng_ClientLPMng_View()
        {
            this.ClientLPMng_ClientLPNotification_View = new HashSet<ClientLPMng_ClientLPNotification_View>();
        }
    
        public int ClientLPID { get; set; }
        public int ClientID { get; set; }
        public int LPManagingTeamID { get; set; }
        public Nullable<bool> IsAutoUpdateSimilarLP { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string LPManagingTeamNM { get; set; }
        public string UpdaterName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientLPMng_ClientLPNotification_View> ClientLPMng_ClientLPNotification_View { get; set; }
    }
}
