﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotificationTask.FactoryCertificate
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EmailNotificationEntities : DbContext
    {
        public EmailNotificationEntities()
            : base("name=EmailNotificationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<EmailNotificationMessage_FactoryCertificate_View> EmailNotificationMessage_FactoryCertificate_View { get; set; }
    
        public virtual int EmailNotificationMessage_AutoCheckFactoryCertificateValidUntil()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EmailNotificationMessage_AutoCheckFactoryCertificateValidUntil");
        }
    }
}
