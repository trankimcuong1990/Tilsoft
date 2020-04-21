using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryPLCMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryPLCMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryPLCMng_PLCSearchResult_View, DTO.PLCSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.RatedDate, o => o.ResolveUsing(s => s.RatedDate.HasValue ? s.RatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPLCMng_BookingSearchResult_View, DTO.BookingSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPLCMng_ItemSearchResult_View, DTO.ItemSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPLCMng_PLC_View, DTO.PLC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.RatedDate, o => o.ResolveUsing(s => s.RatedDate.HasValue ? s.RatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.PLCImages, o => o.MapFrom(s => s.FactoryPLCMng_PLCImage_View))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPLCMng_PLCImage_View, DTO.PLCImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PLC, PLC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PLCID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.RatedBy, o => o.Ignore())
                    .ForMember(d => d.RatedDate, o => o.Ignore())
                    .ForMember(d => d.RatingComment, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.PLCImage, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PLCImage, PLCImage>().IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PLCSearchResult> DB2DTO_PLCSearchResultList(List<FactoryPLCMng_PLCSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPLCMng_PLCSearchResult_View>, List<DTO.PLCSearchResult>>(dbItems);
        }

        public List<DTO.BookingSearchResult> DB2DTO_BookingSearchResultList(List<FactoryPLCMng_BookingSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPLCMng_BookingSearchResult_View>, List<DTO.BookingSearchResult>>(dbItems);
        }

        public List<DTO.ItemSearchResult> DB2DTO_ItemSearchResultList(List<FactoryPLCMng_ItemSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPLCMng_ItemSearchResult_View>, List<DTO.ItemSearchResult>>(dbItems);
        }

        public DTO.PLC DB2DTO_PLC(FactoryPLCMng_PLC_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryPLCMng_PLC_View, DTO.PLC>(dbItem);
        }

        public void DTO2DB(DTO.PLC dtoItem, ref PLC dbItem, string _tempFolder)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.PLC, PLC>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;

            // map images
            if (dtoItem.PLCImages != null)
            {
                // check for child rows deleted
                foreach (PLCImage dbImage in dbItem.PLCImage.ToArray())
                {
                    if (!dtoItem.PLCImages.Select(o => o.PLCImageID).Contains(dbImage.PLCImageID))
                    {
                        if (dbImage.ImageFile != string.Empty)
                        {
                            (new Module.Framework.DAL.DataFactory()).RemoveImageFile(dbImage.ImageFile);
                        }

                        dbItem.PLCImage.Remove(dbImage);
                    }
                }

                // map child rows
                foreach (DTO.PLCImage dtoImage in dtoItem.PLCImages)
                {
                    PLCImage dbImage;
                    if (dtoImage.PLCImageID <= 0)
                    {
                        dbImage = new PLCImage();
                        dbItem.PLCImage.Add(dbImage);
                    }
                    else
                    {
                        dbImage = dbItem.PLCImage.FirstOrDefault(o => o.PLCImageID == dtoImage.PLCImageID);
                    }

                    if (dbImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PLCImage, PLCImage>(dtoImage, dbImage);
                        if (dtoImage.ImageFile_HasChange)
                        {
                            dbImage.ImageFile = (new Module.Framework.DAL.DataFactory()).CreateFilePointer(_tempFolder, dtoImage.ImageFile_NewFile, dtoImage.ImageFile);
                        }
                    }
                }
            }
        }
    }
}
