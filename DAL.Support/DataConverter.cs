using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.Support
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SupportMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Purchasing fix price
                AutoMapper.Mapper.CreateMap<Shared_ConfiguredPurchasingPriceModelConfirmedFixedPrice_View, DTO.Support.ConfiguredPurchasingPriceModelConfirmedFixedPrice>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Purchasing config price
                AutoMapper.Mapper.CreateMap<Shared_ConfiguredPurchasingPriceModelConfirmedPriceConfiguration_View, DTO.Support.ConfiguredPurchasingPriceModelConfirmedPriceConfiguration>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //ClientEstimatedAdditionalCost
                AutoMapper.Mapper.CreateMap<SupportMng_ClientEstimatedAdditionalCost_View, DTO.Support.ClientEstimatedAdditionalCostDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //company 3
                AutoMapper.Mapper.CreateMap<SupportMng_InternalCompany3_View, DTO.Support.InternalCompany3>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //product element
                AutoMapper.Mapper.CreateMap<SupportMng_ProductElement_View, DTO.Support.ProductElement>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //model price config
                AutoMapper.Mapper.CreateMap<SupportMng_ModelPriceConfigurationDetail_View, DTO.Support.ModelPriceConfigurationDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //model fix price
                AutoMapper.Mapper.CreateMap<SupportMng_ModelFixPriceConfiguration_View, DTO.Support.ModelFixPriceConfiguration>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //loading plan
                AutoMapper.Mapper.CreateMap<SupportMng_LoadingPlan_View, DTO.Support.LoadingPlan>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                //sale order
                AutoMapper.Mapper.CreateMap<SupportMng_SaleOrder_View, DTO.Support.SaleOrder>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                // product type
                AutoMapper.Mapper.CreateMap<List_ProductType_View, DTO.Support.ProductType>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product group 
                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.Support.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // packaging method
                AutoMapper.Mapper.CreateMap<List_PackagingMethod_View, DTO.Support.PackagingMethod>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model
                AutoMapper.Mapper.CreateMap<List_Model_View, DTO.Support.Model>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model material
                AutoMapper.Mapper.CreateMap<SupportMng_ModelMaterial_View, DTO.Support.ModelMaterialOption>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model material type
                AutoMapper.Mapper.CreateMap<SupportMng_ModelMaterialType_View, DTO.Support.ModelMaterialTypeOption>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model material color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelMaterialColor_View, DTO.Support.ModelMaterialColorOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model frame material
                AutoMapper.Mapper.CreateMap<SupportMng_ModelFrameMaterial_View, DTO.Support.ModelFrameMaterialOption>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model frame material color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelFrameMaterialColor_View, DTO.Support.ModelFrameMaterialColorOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model sub material
                AutoMapper.Mapper.CreateMap<SupportMng_ModelSubMaterial_View, DTO.Support.ModelSubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model sub material color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelSubMaterialColor_View, DTO.Support.ModelSubMaterialColorOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model cushion
                AutoMapper.Mapper.CreateMap<SupportMng_ModelCushion_View, DTO.Support.ModelCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model cushion type
                AutoMapper.Mapper.CreateMap<SupportMng_ModelCushionType_View, DTO.Support.ModelCushionTypeOption>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model cushion color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelCushionColor_View, DTO.Support.ModelCushionColorOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    //.ForMember(d => d.IsStandard, o => o.MapFrom(s => (s.IsStandard.HasValue && s.IsStandard.Value && s.Season == Library.OMSHelper.Helper.GetCurrentSeason()) ? true : false))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model packaging method
                AutoMapper.Mapper.CreateMap<SupportMng_ModelPackagingMethod_View, DTO.Support.ModelPackagingMethodOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // manufacturer country
                AutoMapper.Mapper.CreateMap<List_ManufacturerCountry_View, DTO.Support.ManufacturerCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // country
                AutoMapper.Mapper.CreateMap<List_Country_View, DTO.Support.Country>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // warehouse 
                AutoMapper.Mapper.CreateMap<List_WareHouse_View, DTO.Support.WareHouse>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // warehouse area
                AutoMapper.Mapper.CreateMap<List_WareHouseArea_View, DTO.Support.WareHouseArea>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // delivery term
                AutoMapper.Mapper.CreateMap<List_DeliveryTerm, DTO.Support.DeliveryTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // payment term
                AutoMapper.Mapper.CreateMap<List_PaymentTerm, DTO.Support.PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product status            
                AutoMapper.Mapper.CreateMap<SupportMng_ProductStatus_View, DTO.Support.ProductStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // factory
                AutoMapper.Mapper.CreateMap<List_Factory, DTO.Support.Factory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // saler
                AutoMapper.Mapper.CreateMap<List_Sale, DTO.Support.Saler>()
                    .IgnoreAllNonExisting();
                    //.ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<List_Users, DTO.Support.User>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                // Agents
                AutoMapper.Mapper.CreateMap<List_Agents, DTO.Support.Agents>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client
                AutoMapper.Mapper.CreateMap<List_Client_View, DTO.Support.Client>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // forwarder
                AutoMapper.Mapper.CreateMap<List_Forwarder_View, DTO.Support.Forwarder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // put in production term            
                AutoMapper.Mapper.CreateMap<List_PutInProductionTerm_View, DTO.Support.PutInProductionTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // pod
                AutoMapper.Mapper.CreateMap<List_POD_View, DTO.Support.POD>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // pol
                AutoMapper.Mapper.CreateMap<List_POL_View, DTO.Support.POL>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // material option
                AutoMapper.Mapper.CreateMap<List_MaterialOption_View, DTO.Support.MaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // frame material
                AutoMapper.Mapper.CreateMap<List_FrameMaterial_View, DTO.Support.FrameMaterial>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // confirmed product            
                AutoMapper.Mapper.CreateMap<SupportMng_ConfirmedProduct_View, DTO.Support.ConfirmedProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ImageFile) ? "" : FrameworkSetting.Setting.MediaThumbnailUrl + s.ImageFile)))
                    .ForMember(d => d.ImageFileFullSize, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ImageFileFullSize) ? "" : FrameworkSetting.Setting.MediaFullSizeUrl + s.ImageFileFullSize)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // warehouse import type            
                AutoMapper.Mapper.CreateMap<SupportMng_WarehouseImportType_View, DTO.Support.WarehouseImportType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sparepart            
                AutoMapper.Mapper.CreateMap<SupportMng_Sparepart_View, DTO.Support.Sparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // supplier
                AutoMapper.Mapper.CreateMap<SupportMng_Supplier_View, DTO.Support.Supplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // movement term
                AutoMapper.Mapper.CreateMap<List_MovementTerm_View, DTO.Support.MovementTerm>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // material
                AutoMapper.Mapper.CreateMap<List_Material_View, DTO.Support.Material>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // material type
                AutoMapper.Mapper.CreateMap<List_MaterialType_View, DTO.Support.MaterialType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // material color
                AutoMapper.Mapper.CreateMap<LIst_MaterialColor_View, DTO.Support.MaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // cushion
                AutoMapper.Mapper.CreateMap<LIst_Cushion_View, DTO.Support.Cushion>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // cushion color
                AutoMapper.Mapper.CreateMap<List_CushionColor_View, DTO.Support.CushionColor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // location
                AutoMapper.Mapper.CreateMap<List_Location_View, DTO.Support.Location>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // frame material search result            
                AutoMapper.Mapper.CreateMap<SupportMng_FrameMaterialOptionSearchResult_View, DTO.Support.FrameMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // material search result            
                AutoMapper.Mapper.CreateMap<SupportMng_MaterialOptionSearchResult_View, DTO.Support.MaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sub material search result            
                AutoMapper.Mapper.CreateMap<SupportMng_SubMaterialOptionSearchResult_View, DTO.Support.SubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // cushion search result            
                AutoMapper.Mapper.CreateMap<SupportMng_CushionOptionSearchResult_View, DTO.Support.CushionOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // cushion color option search result            
                AutoMapper.Mapper.CreateMap<SupportMng_CushionColorOptionSearchResult_View, DTO.Support.CushionColorOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // piece model search result            
                AutoMapper.Mapper.CreateMap<SupportMng_PieceModelSearchResult_View, DTO.Support.Model>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // container type
                AutoMapper.Mapper.CreateMap<List_ContainerType_View, DTO.Support.ContainerType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client
                AutoMapper.Mapper.CreateMap<SupportMng_Client_View, DTO.Support.Client>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // frame material color            
                AutoMapper.Mapper.CreateMap<List_FrameMaterialColor_View, DTO.Support.FrameMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sub material
                AutoMapper.Mapper.CreateMap<List_SubMaterial_View, DTO.Support.SubMaterial>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // sub material color
                AutoMapper.Mapper.CreateMap<List_SubMaterialColor_View, DTO.Support.SubMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model sparepart            
                AutoMapper.Mapper.CreateMap<SupportMng_ModelSparepart_View, DTO.Support.ModelSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model season            
                AutoMapper.Mapper.CreateMap<SupportMng_ModelSeason_View, DTO.Support.ModelSeason>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // article code            
                AutoMapper.Mapper.CreateMap<OfferMng_SearchArticleWithModel_View, DTO.Support.ModelSeason>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // cushion type            
                AutoMapper.Mapper.CreateMap<SupportMng_CushionType_View, DTO.Support.CushionType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model frame material            
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneFrameMaterial_View, DTO.Support.ModelFrameMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model frame material color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneFrameMaterialColor_View, DTO.Support.ModelFrameMaterialColorOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model sub material
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneSubMaterial_View, DTO.Support.ModelSubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model sub material color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneSubMaterialColor_View, DTO.Support.ModelSubMaterialColorOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model cushion
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneCushion_View, DTO.Support.ModelCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model cushion color
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneCushionColor_View, DTO.Support.ModelCushionColorOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client city
                AutoMapper.Mapper.CreateMap<SupportMng_ClientCity_View, DTO.Support.ClientCity>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client country
                AutoMapper.Mapper.CreateMap<SupportMng_ClientCountry_View, DTO.Support.ClientCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client group
                AutoMapper.Mapper.CreateMap<SupportMng_ClientGroup_View, DTO.Support.ClientGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client language
                AutoMapper.Mapper.CreateMap<SupportMng_ClientLanguage_View, DTO.Support.Language>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client relationship type
                AutoMapper.Mapper.CreateMap<SupportMng_ClientRelationshipType_View, DTO.Support.ClientRelationshipType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // client relationship type
                AutoMapper.Mapper.CreateMap<SupportMng_ClientType_View, DTO.Support.ClientType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model status
                AutoMapper.Mapper.CreateMap<SupportMng_ModelStatus_View, DTO.Support.ModelStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // office 
                AutoMapper.Mapper.CreateMap<SupportMng_Office_View, DTO.Support.Office>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // user group 
                AutoMapper.Mapper.CreateMap<SupportMng_UserGroup_View, DTO.Support.UserGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // module
                AutoMapper.Mapper.CreateMap<SupportMng_Module_View, DTO.Support.Module>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // back cushion option
                AutoMapper.Mapper.CreateMap<SupportMng_BackCushionOptionSearchResult_View, DTO.Support.BackCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // seat cushion option
                AutoMapper.Mapper.CreateMap<SupportMng_SeatCushionOptionSearchResult_View, DTO.Support.SeatCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model back cushion option
                AutoMapper.Mapper.CreateMap<SupportMng_ModelBackCushion_View, DTO.Support.ModelBackCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model seat cushion option
                AutoMapper.Mapper.CreateMap<SupportMng_ModelSeatCushion_View, DTO.Support.ModelSeatCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model back cushion option
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneBackCushion_View, DTO.Support.ModelBackCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // model seat cushion option
                AutoMapper.Mapper.CreateMap<SupportMng_ModelNoneSeatCushion_View, DTO.Support.ModelSeatCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // quotation offer direction
                AutoMapper.Mapper.CreateMap<SupportMng_QuotationOfferDirection_View, DTO.Support.OfferDirection>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // quotation status
                AutoMapper.Mapper.CreateMap<SupportMng_QuotationStatus_View, DTO.Support.QuotationStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // quotation status
                AutoMapper.Mapper.CreateMap<SupportMng_FSCType_View, DTO.Support.FSCType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // quotation status
                AutoMapper.Mapper.CreateMap<SupportMng_FSCPercent_View, DTO.Support.FSCPercent>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // plc image type 
                AutoMapper.Mapper.CreateMap<SupportMng_PLCImageType_View, DTO.Support.PLCImageType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // plc image type 
                AutoMapper.Mapper.CreateMap<SupportMng_PriceDifference_View, DTO.Support.PriceDifference>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // consignee type 
                AutoMapper.Mapper.CreateMap<SupportMng_ConsigneeType_View, DTO.Support.ConsigneeType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // notify type 
                AutoMapper.Mapper.CreateMap<SupportMng_NotifyType_View, DTO.Support.NotifyType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // order type
                AutoMapper.Mapper.CreateMap<SupportMng_OrderType_View, DTO.Support.OrderType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // item standard 
                AutoMapper.Mapper.CreateMap<SupportMng_ItemStandard_View, DTO.Support.ItemStandard>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_TestSamplingOption_View, DTO.Support.TestSamplingOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_LabelingOption_View, DTO.Support.LabelingOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PackagingOption_View, DTO.Support.PackagingOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_InspectionOption_View, DTO.Support.InspectionOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ShipmentToOption_View, DTO.Support.ShipmentToOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ShipmentTypeOption_View, DTO.Support.ShipmentTypeOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SupplyChainPerson_View, DTO.Support.SupplyChainPerson>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_BackCushionOptionRaw_View, DTO.Support.BackCushionOptionRaw>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SeatCushionOptionRaw_View, DTO.Support.SeatCushionOptionRaw>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_MaterialTypeOptionRaw_View, DTO.Support.MaterialTypeOptionRaw>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_MaterialColorOptionRaw_View, DTO.Support.MaterialColorOptionRaw>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_CushionColorOptionRaw_View, DTO.Support.CushionColorOptionRaw>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_GalleryItemType_View, DTO.Support.GalleryItemType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_SeasonType_View, DTO.Support.SeasonType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SamplePurpose_View, DTO.Support.SamplePurpose>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SampleRemarkStatus_View, DTO.Support.SampleRemarkStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SampleRequestType_View, DTO.Support.SampleRequestType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SampleStatus_View, DTO.Support.SampleStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SampleTransportType_View, DTO.Support.SampleTransportType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_Showroom_View, DTO.Support.Showroom>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ShowroomItem_View, DTO.Support.ShowroomItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ShowroomThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomThumbnailImage) ? "" : FrameworkSetting.Setting.MediaThumbnailUrl + s.ShowroomThumbnailImage)))
                    .ForMember(d => d.ShowroomFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomFullImage) ? "" : FrameworkSetting.Setting.MediaFullSizeUrl + s.ShowroomFullImage)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_FreeToSaleCategory_View, DTO.Support.FreeToSaleCategory>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_WarehouseItem_View, DTO.Support.WarehouseItem>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.SelectedThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SelectedThumbnailImage) ? "" : FrameworkSetting.Setting.MediaThumbnailUrl + s.SelectedThumbnailImage)))
                  .ForMember(d => d.SelectedFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SelectedFullImage) ? "" : FrameworkSetting.Setting.MediaFullSizeUrl + s.SelectedFullImage)))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ShowroomArea_View, DTO.Support.ShowroomArea>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ShowroomAreaByPhysicalQnt_View, DTO.Support.ShowroomAreaByPhysicalQnt>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductCategory_View, DTO.Support.ProductCategory>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SampleOrderStatus_View, DTO.Support.SampleOrderStatus>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // payment type
                AutoMapper.Mapper.CreateMap<List_PaymentType, DTO.Support.PaymentType>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // payment method
                AutoMapper.Mapper.CreateMap<List_PaymentMethod, DTO.Support.PaymentMethod>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Booking
                AutoMapper.Mapper.CreateMap<SupportMng_Booking_View, DTO.Support.Booking>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Fee Type
                AutoMapper.Mapper.CreateMap<List_FeeType_View, DTO.Support.FeeType>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Forwarder
                AutoMapper.Mapper.CreateMap<List_Forwarder_View, DTO.Support.Forwarder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // Report template
                AutoMapper.Mapper.CreateMap<SupportMng_ReportTemplate_View, DTO.Support.ReportTemplate>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //breakdown
                AutoMapper.Mapper.CreateMap<SupportMng_BreakdownCategory_View, DTO.Support.BreakdownCategory>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_BreakdownCategoryOption_View, DTO.Support.BreakdownCategoryOption>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_BreakdownManagementFee_View, DTO.Support.BreakdownManagementFee>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ClientSpecialPackagingMethod_View, DTO.Support.ClientSpecialPackagingMethod>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.Support.ProductElement> DB2DTO_ProductElement(List<SupportMng_ProductElement_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductElement_View>, List<DTO.Support.ProductElement>>(dbItems);
        }

        public List<DTO.Support.ModelPriceConfigurationDetail> DB2DTO_ModelPriceConfigurationDetail(List<SupportMng_ModelPriceConfigurationDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelPriceConfigurationDetail_View>, List<DTO.Support.ModelPriceConfigurationDetail>>(dbItems);
        }

        public List<DTO.Support.ModelFixPriceConfiguration> DB2DTO_ModelFixPriceConfiguration(List<SupportMng_ModelFixPriceConfiguration_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelFixPriceConfiguration_View>, List<DTO.Support.ModelFixPriceConfiguration>>(dbItems);
        }

        public List<DTO.Support.LoadingPlan> DB2DTO_LoadingPlan(List<SupportMng_LoadingPlan_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LoadingPlan_View>, List<DTO.Support.LoadingPlan>>(dbItems);
        }
        
        public List<DTO.Support.SaleOrder> DB2DTO_SaleOrder(List<SupportMng_SaleOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SaleOrder_View>, List<DTO.Support.SaleOrder>>(dbItems);
        }

        public List<DTO.Support.ProductType> DB2DTO_ProductType(List<List_ProductType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_ProductType_View>, List<DTO.Support.ProductType>>(dbItems);
        }

        public List<DTO.Support.Model> DB2DTO_Model(List<List_Model_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Model_View>, List<DTO.Support.Model>>(dbItems);
        }

        public List<DTO.Support.ModelMaterialOption> DB2DTO_ModelMaterialOption(List<SupportMng_ModelMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelMaterial_View>, List<DTO.Support.ModelMaterialOption>>(dbItems);
        }

        public List<DTO.Support.ModelMaterialTypeOption> DB2DTO_ModelMaterialTypeOption(List<SupportMng_ModelMaterialType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelMaterialType_View>, List<DTO.Support.ModelMaterialTypeOption>>(dbItems);
        }

        public List<DTO.Support.ModelMaterialColorOption> DB2DTO_ModelMaterialColorOption(List<SupportMng_ModelMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelMaterialColor_View>, List<DTO.Support.ModelMaterialColorOption>>(dbItems);
        }

        public List<DTO.Support.ModelFrameMaterialOption> DB2DTO_ModelFrameMaterialOption(List<SupportMng_ModelFrameMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelFrameMaterial_View>, List<DTO.Support.ModelFrameMaterialOption>>(dbItems);
        }

        public List<DTO.Support.ModelFrameMaterialColorOption> DB2DTO_ModelFrameMaterialColorOption(List<SupportMng_ModelFrameMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelFrameMaterialColor_View>, List<DTO.Support.ModelFrameMaterialColorOption>>(dbItems);
        }

        public List<DTO.Support.ModelSubMaterialOption> DB2DTO_ModelSubMaterialOption(List<SupportMng_ModelSubMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSubMaterial_View>, List<DTO.Support.ModelSubMaterialOption>>(dbItems);
        }

        public List<DTO.Support.ModelSubMaterialColorOption> DB2DTO_ModelSubMaterialColorOption(List<SupportMng_ModelSubMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSubMaterialColor_View>, List<DTO.Support.ModelSubMaterialColorOption>>(dbItems);
        }

        public List<DTO.Support.ModelCushionOption> DB2DTO_ModelCushionOption(List<SupportMng_ModelCushion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelCushion_View>, List<DTO.Support.ModelCushionOption>>(dbItems);
        }

        public List<DTO.Support.ModelCushionTypeOption> DB2DTO_ModelCushionTypeOption(List<SupportMng_ModelCushionType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelCushionType_View>, List<DTO.Support.ModelCushionTypeOption>>(dbItems);
        }

        public List<DTO.Support.ModelCushionColorOption> DB2DTO_ModelCushionColorOption(List<SupportMng_ModelCushionColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelCushionColor_View>, List<DTO.Support.ModelCushionColorOption>>(dbItems);
        }

        public List<DTO.Support.ModelPackagingMethodOption> DB2DTO_ModelPackagingMethodOption(List<SupportMng_ModelPackagingMethod_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelPackagingMethod_View>, List<DTO.Support.ModelPackagingMethodOption>>(dbItems);
        }

        public List<DTO.Support.ManufacturerCountry> DB2DTO_ManufacturerCountry(List<List_ManufacturerCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_ManufacturerCountry_View>, List<DTO.Support.ManufacturerCountry>>(dbItems);
        }

        public List<DTO.Support.Country> DB2DTO_Country(List<List_Country_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Country_View>, List<DTO.Support.Country>>(dbItems);
        }

        public List<DTO.Support.WareHouse> DB2DTO_WareHouse(List<List_WareHouse_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_WareHouse_View>, List<DTO.Support.WareHouse>>(dbItems);
        }

        public List<DTO.Support.WareHouseArea> DB2DTO_WareHouseArea(List<List_WareHouseArea_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_WareHouseArea_View>, List<DTO.Support.WareHouseArea>>(dbItems);
        }

        public List<DTO.Support.DeliveryTerm> DB2DTO_DeliveryTerm(List<List_DeliveryTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_DeliveryTerm>, List<DTO.Support.DeliveryTerm>>(dbItems);
        }

        public List<DTO.Support.PaymentTerm> DB2DTO_PaymentTerm(List<List_PaymentTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentTerm>, List<DTO.Support.PaymentTerm>>(dbItems);
        }

        public List<DTO.Support.PaymentMethod> DB2DTO_PaymentMethod(List<List_PaymentMethod> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentMethod>, List<DTO.Support.PaymentMethod>>(dbItems);
        }

        public List<DTO.Support.PaymentType> DB2DTO_PaymentType(List<List_PaymentType> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentType>, List<DTO.Support.PaymentType>>(dbItems);
        }

        public List<DTO.Support.Booking> DB2DTO_Booking(List<SupportMng_Booking_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Booking_View>, List<DTO.Support.Booking>>(dbItems);
        }

        public List<DTO.Support.FeeType> DB2DTO_FeeType(List<List_FeeType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_FeeType_View>, List<DTO.Support.FeeType>>(dbItems);
        }

        public List<DTO.Support.ProductStatus> DB2DTO_ProductStatus(List<SupportMng_ProductStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductStatus_View>, List<DTO.Support.ProductStatus>>(dbItems);
        }

        public List<DTO.Support.Factory> DB2DTO_Factory(List<List_Factory> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Factory>, List<DTO.Support.Factory>>(dbItems);
        }

        public List<DTO.Support.Saler> DB2DTO_Saler(List<List_Sale> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Sale>, List<DTO.Support.Saler>>(dbItems);
        }

        public List<DTO.Support.User> DB2DTO_Saler(List<List_Users> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Users>, List<DTO.Support.User>>(dbItems);
        }

        public List<DTO.Support.Agents> DB2DTO_Agents(List<List_Agents> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Agents>, List<DTO.Support.Agents>>(dbItems);
        }
        public List<DTO.Support.Client> DB2DTO_Client(List<List_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Client_View>, List<DTO.Support.Client>>(dbItems);
        }

        public List<DTO.Support.Forwarder> DB2DTO_Forwarder(List<List_Forwarder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Forwarder_View>, List<DTO.Support.Forwarder>>(dbItems);
        }

        public List<DTO.Support.PackagingMethod> DB2DTO_PackagingMethod(List<List_PackagingMethod_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PackagingMethod_View>, List<DTO.Support.PackagingMethod>>(dbItems);
        }

        public List<DTO.Support.PutInProductionTerm> DB2DTO_PutInProductionTerm(List<List_PutInProductionTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PutInProductionTerm_View>, List<DTO.Support.PutInProductionTerm>>(dbItems);
        }

        public List<DTO.Support.POD> DB2DTO_POD(List<List_POD_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_POD_View>, List<DTO.Support.POD>>(dbItems);
        }

        public List<DTO.Support.POL> DB2DTO_POL(List<List_POL_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_POL_View>, List<DTO.Support.POL>>(dbItems);
        }

        public List<DTO.Support.MaterialOption> DB2DTO_MaterialOption(List<List_MaterialOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_MaterialOption_View>, List<DTO.Support.MaterialOption>>(dbItems);
        }

        public List<DTO.Support.FrameMaterial> DB2DTO_FrameMaterial(List<List_FrameMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_FrameMaterial_View>, List<DTO.Support.FrameMaterial>>(dbItems);
        }

        public List<DTO.Support.ConfirmedProduct> DB2DTO_ConfirmedProduct(List<SupportMng_ConfirmedProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ConfirmedProduct_View>, List<DTO.Support.ConfirmedProduct>>(dbItems);
        }

        public List<DTO.Support.WarehouseImportType> DB2DTO_WarehouseImportType(List<SupportMng_WarehouseImportType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_WarehouseImportType_View>, List<DTO.Support.WarehouseImportType>>(dbItems);
        }

        public List<DTO.Support.Sparepart> DB2DTO_Sparepart(List<SupportMng_Sparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Sparepart_View>, List<DTO.Support.Sparepart>>(dbItems);
        }

        public List<DTO.Support.Supplier> DB2DTO_Supplier(List<SupportMng_Supplier_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Supplier_View>, List<DTO.Support.Supplier>>(dbItems);
        }

        public List<DTO.Support.MovementTerm> DB2DTO_MovementTerm(List<List_MovementTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_MovementTerm_View>, List<DTO.Support.MovementTerm>>(dbItems);
        }

        public List<DTO.Support.Material> DB2DTO_Material(List<List_Material_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Material_View>, List<DTO.Support.Material>>(dbItems);
        }

        public List<DTO.Support.MaterialType> DB2DTO_MaterialType(List<List_MaterialType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_MaterialType_View>, List<DTO.Support.MaterialType>>(dbItems);
        }

        public List<DTO.Support.MaterialColor> DB2DTO_MaterialColor(List<LIst_MaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LIst_MaterialColor_View>, List<DTO.Support.MaterialColor>>(dbItems);
        }

        public List<DTO.Support.Cushion> DB2DTO_Cushion(List<LIst_Cushion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LIst_Cushion_View>, List<DTO.Support.Cushion>>(dbItems);
        }

        public List<DTO.Support.CushionColor> DB2DTO_CushionColor(List<List_CushionColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_CushionColor_View>, List<DTO.Support.CushionColor>>(dbItems);
        }

        public List<DTO.Support.Location> DB2DTO_Location(List<List_Location_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_Location_View>, List<DTO.Support.Location>>(dbItems);
        }

        public List<DTO.Support.FrameMaterialOption> DB2DTO_FrameMaterialSearchResult(List<SupportMng_FrameMaterialOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FrameMaterialOptionSearchResult_View>, List<DTO.Support.FrameMaterialOption>>(dbItems);
        }

        public List<DTO.Support.MaterialOption> DB2DTO_MaterialOptionSearchResult(List<SupportMng_MaterialOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialOptionSearchResult_View>, List<DTO.Support.MaterialOption>>(dbItems);
        }

        public List<DTO.Support.SubMaterialOption> DB2DTO_SubMaterialOptionSearchResult(List<SupportMng_SubMaterialOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubMaterialOptionSearchResult_View>, List<DTO.Support.SubMaterialOption>>(dbItems);
        }

        public List<DTO.Support.CushionOption> DB2DTO_CushionOptionSearchResult(List<SupportMng_CushionOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionOptionSearchResult_View>, List<DTO.Support.CushionOption>>(dbItems);
        }

        public List<DTO.Support.CushionColorOption> DB2DTO_CushionColorOptionSearchResult(List<SupportMng_CushionColorOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionColorOptionSearchResult_View>, List<DTO.Support.CushionColorOption>>(dbItems);
        }

        public List<DTO.Support.Model> DB2DTO_PieceModelSearchResult(List<SupportMng_PieceModelSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PieceModelSearchResult_View>, List<DTO.Support.Model>>(dbItems);
        }

        public List<DTO.Support.ContainerType> DB2DTO_ContainerType(List<List_ContainerType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_ContainerType_View>, List<DTO.Support.ContainerType>>(dbItems);
        }

        public List<DTO.Support.Client> DB2DTO_Client(List<SupportMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Client_View>, List<DTO.Support.Client>>(dbItems);
        }

        public List<DTO.Support.FrameMaterialColor> DB2DTO_FrameMaterialColor(List<List_FrameMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_FrameMaterialColor_View>, List<DTO.Support.FrameMaterialColor>>(dbItems);
        }
        public List<DTO.Support.SubMaterial> DB2DTO_SubMaterial(List<List_SubMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_SubMaterial_View>, List<DTO.Support.SubMaterial>>(dbItems);
        }
        public List<DTO.Support.SubMaterialColor> DB2DTO_SubMaterialColor(List<List_SubMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_SubMaterialColor_View>, List<DTO.Support.SubMaterialColor>>(dbItems);
        }

        public List<DTO.Support.ModelSparepart> DB2DTO_ModelSprepart(List<SupportMng_ModelSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSparepart_View>, List<DTO.Support.ModelSparepart>>(dbItems);
        }

        public List<DTO.Support.ModelSeason> DB2DTO_ModelSeason(List<SupportMng_ModelSeason_View> dbItems)
        {
            SupportMng_ModelSeason_View dbitem = new SupportMng_ModelSeason_View();
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSeason_View>, List<DTO.Support.ModelSeason>>(dbItems);
        }

        public List<DTO.Support.ModelSeason> DB2DTO_ArticleCode(List<OfferMng_SearchArticleWithModel_View> dbItems)
        {
            OfferMng_SearchArticleWithModel_View dbitem = new OfferMng_SearchArticleWithModel_View();
            return AutoMapper.Mapper.Map<List<OfferMng_SearchArticleWithModel_View>, List<DTO.Support.ModelSeason>>(dbItems);
        }

        public List<DTO.Support.CushionType> DB2DTO_CushionType(List<SupportMng_CushionType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionType_View>, List<DTO.Support.CushionType>>(dbItems);
        }

        public List<DTO.Support.ProductGroup> DB2DTO_ProductGroup(List<SupportMng_ProductGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.Support.ProductGroup>>(dbItems);
        }

        public List<DTO.Support.ModelFrameMaterialOption> DB2DTO_ModelFrameMaterialOption(List<SupportMng_ModelNoneFrameMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelNoneFrameMaterial_View>, List<DTO.Support.ModelFrameMaterialOption>>(dbItems);
        }

        public List<DTO.Support.ModelFrameMaterialColorOption> DB2DTO_ModelFrameMaterialColorOption(List<SupportMng_ModelNoneFrameMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelNoneFrameMaterialColor_View>, List<DTO.Support.ModelFrameMaterialColorOption>>(dbItems);
        }

        public List<DTO.Support.ModelSubMaterialOption> DB2DTO_ModelSubMaterialOption(List<SupportMng_ModelNoneSubMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelNoneSubMaterial_View>, List<DTO.Support.ModelSubMaterialOption>>(dbItems);
        }

        public List<DTO.Support.ModelSubMaterialColorOption> DB2DTO_ModelSubMaterialColorOption(List<SupportMng_ModelNoneSubMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelNoneSubMaterialColor_View>, List<DTO.Support.ModelSubMaterialColorOption>>(dbItems);
        }

        public List<DTO.Support.ModelCushionOption> DB2DTO_ModelCushionOption(List<SupportMng_ModelNoneCushion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelNoneCushion_View>, List<DTO.Support.ModelCushionOption>>(dbItems);
        }

        public List<DTO.Support.ModelCushionColorOption> DB2DTO_ModelCushionColorOption(List<SupportMng_ModelNoneCushionColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelNoneCushionColor_View>, List<DTO.Support.ModelCushionColorOption>>(dbItems);
        }

        public List<DTO.Support.ClientCity> DB2DTO_ClientCity(List<SupportMng_ClientCity_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientCity_View>, List<DTO.Support.ClientCity>>(dbItems);
        }

        public List<DTO.Support.ClientCountry> DB2DTO_ClientCountry(List<SupportMng_ClientCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientCountry_View>, List<DTO.Support.ClientCountry>>(dbItems);
        }

        public List<DTO.Support.ClientGroup> DB2DTO_ClientGroup(List<SupportMng_ClientGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientGroup_View>, List<DTO.Support.ClientGroup>>(dbItems);
        }

        public List<DTO.Support.Language> DB2DTO_ClientLanguage(List<SupportMng_ClientLanguage_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientLanguage_View>, List<DTO.Support.Language>>(dbItems);
        }

        public List<DTO.Support.ClientRelationshipType> DB2DTO_RelationshipType(List<SupportMng_ClientRelationshipType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientRelationshipType_View>, List<DTO.Support.ClientRelationshipType>>(dbItems);
        }
        public List<DTO.Support.ClientType> DB2DTO_ClientType(List<SupportMng_ClientType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ClientType_View>, List<DTO.Support.ClientType>>(dbItems);
        }

        public List<DTO.Support.FSCType> DB2DTO_FSCType(List<SupportMng_FSCType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FSCType_View>, List<DTO.Support.FSCType>>(dbItems);
        }

        public List<DTO.Support.FSCPercent> DB2DTO_FSCPercent(List<SupportMng_FSCPercent_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_FSCPercent_View>, List<DTO.Support.FSCPercent>>(dbItems);
        }

        public List<DTO.Support.ConsigneeType> DB2DTO_ConsigneeType(List<SupportMng_ConsigneeType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ConsigneeType_View>, List<DTO.Support.ConsigneeType>>(dbItems);
        }

        public List<DTO.Support.NotifyType> DB2DTO_NotifyType(List<SupportMng_NotifyType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_NotifyType_View>, List<DTO.Support.NotifyType>>(dbItems);
        }

        public List<DTO.Support.OrderType> DB2DTO_OrderType(List<SupportMng_OrderType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_OrderType_View>, List<DTO.Support.OrderType>>(dbItems);
        }

        public List<DTO.Support.ItemStandard> DB2DTO_ItemStandard(List<SupportMng_ItemStandard_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ItemStandard_View>, List<DTO.Support.ItemStandard>>(dbItems);
        }

        public List<DTO.Support.TestSamplingOption> DB2DTO_TestSamplingOption(List<SupportMng_TestSamplingOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_TestSamplingOption_View>, List<DTO.Support.TestSamplingOption>>(dbItems);
        }

        public List<DTO.Support.LabelingOption> DB2DTO_LabelingOption(List<SupportMng_LabelingOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_LabelingOption_View>, List<DTO.Support.LabelingOption>>(dbItems);
        }

        public List<DTO.Support.PackagingOption> DB2DTO_PackagingOption(List<SupportMng_PackagingOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PackagingOption_View>, List<DTO.Support.PackagingOption>>(dbItems);
        }

        public List<DTO.Support.InspectionOption> DB2DTO_InspectionOption(List<SupportMng_InspectionOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_InspectionOption_View>, List<DTO.Support.InspectionOption>>(dbItems);
        }

        public List<DTO.Support.ShipmentToOption> DB2DTO_ShipmentToOption(List<SupportMng_ShipmentToOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShipmentToOption_View>, List<DTO.Support.ShipmentToOption>>(dbItems);
        }

        public List<DTO.Support.ShipmentTypeOption> DB2DTO_ShipmentTypeOption(List<SupportMng_ShipmentTypeOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShipmentTypeOption_View>, List<DTO.Support.ShipmentTypeOption>>(dbItems);
        }

        public List<DTO.Support.SupplyChainPerson> DB2DTO_SupplyChainPerson(List<SupportMng_SupplyChainPerson_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SupplyChainPerson_View>, List<DTO.Support.SupplyChainPerson>>(dbItems);
        }

        public List<DTO.Support.BackCushionOptionRaw> DB2DTO_BackCushionOptionRaw(List<SupportMng_BackCushionOptionRaw_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_BackCushionOptionRaw_View>, List<DTO.Support.BackCushionOptionRaw>>(dbItems);
        }
        public List<DTO.Support.SeatCushionOptionRaw> DB2DTO_SeatCushionOptionRaw(List<SupportMng_SeatCushionOptionRaw_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SeatCushionOptionRaw_View>, List<DTO.Support.SeatCushionOptionRaw>>(dbItems);
        }
        public List<DTO.Support.CushionColorOptionRaw> DB2DTO_CushionColorOptionRaw(List<SupportMng_CushionColorOptionRaw_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionColorOptionRaw_View>, List<DTO.Support.CushionColorOptionRaw>>(dbItems);
        }
        public List<DTO.Support.MaterialTypeOptionRaw> DB2DTO_MaterialTypeOptionRaw(List<SupportMng_MaterialTypeOptionRaw_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialTypeOptionRaw_View>, List<DTO.Support.MaterialTypeOptionRaw>>(dbItems);
        }
        public List<DTO.Support.MaterialColorOptionRaw> DB2DTO_MaterialColorOptionRaw(List<SupportMng_MaterialColorOptionRaw_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialColorOptionRaw_View>, List<DTO.Support.MaterialColorOptionRaw>>(dbItems);
        }

        public List<DTO.Support.GalleryItemType> DB2DTO_GalleryItemType(List<List_GalleryItemType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_GalleryItemType_View>, List<DTO.Support.GalleryItemType>>(dbItems);
        }

        public List<DTO.Support.SeasonType> DB2DTO_SeasonType(List<List_SeasonType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_SeasonType_View>, List<DTO.Support.SeasonType>>(dbItems);
        }



        public List<DTO.Support.SamplePurpose> DB2DTO_SamplePurpose(List<SupportMng_SamplePurpose_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SamplePurpose_View>, List<DTO.Support.SamplePurpose>>(dbItems);
        }

        public List<DTO.Support.SampleRemarkStatus> DB2DTO_SampleRemarkStatus(List<SupportMng_SampleRemarkStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleRemarkStatus_View>, List<DTO.Support.SampleRemarkStatus>>(dbItems);
        }

        public List<DTO.Support.SampleRequestType> DB2DTO_SampleRequestType(List<SupportMng_SampleRequestType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleRequestType_View>, List<DTO.Support.SampleRequestType>>(dbItems);
        }

        public List<DTO.Support.SampleStatus> DB2DTO_SampleStatus(List<SupportMng_SampleStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleStatus_View>, List<DTO.Support.SampleStatus>>(dbItems);
        }

        public List<DTO.Support.SampleTransportType> DB2DTO_SampleTransportType(List<SupportMng_SampleTransportType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleTransportType_View>, List<DTO.Support.SampleTransportType>>(dbItems);
        }
        public List<DTO.Support.Showroom> DB2DTO_Showroom(List<SupportMng_Showroom_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Showroom_View>, List<DTO.Support.Showroom>>(dbItems);
        }

        public List<DTO.Support.ShowroomItem> DB2DTO_ShowroomItem(List<SupportMng_ShowroomItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShowroomItem_View>, List<DTO.Support.ShowroomItem>>(dbItems);
        }
        public List<DTO.Support.FreeToSaleCategory> DB2DTO_FreeToSaleCategory(List<List_FreeToSaleCategory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_FreeToSaleCategory_View>, List<DTO.Support.FreeToSaleCategory>>(dbItems);
        }

        public List<DTO.Support.WarehouseItem> DB2DTO_WarehouseItem(List<SupportMng_WarehouseItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_WarehouseItem_View>, List<DTO.Support.WarehouseItem>>(dbItems);
        }

        public List<DTO.Support.ShowroomArea> DB2DTO_ShowroomArea(List<SupportMng_ShowroomArea_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShowroomArea_View>, List<DTO.Support.ShowroomArea>>(dbItems);
        }

        public List<DTO.Support.ShowroomAreaByPhysicalQnt> DB2DTO_ShowroomAreaByPhysicalQnt(List<SupportMng_ShowroomAreaByPhysicalQnt_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ShowroomAreaByPhysicalQnt_View>, List<DTO.Support.ShowroomAreaByPhysicalQnt>>(dbItems);
        }

        public List<DTO.Support.ProductCategory> DB2DTO_ProductCategory(List<SupportMng_ProductCategory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductCategory_View>, List<DTO.Support.ProductCategory>>(dbItems);
        }

        public List<DTO.Support.SampleOrderStatus> DB2DTO_SampleOrderStatus(List<SupportMng_SampleOrderStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SampleOrderStatus_View>, List<DTO.Support.SampleOrderStatus>>(dbItems);
        }

        // Report template
        public List<DTO.Support.ReportTemplate> DB2DTO_ReportTemplate(List<SupportMng_ReportTemplate_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ReportTemplate_View>, List<DTO.Support.ReportTemplate>>(dbItems);
        }
    }
}
