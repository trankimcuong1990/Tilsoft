//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductOverviewRpt_Model_View
    {
        public ProductOverviewRpt_Model_View()
        {
            this.ProductOverviewRpt_PriceOverview_View = new HashSet<ProductOverviewRpt_PriceOverview_View>();
        }
    
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductTypeNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    
        public virtual ICollection<ProductOverviewRpt_PriceOverview_View> ProductOverviewRpt_PriceOverview_View { get; set; }
    }
}
