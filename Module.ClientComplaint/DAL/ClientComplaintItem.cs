//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientComplaint.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientComplaintItem
    {
        public ClientComplaintItem()
        {
            this.ClientComplaintItemImages = new HashSet<ClientComplaintItemImage>();
        }
    
        public int ClientComplaintItemID { get; set; }
        public int ClientComplaintID { get; set; }
        public Nullable<int> LoadingPlanDetailID { get; set; }
        public Nullable<int> QuantityOfComplaint { get; set; }
        public string ComplaintDescription { get; set; }
    
        public virtual ClientComplaint ClientComplaint { get; set; }
        public virtual ICollection<ClientComplaintItemImage> ClientComplaintItemImages { get; set; }
    }
}
