using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MaterialMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MaterialMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MaterialMng_MaterialSearchResult_View, DTO.MaterialMng.MaterialSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialMng_Material_View, DTO.MaterialMng.Material>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialMng.Material, Material>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialMng.MaterialSearchResult> DB2DTO_MaterialSearchResultList(List<MaterialMng_MaterialSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialMng_MaterialSearchResult_View>, List<DTO.MaterialMng.MaterialSearchResult>>(dbItems);
        }

        public DTO.MaterialMng.Material DB2DTO_Material(MaterialMng_Material_View dbItem)
        {
            return AutoMapper.Mapper.Map<MaterialMng_Material_View, DTO.MaterialMng.Material>(dbItem);
        }

        public void DTO2BD_Material(DTO.MaterialMng.Material dtoItem, ref Material dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MaterialMng.Material, Material>(dtoItem, dbItem);
        }
    }
}
