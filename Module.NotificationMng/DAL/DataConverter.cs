using AutoMapper;
using FrameworkSetting;
using Library;
using Module.NotificationMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "NotificationMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                Mapper.CreateMap<NotificationMng_NotificationGroup_View, DTO.NotificationGroupDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.NotificationGroupMemberData, o => o.MapFrom(s => s.NotificationMng_NotificationGroupMember_View))
                    .ForMember(d => d.NotificationGroupStatuses, o => o.MapFrom(s => s.NotificationMng_NotificationGroupStatus_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NotificationMng_NotificationGroupMember_View, DTO.NotificationGroupMemberDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                Mapper.CreateMap<NotificationMng_NotificationUser_View, DTO.Users>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<NotificationGroupMemberDTO, NotificationGroupMember>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.NotificationGroupMemberID, o => o.Ignore())
                .ForMember(d => d.NotificationGroupID, o => o.Ignore());


                Mapper.CreateMap<NotificationGroupDTO, NotificationGroup> ()
                .IgnoreAllNonExisting()
                .ForMember(d => d.NotificationGroupID, o => o.Ignore())
                .ForMember(d => d.NotificationGroupMember, o => o.Ignore())
                .ForMember(d => d.NotificationGroupStatus, o => o.Ignore());


                Mapper.CreateMap<NotificationMng_Module_View, ModuleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<NotificationMng_NotificationGroupStatus_View, NotificationGroupStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<NotificationMng_ModuleStatus_View, ModuleStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<NotificationGroupStatusDTO, NotificationGroupStatus>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.NotificationGroupStatusID, o => o.Ignore())
                   .ForMember(d => d.NotificationGroupID, o => o.Ignore());

                Mapper.CreateMap<ModuleStatusDTO, ModuleStatus>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModuleStatusID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }

        }

        public List<DTO.NotificationGroupDTO> DB2DTO_SearchResult(List<NotificationMng_NotificationGroup_View> dbItem)
        {
            return Mapper.Map<List<NotificationMng_NotificationGroup_View>, List<DTO.NotificationGroupDTO>>(dbItem);
        }
        
        public DTO.NotificationGroupDTO DB2DTO_NotificationGroup(NotificationMng_NotificationGroup_View dbItem)
        {
            return Mapper.Map<NotificationMng_NotificationGroup_View, DTO.NotificationGroupDTO>(dbItem);
        }

        public void DTO2DB_Update(DTO.NotificationGroupDTO dataItem, ref NotificationGroup dbItem)
        {
            //foreach (var item in dbItem.NotificationGroupMember)
            //{
            //    //if (!dataItem.NotificationGroupMemberData.Select(s => s.NotificationGroupMemberID).Contains(item.NotificationGroupMemberID))
            //        dbItem.NotificationGroupMember.Remove(item);
            //}

            if (dataItem.NotificationGroupMemberData != null)
            {
                foreach (var item in dbItem.NotificationGroupMember.ToArray())
                {
                    if (!dataItem.NotificationGroupMemberData.Select(s => s.NotificationGroupMemberID).Contains(item.NotificationGroupMemberID))
                    {
                        dbItem.NotificationGroupMember.Remove(item);
                    }
                }

                foreach (var dto in dataItem.NotificationGroupMemberData)
                {
                    NotificationGroupMember item;

                    if (dto.NotificationGroupMemberID < 0)
                    {
                        item = new NotificationGroupMember();

                        dbItem.NotificationGroupMember.Add(item);
                    }
                    else
                    {
                        item = dbItem.NotificationGroupMember.FirstOrDefault(s => s.NotificationGroupMemberID == dto.NotificationGroupMemberID);
                    }

                    if (item != null)
                    {
                        AutoMapper.Mapper.Map<DTO.NotificationGroupMemberDTO, NotificationGroupMember>(dto, item);
                    }
                }
            }
            if (dataItem.NotificationGroupStatuses != null)
            {
                foreach (var item in dbItem.NotificationGroupStatus.ToList())
                {
                    if (!dataItem.NotificationGroupStatuses.Select(s => s.NotificationGroupStatusID).Contains(item.NotificationGroupStatusID))
                    {
                        dbItem.NotificationGroupStatus.Remove(item);
                    }
                }

                foreach (var dto in dataItem.NotificationGroupStatuses.ToList())
                {
                    NotificationGroupStatus item;

                    if (dto.NotificationGroupStatusID < 0)
                    {
                        item = new NotificationGroupStatus();

                        dbItem.NotificationGroupStatus.Add(item);
                    }
                    else
                    {
                        item = dbItem.NotificationGroupStatus.FirstOrDefault(s => s.NotificationGroupStatusID == dto.NotificationGroupStatusID);
                    }

                    if (item != null)
                    {
                        AutoMapper.Mapper.Map<DTO.NotificationGroupStatusDTO, NotificationGroupStatus>(dto, item);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.NotificationGroupDTO, NotificationGroup>(dataItem, dbItem);
        }
        public List<ModuleDTO> DB2DTO_Module(List<NotificationMng_Module_View> dbItem)
        {
            return Mapper.Map<List<NotificationMng_Module_View>, List<ModuleDTO>>(dbItem);
        }
        public List<ModuleStatusDTO> DB2DTO_ModuleStatus(List<NotificationMng_ModuleStatus_View> dbItem)
        {
            return Mapper.Map<List<NotificationMng_ModuleStatus_View>, List<ModuleStatusDTO>>(dbItem);
        }
    }
}
