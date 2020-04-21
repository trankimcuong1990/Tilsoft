//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LoadingPlanMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackingList
    {
        public PackingList()
        {
            this.PackingListDetail = new HashSet<PackingListDetail>();
            this.PackingListSparepartDetail = new HashSet<PackingListSparepartDetail>();
        }
    
        public int PackingListID { get; set; }
        public string PackingListUD { get; set; }
        public Nullable<System.DateTime> PackingListDate { get; set; }
        public string DescriptionOfGoods { get; set; }
        public string ContainerRemark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
    
        public virtual PurchasingInvoice PurchasingInvoice { get; set; }
        public virtual ICollection<PackingListDetail> PackingListDetail { get; set; }
        public virtual ICollection<PackingListSparepartDetail> PackingListSparepartDetail { get; set; }
    }
}
