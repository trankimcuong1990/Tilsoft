﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientGroupMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ClientGroupMngEntities : DbContext
    {
        public ClientGroupMngEntities()
            : base("name=ClientGroupMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClientGroup> ClientGroup { get; set; }
        public virtual DbSet<ClientGroupMng_ClientGroup_View> ClientGroupMng_ClientGroup_View { get; set; }
        public virtual DbSet<ClientGroupMng_ClientGroupSearchResult_View> ClientGroupMng_ClientGroupSearchResult_View { get; set; }
    
        public virtual ObjectResult<ClientGroupMng_ClientGroupSearchResult_View> ClientGroupMng_function_SearchClientGroup(string sortedBy, string sortedDirection)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientGroupMng_ClientGroupSearchResult_View>("ClientGroupMng_function_SearchClientGroup", sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ClientGroupMng_ClientGroupSearchResult_View> ClientGroupMng_function_SearchClientGroup(string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientGroupMng_ClientGroupSearchResult_View>("ClientGroupMng_function_SearchClientGroup", mergeOption, sortedByParameter, sortedDirectionParameter);
        }
    }
}
