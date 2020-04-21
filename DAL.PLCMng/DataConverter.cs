using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.PLCMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PLCMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PLCMng_PLCSearchResult_View, DTO.PLCMng.PLCSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RatedDate, o => o.ResolveUsing(s => s.RatedDate.HasValue ? s.RatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PLCMng_ItemForCreatePLC_View, DTO.PLCMng.ItemForCreatePLC>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PLCMng_PLCImage_View, DTO.PLCMng.PLCImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_FullSizeUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ImageFile_FullSizeUrl) ? s.ImageFile_FullSizeUrl : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PLCMng_PLC_View, DTO.PLCMng.PLC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RatedDate, o => o.ResolveUsing(s => s.RatedDate.HasValue ? s.RatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PLCImages, o => o.MapFrom(s=>s.PLCMng_PLCImage_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PLCMng_ItemForCreatePLC_View, DTO.PLCMng.PLC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CachedClientUD, o => o.MapFrom(s => s.ClientUD))
                    .ForMember(d => d.CachedContanierNo, o => o.MapFrom(s => s.ContainerNo))
                    .ForMember(d => d.CachedLoadingPlanUD, o => o.MapFrom(s => s.LoadingPlanUD))
                    .ForMember(d => d.CachedProformaInvoiceNo, o => o.MapFrom(s => s.ProfomaInvoiceNo))
                    .ForMember(d => d.PLCImages, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PLCMng.PLCImage, PLCImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PLCImageID, o => o.Ignore())
                    .ForMember(d => d.PLCID, o => o.Ignore())
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PLCMng.PLC, PLC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PLCID, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.RatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PLCMng.PLCSearchResult> DB2DTO_PLCSearchResultList(List<PLCMng_PLCSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PLCMng_PLCSearchResult_View>, List<DTO.PLCMng.PLCSearchResult>>(dbItems);
        }

        public List<DTO.PLCMng.ItemForCreatePLC> DB2DTO_ItemForCreatePLCList(List<PLCMng_ItemForCreatePLC_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PLCMng_ItemForCreatePLC_View>, List<DTO.PLCMng.ItemForCreatePLC>>(dbItems);
        }

        public DTO.PLCMng.PLC DB2DTO_PLC(PLCMng_PLC_View dbItem)
        {
            return AutoMapper.Mapper.Map<PLCMng_PLC_View, DTO.PLCMng.PLC>(dbItem);
        }

        public DTO.PLCMng.PLC Merge(PLCMng_ItemForCreatePLC_View dbItem)
        {
            return AutoMapper.Mapper.Map<PLCMng_ItemForCreatePLC_View, DTO.PLCMng.PLC>(dbItem);
        }

        public void DTO2DB(DTO.PLCMng.PLC dtoItem, string _tempFolder, ref PLC dbItem)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.PLCMng.PLC, PLC>(dtoItem, dbItem);

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
                foreach (DTO.PLCMng.PLCImage dtoImage in dtoItem.PLCImages)
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
                        AutoMapper.Mapper.Map<DTO.PLCMng.PLCImage, PLCImage>(dtoImage, dbImage);
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
