//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientOfferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class POL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POL()
        {
            this.ClientCostItem = new HashSet<ClientCostItem>();
        }
    
        public int PoLID { get; set; }
        public string PoLUD { get; set; }
        public string PoLname { get; set; }
        public Nullable<bool> IsIncludedInTransportOffer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientCostItem> ClientCostItem { get; set; }
    }
}