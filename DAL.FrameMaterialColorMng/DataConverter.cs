using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.FrameMaterialColorMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FrameMaterialColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FrameMaterialColorMng_FrameMaterialColorSearchResult_View, DTO.FrameMaterialColorMng.FrameMaterialColorSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialColorMng_FrameMaterialColor_View, DTO.FrameMaterialColorMng.FrameMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialColorMng.FrameMaterialColor, FrameMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialColorID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialColorUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FrameMaterialColorMng.FrameMaterialColorSearchResult> DB2DTO_FrameMaterialColorSearchResultList(List<FrameMaterialColorMng_FrameMaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FrameMaterialColorMng_FrameMaterialColorSearchResult_View>, List<DTO.FrameMaterialColorMng.FrameMaterialColorSearchResult>>(dbItems);
        }

        public DTO.FrameMaterialColorMng.FrameMaterialColor DB2DTO_FrameMaterialColor(FrameMaterialColorMng_FrameMaterialColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<FrameMaterialColorMng_FrameMaterialColor_View, DTO.FrameMaterialColorMng.FrameMaterialColor>(dbItem);
        }

        public void DTO2BD(DTO.FrameMaterialColorMng.FrameMaterialColor dtoItem, ref FrameMaterialColor dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FrameMaterialColorMng.FrameMaterialColor, FrameMaterialColor>(dtoItem, dbItem);
        }
    }
}
