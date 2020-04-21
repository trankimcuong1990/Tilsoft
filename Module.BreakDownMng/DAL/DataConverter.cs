using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.BreakDownMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<BreakdownMng_Breakdown_View, DTO.BreakdownDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.BreakdownCategoryOptionDTOs, o => o.MapFrom(s => s.BreakdownMng_BreakdownCategoryOption_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_BreakdownCategory_View, DTO.BreakdownCategoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_BreakdownCategoryOption_View, DTO.BreakdownCategoryOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.BreakdownCategoryOptionDetailDTOs, o => o.MapFrom(s => s.BreakdownMng_BreakdownCategoryOptionDetail_View))
                    .ForMember(d => d.BreakdownCategoryOptionImageDTOs, o => o.MapFrom(s => s.BreakdownMng_BreakdownCategoryOptionImage_View))
                    .ForMember(d => d.BreakdownCategoryOptionGroupDTOs, o => o.MapFrom(s => s.BreakdownMng_BreakdownCategoryOptionGroup_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_BreakdownCategoryOptionDetail_View, DTO.BreakdownCategoryOptionDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_BreakdownCategoryOptionImage_View, DTO.BreakdownCategoryOptionImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_Model_View, DTO.ModelDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_SampleProduct_View, DTO.SampleProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_BreakDownMng_SearchView, DTO.BreakDownSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_AvailableOptionByArticleCode_View, DTO.AvailableOptionByArticleCodeDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_LoadDetailCalculationPriceUSD_View, DTO.CaculatePriceUSD>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_BreakdownPriceHistory_View, DTO.BreakdownPriceHistoryDTO>()
                  .IgnoreAllNonExisting()               
                  .ForMember(d=>d.BreakdownPriceHistoryDetailDTOs, o => o.MapFrom(s=>s.BreakDownMng_BreakdownPriceHistoryDetail_View))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_BreakdownPriceHistoryDetail_View, DTO.BreakdownPriceHistoryDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_LoadDetailCalculationPriceMangementCost_View, DTO.CalculationPriceMangementCost>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_SupportModel_View, DTO.SupportModelDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_SupportSampleProduct_View, DTO.SupportSampleProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_BreakdownPriceList_View, DTO.BreakdownPriceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));             

                AutoMapper.Mapper.CreateMap<BreakDownMng_function_PriceDetail_Result, DTO.BreakDownPriceDefaultDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakDownMng_function_BreakdownCategoryOption_Result, DTO.BreakDownBreakdownCategoryDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));                

                AutoMapper.Mapper.CreateMap<BreakdownMng_BreakdownCategoryOptionGroup_View, DTO.BreakdownCategoryOptionGroupDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_Factory_View, DTO.FactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_ClientSpecialPackagingMethod_View, DTO.ClientSpecialPackagingMethodDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_ProductionItemCostType_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_ProductionItemMaterialType_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BreakdownCategoryOptionDTO, BreakdownCategoryOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BreakdownCategoryOptionID, o => o.Ignore())
                    .ForMember(d => d.CompanyID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.IsConfirmed, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.BreakdownCategoryOptionDetail, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BreakdownCategoryOptionDetailDTO, BreakdownCategoryOptionDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BreakdownCategoryOptionDetailID, o => o.Ignore())
                    //.ForMember(d => d.BreakdownCategoryOptionGroupID, o => o.Ignore())
                    .ForMember(d => d.BreakdownCategoryOptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BreakdownPriceHistoryDTO, BreakdownPriceHistory>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.BreakdownPriceHistoryID, o => o.Ignore())
                   .ForMember(d => d.BreakdownPriceHistoryDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BreakdownPriceHistoryDetailDTO, BreakdownPriceHistoryDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BreakdownPriceHistoryDetailID, o => o.Ignore());                

                //
                // option converter definition
                //
                AutoMapper.Mapper.CreateMap<SupportMng_ModelMaterial_View, DTO.MaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelMaterialType_View, DTO.MaterialTypeOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelMaterialColor_View, DTO.MaterialColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelFrameMaterial_View, DTO.FrameMaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelFrameMaterialColor_View, DTO.FrameMaterialColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelSubMaterial_View, DTO.SubMaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelSubMaterialColor_View, DTO.SubMaterialColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelCushionType_View, DTO.CushionTypeOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelBackCushion_View, DTO.BackCushionOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelSeatCushion_View, DTO.SeatCushionOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ModelCushionColor_View, DTO.CushionColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_PackagingMethod_View, DTO.PackagingMethodOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BreakdownMng_FSCType_View, DTO.FSCTypeOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.BreakdownDTO DB2DTO_BreakdownDTO(BreakdownMng_Breakdown_View dbItem)
        {
            return AutoMapper.Mapper.Map<BreakdownMng_Breakdown_View, DTO.BreakdownDTO>(dbItem);
        }

        public DTO.BreakdownCategoryOptionDTO DB2DTO_BreakdownCategoryOptionDTO(BreakdownMng_BreakdownCategoryOption_View dbItem)
        {
            return AutoMapper.Mapper.Map<BreakdownMng_BreakdownCategoryOption_View, DTO.BreakdownCategoryOptionDTO>(dbItem);
        }

        public DTO.BreakdownCategoryOptionGroupDTO DB2DTO_BreakdownCategoryOptionGroupDTO(BreakdownMng_BreakdownCategoryOptionGroup_View dbItem)
        {
            return AutoMapper.Mapper.Map<BreakdownMng_BreakdownCategoryOptionGroup_View, DTO.BreakdownCategoryOptionGroupDTO>(dbItem);
        }

        public List<DTO.BreakdownCategoryDTO> DB2DTO_BreakdownCategoryDTOList(List<BreakdownMng_BreakdownCategory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_BreakdownCategory_View>, List<DTO.BreakdownCategoryDTO>>(dbItems);
        }

        public DTO.ModelDTO DB2DTO_ModelDTO(DAL.BreakDownMng_Model_View dbItem)
        {
            return AutoMapper.Mapper.Map<BreakDownMng_Model_View, DTO.ModelDTO>(dbItem);
        }

        public List<DTO.AvailableOptionByArticleCodeDTO> DB2DTO_AvailableOptionByArticleCodeList(List<DAL.BreakdownMng_AvailableOptionByArticleCode_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_AvailableOptionByArticleCode_View>, List<DTO.AvailableOptionByArticleCodeDTO>>(dbItems);
        }

        public DTO.SampleProductDTO DB2DTO_SampleProductDTO(BreakdownMng_SampleProduct_View dbItem)
        {
            return AutoMapper.Mapper.Map<BreakdownMng_SampleProduct_View, DTO.SampleProductDTO>(dbItem);
        }
        public List<DTO.BreakDownSearchDTO> DB2DTO_BreakDownSearch(List<BreakDownMng_BreakDownMng_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_BreakDownMng_SearchView>, List<DTO.BreakDownSearchDTO>>(dbItems);
        }
        public List<DTO.CaculatePriceUSD> DB2DTO_ListCaculate(List<BreakDownMng_LoadDetailCalculationPriceUSD_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_LoadDetailCalculationPriceUSD_View>, List<DTO.CaculatePriceUSD>>(dbItems);
        }
        public List<DTO.CalculationPriceMangementCost> DB2DTO_ListCalculationPriceMangementCost(List<BreakdownMng_LoadDetailCalculationPriceMangementCost_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_LoadDetailCalculationPriceMangementCost_View>, List<DTO.CalculationPriceMangementCost>>(dbItems);
        }


        public List<DTO.SupportModelDTO> DB2DTO_SupportModel(List<BreakDownMng_SupportModel_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_SupportModel_View>, List<DTO.SupportModelDTO>>(dbItem);
        }

        public List<DTO.SupportSampleProductDTO> DB2DTO_SupportSampleProduct(List<BreakDownMng_SupportSampleProduct_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_SupportSampleProduct_View>, List<DTO.SupportSampleProductDTO>>(dbItem);
        }

        public List<DTO.ProductionItemDTO> DB2DTO_ProductionItemCostTypeDTOList(List<BreakdownMng_ProductionItemCostType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_ProductionItemCostType_View>, List<DTO.ProductionItemDTO>>(dbItems);
        }
        public List<DTO.ProductionItemDTO> DB2DTO_ProductionItemMaterialTypeDTOList(List<BreakdownMng_ProductionItemMaterialType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_ProductionItemMaterialType_View>, List<DTO.ProductionItemDTO>>(dbItems);
        }

        public List<DTO.BreakdownPriceDTO> DB2DTO_BreakdownPriceDTOList(List<BreakdownMng_BreakdownPriceList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_BreakdownPriceList_View>, List<DTO.BreakdownPriceDTO>>(dbItems);
        }    
        public List<DTO.BreakDownPriceDefaultDTO> DB2DTO_BreakDownPriceDefaultDTOs(List<BreakDownMng_function_PriceDetail_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_function_PriceDetail_Result>, List<DTO.BreakDownPriceDefaultDTO>>(dbItems);
        }
        public List<DTO.BreakDownBreakdownCategoryDTO> DB2DTO_BreakDownBreakdownCategoryDTOs(List<BreakDownMng_function_BreakdownCategoryOption_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_function_BreakdownCategoryOption_Result>, List<DTO.BreakDownBreakdownCategoryDTO>>(dbItems);
        }
        public List<DTO.BreakdownPriceHistoryDTO> DB2DTO_BreakdownPriceHistorys(List<BreakDownMng_BreakdownPriceHistory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakDownMng_BreakdownPriceHistory_View>, List<DTO.BreakdownPriceHistoryDTO>>(dbItems);
        }

        public void DTO2DB_BreakdownPriceHistory(DTO.BreakdownPriceHistoryDTO dtoItem, ref BreakdownPriceHistory dbItem, int userId)
        {
            // map category
            AutoMapper.Mapper.Map<DTO.BreakdownPriceHistoryDTO, BreakdownPriceHistory>(dtoItem, dbItem);
            if (userId > 0)
            {
                dbItem.UpdatedBy = userId;
                dbItem.UpdatedDate = DateTime.Now;
            }

            // map category detail
            if (dtoItem.BreakdownPriceHistoryDetailDTOs != null)
            {
                //không có trường hợp xóa
                //foreach (BreakdownCategoryOptionDetail dbDetail in dbItem.BreakdownPriceHistoryDetail.ToArray())
                //{
                //    if (!dtoItem.BreakdownCategoryOptionDetailDTOs.Select(o => o.BreakdownCategoryOptionDetailID).Contains(dbDetail.BreakdownCategoryOptionDetailID))
                //    {
                //        dbItem.BreakdownCategoryOptionDetail.Remove(dbDetail);
                //    }
                //}
                foreach (DTO.BreakdownPriceHistoryDetailDTO dtoDetail in dtoItem.BreakdownPriceHistoryDetailDTOs)
                {
                    BreakdownPriceHistoryDetail dbDetail;
                    if (dtoDetail.BreakdownPriceHistoryDetailID <= 0)
                    {
                        dbDetail = new BreakdownPriceHistoryDetail();
                        dbItem.BreakdownPriceHistoryDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.BreakdownPriceHistoryDetail.FirstOrDefault(o => o.BreakdownPriceHistoryDetailID == dtoDetail.BreakdownPriceHistoryDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BreakdownPriceHistoryDetailDTO, BreakdownPriceHistoryDetail>(dtoDetail, dbDetail);
                    }
                }
            }
        }

        public void DTO2DB_BreakdownCategoryOption(DTO.BreakdownCategoryOptionDTO dtoItem, ref BreakdownCategoryOption dbItem, string TmpPath, int userId)
        {
            // map category
            AutoMapper.Mapper.Map<DTO.BreakdownCategoryOptionDTO, BreakdownCategoryOption>(dtoItem, dbItem);
            if (userId > 0)
            {
                dbItem.UpdatedBy = userId;
                dbItem.UpdatedDate = DateTime.Now;
            }            

            // map category detail
            if (dtoItem.BreakdownCategoryOptionDetailDTOs != null)
            {
                foreach (BreakdownCategoryOptionDetail dbDetail in dbItem.BreakdownCategoryOptionDetail.ToArray())
                {
                    if (!dtoItem.BreakdownCategoryOptionDetailDTOs.Select(o => o.BreakdownCategoryOptionDetailID).Contains(dbDetail.BreakdownCategoryOptionDetailID))
                    {
                        dbItem.BreakdownCategoryOptionDetail.Remove(dbDetail);
                    }
                }
                foreach (DTO.BreakdownCategoryOptionDetailDTO dtoDetail in dtoItem.BreakdownCategoryOptionDetailDTOs)
                {
                    BreakdownCategoryOptionDetail dbDetail;
                    if (dtoDetail.BreakdownCategoryOptionDetailID <= 0)
                    {
                        dbDetail = new BreakdownCategoryOptionDetail();
                        dbItem.BreakdownCategoryOptionDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.BreakdownCategoryOptionDetail.FirstOrDefault(o => o.BreakdownCategoryOptionDetailID == dtoDetail.BreakdownCategoryOptionDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BreakdownCategoryOptionDetailDTO, BreakdownCategoryOptionDetail>(dtoDetail, dbDetail);
                    }
                }
            }

            // map category group
            if (dtoItem.BreakdownCategoryOptionGroupDTOs != null)
            {
                foreach (BreakdownCategoryOptionGroup dbGroup in dbItem.BreakdownCategoryOptionGroup.ToArray())
                {
                    if (!dtoItem.BreakdownCategoryOptionGroupDTOs.Select(o => o.BreakdownCategoryOptionGroupID).Contains(dbGroup.BreakdownCategoryOptionGroupID))
                    {
                        foreach (BreakdownCategoryOptionDetail dbDetail in dbGroup.BreakdownCategoryOptionDetail.ToArray())
                        {
                            dbGroup.BreakdownCategoryOptionDetail.Remove(dbDetail);
                        }
                        dbItem.BreakdownCategoryOptionGroup.Remove(dbGroup);
                    }
                }
                foreach (DTO.BreakdownCategoryOptionGroupDTO dtoGroup in dtoItem.BreakdownCategoryOptionGroupDTOs)
                {
                    BreakdownCategoryOptionGroup dbGroup;
                    if (dtoGroup.BreakdownCategoryOptionGroupID <= 0)
                    {
                        dbGroup = new BreakdownCategoryOptionGroup();
                        dbItem.BreakdownCategoryOptionGroup.Add(dbGroup);
                        foreach (BreakdownCategoryOptionDetail dbDetail in dbItem.BreakdownCategoryOptionDetail)
                        {
                            if (dbDetail.BreakdownCategoryOptionGroupID == dtoGroup.BreakdownCategoryOptionGroupID)
                            {
                                dbGroup.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.BreakdownCategoryOptionGroupID = null;
                            }
                        }
                    }
                    else
                    {
                        dbGroup = dbItem.BreakdownCategoryOptionGroup.FirstOrDefault(o => o.BreakdownCategoryOptionGroupID == dtoGroup.BreakdownCategoryOptionGroupID);
                    }

                    if (dbGroup != null)
                    {
                        dbGroup.BreakdownCategoryOptionGroupNM = dtoGroup.BreakdownCategoryOptionGroupNM;
                        dbGroup.BreakdownCategoryOptionGroupNMEN = dtoGroup.BreakdownCategoryOptionGroupNMEN;
                        dbGroup.Quantity = dtoGroup.Quantity;
                    }
                }
            }
        }

        //
        // option mapping
        //
        public List<DTO.MaterialOptionDTO> DB2DTO_MaterialOptionDTOList(List<SupportMng_ModelMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelMaterial_View>, List<DTO.MaterialOptionDTO>>(dbItems);
        }
        public List<DTO.MaterialTypeOptionDTO> DB2DTO_MaterialTypeOptionDTOList(List<SupportMng_ModelMaterialType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelMaterialType_View>, List<DTO.MaterialTypeOptionDTO>>(dbItems);
        }
        public List<DTO.MaterialColorOptionDTO> DB2DTO_MaterialColorOptionDTOList(List<SupportMng_ModelMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelMaterialColor_View>, List<DTO.MaterialColorOptionDTO>>(dbItems);
        }
        public List<DTO.FrameMaterialOptionDTO> DB2DTO_FrameMaterialOptionDTOList(List<SupportMng_ModelFrameMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelFrameMaterial_View>, List<DTO.FrameMaterialOptionDTO>>(dbItems);
        }
        public List<DTO.FrameMaterialColorOptionDTO> DB2DTO_FrameMaterialColorOptionDTOList(List<SupportMng_ModelFrameMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelFrameMaterialColor_View>, List<DTO.FrameMaterialColorOptionDTO>>(dbItems);
        }
        public List<DTO.SubMaterialOptionDTO> DB2DTO_SubMaterialOptionDTOList(List<SupportMng_ModelSubMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSubMaterial_View>, List<DTO.SubMaterialOptionDTO>>(dbItems);
        }
        public List<DTO.SubMaterialColorOptionDTO> DB2DTO_SubMaterialColorOptionDTOList(List<SupportMng_ModelSubMaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSubMaterialColor_View>, List<DTO.SubMaterialColorOptionDTO>>(dbItems);
        }
        public List<DTO.CushionTypeOptionDTO> DB2DTO_CushionTypeOptionDTOList(List<SupportMng_ModelCushionType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelCushionType_View>, List<DTO.CushionTypeOptionDTO>>(dbItems);
        }
        public List<DTO.BackCushionOptionDTO> DB2DTO_BackCushionOptionDTOList(List<SupportMng_ModelBackCushion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelBackCushion_View>, List<DTO.BackCushionOptionDTO>>(dbItems);
        }
        public List<DTO.SeatCushionOptionDTO> DB2DTO_SeatCushionOptionDTOList(List<SupportMng_ModelSeatCushion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelSeatCushion_View>, List<DTO.SeatCushionOptionDTO>>(dbItems);
        }
        public List<DTO.CushionColorOptionDTO> DB2DTO_CushionColorOptionDTOList(List<SupportMng_ModelCushionColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ModelCushionColor_View>, List<DTO.CushionColorOptionDTO>>(dbItems);
        }
        public List<DTO.PackagingMethodOptionDTO> DB2DTO_PackagingMethodOptionDTOList(List<BreakdownMng_PackagingMethod_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_PackagingMethod_View>, List<DTO.PackagingMethodOptionDTO>>(dbItems);
        }
        public List<DTO.FSCTypeOptionDTO> DB2DTO_FSCTypeOptionDTOList(List<BreakdownMng_FSCType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_FSCType_View>, List<DTO.FSCTypeOptionDTO>>(dbItems);
        }
        public List<DTO.FactoryDTO> DB2DTO_FactoryDTOList(List<BreakdownMng_Factory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_Factory_View>, List<DTO.FactoryDTO>>(dbItems);
        }
        public List<DTO.ClientSpecialPackagingMethodDTO> DB2DTO_ClientSpecialPackagingMethodDTOList(List<BreakdownMng_ClientSpecialPackagingMethod_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownMng_ClientSpecialPackagingMethod_View>, List<DTO.ClientSpecialPackagingMethodDTO>>(dbItems);
        }
    }
}
