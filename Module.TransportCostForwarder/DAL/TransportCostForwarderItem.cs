//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TransportCostForwarder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransportCostForwarderItem
    {
        public int TransportCostForwarderItemID { get; set; }
        public int TransportCostForwarderID { get; set; }
        public Nullable<int> TypeOfCost { get; set; }
        public string ContainerNumber { get; set; }
        public Nullable<int> ContainerType { get; set; }
        public string Description { get; set; }
        public Nullable<double> PricePerUnit { get; set; }
        public Nullable<int> NumberOfUnits { get; set; }
        public Nullable<int> TypeOfCurrency { get; set; }
    
        public virtual TransportCostForwarder TransportCostForwarder { get; set; }
    }
}
