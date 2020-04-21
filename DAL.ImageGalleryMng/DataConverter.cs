using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ImageGalleryMng
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ImageGalleryMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ImageGalleryMng_ImageGallerySearchResult_View, DTO.ImageGalleryMng.ImageGallerySearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SampleImportDate, o => o.ResolveUsing(s => s.SampleImportDate.HasValue ? s.SampleImportDate.Value.ToString("dd/MM/yyyy") : ""))
                    //.ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : s.GalleryItemTypeID == 3 ? "avi.png" : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ImageGalleryMng_ImageGallery_View, DTO.ImageGalleryMng.ImageGallery>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SampleImportDate, o => o.ResolveUsing(s => s.SampleImportDate.HasValue ? s.SampleImportDate.Value.ToString("dd/MM/yyyy") : ""))
                    //.ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : s.GalleryItemTypeID == 3 ? "avi.png" : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.ImageGalleryClients, o => o.MapFrom(s => s.ImageGalleryMng_ImageGalleryClient_View))
                    .ForMember(d => d.ImageGalleryVersions, o => o.MapFrom(s => s.ImageGalleryMng_ImageGalleryVersion_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ImageGalleryMng_ImageGalleryClient_View, DTO.ImageGalleryMng.ImageGalleryClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ImageGalleryMng_ImageGalleryVersion_View, DTO.ImageGalleryMng.ImageGalleryVersion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ImageGalleryMng.ImageGallery, ImageGallery>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageGalleryClient, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.SampleImportDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ImageGalleryMng.ImageGalleryClient, ImageGalleryClient>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageGalleryClientID, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ImageGalleryMng.ImageGalleryVersion, ImageGalleryVersion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryVersionID, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ImageGalleryMng.ImageGallerySearchResult> DB2DTO_ImageGallerySearchResultList(List<ImageGalleryMng_ImageGallerySearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ImageGalleryMng_ImageGallerySearchResult_View>, List<DTO.ImageGalleryMng.ImageGallerySearchResult>>(dbItems);
        }

        public DTO.ImageGalleryMng.ImageGallery DB2DTO_ImageGallery(ImageGalleryMng_ImageGallery_View dbItem)
        {
            return AutoMapper.Mapper.Map<ImageGalleryMng_ImageGallery_View, DTO.ImageGalleryMng.ImageGallery>(dbItem);
        }

        public void DTO2DB(DTO.ImageGalleryMng.ImageGallery dtoItem, ref ImageGallery dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.ImageGalleryMng.ImageGallery, ImageGallery>(dtoItem, dbItem);
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.UpdatedDate = DateTime.Now;

            DateTime tmpDate;
            if (DateTime.TryParse(dtoItem.SampleImportDate, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out tmpDate))
            {
                dbItem.SampleImportDate = tmpDate;
            }

            // map client detail
            if (dtoItem.ImageGalleryClients != null)
            {
                // check for child rows deleted
                foreach (ImageGalleryClient dbClient in dbItem.ImageGalleryClient.ToArray())
                {
                    if (!dtoItem.ImageGalleryClients.Select(o => o.ImageGalleryClientID).Contains(dbClient.ImageGalleryClientID))
                    {
                        dbItem.ImageGalleryClient.Remove(dbClient);
                    }
                }

                // map child rows
                foreach (DTO.ImageGalleryMng.ImageGalleryClient dtoClient in dtoItem.ImageGalleryClients)
                {
                    ImageGalleryClient dbClient;
                    if (dtoClient.ImageGalleryClientID <= 0)
                    {
                        dbClient = new ImageGalleryClient();
                        dbItem.ImageGalleryClient.Add(dbClient);
                    }
                    else
                    {
                        dbClient = dbItem.ImageGalleryClient.FirstOrDefault(o => o.ImageGalleryClientID == dtoClient.ImageGalleryClientID);
                    }

                    if (dbClient != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ImageGalleryMng.ImageGalleryClient, ImageGalleryClient>(dtoClient, dbClient);
                    }
                }
            }

            // map version
            if (dtoItem.ImageGalleryVersions != null)
            {
                // check for child rows deleted
                foreach (ImageGalleryVersion dbVersion in dbItem.ImageGalleryVersion.ToArray())
                {
                    if (!dtoItem.ImageGalleryVersions.Select(o => o.ImageGalleryVersionID).Contains(dbVersion.ImageGalleryVersionID))
                    {
                        dbItem.ImageGalleryVersion.Remove(dbVersion);
                    }
                }

                // map child rows
                foreach (DTO.ImageGalleryMng.ImageGalleryVersion dtoVersion in dtoItem.ImageGalleryVersions)
                {
                    ImageGalleryVersion dbVersion;
                    if (dtoVersion.ImageGalleryVersionID <= 0)
                    {
                        dbVersion = new ImageGalleryVersion();
                        dbItem.ImageGalleryVersion.Add(dbVersion);
                    }
                    else
                    {
                        dbVersion = dbItem.ImageGalleryVersion.FirstOrDefault(o => o.ImageGalleryVersionID == dtoVersion.ImageGalleryVersionID);
                    }
                    dbVersion.UpdatedBy = dtoItem.UpdatedBy;
                    dbVersion.UpdatedDate = DateTime.Now;

                    if (dbVersion != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ImageGalleryMng.ImageGalleryVersion, ImageGalleryVersion>(dtoVersion, dbVersion);
                    }
                }
            }
        }
    }
}
