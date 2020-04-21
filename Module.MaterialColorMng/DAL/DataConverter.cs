using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialColorMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MaterialColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MaterialColorMng_MaterialColorSearchResult_View, DTO.MaterialColorSearchResultData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialColorMng_MaterialColor_View, DTO.MaterialColorData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialColorData, MaterialColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialColorID, o => o.Ignore())
                    .ForMember(d => d.MaterialColorUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialColorSearchResultData> DB2DTO_MaterialColorSearchResultList(List<MaterialColorMng_MaterialColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialColorMng_MaterialColorSearchResult_View>, List<DTO.MaterialColorSearchResultData>>(dbItems);
        }

        public DTO.MaterialColorData DB2DTO_MaterialColor(MaterialColorMng_MaterialColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<MaterialColorMng_MaterialColor_View, DTO.MaterialColorData>(dbItem);
        }

        public void DTO2BD_MaterialColor(DTO.MaterialColorData dtoItem, ref MaterialColor dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MaterialColorData, MaterialColor>(dtoItem, dbItem);
        }
    }
}
