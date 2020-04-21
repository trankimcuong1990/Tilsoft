using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.SubMaterialColorMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SubMaterialColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SubMaterialColorMng_SubMaterialColorSearchResult_View, DTO.SubMaterialColorMng.SubMaterialColorSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SubMaterialColorMng_SubMaterialColor_View, DTO.SubMaterialColorMng.SubMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SubMaterialColorMng.SubMaterialColor, SubMaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SubMaterialColorID, o => o.Ignore())
                    .ForMember(d => d.SubMaterialColorUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SubMaterialColorMng.SubMaterialColorSearchResult> DB2DTO_SubMaterialColorSearchResultList(List<SubMaterialColorMng_SubMaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SubMaterialColorMng_SubMaterialColorSearchResult_View>, List<DTO.SubMaterialColorMng.SubMaterialColorSearchResult>>(dbItems);
        }

        public DTO.SubMaterialColorMng.SubMaterialColor DB2DTO_SubMaterialColor(SubMaterialColorMng_SubMaterialColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<SubMaterialColorMng_SubMaterialColor_View, DTO.SubMaterialColorMng.SubMaterialColor>(dbItem);
        }

        public void DTO2BD(DTO.SubMaterialColorMng.SubMaterialColor dtoItem, ref SubMaterialColor dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SubMaterialColorMng.SubMaterialColor, SubMaterialColor>(dtoItem, dbItem);
        }
    }
}
