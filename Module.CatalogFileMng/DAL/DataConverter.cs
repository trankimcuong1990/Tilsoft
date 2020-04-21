using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;


namespace Module.CatalogFileMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
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
                AutoMapper.Mapper.CreateMap<CatalogFileMng_CatalogFileSearchResult_View, DTO.CatalogFileSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.PLFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.PLFileLocation) ? s.PLFileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CatalogFileMng_CatalogFile_View, DTO.CatalogFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.PLFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.PLFileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CatalogFileDTO, CatalogFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CatalogFileID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.PriceListFileUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CatalogFileSearchResult> DB2DTO_CatalogFileSearchResultList(List<CatalogFileMng_CatalogFileSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CatalogFileMng_CatalogFileSearchResult_View>, List<DTO.CatalogFileSearchResult>>(dbItems);
        }

        public DTO.CatalogFileDTO DB2DTO_CatalogFile(CatalogFileMng_CatalogFile_View dbItem)
        {
            return AutoMapper.Mapper.Map<CatalogFileMng_CatalogFile_View, DTO.CatalogFileDTO>(dbItem);
        }

        public void DTO2DB(DTO.CatalogFileDTO dtoItem, ref CatalogFile dbItem, string TmpFile, int userId)
        {
            // employee
            AutoMapper.Mapper.Map<DTO.CatalogFileDTO, CatalogFile>(dtoItem, dbItem);
            if (dtoItem.HasChanged)
            {
                dbItem.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoItem.NewFile, dbItem.FileUD, dtoItem.FriendlyName);
            }
            if (dtoItem.PLHasChanged)
            {
                dbItem.PriceListFileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoItem.PLNewFile, dbItem.PriceListFileUD, dtoItem.PLFriendlyName);
            }
        }
    }
}
