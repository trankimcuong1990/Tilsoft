﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotificationTask.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ModelNotificationEntities : DbContext
    {
        public ModelNotificationEntities()
            : base("name=ModelNotificationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Notification_Factory_View> Notification_Factory_View { get; set; }
        public virtual DbSet<Notification_FactoryUser_View> Notification_FactoryUser_View { get; set; }
        public virtual DbSet<Notification_MissionInfoModel_View> Notification_MissionInfoModel_View { get; set; }
        public virtual DbSet<AccountMng_UserPermission_View> AccountMng_UserPermission_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<Notification_EmailContact_View> Notification_EmailContact_View { get; set; }
    
        public virtual ObjectResult<Notification_MissionInfoModel_View> Notification_function_GetMissingInfoModel(Nullable<int> factoryID)
        {
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Notification_MissionInfoModel_View>("Notification_function_GetMissingInfoModel", factoryIDParameter);
        }
    
        public virtual ObjectResult<Notification_MissionInfoModel_View> Notification_function_GetMissingInfoModel(Nullable<int> factoryID, MergeOption mergeOption)
        {
            var factoryIDParameter = factoryID.HasValue ?
                new ObjectParameter("FactoryID", factoryID) :
                new ObjectParameter("FactoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Notification_MissionInfoModel_View>("Notification_function_GetMissingInfoModel", mergeOption, factoryIDParameter);
        }
    }
}
