//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TransportCICharge.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransportCICharge_TransportCIChargeDetail_View
    {
        public int TransportCIChargeDetailID { get; set; }
        public Nullable<int> ChargeTypeID { get; set; }
        public string ContainerNo { get; set; }
        public Nullable<int> ContainerTypeID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public Nullable<decimal> NumberOfUnits { get; set; }
        public Nullable<int> TransportCIChargeID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> CurrencyTypeID { get; set; }
    
        public virtual TransportCICharge_TransportCICharge_View TransportCICharge_TransportCICharge_View { get; set; }
    }
}
