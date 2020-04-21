using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.PriceListFile.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PriceListFile_PriceListFileSearch_View, DTO.PriceListFileSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PDFFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.PDFFileLocation) ? s.PDFFileLocation : "no-image.jpg")))
                    .ForMember(d => d.ExcelFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ExcelFileLocation) ? s.ExcelFileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PriceListFile_PriceListFile_View, DTO.PriceListFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PDFFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.PDFFileLocation) ? s.PDFFileLocation : "no-image.jpg")))
                    .ForMember(d => d.ExcelFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ExcelFileLocation) ? s.ExcelFileLocation : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PriceListFile, PriceListFile>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.PriceListFileID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PriceListFileSearch> DB2DTO_PriceListFileSearchList(List<PriceListFile_PriceListFileSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PriceListFile_PriceListFileSearch_View>, List<DTO.PriceListFileSearch>>(dbItems);
        }

        public DTO.PriceListFile DB2DTO_PriceListFile(PriceListFile_PriceListFile_View dbItem)
        {
            return AutoMapper.Mapper.Map<PriceListFile_PriceListFile_View, DTO.PriceListFile>(dbItem);
        }

        public void DTO2DB(DTO.PriceListFile dtoItem, ref PriceListFile dbItem)
        {
            AutoMapper.Mapper.Map<DTO.PriceListFile, PriceListFile>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
