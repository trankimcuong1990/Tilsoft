//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferSeasonMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferSeasonMng_OfferLine_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfferSeasonMng_OfferLine_View()
        {
            this.OfferSeasonMng_SaleOrderDetail_View = new HashSet<OfferSeasonMng_SaleOrderDetail_View>();
        }
    
        public int OfferLineID { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string OfferUD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferSeasonMng_SaleOrderDetail_View> OfferSeasonMng_SaleOrderDetail_View { get; set; }
    }
}