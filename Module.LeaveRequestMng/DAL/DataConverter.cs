using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.LeaveRequestMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<LeaveRequestMng_LeaveRequestSearchResult_View, DTO.LeaveRequestSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FromDate, o => o.ResolveUsing(s => s.FromDate.HasValue ? s.FromDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ToDate, o => o.ResolveUsing(s => s.ToDate.HasValue ? s.ToDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LeaveRequestMng_LeaveRequest_View, DTO.LeaveRequest>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FromDate, o => o.ResolveUsing(s => s.FromDate.HasValue ? s.FromDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ToDate, o => o.ResolveUsing(s => s.ToDate.HasValue ? s.ToDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.LeaveRequest, LeaveRequest>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FromDate, o => o.Ignore())
                    .ForMember(d => d.ToDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.LeaveRequestID, o => o.Ignore());

                //List Manager
                AutoMapper.Mapper.CreateMap<LeaveRequestMng_Manager_View, DTO.Manager>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //List Director
                AutoMapper.Mapper.CreateMap<LeaveRequestMng_Director_View, DTO.Director>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.LeaveRequestSearchResult> DB2DTO_LeaveRequestSearchResultList(List<LeaveRequestMng_LeaveRequestSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LeaveRequestMng_LeaveRequestSearchResult_View>, List<DTO.LeaveRequestSearchResult>>(dbItems);
        }
        public DTO.LeaveRequest DB2DTO_LeaveRequest(LeaveRequestMng_LeaveRequest_View dbItem)
        {
            return AutoMapper.Mapper.Map<LeaveRequestMng_LeaveRequest_View, DTO.LeaveRequest>(dbItem);
        }
        public void DTO2BD(DTO.LeaveRequest dtoItem, ref LeaveRequest dbItem)
        {
            dbItem.FromDate = dtoItem.FromDate.ConvertStringToDateTime();
            dbItem.ToDate = dtoItem.ToDate.ConvertStringToDateTime();
            AutoMapper.Mapper.Map<DTO.LeaveRequest, LeaveRequest>(dtoItem, dbItem);
        }
        public List<DTO.Manager> DB2DTO_Manager(List<LeaveRequestMng_Manager_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LeaveRequestMng_Manager_View>, List<DTO.Manager>>(dbItems);
        }
        public List<DTO.Director> DB2DTO_Director(List<LeaveRequestMng_Director_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LeaveRequestMng_Director_View>, List<DTO.Director>>(dbItems);
        }
    }
}
