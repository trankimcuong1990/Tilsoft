using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.SampleItemMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<SampleItemMng_FactoryBreakdownDetail_View, DTO.FactoryBreakdownDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_FactoryBreakdown_View, DTO.FactoryBreakdownDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactoryBreakdownDetailDTOs, o => o.MapFrom(s => s.SampleItemMng_FactoryBreakdownDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SampleProductDTO, SampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.SampleProgress, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryDeadline, o => o.Ignore())
                    .ForMember(d => d.ItemInfoUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ETADestination, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryBreakdownDTO, FactoryBreakdown>()
                    .IgnoreAllNonExisting()                 
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryBreakdownDetailDTO, FactoryBreakdownDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleProductStatus_View, DTO.SampleProductStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleQARemark_View, DTO.SampleQARemarkDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                   .ForMember(d => d.SampleQARemarkImageDTOs, o => o.MapFrom(s => s.SampleItemMng_SampleQARemarkImage_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleQARemarkImage_View, DTO.SampleQARemarkImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleProduct_View, DTO.SampleProductDTO>()                    
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ModelThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ModelThumbnailLocation) ? s.ModelThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.ModelFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ModelFileLocation) ? s.ModelFileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ETADestination, o => o.ResolveUsing(s => s.ETADestination.HasValue ? s.ETADestination.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ItemInfoUpdatedDate, o => o.ResolveUsing(s => s.ItemInfoUpdatedDate.HasValue ? s.ItemInfoUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))                    
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                    .ForMember(d => d.FactoryBreakdownDTO, o => o.MapFrom(s => s.SampleItemMng_FactoryBreakdown_View.FirstOrDefault()))
                    .ForMember(d => d.SampleProgressDTOs, o => o.MapFrom(s => s.SampleItemMng_SampleProgress_View))
                    .ForMember(d => d.SampleQARemarkDTOs, o => o.MapFrom(s => s.SampleItemMng_SampleQARemark_View))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleItemSearchResult_View, DTO.SampleItemSearchResultDTO>()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleProgress_View, DTO.SampleProgressDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.SampleProgressImageDTOs, o => o.MapFrom(s => s.SampleItemMng_SampleProgressImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SampleItemMng_SampleProgressImage_View, DTO.SampleProgressImageDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SampleProgressDTO, SampleProgress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProgressID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SampleProgressImage, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleProgressImageDTO, SampleProgressImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProgressImageID, o => o.Ignore())
                    .ForMember(d => d.SampleProgressID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleQARemarkDTO, SampleQARemark>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SampleQARemarkID, o => o.Ignore())
                   .ForMember(d => d.UpdatedBy, o => o.Ignore())
                   .ForMember(d => d.UpdatedDate, o => o.Ignore())
                   .ForMember(d => d.SampleQARemarkImage, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleQARemarkImageDTO, SampleQARemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleQARemarkImageID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }        
        }

        public DTO.SampleQARemarkDTO DB2DTO_SampleQARemark(SampleItemMng_SampleQARemark_View dbItem)
        {
            return AutoMapper.Mapper.Map<SampleItemMng_SampleQARemark_View, DTO.SampleQARemarkDTO>(dbItem);
        }

        public void DTO2DB_SampleQARemark(DTO.SampleQARemarkDTO dtoItem, ref SampleQARemark dbItem, string TmpFile, int userId)
        {
            // remark
            AutoMapper.Mapper.Map<DTO.SampleQARemarkDTO, SampleQARemark>(dtoItem, dbItem);

            // remark image
            foreach (SampleQARemarkImage dbImage in dbItem.SampleQARemarkImage.ToArray())
            {
                if (!dtoItem.SampleQARemarkImageDTOs.Select(o => o.SampleQARemarkImageID).Contains(dbImage.SampleQARemarkImageID))
                {
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleQARemarkImage.Remove(dbImage);
                }
            }
            foreach (DTO.SampleQARemarkImageDTO dtoImage in dtoItem.SampleQARemarkImageDTOs)
            {
                SampleQARemarkImage dbImage;
                if (dtoImage.SampleQARemarkImageID <= 0)
                {
                    dbImage = new SampleQARemarkImage();
                    dbItem.SampleQARemarkImage.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleQARemarkImage.FirstOrDefault(o => o.SampleQARemarkImageID == dtoImage.SampleQARemarkImageID);
                }

                if (dbImage != null)
                {
                    // change or add images
                    if (dtoImage.HasChanged)
                    {
                        dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD, dtoImage.FriendlyName);
                    }
                    AutoMapper.Mapper.Map<DTO.SampleQARemarkImageDTO, SampleQARemarkImage>(dtoImage, dbImage);
                }
            }
        }

        public DTO.FactoryBreakdownDTO DB2DTO_FactoryBreakdownDTO(SampleItemMng_FactoryBreakdown_View dbItem)
        {
            return AutoMapper.Mapper.Map<SampleItemMng_FactoryBreakdown_View, DTO.FactoryBreakdownDTO>(dbItem);
        }
        public void DTO2DB_SampleProduct(DTO.SampleProductDTO dtoItem, ref SampleProduct dbItem)
        {
            Mapper.Map<DTO.SampleProductDTO, SampleProduct>(dtoItem, dbItem);
            //Covert String to Dateime
            dbItem.StatusUpdatedDate = dtoItem.StatusUpdatedDate.ConvertStringToDateTime();
            dbItem.FactoryDeadline = dtoItem.FactoryDeadline.ConvertStringToDateTime();
            dbItem.ItemInfoUpdatedDate = dtoItem.ItemInfoUpdatedDate.ConvertStringToDateTime();
            dbItem.ETADestination = dtoItem.ETADestination.ConvertStringToDateTime();

            if(dtoItem.FactoryBreakdownDTO != null)
            {
                FactoryBreakdown dbFactoryBreakdown = dbItem.FactoryBreakdown.FirstOrDefault();

                if(dbFactoryBreakdown!= null)
                {
                    Mapper.Map<DTO.FactoryBreakdownDTO, FactoryBreakdown>(dtoItem.FactoryBreakdownDTO, dbFactoryBreakdown);

                    if(dtoItem.FactoryBreakdownDTO.FactoryBreakdownDetailDTOs != null)
                    {
                        foreach (var item in dtoItem.FactoryBreakdownDTO.FactoryBreakdownDetailDTOs)
                        {
                            FactoryBreakdownDetail dbFactoryBreakdownDetail = dbFactoryBreakdown.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID);
                            Mapper.Map<DTO.FactoryBreakdownDetailDTO, FactoryBreakdownDetail>(item, dbFactoryBreakdownDetail);
                        }
                    }
                }             
            }
        }
        public List<DTO.SampleProductStatusDTO> DB2DTO_SampleProductStatusDTOs(List<SampleItemMng_SampleProductStatus_View> dbItem)
        {
            return Mapper.Map<List<SampleItemMng_SampleProductStatus_View>, List<DTO.SampleProductStatusDTO>>(dbItem);
        }
        public List<DTO.SampleItemSearchResultDTO> DB2DTO_SampleItemSearchResultDTOs(List<SampleItemMng_SampleItemSearchResult_View> dbItem)
        {
            return Mapper.Map<List<SampleItemMng_SampleItemSearchResult_View>, List<DTO.SampleItemSearchResultDTO>>(dbItem);
        }
        public DTO.SampleProgressDTO DB2DTO_SampleProgress(SampleItemMng_SampleProgress_View dbItem)
        {
            return AutoMapper.Mapper.Map<SampleItemMng_SampleProgress_View, DTO.SampleProgressDTO>(dbItem);
        }

        public void DTO2DB_SampleProgress(DTO.SampleProgressDTO dtoItem, ref SampleProgress dbItem, string TmpFile, int userId)
        {
            // progress
            AutoMapper.Mapper.Map<DTO.SampleProgressDTO, SampleProgress>(dtoItem, dbItem);

            // progress image
            foreach (SampleProgressImage dbImage in dbItem.SampleProgressImage.ToArray())
            {
                if (!dtoItem.SampleProgressImageDTOs.Select(o => o.SampleProgressImageID).Contains(dbImage.SampleProgressImageID))
                {
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleProgressImage.Remove(dbImage);
                }
            }
            foreach (DTO.SampleProgressImageDTO dtoImage in dtoItem.SampleProgressImageDTOs)
            {
                SampleProgressImage dbImage;
                if (dtoImage.SampleProgressImageID <= 0)
                {
                    dbImage = new SampleProgressImage();
                    dbItem.SampleProgressImage.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleProgressImage.FirstOrDefault(o => o.SampleProgressImageID == dtoImage.SampleProgressImageID);
                }

                if (dbImage != null)
                {
                    // change or add images
                    if (dtoImage.HasChanged)
                    {
                        dbImage.FileUD = fwFactory.CreateFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD);
                    }
                    AutoMapper.Mapper.Map<DTO.SampleProgressImageDTO, SampleProgressImage>(dtoImage, dbImage);
                }
            }
        }
    }
}
