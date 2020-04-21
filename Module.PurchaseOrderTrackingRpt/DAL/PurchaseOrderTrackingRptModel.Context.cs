﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseOrderTrackingRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PurchaseOrderTrackingRptEntities : DbContext
    {
        public PurchaseOrderTrackingRptEntities()
            : base("name=PurchaseOrderTrackingRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PurchaseOrderTrackingRpt_SupportProductionItemGroup_View> PurchaseOrderTrackingRpt_SupportProductionItemGroup_View { get; set; }
        public virtual DbSet<PurchaseOrderTrackingRpt_SupportSupplier_View> PurchaseOrderTrackingRpt_SupportSupplier_View { get; set; }
        public virtual DbSet<PurchaseOrderTrackingRpt_SupportWorkOrder_View> PurchaseOrderTrackingRpt_SupportWorkOrder_View { get; set; }
        public virtual DbSet<PurchaseOrderTrackingRpt_SupportClient_View> PurchaseOrderTrackingRpt_SupportClient_View { get; set; }
        public virtual DbSet<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View> PurchaseOrderTrackingRpt_PurchaseOrderTracking_View { get; set; }
        public virtual DbSet<PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View> PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View { get; set; }
        public virtual DbSet<PurchaseOrderTrackingRpt_WeekInfo_View> PurchaseOrderTrackingRpt_WeekInfo_View { get; set; }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_SupportClient_View> PurchaseOrderTrackingRpt_function_SupportClient(string searchString)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("searchString", searchString) :
                new ObjectParameter("searchString", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_SupportClient_View>("PurchaseOrderTrackingRpt_function_SupportClient", searchStringParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_SupportClient_View> PurchaseOrderTrackingRpt_function_SupportClient(string searchString, MergeOption mergeOption)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("searchString", searchString) :
                new ObjectParameter("searchString", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_SupportClient_View>("PurchaseOrderTrackingRpt_function_SupportClient", mergeOption, searchStringParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_SupportWorkOrder_View> PurchaseOrderTrackingRpt_function_SupportWorkOrder(string searchString, Nullable<int> clientID, Nullable<int> companyID)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("searchString", searchString) :
                new ObjectParameter("searchString", typeof(string));
    
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("clientID", clientID) :
                new ObjectParameter("clientID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_SupportWorkOrder_View>("PurchaseOrderTrackingRpt_function_SupportWorkOrder", searchStringParameter, clientIDParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_SupportWorkOrder_View> PurchaseOrderTrackingRpt_function_SupportWorkOrder(string searchString, Nullable<int> clientID, Nullable<int> companyID, MergeOption mergeOption)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("searchString", searchString) :
                new ObjectParameter("searchString", typeof(string));
    
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("clientID", clientID) :
                new ObjectParameter("clientID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_SupportWorkOrder_View>("PurchaseOrderTrackingRpt_function_SupportWorkOrder", mergeOption, searchStringParameter, clientIDParameter, companyIDParameter);
        }
    
        public virtual int PurchaseOrderTrackingRpt_function_PurchaseOrderTracking(string startDate, string endDate, Nullable<int> supplierID, Nullable<int> customerID, string piNo, Nullable<int> workOrderID, Nullable<int> itemGroupID, string sortedBy, string sortedDirection)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("customerID", customerID) :
                new ObjectParameter("customerID", typeof(int));
    
            var piNoParameter = piNo != null ?
                new ObjectParameter("piNo", piNo) :
                new ObjectParameter("piNo", typeof(string));
    
            var workOrderIDParameter = workOrderID.HasValue ?
                new ObjectParameter("workOrderID", workOrderID) :
                new ObjectParameter("workOrderID", typeof(int));
    
            var itemGroupIDParameter = itemGroupID.HasValue ?
                new ObjectParameter("itemGroupID", itemGroupID) :
                new ObjectParameter("itemGroupID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("sortedBy", sortedBy) :
                new ObjectParameter("sortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("sortedDirection", sortedDirection) :
                new ObjectParameter("sortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PurchaseOrderTrackingRpt_function_PurchaseOrderTracking", startDateParameter, endDateParameter, supplierIDParameter, customerIDParameter, piNoParameter, workOrderIDParameter, itemGroupIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingActual(string startDate, string endDate)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingActual", startDateParameter, endDateParameter);
        }
    
        public virtual int PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingPlan(string startDate, string endDate)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingPlan", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View> PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult(string fromDate, string toDate, Nullable<int> supplierID, string status, string sortedBy, string sortedDirection)
        {
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("sortedBy", sortedBy) :
                new ObjectParameter("sortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("sortedDirection", sortedDirection) :
                new ObjectParameter("sortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View>("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult", fromDateParameter, toDateParameter, supplierIDParameter, statusParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View> PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult(string fromDate, string toDate, Nullable<int> supplierID, string status, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("sortedBy", sortedBy) :
                new ObjectParameter("sortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("sortedDirection", sortedDirection) :
                new ObjectParameter("sortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View>("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult", mergeOption, fromDateParameter, toDateParameter, supplierIDParameter, statusParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_WeekInfo_View> PurchaseOrderTrackingRpt_function_WeekInfo(string startDate, string endDate)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_WeekInfo_View>("PurchaseOrderTrackingRpt_function_WeekInfo", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_WeekInfo_View> PurchaseOrderTrackingRpt_function_WeekInfo(string startDate, string endDate, MergeOption mergeOption)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_WeekInfo_View>("PurchaseOrderTrackingRpt_function_WeekInfo", mergeOption, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View> PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingDetail(string fromDate, string toDate, Nullable<int> supplierID, string status, string sortedBy, string sortedDirection)
        {
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("sortedBy", sortedBy) :
                new ObjectParameter("sortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("sortedDirection", sortedDirection) :
                new ObjectParameter("sortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View>("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingDetail", fromDateParameter, toDateParameter, supplierIDParameter, statusParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View> PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingDetail(string fromDate, string toDate, Nullable<int> supplierID, string status, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("sortedBy", sortedBy) :
                new ObjectParameter("sortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("sortedDirection", sortedDirection) :
                new ObjectParameter("sortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View>("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingDetail", mergeOption, fromDateParameter, toDateParameter, supplierIDParameter, statusParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}