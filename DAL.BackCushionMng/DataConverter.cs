using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.BackCushionMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "BackCushionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<BackCushionMng_BackCushionSearchResult_View, DTO.BackCushionMng.BackCushionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackCushionMng_BackCushionProductGroup_View, DTO.BackCushionMng.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackCushionMng_BackCushion_View, DTO.BackCushionMng.BackCushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.ProductGroups, o => o.MapFrom(s => s.BackCushionMng_BackCushionProductGroup_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BackCushionMng.BackCushion, BackCushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForMember(d => d.BackCushionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BackCushionMng.ProductGroup, BackCushionProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BackCushionProductGroupID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.BackCushionMng.BackCushionSearchResult> DB2DTO_BackCushionSearchResultList(List<BackCushionMng_BackCushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BackCushionMng_BackCushionSearchResult_View>, List<DTO.BackCushionMng.BackCushionSearchResult>>(dbItems);
        }

        public DTO.BackCushionMng.BackCushion DB2DTO_BackCushion(BackCushionMng_BackCushion_View dbItem)
        {
            return AutoMapper.Mapper.Map<BackCushionMng_BackCushion_View, DTO.BackCushionMng.BackCushion>(dbItem);
        }

        public void DTO2BD(DTO.BackCushionMng.BackCushion dtoItem, ref BackCushion dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BackCushionMng.BackCushion, BackCushion>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;


            // Tri Add created date for BackCushion
            if(dtoItem.BackCushionID == 0)
            {
                dbItem.CreatedBy = dtoItem.CreatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }

            // map child
            if (dtoItem.ProductGroups != null)
            {
                // map child rows
                foreach (DTO.BackCushionMng.ProductGroup dtoGroup in dtoItem.ProductGroups)
                {
                    BackCushionProductGroup dbGroup;
                    if (dtoGroup.BackCushionProductGroupID <= 0)
                    {
                        dbGroup = new BackCushionProductGroup();
                        dbItem.BackCushionProductGroup.Add(dbGroup);
                    }
                    else
                    {
                        dbGroup = dbItem.BackCushionProductGroup.FirstOrDefault(o => o.BackCushionProductGroupID == dtoGroup.BackCushionProductGroupID);
                    }

                    if (dbGroup != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BackCushionMng.ProductGroup, BackCushionProductGroup>(dtoGroup, dbGroup);
                    }
                }
            }
        }
    }
}
