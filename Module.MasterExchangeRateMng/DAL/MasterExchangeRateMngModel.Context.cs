﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MasterExchangeRateMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MasterExchangeRateMngEntities : DbContext
    {
        public MasterExchangeRateMngEntities()
            : base("name=MasterExchangeRateMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MasterExchangeRateMng_MasterExchangeRate_SearchView> MasterExchangeRateMng_MasterExchangeRate_SearchView { get; set; }
        public virtual DbSet<MasterExchangeRateMng_MasterExchangeRate_View> MasterExchangeRateMng_MasterExchangeRate_View { get; set; }
        public virtual DbSet<MasterExchangeRate> MasterExchangeRate { get; set; }
    
        public virtual ObjectResult<MasterExchangeRateMng_MasterExchangeRate_SearchView> MasterExchangeRateMng_Function_SearchView(Nullable<System.DateTime> validDate, string currency, string sortedBy, string sortedDirection)
        {
            var validDateParameter = validDate.HasValue ?
                new ObjectParameter("ValidDate", validDate) :
                new ObjectParameter("ValidDate", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MasterExchangeRateMng_MasterExchangeRate_SearchView>("MasterExchangeRateMng_Function_SearchView", validDateParameter, currencyParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<MasterExchangeRateMng_MasterExchangeRate_SearchView> MasterExchangeRateMng_Function_SearchView(Nullable<System.DateTime> validDate, string currency, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var validDateParameter = validDate.HasValue ?
                new ObjectParameter("ValidDate", validDate) :
                new ObjectParameter("ValidDate", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MasterExchangeRateMng_MasterExchangeRate_SearchView>("MasterExchangeRateMng_Function_SearchView", mergeOption, validDateParameter, currencyParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int MasterExchangeRateMng_function_GetExchangeRate(Nullable<System.DateTime> inputDate, string currency)
        {
            var inputDateParameter = inputDate.HasValue ?
                new ObjectParameter("InputDate", inputDate) :
                new ObjectParameter("InputDate", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MasterExchangeRateMng_function_GetExchangeRate", inputDateParameter, currencyParameter);
        }
    }
}
