//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MIFactorySaleRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MIFactorySaleRpt_FactorySale_View
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string FactoryName { get; set; }
        public string ProductSpecificNM { get; set; }
        public Nullable<decimal> CapacityAllWeek { get; set; }
        public Nullable<decimal> EST { get; set; }
        public Nullable<decimal> NumContOrder { get; set; }
        public Nullable<decimal> NumContShipped { get; set; }
        public Nullable<decimal> FactoryProformaInvoiceTotalAmount { get; set; }
        public Nullable<decimal> FactoryProformaInvoiceTotalCont { get; set; }
        public string LocationNM { get; set; }
        public Nullable<decimal> NumContTobeShipped { get; set; }
        public Nullable<decimal> FactoryInvoiceTotalAmount { get; set; }
        public Nullable<decimal> FactoryInvoiceTotalCont { get; set; }
    }
}
