using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.OnlineFileMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
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
                AutoMapper.Mapper.CreateMap<OnlineFileMng_OnlineFile_View, DTO.OnlineFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.OnlineFilePermissions, o => o.MapFrom(s => s.OnlineFileMng_OnlineFilePermission_View))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OnlineFileMng_OnlineFilePermission_View, DTO.OnlineFilePermission>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.OnlineFile, OnlineFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OnlineFileID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.OnlineFilePermission, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.OnlineFilePermission, OnlineFilePermission>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OnlineFilePermissionID, o => o.Ignore())
                    .ForMember(d => d.OnlineFileID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<OnLineFileMng_User2_View, DTO.User2>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.OnlineFile> DB2DTO_OnlineFileList(List<OnlineFileMng_OnlineFile_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OnlineFileMng_OnlineFile_View>, List<DTO.OnlineFile>>(dbItems);
        }

        public List<DTO.User2> DB2DTO_OnlineFileUserList(List<OnLineFileMng_User2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OnLineFileMng_User2_View>, List<DTO.User2>>(dbItems);
        }

        public void DTO2DB(DTO.OnlineFile dtoItem, ref OnlineFile dbItem, string TmpFile, int userId)
        {
            // file info
            AutoMapper.Mapper.Map<DTO.OnlineFile, OnlineFile>(dtoItem, dbItem);
            dbItem.UpdatedBy = userId;
            dbItem.UpdatedDate = DateTime.Now;

            // processing file
            if (dtoItem.HasChanged)
            {
                dbItem.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoItem.NewFile, dtoItem.FileUD, dtoItem.FriendlyName);
            }

            // file permission
            if (dtoItem.OnlineFilePermissions != null)
            {
                foreach (OnlineFilePermission dbPermission in dbItem.OnlineFilePermission.ToArray())
                {
                    if (!dtoItem.OnlineFilePermissions.Select(o => o.OnlineFilePermissionID).Contains(dbPermission.OnlineFilePermissionID))
                    {
                        dbItem.OnlineFilePermission.Remove(dbPermission);
                    }
                }
                foreach (DTO.OnlineFilePermission dtoPermission in dtoItem.OnlineFilePermissions.Where(o => o.OnlineFilePermissionID <= 0))
                {
                    OnlineFilePermission dbPermission = new OnlineFilePermission();
                    dbItem.OnlineFilePermission.Add(dbPermission);
                    AutoMapper.Mapper.Map<DTO.OnlineFilePermission, OnlineFilePermission>(dtoPermission, dbPermission);
                }
            }            
        }
    }
}
