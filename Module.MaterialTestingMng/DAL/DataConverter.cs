using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTestingMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;
        public DataConverter()
        {
            string mapName = "MaterialTestingMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Search View
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_SearchMaterialTesting_View, DTO.MaterialTestReportSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.MaterialTestingFileSearchResultDTOs, o => o.MapFrom(s => s.MaterialTestingMng_MaterialTesting_SearchFilesView))
                    .ForMember(d => d.MaterialTestStandardSearchViewDTOs, o => o.MapFrom(s => s.MaterialTestingMng_SearchMaterialTestStandard_SearchView))
                    //.ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTesting_SearchFilesView, DTO.MaterialFileSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : " ")))
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //Search Result Test Standard
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_SearchMaterialTestStandard_SearchView, DTO.MaterialTestStandardSearchViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //GetView TestStandard 
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTestStandard_View, DTO.MaterialTestStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //GetView
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTestReport_View, DTO.MaterialTestReportDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTestReportFileDTOs, o => o.MapFrom(s => s.MaterialTestingMng_MaterialTestReportFile_View))
                    .ForMember(d => d.MaterialTestStandardDTOs, o => o.MapFrom(s => s.MaterialTestingMng_MaterialTestStandard_View))
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //GetView File
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTestReportFile_View, DTO.MaterialTestReportFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Get quick search
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialOption_View, DTO.SupportMaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //Test File 
                AutoMapper.Mapper.CreateMap<DTO.MaterialTestReportFileDTO, MaterialTestReportFile>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.MaterialTestReportFileID, o => o.Ignore());

                //DB2DTO MaterialTesting
                AutoMapper.Mapper.CreateMap<DTO.MaterialTestReportDTO, MaterialTestReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTestReportID, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.TestDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DTO2DB

                AutoMapper.Mapper.CreateMap<DTO.MaterialTestStandardDTO, MaterialTestUsingMaterialStandard>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTestUsingMaterialStandardID, o => o.Ignore())
                    .ForMember(d => d.MaterialTestReportID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //Support Material Test Standard list
                AutoMapper.Mapper.CreateMap<SupportMng_MaterialTestStandard_View, DTO.SupportMaterialTestStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //OVERVIEW
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTestReport_OverView, DTO.MaterialTestReportOverViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTestReportFileOverViewDTOs, o => o.MapFrom(s => s.MaterialTestingMng_MaterialTestReportFile_OverView))
                    .ForMember(d => d.MaterialTestStandardOverViewDTOs, o => o.MapFrom(s => s.MaterialTestingMng_MaterialTestStandard_OverView))
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTestReportFile_OverView, DTO.MaterialTestReportFileOverViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<MaterialTestingMng_MaterialTestStandard_OverView, DTO.MaterialTestStandardOverViewDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.MaterialTestReportSearchDTO> DB2DTO_MaterialTestReportSearch(List<MaterialTestingMng_SearchMaterialTesting_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialTestingMng_SearchMaterialTesting_View>, List<DTO.MaterialTestReportSearchDTO>>(dbItems);
        }

        public DTO.MaterialTestReportDTO DB2DTO_GetMaterialTestReport(MaterialTestingMng_MaterialTestReport_View dbItems)
        {
            return AutoMapper.Mapper.Map<MaterialTestingMng_MaterialTestReport_View, DTO.MaterialTestReportDTO>(dbItems);
        }

        public DTO.MaterialTestReportOverViewDTO DB2DTO_GetOverViewMaterialTestReport(MaterialTestingMng_MaterialTestReport_OverView dbItems)
        {
            return AutoMapper.Mapper.Map<MaterialTestingMng_MaterialTestReport_OverView, DTO.MaterialTestReportOverViewDTO>(dbItems);
        }

        public List<DTO.SupportMaterialOptionDTO> DB2DTO_GetMaterialOption(List<MaterialTestingMng_MaterialOption_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<MaterialTestingMng_MaterialOption_View>, List<DTO.SupportMaterialOptionDTO>>(dbItem);
        }

        public List<DTO.SupportMaterialTestStandardDTO> DB2DTO_GetSupportMaterialTestStandard(List<SupportMng_MaterialTestStandard_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_MaterialTestStandard_View>, List<DTO.SupportMaterialTestStandardDTO>>(dbItems);
        }
        public void DTO2DB_MaterialTestReport(DTO.MaterialTestReportDTO dtoItem, ref MaterialTestReport dbItem, string TmpFile, int userId)
        {
            foreach (MaterialTestReportFile dbFile in dbItem.MaterialTestReportFile.ToArray())
            {
                if (!dtoItem.MaterialTestReportFileDTOs.Select(o => o.MaterialTestReportFileID).Contains(dbFile.MaterialTestReportFileID))
                {
                    if (!string.IsNullOrEmpty(dbFile.FileUD))
                    {
                        // remove file
                        fwFactory.RemoveImageFile(dbFile.FileUD);
                    }
                    dbItem.MaterialTestReportFile.Remove(dbFile);
                }
            }
            foreach (DTO.MaterialTestReportFileDTO dtoFile in dtoItem.MaterialTestReportFileDTOs)
            {
                MaterialTestReportFile dbFile;
                if (dtoFile.MaterialTestReportFileID <= 0)
                {
                    dbFile = new MaterialTestReportFile();
                    dbItem.MaterialTestReportFile.Add(dbFile);
                }
                else
                {
                    dbFile = dbItem.MaterialTestReportFile.FirstOrDefault(o => o.MaterialTestReportFileID == dtoFile.MaterialTestReportFileID);
                }

                if (dbFile != null)
                {
                    // change or add file
                    if (dtoFile.ScanHasChange)
                    {
                        dtoFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.ScanNewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }
                    AutoMapper.Mapper.Map<DTO.MaterialTestReportFileDTO, MaterialTestReportFile>(dtoFile, dbFile);
                }
            }

            if (dtoItem.MaterialTestReportFileDTOs != null)
            {
                foreach (MaterialTestReportFile item in  dbItem.MaterialTestReportFile.ToList())
                {
                    if (!dtoItem.MaterialTestReportFileDTOs.Select(s => s.MaterialTestReportFileID).Contains(item.MaterialTestReportFileID))
                        dbItem.MaterialTestReportFile.Remove(item);
                }

                foreach (DTO.MaterialTestReportFileDTO dto in dtoItem.MaterialTestReportFileDTOs)
                {
                    MaterialTestReportFile item;

                    if (dto.MaterialTestReportFileID < 0)
                    {
                        item = new MaterialTestReportFile();

                        dbItem.MaterialTestReportFile.Add(item);
                    }
                    else
                    {
                        item = dbItem.MaterialTestReportFile.FirstOrDefault(s => s.MaterialTestReportFileID == dto.MaterialTestReportFileID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.MaterialTestReportFileDTO, MaterialTestReportFile>(dto, item);
                }
            }

            if (dtoItem.MaterialTestStandardDTOs != null)
            {

                foreach (var item in dbItem.MaterialTestUsingMaterialStandard.ToArray())
                {
                    if (!dtoItem.MaterialTestStandardDTOs.Select(o => o.MaterialTestUsingMaterialStandardID).Contains(item.MaterialTestUsingMaterialStandardID))
                    {
                        dbItem.MaterialTestUsingMaterialStandard.Remove(item);
                    }
                }

                foreach (var item in dtoItem.MaterialTestStandardDTOs)
                {
                    MaterialTestUsingMaterialStandard dbTestStandard = new MaterialTestUsingMaterialStandard();
                    if (item.MaterialTestUsingMaterialStandardID < 0)
                    {
                        dbItem.MaterialTestUsingMaterialStandard.Add(dbTestStandard);
                    }
                    else
                    {
                        dbTestStandard = dbItem.MaterialTestUsingMaterialStandard.Where(s => s.MaterialTestUsingMaterialStandardID == item.MaterialTestUsingMaterialStandardID).FirstOrDefault();

                    }
                    if (dbTestStandard != null)
                    {
                        AutoMapper.Mapper.Map<DTO.MaterialTestStandardDTO, MaterialTestUsingMaterialStandard>(item, dbTestStandard);
                    }
                }
            }

            //if (!string.IsNullOrEmpty(dtoItem.TestDate))
            //{
            //    if (DateTime.TryParse(dtoItem.TestDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //    }
            //}
            //if (!string.IsNullOrEmpty(dtoItem.UpdatedDate))
            //{
            //    if (DateTime.TryParse(dtoItem.UpdatedDate, nl, System.Globalization.DateTimeStyles.None, out tmpUpdateDate))
            //    {
            //    }
            //}
            AutoMapper.Mapper.Map<DTO.MaterialTestReportDTO, MaterialTestReport>(dtoItem, dbItem);
            dbItem.TestDate = dtoItem.TestDate.ConvertStringToDateTime();
        }
    }
}
