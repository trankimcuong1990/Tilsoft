//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceivingNote.DAL
{
    using System;
    
    public partial class ReceivingNoteMng_function_TransferNotePreToWork_Result
    {
        public int WorkOrderID { get; set; }
        public Nullable<int> PreOrderWorkOrderID { get; set; }
        public Nullable<int> OPSequenceDetailID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> NormQnt { get; set; }
        public Nullable<int> CountComponent { get; set; }
    }
}
