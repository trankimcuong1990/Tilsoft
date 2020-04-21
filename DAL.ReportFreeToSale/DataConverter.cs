using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ReportFreeToSale
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ReportFreeToSaleMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ReportMng_FreeToSaleOverview_View, DTO.ReportFreeToSale.FreeToSale>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SelectedThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SelectedThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.SelectedThumbnailImage)))
                    .ForMember(d => d.SelectedFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SelectedFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SelectedFullImage)))
                    .ForMember(d => d.CushionImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.CushionImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.CushionImage)))
                    .ForMember(d => d.MaterialImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.MaterialImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.MaterialImage)))

                    .ForMember(d => d.ProductThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ProductThumbnailImage)))
                    .ForMember(d => d.ProductGardenThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductGardenThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ProductGardenThumbnailImage)))
                    .ForMember(d => d.ModelThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ModelThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ModelThumbnailImage)))
                    .ForMember(d => d.ProductFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ProductFullImage)))
                    .ForMember(d => d.ProductGardenFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ProductGardenFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ProductGardenFullImage)))
                    .ForMember(d => d.ModelFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ModelFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ModelFullImage)))

                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.ReportFreeToSale.FreeToSale> DB2DTO_FreeToSaleSearch(List<ReportMng_FreeToSaleOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_FreeToSaleOverview_View>, List<DTO.ReportFreeToSale.FreeToSale>>(dbItems);
        }

    }
}
