//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CostInvoice2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostInvoice2Creditor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CostInvoice2Creditor()
        {
            this.CostInvoice2 = new HashSet<CostInvoice2>();
        }
    
        public int CostInvoice2CreditorID { get; set; }
        public string CostInvoice2CreditorUD { get; set; }
        public string CostInvoice2CreditorNM { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostInvoice2> CostInvoice2 { get; set; }
    }
}
