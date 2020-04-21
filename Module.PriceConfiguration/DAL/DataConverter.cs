using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration.DAL
{
    internal class DataConverter
    {
        private string FormatDateToString = "dd/MM/yyyy";

        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<PriceConfiguration_PriceConfigurationSearchResult_View, DTO.PriceConfigurationSearchResultDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(FormatDateToString) : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<PriceConfiguration_PriceConfiguration_View, DTO.PriceConfigurationDto>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(FormatDateToString) : string.Empty))
                .ForMember(d => d.PriceConfigurationDetailOfFCS, o => o.MapFrom(s => s.PriceConfiguration_PriceConfigurationDetail_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<PriceConfiguration_PriceConfigurationDetail_View, DTO.PriceConfigurationDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<CushionColorMng_CushionColor_View, DTO.CushionColorDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PriceConfiguration_FSC_View, DTO.PriceConfigurationDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<PriceConfiguration_FrameMaterial_View, DTO.PriceConfigurationDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<PriceConfiguration_PackagingMethod_View, DTO.PriceConfigurationDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<PriceConfiguration_MaterialColor_View, DTO.PriceConfigurationDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<PriceConfiguration_CushionColor_View, DTO.PriceConfigurationDetailDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.PriceConfigurationDto, PriceConfiguration>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductElementID, o => o.Ignore())
                .ForMember(d => d.Season, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(o => o.PriceConfigurationID, o => o.Ignore())
                .ForMember(d => d.PriceConfigurationDetail, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.PriceConfigurationDetailDto, PriceConfigurationDetail>()
                .IgnoreAllNonExisting()
                .ForMember(o => o.PriceConfigurationDetailID, o => o.Ignore())
                .ForMember(o => o.PriceConfigurationID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.PriceConfigurationSearchResultDto> DB2DTO_Search(List<PriceConfiguration_PriceConfigurationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_PriceConfigurationSearchResult_View>, List<DTO.PriceConfigurationSearchResultDto>>(dbItems);
        }

        public DTO.PriceConfigurationDto DB2DTO_Get(PriceConfiguration_PriceConfiguration_View dbItem)
        {
            return AutoMapper.Mapper.Map<PriceConfiguration_PriceConfiguration_View, DTO.PriceConfigurationDto>(dbItem);
        }

        public List<DTO.CushionColorDto> DB2DTO_CushionColor(List<CushionColorMng_CushionColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CushionColorMng_CushionColor_View>, List<DTO.CushionColorDto>>(dbItems);
        }

        public List<DTO.PriceConfigurationDetailDto> DB2DTO_List(List<PriceConfiguration_PriceConfigurationDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_PriceConfigurationDetail_View>, List<DTO.PriceConfigurationDetailDto>>(dbItems);
        }

        public List<DTO.PriceConfigurationDetailDto> DB2DTO_ListFSC(List<PriceConfiguration_FSC_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_FSC_View>, List<DTO.PriceConfigurationDetailDto>>(dbItems);
        }

        public List<DTO.PriceConfigurationDetailDto> DB2DTO_ListFrameMaterial(List<PriceConfiguration_FrameMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_FrameMaterial_View>, List<DTO.PriceConfigurationDetailDto>>(dbItems);
        }

        public List<DTO.PriceConfigurationDetailDto> DB2DTO_ListPackagingMethod(List<PriceConfiguration_PackagingMethod_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_PackagingMethod_View>, List<DTO.PriceConfigurationDetailDto>>(dbItems);
        }

        public List<DTO.PriceConfigurationDetailDto> DB2DTO_ListMaterialColor(List<PriceConfiguration_MaterialColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_MaterialColor_View>, List<DTO.PriceConfigurationDetailDto>>(dbItems);
        }

        public List<DTO.PriceConfigurationDetailDto> DB2DTO_ListCushionColor(List<PriceConfiguration_CushionColor_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceConfiguration_CushionColor_View>, List<DTO.PriceConfigurationDetailDto>>(dbItems);
        }

        public void DTO2DB_UpdateFCS(DTO.PriceConfigurationDto dtoItem, PriceConfiguration dbItem)
        {
            if (dtoItem.PriceConfigurationDetailOfFCS != null)
            {
                //var dbItems = dbItem.PriceConfigurationDetail.Where(o => dtoItem.PriceConfigurationID.CompareTo(o.PriceConfigurationID) == 0).ToList();

                foreach (PriceConfigurationDetail item in dbItem.PriceConfigurationDetail.ToList())
                {
                    if (!dtoItem.PriceConfigurationDetailOfFCS.Select(s => s.PriceConfigurationDetailID).Contains(item.PriceConfigurationDetailID))
                        dbItem.PriceConfigurationDetail.Remove(item);
                }

                foreach (DTO.PriceConfigurationDetailDto dto in dtoItem.PriceConfigurationDetailOfFCS)
                {
                    PriceConfigurationDetail item;

                    if (dto.PriceConfigurationDetailID <= 0)
                    {
                        item = new PriceConfigurationDetail();
                        dbItem.PriceConfigurationDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceConfigurationDetail.FirstOrDefault(o => o.PriceConfigurationDetailID == dto.PriceConfigurationDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.PriceConfigurationDetailDto, PriceConfigurationDetail>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.PriceConfigurationDto, PriceConfiguration>(dtoItem, dbItem);
        }

        public void DTO2DB_UpdateMaterialColor(DTO.PriceConfigurationDto dtoItem, PriceConfiguration dbItem)
        {
            if (dtoItem.PriceConfigurationDetailOfMaterialColor != null)
            {
                //var dbItems = dbItem.PriceConfigurationDetail.Where(o => dtoItem.PriceConfigurationID.CompareTo(o.PriceConfigurationID) == 0).ToList();

                foreach (PriceConfigurationDetail item in dbItem.PriceConfigurationDetail.ToList())
                {
                    if (!dtoItem.PriceConfigurationDetailOfMaterialColor.Select(s => s.PriceConfigurationDetailID).Contains(item.PriceConfigurationDetailID))
                        dbItem.PriceConfigurationDetail.Remove(item);
                }

                foreach (DTO.PriceConfigurationDetailDto dto in dtoItem.PriceConfigurationDetailOfMaterialColor)
                {
                    PriceConfigurationDetail item;

                    if (dto.PriceConfigurationDetailID <= 0)
                    {
                        item = new PriceConfigurationDetail();

                        dbItem.PriceConfigurationDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceConfigurationDetail.FirstOrDefault(o => o.PriceConfigurationDetailID == dto.PriceConfigurationDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.PriceConfigurationDetailDto, PriceConfigurationDetail>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.PriceConfigurationDto, PriceConfiguration>(dtoItem, dbItem);
        }

        public void DTO2DB_UpdateFrameMaterial(DTO.PriceConfigurationDto dtoItem, PriceConfiguration dbItem)
        {
            if (dtoItem.PriceConfigurationDetailOfFrameMaterial != null)
            {
                //var dbItems = dbItem.PriceConfigurationDetail.Where(o => dtoItem.PriceConfigurationID.CompareTo(o.PriceConfigurationID) == 0).ToList();

                foreach (PriceConfigurationDetail item in dbItem.PriceConfigurationDetail.ToList())
                {
                    if (!dtoItem.PriceConfigurationDetailOfFrameMaterial.Select(s => s.PriceConfigurationDetailID).Contains(item.PriceConfigurationDetailID))
                        dbItem.PriceConfigurationDetail.Remove(item);
                }

                foreach (DTO.PriceConfigurationDetailDto dto in dtoItem.PriceConfigurationDetailOfFrameMaterial)
                {
                    PriceConfigurationDetail item;

                    if (dto.PriceConfigurationDetailID <= 0)
                    {
                        item = new PriceConfigurationDetail();

                        dbItem.PriceConfigurationDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceConfigurationDetail.FirstOrDefault(o => o.PriceConfigurationDetailID == dto.PriceConfigurationDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.PriceConfigurationDetailDto, PriceConfigurationDetail>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.PriceConfigurationDto, PriceConfiguration>(dtoItem, dbItem);
        }

        public void DTO2DB_UpdateCushionColor(DTO.PriceConfigurationDto dtoItem, PriceConfiguration dbItem)
        {
            if (dtoItem.PriceConfigurationDetailOfCushionColor != null)
            {
                //var dbItems = dbItem.PriceConfigurationDetail.Where(o => dtoItem.PriceConfigurationID.CompareTo(o.PriceConfigurationID) == 0).ToList();

                foreach (PriceConfigurationDetail item in dbItem.PriceConfigurationDetail.ToList())
                {
                    if (!dtoItem.PriceConfigurationDetailOfCushionColor.Select(s => s.PriceConfigurationDetailID).Contains(item.PriceConfigurationDetailID))
                        dbItem.PriceConfigurationDetail.Remove(item);
                }

                foreach (DTO.PriceConfigurationDetailDto dto in dtoItem.PriceConfigurationDetailOfCushionColor)
                {
                    PriceConfigurationDetail item;

                    if (dto.PriceConfigurationDetailID <= 0)
                    {
                        item = new PriceConfigurationDetail();

                        dbItem.PriceConfigurationDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceConfigurationDetail.FirstOrDefault(o => o.PriceConfigurationDetailID == dto.PriceConfigurationDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.PriceConfigurationDetailDto, PriceConfigurationDetail>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.PriceConfigurationDto, PriceConfiguration>(dtoItem, dbItem);
        }

        public void DTO2DB_UpdatePackagingMethod(DTO.PriceConfigurationDto dtoItem, PriceConfiguration dbItem)
        {
            if (dtoItem.PriceConfigurationDetailOfPackingMethod != null)
            {
                //var dbItems = dbItem.PriceConfigurationDetail.Where(o => dtoItem.PriceConfigurationID.CompareTo(o.PriceConfigurationID) == 0).ToList();

                foreach (PriceConfigurationDetail item in dbItem.PriceConfigurationDetail.ToList())
                {
                    if (!dtoItem.PriceConfigurationDetailOfPackingMethod.Select(s => s.PriceConfigurationDetailID).Contains(item.PriceConfigurationDetailID))
                        dbItem.PriceConfigurationDetail.Remove(item);
                }

                foreach (DTO.PriceConfigurationDetailDto dto in dtoItem.PriceConfigurationDetailOfPackingMethod)
                {
                    PriceConfigurationDetail item;

                    if (dto.PriceConfigurationDetailID <= 0)
                    {
                        item = new PriceConfigurationDetail();

                        dbItem.PriceConfigurationDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.PriceConfigurationDetail.FirstOrDefault(o => o.PriceConfigurationDetailID == dto.PriceConfigurationDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.PriceConfigurationDetailDto, PriceConfigurationDetail>(dto, item);
                }
            }

            AutoMapper.Mapper.Map<DTO.PriceConfigurationDto, PriceConfiguration>(dtoItem, dbItem);
        }
    }
}
