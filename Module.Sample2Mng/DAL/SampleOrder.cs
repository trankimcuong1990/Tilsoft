//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SampleOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SampleOrder()
        {
            this.SampleProduct = new HashSet<SampleProduct>();
            this.SampleMonitor = new HashSet<SampleMonitor>();
        }
    
        public int SampleOrderID { get; set; }
        public string SampleOrderUD { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<System.DateTime> HardDeadline { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public Nullable<int> TransportTypeID { get; set; }
        public Nullable<int> PurposeID { get; set; }
        public string Remark { get; set; }
        public string NotifyEmail { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> SampleOrderStatusID { get; set; }
        public string Destination { get; set; }
        public Nullable<int> StatusUpdatedBy { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public string ShipmentDetail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleProduct> SampleProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleMonitor> SampleMonitor { get; set; }
    }
}
