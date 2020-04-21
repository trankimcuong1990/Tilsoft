using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.SubMaterialOptionMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SubMaterialOptionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SubMaterialOptionMng_SubMaterialOptionSearchResult_View, DTO.SubMaterialOptionMng.SubMaterialOptionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SubMaterialOptionMng_SubMaterialOption_View, DTO.SubMaterialOptionMng.SubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SubMaterialOptionMng.SubMaterialOption, SubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SubMaterialOptionID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SubMaterialOptionMng.SubMaterialOptionSearchResult> DB2DTO_SubMaterialOptionSearchResultList(List<SubMaterialOptionMng_SubMaterialOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SubMaterialOptionMng_SubMaterialOptionSearchResult_View>, List<DTO.SubMaterialOptionMng.SubMaterialOptionSearchResult>>(dbItems);
        }

        public DTO.SubMaterialOptionMng.SubMaterialOption DB2DTO_SubMaterialOption(SubMaterialOptionMng_SubMaterialOption_View dbItem)
        {
            DTO.SubMaterialOptionMng.SubMaterialOption dtoItem = AutoMapper.Mapper.Map<SubMaterialOptionMng_SubMaterialOption_View, DTO.SubMaterialOptionMng.SubMaterialOption>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);
            return dtoItem;
        }

        public void DTO2BD(DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, ref SubMaterialOption dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SubMaterialOptionMng.SubMaterialOption, SubMaterialOption>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            if (dtoItem.SubMaterialOptionID == 0)
            {
                dbItem.CreatedBy = dtoItem.CreatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }
            
        }
    }
}
