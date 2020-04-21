//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackSaleOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferLineSparepart
    {
        public OfferLineSparepart()
        {
            this.SaleOrderDetailSparepart = new HashSet<SaleOrderDetailSparepart>();
        }
    
        public int OfferLineSparePartID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> PartID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> CushionID { get; set; }
    
        public virtual Offer Offer { get; set; }
        public virtual ICollection<SaleOrderDetailSparepart> SaleOrderDetailSparepart { get; set; }
    }
}