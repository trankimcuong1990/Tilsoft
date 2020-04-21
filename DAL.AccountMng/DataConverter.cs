using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using DAL.AccountMng;

namespace DAL.AccountMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "AccountMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AccountMng_UserProfile_View, DTO.AccountMng.User>()                    
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Permissions, o => o.MapFrom(s => s.AccountMng_UserPermission_View))
                    .ForMember(d => d.ModuleSpecialPermissionAccesses, o => o.MapFrom(s => s.AccountMng_ModuleSpecialPermissionAccess_View))
                    .ForMember(d => d.Branches, o => o.MapFrom(s => s.AccountMng_Branch_View))
                    .ForMember(d => d.PersonalPhoto_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.PersonalPhoto_DisplayUrl) ? s.PersonalPhoto_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.SignatureImage_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.SignatureImage_DisplayUrl) ? s.SignatureImage_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountMng_UserPermission_View, DTO.AccountMng.UserPermission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountMng_Branch_View, DTO.AccountMng.Branch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountMng_ModuleSpecialPermissionAccess_View, DTO.AccountMng.ModuleSpecialPermissionAccess>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountMng_UserProfile_View, DTO.AccountMng.User>()
                    .ForMember(d => d.Permissions, o => o.MapFrom(s => s.AccountMng_UserPermission_View))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccountMng_UserPermission_View, DTO.AccountMng.UserPermission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.AccountMng.User DB2DTO_User(AccountMng_UserProfile_View dbItem)
        {
            return AutoMapper.Mapper.Map<AccountMng_UserProfile_View, DTO.AccountMng.User>(dbItem);
        }

        public List<DTO.AccountMng.User> DB2DTO_UserList(List<AccountMng_UserProfile_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccountMng_UserProfile_View>, List<DTO.AccountMng.User>>(dbItems);
        }
    }
}
