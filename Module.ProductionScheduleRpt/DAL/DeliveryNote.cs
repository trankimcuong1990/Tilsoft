//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionScheduleRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeliveryNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryNote()
        {
            this.DeliveryNoteDetail = new HashSet<DeliveryNoteDetail>();
        }
    
        public int DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public Nullable<System.DateTime> DeliveryNoteDate { get; set; }
        public Nullable<int> FromProductionTeamID { get; set; }
        public Nullable<int> ToProductionTeamID { get; set; }
        public string SaleOrderNo { get; set; }
        public string Description { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ViewName { get; set; }
        public Nullable<int> DeliveryNoteTypeID { get; set; }
        public string RelatedDocumentNo { get; set; }
        public Nullable<int> WarehouseTransferID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string WorkOrderIDs { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> SubSupplierID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryNoteDetail> DeliveryNoteDetail { get; set; }
    }
}