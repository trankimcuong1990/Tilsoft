﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ComplianceCertificateTypeMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ComplianceCertificateTypeEntities : DbContext
    {
        public ComplianceCertificateTypeEntities()
            : base("name=ComplianceCertificateTypeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ComplianceCertificateType> ComplianceCertificateType { get; set; }
        public virtual DbSet<ComplianceCertificateTypeMng_ComplianceCertificateType_View> ComplianceCertificateTypeMng_ComplianceCertificateType_View { get; set; }
        public virtual DbSet<ComplianceCertificateTypeMng_ComplianceCertificateTypeSearch_View> ComplianceCertificateTypeMng_ComplianceCertificateTypeSearch_View { get; set; }
        public virtual DbSet<ComplianceCertificateTypeMng_ComplianceCertificateTypeCheck_View> ComplianceCertificateTypeMng_ComplianceCertificateTypeCheck_View { get; set; }
    
        public virtual ObjectResult<ComplianceCertificateTypeMng_ComplianceCertificateType_View> ComplianceCertificateTypeMng_function_ComplianceCertificateTypeSearchResult(string complianceCertificateTypeUD, string complianceCertificateTypeNM, Nullable<bool> isRequired, string sortedBy, string sortedDirection)
        {
            var complianceCertificateTypeUDParameter = complianceCertificateTypeUD != null ?
                new ObjectParameter("ComplianceCertificateTypeUD", complianceCertificateTypeUD) :
                new ObjectParameter("ComplianceCertificateTypeUD", typeof(string));
    
            var complianceCertificateTypeNMParameter = complianceCertificateTypeNM != null ?
                new ObjectParameter("ComplianceCertificateTypeNM", complianceCertificateTypeNM) :
                new ObjectParameter("ComplianceCertificateTypeNM", typeof(string));
    
            var isRequiredParameter = isRequired.HasValue ?
                new ObjectParameter("IsRequired", isRequired) :
                new ObjectParameter("IsRequired", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ComplianceCertificateTypeMng_ComplianceCertificateType_View>("ComplianceCertificateTypeMng_function_ComplianceCertificateTypeSearchResult", complianceCertificateTypeUDParameter, complianceCertificateTypeNMParameter, isRequiredParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ComplianceCertificateTypeMng_ComplianceCertificateType_View> ComplianceCertificateTypeMng_function_ComplianceCertificateTypeSearchResult(string complianceCertificateTypeUD, string complianceCertificateTypeNM, Nullable<bool> isRequired, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var complianceCertificateTypeUDParameter = complianceCertificateTypeUD != null ?
                new ObjectParameter("ComplianceCertificateTypeUD", complianceCertificateTypeUD) :
                new ObjectParameter("ComplianceCertificateTypeUD", typeof(string));
    
            var complianceCertificateTypeNMParameter = complianceCertificateTypeNM != null ?
                new ObjectParameter("ComplianceCertificateTypeNM", complianceCertificateTypeNM) :
                new ObjectParameter("ComplianceCertificateTypeNM", typeof(string));
    
            var isRequiredParameter = isRequired.HasValue ?
                new ObjectParameter("IsRequired", isRequired) :
                new ObjectParameter("IsRequired", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ComplianceCertificateTypeMng_ComplianceCertificateType_View>("ComplianceCertificateTypeMng_function_ComplianceCertificateTypeSearchResult", mergeOption, complianceCertificateTypeUDParameter, complianceCertificateTypeNMParameter, isRequiredParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}