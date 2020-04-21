using Library;
using Module.FrameMaterialColorMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialColorMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FrameMaterialColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FrameMaterialColorMng_FrameMaterialColorSearchResult_View, FrameMaterialColorSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialColorMng_FrameMaterialColor_View, FrameMaterialColorDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialColorDTO, FrameMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialColorID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialColorUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<FrameMaterialColorSearchResult> DB2DTO_FrameMaterialColorSearchResultList(List<FrameMaterialColorMng_FrameMaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FrameMaterialColorMng_FrameMaterialColorSearchResult_View>, List<FrameMaterialColorSearchResult>>(dbItems);
        }

        public FrameMaterialColorDTO DB2DTO_FrameMaterialColor(FrameMaterialColorMng_FrameMaterialColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<FrameMaterialColorMng_FrameMaterialColor_View, FrameMaterialColorDTO>(dbItem);
        }

        public void DTO2BD(FrameMaterialColorDTO dtoItem, ref FrameMaterialColor dbItem)
        {
            AutoMapper.Mapper.Map<FrameMaterialColorDTO, FrameMaterialColor>(dtoItem, dbItem);
        }
    }
}
