//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySpecificationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactorySpecificationMng_OfferLinePI_View
    {
        public long PrimaryID { get; set; }
        public int FactorySpecificationID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
    
        public virtual FactorySpecificationMng_FactorySpecification_Search FactorySpecificationMng_FactorySpecification_Search { get; set; }
    }
}
