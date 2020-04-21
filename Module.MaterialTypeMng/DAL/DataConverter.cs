using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTypeMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MaterialTypeMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MaterialTypeMng_MaterialTypeSearchResult_View, DTO.MaterialTypeSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialTypeMng_MaterialType_View, DTO.MaterialTypes>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialTypes, MaterialType>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTypeID, o => o.Ignore())
                    .ForMember(d => d.MaterialTypeUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialTypeSearchResult> DB2DTO_MaterialTypeSearchResultList(List<MaterialTypeMng_MaterialTypeSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialTypeMng_MaterialTypeSearchResult_View>, List<DTO.MaterialTypeSearchResult>>(dbItems);
        }

        public DTO.MaterialTypes DB2DTO_MaterialType(MaterialTypeMng_MaterialType_View dbItem)
        {
            return AutoMapper.Mapper.Map<MaterialTypeMng_MaterialType_View, DTO.MaterialTypes>(dbItem);
        }

        public void DTO2BD_MaterialType(DTO.MaterialTypes dtoItem, ref MaterialType dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MaterialTypes, MaterialType>(dtoItem, dbItem);
        }
    }
}
