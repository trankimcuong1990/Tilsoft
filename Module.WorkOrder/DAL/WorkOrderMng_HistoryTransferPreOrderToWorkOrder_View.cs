//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WorkOrder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrderMng_HistoryTransferPreOrderToWorkOrder_View
    {
        public int TransferWorkOrderID { get; set; }
        public string TransferWorkOrderUD { get; set; }
        public Nullable<System.DateTime> TransferWorkOrderDate { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}