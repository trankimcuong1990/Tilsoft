//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ProductMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMng_ProductColliPiece_View
    {
        public int ProductColliPieceID { get; set; }
        public Nullable<int> ProductColliID { get; set; }
        public Nullable<int> PieceID { get; set; }
        public Nullable<int> Colli { get; set; }
        public Nullable<int> Pcs { get; set; }
        public string PiceDescription { get; set; }
        public string ColliPieceDescription { get; set; }
    
        public virtual ProductMng_ProductColli_View ProductMng_ProductColli_View { get; set; }
    }
}
