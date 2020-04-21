﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ModuleStatusMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ModuleStatusMngEntities : DbContext
    {
        public ModuleStatusMngEntities()
            : base("name=ModuleStatusMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ModuleStatusMng_Module_View> ModuleStatusMng_Module_View { get; set; }
        public virtual DbSet<ModuleStatusMng_ModuleStatus_View> ModuleStatusMng_ModuleStatus_View { get; set; }
        public virtual DbSet<ModuleStatusMng_ModuleStatusSearch_View> ModuleStatusMng_ModuleStatusSearch_View { get; set; }
        public virtual DbSet<ModuleStatus> ModuleStatus { get; set; }
    
        public virtual ObjectResult<ModuleStatusMng_ModuleStatusSearch_View> ModuleStatusMng_function_SearchModuleStatus(string statusUD, Nullable<int> moduleID, string statusNM, Nullable<bool> isActived, string sortedBy, string sortedDirection)
        {
            var statusUDParameter = statusUD != null ?
                new ObjectParameter("StatusUD", statusUD) :
                new ObjectParameter("StatusUD", typeof(string));
    
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var statusNMParameter = statusNM != null ?
                new ObjectParameter("StatusNM", statusNM) :
                new ObjectParameter("StatusNM", typeof(string));
    
            var isActivedParameter = isActived.HasValue ?
                new ObjectParameter("IsActived", isActived) :
                new ObjectParameter("IsActived", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ModuleStatusMng_ModuleStatusSearch_View>("ModuleStatusMng_function_SearchModuleStatus", statusUDParameter, moduleIDParameter, statusNMParameter, isActivedParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ModuleStatusMng_ModuleStatusSearch_View> ModuleStatusMng_function_SearchModuleStatus(string statusUD, Nullable<int> moduleID, string statusNM, Nullable<bool> isActived, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var statusUDParameter = statusUD != null ?
                new ObjectParameter("StatusUD", statusUD) :
                new ObjectParameter("StatusUD", typeof(string));
    
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var statusNMParameter = statusNM != null ?
                new ObjectParameter("StatusNM", statusNM) :
                new ObjectParameter("StatusNM", typeof(string));
    
            var isActivedParameter = isActived.HasValue ?
                new ObjectParameter("IsActived", isActived) :
                new ObjectParameter("IsActived", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ModuleStatusMng_ModuleStatusSearch_View>("ModuleStatusMng_function_SearchModuleStatus", mergeOption, statusUDParameter, moduleIDParameter, statusNMParameter, isActivedParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
