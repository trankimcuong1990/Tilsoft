//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ModelMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModelPiece
    {
        public int ModelPieceID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> PieceModelID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> RowIndex { get; set; }
    
        public virtual Model Model { get; set; }
    }
}
