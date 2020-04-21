using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.CushionMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CushionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CushionMng_CushionSearchResult_View, DTO.CushionMng.CushionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_FullSizeUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ImageFile_FullSizeUrl) ? s.ImageFile_FullSizeUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CushionMng_Cushion_View, DTO.CushionMng.Cushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CushionMng.Cushion, Cushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForMember(d => d.CushionID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CushionMng.CushionSearchResult> DB2DTO_CushionSearchResultList(List<CushionMng_CushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CushionMng_CushionSearchResult_View>, List<DTO.CushionMng.CushionSearchResult>>(dbItems);
        }

        public DTO.CushionMng.Cushion DB2DTO_Cushion(CushionMng_Cushion_View dbItem)
        {
            return AutoMapper.Mapper.Map<CushionMng_Cushion_View, DTO.CushionMng.Cushion>(dbItem);
        }

        public void DTO2BD(DTO.CushionMng.Cushion dtoItem, ref Cushion dbItem)
        {
            AutoMapper.Mapper.Map<DTO.CushionMng.Cushion, Cushion>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
