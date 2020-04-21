using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.SubMaterialMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SubMaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SubMaterialMng_SubMaterialSearchResult_View, DTO.SubMaterialMng.SubMaterialSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SubMaterialMng_SubMaterial_View, DTO.SubMaterialMng.SubMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SubMaterialMng.SubMaterial, SubMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SubMaterialID, o => o.Ignore())
                    .ForMember(d => d.SubMaterialUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SubMaterialMng.SubMaterialSearchResult> DB2DTO_SubMaterialSearchResultList(List<SubMaterialMng_SubMaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SubMaterialMng_SubMaterialSearchResult_View>, List<DTO.SubMaterialMng.SubMaterialSearchResult>>(dbItems);
        }

        public DTO.SubMaterialMng.SubMaterial DB2DTO_SubMaterial(SubMaterialMng_SubMaterial_View dbItem)
        {
            return AutoMapper.Mapper.Map<SubMaterialMng_SubMaterial_View, DTO.SubMaterialMng.SubMaterial>(dbItem);
        }

        public void DTO2BD(DTO.SubMaterialMng.SubMaterial dtoItem, ref SubMaterial dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SubMaterialMng.SubMaterial, SubMaterial>(dtoItem, dbItem);
        }
    }
}
