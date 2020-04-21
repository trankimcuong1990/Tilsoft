using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.UserGroupMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "UserGroupMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<UserGroupMng_UserGroupSearchResult_View, DTO.UserGroupMng.UserGroupSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserGroupMng_UserGroupPermission_View, DTO.UserGroupMng.UserGroupPermission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserGroupMng_UserGroup_View, DTO.UserGroupMng.UserGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Permissions, o => o.MapFrom(s => s.UserGroupMng_UserGroupPermission_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.UserGroupMng.UserGroup, UserGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UserGroupID, o => o.Ignore());


                AutoMapper.Mapper.CreateMap<DTO.UserGroupMng.UserGroupPermission, UserGroupPermission>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UserGroupID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.UserGroupMng.UserGroupSearchResult> DB2DTO_UserGroupSearchResultList(List<UserGroupMng_UserGroupSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UserGroupMng_UserGroupSearchResult_View>, List<DTO.UserGroupMng.UserGroupSearchResult>>(dbItems);
        }

        public DTO.UserGroupMng.UserGroup DB2DTO_UserGroup(UserGroupMng_UserGroup_View dbItem)
        {
            return AutoMapper.Mapper.Map<UserGroupMng_UserGroup_View, DTO.UserGroupMng.UserGroup>(dbItem);
        }

        public void DTO2DB(DTO.UserGroupMng.UserGroup dtoItem, ref UserGroup dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.UserGroupMng.UserGroup, UserGroup>(dtoItem, dbItem);

            // map factory access
            if (dtoItem.Permissions != null)
            {
                // check for child rows deleted
                foreach (UserGroupPermission dbPermission in dbItem.UserGroupPermission.ToArray())
                {
                    if (!dtoItem.Permissions.Select(o => o.UserGroupPermissionID).Contains(dbPermission.UserGroupPermissionID))
                    {
                        dbItem.UserGroupPermission.Remove(dbPermission);
                    }
                }

                // map child rows
                foreach (DTO.UserGroupMng.UserGroupPermission dtoPermission in dtoItem.Permissions)
                {
                    UserGroupPermission dbPermission;
                    if (dtoPermission.UserGroupPermissionID <= 0)
                    {
                        dbPermission = new UserGroupPermission();
                        dbItem.UserGroupPermission.Add(dbPermission);
                    }
                    else
                    {
                        dbPermission = dbItem.UserGroupPermission.FirstOrDefault(o => o.UserGroupPermissionID == dtoPermission.UserGroupPermissionID);
                    }

                    if (dbPermission != null)
                    {
                        AutoMapper.Mapper.Map<DTO.UserGroupMng.UserGroupPermission, UserGroupPermission>(dtoPermission, dbPermission);
                    }
                }
            }
        }
    }
}
