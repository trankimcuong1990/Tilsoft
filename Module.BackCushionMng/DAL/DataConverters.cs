using Library;
using Module.BackCushionMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BackCushionMng.DAL
{
    internal class DataConverters
    {
        public DataConverters()
        {
            string mapName = "BackCushionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<BackCushionMng_BackCushionSearchResult_View, BackCushionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackCushionMng_BackCushionProductGroup_View, ProductGroupDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackCushionMng_BackCushion_View, BackCushionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.ProductGroups, o => o.MapFrom(s => s.BackCushionMng_BackCushionProductGroup_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BackCushionDTO, BackCushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForMember(d => d.BackCushionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ProductGroupDTO, BackCushionProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BackCushionProductGroupID, o => o.Ignore());

                // product group 
                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, ProductGroupDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<ProductGroupDTO> DB2DTO_ProductGroup(List<SupportMng_ProductGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<ProductGroupDTO>>(dbItems);
        }
        public List<BackCushionSearchResult> DB2DTO_BackCushionSearchResultList(List<BackCushionMng_BackCushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BackCushionMng_BackCushionSearchResult_View>, List<BackCushionSearchResult>>(dbItems);
        }

        public BackCushionDTO DB2DTO_BackCushion(BackCushionMng_BackCushion_View dbItem)
        {
            return AutoMapper.Mapper.Map<BackCushionMng_BackCushion_View, BackCushionDTO>(dbItem);
        }

        public void DTO2BD(BackCushionDTO dtoItem, ref BackCushion dbItem)
        {
            AutoMapper.Mapper.Map<BackCushionDTO, BackCushion>(dtoItem, dbItem);            
                     
            // map child
            if (dtoItem.ProductGroups != null)
            {
                // map child rows
                foreach (ProductGroupDTO dtoGroup in dtoItem.ProductGroups)
                {
                    BackCushionProductGroup dbGroup;
                    if (dtoGroup.BackCushionProductGroupID <= 0)
                    {
                        dbGroup = new BackCushionProductGroup();
                        dbItem.BackCushionProductGroup.Add(dbGroup);
                        dbItem.CreatedBy = dtoItem.CreatedBy;
                        dbItem.CreatedDate =  DateTime.Now;
                    }
                    else
                    {
                        dbGroup = dbItem.BackCushionProductGroup.FirstOrDefault(o => o.BackCushionProductGroupID == dtoGroup.BackCushionProductGroupID);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;
                    }

                    if (dbGroup != null)
                    {
                        AutoMapper.Mapper.Map<ProductGroupDTO, BackCushionProductGroup>(dtoGroup, dbGroup);
                    }
                }
            }
        }
    }
}
