﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotificationTask.StockStatusQnt
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StockStatusQntNotificationEntities : DbContext
    {
        public StockStatusQntNotificationEntities()
            : base("name=StockStatusQntNotificationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<StockStatusQntRpt_StockStatusQnt_View> StockStatusQntRpt_StockStatusQnt_View { get; set; }
        public virtual DbSet<Notification_StockStatusQntUser_View> Notification_StockStatusQntUser_View { get; set; }
        public virtual DbSet<ProductionItemMng_ProductionItemGroup_View> ProductionItemMng_ProductionItemGroup_View { get; set; }
    }
}