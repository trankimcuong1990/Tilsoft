//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BOM.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOMMng_SupportWorkOrderOPSequence_View
    {
        public Nullable<int> WorkOrderID { get; set; }
        public int OPSequenceDetailID { get; set; }
        public string OPSequenceDetailNM { get; set; }
        public Nullable<int> SequenceIndexNumber { get; set; }
        public Nullable<int> OPSequenceID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string OPSequenceNM { get; set; }
        public string WorkCenterNM { get; set; }
        public Nullable<decimal> OperatingTime { get; set; }
        public Nullable<decimal> DefaultCost { get; set; }
        public Nullable<int> NextWorkCenterID { get; set; }
        public Nullable<int> DefaultFactoryWarehouseID { get; set; }
        public string DefaultFactoryWarehouseNM { get; set; }
        public Nullable<bool> IsExceptionAtConfirmFrameStatus { get; set; }
        public long PrimaryID { get; set; }
    }
}
