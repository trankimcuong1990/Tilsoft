//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CashBookReceiptMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashBookPaidBy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashBookPaidBy()
        {
            this.CashBook = new HashSet<CashBook>();
        }
    
        public int CashBookPaidByID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashBook> CashBook { get; set; }
    }
}