﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SupplierMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SupplierMngEntities : DbContext
    {
        public SupplierMngEntities()
            : base("name=SupplierMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierMng_Factory_View> SupplierMng_Factory_View { get; set; }
        public virtual DbSet<SupplierMng_Supplier_View> SupplierMng_Supplier_View { get; set; }
        public virtual DbSet<SupplierMng_SupplierSearchResult_View> SupplierMng_SupplierSearchResult_View { get; set; }
        public virtual DbSet<List_DeliveryTerm> List_DeliveryTerm { get; set; }
        public virtual DbSet<List_ManufacturerCountry_View> List_ManufacturerCountry_View { get; set; }
        public virtual DbSet<List_PaymentTerm> List_PaymentTerm { get; set; }
        public virtual DbSet<List_Company_View> List_Company_View { get; set; }
        public virtual DbSet<SupplierBank> SupplierBank { get; set; }
        public virtual DbSet<SupplierMng_SupplierBank_View> SupplierMng_SupplierBank_View { get; set; }
    
        public virtual ObjectResult<SupplierMng_SupplierSearchResult_View> SupplierMng_function_SearchSupplier(string sortedBy, string sortedDirection)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SupplierMng_SupplierSearchResult_View>("SupplierMng_function_SearchSupplier", sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<SupplierMng_SupplierSearchResult_View> SupplierMng_function_SearchSupplier(string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SupplierMng_SupplierSearchResult_View>("SupplierMng_function_SearchSupplier", mergeOption, sortedByParameter, sortedDirectionParameter);
        }
    }
}
