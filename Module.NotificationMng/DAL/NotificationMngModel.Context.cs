﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.NotificationMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NotificationMngEntities : DbContext
    {
        public NotificationMngEntities()
            : base("name=NotificationMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NotificationGroup> NotificationGroup { get; set; }
        public virtual DbSet<NotificationGroupMember> NotificationGroupMember { get; set; }
        public virtual DbSet<NotificationMng_NotificationGroup_View> NotificationMng_NotificationGroup_View { get; set; }
        public virtual DbSet<NotificationMng_NotificationGroupMember_View> NotificationMng_NotificationGroupMember_View { get; set; }
        public virtual DbSet<ModuleStatus> ModuleStatus { get; set; }
        public virtual DbSet<NotificationGroupStatus> NotificationGroupStatus { get; set; }
        public virtual DbSet<NotificationMng_Module_View> NotificationMng_Module_View { get; set; }
        public virtual DbSet<NotificationMng_ModuleStatus_View> NotificationMng_ModuleStatus_View { get; set; }
        public virtual DbSet<NotificationMng_NotificationGroupStatus_View> NotificationMng_NotificationGroupStatus_View { get; set; }
        public virtual DbSet<NotificationMng_NotificationUser_View> NotificationMng_NotificationUser_View { get; set; }
    
        public virtual ObjectResult<NotificationMng_NotificationGroup_View> NotificationMng_function_SearchResult(string notificationGroupUD, string notificationGroupNM, string description, string sortedBy, string sortedDirection)
        {
            var notificationGroupUDParameter = notificationGroupUD != null ?
                new ObjectParameter("NotificationGroupUD", notificationGroupUD) :
                new ObjectParameter("NotificationGroupUD", typeof(string));
    
            var notificationGroupNMParameter = notificationGroupNM != null ?
                new ObjectParameter("NotificationGroupNM", notificationGroupNM) :
                new ObjectParameter("NotificationGroupNM", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NotificationMng_NotificationGroup_View>("NotificationMng_function_SearchResult", notificationGroupUDParameter, notificationGroupNMParameter, descriptionParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<NotificationMng_NotificationGroup_View> NotificationMng_function_SearchResult(string notificationGroupUD, string notificationGroupNM, string description, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var notificationGroupUDParameter = notificationGroupUD != null ?
                new ObjectParameter("NotificationGroupUD", notificationGroupUD) :
                new ObjectParameter("NotificationGroupUD", typeof(string));
    
            var notificationGroupNMParameter = notificationGroupNM != null ?
                new ObjectParameter("NotificationGroupNM", notificationGroupNM) :
                new ObjectParameter("NotificationGroupNM", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NotificationMng_NotificationGroup_View>("NotificationMng_function_SearchResult", mergeOption, notificationGroupUDParameter, notificationGroupNMParameter, descriptionParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
