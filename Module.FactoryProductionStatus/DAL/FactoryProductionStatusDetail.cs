//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryProductionStatus.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryProductionStatusDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactoryProductionStatusDetail()
        {
            this.FactoryProductionStatusOrderDetail = new HashSet<FactoryProductionStatusOrderDetail>();
        }
    
        public int FactoryProductionStatusDetailID { get; set; }
        public Nullable<int> FactoryProductionStatusID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<decimal> ProducedContainerQnt { get; set; }
        public string Remark { get; set; }
    
        public virtual FactoryProductionStatus FactoryProductionStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactoryProductionStatusOrderDetail> FactoryProductionStatusOrderDetail { get; set; }
    }
}
