using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.ProductBreakDownPALMng.DAL
{
    internal class DataConverter
    {
        private readonly Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mappingName = "ProductBreakDownPALMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mappingName))
            {
                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownCalculationTypePAL_View, DTO.SupportProductBreakDownCalculationTypePALData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_OptionToGetPricePAL_View, DTO.SupportProductBreakDownOptionPricePALData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_OptionToGetQuantityPAL_View, DTO.SupportProductBreakDownOptionQuantityPALData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownDefaultCategoryPALSearchResult_View, DTO.ProductBreakDownDefaultCategoryPALSearchResultData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownDefaultCategoryPAL_View, DTO.ProductBreakDownDefaultCategoryPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null));

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownDefaultCategoryPALData, ProductBreakDownDefaultCategoryPAL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownDefaultCategoryID, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownPALSearchResult_View, DTO.ProductBreakDownPALSearchResultData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownPAL_View, DTO.ProductBreakDownPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.PriceDate, o => o.ResolveUsing(s => s.PriceDate.HasValue ? s.PriceDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.ProductBreakDownCategoryPAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryPAL_View))
                    .ForMember(d => d.ProductBreakDownCategoryAmountPAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryAmountPAL_View))
                    .ForMember(d => d.ProductBreakDownCategoryPercentPAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryPercentPAL_View));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownCategoryPAL_View, DTO.ProductBreakDownCategoryPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownCategoryImagePAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryImagePAL_View))
                    .ForMember(d => d.ProductBreakDownCategoryTypePAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryTypePAL_View));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownCategoryAmountPAL_View, DTO.ProductBreakDownCategoryAmountPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownCategoryImagePAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryImagePAL_View))
                    .ForMember(d => d.ProductBreakDownCategoryTypePAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryTypePAL_View));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownCategoryPercentPAL_View, DTO.ProductBreakDownCategoryPercentPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownCategoryImagePAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryImagePAL_View))
                    .ForMember(d => d.ProductBreakDownCategoryTypePAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownCategoryTypePAL_View));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownCategoryImagePAL_View, DTO.ProductBreakDownCategoryImagePALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownCategoryTypePAL_View, DTO.ProductBreakDownCategoryTypePALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownDetailPAL, o => o.MapFrom(s => s.ProductBreakDownPAL_ProductBreakDownDetailPAL_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ProductBreakDownDetailPAL_View, DTO.ProductBreakDownDetailPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownPALData, ProductBreakDownPAL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.PriceDate, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownCategoryPAL, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownCategoryPALData, ProductBreakDownCategoryPAL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownCategoryID, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownID, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownCategoryImagePAL, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownCategoryTypePAL, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownCategoryImagePALData, ProductBreakDownCategoryImagePAL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownCategoryImageID, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownCategoryID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownCategoryTypePALData, ProductBreakDownCategoryTypePAL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownCategoryTypeID, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownCategoryID, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownDetailPAL, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownDetailPALData, ProductBreakDownDetailPAL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductBreakDownDetailID, o => o.Ignore())
                    .ForMember(d => d.ProductBreakDownCategoryTypeID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_SupportModelPAL_View, DTO.SupportProductBreakDownPALModelData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_SupportSampleProductPAL_View, DTO.SupportProductBreakDownPALSampleProductData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_SupportClientPAL_View, DTO.SupportProductBreakDownPALClientData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_SupportProductionItem_View, DTO.SupportProductBreakDownPALProductionItemData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_SupportProduct_View, DTO.SupportProductBreakDownPALProductData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_SupportProductBreakDownCategoryPAL_View, DTO.SupportProductBreakDownPALCategoryData>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownPALData, DTO.ProductBreakDownPALData>()
                    .IgnoreAllNonExisting()
                    .ForMember(o => o.ProductBreakDownID, o => o.UseValue(0))
                    .ForMember(o => o.ModelID, o => o.Ignore())
                    .ForMember(o => o.ModelUD, o => o.Ignore())
                    .ForMember(o => o.ModelNM, o => o.Ignore())
                    .ForMember(o => o.ProductID, o => o.Ignore())
                    .ForMember(o => o.ArticleCode, o => o.Ignore())
                    .ForMember(o => o.ArticleDescription, o => o.Ignore())
                    .ForMember(o => o.SampleProductID, o => o.Ignore())
                    .ForMember(o => o.SampleProductUD, o => o.Ignore())
                    .ForMember(o => o.Description, o => o.Ignore())
                    .ForMember(o => o.CartonSize, o => o.Ignore())
                    .ForMember(o => o.CushionDescription, o => o.Ignore())
                    .ForMember(o => o.FrameDescription, o => o.Ignore())
                    .ForMember(o => o.ItemSize, o => o.Ignore())
                    .ForMember(o => o.Remark, o => o.Ignore())
                    .ForMember(o => o.ClientID, o => o.Ignore())
                    .ForMember(o => o.ClientUD, o => o.Ignore())
                    .ForMember(o => o.PriceDate, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.PriceDate) ? s.PriceDate : DateTime.Now.ToString("dd/MM/yyyy")))
                    .ForMember(o => o.ProductBreakDownCategoryAmountPAL, o => o.ResolveUsing(s => s.ProductBreakDownCategoryAmountPAL))
                    .ForMember(o => o.ProductBreakDownCategoryPercentPAL, o => o.ResolveUsing(s => s.ProductBreakDownCategoryPercentPAL))
                    .ForMember(o => o.ProductBreakDownCategoryPAL, o => o.ResolveUsing(s => s.ProductBreakDownCategoryPAL))
                    .ForMember(o => o.UpdatedBy, o => o.Ignore())
                    .ForMember(o => o.UpdatedDate, o => o.Ignore())
                    .ForMember(o => o.ConfirmedBy, o => o.Ignore())
                    .ForMember(o => o.ConfirmedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductBreakDownPAL_ModelDefaultOption_View, DTO.ModelDefaultOption>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            }
        }

        public List<DTO.SupportProductBreakDownCalculationTypePALData> DB2DTO_CalculationTypePAL(List<ProductBreakDownPAL_ProductBreakDownCalculationTypePAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_ProductBreakDownCalculationTypePAL_View>, List<DTO.SupportProductBreakDownCalculationTypePALData>>(dbItem);
        }

        public List<DTO.SupportProductBreakDownOptionPricePALData> DB2DTO_OptionPricePAL(List<ProductBreakDownPAL_OptionToGetPricePAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_OptionToGetPricePAL_View>, List<DTO.SupportProductBreakDownOptionPricePALData>>(dbItem);
        }

        public List<DTO.SupportProductBreakDownOptionQuantityPALData> DB2DTO_OptionQuantityPAL(List<ProductBreakDownPAL_OptionToGetQuantityPAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_OptionToGetQuantityPAL_View>, List<DTO.SupportProductBreakDownOptionQuantityPALData>>(dbItem);
        }

        public List<DTO.ProductBreakDownDefaultCategoryPALSearchResultData> DB2DTO_DefaultPALSearchResult(List<ProductBreakDownPAL_ProductBreakDownDefaultCategoryPALSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_ProductBreakDownDefaultCategoryPALSearchResult_View>, List<DTO.ProductBreakDownDefaultCategoryPALSearchResultData>>(dbItem);
        }

        public DTO.ProductBreakDownDefaultCategoryPALData DB2DTO_DefaultCategoryPAL(ProductBreakDownPAL_ProductBreakDownDefaultCategoryPAL_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductBreakDownPAL_ProductBreakDownDefaultCategoryPAL_View, DTO.ProductBreakDownDefaultCategoryPALData>(dbItem);
        }

        public void DTO2DB_DefaultCategory(DTO.ProductBreakDownDefaultCategoryPALData dtoItem, ref ProductBreakDownDefaultCategoryPAL dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductBreakDownDefaultCategoryPALData, ProductBreakDownDefaultCategoryPAL>(dtoItem, dbItem);
        }

        public List<DTO.ProductBreakDownPALSearchResultData> DB2DTO_ProductBreakDownPALSearchResult(List<ProductBreakDownPAL_ProductBreakDownPALSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_ProductBreakDownPALSearchResult_View>, List<DTO.ProductBreakDownPALSearchResultData>>(dbItem);
        }

        public List<DTO.ProductBreakDownCategoryPALData> DB2DTO_ProductBreakDownCategoryPAL(List<ProductBreakDownPAL_ProductBreakDownCategoryPAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_ProductBreakDownCategoryPAL_View>, List<DTO.ProductBreakDownCategoryPALData>>(dbItem);
        }

        public DTO.ProductBreakDownPALData DB2DTO_ProductBreakDownPAL(ProductBreakDownPAL_ProductBreakDownPAL_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductBreakDownPAL_ProductBreakDownPAL_View, DTO.ProductBreakDownPALData>(dbItem);
        }

        public List<DTO.SupportProductBreakDownPALModelData> DB2DTO_ModelPAL(List<ProductBreakDownPAL_SupportModelPAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_SupportModelPAL_View>, List<DTO.SupportProductBreakDownPALModelData>>(dbItem);
        }

        public List<DTO.SupportProductBreakDownPALSampleProductData> DB2DTO_SamplePAL(List<ProductBreakDownPAL_SupportSampleProductPAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_SupportSampleProductPAL_View>, List<DTO.SupportProductBreakDownPALSampleProductData>>(dbItem);
        }

        public List<DTO.SupportProductBreakDownPALClientData> DB2DTO_ClientPAL(List<ProductBreakDownPAL_SupportClientPAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_SupportClientPAL_View>, List<DTO.SupportProductBreakDownPALClientData>>(dbItem);
        }

        public void DTO2DB_ProductBreakDownPAL2(DTO.ProductBreakDownPALData dtoItem, ref ProductBreakDownPAL dbItem, string tempFile, int userId)
        {
            AutoMapper.Mapper.Map<DTO.ProductBreakDownPALData, ProductBreakDownPAL>(dtoItem, dbItem);

            foreach (var dbCategory in dbItem.ProductBreakDownCategoryPAL.ToArray())
            {
                if (!dtoItem.ProductBreakDownCategoryPAL.Select(o => o.ProductBreakDownCategoryID).Contains(dbCategory.ProductBreakDownCategoryID))
                {
                    foreach (var dbCategoryImage in dbCategory.ProductBreakDownCategoryImagePAL.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbCategoryImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbCategoryImage.FileUD);
                        }

                        dbCategory.ProductBreakDownCategoryImagePAL.Remove(dbCategoryImage);
                    }

                    foreach (var dbCategoryType in dbCategory.ProductBreakDownCategoryTypePAL.ToArray())
                    {
                        foreach (var dbDetail in dbCategoryType.ProductBreakDownDetailPAL.ToArray())
                        {
                            dbCategoryType.ProductBreakDownDetailPAL.Remove(dbDetail);
                        }

                        dbCategory.ProductBreakDownCategoryTypePAL.Remove(dbCategoryType);
                    }

                    dbItem.ProductBreakDownCategoryPAL.Remove(dbCategory);
                }
            }

            foreach (var dtoCategory in dtoItem.ProductBreakDownCategoryPAL)
            {
                if (string.IsNullOrEmpty(dtoCategory.ProductBreakDownCategoryNM))
                {
                    continue;
                }

                if (dtoCategory.ProductBreakDownCalculationTypeID == 1 && dtoCategory.UnitPrice == 0)
                {
                    continue;
                }

                ProductBreakDownCategoryPAL dbCategory;

                if (dtoCategory.ProductBreakDownCategoryID < 0)
                {
                    dbCategory = new ProductBreakDownCategoryPAL();
                    dbItem.ProductBreakDownCategoryPAL.Add(dbCategory);
                }
                else
                {
                    dbCategory = dbItem.ProductBreakDownCategoryPAL.FirstOrDefault(o => o.ProductBreakDownCategoryID == dtoCategory.ProductBreakDownCategoryID);
                }

                if (dbCategory != null)
                {
                    AutoMapper.Mapper.Map<DTO.ProductBreakDownCategoryPALData, ProductBreakDownCategoryPAL>(dtoCategory, dbCategory);

                    foreach (var dbCategoryImage in dbCategory.ProductBreakDownCategoryImagePAL.ToArray())
                    {
                        if (!dtoCategory.ProductBreakDownCategoryImagePAL.Select(o => o.ProductBreakDownCategoryImageID).Contains(dbCategoryImage.ProductBreakDownCategoryImageID))
                        {
                            if (!string.IsNullOrEmpty(dbCategoryImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbCategoryImage.FileUD);
                            }
                            dbCategory.ProductBreakDownCategoryImagePAL.Remove(dbCategoryImage);
                        }
                    }

                    foreach (var dbCategoryType in dbCategory.ProductBreakDownCategoryTypePAL.ToArray())
                    {
                        if (!dtoCategory.ProductBreakDownCategoryTypePAL.Select(o => o.ProductBreakDownCategoryTypeID).Contains(dbCategoryType.ProductBreakDownCategoryTypeID))
                        {
                            foreach (var dbDetail in dbCategoryType.ProductBreakDownDetailPAL.ToArray())
                            {
                                dbCategoryType.ProductBreakDownDetailPAL.Remove(dbDetail);
                            }

                            dbCategory.ProductBreakDownCategoryTypePAL.Remove(dbCategoryType);
                        }
                    }

                    foreach (var dtoCategoryImage in dtoCategory.ProductBreakDownCategoryImagePAL)
                    {
                        ProductBreakDownCategoryImagePAL dbCategoryImage;

                        if (dtoCategoryImage.ProductBreakDownCategoryImageID < 0)
                        {
                            dbCategoryImage = new ProductBreakDownCategoryImagePAL();
                            dbCategory.ProductBreakDownCategoryImagePAL.Add(dbCategoryImage);
                        }
                        else
                        {
                            dbCategoryImage = dbCategory.ProductBreakDownCategoryImagePAL.FirstOrDefault(o => o.ProductBreakDownCategoryImageID == dtoCategoryImage.ProductBreakDownCategoryImageID);
                        }

                        if (dbCategoryImage != null)
                        {
                            if (dtoCategoryImage.HasChange.HasValue && dtoCategoryImage.HasChange.Value)
                            {
                                dbCategoryImage.FileUD = fwFactory.CreateFilePointer(tempFile, dtoCategoryImage.NewFile, dtoCategoryImage.FileUD, dtoCategoryImage.FriendlyName);
                            }

                            AutoMapper.Mapper.Map<DTO.ProductBreakDownCategoryImagePALData, ProductBreakDownCategoryImagePAL>(dtoCategoryImage, dbCategoryImage);
                        }
                    }

                    foreach (var dtoCategoryType in dtoCategory.ProductBreakDownCategoryTypePAL)
                    {
                        ProductBreakDownCategoryTypePAL dbCategoryType;

                        if (dtoCategoryType.ProductBreakDownCategoryTypeID < 0)
                        {
                            dbCategoryType = new ProductBreakDownCategoryTypePAL();
                            dbCategory.ProductBreakDownCategoryTypePAL.Add(dbCategoryType);
                        }
                        else
                        {
                            dbCategoryType = dbCategory.ProductBreakDownCategoryTypePAL.FirstOrDefault(o => o.ProductBreakDownCategoryTypeID == dtoCategoryType.ProductBreakDownCategoryTypeID);
                        }

                        if (dbCategoryType != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ProductBreakDownCategoryTypePALData, ProductBreakDownCategoryTypePAL>(dtoCategoryType, dbCategoryType);

                            foreach (var dbDetail in dbCategoryType.ProductBreakDownDetailPAL.ToArray())
                            {
                                if (!dtoCategoryType.ProductBreakDownDetailPAL.Select(s => s.ProductBreakDownDetailID).Contains(dbDetail.ProductBreakDownDetailID))
                                {
                                    dbCategoryType.ProductBreakDownDetailPAL.Remove(dbDetail);
                                }
                            }

                            foreach (var dtoDetail in dtoCategoryType.ProductBreakDownDetailPAL)
                            {
                                ProductBreakDownDetailPAL dbDetail;

                                if (dtoDetail.ProductBreakDownDetailID <= 0)
                                {
                                    dbDetail = new ProductBreakDownDetailPAL();
                                    dbCategoryType.ProductBreakDownDetailPAL.Add(dbDetail);
                                }
                                else
                                {
                                    dbDetail = dbCategoryType.ProductBreakDownDetailPAL.FirstOrDefault(o => o.ProductBreakDownDetailID == dtoDetail.ProductBreakDownDetailID);
                                }

                                if (dbDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ProductBreakDownDetailPALData, ProductBreakDownDetailPAL>(dtoDetail, dbDetail);
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<DTO.SupportProductBreakDownPALProductionItemData> DB2DTO_ProductionItemPAL(List<ProductBreakDownPAL_SupportProductionItem_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_SupportProductionItem_View>, List<DTO.SupportProductBreakDownPALProductionItemData>>(dbItem);
        }

        public List<DTO.SupportProductBreakDownPALProductData> DB2DTO_ProductPAL(List<ProductBreakDownPAL_SupportProduct_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_SupportProduct_View>, List<DTO.SupportProductBreakDownPALProductData>>(dbItem);
        }

        public List<DTO.SupportProductBreakDownPALCategoryData> DB2DTO_CategoryPAL(List<ProductBreakDownPAL_SupportProductBreakDownCategoryPAL_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownPAL_SupportProductBreakDownCategoryPAL_View>, List<DTO.SupportProductBreakDownPALCategoryData>>(dbItem);
        }

        public void DTO2DTO_ProductBreakDownPAL(DTO.ProductBreakDownPALData oldItem, ref DTO.ProductBreakDownPALData newItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductBreakDownPALData, DTO.ProductBreakDownPALData>(oldItem, newItem);

            int newProducBeakDownID = newItem.ProductBreakDownID;

            int newProductBreakDownCategoryAmountID = -1;
            for (int i = 0; i < newItem.ProductBreakDownCategoryAmountPAL.Count(); i++)
            {
                newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownID = newProducBeakDownID;
                newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryID = newProductBreakDownCategoryAmountID;

                int newProductBreakDownCategoryImageID = -1;
                for (int j = 0; j < newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryImagePAL.Count(); j++)
                {
                    newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryImagePAL[j].ProductBreakDownCategoryID = newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryID;
                    newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryImagePAL[j].ProductBreakDownCategoryImageID = newProductBreakDownCategoryImageID;

                    newProductBreakDownCategoryImageID--;
                }

                int newProductBreakDownCategoryTypeID = -1;
                for (int j = 0; j < newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL.Count(); j++)
                {
                    newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownCategoryID = newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryID;
                    newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownCategoryTypeID = newProductBreakDownCategoryTypeID;

                    int newProductBreakDownDetailID = -1;
                    for (int k = 0; k < newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownDetailPAL.Count(); k++)
                    {
                        newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownDetailPAL[k].ProductBreakDownCategoryTypeID = newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownCategoryTypeID;
                        newItem.ProductBreakDownCategoryAmountPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownDetailPAL[k].ProductBreakDownDetailID = newProductBreakDownDetailID;

                        newProductBreakDownDetailID--;
                    }

                    newProductBreakDownCategoryTypeID--;
                }

                newProductBreakDownCategoryAmountID--;
            }

            int newProductBreakDownCategoryPercentID = -1;
            for (int i = 0; i < newItem.ProductBreakDownCategoryPercentPAL.Count(); i++)
            {
                newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownID = newProducBeakDownID;
                newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryID = newProductBreakDownCategoryPercentID;

                int newProductBreakDownCategoryImageID = -1;
                for (int j = 0; j < newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryImagePAL.Count(); j++)
                {
                    newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryImagePAL[j].ProductBreakDownCategoryID = newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryID;
                    newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryImagePAL[j].ProductBreakDownCategoryImageID = newProductBreakDownCategoryImageID;

                    newProductBreakDownCategoryImageID--;
                }

                int newProductBreakDownCategoryTypeID = -1;
                for (int j = 0; j < newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL.Count(); j++)
                {
                    newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownCategoryID = newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryID;
                    newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownCategoryTypeID = newProductBreakDownCategoryTypeID;

                    int newProductBreakDownDetailID = -1;
                    for (int k = 0; k < newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownDetailPAL.Count(); k++)
                    {
                        newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownDetailPAL[k].ProductBreakDownCategoryTypeID = newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownCategoryTypeID;
                        newItem.ProductBreakDownCategoryPercentPAL[i].ProductBreakDownCategoryTypePAL[j].ProductBreakDownDetailPAL[k].ProductBreakDownDetailID = newProductBreakDownDetailID;

                        newProductBreakDownDetailID--;
                    }

                    newProductBreakDownCategoryTypeID--;
                }

                newProductBreakDownCategoryPercentID--;
            }
        }
    }
}
