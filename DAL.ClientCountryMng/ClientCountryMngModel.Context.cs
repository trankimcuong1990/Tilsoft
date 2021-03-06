﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientCountryMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ClientCountryMngEntities : DbContext
    {
        public ClientCountryMngEntities()
            : base("name=ClientCountryMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClientCountry> ClientCountry { get; set; }
        public virtual DbSet<ClientCountryMng_ClientCountrySearchResult_View> ClientCountryMng_ClientCountrySearchResult_View { get; set; }
        public virtual DbSet<ClientCountryMng_ClientCountry_View> ClientCountryMng_ClientCountry_View { get; set; }
    
        public virtual ObjectResult<ClientCountryMng_ClientCountrySearchResult_View> ClientCountryMng_function_SearchCountry(string clientCountryUD, string clientCountryNM, string sortedBy, string sortedDirection)
        {
            var clientCountryUDParameter = clientCountryUD != null ?
                new ObjectParameter("ClientCountryUD", clientCountryUD) :
                new ObjectParameter("ClientCountryUD", typeof(string));
    
            var clientCountryNMParameter = clientCountryNM != null ?
                new ObjectParameter("ClientCountryNM", clientCountryNM) :
                new ObjectParameter("ClientCountryNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientCountryMng_ClientCountrySearchResult_View>("ClientCountryMng_function_SearchCountry", clientCountryUDParameter, clientCountryNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ClientCountryMng_ClientCountrySearchResult_View> ClientCountryMng_function_SearchCountry(string clientCountryUD, string clientCountryNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var clientCountryUDParameter = clientCountryUD != null ?
                new ObjectParameter("ClientCountryUD", clientCountryUD) :
                new ObjectParameter("ClientCountryUD", typeof(string));
    
            var clientCountryNMParameter = clientCountryNM != null ?
                new ObjectParameter("ClientCountryNM", clientCountryNM) :
                new ObjectParameter("ClientCountryNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientCountryMng_ClientCountrySearchResult_View>("ClientCountryMng_function_SearchCountry", mergeOption, clientCountryUDParameter, clientCountryNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
