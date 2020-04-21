﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OPSequence.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OPSequenceEntities : DbContext
    {
        public OPSequenceEntities()
            : base("name=OPSequenceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OPSequence> OPSequence { get; set; }
        public virtual DbSet<OPSequenceDetail> OPSequenceDetail { get; set; }
        public virtual DbSet<OPSequence_OPSequence_View> OPSequence_OPSequence_View { get; set; }
        public virtual DbSet<OPSequence_OPSequenceDetail_View> OPSequence_OPSequenceDetail_View { get; set; }
        public virtual DbSet<OPSequence_OPSequenceSearchResult_View> OPSequence_OPSequenceSearchResult_View { get; set; }
    
        public virtual ObjectResult<OPSequence_OPSequenceSearchResult_View> OPSequence_function_OPSequenceSearchResult(string name, string company, string orderBy, string orderDirection)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var companyParameter = company != null ?
                new ObjectParameter("Company", company) :
                new ObjectParameter("Company", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            var orderDirectionParameter = orderDirection != null ?
                new ObjectParameter("OrderDirection", orderDirection) :
                new ObjectParameter("OrderDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OPSequence_OPSequenceSearchResult_View>("OPSequence_function_OPSequenceSearchResult", nameParameter, companyParameter, orderByParameter, orderDirectionParameter);
        }
    
        public virtual ObjectResult<OPSequence_OPSequenceSearchResult_View> OPSequence_function_OPSequenceSearchResult(string name, string company, string orderBy, string orderDirection, MergeOption mergeOption)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var companyParameter = company != null ?
                new ObjectParameter("Company", company) :
                new ObjectParameter("Company", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            var orderDirectionParameter = orderDirection != null ?
                new ObjectParameter("OrderDirection", orderDirection) :
                new ObjectParameter("OrderDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OPSequence_OPSequenceSearchResult_View>("OPSequence_function_OPSequenceSearchResult", mergeOption, nameParameter, companyParameter, orderByParameter, orderDirectionParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> OPSequence_function_HasBOM(Nullable<int> oPSequenceID)
        {
            var oPSequenceIDParameter = oPSequenceID.HasValue ?
                new ObjectParameter("OPSequenceID", oPSequenceID) :
                new ObjectParameter("OPSequenceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("OPSequence_function_HasBOM", oPSequenceIDParameter);
        }
    }
}