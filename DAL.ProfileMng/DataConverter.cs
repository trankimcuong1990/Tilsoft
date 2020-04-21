using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;

namespace DAL.ProfileMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ProfileMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProfileMng_User_View, DTO.ProfileMng.User>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PersonalPhoto_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.PersonalPhoto_DisplayUrl) ? s.PersonalPhoto_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.SignatureImage_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.SignatureImage_DisplayUrl) ? s.SignatureImage_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProfileMng.User, UserProfile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.UserId, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.ProfileMng.User DB2DTO(ProfileMng_User_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProfileMng_User_View, DTO.ProfileMng.User>(dbItem);
        }

        public void DTO2DB(DTO.ProfileMng.User dtoItem, ref UserProfile dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProfileMng.User, UserProfile>(dtoItem, dbItem);
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
        }
    }
}
