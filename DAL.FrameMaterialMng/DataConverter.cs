using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.FrameMaterialMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FrameMaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FrameMaterialMng_FrameMaterialSearchResult_View, DTO.FrameMaterialMng.FrameMaterialSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialMng_FrameMaterial_View, DTO.FrameMaterialMng.FrameMaterial>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialMng.FrameMaterial, FrameMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FrameMaterialMng.FrameMaterialSearchResult> DB2DTO_FrameMaterialSearchResultList(List<FrameMaterialMng_FrameMaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FrameMaterialMng_FrameMaterialSearchResult_View>, List<DTO.FrameMaterialMng.FrameMaterialSearchResult>>(dbItems);
        }

        public DTO.FrameMaterialMng.FrameMaterial DB2DTO_FrameMaterial(FrameMaterialMng_FrameMaterial_View dbItem)
        {
            return AutoMapper.Mapper.Map<FrameMaterialMng_FrameMaterial_View, DTO.FrameMaterialMng.FrameMaterial>(dbItem);
        }

        public void DTO2BD(DTO.FrameMaterialMng.FrameMaterial dtoItem, ref FrameMaterial dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FrameMaterialMng.FrameMaterial, FrameMaterial>(dtoItem, dbItem);
        }
    }
}
