﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.NormOfProduction.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NormOfProductionEntities : DbContext
    {
        public NormOfProductionEntities()
            : base("name=NormOfProductionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NormOfProductionMng_BOM_View> NormOfProductionMng_BOM_View { get; set; }
        public virtual DbSet<NormOfProductionMng_BOMDetail_View> NormOfProductionMng_BOMDetail_View { get; set; }
        public virtual DbSet<NormOfProductionMng_WorkOrder_View> NormOfProductionMng_WorkOrder_View { get; set; }
    
        public virtual ObjectResult<NormOfProductionMng_WorkOrder_View> NormOfProductionMng_function_SearchWorkOrder(Nullable<int> workCenterID, Nullable<int> workOrderStatusID, string clientUD, string clientNM, string startFrom, string startTo, string workOrderUD, string sortedBy, string sortedDirection)
        {
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("WorkOrderStatusID", workOrderStatusID) :
                new ObjectParameter("WorkOrderStatusID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var startFromParameter = startFrom != null ?
                new ObjectParameter("StartFrom", startFrom) :
                new ObjectParameter("StartFrom", typeof(string));
    
            var startToParameter = startTo != null ?
                new ObjectParameter("StartTo", startTo) :
                new ObjectParameter("StartTo", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("WorkOrderUD", workOrderUD) :
                new ObjectParameter("WorkOrderUD", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NormOfProductionMng_WorkOrder_View>("NormOfProductionMng_function_SearchWorkOrder", workCenterIDParameter, workOrderStatusIDParameter, clientUDParameter, clientNMParameter, startFromParameter, startToParameter, workOrderUDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<NormOfProductionMng_WorkOrder_View> NormOfProductionMng_function_SearchWorkOrder(Nullable<int> workCenterID, Nullable<int> workOrderStatusID, string clientUD, string clientNM, string startFrom, string startTo, string workOrderUD, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var workCenterIDParameter = workCenterID.HasValue ?
                new ObjectParameter("WorkCenterID", workCenterID) :
                new ObjectParameter("WorkCenterID", typeof(int));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("WorkOrderStatusID", workOrderStatusID) :
                new ObjectParameter("WorkOrderStatusID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var clientNMParameter = clientNM != null ?
                new ObjectParameter("ClientNM", clientNM) :
                new ObjectParameter("ClientNM", typeof(string));
    
            var startFromParameter = startFrom != null ?
                new ObjectParameter("StartFrom", startFrom) :
                new ObjectParameter("StartFrom", typeof(string));
    
            var startToParameter = startTo != null ?
                new ObjectParameter("StartTo", startTo) :
                new ObjectParameter("StartTo", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("WorkOrderUD", workOrderUD) :
                new ObjectParameter("WorkOrderUD", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NormOfProductionMng_WorkOrder_View>("NormOfProductionMng_function_SearchWorkOrder", mergeOption, workCenterIDParameter, workOrderStatusIDParameter, clientUDParameter, clientNMParameter, startFromParameter, startToParameter, workOrderUDParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}