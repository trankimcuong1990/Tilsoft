//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySalesQuotationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactorySaleQuotationAttachedFile
    {
        public int FactorySaleQuotationAttachedFileID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
    
        public virtual FactorySaleQuotation FactorySaleQuotation { get; set; }
    }
}