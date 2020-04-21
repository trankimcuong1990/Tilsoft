using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FrameMaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FrameMaterialMng_FrameMaterialSearchResult_View, DTO.FrameMaterialSearchResultDto>()                    
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialMng_FrameMaterial_View, DTO.FrameMaterialDto>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialDto, FrameMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FrameMaterialSearchResultDto> DB2DTO_FrameMaterialSearchResultList(List<FrameMaterialMng_FrameMaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FrameMaterialMng_FrameMaterialSearchResult_View>, List<DTO.FrameMaterialSearchResultDto>>(dbItems);
        }

        public DTO.FrameMaterialDto DB2DTO_FrameMaterial(FrameMaterialMng_FrameMaterial_View dbItem)
        {
            return AutoMapper.Mapper.Map<FrameMaterialMng_FrameMaterial_View, DTO.FrameMaterialDto>(dbItem);
        }

        public void DTO2BD(DTO.FrameMaterialDto dtoItem, ref FrameMaterial dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FrameMaterialDto, FrameMaterial>(dtoItem, dbItem);
        }
    }
}
