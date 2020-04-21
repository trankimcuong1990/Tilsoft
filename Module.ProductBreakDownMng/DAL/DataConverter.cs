using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DAL
{
    internal class DataConverter
    {
        private readonly Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = "ProductBreakDownMng";

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            /// Mapping search result ProductBreakDownDefaultCategory
            AutoMapper.Mapper.CreateMap<ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_SearchResultView, DTO.ProductBreakDownDefaultCategorySearchResultData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping object ProductBreakDownDefaultCategory
            AutoMapper.Mapper.CreateMap<ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_View, DTO.ProductBreakDownDefaultCategoryData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping list support ProductBreakDownCalculationType
            AutoMapper.Mapper.CreateMap<SupportMng_ProductBreakDownCalculationType_View, DTO.ProductBreakDownCalculationTypeData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping list support OptionToGetQuantity
            AutoMapper.Mapper.CreateMap<ProductBreakDownDefaultCategoryMng_OptionToGetQuantity_View, DTO.OptionToGetQuantityData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping object to SQL ProductBreakDownDefaultCategory
            AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownDefaultCategoryData, ProductBreakDownDefaultCategory>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownDefaultCategoryID, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore());

            /// Mapping search result ProductBreakDown
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDown_SearchResultView, DTO.ProductBreakDownSearchResultData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping object ProductBreakDown
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDown_View, DTO.ProductBreakDownData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ProductBreakDownCategory, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategory_View))
                .ForMember(d => d.ProductBreakDownCategoryAmount, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryAmount_View))
                .ForMember(d => d.ProductBreakDownCategoryPercent, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryPercent_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping quick search result Model
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ModelQuickSearch_View, DTO.ModelSearchData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping quick search result SampleProduct
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_SampleProductQuickSearch_View, DTO.SampleProductSearchData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping object to SQL ProductBreakDown
            AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownData, ProductBreakDown>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownID, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownCategory, o => o.Ignore());

            /// Mapping autofill ProductBreakDownCategory
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDownCategory_View, DTO.ProductBreakDownCategoryData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownCategoryImage, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryImage_View))
                .ForMember(d => d.ProductBreakDownCategoryType, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryType_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping autofill ProductBreakDownCategoryAmount
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDownCategoryAmount_View, DTO.ProductBreakDownCategoryAmountData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownCategoryImage, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryImage_View))
                .ForMember(d => d.ProductBreakDownCategoryType, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryType_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping autofill ProductBreakDownCategoryPercent
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDownCategoryPercent_View, DTO.ProductBreakDownCategoryPercentData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownCategoryImage, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryImage_View))
                .ForMember(d => d.ProductBreakDownCategoryType, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownCategoryType_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping autofill ProductBreakDownCategoryImage
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDownCategoryImage_View, DTO.ProductBreakDownCategoryImageData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping autofill ProductBreakDownCategoryType
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDownCategoryType_View, DTO.ProductBreakDownCategoryTypeData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownDetail, o => o.MapFrom(s => s.ProductBreakDownMng_ProductBreakDownDetail_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping autofill ProductBreakDownDetail
            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_ProductBreakDownDetail_View, DTO.ProductBreakDownDetailData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Mapping object to SQL ProductBreakDownCategory
            AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownCategoryData, ProductBreakDownCategory>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownCategoryID, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownID, o => o.Ignore())
                .ForMember(d => d.FactoryProductBreakDownCategory, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownCategoryImage, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownCategoryType, o => o.Ignore());

            /// Mapping object to SQL ProductBreakDownCategoryImage
            AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownCategoryImageData, ProductBreakDownCategoryImage>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownCategoryImageID, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownCategoryID, o => o.Ignore())
                .ForMember(d => d.FileUD, o => o.Ignore());

            /// Mapping object to SQL ProductBreakDownCategoryType
            AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownCategoryTypeData, ProductBreakDownCategoryType>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownCategoryTypeID, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownCategoryID, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownDetail, o => o.Ignore());

            /// Mapping object to SQL ProductBreakDownDetail
            AutoMapper.Mapper.CreateMap<DTO.ProductBreakDownDetailData, ProductBreakDownDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductBreakDownDetailID, o => o.Ignore())
                .ForMember(d => d.ProductBreakDownCategoryTypeID, o => o.Ignore());

            /// Mapping list support OptionToGetPrice
            AutoMapper.Mapper.CreateMap<ProductBreakDownDefaultCategoryMng_OptionToGetPrice_View, DTO.OptionToGetPriceData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<ProductBreakDownMng_SupportClient_View, DTO.SupportClientData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
        }

        public List<DTO.ProductBreakDownDefaultCategorySearchResultData> DB2DTO_ProductBreakDownDefaultCategorySearchResult(List<ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_SearchResultView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_SearchResultView>, List<DTO.ProductBreakDownDefaultCategorySearchResultData>>(dbItem);
        }

        public DTO.ProductBreakDownDefaultCategoryData DB2DTO_ProductBreakDownDefaultCategory(ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_View, DTO.ProductBreakDownDefaultCategoryData>(dbItem);
        }

        public List<DTO.ProductBreakDownCalculationTypeData> DB2DTO_ProductBreakDownCalculationType(List<SupportMng_ProductBreakDownCalculationType_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductBreakDownCalculationType_View>, List<DTO.ProductBreakDownCalculationTypeData>>(dbItem);
        }

        public List<DTO.OptionToGetQuantityData> DB2DTO_OptionToGetQuantity(List<ProductBreakDownDefaultCategoryMng_OptionToGetQuantity_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownDefaultCategoryMng_OptionToGetQuantity_View>, List<DTO.OptionToGetQuantityData>>(dbItem);
        }

        public void DTO2DB_ProductBreakDownDefaultCategory(DTO.ProductBreakDownDefaultCategoryData dtoItem, ref ProductBreakDownDefaultCategory dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductBreakDownDefaultCategoryData, ProductBreakDownDefaultCategory>(dtoItem, dbItem);
        }

        public List<DTO.ProductBreakDownSearchResultData> DB2DTO_ProductBreakDownSearchResult(List<ProductBreakDownMng_ProductBreakDown_SearchResultView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownMng_ProductBreakDown_SearchResultView>, List<DTO.ProductBreakDownSearchResultData>>(dbItem);
        }

        public DTO.ProductBreakDownData DB2DTO_ProductBreakDown(ProductBreakDownMng_ProductBreakDown_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductBreakDownMng_ProductBreakDown_View, DTO.ProductBreakDownData>(dbItem);
        }

        public List<DTO.ModelSearchData> DB2DTO_ModelSearch(List<ProductBreakDownMng_ModelQuickSearch_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownMng_ModelQuickSearch_View>, List<DTO.ModelSearchData>>(dbItem);
        }

        public List<DTO.SampleProductSearchData> DB2DTO_SampleProductSearch(List<ProductBreakDownMng_SampleProductQuickSearch_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownMng_SampleProductQuickSearch_View>, List<DTO.SampleProductSearchData>>(dbItem);
        }

        public void DTO2DB_ProductBreakDown(DTO.ProductBreakDownData dtoItem, ref ProductBreakDown dbItem, string tempFile, int userId)
        {
            // ProductBreakDown
            AutoMapper.Mapper.Map<DTO.ProductBreakDownData, ProductBreakDown>(dtoItem, dbItem);

            // ProductBreakDown: Category [Progress on Database]
            foreach (var dbCategory in dbItem.ProductBreakDownCategory.ToArray())
            {
                if (!dtoItem.ProductBreakDownCategory.Select(o => o.ProductBreakDownCategoryID).Contains(dbCategory.ProductBreakDownCategoryID))
                {
                    // Category image
                    foreach (var dbCategoryImage in dbCategory.ProductBreakDownCategoryImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbCategoryImage.FileUD))
                        {
                            // Remove Files
                            fwFactory.RemoveImageFile(dbCategoryImage.FileUD);
                        }
                        // Remove ProductBreakDownCategoryImage
                        dbCategory.ProductBreakDownCategoryImage.Remove(dbCategoryImage);
                    }

                    // Category type
                    foreach (var dbCategoryType in dbCategory.ProductBreakDownCategoryType.ToArray())
                    {
                        // Detail
                        foreach (var dbDetail in dbCategoryType.ProductBreakDownDetail.ToArray())
                        {
                            dbCategoryType.ProductBreakDownDetail.Remove(dbDetail);
                        }

                        dbCategory.ProductBreakDownCategoryType.Remove(dbCategoryType);
                    }

                    // Remove ProductBreakDownCategory
                    dbItem.ProductBreakDownCategory.Remove(dbCategory);
                }
            }

            // ProductBreakDown: Category [Progress on DTO]
            foreach (var dtoCategory in dtoItem.ProductBreakDownCategory)
            {
                ProductBreakDownCategory dbCategory;

                if (dtoCategory.ProductBreakDownCategoryID < 0)
                {
                    dbCategory = new ProductBreakDownCategory();
                    dbItem.ProductBreakDownCategory.Add(dbCategory);
                }
                else
                {
                    dbCategory = dbItem.ProductBreakDownCategory.FirstOrDefault(o => o.ProductBreakDownCategoryID == dtoCategory.ProductBreakDownCategoryID);
                }

                if (dbCategory != null)
                {
                    AutoMapper.Mapper.Map<DTO.ProductBreakDownCategoryData, ProductBreakDownCategory>(dtoCategory, dbCategory);

                    // Category image
                    foreach (var dbCategoryImage in dbCategory.ProductBreakDownCategoryImage.ToArray())
                    {
                        if (!dtoCategory.ProductBreakDownCategoryImage.Select(o => o.ProductBreakDownCategoryImageID).Contains(dbCategoryImage.ProductBreakDownCategoryImageID))
                        {
                            if (!string.IsNullOrEmpty(dbCategoryImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbCategoryImage.FileUD);
                            }
                            dbCategory.ProductBreakDownCategoryImage.Remove(dbCategoryImage);
                        }
                    }

                    // Category type
                    foreach (var dbCategoryType in dbCategory.ProductBreakDownCategoryType.ToArray())
                    {
                        if (!dtoCategory.ProductBreakDownCategoryType.Select(o => o.ProductBreakDownCategoryTypeID).Contains(dbCategoryType.ProductBreakDownCategoryTypeID))
                        {
                            // Remove detail
                            foreach (var dbDetail in dbCategoryType.ProductBreakDownDetail.ToArray())
                            {
                                dbCategoryType.ProductBreakDownDetail.Remove(dbDetail);
                            }

                            dbCategory.ProductBreakDownCategoryType.Remove(dbCategoryType);
                        }
                    }

                    // Category image
                    foreach (var dtoCategoryImage in dtoCategory.ProductBreakDownCategoryImage)
                    {
                        ProductBreakDownCategoryImage dbCategoryImage;

                        if (dtoCategoryImage.ProductBreakDownCategoryImageID < 0)
                        {
                            dbCategoryImage = new ProductBreakDownCategoryImage();
                            dbCategory.ProductBreakDownCategoryImage.Add(dbCategoryImage);
                        }
                        else
                        {
                            dbCategoryImage = dbCategory.ProductBreakDownCategoryImage.FirstOrDefault(o => o.ProductBreakDownCategoryImageID == dtoCategoryImage.ProductBreakDownCategoryImageID);
                        }

                        if (dbCategoryImage != null)
                        {
                            // Progress category image
                            if (dtoCategoryImage.HasChange.HasValue && dtoCategoryImage.HasChange.Value)
                            {
                                dbCategoryImage.FileUD = fwFactory.CreateFilePointer(tempFile, dtoCategoryImage.NewFile, dtoCategoryImage.FileUD, dtoCategoryImage.FriendlyName);
                            }

                            AutoMapper.Mapper.Map<DTO.ProductBreakDownCategoryImageData, ProductBreakDownCategoryImage>(dtoCategoryImage, dbCategoryImage);
                        }
                    }

                    // Category type
                    foreach (var dtoCategoryType in dtoCategory.ProductBreakDownCategoryType)
                    {
                        ProductBreakDownCategoryType dbCategoryType;

                        if (dtoCategoryType.ProductBreakDownCategoryTypeID < 0)
                        {
                            dbCategoryType = new ProductBreakDownCategoryType();
                            dbCategory.ProductBreakDownCategoryType.Add(dbCategoryType);
                        }
                        else
                        {
                            dbCategoryType = dbCategory.ProductBreakDownCategoryType.FirstOrDefault(o => o.ProductBreakDownCategoryTypeID == dtoCategoryType.ProductBreakDownCategoryTypeID);
                        }

                        if (dbCategoryType != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ProductBreakDownCategoryTypeData, ProductBreakDownCategoryType>(dtoCategoryType, dbCategoryType);

                            foreach (var dbDetail in dbCategoryType.ProductBreakDownDetail.ToArray())
                            {
                                if (!dtoCategoryType.ProductBreakDownDetail.Select(s => s.ProductBreakDownDetailID).Contains(dbDetail.ProductBreakDownDetailID))
                                {
                                    dbCategoryType.ProductBreakDownDetail.Remove(dbDetail);
                                }
                            }

                            foreach (var dtoDetail in dtoCategoryType.ProductBreakDownDetail)
                            {
                                ProductBreakDownDetail dbDetail;

                                if (dtoDetail.ProductBreakDownDetailID <= 0)
                                {
                                    dbDetail = new ProductBreakDownDetail();
                                    dbCategoryType.ProductBreakDownDetail.Add(dbDetail);
                                }
                                else
                                {
                                    dbDetail = dbCategoryType.ProductBreakDownDetail.FirstOrDefault(o => o.ProductBreakDownDetailID == dtoDetail.ProductBreakDownDetailID);
                                }

                                if (dbDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ProductBreakDownDetailData, ProductBreakDownDetail>(dtoDetail, dbDetail);
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<DTO.ProductBreakDownCategoryData> DB2DTO_ProductBreakDownCategory(List<ProductBreakDownMng_ProductBreakDownCategory_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownMng_ProductBreakDownCategory_View>, List<DTO.ProductBreakDownCategoryData>>(dbItem);
        }

        public List<DTO.OptionToGetPriceData> DB2DTO_OptionToGetPrice(List<ProductBreakDownDefaultCategoryMng_OptionToGetPrice_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownDefaultCategoryMng_OptionToGetPrice_View>, List<DTO.OptionToGetPriceData>>(dbItem);
        }

        public List<DTO.SupportClientData> DB2DTO_SupportClient(List<ProductBreakDownMng_SupportClient_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductBreakDownMng_SupportClient_View>, List<DTO.SupportClientData>>(dbItem);
        }
    }
}
