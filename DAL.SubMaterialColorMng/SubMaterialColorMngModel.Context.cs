﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SubMaterialColorMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SubMaterialColorMngEntities : DbContext
    {
        public SubMaterialColorMngEntities()
            : base("name=SubMaterialColorMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SubMaterialColor> SubMaterialColor { get; set; }
        public virtual DbSet<SubMaterialColorMng_SubMaterialColor_View> SubMaterialColorMng_SubMaterialColor_View { get; set; }
        public virtual DbSet<SubMaterialColorMng_SubMaterialColorSearchResult_View> SubMaterialColorMng_SubMaterialColorSearchResult_View { get; set; }
        public virtual DbSet<SubMaterialColorMng_SubMaterialColorCheck_View> SubMaterialColorMng_SubMaterialColorCheck_View { get; set; }
    
        public virtual ObjectResult<SubMaterialColorMng_SubMaterialColorSearchResult_View> SubMaterialColorMng_function_SearchSubMaterialColor(string subMaterialColorUD, string subMaterialColorNM, string sortedBy, string sortedDirection)
        {
            var subMaterialColorUDParameter = subMaterialColorUD != null ?
                new ObjectParameter("SubMaterialColorUD", subMaterialColorUD) :
                new ObjectParameter("SubMaterialColorUD", typeof(string));
    
            var subMaterialColorNMParameter = subMaterialColorNM != null ?
                new ObjectParameter("SubMaterialColorNM", subMaterialColorNM) :
                new ObjectParameter("SubMaterialColorNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SubMaterialColorMng_SubMaterialColorSearchResult_View>("SubMaterialColorMng_function_SearchSubMaterialColor", subMaterialColorUDParameter, subMaterialColorNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<SubMaterialColorMng_SubMaterialColorSearchResult_View> SubMaterialColorMng_function_SearchSubMaterialColor(string subMaterialColorUD, string subMaterialColorNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var subMaterialColorUDParameter = subMaterialColorUD != null ?
                new ObjectParameter("SubMaterialColorUD", subMaterialColorUD) :
                new ObjectParameter("SubMaterialColorUD", typeof(string));
    
            var subMaterialColorNMParameter = subMaterialColorNM != null ?
                new ObjectParameter("SubMaterialColorNM", subMaterialColorNM) :
                new ObjectParameter("SubMaterialColorNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SubMaterialColorMng_SubMaterialColorSearchResult_View>("SubMaterialColorMng_function_SearchSubMaterialColor", mergeOption, subMaterialColorUDParameter, subMaterialColorNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<string> SubMaterialColorMng_function_GenerateCode()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SubMaterialColorMng_function_GenerateCode");
        }
    }
}
