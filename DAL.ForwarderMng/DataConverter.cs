using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ForwarderMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ForwarderMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ForwarderMng_ForwarderSearchResult_View, DTO.ForwarderMng.ForwarderSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ForwarderMng_Forwarder_View, DTO.ForwarderMng.Forwarder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ForwarderImages, o => o.MapFrom(s => s.ForwarderMng_ForwarderImage_View))
                    .ForMember(d => d.ForwarderPICs, o => o.MapFrom(s => s.ForwarderMng_ForwarderPIC_View));

                Mapper.CreateMap<ForwarderMng_ForwarderImage_View, DTO.ForwarderMng.ForwarderImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "")));

                AutoMapper.Mapper.CreateMap<ForwarderMng_ForwarderPIC_View, DTO.ForwarderMng.ForwarderPIC>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                Mapper.CreateMap<SupportMng_Country_View, DTO.ForwarderMng.Country>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<SupportMng_InternalDepartment_View, DTO.ForwarderMng.Department>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<SupportMng_Employee_View, DTO.ForwarderMng.SupportEmployee>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ForwarderMng.Forwarder, Forwarder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ForwarderID, o => o.Ignore())
                    .ForMember(d => d.ForwarderImage, o => o.Ignore())
                    .ForMember(d => d.ForwarderPIC, o => o.Ignore());

                Mapper.CreateMap<DTO.ForwarderMng.ForwarderImage, ForwarderImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ForwarderImageID, o => o.Ignore())
                    .ForMember(d => d.ForwarderID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.ForwarderID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ForwarderMng.ForwarderPIC, ForwarderPIC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ForwarderPICID, o => o.Ignore())
                    .ForMember(d => d.ForwarderID, o => o.Ignore());
                    

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ForwarderMng.ForwarderSearchResult> DB2DTO_ForwarderSearchResultList(List<ForwarderMng_ForwarderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ForwarderMng_ForwarderSearchResult_View>, List<DTO.ForwarderMng.ForwarderSearchResult>>(dbItems);
        }

        public DTO.ForwarderMng.Forwarder DB2DTO_Forwarder(ForwarderMng_Forwarder_View dbItem)
        {
            return AutoMapper.Mapper.Map<ForwarderMng_Forwarder_View, DTO.ForwarderMng.Forwarder>(dbItem);
        }

        public void DTO2BD_Forwarder(DTO.ForwarderMng.Forwarder dtoItem, ref Forwarder dbItem, string tempFolder)
        {
            if (dtoItem.ForwarderImages != null)
            {
                foreach (var item in dbItem.ForwarderImage.ToArray())
                {
                    if (!dtoItem.ForwarderImages.Select(s => s.ForwarderImageID).Contains(item.ForwarderImageID))
                    {
                        dbItem.ForwarderImage.Remove(item);
                    }
                }

                foreach (var dto in dtoItem.ForwarderImages)
                {
                    ForwarderImage item;

                    if (dto.ForwarderImageID < 0)
                    {
                        item = new ForwarderImage();

                        dbItem.ForwarderImage.Add(item);
                    }
                    else
                    {
                        item = dbItem.ForwarderImage.FirstOrDefault(s => s.ForwarderImageID == dto.ForwarderImageID);
                    }

                    if (item != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ForwarderMng.ForwarderImage, ForwarderImage>(dto, item);

                        if (dto.File_HasChange.HasValue && dto.File_HasChange.Value)
                        {
                            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                            item.FileUD = fwFactory.CreateNoneImageFilePointer(tempFolder, dto.File_NewFile, dto.FileUD);
                        }
                    }
                }
            }

            if (dtoItem.ForwarderPICs != null)
            {
                foreach (var item1 in dbItem.ForwarderPIC.ToArray())
                {
                    if (!dtoItem.ForwarderPICs.Select(s => s.ForwarderPICID).Contains(item1.ForwarderPICID))
                    {
                        dbItem.ForwarderPIC.Remove(item1);
                    }
                }

                foreach (var dto1 in dtoItem.ForwarderPICs)
                {
                    ForwarderPIC item1;

                    if (dto1.ForwarderPICID < 0)
                    {
                        item1 = new ForwarderPIC();
                        dbItem.ForwarderPIC.Add(item1);
                    }
                    else
                    {
                        item1 = dbItem.ForwarderPIC.FirstOrDefault(s => s.ForwarderPICID == dto1.ForwarderPICID);
                    }

                    if (item1 != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ForwarderMng.ForwarderPIC, ForwarderPIC>(dto1, item1);
                        
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.ForwarderMng.Forwarder, Forwarder>(dtoItem, dbItem);
        }

        public List<DTO.ForwarderMng.Country> DB2DTO_Country(List<SupportMng_Country_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_Country_View>, List<DTO.ForwarderMng.Country>>(dbItem);
        }

        public List<DTO.ForwarderMng.SupportEmployee> DB2DTO_SupportEmployee(List<SupportMng_Employee_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_Employee_View>, List<DTO.ForwarderMng.SupportEmployee>>(dbItem);
        }

        public List<DTO.ForwarderMng.Department> DB2DTO_Department(List<SupportMng_InternalDepartment_View> dbItem)
        {
            return Mapper.Map<List<SupportMng_InternalDepartment_View>, List<DTO.ForwarderMng.Department>>(dbItem);
        }
    }
}
