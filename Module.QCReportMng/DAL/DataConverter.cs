using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DAL
{
    public class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataConverter()
        {
            string mapName = "QCReportMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                //Search View
                AutoMapper.Mapper.CreateMap<QCReportMng_QCReport_SearchView, DTO.QCReportSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.InspectedDate, o => o.ResolveUsing(s => s.InspectedDate.HasValue ? s.InspectedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportSetting_SearchView, DTO.SupportQCReportSetting>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportSection_View, DTO.QCReportSectionDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReport_View2, DTO.QCReportDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportSummaryDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportSummary_View))
                .ForMember(d => d.QCReportDetailDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportDetail_View2))
                .ForMember(d => d.QCReportDefectDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportDefect_View2))
                .ForMember(d => d.QCReportImageDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportImage_View2))
                .ForMember(d => d.QCReportTestEnvironmentDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportTestEnvironment_View))
                .ForMember(d => d.QCReportFactoryOrderDetailDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportFactoryOrderDetail_View))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                .ForMember(d => d.ReportFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ReportFileLocation) ? s.ReportFileLocation : "no-image.jpg")))
                .ForMember(d => d.InspectedDate, o => o.ResolveUsing(s => s.InspectedDate.HasValue ? s.InspectedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportSummary_View, DTO.QCReportSummaryDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportFactoryOrderDetail_View, DTO.QCReportFactoryOrderDetailDTO>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportDetail_View2, DTO.QCReportDetailDTO>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportDefect_View2, DTO.QCReportDefectDTO>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportImage_View2, DTO.QCReportImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportTestEnvironment_View, DTO.QCReportTestEnvironmentDTO>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportTestEnvironmentItem_View, DTO.QCReportTestEnvironmentItemDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_QCReportTestEnvironmentCategory_View, DTO.QCReportTestEnvironmentCategoryDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportTestEnvironmentItemDTOs, o => o.MapFrom(s => s.QCReportMng_QCReportTestEnvironmentItem_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_InspectionStage_View, DTO.SupportInspectionStage>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_InspectionResult_View, DTO.SupportInspectionResult>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_PackagingMethod_View, DTO.SupportPackagingMethod>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_QCReportSummaryResult_View, DTO.SupportSummaryResult>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QCReportMng_ListPIFromFactoryOrder_View, DTO.ListPIFromFactoryOrderDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportDTO, QCReport>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportID, o => o.Ignore())
                .ForMember(d => d.InspectedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportSummaryDTO, QCReportSummary>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportSummaryID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportTestEnvironmentDTO, QCReportTestEnvironment>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportTestEnvironmentID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportDetailDTO, QCReportDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportDetailID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportDefectDTO, QCReportDefect>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportDefectID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QCReportImageDTO, QCReportImage>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.QCReportImageID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.QCReportFactoryOrderDetailDTO, QCReportFactoryOrderDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QCReportFactoryOrderDetailID, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.SupportQCReportSetting> DB2DTO_SearchPI(List<QCReportMng_QCReportSetting_SearchView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReportSetting_SearchView>, List<DTO.SupportQCReportSetting>>(dbItem);
        }

        public List<DTO.QCReportSearchDTO> DB2DTO_SearchQCReport(List<QCReportMng_QCReport_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReport_SearchView>, List<DTO.QCReportSearchDTO>>(dbItems);
        }

        public List<DTO.SupportSummaryResult> DB2DTO_GetSummaryResult(List<SupportMng_QCReportSummaryResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QCReportSummaryResult_View>, List<DTO.SupportSummaryResult>>(dbItem);
        }
        public List<DTO.SupportPackagingMethod> DB2DTO_GetPackagingMethod(List<QCReportMng_PackagingMethod_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_PackagingMethod_View>, List<DTO.SupportPackagingMethod>>(dbItem);
        }

        public List<DTO.SupportInspectionStage> DB2DTO_GetInspectionStage(List<SupportMng_InspectionStage_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_InspectionStage_View>, List<DTO.SupportInspectionStage>>(dbItems);
        }

        public List<DTO.SupportInspectionResult> DB2DTO_GetInspectionResult(List<SupportMng_InspectionResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_InspectionResult_View>, List<DTO.SupportInspectionResult>>(dbItems);
        }

        public List<DTO.QCReportSectionDTO> DB2DTO_GetSection(List<QCReportMng_QCReportSection_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReportSection_View>, List<DTO.QCReportSectionDTO>>(dbItem);
        }

        public List<DTO.QCReportTestEnvironmentCategoryDTO> DB2DTO_GetTestEnvironmentCategory(List<QCReportMng_QCReportTestEnvironmentCategory_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_QCReportTestEnvironmentCategory_View>, List<DTO.QCReportTestEnvironmentCategoryDTO>>(dbItem);
        }

        public List<DTO.ListPIFromFactoryOrderDTO> DB2DTO_ListPIFromFactoryOrderDTO(List<QCReportMng_ListPIFromFactoryOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<QCReportMng_ListPIFromFactoryOrder_View>, List<DTO.ListPIFromFactoryOrderDTO>>(dbItem);
        }

        public DTO.QCReportDTO DB2DTO_GetQCReport(QCReportMng_QCReport_View2 dbItems)
        {
            return AutoMapper.Mapper.Map<QCReportMng_QCReport_View2, DTO.QCReportDTO>(dbItems);
        }

        public void DTO2DB_QCReport(DTO.QCReportDTO dtoItem, ref QCReport dbItem)
        {

            //QCReportImage
            if (dtoItem.QCReportImageDTOs != null)
            {
                foreach (QCReportImage item in dbItem.QCReportImage.ToList())
                {
                    if (!dtoItem.QCReportImageDTOs.Select(s => s.QCReportImageID).Contains(item.QCReportImageID))
                        dbItem.QCReportImage.Remove(item);
                }

                foreach (DTO.QCReportImageDTO dto in dtoItem.QCReportImageDTOs)
                {
                    QCReportImage item;

                    if (dto.QCReportImageID < 0)
                    {
                        item = new QCReportImage();

                        dbItem.QCReportImage.Add(item);
                    }
                    else
                    {
                        item = dbItem.QCReportImage.FirstOrDefault(s => s.QCReportImageID == dto.QCReportImageID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.QCReportImageDTO, QCReportImage>(dto, item);
                }
            }

            //QCReportSummary
            if (dtoItem.QCReportSummaryDTOs != null)
            {
                foreach (QCReportSummary item in dbItem.QCReportSummary.ToList())
                {
                    if (!dtoItem.QCReportSummaryDTOs.Select(s => s.QCReportSummaryID).Contains(item.QCReportSummaryID))
                        dbItem.QCReportSummary.Remove(item);
                }

                foreach (DTO.QCReportSummaryDTO dto in dtoItem.QCReportSummaryDTOs)
                {
                    QCReportSummary item;

                    if (dto.QCReportSummaryID < 0)
                    {
                        item = new QCReportSummary();

                        dbItem.QCReportSummary.Add(item);
                    }
                    else
                    {
                        item = dbItem.QCReportSummary.FirstOrDefault(s => s.QCReportSummaryID == dto.QCReportSummaryID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.QCReportSummaryDTO, QCReportSummary>(dto, item);
                }
            }

            //QCReportDetail
            if (dtoItem.QCReportDetailDTOs != null)
            {
                foreach (QCReportDetail item in dbItem.QCReportDetail.ToList())
                {
                    if (!dtoItem.QCReportDetailDTOs.Select(s => s.QCReportDetailID).Contains(item.QCReportDetailID))
                        dbItem.QCReportDetail.Remove(item);
                }

                foreach (DTO.QCReportDetailDTO dto in dtoItem.QCReportDetailDTOs)
                {
                    QCReportDetail item;

                    if (dto.QCReportDetailID < 0)
                    {
                        item = new QCReportDetail();

                        dbItem.QCReportDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.QCReportDetail.FirstOrDefault(s => s.QCReportDetailID == dto.QCReportDetailID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.QCReportDetailDTO, QCReportDetail>(dto, item);
                }
            }

            //QCReportDefect
            if (dtoItem.QCReportDefectDTOs != null)
            {
                foreach (QCReportDefect item in dbItem.QCReportDefect.ToList())
                {
                    if (!dtoItem.QCReportDefectDTOs.Select(s => s.QCReportDefectID).Contains(item.QCReportDefectID))
                        dbItem.QCReportDefect.Remove(item);
                }

                foreach (DTO.QCReportDefectDTO dto in dtoItem.QCReportDefectDTOs)
                {
                    QCReportDefect item;

                    if (dto.QCReportDefectID < 0)
                    {
                        item = new QCReportDefect();

                        dbItem.QCReportDefect.Add(item);
                    }
                    else
                    {
                        item = dbItem.QCReportDefect.FirstOrDefault(s => s.QCReportDefectID == dto.QCReportDefectID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.QCReportDefectDTO, QCReportDefect>(dto, item);
                }
            }

            //QCReportTestEnvironment
            if (dtoItem.QCReportTestEnvironmentDTOs != null)
            {
                foreach (QCReportTestEnvironment item in dbItem.QCReportTestEnvironment.ToList())
                {
                    if (!dtoItem.QCReportTestEnvironmentDTOs.Select(s => s.QCReportTestEnvironmentID).Contains(item.QCReportTestEnvironmentID))
                        dbItem.QCReportTestEnvironment.Remove(item);
                }

                foreach (DTO.QCReportTestEnvironmentDTO dto in dtoItem.QCReportTestEnvironmentDTOs)
                {
                    QCReportTestEnvironment item;

                    if (dto.QCReportTestEnvironmentID < 0)
                    {
                        item = new QCReportTestEnvironment();

                        dbItem.QCReportTestEnvironment.Add(item);
                    }
                    else
                    {
                        item = dbItem.QCReportTestEnvironment.FirstOrDefault(s => s.QCReportTestEnvironmentID == dto.QCReportTestEnvironmentID);
                    }

                    if (item != null)
                        AutoMapper.Mapper.Map<DTO.QCReportTestEnvironmentDTO, QCReportTestEnvironment>(dto, item);
                }
            }

            //QCReportFactoryOrderDetail
            if (dtoItem.QCReportFactoryOrderDetailDTOs != null)
            {
                foreach (var sItem in dtoItem.QCReportFactoryOrderDetailDTOs.ToList())
                {
                    if (sItem.IsSelected == false)
                        dtoItem.QCReportFactoryOrderDetailDTOs.Remove(sItem);
                }

                foreach (QCReportFactoryOrderDetail item in dbItem.QCReportFactoryOrderDetail.ToList())
                {
                    if (!dtoItem.QCReportFactoryOrderDetailDTOs.Select(s => s.QCReportFactoryOrderDetailID).Contains(item.QCReportFactoryOrderDetailID))
                        dbItem.QCReportFactoryOrderDetail.Remove(item);
                }

                foreach (DTO.QCReportFactoryOrderDetailDTO dto in dtoItem.QCReportFactoryOrderDetailDTOs)
                {
                    QCReportFactoryOrderDetail item;

                    if (dto.QCReportFactoryOrderDetailID < 0)
                    {
                        item = new QCReportFactoryOrderDetail();

                        dbItem.QCReportFactoryOrderDetail.Add(item);
                    }
                    else
                    {
                        item = dbItem.QCReportFactoryOrderDetail.FirstOrDefault(s => s.QCReportFactoryOrderDetailID == dto.QCReportFactoryOrderDetailID);
                    }

                    if (item != null)
                    {
                        //select is save
                        if(dto.IsSelected == true)
                        {
                            AutoMapper.Mapper.Map<DTO.QCReportFactoryOrderDetailDTO, QCReportFactoryOrderDetail>(dto, item);
                        }
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.QCReportDTO, QCReport>(dtoItem, dbItem);
            dbItem.InspectedDate = dtoItem.InspectedDate.ConvertStringToDateTime();
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }
    }
}
