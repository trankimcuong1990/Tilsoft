//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryOrderNorm.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryFinishedProduct
    {
        public FactoryFinishedProduct()
        {
            this.FactoryFinishedProductOrderNorm = new HashSet<FactoryFinishedProductOrderNorm>();
        }
    
        public int FactoryFinishedProductID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
    
        public virtual ICollection<FactoryFinishedProductOrderNorm> FactoryFinishedProductOrderNorm { get; set; }
    }
}
