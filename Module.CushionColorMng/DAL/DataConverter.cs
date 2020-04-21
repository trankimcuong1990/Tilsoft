using AutoMapper;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionColorMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CushionColorMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CushionColorMng_CushionColorSearchResult_View, DTO.CushionColorSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation1, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation1) ? s.FileLocation1 : "no-image.jpg")))
                    .ForMember(d => d.FileLocation2, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation2) ? s.FileLocation2 : "no-image.jpg")))
                    .ForMember(d => d.FileLocation3, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation3) ? s.FileLocation3 : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_FullSizeUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ImageFile_FullSizeUrl) ? s.ImageFile_FullSizeUrl : "no-image.jpg")))
                    .ForMember(d => d.CushionColorTestReports, o => o.MapFrom(s => s.CushionColorMng_CushionColorTestReport_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CushionColorMng_CushionColorProductGroup_View, DTO.CushionColorProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CushionColorMng_CushionColor_View, DTO.CushionColor>()
                    .IgnoreAllNonExisting()
                     .ForMember(d => d.CushionTestingDTOs, o => o.MapFrom(s => s.CushionColorMng_CushionTesting_View))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.FileLocation1, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation1) ? s.FileLocation1 : "no-image.jpg")))
                    .ForMember(d => d.FileLocation2, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation2) ? s.FileLocation2 : "no-image.jpg")))
                    .ForMember(d => d.FileLocation3, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation3) ? s.FileLocation3 : "no-image.jpg")))
                    .ForMember(d => d.CushionColorProductGroups, o => o.MapFrom(s => s.CushionColorMng_CushionColorProductGroup_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.CushionColorTestReports, o => o.MapFrom(s => s.CushionColorMng_CushionColorTestReport_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CushionColorProductGroup, CushionColorProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CushionColorProductGroupID, o => o.Ignore())
                    .ForMember(d => d.CushionColorID, o => o.Ignore());

                Mapper.CreateMap<CushionColorMng_CushionColorTestReport_View, DTO.CushionColorTestReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<DTO.CushionColorTestReport, CushionColorTestReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CushionColorTestReportID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.CushionColor, CushionColor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile, o => o.Ignore())
                    .ForMember(d => d.TestReportFile1, o => o.Ignore())
                    .ForMember(d => d.TestReportFile2, o => o.Ignore())
                    .ForMember(d => d.TestReportFile3, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CushionColorID, o => o.Ignore())
                    .ForMember(d => d.CushionColorUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<CushionColorMng_CushionTestingFile_View, DTO.CushionTestingFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CushionColorMng_CushionTestingStandard_View, DTO.CushionTestingStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(s => s.Condition(d => !d.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CushionColorMng_CushionTesting_View, DTO.CushionTestingDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CushionTestingFileDTOs, o => o.MapFrom(s => s.CushionColorMng_CushionTestingFile_View))
                    .ForMember(d => d.CushionTestingStandardDTOs, o => o.MapFrom(s => s.CushionColorMng_CushionTestingStandard_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_CushionType_View, DTO.CushionType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(s => s.Condition(d => !d.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(s => s.Condition(d => !d.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CushionType> DB2DTO_CushionType(List<SupportMng_CushionType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_CushionType_View>, List<DTO.CushionType>>(dbItems);
        }

        public List<DTO.ProductGroup> DB2DTO_ProductGroup(List<SupportMng_ProductGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.ProductGroup>>(dbItems);
        }

        public List<DTO.CushionColorSearchResult> DB2DTO_CushionColorSearchResultList(List<CushionColorMng_CushionColorSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CushionColorMng_CushionColorSearchResult_View>, List<DTO.CushionColorSearchResult>>(dbItems);
        }

        public DTO.CushionColor DB2DTO_CushionColor(CushionColorMng_CushionColor_View dbItem)
        {
            return AutoMapper.Mapper.Map<CushionColorMng_CushionColor_View, DTO.CushionColor>(dbItem);
        }

        public void DTO2BD(DTO.CushionColor dtoItem, string tempFolder, ref CushionColor dbItem)
        {
            AutoMapper.Mapper.Map<DTO.CushionColor, CushionColor>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;

            // Tri 
            // Add created Cushion Color
            if (dtoItem.CushionColorID == 0)
            {
                dbItem.CreatedBy = dtoItem.CreatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }

            // map child
            if (dtoItem.CushionColorProductGroups != null)
            {
                // map child rows
                foreach (DTO.CushionColorProductGroup dtoGroup in dtoItem.CushionColorProductGroups)
                {
                    CushionColorProductGroup dbGroup;
                    if (dtoGroup.CushionColorProductGroupID <= 0)
                    {
                        dbGroup = new CushionColorProductGroup();
                        dbItem.CushionColorProductGroup.Add(dbGroup);
                    }
                    else
                    {
                        dbGroup = dbItem.CushionColorProductGroup.FirstOrDefault(o => o.CushionColorProductGroupID == dtoGroup.CushionColorProductGroupID);
                    }

                    if (dbGroup != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CushionColorProductGroup, CushionColorProductGroup>(dtoGroup, dbGroup);
                    }
                }
            }

            // Mapping cushion color test report
            if (dtoItem.CushionColorTestReports != null)
            {
                foreach (var item in dbItem.CushionColorTestReport.ToArray())
                {
                    if (!dtoItem.CushionColorTestReports.Select(s => s.CushionColorTestReportID).Contains(item.CushionColorTestReportID))
                    {
                        dbItem.CushionColorTestReport.Remove(item);
                    }
                }

                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

                // Mapping cushion color test report rows
                foreach (DTO.CushionColorTestReport dtoCushionColorTestReport in dtoItem.CushionColorTestReports)
                {
                    CushionColorTestReport dbCushionColorTestReport;

                    if (dtoCushionColorTestReport.CushionColorTestReportID <= 0)
                    {
                        dbCushionColorTestReport = new CushionColorTestReport();
                        dbItem.CushionColorTestReport.Add(dbCushionColorTestReport);
                    }
                    else
                    {
                        dbCushionColorTestReport = dbItem.CushionColorTestReport.FirstOrDefault(o => o.CushionColorTestReportID == dtoCushionColorTestReport.CushionColorTestReportID);
                    }

                    if (dbCushionColorTestReport != null)
                    {
                        Mapper.Map<DTO.CushionColorTestReport, CushionColorTestReport>(dtoCushionColorTestReport, dbCushionColorTestReport);

                        if (dtoCushionColorTestReport.File_HasChange.HasValue && dtoCushionColorTestReport.File_HasChange.Value)
                        {
                            dbCushionColorTestReport.FileUD = fwFactory.CreateNoneImageFilePointer(tempFolder, dtoCushionColorTestReport.File_NewFile, dtoCushionColorTestReport.FileUD);
                        }
                    }
                }
            }
        }
    }
}
