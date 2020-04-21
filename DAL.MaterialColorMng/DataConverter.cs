using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.MaterialColorMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MaterialColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MaterialColorMng_MaterialColorSearchResult_View, DTO.MaterialColorMng.MaterialColorSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialColorMng_MaterialColor_View, DTO.MaterialColorMng.MaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialColorMng.MaterialColor, MaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialColorID, o => o.Ignore())
                    .ForMember(d => d.MaterialColorUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialColorMng.MaterialColorSearchResult> DB2DTO_MaterialColorSearchResultList(List<MaterialColorMng_MaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialColorMng_MaterialColorSearchResult_View>, List<DTO.MaterialColorMng.MaterialColorSearchResult>>(dbItems);
        }

        public DTO.MaterialColorMng.MaterialColor DB2DTO_MaterialColor(MaterialColorMng_MaterialColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<MaterialColorMng_MaterialColor_View, DTO.MaterialColorMng.MaterialColor>(dbItem);
        }

        public void DTO2BD_MaterialColor(DTO.MaterialColorMng.MaterialColor dtoItem, ref MaterialColor dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MaterialColorMng.MaterialColor, MaterialColor>(dtoItem, dbItem);
        }
    }
}
