﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.TutorialsMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TutorialsMngEntities : DbContext
    {
        public TutorialsMngEntities()
            : base("name=TutorialsMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tutorials> Tutorials { get; set; }
        public virtual DbSet<TutorialsList_View> TutorialsList_View { get; set; }
    
        public virtual ObjectResult<TutorialsList_View> TutorialsMng_function_getTutorials()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TutorialsList_View>("TutorialsMng_function_getTutorials");
        }
    
        public virtual ObjectResult<TutorialsList_View> TutorialsMng_function_getTutorials(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TutorialsList_View>("TutorialsMng_function_getTutorials", mergeOption);
        }
    }
}
