﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CheckListGroupMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CheckListGroupEntities : DbContext
    {
        public CheckListGroupEntities()
            : base("name=CheckListGroupEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CheckListGroup> CheckListGroup { get; set; }
        public virtual DbSet<CheckListMng_CheckListGroup_View> CheckListMng_CheckListGroup_View { get; set; }
    
        public virtual ObjectResult<CheckListMng_CheckListGroup_View> CheckListGroupMng_function_CheckListGroupSearchResult(string checkListGroupUD, string checkListGroupNM, string remark, string sortedBy, string sortedDirection)
        {
            var checkListGroupUDParameter = checkListGroupUD != null ?
                new ObjectParameter("CheckListGroupUD", checkListGroupUD) :
                new ObjectParameter("CheckListGroupUD", typeof(string));
    
            var checkListGroupNMParameter = checkListGroupNM != null ?
                new ObjectParameter("CheckListGroupNM", checkListGroupNM) :
                new ObjectParameter("CheckListGroupNM", typeof(string));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CheckListMng_CheckListGroup_View>("CheckListGroupMng_function_CheckListGroupSearchResult", checkListGroupUDParameter, checkListGroupNMParameter, remarkParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<CheckListMng_CheckListGroup_View> CheckListGroupMng_function_CheckListGroupSearchResult(string checkListGroupUD, string checkListGroupNM, string remark, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var checkListGroupUDParameter = checkListGroupUD != null ?
                new ObjectParameter("CheckListGroupUD", checkListGroupUD) :
                new ObjectParameter("CheckListGroupUD", typeof(string));
    
            var checkListGroupNMParameter = checkListGroupNM != null ?
                new ObjectParameter("CheckListGroupNM", checkListGroupNM) :
                new ObjectParameter("CheckListGroupNM", typeof(string));
    
            var remarkParameter = remark != null ?
                new ObjectParameter("Remark", remark) :
                new ObjectParameter("Remark", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CheckListMng_CheckListGroup_View>("CheckListGroupMng_function_CheckListGroupSearchResult", mergeOption, checkListGroupUDParameter, checkListGroupNMParameter, remarkParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}