//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WorkOrderRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrderRpt_InOutReceipt_View
    {
        public long PrimaryID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> OPSequenceDetailID { get; set; }
        public Nullable<int> ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public Nullable<int> DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public int FromFactoryWarehouseID { get; set; }
        public int FromProductionTeamID { get; set; }
        public int ToFactoryWarehouseID { get; set; }
        public int ToProductionTeamID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public int InOutType { get; set; }
    }
}
