﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryRawMaterialMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FactoryRawMaterialMngEntities : DbContext
    {
        public FactoryRawMaterialMngEntities()
            : base("name=FactoryRawMaterialMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FactoryRawMaterial> FactoryRawMaterial { get; set; }
        public virtual DbSet<FactoryRawMaterialCertificate> FactoryRawMaterialCertificate { get; set; }
        public virtual DbSet<FactoryRawMaterialTest> FactoryRawMaterialTest { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterial_View> FactoryRawMaterialMng_FactoryRawMaterial_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialCertificate_View> FactoryRawMaterialMng_FactoryRawMaterialCertificate_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialTest_View> FactoryRawMaterialMng_FactoryRawMaterialTest_View { get; set; }
        public virtual DbSet<KeyRawMaterial> KeyRawMaterial { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_KeyRawMaterial_View> FactoryRawMaterialMng_KeyRawMaterial_View { get; set; }
        public virtual DbSet<FactoryRawMaterialPricingPerson> FactoryRawMaterialPricingPerson { get; set; }
        public virtual DbSet<FactoryRawMaterialQualityPerson> FactoryRawMaterialQualityPerson { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialPricingPerson_View> FactoryRawMaterialMng_FactoryRawMaterialPricingPerson_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialQualityPerson_View> FactoryRawMaterialMng_FactoryRawMaterialQualityPerson_View { get; set; }
        public virtual DbSet<FactoryRawMaterialImage> FactoryRawMaterialImage { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialImage_View> FactoryRawMaterialMng_FactoryRawMaterialImage_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialSearch_View> FactoryRawMaterialMng_FactoryRawMaterialSearch_View { get; set; }
        public virtual DbSet<FactoryRawMaterialPaymentTerm> FactoryRawMaterialPaymentTerm { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SupplierPaymentTerm_View> FactoryRawMaterialMng_SupplierPaymentTerm_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SupportSupplierPaymentTerm_View> FactoryRawMaterialMng_SupportSupplierPaymentTerm_View { get; set; }
        public virtual DbSet<SubSupplierContract> SubSupplierContract { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SubSupplierContract_View> FactoryRawMaterialMng_SubSupplierContract_View { get; set; }
        public virtual DbSet<SupportMng_SubSupplierDocumentType_View> SupportMng_SubSupplierDocumentType_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SupplierContactQuickInfo_View> FactoryRawMaterialMng_SupplierContactQuickInfo_View { get; set; }
        public virtual DbSet<SupplierContactQuickInfo> SupplierContactQuickInfo { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SupplierManager_View> FactoryRawMaterialMng_SupplierManager_View { get; set; }
        public virtual DbSet<SupplierManager> SupplierManager { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SupplierDirector_View> FactoryRawMaterialMng_SupplierDirector_View { get; set; }
        public virtual DbSet<SupplierDirector> SupplierDirector { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SupplierSampleTechnical_View> FactoryRawMaterialMng_SupplierSampleTechnical_View { get; set; }
        public virtual DbSet<SupplierSampleTechnical> SupplierSampleTechnical { get; set; }
        public virtual DbSet<SupportMng_ProductGroup_View> SupportMng_ProductGroup_View { get; set; }
        public virtual DbSet<FactoryRawMaterialProductGroupDTO> FactoryRawMaterialProductGroupDTO { get; set; }
        public virtual DbSet<FactoryRawMaterial_FactoryRawMaterialProductGroupDTO_View> FactoryRawMaterial_FactoryRawMaterialProductGroupDTO_View { get; set; }
        public virtual DbSet<MaterialsPrice> MaterialsPrice { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_MaterialsPrice_View> FactoryRawMaterialMng_MaterialsPrice_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_SearchProductionItem_View> FactoryRawMaterialMng_SearchProductionItem_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_StatusMaterialsPrice_View> FactoryRawMaterialMng_StatusMaterialsPrice_View { get; set; }
        public virtual DbSet<MaterialPriceHistory> MaterialPriceHistory { get; set; }
        public virtual DbSet<FactoryRawMaterial_UnitPriceMaterials_View> FactoryRawMaterial_UnitPriceMaterials_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialTurnover_View> FactoryRawMaterialMng_FactoryRawMaterialTurnover_View { get; set; }
        public virtual DbSet<PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View> PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View { get; set; }
        public virtual DbSet<PurchasingCalendarMng_PurchasingCalendarUser_View> PurchasingCalendarMng_PurchasingCalendarUser_View { get; set; }
        public virtual DbSet<PurchasingCalendarMng_PurchasingCalendarAppointment_View> PurchasingCalendarMng_PurchasingCalendarAppointment_View { get; set; }
        public virtual DbSet<FactoryRawMaterialBusinessCard> FactoryRawMaterialBusinessCard { get; set; }
        public virtual DbSet<SupportMng_User2_View> SupportMng_User2_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialBusinessCard_View> FactoryRawMaterialMng_FactoryRawMaterialBusinessCard_View { get; set; }
        public virtual DbSet<FactoryRawMaterialGallery> FactoryRawMaterialGallery { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_FactoryRawMaterialGallery_View> FactoryRawMaterialMng_FactoryRawMaterialGallery_View { get; set; }
        public virtual DbSet<FactoryRawMaterialMng_MaterialPriceHistory_View> FactoryRawMaterialMng_MaterialPriceHistory_View { get; set; }
    
        public virtual ObjectResult<FactoryRawMaterialMng_FactoryRawMaterialSearch_View> FactoryRawMaterialMng_function_FactoryRawMaterial(Nullable<int> companyID, string factoryRawMaterialUD, string factoryRawMaterialOfficialNM, string factoryRawMaterialShortNM, Nullable<int> locationID, Nullable<int> keyRawMaterialID, string contactPerson, Nullable<bool> isPotential, string updatorName, string sortedBy, string sortedDirection)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var factoryRawMaterialUDParameter = factoryRawMaterialUD != null ?
                new ObjectParameter("FactoryRawMaterialUD", factoryRawMaterialUD) :
                new ObjectParameter("FactoryRawMaterialUD", typeof(string));
    
            var factoryRawMaterialOfficialNMParameter = factoryRawMaterialOfficialNM != null ?
                new ObjectParameter("FactoryRawMaterialOfficialNM", factoryRawMaterialOfficialNM) :
                new ObjectParameter("FactoryRawMaterialOfficialNM", typeof(string));
    
            var factoryRawMaterialShortNMParameter = factoryRawMaterialShortNM != null ?
                new ObjectParameter("FactoryRawMaterialShortNM", factoryRawMaterialShortNM) :
                new ObjectParameter("FactoryRawMaterialShortNM", typeof(string));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("LocationID", locationID) :
                new ObjectParameter("LocationID", typeof(int));
    
            var keyRawMaterialIDParameter = keyRawMaterialID.HasValue ?
                new ObjectParameter("KeyRawMaterialID", keyRawMaterialID) :
                new ObjectParameter("KeyRawMaterialID", typeof(int));
    
            var contactPersonParameter = contactPerson != null ?
                new ObjectParameter("ContactPerson", contactPerson) :
                new ObjectParameter("ContactPerson", typeof(string));
    
            var isPotentialParameter = isPotential.HasValue ?
                new ObjectParameter("IsPotential", isPotential) :
                new ObjectParameter("IsPotential", typeof(bool));
    
            var updatorNameParameter = updatorName != null ?
                new ObjectParameter("UpdatorName", updatorName) :
                new ObjectParameter("UpdatorName", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryRawMaterialMng_FactoryRawMaterialSearch_View>("FactoryRawMaterialMng_function_FactoryRawMaterial", companyIDParameter, factoryRawMaterialUDParameter, factoryRawMaterialOfficialNMParameter, factoryRawMaterialShortNMParameter, locationIDParameter, keyRawMaterialIDParameter, contactPersonParameter, isPotentialParameter, updatorNameParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryRawMaterialMng_FactoryRawMaterialSearch_View> FactoryRawMaterialMng_function_FactoryRawMaterial(Nullable<int> companyID, string factoryRawMaterialUD, string factoryRawMaterialOfficialNM, string factoryRawMaterialShortNM, Nullable<int> locationID, Nullable<int> keyRawMaterialID, string contactPerson, Nullable<bool> isPotential, string updatorName, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var factoryRawMaterialUDParameter = factoryRawMaterialUD != null ?
                new ObjectParameter("FactoryRawMaterialUD", factoryRawMaterialUD) :
                new ObjectParameter("FactoryRawMaterialUD", typeof(string));
    
            var factoryRawMaterialOfficialNMParameter = factoryRawMaterialOfficialNM != null ?
                new ObjectParameter("FactoryRawMaterialOfficialNM", factoryRawMaterialOfficialNM) :
                new ObjectParameter("FactoryRawMaterialOfficialNM", typeof(string));
    
            var factoryRawMaterialShortNMParameter = factoryRawMaterialShortNM != null ?
                new ObjectParameter("FactoryRawMaterialShortNM", factoryRawMaterialShortNM) :
                new ObjectParameter("FactoryRawMaterialShortNM", typeof(string));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("LocationID", locationID) :
                new ObjectParameter("LocationID", typeof(int));
    
            var keyRawMaterialIDParameter = keyRawMaterialID.HasValue ?
                new ObjectParameter("KeyRawMaterialID", keyRawMaterialID) :
                new ObjectParameter("KeyRawMaterialID", typeof(int));
    
            var contactPersonParameter = contactPerson != null ?
                new ObjectParameter("ContactPerson", contactPerson) :
                new ObjectParameter("ContactPerson", typeof(string));
    
            var isPotentialParameter = isPotential.HasValue ?
                new ObjectParameter("IsPotential", isPotential) :
                new ObjectParameter("IsPotential", typeof(bool));
    
            var updatorNameParameter = updatorName != null ?
                new ObjectParameter("UpdatorName", updatorName) :
                new ObjectParameter("UpdatorName", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryRawMaterialMng_FactoryRawMaterialSearch_View>("FactoryRawMaterialMng_function_FactoryRawMaterial", mergeOption, companyIDParameter, factoryRawMaterialUDParameter, factoryRawMaterialOfficialNMParameter, factoryRawMaterialShortNMParameter, locationIDParameter, keyRawMaterialIDParameter, contactPersonParameter, isPotentialParameter, updatorNameParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<FactoryRawMaterialMng_SearchProductionItem_View> FactoryRawMaterial_function_GetProductionItemMaterial(string searchProductionItem, string searchProductionItemUD)
        {
            var searchProductionItemParameter = searchProductionItem != null ?
                new ObjectParameter("searchProductionItem", searchProductionItem) :
                new ObjectParameter("searchProductionItem", typeof(string));
    
            var searchProductionItemUDParameter = searchProductionItemUD != null ?
                new ObjectParameter("searchProductionItemUD", searchProductionItemUD) :
                new ObjectParameter("searchProductionItemUD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryRawMaterialMng_SearchProductionItem_View>("FactoryRawMaterial_function_GetProductionItemMaterial", searchProductionItemParameter, searchProductionItemUDParameter);
        }
    
        public virtual ObjectResult<FactoryRawMaterialMng_SearchProductionItem_View> FactoryRawMaterial_function_GetProductionItemMaterial(string searchProductionItem, string searchProductionItemUD, MergeOption mergeOption)
        {
            var searchProductionItemParameter = searchProductionItem != null ?
                new ObjectParameter("searchProductionItem", searchProductionItem) :
                new ObjectParameter("searchProductionItem", typeof(string));
    
            var searchProductionItemUDParameter = searchProductionItemUD != null ?
                new ObjectParameter("searchProductionItemUD", searchProductionItemUD) :
                new ObjectParameter("searchProductionItemUD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FactoryRawMaterialMng_SearchProductionItem_View>("FactoryRawMaterial_function_GetProductionItemMaterial", mergeOption, searchProductionItemParameter, searchProductionItemUDParameter);
        }
    }
}
