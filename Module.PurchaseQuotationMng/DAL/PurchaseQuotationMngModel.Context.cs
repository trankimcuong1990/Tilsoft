﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseQuotationMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PurchaseQuotationMngEntities : DbContext
    {
        public PurchaseQuotationMngEntities()
            : base("name=PurchaseQuotationMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PurchaseQuotation> PurchaseQuotation { get; set; }
        public virtual DbSet<PurchaseQuotationDetail> PurchaseQuotationDetail { get; set; }
        public virtual DbSet<SupportMng_PurchaseDeliveryTerm_View> SupportMng_PurchaseDeliveryTerm_View { get; set; }
        public virtual DbSet<SupportMng_PurchasePaymentTerm_View> SupportMng_PurchasePaymentTerm_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_DefaultPrice_Suppliers_View> PurchaseQuotationMng_DefaultPrice_Suppliers_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_DefaultPrice_View> PurchaseQuotationMng_DefaultPrice_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_PurchaseQuotationSearch_View> PurchaseQuotationMng_PurchaseQuotationSearch_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_PurchaseQuotationDetail_View> PurchaseQuotationMng_PurchaseQuotationDetail_View { get; set; }
        public virtual DbSet<PurchaseQoutationMng_GetFactoryRawMaterial_View> PurchaseQoutationMng_GetFactoryRawMaterial_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_FactoryRawMaterial_View> PurchaseQuotationMng_FactoryRawMaterial_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_PurchaseQuotation_view> PurchaseQuotationMng_PurchaseQuotation_view { get; set; }
        public virtual DbSet<PurchaseQuotationMng_DefaultPrice_ProductionItem_View> PurchaseQuotationMng_DefaultPrice_ProductionItem_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View> PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View { get; set; }
        public virtual DbSet<PurchaseQuotationMng_ProductionItemDefaultPrice_View> PurchaseQuotationMng_ProductionItemDefaultPrice_View { get; set; }
        public virtual DbSet<NotificationGroup> NotificationGroup { get; set; }
        public virtual DbSet<NotificationGroupMember> NotificationGroupMember { get; set; }
        public virtual DbSet<FactoryRawMaterial> FactoryRawMaterial { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<ProductionItemGroup> ProductionItemGroup { get; set; }
        public virtual DbSet<SupportMng_ProductionItem_View> SupportMng_ProductionItem_View { get; set; }
        public virtual DbSet<ProductionFrameWeight> ProductionFrameWeight { get; set; }
        public virtual DbSet<ProductionFrameWeightHistory> ProductionFrameWeightHistory { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View> PurchasingPriceFactoryOverview_PurchasingPriceFactory_View { get; set; }
    
        public virtual ObjectResult<PurchaseQuotationMng_PurchaseQuotationSearch_View> PurchaseQuotationMng_function_PurchaseQoutationSearchReasult(string purchaseQuotationUD, Nullable<int> factoryRawMaterialID, Nullable<System.DateTime> validFrom, Nullable<System.DateTime> validTo, Nullable<int> purchaseDeliveryTermID, Nullable<int> purchasePaymentTermID, string currency, string sortedBy, string sortedDirection)
        {
            var purchaseQuotationUDParameter = purchaseQuotationUD != null ?
                new ObjectParameter("PurchaseQuotationUD", purchaseQuotationUD) :
                new ObjectParameter("PurchaseQuotationUD", typeof(string));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var validFromParameter = validFrom.HasValue ?
                new ObjectParameter("ValidFrom", validFrom) :
                new ObjectParameter("ValidFrom", typeof(System.DateTime));
    
            var validToParameter = validTo.HasValue ?
                new ObjectParameter("ValidTo", validTo) :
                new ObjectParameter("ValidTo", typeof(System.DateTime));
    
            var purchaseDeliveryTermIDParameter = purchaseDeliveryTermID.HasValue ?
                new ObjectParameter("PurchaseDeliveryTermID", purchaseDeliveryTermID) :
                new ObjectParameter("PurchaseDeliveryTermID", typeof(int));
    
            var purchasePaymentTermIDParameter = purchasePaymentTermID.HasValue ?
                new ObjectParameter("PurchasePaymentTermID", purchasePaymentTermID) :
                new ObjectParameter("PurchasePaymentTermID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_PurchaseQuotationSearch_View>("PurchaseQuotationMng_function_PurchaseQoutationSearchReasult", purchaseQuotationUDParameter, factoryRawMaterialIDParameter, validFromParameter, validToParameter, purchaseDeliveryTermIDParameter, purchasePaymentTermIDParameter, currencyParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_PurchaseQuotationSearch_View> PurchaseQuotationMng_function_PurchaseQoutationSearchReasult(string purchaseQuotationUD, Nullable<int> factoryRawMaterialID, Nullable<System.DateTime> validFrom, Nullable<System.DateTime> validTo, Nullable<int> purchaseDeliveryTermID, Nullable<int> purchasePaymentTermID, string currency, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var purchaseQuotationUDParameter = purchaseQuotationUD != null ?
                new ObjectParameter("PurchaseQuotationUD", purchaseQuotationUD) :
                new ObjectParameter("PurchaseQuotationUD", typeof(string));
    
            var factoryRawMaterialIDParameter = factoryRawMaterialID.HasValue ?
                new ObjectParameter("FactoryRawMaterialID", factoryRawMaterialID) :
                new ObjectParameter("FactoryRawMaterialID", typeof(int));
    
            var validFromParameter = validFrom.HasValue ?
                new ObjectParameter("ValidFrom", validFrom) :
                new ObjectParameter("ValidFrom", typeof(System.DateTime));
    
            var validToParameter = validTo.HasValue ?
                new ObjectParameter("ValidTo", validTo) :
                new ObjectParameter("ValidTo", typeof(System.DateTime));
    
            var purchaseDeliveryTermIDParameter = purchaseDeliveryTermID.HasValue ?
                new ObjectParameter("PurchaseDeliveryTermID", purchaseDeliveryTermID) :
                new ObjectParameter("PurchaseDeliveryTermID", typeof(int));
    
            var purchasePaymentTermIDParameter = purchasePaymentTermID.HasValue ?
                new ObjectParameter("PurchasePaymentTermID", purchasePaymentTermID) :
                new ObjectParameter("PurchasePaymentTermID", typeof(int));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_PurchaseQuotationSearch_View>("PurchaseQuotationMng_function_PurchaseQoutationSearchReasult", mergeOption, purchaseQuotationUDParameter, factoryRawMaterialIDParameter, validFromParameter, validToParameter, purchaseDeliveryTermIDParameter, purchasePaymentTermIDParameter, currencyParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_DefaultPrice_View> PurchaseQuotationMng_function_DefaultPrice(string sortedDirection)
        {
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_DefaultPrice_View>("PurchaseQuotationMng_function_DefaultPrice", sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_DefaultPrice_View> PurchaseQuotationMng_function_DefaultPrice(string sortedDirection, MergeOption mergeOption)
        {
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_DefaultPrice_View>("PurchaseQuotationMng_function_DefaultPrice", mergeOption, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<string> PurchaseQuotationMng_function_GeneratePurchaseQuotationUD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("PurchaseQuotationMng_function_GeneratePurchaseQuotationUD");
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_DefaultPrice_ProductionItem_View> PurchaseQuotationMng_function_GetProductionItem(Nullable<int> supplier, string productionItem, Nullable<bool> isDefault, Nullable<int> company)
        {
            var supplierParameter = supplier.HasValue ?
                new ObjectParameter("supplier", supplier) :
                new ObjectParameter("supplier", typeof(int));
    
            var productionItemParameter = productionItem != null ?
                new ObjectParameter("productionItem", productionItem) :
                new ObjectParameter("productionItem", typeof(string));
    
            var isDefaultParameter = isDefault.HasValue ?
                new ObjectParameter("isDefault", isDefault) :
                new ObjectParameter("isDefault", typeof(bool));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_DefaultPrice_ProductionItem_View>("PurchaseQuotationMng_function_GetProductionItem", supplierParameter, productionItemParameter, isDefaultParameter, companyParameter);
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_DefaultPrice_ProductionItem_View> PurchaseQuotationMng_function_GetProductionItem(Nullable<int> supplier, string productionItem, Nullable<bool> isDefault, Nullable<int> company, MergeOption mergeOption)
        {
            var supplierParameter = supplier.HasValue ?
                new ObjectParameter("supplier", supplier) :
                new ObjectParameter("supplier", typeof(int));
    
            var productionItemParameter = productionItem != null ?
                new ObjectParameter("productionItem", productionItem) :
                new ObjectParameter("productionItem", typeof(string));
    
            var isDefaultParameter = isDefault.HasValue ?
                new ObjectParameter("isDefault", isDefault) :
                new ObjectParameter("isDefault", typeof(bool));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_DefaultPrice_ProductionItem_View>("PurchaseQuotationMng_function_GetProductionItem", mergeOption, supplierParameter, productionItemParameter, isDefaultParameter, companyParameter);
        }
    
        public virtual int PurchaseQuotationMng_function_GetPurchaseQuotation(Nullable<int> subSupplierID, string productionItemUD, Nullable<bool> isDefault)
        {
            var subSupplierIDParameter = subSupplierID.HasValue ?
                new ObjectParameter("subSupplierID", subSupplierID) :
                new ObjectParameter("subSupplierID", typeof(int));
    
            var productionItemUDParameter = productionItemUD != null ?
                new ObjectParameter("productionItemUD", productionItemUD) :
                new ObjectParameter("productionItemUD", typeof(string));
    
            var isDefaultParameter = isDefault.HasValue ?
                new ObjectParameter("isDefault", isDefault) :
                new ObjectParameter("isDefault", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PurchaseQuotationMng_function_GetPurchaseQuotation", subSupplierIDParameter, productionItemUDParameter, isDefaultParameter);
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View> PurchaseQuotationMng_function_GetPurchaseQuotationDetail(Nullable<int> supplier, string productionItem, Nullable<bool> isDefault, Nullable<int> company)
        {
            var supplierParameter = supplier.HasValue ?
                new ObjectParameter("supplier", supplier) :
                new ObjectParameter("supplier", typeof(int));
    
            var productionItemParameter = productionItem != null ?
                new ObjectParameter("productionItem", productionItem) :
                new ObjectParameter("productionItem", typeof(string));
    
            var isDefaultParameter = isDefault.HasValue ?
                new ObjectParameter("isDefault", isDefault) :
                new ObjectParameter("isDefault", typeof(bool));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View>("PurchaseQuotationMng_function_GetPurchaseQuotationDetail", supplierParameter, productionItemParameter, isDefaultParameter, companyParameter);
        }
    
        public virtual ObjectResult<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View> PurchaseQuotationMng_function_GetPurchaseQuotationDetail(Nullable<int> supplier, string productionItem, Nullable<bool> isDefault, Nullable<int> company, MergeOption mergeOption)
        {
            var supplierParameter = supplier.HasValue ?
                new ObjectParameter("supplier", supplier) :
                new ObjectParameter("supplier", typeof(int));
    
            var productionItemParameter = productionItem != null ?
                new ObjectParameter("productionItem", productionItem) :
                new ObjectParameter("productionItem", typeof(string));
    
            var isDefaultParameter = isDefault.HasValue ?
                new ObjectParameter("isDefault", isDefault) :
                new ObjectParameter("isDefault", typeof(bool));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchaseQuotationMng_DefaultPrice_PurchaseQuotationDetail_View>("PurchaseQuotationMng_function_GetPurchaseQuotationDetail", mergeOption, supplierParameter, productionItemParameter, isDefaultParameter, companyParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> PurchaseQuotationMng_function_GetMaxPurchasing(Nullable<int> supplier, string productionItem, Nullable<bool> isDefault, Nullable<int> company)
        {
            var supplierParameter = supplier.HasValue ?
                new ObjectParameter("supplier", supplier) :
                new ObjectParameter("supplier", typeof(int));
    
            var productionItemParameter = productionItem != null ?
                new ObjectParameter("productionItem", productionItem) :
                new ObjectParameter("productionItem", typeof(string));
    
            var isDefaultParameter = isDefault.HasValue ?
                new ObjectParameter("isDefault", isDefault) :
                new ObjectParameter("isDefault", typeof(bool));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PurchaseQuotationMng_function_GetMaxPurchasing", supplierParameter, productionItemParameter, isDefaultParameter, companyParameter);
        }
    
        public virtual ObjectResult<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View> PurchasingPriceFactoryOverview_function_GetPurchasingPrice(string validDate, Nullable<int> supplierID, Nullable<int> materialGroupID, string materialNM, Nullable<bool> statusID, string orderBy, string orderDirection)
        {
            var validDateParameter = validDate != null ?
                new ObjectParameter("validDate", validDate) :
                new ObjectParameter("validDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var materialGroupIDParameter = materialGroupID.HasValue ?
                new ObjectParameter("materialGroupID", materialGroupID) :
                new ObjectParameter("materialGroupID", typeof(int));
    
            var materialNMParameter = materialNM != null ?
                new ObjectParameter("materialNM", materialNM) :
                new ObjectParameter("materialNM", typeof(string));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("statusID", statusID) :
                new ObjectParameter("statusID", typeof(bool));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("orderBy", orderBy) :
                new ObjectParameter("orderBy", typeof(string));
    
            var orderDirectionParameter = orderDirection != null ?
                new ObjectParameter("orderDirection", orderDirection) :
                new ObjectParameter("orderDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View>("PurchasingPriceFactoryOverview_function_GetPurchasingPrice", validDateParameter, supplierIDParameter, materialGroupIDParameter, materialNMParameter, statusIDParameter, orderByParameter, orderDirectionParameter);
        }
    
        public virtual ObjectResult<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View> PurchasingPriceFactoryOverview_function_GetPurchasingPrice(string validDate, Nullable<int> supplierID, Nullable<int> materialGroupID, string materialNM, Nullable<bool> statusID, string orderBy, string orderDirection, MergeOption mergeOption)
        {
            var validDateParameter = validDate != null ?
                new ObjectParameter("validDate", validDate) :
                new ObjectParameter("validDate", typeof(string));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("supplierID", supplierID) :
                new ObjectParameter("supplierID", typeof(int));
    
            var materialGroupIDParameter = materialGroupID.HasValue ?
                new ObjectParameter("materialGroupID", materialGroupID) :
                new ObjectParameter("materialGroupID", typeof(int));
    
            var materialNMParameter = materialNM != null ?
                new ObjectParameter("materialNM", materialNM) :
                new ObjectParameter("materialNM", typeof(string));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("statusID", statusID) :
                new ObjectParameter("statusID", typeof(bool));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("orderBy", orderBy) :
                new ObjectParameter("orderBy", typeof(string));
    
            var orderDirectionParameter = orderDirection != null ?
                new ObjectParameter("orderDirection", orderDirection) :
                new ObjectParameter("orderDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PurchasingPriceFactoryOverview_PurchasingPriceFactory_View>("PurchasingPriceFactoryOverview_function_GetPurchasingPrice", mergeOption, validDateParameter, supplierIDParameter, materialGroupIDParameter, materialNMParameter, statusIDParameter, orderByParameter, orderDirectionParameter);
        }
    }
}
