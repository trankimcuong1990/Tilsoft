//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseExportMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseExportMng_WarehouseExport_View
    {
        public WarehouseExportMng_WarehouseExport_View()
        {
            this.WarehouseExportMng_WarehouseExportProductDetail_View = new HashSet<WarehouseExportMng_WarehouseExportProductDetail_View>();
        }
    
        public int WarehouseExportID { get; set; }
        public string ReceiptNo { get; set; }
        public string CMRNo { get; set; }
        public Nullable<System.DateTime> ExportedDate { get; set; }
        public Nullable<int> WarehousePickingListID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ProcessingStatusID { get; set; }
        public Nullable<int> StatusUpdatedBy { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string StatusUpdatorName { get; set; }
        public string WarehousePickingListNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ProcessingStatusNM { get; set; }
    
        public virtual ICollection<WarehouseExportMng_WarehouseExportProductDetail_View> WarehouseExportMng_WarehouseExportProductDetail_View { get; set; }
    }
}
