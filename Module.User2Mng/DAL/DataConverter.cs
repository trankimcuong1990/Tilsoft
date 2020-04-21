using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.User2Mng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;  

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<User2Mng_UserSearchResult_View, DTO.UserSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LicensedDate, o => o.ResolveUsing(s => s.LicensedDate.HasValue ? s.LicensedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.CVFileLocation, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.CVFileLocation) ? FrameworkSetting.Setting.MediaFullSizeUrl + s.CVFileLocation : "")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<User2Mng_NotYetMappedEmployee_View, DTO.NotYetMappedEmployee>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<User2Mng_UserProfile_View, DTO.UserProfile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.CVFileLocation, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.CVFileLocation) ? FrameworkSetting.Setting.MediaFullSizeUrl + s.CVFileLocation : "")))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LicensedDate, o => o.ResolveUsing(s => s.LicensedDate.HasValue ? s.LicensedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.LastLoginDate, o => o.ResolveUsing(s => s.LastLoginDate.HasValue ? s.LastLoginDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EmployeeFactories, o => o.Ignore())
                    .ForMember(d => d.FactoryAccesses, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<User2Mng_UserGroupPermission_View, DTO.Permission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<User2Mng_UserPermission_View, DTO.Permission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<User2Mng_FactoryAccess_View, DTO.FactoryAccess>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<User2Mng_EmployeeFactory_View, DTO.EmployeeFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.EmployeeFactory, EmployeeFactory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.EmployeeID, o => o.Ignore())
                    .ForMember(d => d.EmployeeFactoryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryAccess, UserFactoryAccess>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UserID, o => o.Ignore())
                    .ForMember(d => d.UserFactoryAccessID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.UserProfile, Employee>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.EmployeeFactory, o => o.Ignore())
                    .ForMember(d => d.PersonalPhoto, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.DateStart, o => o.Ignore())
                    .ForMember(d => d.EmployeeID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.UserSearchResult> DB2DTO_UserSearchResult(List<User2Mng_UserSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<User2Mng_UserSearchResult_View>, List<DTO.UserSearchResult>>(dbItems);
        }

        public List<DTO.NotYetMappedEmployee> DB2DTO_NotYetMappedEmployeeList(List<User2Mng_NotYetMappedEmployee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<User2Mng_NotYetMappedEmployee_View>, List<DTO.NotYetMappedEmployee>>(dbItems);
        }

        public DTO.UserProfile DB2DTO_UserProfile(User2Mng_UserProfile_View dbItem)
        {
            return AutoMapper.Mapper.Map<User2Mng_UserProfile_View, DTO.UserProfile>(dbItem);
        }

        public List<DTO.Permission> DB2DTO_UserPermissionList(List<User2Mng_UserPermission_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<User2Mng_UserPermission_View>, List<DTO.Permission>>(dbItems);
        }

        public List<DTO.Permission> DB2DTO_UserGroupPermissionList(List<User2Mng_UserGroupPermission_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<User2Mng_UserGroupPermission_View>, List<DTO.Permission>>(dbItems);
        }

        public List<DTO.FactoryAccess> DB2DTO_FactoryAccessList(List<User2Mng_FactoryAccess_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<User2Mng_FactoryAccess_View>, List<DTO.FactoryAccess>>(dbItems);
        }

        public List<DTO.EmployeeFactory> DB2DTO_EmployeeFactoryList(List<User2Mng_EmployeeFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<User2Mng_EmployeeFactory_View>, List<DTO.EmployeeFactory>>(dbItems);
        }

        public void DTO2DB(DTO.UserProfile dtoItem, ref Employee dbItem, string TmpFile, int userId)
        {
            // employee
            AutoMapper.Mapper.Map<DTO.UserProfile, Employee>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.DateOfBirth))
            {
                if (DateTime.TryParse(dtoItem.DateOfBirth, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.DateOfBirth = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.DateStart))
            {
                if (DateTime.TryParse(dtoItem.DateStart, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.DateStart = tmpDate;
                }
            }
            if (dtoItem.HasChanged)
            {
                dbItem.PersonalPhoto = fwFactory.CreateFilePointer(TmpFile, dtoItem.NewFile, dbItem.PersonalPhoto);
            }
            if (dtoItem.CVHasChanged)
            {
                dbItem.ResumeFile = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoItem.CVNewFile, dbItem.ResumeFile, dtoItem.CVFileName);
            }
            foreach (DTO.EmployeeFactory dtoFactory in dtoItem.EmployeeFactories)
            {
                EmployeeFactory dbFactory = dbItem.EmployeeFactory.FirstOrDefault(o => o.EmployeeFactoryID == dtoFactory.EmployeeFactoryID);
                AutoMapper.Mapper.Map<DTO.EmployeeFactory, EmployeeFactory>(dtoFactory, dbFactory);
            }
        }
    }
}
