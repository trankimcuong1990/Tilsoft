using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using System.Globalization;

namespace Module.OrganigramMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OrganigramMng_OrganigramSearchResult_View, DTO.OrganigramSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OrganigramMng_Organigram_View, DTO.Employee>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OrganigramMng_OrganizationChart_View, DTO.OrganizationChart>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                    .ForMember(d => d.WorkSpaceFileThumbnail, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.WorkSpaceFileThumbnail) ? s.WorkSpaceFileThumbnail : "no-image.jpg")))
                    .ForMember(d => d.WorkSpaceFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.WorkSpaceFileLocation) ? s.WorkSpaceFileLocation : "")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.OrganizationChart, Organigram>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.OrganigramID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.OrganigramSearchResult> DB2DTO_OrganigramSearchResultList(List<OrganigramMng_OrganigramSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OrganigramMng_OrganigramSearchResult_View>, List<DTO.OrganigramSearchResult>>(dbItems);
        }

        public DTO.OrganizationChart DB2DTO_OrganizationChart(OrganigramMng_OrganizationChart_View dbItems)
        {
            return AutoMapper.Mapper.Map<OrganigramMng_OrganizationChart_View, DTO.OrganizationChart>(dbItems);
        }

        public List<DTO.Employee> DB2DTO_Organigram(List<OrganigramMng_Organigram_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<OrganigramMng_Organigram_View>, List<DTO.Employee>>(dbItem);
        }

        public void DTO2BD(DTO.OrganizationChart dtoItem, ref Organigram dbItem)
        {
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
            AutoMapper.Mapper.Map<DTO.OrganizationChart, Organigram>(dtoItem, dbItem);
        }
    }
}
