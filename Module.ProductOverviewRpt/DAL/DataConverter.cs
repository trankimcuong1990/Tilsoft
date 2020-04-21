using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.ProductOverviewRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductOverviewRpt_Model_View, DTO.ModelDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.PriceOverviewDTOs, o => o.MapFrom(s => s.ProductOverviewRpt_PriceOverview_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductOverviewRpt_PriceOverview_View, DTO.PriceOverviewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductOverviewRpt_PriceComparison_ProductOptionDetail_View, DTO.PriceComparison.ProductOptionDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductOverviewRpt_PriceComparison_PriceOfferHistory_View, DTO.PriceComparison.PriceOfferHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductOverviewRpt_OfferSeasonDetail_View, DTO.PriceComparison.ProductOptionDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.ModelDTO DB2DTO_Model(ProductOverviewRpt_Model_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductOverviewRpt_Model_View, DTO.ModelDTO>(dbItem);
        }

        public DTO.PriceComparison.ProductOptionDetailDTO DB2DTO_PriceComparison_ProductOptionDetail(ProductOverviewRpt_PriceComparison_ProductOptionDetail_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductOverviewRpt_PriceComparison_ProductOptionDetail_View, DTO.PriceComparison.ProductOptionDetailDTO>(dbItem);
        }

        public List<DTO.PriceComparison.ProductOptionDetailDTO> DB2DTO_PriceComparison_ProductOptionDetailList(List<ProductOverviewRpt_PriceComparison_ProductOptionDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductOverviewRpt_PriceComparison_ProductOptionDetail_View>, List<DTO.PriceComparison.ProductOptionDetailDTO>>(dbItems);
        }

        public List<DTO.PriceComparison.PriceOfferHistoryDTO> DB2DTO_PriceComparison_PriceOfferHistoryList(List<ProductOverviewRpt_PriceComparison_PriceOfferHistory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductOverviewRpt_PriceComparison_PriceOfferHistory_View>, List<DTO.PriceComparison.PriceOfferHistoryDTO>>(dbItems);
        }
    }
}
