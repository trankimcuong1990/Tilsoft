//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehousePickingListMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehousePickingListMng_ModelPiece_View
    {
        public long KeyID { get; set; }
        public int WarehousePickingListProductDetailID { get; set; }
        public Nullable<int> ModelPieceID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> PieceModelID { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
    
        public virtual WarehousePickingListMng_WarehousePickingListProductDetail_View WarehousePickingListMng_WarehousePickingListProductDetail_View { get; set; }
    }
}