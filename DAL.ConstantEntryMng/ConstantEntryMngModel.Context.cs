﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ConstantEntryMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ConstantEntryMngEntities : DbContext
    {
        public ConstantEntryMngEntities()
            : base("name=ConstantEntryMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ConstantEntry> ConstantEntry { get; set; }
        public virtual DbSet<ConstantEntryMng_ConstantEntry_SearchResultView> ConstantEntryMng_ConstantEntry_SearchResultView { get; set; }
        public virtual DbSet<ConstantEntryMng_ConstantEntry_View> ConstantEntryMng_ConstantEntry_View { get; set; }
        public virtual DbSet<ConstantEntryMng_ComboList> ConstantEntryMng_ComboList { get; set; }
    
        public virtual ObjectResult<ConstantEntryMng_ConstantEntry_SearchResultView> ConstantEntryMng_function_SearchConstantEntry(string displayText, string entryGroup, string sortedBy, string sortedDirection)
        {
            var displayTextParameter = displayText != null ?
                new ObjectParameter("DisplayText", displayText) :
                new ObjectParameter("DisplayText", typeof(string));
    
            var entryGroupParameter = entryGroup != null ?
                new ObjectParameter("EntryGroup", entryGroup) :
                new ObjectParameter("EntryGroup", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConstantEntryMng_ConstantEntry_SearchResultView>("ConstantEntryMng_function_SearchConstantEntry", displayTextParameter, entryGroupParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<ConstantEntryMng_ConstantEntry_SearchResultView> ConstantEntryMng_function_SearchConstantEntry(string displayText, string entryGroup, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var displayTextParameter = displayText != null ?
                new ObjectParameter("DisplayText", displayText) :
                new ObjectParameter("DisplayText", typeof(string));
    
            var entryGroupParameter = entryGroup != null ?
                new ObjectParameter("EntryGroup", entryGroup) :
                new ObjectParameter("EntryGroup", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConstantEntryMng_ConstantEntry_SearchResultView>("ConstantEntryMng_function_SearchConstantEntry", mergeOption, displayTextParameter, entryGroupParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
