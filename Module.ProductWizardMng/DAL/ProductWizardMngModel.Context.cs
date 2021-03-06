﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductWizardMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ProductWizardMngEntities : DbContext
    {
        public ProductWizardMngEntities()
            : base("name=ProductWizardMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProductWizardMng_Model_View> ProductWizardMng_Model_View { get; set; }
        public virtual DbSet<ProductWizardMng_FSCPercent_View> ProductWizardMng_FSCPercent_View { get; set; }
        public virtual DbSet<ProductWizardMng_FSCType_View> ProductWizardMng_FSCType_View { get; set; }
    
        public virtual ObjectResult<ProductWizardMng_function_GetBackCushionOption_Result> ProductWizardMng_function_GetBackCushionOption(Nullable<int> productGroupID, string season, Nullable<bool> hasCushion)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasCushionParameter = hasCushion.HasValue ?
                new ObjectParameter("HasCushion", hasCushion) :
                new ObjectParameter("HasCushion", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetBackCushionOption_Result>("ProductWizardMng_function_GetBackCushionOption", productGroupIDParameter, seasonParameter, hasCushionParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetCushionColorOption_Result> ProductWizardMng_function_GetCushionColorOption(Nullable<int> productGroupID, string season, Nullable<bool> hasCushion)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasCushionParameter = hasCushion.HasValue ?
                new ObjectParameter("HasCushion", hasCushion) :
                new ObjectParameter("HasCushion", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetCushionColorOption_Result>("ProductWizardMng_function_GetCushionColorOption", productGroupIDParameter, seasonParameter, hasCushionParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetCushionTypeOption_Result> ProductWizardMng_function_GetCushionTypeOption(Nullable<int> productGroupID, string season, Nullable<bool> hasCushion)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasCushionParameter = hasCushion.HasValue ?
                new ObjectParameter("HasCushion", hasCushion) :
                new ObjectParameter("HasCushion", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetCushionTypeOption_Result>("ProductWizardMng_function_GetCushionTypeOption", productGroupIDParameter, seasonParameter, hasCushionParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetFrameMaterialColorOption_Result> ProductWizardMng_function_GetFrameMaterialColorOption(Nullable<int> productGroupID, string season, Nullable<bool> hasFrameMaterial)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasFrameMaterialParameter = hasFrameMaterial.HasValue ?
                new ObjectParameter("HasFrameMaterial", hasFrameMaterial) :
                new ObjectParameter("HasFrameMaterial", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetFrameMaterialColorOption_Result>("ProductWizardMng_function_GetFrameMaterialColorOption", productGroupIDParameter, seasonParameter, hasFrameMaterialParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetFrameMaterialOption_Result> ProductWizardMng_function_GetFrameMaterialOption(Nullable<int> productGroupID, string season, Nullable<bool> hasFrameMaterial)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasFrameMaterialParameter = hasFrameMaterial.HasValue ?
                new ObjectParameter("HasFrameMaterial", hasFrameMaterial) :
                new ObjectParameter("HasFrameMaterial", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetFrameMaterialOption_Result>("ProductWizardMng_function_GetFrameMaterialOption", productGroupIDParameter, seasonParameter, hasFrameMaterialParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetMaterialColorOption_Result> ProductWizardMng_function_GetMaterialColorOption(Nullable<int> productGroupID, string season)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetMaterialColorOption_Result>("ProductWizardMng_function_GetMaterialColorOption", productGroupIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetMaterialOption_Result> ProductWizardMng_function_GetMaterialOption(Nullable<int> productGroupID)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetMaterialOption_Result>("ProductWizardMng_function_GetMaterialOption", productGroupIDParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetMaterialTypeOption_Result> ProductWizardMng_function_GetMaterialTypeOption(Nullable<int> productGroupID, string season)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetMaterialTypeOption_Result>("ProductWizardMng_function_GetMaterialTypeOption", productGroupIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetPackagingMethodOption_Result> ProductWizardMng_function_GetPackagingMethodOption(Nullable<int> modelID, Nullable<int> clientID)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetPackagingMethodOption_Result>("ProductWizardMng_function_GetPackagingMethodOption", modelIDParameter, clientIDParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetSeatCushionOption_Result> ProductWizardMng_function_GetSeatCushionOption(Nullable<int> productGroupID, string season, Nullable<bool> hasCushion)
        {
            var productGroupIDParameter = productGroupID.HasValue ?
                new ObjectParameter("ProductGroupID", productGroupID) :
                new ObjectParameter("ProductGroupID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasCushionParameter = hasCushion.HasValue ?
                new ObjectParameter("HasCushion", hasCushion) :
                new ObjectParameter("HasCushion", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetSeatCushionOption_Result>("ProductWizardMng_function_GetSeatCushionOption", productGroupIDParameter, seasonParameter, hasCushionParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetSubMaterialColorOption_Result> ProductWizardMng_function_GetSubMaterialColorOption(Nullable<int> modelID, string season, Nullable<bool> hasSubMaterial)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasSubMaterialParameter = hasSubMaterial.HasValue ?
                new ObjectParameter("HasSubMaterial", hasSubMaterial) :
                new ObjectParameter("HasSubMaterial", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetSubMaterialColorOption_Result>("ProductWizardMng_function_GetSubMaterialColorOption", modelIDParameter, seasonParameter, hasSubMaterialParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetSubMaterialOption_Result> ProductWizardMng_function_GetSubMaterialOption(Nullable<int> modelID, string season, Nullable<bool> hasSubMaterial)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var hasSubMaterialParameter = hasSubMaterial.HasValue ?
                new ObjectParameter("HasSubMaterial", hasSubMaterial) :
                new ObjectParameter("HasSubMaterial", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetSubMaterialOption_Result>("ProductWizardMng_function_GetSubMaterialOption", modelIDParameter, seasonParameter, hasSubMaterialParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetBreakdown_Result> ProductWizardMng_function_GetBreakdown(Nullable<int> modelID)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetBreakdown_Result>("ProductWizardMng_function_GetBreakdown", modelIDParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetBreakdownCategory_Result> ProductWizardMng_function_GetBreakdownCategory()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetBreakdownCategory_Result>("ProductWizardMng_function_GetBreakdownCategory");
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetBreakdownCategoryOption_Result> ProductWizardMng_function_GetBreakdownCategoryOption(Nullable<int> modelID)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetBreakdownCategoryOption_Result>("ProductWizardMng_function_GetBreakdownCategoryOption", modelIDParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetBreakdownManagementFee_Result> ProductWizardMng_function_GetBreakdownManagementFee(Nullable<int> modelID)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetBreakdownManagementFee_Result>("ProductWizardMng_function_GetBreakdownManagementFee", modelIDParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetClientSpecialPackagingMethod_Result> ProductWizardMng_function_GetClientSpecialPackagingMethod(Nullable<int> clientID)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetClientSpecialPackagingMethod_Result>("ProductWizardMng_function_GetClientSpecialPackagingMethod", clientIDParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice_Result> ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice(Nullable<int> modelID, string season)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice_Result>("ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice", modelIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration_Result> ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration(Nullable<int> modelID, string season)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration_Result>("ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration", modelIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetModelFixPriceConfiguration_Result> ProductWizardMng_function_GetModelFixPriceConfiguration(Nullable<int> modelID, string season)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetModelFixPriceConfiguration_Result>("ProductWizardMng_function_GetModelFixPriceConfiguration", modelIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetModelPriceConfigurationDetail_Result> ProductWizardMng_function_GetModelPriceConfigurationDetail(Nullable<int> modelID, string season)
        {
            var modelIDParameter = modelID.HasValue ?
                new ObjectParameter("ModelID", modelID) :
                new ObjectParameter("ModelID", typeof(int));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetModelPriceConfigurationDetail_Result>("ProductWizardMng_function_GetModelPriceConfigurationDetail", modelIDParameter, seasonParameter);
        }
    
        public virtual ObjectResult<ProductWizardMng_function_GetExchangeRate_Result> ProductWizardMng_function_GetExchangeRate(Nullable<System.DateTime> inputDate, string currency)
        {
            var inputDateParameter = inputDate.HasValue ?
                new ObjectParameter("InputDate", inputDate) :
                new ObjectParameter("InputDate", typeof(System.DateTime));
    
            var currencyParameter = currency != null ?
                new ObjectParameter("Currency", currency) :
                new ObjectParameter("Currency", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductWizardMng_function_GetExchangeRate_Result>("ProductWizardMng_function_GetExchangeRate", inputDateParameter, currencyParameter);
        }
    }
}
