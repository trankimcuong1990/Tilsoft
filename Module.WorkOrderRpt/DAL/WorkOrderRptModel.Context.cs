﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WorkOrderRptEntities : DbContext
    {
        public WorkOrderRptEntities()
            : base("name=WorkOrderRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<WorkOrderRpt_FactoryWarehouseBySequenceDetail_View> WorkOrderRpt_FactoryWarehouseBySequenceDetail_View { get; set; }
        public virtual DbSet<WorkOrderRpt_InOutDetail_View> WorkOrderRpt_InOutDetail_View { get; set; }
        public virtual DbSet<WorkOrderRpt_InOutReceipt_View> WorkOrderRpt_InOutReceipt_View { get; set; }
        public virtual DbSet<WorkOrderRpt_OPSequenceDetail_View> WorkOrderRpt_OPSequenceDetail_View { get; set; }
        public virtual DbSet<WorkOrderRpt_ProductionTeamBySequenceDetail_View> WorkOrderRpt_ProductionTeamBySequenceDetail_View { get; set; }
        public virtual DbSet<WorkOrderRpt_WorkOrder_View> WorkOrderRpt_WorkOrder_View { get; set; }
        public virtual DbSet<WorkOrderRpt_ReceiptOverview_View> WorkOrderRpt_ReceiptOverview_View { get; set; }
        public virtual DbSet<WorkOrderRpt_ReceiptOverviewDetail_View> WorkOrderRpt_ReceiptOverviewDetail_View { get; set; }
    }
}
