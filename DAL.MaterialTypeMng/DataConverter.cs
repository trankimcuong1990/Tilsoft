using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.MaterialTypeMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MaterialTypeMng";
            if(!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MaterialTypeMng_MaterialTypeSearchResult_View, DTO.MaterialTypeMng.MaterialTypeSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialTypeMng_MaterialType_View, DTO.MaterialTypeMng.MaterialType>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialTypeMng.MaterialType, MaterialType>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTypeID, o => o.Ignore())
                    .ForMember(d => d.MaterialTypeUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialTypeMng.MaterialTypeSearchResult> DB2DTO_MaterialTypeSearchResultList(List<MaterialTypeMng_MaterialTypeSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialTypeMng_MaterialTypeSearchResult_View>, List<DTO.MaterialTypeMng.MaterialTypeSearchResult>>(dbItems);
        }

        public DTO.MaterialTypeMng.MaterialType DB2DTO_MaterialType(MaterialTypeMng_MaterialType_View dbItem)
        {
            return AutoMapper.Mapper.Map<MaterialTypeMng_MaterialType_View, DTO.MaterialTypeMng.MaterialType>(dbItem);
        }

        public void DTO2BD_MaterialType(DTO.MaterialTypeMng.MaterialType dtoItem, ref MaterialType dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MaterialTypeMng.MaterialType, MaterialType>(dtoItem, dbItem);
        }
    }
}
