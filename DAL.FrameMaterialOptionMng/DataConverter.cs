using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.FrameMaterialOptionMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FrameMaterialOptionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FrameMaterialOptionMng_FrameMaterialOptionSearchResult_View, DTO.FrameMaterialOptionMng.FrameMaterialOptionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialOptionMng_FrameMaterialOptionProductGroup_View, DTO.FrameMaterialOptionMng.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialOptionMng_FrameMaterialOptionMaterialOption_View, DTO.FrameMaterialOptionMng.FrameMaterialOptionMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FrameMaterialOptionMng_FrameMaterialOption_View, DTO.FrameMaterialOptionMng.FrameMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ProductGroups, o => o.MapFrom(s => s.FrameMaterialOptionMng_FrameMaterialOptionProductGroup_View))
                    .ForMember(d => d.MaterialOptions, o => o.MapFrom(s => s.FrameMaterialOptionMng_FrameMaterialOptionMaterialOption_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialOptionMng.ProductGroup, FrameMaterialOptionProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialOptionProductGroupID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialOptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialOptionMng.FrameMaterialOptionMaterialOption, FrameMaterialOptionMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FrameMaterialOptionMaterialOptionID, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialOptionID, o => o.Ignore()); 

                AutoMapper.Mapper.CreateMap<DTO.FrameMaterialOptionMng.FrameMaterialOption, FrameMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.FrameMaterialOptionID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FrameMaterialOptionMng.FrameMaterialOptionSearchResult> DB2DTO_FrameMaterialOptionSearchResultList(List<FrameMaterialOptionMng_FrameMaterialOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FrameMaterialOptionMng_FrameMaterialOptionSearchResult_View>, List<DTO.FrameMaterialOptionMng.FrameMaterialOptionSearchResult>>(dbItems);
        }

        public DTO.FrameMaterialOptionMng.FrameMaterialOption DB2DTO_FrameMaterialOption(FrameMaterialOptionMng_FrameMaterialOption_View dbItem)
        {
            DTO.FrameMaterialOptionMng.FrameMaterialOption dtoItem =  AutoMapper.Mapper.Map<FrameMaterialOptionMng_FrameMaterialOption_View, DTO.FrameMaterialOptionMng.FrameMaterialOption>(dbItem);
            return dtoItem;
        }

        public void DTO2BD(DTO.FrameMaterialOptionMng.FrameMaterialOption dtoItem, ref FrameMaterialOption dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FrameMaterialOptionMng.FrameMaterialOption, FrameMaterialOption>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;

            if (dbItem.FrameMaterialOptionID == 0)
            {
                dbItem.CreatedBy = dtoItem.CreatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }

            // map child
            if (dtoItem.ProductGroups != null)
            {
                // map child rows
                foreach (DTO.FrameMaterialOptionMng.ProductGroup dtoGroup in dtoItem.ProductGroups)
                {
                    FrameMaterialOptionProductGroup dbGroup;
                    if (dtoGroup.FrameMaterialOptionProductGroupID <= 0)
                    {
                        dbGroup = new FrameMaterialOptionProductGroup();
                        dbItem.FrameMaterialOptionProductGroup.Add(dbGroup);
                    }
                    else
                    {
                        dbGroup = dbItem.FrameMaterialOptionProductGroup.FirstOrDefault(o => o.FrameMaterialOptionProductGroupID == dtoGroup.FrameMaterialOptionProductGroupID);
                    }

                    if (dbGroup != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FrameMaterialOptionMng.ProductGroup, FrameMaterialOptionProductGroup>(dtoGroup, dbGroup);
                    }
                }
            }

            // map pieces
            if (dtoItem.MaterialOptions != null)
            {
                // check for child rows deleted
                foreach (FrameMaterialOptionMaterialOption dbMaterialOption in dbItem.FrameMaterialOptionMaterialOption.ToArray())
                {
                    if (!dtoItem.MaterialOptions.Select(o => o.FrameMaterialOptionMaterialOptionID).Contains(dbMaterialOption.FrameMaterialOptionMaterialOptionID))
                    {
                        dbItem.FrameMaterialOptionMaterialOption.Remove(dbMaterialOption);
                    }
                }

                // map child rows
                foreach (DTO.FrameMaterialOptionMng.FrameMaterialOptionMaterialOption dtoMaterialOption in dtoItem.MaterialOptions)
                {
                    FrameMaterialOptionMaterialOption dbMaterialOption;
                    if (dtoMaterialOption.FrameMaterialOptionMaterialOptionID <= 0)
                    {
                        dbMaterialOption = new FrameMaterialOptionMaterialOption();
                        dbItem.FrameMaterialOptionMaterialOption.Add(dbMaterialOption);
                    }
                    else
                    {
                        dbMaterialOption = dbItem.FrameMaterialOptionMaterialOption.FirstOrDefault(o => o.FrameMaterialOptionMaterialOptionID == dtoMaterialOption.FrameMaterialOptionMaterialOptionID);
                    }

                    if (dbMaterialOption != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FrameMaterialOptionMng.FrameMaterialOptionMaterialOption, FrameMaterialOptionMaterialOption>(dtoMaterialOption, dbMaterialOption);
                    }
                }
            }
        }
    }
}
