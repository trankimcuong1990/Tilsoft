using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using AutoMapper;
using Library;

namespace Module.SpecificationOfProductMng.DAL
{
    public class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            //SearchView
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationOfProduct_SearchView, DTO.SpecificationOfProductSearchDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //GetView
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationOfProduct_View, DTO.SpecificationOfProductDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.specificationCushionImageDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationCushionImage_view))
                .ForMember(d => d.specificationImageDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationImage_View))
                .ForMember(d => d.specificationPackingDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationPacking_View))
                .ForMember(d => d.specificationWeavingFileDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationWeavingFile_View))
                .ForMember(d => d.specificationWoodenartDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationWoodenPart_View))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //SpecificationCushionImage
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationCushionImage_view, DTO.SpecificationCushionImageDTO>()
                .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

            //SpecificationImage
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationImage_View, DTO.SpecificationImageDTO>()
                .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

            //SpecificationWeavingFile
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationWeavingFile_View, DTO.SpecificationWeavingFileDTO>()
                .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

            //SpecificationPacking
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationPacking_View, DTO.SpecificationPackingDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            //SpecificationWoodenPart
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationWoodenPart_View, DTO.SpecificationWoodenartDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //PackingSpecification
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_PackingSpecification_View, DTO.PackingSpecificationDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_PackingSpecification_View, DTO.SpecificationPackingDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO SpecificationOfProduct
            AutoMapper.Mapper.CreateMap<DTO.SpecificationOfProductDTO, ProductSpecification>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductSpecificationID, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO SpecificationCushionImage
            AutoMapper.Mapper.CreateMap<DTO.SpecificationCushionImageDTO, ProductSpecificationCushionImage>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductSpecificationID, o => o.Ignore())
                .ForMember(d => d.ProductSpecificationCushionImageID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO SpecificationImage
            AutoMapper.Mapper.CreateMap<DTO.SpecificationImageDTO, ProductSpecificationImage>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductSpecificationID, o => o.Ignore())
                .ForMember(d => d.ProductSpecificationImageID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO SpecificationWeavingFile
            AutoMapper.Mapper.CreateMap<DTO.SpecificationWeavingFileDTO, ProductSpecificationWeavingFile>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductSpecificationID, o => o.Ignore())
                .ForMember(d => d.ProductSpecificationWeavingFileID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO SpecificationPacking
            AutoMapper.Mapper.CreateMap<DTO.SpecificationPackingDTO, ProductSpecificationPacking>()
                .IgnoreAllNonExisting()
                //.ForMember(d => d.PackingSpecificationID, o => o.Ignore())
                .ForMember(d => d.ProductSpecificationID, o => o.Ignore())
                .ForMember(d => d.ProductSpecificationPackingID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO SpecificationWoodenPart
            AutoMapper.Mapper.CreateMap<DTO.SpecificationWoodenartDTO, ProductSpecificationWoodenPart>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductSpecificationID, o => o.Ignore())
                .ForMember(d => d.ProductSpecificationWoodenPartID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO QuickSearchSample
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_QuickSearchSampleProduct_View, DTO.QuickSearchSampleProductDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            //DB2DTO ClientForProduct
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_ClientOfProduct_View, DTO.ClientOfProductDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_Product_View, DTO.SpecificationImageDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductSpecificationImageID, o => o.UseValue(-1))
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ScanFileLocation) ? s.ScanFileLocation : "no-image.jpg")))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationOfProductList_View, DTO.QuickViewSpecOfProDuctListDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            //GetView coppy
            AutoMapper.Mapper.CreateMap<SpecificationOfProductMng_SpecificationOfProduct_View, DTO.SpecOfProductCopyDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.specificationCushionImageDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationCushionImage_view))
                .ForMember(d => d.specificationImageDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationImage_View))
                .ForMember(d => d.specificationPackingDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationPacking_View))
                .ForMember(d => d.specificationWeavingFileDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationWeavingFile_View))
                .ForMember(d => d.specificationWoodenartDTOs, o => o.MapFrom(s => s.SpecificationOfProductMng_SpecificationWoodenPart_View))
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }


        //SearchView
        public List<DTO.SpecificationOfProductSearchDTO> DB2DTO_SearchResult(List<SpecificationOfProductMng_SpecificationOfProduct_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_SpecificationOfProduct_SearchView>, List<DTO.SpecificationOfProductSearchDTO>>(dbItems);
        }

        //GetData
        public DTO.SpecificationOfProductDTO DB2DTO_GetDataSpecificationOfProduct(SpecificationOfProductMng_SpecificationOfProduct_View dbItem)
        {
            return AutoMapper.Mapper.Map<SpecificationOfProductMng_SpecificationOfProduct_View, DTO.SpecificationOfProductDTO>(dbItem);
        }

        public DTO.SpecOfProductCopyDTO DB2DTO_GetcopyData(SpecificationOfProductMng_SpecificationOfProduct_View dbItem)
        {
            return AutoMapper.Mapper.Map<SpecificationOfProductMng_SpecificationOfProduct_View, DTO.SpecOfProductCopyDTO>(dbItem);
        }
        //GetPackingSpecification
        public List<DTO.PackingSpecificationDTO> DB2DTO_GetPacking(List<SpecificationOfProductMng_PackingSpecification_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_PackingSpecification_View>, List<DTO.PackingSpecificationDTO>>(dbItem);
        }

        public List<DTO.SpecificationPackingDTO> DB2DTO_GetPacking2(List<SpecificationOfProductMng_PackingSpecification_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_PackingSpecification_View>, List<DTO.SpecificationPackingDTO>>(dbItem);
        }

        public List<DTO.QuickSearchSampleProductDTO> DB2DTO_QuickSerachSample(List<SpecificationOfProductMng_QuickSearchSampleProduct_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_QuickSearchSampleProduct_View>, List<DTO.QuickSearchSampleProductDTO>>(dbItem);
        }

        public List<DTO.ClientOfProductDTO> DB2DTO_ClientOfProduct(List<SpecificationOfProductMng_ClientOfProduct_View> cItem)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_ClientOfProduct_View>, List<DTO.ClientOfProductDTO>>(cItem);
        }

        public List<DTO.SpecificationImageDTO> DB2DTO_FromProductImage(List<SpecificationOfProductMng_Product_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_Product_View>, List<DTO.SpecificationImageDTO>>(dbItem);
        }

        //Gets specList
        public List<DTO.QuickViewSpecOfProDuctListDTO> DB2DTO_GetSpecOfProductList(List<SpecificationOfProductMng_SpecificationOfProductList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SpecificationOfProductMng_SpecificationOfProductList_View>, List<DTO.QuickViewSpecOfProDuctListDTO>>(dbItems);
        }

        //UpdateData
        public void DTO2DB_UpdateSpecificationOfProduct(DTO.SpecificationOfProductDTO dtoItem, ref ProductSpecification dbItem)
        {
            //SpecificationCushionImage
            if (dtoItem.specificationCushionImageDTOs != null)
            {
                foreach (var item in dbItem.ProductSpecificationCushionImage.ToArray())
                {
                    if (!dtoItem.specificationCushionImageDTOs.Select(s => s.ProductSpecificationCushionImageID).Contains(item.ProductSpecificationCushionImageID))
                    {
                        dbItem.ProductSpecificationCushionImage.Remove(item);
                    }
                }

                foreach (var item in dtoItem.specificationCushionImageDTOs)
                {
                    ProductSpecificationCushionImage dbSpecificationCushionImage = new ProductSpecificationCushionImage();
                    if (item.ProductSpecificationCushionImageID < 0)
                    {
                        dbItem.ProductSpecificationCushionImage.Add(dbSpecificationCushionImage);
                    }
                    else
                    {
                        dbSpecificationCushionImage = dbItem.ProductSpecificationCushionImage.Where(s => s.ProductSpecificationCushionImageID == item.ProductSpecificationCushionImageID).FirstOrDefault();
                    }
                    if (dbSpecificationCushionImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SpecificationCushionImageDTO, ProductSpecificationCushionImage>(item, dbSpecificationCushionImage);
                    }
                }
            }

            //SpecificationImage
            if(dtoItem.specificationImageDTOs != null)
            {
                foreach(var item in dbItem.ProductSpecificationImage.ToArray())
                {
                    if (!dtoItem.specificationImageDTOs.Select(s => s.ProductSpecificationImageID).Contains(item.ProductSpecificationImageID))
                    {
                        dbItem.ProductSpecificationImage.Remove(item);
                    }
                }

                foreach (var item in dtoItem.specificationImageDTOs)
                {
                    ProductSpecificationImage dbSpecificationImage = new ProductSpecificationImage();
                    if(item.ProductSpecificationImageID < 0)
                    {
                        dbItem.ProductSpecificationImage.Add(dbSpecificationImage);
                    }
                    else
                    {
                        dbSpecificationImage = dbItem.ProductSpecificationImage.Where(s => s.ProductSpecificationImageID == item.ProductSpecificationImageID).FirstOrDefault();
                    }
                    if(dbSpecificationImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SpecificationImageDTO, ProductSpecificationImage>(item, dbSpecificationImage);
                    }
                }
            }

            //SpecificationWeavingFile
            if (dtoItem.specificationWeavingFileDTOs != null)
            {
                foreach (var item in dbItem.ProductSpecificationWeavingFile.ToArray())
                {
                    if (!dtoItem.specificationWeavingFileDTOs.Select(s => s.ProductSpecificationWeavingFileID).Contains(item.ProductSpecificationWeavingFileID))
                    {
                        dbItem.ProductSpecificationWeavingFile.Remove(item);
                    }
                }

                foreach (var item in dtoItem.specificationWeavingFileDTOs)
                {
                    ProductSpecificationWeavingFile dbSpecificationWeavingFile = new ProductSpecificationWeavingFile();
                    if (item.ProductSpecificationWeavingFileID < 0)
                    {
                        dbItem.ProductSpecificationWeavingFile.Add(dbSpecificationWeavingFile);
                    }
                    else
                    {
                        dbSpecificationWeavingFile = dbItem.ProductSpecificationWeavingFile.Where(s => s.ProductSpecificationWeavingFileID == item.ProductSpecificationWeavingFileID).FirstOrDefault();
                    }
                    if (dbSpecificationWeavingFile != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SpecificationWeavingFileDTO, ProductSpecificationWeavingFile>(item, dbSpecificationWeavingFile);
                    }
                }
            }

            //SpecificationPacking
            if (dtoItem.specificationPackingDTOs != null)
            {
                foreach (var item in dbItem.ProductSpecificationPacking.ToArray())
                {
                    if (!dtoItem.specificationPackingDTOs.Select(s => s.ProductSpecificationPackingID).Contains(item.ProductSpecificationPackingID))
                    {
                        dbItem.ProductSpecificationPacking.Remove(item);
                    }
                }

                foreach (var item in dtoItem.specificationPackingDTOs)
                {
                    ProductSpecificationPacking dbSpecificationPacking = new ProductSpecificationPacking();
                    if (item.ProductSpecificationPackingID < 0)
                    {
                        dbItem.ProductSpecificationPacking.Add(dbSpecificationPacking);
                    }
                    else
                    {
                        dbSpecificationPacking = dbItem.ProductSpecificationPacking.Where(s => s.ProductSpecificationPackingID == item.ProductSpecificationPackingID).FirstOrDefault();
                    }
                    if (dbSpecificationPacking != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SpecificationPackingDTO, ProductSpecificationPacking>(item, dbSpecificationPacking);
                    }
                }
            }

            //SpecificationWoodenPart
            if (dtoItem.specificationWoodenartDTOs != null)
            {
                foreach (var item in dbItem.ProductSpecificationWoodenPart.ToArray())
                {
                    if (!dtoItem.specificationWoodenartDTOs.Select(s => s.ProductSpecificationWoodenPartID).Contains(item.ProductSpecificationWoodenPartID))
                    {
                        dbItem.ProductSpecificationWoodenPart.Remove(item);
                    }
                }

                foreach (var item in dtoItem.specificationWoodenartDTOs)
                {
                    ProductSpecificationWoodenPart dbSpecificationWoodenPart = new ProductSpecificationWoodenPart();
                    if (item.ProductSpecificationWoodenPartID < 0)
                    {
                        dbItem.ProductSpecificationWoodenPart.Add(dbSpecificationWoodenPart);
                    }
                    else
                    {
                        dbSpecificationWoodenPart = dbItem.ProductSpecificationWoodenPart.Where(s => s.ProductSpecificationWoodenPartID == item.ProductSpecificationWoodenPartID).FirstOrDefault();
                    }
                    if (dbSpecificationWoodenPart != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SpecificationWoodenartDTO, ProductSpecificationWoodenPart>(item, dbSpecificationWoodenPart);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.SpecificationOfProductDTO, ProductSpecification>(dtoItem, dbItem);
        }

    }
}
