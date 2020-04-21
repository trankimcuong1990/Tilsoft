using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.UserMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "UserMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<UserMng_UserProfileSearchResult_View, DTO.UserMng.UserProfileSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LastLoginDate, o => o.ResolveUsing(s => s.LastLoginDate.HasValue ? s.LastLoginDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.PersonalPhoto_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.PersonalPhoto_DisplayURL) ? s.PersonalPhoto_DisplayURL : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserMng_UserFactoryAccess_View, DTO.UserMng.FactoryAccess>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserMng_UserPermission_View, DTO.UserMng.UserPermission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserMng_UserProfile_View, DTO.UserMng.UserProfile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy H:m") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy H:m") : ""))
                    .ForMember(d => d.LastLoginDate, o => o.ResolveUsing(s => s.LastLoginDate.HasValue ? s.LastLoginDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryAccesses, o => o.MapFrom(s => s.UserMng_UserFactoryAccess_View))
                    .ForMember(d => d.UserPermissions, o => o.MapFrom(s => s.UserMng_UserPermission_View))
                    .ForMember(d => d.AffectivePermissions, o => o.Ignore())
                    .ForMember(d => d.PersonalPhoto_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.PersonalPhoto_DisplayURL) ? s.PersonalPhoto_DisplayURL : "no-image.jpg")))
                    .ForMember(d => d.SignatureImage_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.SignatureImage_DisplayURL) ? s.SignatureImage_DisplayURL : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.UserMng.UserProfile, UserProfile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UserUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.LastLoginDate, o => o.Ignore())
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.UserId, o => o.Ignore())
                    .ForMember(d => d.UserFactoryAccess, o => o.Ignore())
                    .ForMember(d => d.UserPermission, o => o.Ignore())
                    .ForMember(d => d.SignatureImage, o => o.Ignore())
                    .ForMember(d => d.PersonalPhoto, o => o.Ignore());                    

                AutoMapper.Mapper.CreateMap<DTO.UserMng.FactoryAccess, UserFactoryAccess>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UserFactoryAccessID, o => o.Ignore())
                    .ForMember(d => d.UserID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.UserMng.UserPermission, UserPermission>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UserPermissionID, o => o.Ignore())
                    .ForMember(d => d.UserID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.UserMng.UserProfileSearchResult> DB2DTO_UserProfileSearchResultList(List<UserMng_UserProfileSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UserMng_UserProfileSearchResult_View>, List<DTO.UserMng.UserProfileSearchResult>>(dbItems);
        }

        public DTO.UserMng.UserProfile DB2DTO_UserProfile(UserMng_UserProfile_View dbItem)
        {
            DTO.UserMng.UserProfile result =  AutoMapper.Mapper.Map<UserMng_UserProfile_View, DTO.UserMng.UserProfile>(dbItem);
            result.AffectivePermissions = new List<DTO.UserMng.UserPermission>();
            result.AffectivePermissions = result.UserPermissions.Select(o => new DTO.UserMng.UserPermission() { 
                CanApprove = o.CanApprove,
                CanCreate = o.CanCreate,
                CanDelete = o.CanDelete,
                CanPrint = o.CanPrint,
                CanRead = o.CanRead,
                CanReset = o.CanReset,
                CanUpdate = o.CanUpdate,
                DisplayOrder = o.DisplayOrder,
                DisplayText = o.DisplayText,
                ModuleID = o.ModuleID,
                ParentID = o.ParentID,
                UserPermissionID = o.UserPermissionID
            }).ToList();
            foreach (UserMng_UserGroupPermission_View dbGroupPermission in dbItem.UserMng_UserGroupPermission_View)
            {
                DTO.UserMng.UserPermission dtoPermission = result.AffectivePermissions.FirstOrDefault(o => o.ModuleID == dbGroupPermission.ModuleID.Value);
                if (dbGroupPermission.CanCreate.HasValue && dbGroupPermission.CanCreate.Value)
                {
                    dtoPermission.CanCreate = true;
                }
                if (dbGroupPermission.CanRead.HasValue && dbGroupPermission.CanRead.Value)
                {
                    dtoPermission.CanRead = true;
                }
                if (dbGroupPermission.CanUpdate.HasValue && dbGroupPermission.CanUpdate.Value)
                {
                    dtoPermission.CanUpdate = true;
                }
                if (dbGroupPermission.CanDelete.HasValue && dbGroupPermission.CanDelete.Value)
                {
                    dtoPermission.CanDelete = true;
                }
                if (dbGroupPermission.CanApprove.HasValue && dbGroupPermission.CanApprove.Value)
                {
                    dtoPermission.CanApprove = true;
                }
                if (dbGroupPermission.CanReset.HasValue && dbGroupPermission.CanReset.Value)
                {
                    dtoPermission.CanReset = true;
                }
                if (dbGroupPermission.CanPrint.HasValue && dbGroupPermission.CanPrint.Value)
                {
                    dtoPermission.CanPrint = true;
                }
            }

            return result;
        }

        public void DTO2DB(DTO.UserMng.UserProfile dtoItem, ref UserProfile dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.UserMng.UserProfile, UserProfile>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            if (!string.IsNullOrEmpty(dtoItem.DateOfBirth))
            {
                try
                {
                    dbItem.DateOfBirth = DateTime.ParseExact(dtoItem.DateOfBirth, "d", new System.Globalization.CultureInfo("vi-VN"));
                }
                catch 
                {
                    dbItem.DateOfBirth = null;
                }
            }

            // map factory access
            if (dtoItem.FactoryAccesses != null)
            {
                // check for child rows deleted
                foreach (UserFactoryAccess dbFactoryAccess in dbItem.UserFactoryAccess.ToArray())
                {
                    if (!dtoItem.FactoryAccesses.Select(o => o.UserFactoryAccessID).Contains(dbFactoryAccess.UserFactoryAccessID))
                    {
                        dbItem.UserFactoryAccess.Remove(dbFactoryAccess);
                    }
                }

                // map child rows
                foreach (DTO.UserMng.FactoryAccess dtoFactoryAccess in dtoItem.FactoryAccesses)
                {
                    UserFactoryAccess dbFactoryAccess;
                    if (dtoFactoryAccess.UserFactoryAccessID <= 0)
                    {
                        dbFactoryAccess = new UserFactoryAccess();
                        dbItem.UserFactoryAccess.Add(dbFactoryAccess);
                    }
                    else
                    {
                        dbFactoryAccess = dbItem.UserFactoryAccess.FirstOrDefault(o => o.UserFactoryAccessID == dtoFactoryAccess.UserFactoryAccessID);
                    }

                    if (dbFactoryAccess != null)
                    {
                        AutoMapper.Mapper.Map<DTO.UserMng.FactoryAccess, UserFactoryAccess>(dtoFactoryAccess, dbFactoryAccess);
                    }
                }
            }

            // map user permission
            if (dtoItem.UserPermissions != null)
            {
                // check for child rows deleted
                foreach (UserPermission dbPermission in dbItem.UserPermission.ToArray())
                {
                    if (!dtoItem.UserPermissions.Select(o => o.UserPermissionID).Contains(dbPermission.UserPermissionID))
                    {
                        dbItem.UserPermission.Remove(dbPermission);
                    }
                }

                // map child rows
                foreach (DTO.UserMng.UserPermission dtoPermission in dtoItem.UserPermissions)
                {
                    UserPermission dbPermission;
                    if (dtoPermission.UserPermissionID <= 0)
                    {
                        dbPermission = new UserPermission();
                        dbItem.UserPermission.Add(dbPermission);
                    }
                    else
                    {
                        dbPermission = dbItem.UserPermission.FirstOrDefault(o => o.UserPermissionID == dtoPermission.UserPermissionID);
                    }

                    AutoMapper.Mapper.Map<DTO.UserMng.UserPermission, UserPermission>(dtoPermission, dbPermission);
                }
            }
        }
    }
}
