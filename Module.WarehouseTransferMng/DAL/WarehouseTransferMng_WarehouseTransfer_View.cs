//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WarehouseTransferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseTransferMng_WarehouseTransfer_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WarehouseTransferMng_WarehouseTransfer_View()
        {
            this.WarehouseTransferMng_WarehouseTransferDetail_View = new HashSet<WarehouseTransferMng_WarehouseTransferDetail_View>();
        }
    
        public int WarehouseTransferID { get; set; }
        public string ReceiptNo { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> FromFactoryWarehouseID { get; set; }
        public Nullable<int> ToFactoryWarehouseID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> FromBranchID { get; set; }
        public string FromBranchUD { get; set; }
        public string FromBranchNM { get; set; }
        public Nullable<int> ToBranchID { get; set; }
        public string ToBranchUD { get; set; }
        public string ToBranchNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<bool> IsConfirmReceiving { get; set; }
        public Nullable<bool> IsConfirmDelivering { get; set; }
        public string WarehouseTransferType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseTransferMng_WarehouseTransferDetail_View> WarehouseTransferMng_WarehouseTransferDetail_View { get; set; }
    }
}
