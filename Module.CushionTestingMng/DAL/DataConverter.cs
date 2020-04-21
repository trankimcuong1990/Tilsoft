using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng.DAL
{
    public class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;
        public DataConverter()
        {
            string mapName = "CushionTestingMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Search View
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestReport_SearchView, DTO.CushionTestReportSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CushionTestFileSearchDTOs, o => o.MapFrom(s => s.CushionTestingMng_CushionTestReportFile_SearchView))
                    .ForMember(d => d.CushionTestStandardSearchDTOs, o => o.MapFrom(s => s.CushionTestingMng_CushionTestStandard_SearchView))
                    //.ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestReportFile_SearchView, DTO.CushionTestFileSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : " ")))
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //Search Result Test Standard
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestStandard_SearchView, DTO.CushionTestStandardSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //GetView TestStandard 
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestStandard_View, DTO.CushionTestStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //GetView
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestReport_View, DTO.CushionTestReportDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CushionTestReportFileDTOs, o => o.MapFrom(s => s.CushionTestingMng_CushionTestReportFile_View))
                    .ForMember(d => d.CushionTestStandardDTOs, o => o.MapFrom(s => s.CushionTestingMng_CushionTestStandard_View))
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //GetView File
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestReportFile_View, DTO.CushionTestReportFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Get quick search
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionColor_View, DTO.SupportCushionColorDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //Test File 
                AutoMapper.Mapper.CreateMap<DTO.CushionTestReportFileDTO, CushionTestReportFile>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CushionTestReportFileID, o => o.Ignore());

                //DB2DTO MaterialTesting
                AutoMapper.Mapper.CreateMap<DTO.CushionTestReportDTO, CushionTestReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CushionTestReportID, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.TestDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DTO2DB

                AutoMapper.Mapper.CreateMap<DTO.CushionTestStandardDTO, CushionTestReportUsingCushionStandard>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CushionTestReportUsingCushionStandardID, o => o.Ignore())
                    .ForMember(d => d.CushionTestReportID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                //Support Material Test Standard list
                AutoMapper.Mapper.CreateMap<SupportMng_CushionTestStandard_View, DTO.SupportCushionTestStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //OVERVIEW
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestReport_OverView, DTO.CushionTestReportOverViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CushionTestReportFileOverViewDTOs, o => o.MapFrom(s => s.CushionTestingMng_CushionTestReportFile_OverView))
                    .ForMember(d => d.CushionTestStandardOverViewDTOs, o => o.MapFrom(s => s.CushionTestingMng_CushionTestStandard_OverView))
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestReportFile_OverView, DTO.CushionTestReportFileOverViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<CushionTestingMng_CushionTestStandard_OverView, DTO.CushionTestStandardOverViewDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CushionTestReportSearchDTO> DB2DTO_CushionTestReportSearch(List<CushionTestingMng_CushionTestReport_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CushionTestingMng_CushionTestReport_SearchView>, List<DTO.CushionTestReportSearchDTO>>(dbItems);
        }

        public DTO.CushionTestReportDTO DB2DTO_GetCushionTestReport(CushionTestingMng_CushionTestReport_View dbItems)
        {
            return AutoMapper.Mapper.Map<CushionTestingMng_CushionTestReport_View, DTO.CushionTestReportDTO>(dbItems);
        }

        public DTO.CushionTestReportOverViewDTO DB2DTO_GetOverViewCushionTestReport(CushionTestingMng_CushionTestReport_OverView dbItems)
        {
            return AutoMapper.Mapper.Map<CushionTestingMng_CushionTestReport_OverView, DTO.CushionTestReportOverViewDTO>(dbItems);
        }

        public List<DTO.SupportCushionColorDTO> DB2DTO_GetCushionColor(List<CushionTestingMng_CushionColor_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<CushionTestingMng_CushionColor_View>, List<DTO.SupportCushionColorDTO>>(dbItem);
        }

        public List<DTO.SupportCushionTestStandardDTO> DB2DTO_GetSupportCushionTestStandard(List<SupportMng_CushionTestStandard_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionTestStandard_View>, List<DTO.SupportCushionTestStandardDTO>>(dbItems);
        }
        public void DTO2DB_CushionTestReport(DTO.CushionTestReportDTO dtoItem, ref CushionTestReport dbItem, string TmpFile, int userId)
        {
            foreach (CushionTestReportFile dbFile in dbItem.CushionTestReportFile.ToArray())
            {
                if (!dtoItem.CushionTestReportFileDTOs.Select(o => o.CushionTestReportFileID).Contains(dbFile.CushionTestReportFileID))
                {
                    if (!string.IsNullOrEmpty(dbFile.FileUD))
                    {
                        // remove file
                        fwFactory.RemoveImageFile(dbFile.FileUD);
                    }
                    dbItem.CushionTestReportFile.Remove(dbFile);
                }
            }
            foreach (DTO.CushionTestReportFileDTO dtoFile in dtoItem.CushionTestReportFileDTOs)
            {
                CushionTestReportFile dbFile;
                if (dtoFile.CushionTestReportFileID <= 0)
                {
                    dbFile = new CushionTestReportFile();
                    dbItem.CushionTestReportFile.Add(dbFile);
                }
                else
                {
                    dbFile = dbItem.CushionTestReportFile.FirstOrDefault(o => o.CushionTestReportFileID == dtoFile.CushionTestReportFileID);
                }

                if (dbFile != null)
                {
                    // change or add file
                    if (dtoFile.ScanHasChange)
                    {
                        dtoFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.ScanNewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }
                    AutoMapper.Mapper.Map<DTO.CushionTestReportFileDTO, CushionTestReportFile>(dtoFile, dbFile);
                }
            }

            if (dtoItem.CushionTestReportFileDTOs != null)
            {
                foreach (CushionTestReportFile item in dbItem.CushionTestReportFile.ToList())
                {
                    if (!dtoItem.CushionTestReportFileDTOs.Select(s => s.CushionTestReportFileID).Contains(item.CushionTestReportFileID))
                        dbItem.CushionTestReportFile.Remove(item);
                }

                foreach (DTO.CushionTestReportFileDTO dto in dtoItem.CushionTestReportFileDTOs)
                {
                    CushionTestReportFile item;

                    if (dto.CushionTestReportFileID < 0)
                    {
                        item = new CushionTestReportFile();

                        dbItem.CushionTestReportFile.Add(item);
                    }
                    else
                    {
                        item = dbItem.CushionTestReportFile.FirstOrDefault(s => s.CushionTestReportFileID == dto.CushionTestReportFileID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.CushionTestReportFileDTO, CushionTestReportFile>(dto, item);
                }
            }

            if (dtoItem.CushionTestStandardDTOs != null)
            {

                foreach (var item in dbItem.CushionTestReportUsingCushionStandard.ToArray())
                {
                    if (!dtoItem.CushionTestStandardDTOs.Select(o => o.CushionTestReportUsingCushionStandardID).Contains(item.CushionTestReportUsingCushionStandardID))
                    {
                        dbItem.CushionTestReportUsingCushionStandard.Remove(item);
                    }
                }

                foreach (var item in dtoItem.CushionTestStandardDTOs)
                {
                    CushionTestReportUsingCushionStandard dbTestStandard = new CushionTestReportUsingCushionStandard();
                    if (item.CushionTestReportUsingCushionStandardID < 0)
                    {
                        dbItem.CushionTestReportUsingCushionStandard.Add(dbTestStandard);
                    }
                    else
                    {
                        dbTestStandard = dbItem.CushionTestReportUsingCushionStandard.Where(s => s.CushionTestReportUsingCushionStandardID == item.CushionTestReportUsingCushionStandardID).FirstOrDefault();

                    }
                    if (dbTestStandard != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CushionTestStandardDTO, CushionTestReportUsingCushionStandard>(item, dbTestStandard);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.CushionTestReportDTO, CushionTestReport>(dtoItem, dbItem);
            dbItem.TestDate = dtoItem.TestDate.ConvertStringToDateTime();
        }
    }
}
