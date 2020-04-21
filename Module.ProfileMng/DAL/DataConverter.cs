using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.ProfileMng.DAL
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
                AutoMapper.Mapper.CreateMap<ProfileMng_UserProfile_View, DTO.UserProfile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.CVFileLocation, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.CVFileLocation) ? FrameworkSetting.Setting.FullSizeUrl + s.CVFileLocation : "")))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LicensedDate, o => o.ResolveUsing(s => s.LicensedDate.HasValue ? s.LicensedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.LastLoginDate, o => o.ResolveUsing(s => s.LastLoginDate.HasValue ? s.LastLoginDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LastPasswordChanged, o => o.ResolveUsing(s => s.LastPasswordChanged.HasValue ? s.LastPasswordChanged.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.UserProfile, Employee>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PersonalPhoto, o => o.Ignore())
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DateEnd, o => o.Ignore())
                    .ForMember(d => d.DateStart, o => o.Ignore())
                    .ForMember(d => d.EmployeeID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.UserProfile DB2DTO_UserProfile(ProfileMng_UserProfile_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProfileMng_UserProfile_View, DTO.UserProfile>(dbItem);
        }

        public void DTO2DB(DTO.UserProfile dtoItem, ref Employee dbItem, string TmpFile, int userId)
        {
            AutoMapper.Mapper.Map<DTO.UserProfile, Employee>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.DateOfBirth))
            {
                if (DateTime.TryParse(dtoItem.DateOfBirth, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.DateOfBirth = tmpDate;
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
            if (dtoItem.SignatureHasChanged) {

                dbItem.SignatureFile = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoItem.SignatureNewFile, dbItem.SignatureFile, dtoItem.SignatureFileName);
            }
        }
    }
}
