using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;
namespace DAL.OfferMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "OfferMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<List_TrackingStatus, DTO.OfferMng.OfferStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferSearchResult_View, DTO.OfferMng.OfferSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.OfferDate, o => o.ResolveUsing(s => s.OfferDate.HasValue ? s.OfferDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PlaningPurchasingPriceSource_View, DTO.OfferMng.PlaningPurchasingPriceSourceDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLineSalePriceHistory_View, DTO.OfferMng.OfferLineSalePriceHistoryDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLineSalePriceHistoryLastSeason_View, DTO.OfferMng.OfferLineSalePriceHistoryLastSeasonDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_function_getExchangeRate_Result, DTO.OfferMng.ExchangeRateDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_function_GetOfferLinePlaningPurchasingPrice_Result, DTO.OfferMng.PlaningPurchasingPriceDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_function_GetPlaningPurchasingPrice_Result, DTO.OfferMng.PlaningPurchasingPriceDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLinePriceOption_View, DTO.OfferMng.OfferLinePriceOption>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLine_View, DTO.OfferMng.OfferLine>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.OfferLinePriceOptions, o => o.MapFrom(s => s.OfferMng_OfferLinePriceOption_View))
                   .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.ResolveUsing(s => s.PlaningPurchasingPriceSelectedDate.HasValue ? s.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ProductThumbnailLocation) ? s.ProductThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductFileLocation) ? s.ProductFileLocation : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLineSparepart_View, DTO.OfferMng.OfferLineSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLineSampleProduct_View, DTO.OfferMng.OfferLineSampleProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_SampleProductSearchResult_View, DTO.OfferMng.SampleProductSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_Offer_View, DTO.OfferMng.Offer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferScanFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.OfferScanFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.OfferScanFileURL)))
                    .ForMember(d => d.OfferLines, o => o.MapFrom(s => s.OfferMng_OfferLine_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.OfferLineSpareparts, o => o.MapFrom(s => s.OfferMng_OfferLineSparepart_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.OfferLineSampleProductDTOs, o => o.MapFrom(s => s.OfferMng_OfferLineSampleProduct_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.Offer, Offer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferID, o => o.Ignore())
                    .ForMember(d => d.OfferScanFile, o => o.Ignore())
                    .ForMember(d => d.OfferLine, o => o.Ignore())
                    .ForMember(d => d.OfferLineSparepart, o => o.Ignore())
                    .ForMember(d => d.IsApproved, o => o.Ignore())
                    .ForMember(d => d.ApprovedBy, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.OfferDate, o => o.Ignore())
                    .ForMember(d => d.ValidUntil, o => o.Ignore())
                    .ForMember(d => d.LDS, o => o.Ignore())
                    .ForMember(d => d.EstimatedDeliveryDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore());

                //dbItem.OfferDate = Library.Helper.ConvertStringToDateTime(dtoItem.OfferDate, new System.Globalization.CultureInfo("vi-VN"));
                //dbItem.ValidUntil = Library.Helper.ConvertStringToDateTime(dtoItem.ValidUntil, new System.Globalization.CultureInfo("vi-VN"));
                //dbItem.LDS = Library.Helper.ConvertStringToDateTime(dtoItem.LDS, new System.Globalization.CultureInfo("vi-VN"));
                //dbItem.EstimatedDeliveryDate = Library.Helper.ConvertStringToDateTime(dtoItem.EstimatedDeliveryDate, new System.Globalization.CultureInfo("vi-VN"));



                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferLine, OfferLine>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.OfferLineID, o => o.Ignore())
                     .ForMember(d => d.OfferLinePriceOption, o => o.Ignore())
                     .ForMember(d => d.ModelID, o => o.Ignore())
                     .ForMember(d => d.FrameMaterialID, o => o.Ignore())
                     .ForMember(d => d.FrameMaterialColorID, o => o.Ignore())
                     .ForMember(d => d.MaterialID, o => o.Ignore())
                     .ForMember(d => d.MaterialTypeID, o => o.Ignore())
                     .ForMember(d => d.MaterialColorID, o => o.Ignore())
                     .ForMember(d => d.SubMaterialID, o => o.Ignore())
                     .ForMember(d => d.SubMaterialColorID, o => o.Ignore())
                     .ForMember(d => d.SeatCushionID, o => o.Ignore())
                     .ForMember(d => d.BackCushionID, o => o.Ignore())
                     .ForMember(d => d.CushionColorID, o => o.Ignore())
                     .ForMember(d => d.FSCTypeID, o => o.Ignore())
                     .ForMember(d => d.FSCPercentID, o => o.Ignore())
                     .ForMember(d => d.ArticleCode, o => o.Ignore())
                     .ForMember(d => d.Description, o => o.Ignore())
                     .ForMember(d => d.EstimatedPurchasingPriceUpdatedByID, o => o.Ignore())
                     .ForMember(d => d.PlaningPurchasingPriceSelectedBy, o => o.Ignore())
                     .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.Ignore())
                     .ForMember(d => d.IsApproved, o => o.Ignore())
                    .ForMember(d => d.ApprovedBy, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferLinePriceOption, OfferLinePriceOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLinePriceOptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferLineSparepart, OfferLineSparepart>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLineSparePartID, o => o.Ignore())
                    .ForMember(d => d.ModelID, o => o.Ignore())
                    .ForMember(d => d.PartID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferLineSampleProductDTO, OfferLineSampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLineSampleProductID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLine_EditView, DTO.OfferMng.OfferLineEdit>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_Model_View, DTO.OfferMng.Model>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.MaterialThumbnailLocation))
                    .ForMember(d => d.FrameMaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.FrameMaterialThumbnailLocation))
                    .ForMember(d => d.SubMaterialColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SubMaterialColorThumbnailLocation))
                    .ForMember(d => d.CushionColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.CushionColorThumbnailLocation))
                    .ForMember(d => d.BackCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.BackCushionThumbnailLocation))
                    .ForMember(d => d.SeatCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SeatCushionThumbnailLocation))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //For OverView

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLinePriceOption_OverView, DTO.OfferMng.OfferLinePriceOptionOverView>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLine_OverView, DTO.OfferMng.OfferlineOverView>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.OfferLinePriceOptions, o => o.MapFrom(s => s.OfferMng_OfferLinePriceOption_OverView))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_OfferLineSparepart_OverView, DTO.OfferMng.OfferLineSparepartOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferMng_Offer_OverView, DTO.OfferMng.OfferOverView>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferScanFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.OfferScanFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.OfferScanFileURL)))
                    .ForMember(d => d.OfferLines, o => o.MapFrom(s => s.OfferMng_OfferLine_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.OfferLineSpareparts, o => o.MapFrom(s => s.OfferMng_OfferLineSparepart_OverView.OrderBy(x => x.RowIndex)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferOverView, Offer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferID, o => o.Ignore())
                    .ForMember(d => d.OfferScanFile, o => o.Ignore())
                    .ForMember(d => d.OfferLine, o => o.Ignore())
                    .ForMember(d => d.OfferLineSparepart, o => o.Ignore())
                    ;



                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferlineOverView, OfferLine>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.OfferLineID, o => o.Ignore())
                     .ForMember(d => d.OfferLinePriceOption, o => o.Ignore())
                     .ForMember(d => d.ModelID, o => o.Ignore())
                     .ForMember(d => d.FrameMaterialID, o => o.Ignore())
                     .ForMember(d => d.FrameMaterialColorID, o => o.Ignore())
                     .ForMember(d => d.MaterialID, o => o.Ignore())
                     .ForMember(d => d.MaterialTypeID, o => o.Ignore())
                     .ForMember(d => d.MaterialColorID, o => o.Ignore())
                     .ForMember(d => d.SubMaterialID, o => o.Ignore())
                     .ForMember(d => d.SubMaterialColorID, o => o.Ignore())
                     .ForMember(d => d.SeatCushionID, o => o.Ignore())
                     .ForMember(d => d.BackCushionID, o => o.Ignore())
                     .ForMember(d => d.CushionColorID, o => o.Ignore())
                     .ForMember(d => d.FSCTypeID, o => o.Ignore())
                     .ForMember(d => d.FSCPercentID, o => o.Ignore())
                     .ForMember(d => d.ArticleCode, o => o.Ignore())
                     .ForMember(d => d.Description, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferLinePriceOptionOverView, OfferLinePriceOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLinePriceOptionID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.OfferMng.OfferLineSparepartOverView, OfferLineSparepart>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLineSparePartID, o => o.Ignore())

                    .ForMember(d => d.ModelID, o => o.Ignore())
                    .ForMember(d => d.PartID, o => o.Ignore())
                    ;

                Mapper.CreateMap<DTO.OfferMng.FactoryMng_FactoryPermissionDTO, FactoryMng_FactoryPermission_View>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<FactoryMng_FactoryPermission_View, DTO.OfferMng.FactoryMng_FactoryPermissionDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<FactoryMng_Function_GetFactoryPermission_Result, DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO, FactoryMng_Function_GetFactoryPermission_Result>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<OfferMng_ClientEstimatedAdditionalCost_View, DTO.OfferMng.ClientEstimatedAdditionalCostDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.OfferMng.OfferStatusDTO> DB2DTO_OfferStatus(List<List_TrackingStatus> dbItem)
        {
            return AutoMapper.Mapper.Map<List<List_TrackingStatus>, List<DTO.OfferMng.OfferStatusDTO>>(dbItem);
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceSourceDTO> DB2DTO_PlaningPurchasingPriceSourceDTO(List<SupportMng_PlaningPurchasingPriceSource_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_PlaningPurchasingPriceSource_View>, List<DTO.OfferMng.PlaningPurchasingPriceSourceDTO>>(dbItem);
        }

        public List<DTO.OfferMng.OfferLineSalePriceHistoryDTO> DB2DTO_OfferLineSalePriceHistoryDTO(List<OfferMng_OfferLineSalePriceHistory_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_OfferLineSalePriceHistory_View>, List<DTO.OfferMng.OfferLineSalePriceHistoryDTO>>(dbItem);
        }

        public List<DTO.OfferMng.OfferLineSalePriceHistoryLastSeasonDTO> DB2DTO_OfferLineSalePriceHistoryLastSeasonDTO(List<OfferMng_OfferLineSalePriceHistoryLastSeason_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_OfferLineSalePriceHistoryLastSeason_View>,List<DTO.OfferMng.OfferLineSalePriceHistoryLastSeasonDTO>> (dbItem);
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceDTO> DB2DTO_PlaningPurchasingPriceDTO(List<OfferMng_function_GetOfferLinePlaningPurchasingPrice_Result> dbItem)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_function_GetOfferLinePlaningPurchasingPrice_Result>, List<DTO.OfferMng.PlaningPurchasingPriceDTO>>(dbItem);
        }

        public List<DTO.OfferMng.PlaningPurchasingPriceDTO> DB2DTO_PlaningPurchasingPriceDTO(List<OfferMng_function_GetPlaningPurchasingPrice_Result> dbItem)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_function_GetPlaningPurchasingPrice_Result>, List<DTO.OfferMng.PlaningPurchasingPriceDTO>>(dbItem);
        }

        public List<DTO.OfferMng.OfferSearchResult> DB2DTO_OfferSearchResultList(List<OfferMng_OfferSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_OfferSearchResult_View>, List<DTO.OfferMng.OfferSearchResult>>(dbItems);
        }

        public List<DTO.OfferMng.ExchangeRateDTO> DB2DTO_ExchangeRateDTO(List<OfferMng_function_getExchangeRate_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_function_getExchangeRate_Result>, List<DTO.OfferMng.ExchangeRateDTO>>(dbItems);
        }

        public List<DTO.OfferMng.OfferLine> DB2DTO_OfferLine(List<OfferMng_OfferLine_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_OfferLine_View>, List<DTO.OfferMng.OfferLine>>(dbItems);
        }

        public List<DTO.OfferMng.SampleProductSearchResultDTO> DB2DTO_SampleProductSearchResultDTO(List<OfferMng_SampleProductSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_SampleProductSearchResult_View>, List<DTO.OfferMng.SampleProductSearchResultDTO>>(dbItems);
        }

        public List<DTO.OfferMng.OfferLineSampleProductDTO> DB2DTO_OfferLineSampleProductDTO(List<OfferMng_OfferLineSampleProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferMng_OfferLineSampleProduct_View>, List<DTO.OfferMng.OfferLineSampleProductDTO>>(dbItems);
        }

        public DTO.OfferMng.Offer DB2DTO_Offer(OfferMng_Offer_View dbItem)
        {
            DTO.OfferMng.Offer dtoItem = AutoMapper.Mapper.Map<OfferMng_Offer_View, DTO.OfferMng.Offer>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.OfferDate.HasValue)
                dtoItem.OfferDateFormated = dbItem.OfferDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.ValidUntil.HasValue)
                dtoItem.ValidUntilFormated = dbItem.ValidUntil.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS.HasValue)
                dtoItem.LDSFormated = dbItem.LDS.Value.ToString("dd/MM/yyyy");

            if (dbItem.EstimatedDeliveryDate.HasValue)
                dtoItem.EstimatedDeliveryDateFormated = dbItem.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2DB_Offer(DTO.OfferMng.Offer dtoItem, ref Offer dbItem, int actionType, int userId)
        {
            OfferStatus dbOfferStatus = dbItem.OfferStatus.Where(o => o.IsCurrentStatus.HasValue && o.IsCurrentStatus.Value).FirstOrDefault();
            /*
             * MAP & CHECK  OFFERLINE
             */
            List<OfferLine> ItemNeedDelete_Extends = new List<OfferLine>();
            if (dtoItem.OfferLines != null)
            {
                //CHECK
                foreach (OfferLine dbDetail in dbItem.OfferLine.Where(o => !dtoItem.OfferLines.Select(s => s.OfferLineID).Contains(o.OfferLineID)))
                {
                    ItemNeedDelete_Extends.Add(dbDetail);
                }
                foreach (OfferLine dbDetail in ItemNeedDelete_Extends)
                {
                    dbItem.OfferLine.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.OfferMng.OfferLine dtoDetail in dtoItem.OfferLines)
                {
                    OfferLine dbDetail;
                    OfferLinePriceOption dbPriceOption;
                    bool isAllowEditItem = true;
                    if (dtoDetail.OfferLineID < 0 || actionType != 1) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        dbDetail = new OfferLine();
                        if (dtoDetail.OfferLinePriceOptions != null)
                        {
                            foreach (DTO.OfferMng.OfferLinePriceOption dtoPriceOption in dtoDetail.OfferLinePriceOptions)
                            {
                                dbPriceOption = new OfferLinePriceOption();
                                dbDetail.OfferLinePriceOption.Add(dbPriceOption);
                                AutoMapper.Mapper.Map<DTO.OfferMng.OfferLinePriceOption, OfferLinePriceOption>(dtoPriceOption, dbPriceOption);
                            }
                        }
                        dbItem.OfferLine.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.OfferLine.FirstOrDefault(o => o.OfferLineID == dtoDetail.OfferLineID);
                        if (dbDetail != null && dtoDetail.OfferLinePriceOptions != null)
                        {
                            foreach (DTO.OfferMng.OfferLinePriceOption dtoPriceOption in dtoDetail.OfferLinePriceOptions)
                            {
                                if (dtoPriceOption.OfferLinePriceOptionID < 0)
                                {
                                    dbPriceOption = new OfferLinePriceOption();
                                    dbDetail.OfferLinePriceOption.Add(dbPriceOption);
                                }
                                else
                                {
                                    dbPriceOption = dbDetail.OfferLinePriceOption.FirstOrDefault(o => o.OfferLinePriceOptionID == dtoPriceOption.OfferLinePriceOptionID);
                                }
                                if (dbPriceOption != null)
                                {
                                    Mapper.Map(dtoPriceOption, dbPriceOption);

                                }
                            }
                        }
                        //check item exist in factory order
                        var x = dbDetail.SaleOrderDetail.Where(o => o.FactoryOrderDetail != null && o.FactoryOrderDetail.Count() > 0);
                        isAllowEditItem = !(x != null && x.Count() > 0);

                        // detect changes for approved items
                        if (dtoDetail.OfferItemTypeID == 1 && dtoDetail.IsApproved.HasValue && !dtoDetail.IsApproved.Value)
                        {
                            dbDetail.IsApproved = false;
                            dbDetail.ApprovedBy = null;
                            dbDetail.ApprovedDate = null;
                        }

                        //
                        // Author       : The My
                        // Description  : force update configuration changed even if item exists in factory order
                        //
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CHANGE_OFFER_ITEM_OPTION))
                        {
                            isAllowEditItem = true;
                        }
                    }

                    if (dbDetail != null)
                    {
                        dtoDetail.FinalPrice = (
                                                        (dtoItem.CommissionPercent == null ? 0 : dtoItem.CommissionPercent) +
                                                        (dtoItem.SurChargePercent == null ? 0 : dtoItem.SurChargePercent) +
                                                        (dtoDetail.IncreasePercent == null ? 0 : dtoDetail.IncreasePercent)
                                                    )
                                                    / 100 * (dtoDetail.UnitPrice == null ? 0 : dtoDetail.UnitPrice)
                                                    + (dtoDetail.UnitPrice == null ? 0 : dtoDetail.UnitPrice);

                        // set update by for est purchasing price
                        if (dtoDetail.EstimatedPurchasingPrice != dbDetail.EstimatedPurchasingPrice)
                        {
                            dbDetail.EstimatedPurchasingPriceUpdatedByID = userId;
                        }

                        // set selected by for planing purchasing price
                        if (dtoDetail.PlaningPurchasingPriceSelectedDate == "just now")
                        {
                            dbDetail.PlaningPurchasingPriceSelectedBy = userId;
                            dbDetail.PlaningPurchasingPriceSelectedDate = DateTime.Now;
                        }

                        Mapper.Map(dtoDetail, dbDetail);

                        // only allow edit item in case item does not put in factory order detail
                        if (isAllowEditItem)
                        {
                            dbDetail.ModelID = dtoDetail.ModelID;
                            dbDetail.FrameMaterialID = dtoDetail.FrameMaterialID;
                            dbDetail.FrameMaterialColorID = dtoDetail.FrameMaterialColorID;
                            dbDetail.MaterialID = dtoDetail.MaterialID;
                            dbDetail.MaterialTypeID = dtoDetail.MaterialTypeID;
                            dbDetail.MaterialColorID = dtoDetail.MaterialColorID;
                            dbDetail.SubMaterialID = dtoDetail.SubMaterialID;
                            dbDetail.SubMaterialColorID = dtoDetail.SubMaterialColorID;
                            dbDetail.SeatCushionID = dtoDetail.SeatCushionID;
                            dbDetail.BackCushionID = dtoDetail.BackCushionID;
                            dbDetail.CushionColorID = dtoDetail.CushionColorID;
                            dbDetail.FSCTypeID = dtoDetail.FSCTypeID;
                            dbDetail.FSCPercentID = dtoDetail.FSCPercentID;
                        }
                    }
                }
            }

            /*
            * MAP & CHECK  OFFERLINE SPAREPART
            */
            List<OfferLineSparepart> needItemDelete = new List<OfferLineSparepart>();
            if (dtoItem.OfferLineSpareparts != null)
            {
                //CHECK
                foreach (OfferLineSparepart dbDetail in dbItem.OfferLineSparepart.Where(o => !dtoItem.OfferLineSpareparts.Select(s => s.OfferLineSparePartID).Contains(o.OfferLineSparePartID)))
                {
                    needItemDelete.Add(dbDetail);
                }
                foreach (OfferLineSparepart dbDetail in needItemDelete)
                {
                    dbItem.OfferLineSparepart.Remove(dbDetail);
                }
                //MAP
                foreach (DTO.OfferMng.OfferLineSparepart dtoDetail in dtoItem.OfferLineSpareparts)
                {
                    OfferLineSparepart dbDetail;
                    bool isAllowEditItem = true;
                    if (dtoDetail.OfferLineSparePartID < 0 || actionType != 1) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        dbDetail = new OfferLineSparepart();
                        dbItem.OfferLineSparepart.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.OfferLineSparepart.FirstOrDefault(o => o.OfferLineSparePartID == dtoDetail.OfferLineSparePartID);
                        var x = dbDetail.SaleOrderDetailSparepart.Where(o => o.FactoryOrderSparepartDetail != null && o.FactoryOrderSparepartDetail.Count() > 0);
                        isAllowEditItem = !(x != null && x.Count() > 0);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.OfferMng.OfferLineSparepart, OfferLineSparepart>(dtoDetail, dbDetail);

                        // only allow edit item in case item does not put in factory order detail
                        if (isAllowEditItem)
                        {
                            dbDetail.ModelID = dtoDetail.ModelID;
                            dbDetail.PartID = dtoDetail.PartID;
                        }
                    }
                }
            }

            /*
            * MAP & CHECK  OFFERLINE SAMPLE PRODUCT
            */
            List<OfferLineSampleProduct> toBeDeletedItems = new List<OfferLineSampleProduct>();
            if (dtoItem.OfferLineSampleProductDTOs != null)
            {
                //CHECK
                foreach (OfferLineSampleProduct dbSample in dbItem.OfferLineSampleProduct.Where(o => !dtoItem.OfferLineSampleProductDTOs.Select(s => s.OfferLineSampleProductID).Contains(o.OfferLineSampleProductID)).ToArray())
                {
                    dbItem.OfferLineSampleProduct.Remove(dbSample);
                }
                //MAP
                foreach (DTO.OfferMng.OfferLineSampleProductDTO dtoSample in dtoItem.OfferLineSampleProductDTOs)
                {
                    OfferLineSampleProduct dbSample = null;
                    if (dtoSample.OfferLineSampleProductID <= 0)
                    {
                        dbSample = new OfferLineSampleProduct();
                        dbItem.OfferLineSampleProduct.Add(dbSample);
                    }
                    else
                    {
                        dbSample = dbItem.OfferLineSampleProduct.FirstOrDefault(o => o.OfferLineSampleProductID == dtoSample.OfferLineSampleProductID);
                        if (dbSample == null)
                        {
                            throw new Exception("Sample item not found!");
                        }
                    }
                    AutoMapper.Mapper.Map<DTO.OfferMng.OfferLineSampleProductDTO, OfferLineSampleProduct>(dtoSample, dbSample);
                }
            }

            /*
             * SETUP FORMATED FIELD
             */
            //dbItem.OfferDate = Library.Helper.ConvertStringToDateTime(dtoItem.OfferDate, new System.Globalization.CultureInfo("vi-VN"));
            //dbItem.ValidUntil = Library.Helper.ConvertStringToDateTime(dtoItem.ValidUntil, new System.Globalization.CultureInfo("vi-VN"));
            //dbItem.LDS = Library.Helper.ConvertStringToDateTime(dtoItem.LDS, new System.Globalization.CultureInfo("vi-VN"));
            //dbItem.EstimatedDeliveryDate = Library.Helper.ConvertStringToDateTime(dtoItem.EstimatedDeliveryDate, new System.Globalization.CultureInfo("vi-VN"));
            Mapper.Map(dtoItem, dbItem);
            dbItem.OfferDate = dtoItem.OfferDateFormated.ConvertStringToDateTime();
            dbItem.ValidUntil = dtoItem.ValidUntilFormated.ConvertStringToDateTime();
            dbItem.LDS = dtoItem.LDSFormated.ConvertStringToDateTime();
            dbItem.EstimatedDeliveryDate = dtoItem.EstimatedDeliveryDateFormated.ConvertStringToDateTime();

            //var dtoEstimatedPurchasingPriceUpdatedBy = new DTO.OfferMng.OfferLine().EstimatedPurchasingPriceUpdatedByID = userId;
            //dtoItem.OfferLines.Select(x => x.EstimatedPurchasingPriceUpdatedByID = dtoEstimatedPurchasingPriceUpdatedBy).ToList();
            //var dbdtoEstimatedPurchasingPriceUpdatedBy = new OfferLine().EstimatedPurchasingPriceUpdatedByID = userId;
            //dbItem.OfferLine.Select(x => x.EstimatedPurchasingPriceUpdatedByID = dbdtoEstimatedPurchasingPriceUpdatedBy).ToList();

            //dtoEstimatedPurchasingPriceUpdatedBy = dbdtoEstimatedPurchasingPriceUpdatedBy;
        }

        public DTO.OfferMng.OfferLine DB2DTO_OfferLineEdit(OfferMng_Offer_View dbItem)
        {
            DTO.OfferMng.OfferLine dtoItem = AutoMapper.Mapper.Map<OfferMng_Offer_View, DTO.OfferMng.OfferLine>(dbItem);
            return dtoItem;
        }

        public DTO.OfferMng.OfferLineEdit DB2DTO_OfferLineEdit(OfferMng_OfferLine_EditView dbItem)
        {
            return AutoMapper.Mapper.Map<OfferMng_OfferLine_EditView, DTO.OfferMng.OfferLineEdit>(dbItem);
        }

        public DTO.OfferMng.Model DB2DTO_Model(OfferMng_Model_View dbItem)
        {
            return AutoMapper.Mapper.Map<OfferMng_Model_View, DTO.OfferMng.Model>(dbItem);
        }

        public DTO.OfferMng.FactoryMng_FactoryPermissionDTO DB2DTO_FactoryMng_FactoryPermission(FactoryMng_FactoryPermission_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMng_FactoryPermission_View, DTO.OfferMng.FactoryMng_FactoryPermissionDTO>(dbItem);
        }

        public DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO DB2DTO_FactoryMng_Function_GetFactoryPermission_Result(FactoryMng_Function_GetFactoryPermission_Result dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMng_Function_GetFactoryPermission_Result, DTO.OfferMng.FactoryMng_Function_GetFactoryPermission_ResultDTO>(dbItem);
        }

        public DTO.OfferMng.OfferOverView DB2DTO_OfferOverView(OfferMng_Offer_OverView dbItem)
        {
            DTO.OfferMng.OfferOverView dtoItem = AutoMapper.Mapper.Map<OfferMng_Offer_OverView, DTO.OfferMng.OfferOverView>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.OfferDate.HasValue)
                dtoItem.OfferDateFormated = dbItem.OfferDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.ValidUntil.HasValue)
                dtoItem.ValidUntilFormated = dbItem.ValidUntil.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS.HasValue)
                dtoItem.LDSFormated = dbItem.LDS.Value.ToString("dd/MM/yyyy");

            if (dbItem.EstimatedDeliveryDate.HasValue)
                dtoItem.EstimatedDeliveryDateFormated = dbItem.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }
    }
}