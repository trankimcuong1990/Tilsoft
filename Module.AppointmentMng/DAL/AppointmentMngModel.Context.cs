﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AppointmentMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AppointmentMngEntities : DbContext
    {
        public AppointmentMngEntities()
            : base("name=AppointmentMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<AppointmentMng_Appointment_View> AppointmentMng_Appointment_View { get; set; }
        public virtual DbSet<AppointmentMng_ClientSearchResult_View> AppointmentMng_ClientSearchResult_View { get; set; }
    
        public virtual ObjectResult<AppointmentMng_ClientSearchResult_View> AppointmentMng_function_SearchClient(string query)
        {
            var queryParameter = query != null ?
                new ObjectParameter("Query", query) :
                new ObjectParameter("Query", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AppointmentMng_ClientSearchResult_View>("AppointmentMng_function_SearchClient", queryParameter);
        }
    
        public virtual ObjectResult<AppointmentMng_ClientSearchResult_View> AppointmentMng_function_SearchClient(string query, MergeOption mergeOption)
        {
            var queryParameter = query != null ?
                new ObjectParameter("Query", query) :
                new ObjectParameter("Query", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AppointmentMng_ClientSearchResult_View>("AppointmentMng_function_SearchClient", mergeOption, queryParameter);
        }
    }
}