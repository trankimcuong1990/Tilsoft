using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SeatCushionMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SeatCushionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SeatCushionMng_SeatCushionSearchResult_View, DTO.SeatCushionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SeatCushionMng_SeatCushionProductGroup_View, DTO.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SeatCushionMng_SeatCushion_View, DTO.SeatCushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))                    
                    .ForMember(d => d.ProductGroups, o => o.MapFrom(s => s.SeatCushionMng_SeatCushionProductGroup_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SeatCushion, SeatCushion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForMember(d => d.SeatCushionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductGroup, SeatCushionProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SeatCushionProductGroupID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<SeatCushionMng_SeatCushionProductGroup_View, DTO.spProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.spProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.spProductGroup> DB2DTO_ProductGroup(List<SeatCushionMng_SeatCushionProductGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SeatCushionMng_SeatCushionProductGroup_View>, List<DTO.spProductGroup>>(dbItems);
        }

        public List<DTO.spProductGroup> DB2DTO_spProductGroup(List<SupportMng_ProductGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.spProductGroup>>(dbItems);
        }

        public List<DTO.SeatCushionSearchResult> DB2DTO_SeatCushionSearchResultList(List<SeatCushionMng_SeatCushionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SeatCushionMng_SeatCushionSearchResult_View>, List<DTO.SeatCushionSearchResult>>(dbItems);
        }

        public DTO.SeatCushion DB2DTO_SeatCushion(SeatCushionMng_SeatCushion_View dbItem)
        {
            return AutoMapper.Mapper.Map<SeatCushionMng_SeatCushion_View, DTO.SeatCushion>(dbItem);
        }

        public void DTO2BD(DTO.SeatCushion dtoItem, ref SeatCushion dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SeatCushion, SeatCushion>(dtoItem, dbItem);

            // Tri Add created date for the Seat Cushion
            if (dtoItem.SeatCushionID == 0)
            {
                dbItem.CreatedBy = dtoItem.CreatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }
            else
            {
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
                dbItem.UpdatedDate = DateTime.Now;
            }

            // map child
            if (dtoItem.ProductGroups != null)
            {
                // map child rows
                foreach (DTO.ProductGroup dtoGroup in dtoItem.ProductGroups)
                {
                    SeatCushionProductGroup dbGroup;
                    if (dtoGroup.SeatCushionProductGroupID <= 0)
                    {
                        dbGroup = new SeatCushionProductGroup();
                        dbItem.SeatCushionProductGroup.Add(dbGroup);
                    }
                    else
                    {
                        dbGroup = dbItem.SeatCushionProductGroup.FirstOrDefault(o => o.SeatCushionProductGroupID == dtoGroup.SeatCushionProductGroupID);
                    }

                    if (dbGroup != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductGroup, SeatCushionProductGroup>(dtoGroup, dbGroup);
                    }
                }
            }
        }
    }
}
