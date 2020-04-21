using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MaterialOptionMng
{
    internal class DataConverter
    {
        private MaterialOptionTestReport dbTest;

        public DataConverter()
        {
            string mapName = "MaterialOptionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialOptionSearchResult_View, DTO.MaterialOptionMng.MaterialOptionSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.MaterialOptionTestReports, o => o.MapFrom(s => s.MaterialOptionMng_MaterialOptionTestReport_View))
                    .ForMember(d => d.materialTestingDTOs, o => o.MapFrom(s => s.MaterialOptionMng_MaterialTesting_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialOptionProductGroup_View, DTO.MaterialOptionMng.ProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialOption_View, DTO.MaterialOptionMng.MaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialTestingDTOs, o => o.MapFrom(s => s.MaterialOptionMng_MaterialTesting_View))
                    .ForMember(d => d.ProductGroups, o => o.MapFrom(s => s.MaterialOptionMng_MaterialOptionProductGroup_View))
                    .ForMember(d => d.MaterialOptionTestReports, o => o.MapFrom(s => s.MaterialOptionMng_MaterialOptionTestReport_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialOptionTestReport_View, DTO.MaterialOptionMng.MaterialOptionTestReport>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialOptionMng.ProductGroup, MaterialOptionProductGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialOptionProductGroupID, o => o.Ignore())
                    .ForMember(d => d.MaterialOptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.MaterialOptionMng.MaterialOption, MaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.MaterialOptionProductGroup, o => o.Ignore())
                    .ForMember(d => d.MaterialOptionID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.MaterialOptionTestReport, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.MaterialOptionMng.MaterialOptionTestReport, MaterialOptionTestReport>()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.MaterialOptionTestReportID, o => o.Ignore())
                    .ForMember(d => d.MaterialOptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialTestingFile_View, DTO.MaterialOptionMng.MaterialTestingFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialTestingStandard_View, DTO.MaterialOptionMng.MaterialTestingStandardDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(s => s.Condition(d => !d.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MaterialOptionMng_MaterialTesting_View, DTO.MaterialOptionMng.MaterialTestingDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TestDate, o => o.ResolveUsing(s => s.TestDate.HasValue ? s.TestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.MaterialTestingFileDTOs, o => o.MapFrom(s => s.MaterialOptionMng_MaterialTestingFile_View))
                    .ForMember(d => d.MaterialTestingStandardDTOs, o => o.MapFrom(s => s.MaterialOptionMng_MaterialTestingStandard_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MaterialOptionMng.MaterialOptionSearchResult> DB2DTO_MaterialOptionSearchResultList(List<MaterialOptionMng_MaterialOptionSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MaterialOptionMng_MaterialOptionSearchResult_View>, List<DTO.MaterialOptionMng.MaterialOptionSearchResult>>(dbItems);
        }

        public DTO.MaterialOptionMng.MaterialOption DB2DTO_MaterialOption(MaterialOptionMng_MaterialOption_View dbItem)
        {
            DTO.MaterialOptionMng.MaterialOption dtoItem =  AutoMapper.Mapper.Map<MaterialOptionMng_MaterialOption_View, DTO.MaterialOptionMng.MaterialOption>(dbItem);
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);
            return dtoItem;
        }

        public void DTO2DB(DTO.MaterialOptionMng.MaterialOption dtoItem, ref MaterialOption dbItem)
        {
            AutoMapper.Mapper.Map<DTO.MaterialOptionMng.MaterialOption, MaterialOption>(dtoItem, dbItem);
            //dbItem.UpdatedDate = DateTime.Now;

            //if (dtoItem.MaterialOptionID == 0)
            //{
            //    dbItem.CreatedBy = dtoItem.CreatedBy;
            //    dbItem.CreatedDate = DateTime.Now;
            //}

            // map child
            if (dtoItem.ProductGroups != null)
            {
                // map child rows
                foreach (DTO.MaterialOptionMng.ProductGroup dtoGroup in dtoItem.ProductGroups)
                {
                    MaterialOptionProductGroup dbGroup;
                    if (dtoGroup.MaterialOptionProductGroupID <= 0)
                    {
                        dbGroup = new MaterialOptionProductGroup();
                        dbItem.MaterialOptionProductGroup.Add(dbGroup);
                    }
                    else
                    {
                        dbGroup = dbItem.MaterialOptionProductGroup.FirstOrDefault(o => o.MaterialOptionProductGroupID == dtoGroup.MaterialOptionProductGroupID);
                    }

                    if (dbGroup != null)
                    {
                        AutoMapper.Mapper.Map<DTO.MaterialOptionMng.ProductGroup, MaterialOptionProductGroup>(dtoGroup, dbGroup);
                    }
                }
            }

            if (dtoItem.MaterialOptionTestReports != null)
            {
                // check for child rows deleted
                foreach (var dbReport in dbItem.MaterialOptionTestReport.ToArray())
                {
                    if (!dtoItem.MaterialOptionTestReports.Select(o => o.MaterialOptionTestReportID).Contains(dbReport.MaterialOptionTestReportID))
                    {
                        dbItem.MaterialOptionTestReport.Remove(dbReport);
                    }
                }

                foreach (var dtoReport in dtoItem.MaterialOptionTestReports)
                {
                    MaterialOptionTestReport dbReport;

                    if (dtoReport.MaterialOptionTestReportID <= 0)
                    {
                        dbReport = new MaterialOptionTestReport();
                        dbItem.MaterialOptionTestReport.Add(dbReport);
                    }
                    else
                    {
                        dbReport = dbItem.MaterialOptionTestReport.FirstOrDefault(o => o.MaterialOptionTestReportID == dtoReport.MaterialOptionTestReportID);
                    }

                    if (dbReport != null)
                    {
                        AutoMapper.Mapper.Map<DTO.MaterialOptionMng.MaterialOptionTestReport, MaterialOptionTestReport>(dtoReport, dbReport);

                        dbReport.UpdatedBy = dtoItem.UpdatedBy;
                        dbReport.UpdatedDate = DateTime.Now;
                    }
                }
            }
        }
    }
}
