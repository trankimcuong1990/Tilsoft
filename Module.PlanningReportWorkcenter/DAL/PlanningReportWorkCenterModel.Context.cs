﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PlanningReportWorkcenter.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PlanningReportWorkCenterEntities : DbContext
    {
        public PlanningReportWorkCenterEntities()
            : base("name=PlanningReportWorkCenterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<WeekInfo> WeekInfo { get; set; }
        public virtual DbSet<WorkCenter> WorkCenter { get; set; }
        public virtual DbSet<PlanningReport_WorkOrder_View> PlanningReport_WorkOrder_View { get; set; }
        public virtual DbSet<PlanningReportWorkCenter_ReceivingDetail_View> PlanningReportWorkCenter_ReceivingDetail_View { get; set; }
        public virtual DbSet<PlanningReportWorkCenter_SetDetail_View> PlanningReportWorkCenter_SetDetail_View { get; set; }
        public virtual DbSet<PlanningReportWorkCenter_GetWorkCenterStatus_View> PlanningReportWorkCenter_GetWorkCenterStatus_View { get; set; }
        public virtual DbSet<PlanningReportWorkCenter_GetMaterialStatus_View> PlanningReportWorkCenter_GetMaterialStatus_View { get; set; }
    
        public virtual ObjectResult<PlanningReport_WorkOrder_View> PlanningReportWorkCenter_function_SearchWorkOrder(string startDateFrom, string startDateTo, string finishedDateFrom, string finishedDateTo, string workOrderUD, string clientUD, string proformaInvoiceNo, string workOrderStatus, Nullable<int> workCenterID, Nullable<int> companyID)
        {
            var startDateFromParameter = startDateFrom != null ?
                new ObjectParameter("StartDateFrom", startDateFrom) :
                new ObjectParameter("StartDateFrom", typeof(string));
    
            var startDateToParameter = startDateTo != null ?
                new ObjectParameter("StartDateTo", startDateTo) :
                new ObjectParameter("StartDateTo", typeof(string));
    
            var finishedDateFromParameter = finishedDateFrom != null ?
                new ObjectParameter("FinishedDateFrom", finishedDateFrom) :
                new ObjectParameter("FinishedDateFrom", typeof(string));
    
            var finishedDateToParameter = finishedDateTo != null ?
                new ObjectParameter("FinishedDateTo", finishedDateTo) :
                new ObjectParameter("FinishedDateTo", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("WorkOrderUD", workOrderUD) :
                new ObjectParameter("WorkOrderUD", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var workOrderStatusParameter = workOrderStatus != null ?
                new ObjectParameter("WorkOrderStatus", workOrderStatus) :
                new ObjectParameter("WorkOrderStatus", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PlanningReport_WorkOrder_View>("PlanningReportWorkCenter_function_SearchWorkOrder", startDateFromParameter, startDateToParameter, finishedDateFromParameter, finishedDateToParameter, workOrderUDParameter, clientUDParameter, proformaInvoiceNoParameter, workOrderStatusParameter, workCenterIDParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<PlanningReport_WorkOrder_View> PlanningReportWorkCenter_function_SearchWorkOrder(string startDateFrom, string startDateTo, string finishedDateFrom, string finishedDateTo, string workOrderUD, string clientUD, string proformaInvoiceNo, string workOrderStatus, Nullable<int> workCenterID, Nullable<int> companyID, MergeOption mergeOption)
        {
            var startDateFromParameter = startDateFrom != null ?
                new ObjectParameter("StartDateFrom", startDateFrom) :
                new ObjectParameter("StartDateFrom", typeof(string));
    
            var startDateToParameter = startDateTo != null ?
                new ObjectParameter("StartDateTo", startDateTo) :
                new ObjectParameter("StartDateTo", typeof(string));
    
            var finishedDateFromParameter = finishedDateFrom != null ?
                new ObjectParameter("FinishedDateFrom", finishedDateFrom) :
                new ObjectParameter("FinishedDateFrom", typeof(string));
    
            var finishedDateToParameter = finishedDateTo != null ?
                new ObjectParameter("FinishedDateTo", finishedDateTo) :
                new ObjectParameter("FinishedDateTo", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("WorkOrderUD", workOrderUD) :
                new ObjectParameter("WorkOrderUD", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var workOrderStatusParameter = workOrderStatus != null ?
                new ObjectParameter("WorkOrderStatus", workOrderStatus) :
                new ObjectParameter("WorkOrderStatus", typeof(string));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PlanningReport_WorkOrder_View>("PlanningReportWorkCenter_function_SearchWorkOrder", mergeOption, startDateFromParameter, startDateToParameter, finishedDateFromParameter, finishedDateToParameter, workOrderUDParameter, clientUDParameter, proformaInvoiceNoParameter, workOrderStatusParameter, workCenterIDParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> PlanningReportWorkCenter_function_GetWorkCenterStatus(Nullable<int> workOrderID, Nullable<int> workCenterID, Nullable<int> companyID)
        {
            var workOrderIDParameter = workOrderID.HasValue ?
                new ObjectParameter("WorkOrderID", workOrderID) :
                new ObjectParameter("WorkOrderID", typeof(int));
    
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PlanningReportWorkCenter_function_GetWorkCenterStatus", workOrderIDParameter, workCenterIDParameter, companyIDParameter);
        }
    }
}
