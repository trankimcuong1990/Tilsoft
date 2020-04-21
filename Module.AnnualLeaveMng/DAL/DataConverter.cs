using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Module.AnnualLeaveMng.DTO;

namespace Module.AnnualLeaveMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_AnnualLeaveTrackingDetailSearchResult_View, AnnualLeaveSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FromDate, o => o.ResolveUsing(s => s.FromDate.HasValue ? s.FromDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ToDate, o => o.ResolveUsing(s => s.ToDate.HasValue ? s.ToDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_AnnualLeaveTracking_View, AnnualLeaveTrackingDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AnnualLeaveTrackingDetailDTOs, o => o.MapFrom(s => s.AnnualLeaveMng_AnnualLeaveTrackingDetail_View))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_AnnualLeaveTrackingDetail_View, AnnualLeaveTrackingDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FromDate, o => o.ResolveUsing(s => s.FromDate.HasValue ? s.FromDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FromTime, o => o.ResolveUsing(s => s.FromDate.HasValue ? s.FromDate.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.ToDate, o => o.ResolveUsing(s => s.ToDate.HasValue ? s.ToDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ToTime, o => o.ResolveUsing(s => s.ToDate.HasValue ? s.ToDate.Value.ToString("HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_Employee_View, EmployeeDTO>()
                    .IgnoreAllNonExisting()                 
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_AnnualLeaveStatus_View, AnnualLeaveStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_AnnualLeaveType_View, AnnualLeaveTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AnnualLeaveTrackingDTO, AnnualLeaveTracking>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AnnualLeaveTrackingID, o => o.Ignore())
                    .ForMember(d => d.AnnualLeaveTrackingDetail, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AnnualLeaveTrackingDetailDTO, AnnualLeaveTrackingDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.AnnualLeaveTrackingDetailID, o => o.Ignore())
                   .ForMember(d => d.FromDate, o => o.Ignore())
                   .ForMember(d => d.ToDate, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AnnualLeaveMng_Company_View, CompanyDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<CompanyDTO> DB2DTO_CompanyDTO(List<AnnualLeaveMng_Company_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AnnualLeaveMng_Company_View>, List<CompanyDTO>>(dbItems);
        }
        public List<AnnualLeaveSearchResultDTO> DB2DTO_AnnualLeaveSearchResult(List<AnnualLeaveMng_AnnualLeaveTrackingDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AnnualLeaveMng_AnnualLeaveTrackingDetailSearchResult_View>, List<AnnualLeaveSearchResultDTO>>(dbItems);
        }
        public AnnualLeaveTrackingDTO DB2DTO_AnnualLeaveTrackingDTO(AnnualLeaveMng_AnnualLeaveTracking_View dbItems)
        {
            return AutoMapper.Mapper.Map<AnnualLeaveMng_AnnualLeaveTracking_View, AnnualLeaveTrackingDTO>(dbItems);
        }
        public List<EmployeeDTO> DB2DTO_EmployeeDTO(List<AnnualLeaveMng_Employee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AnnualLeaveMng_Employee_View>, List<EmployeeDTO>>(dbItems);
        }
        public List<AnnualLeaveStatusDTO> DB2DTO_AnnualLeaveStatusDTO(List<AnnualLeaveMng_AnnualLeaveStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AnnualLeaveMng_AnnualLeaveStatus_View>, List<AnnualLeaveStatusDTO>>(dbItems);
        }
        public List<AnnualLeaveTypeDTO> DB2DTO_AnnualLeaveTypeDTO(List<AnnualLeaveMng_AnnualLeaveType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AnnualLeaveMng_AnnualLeaveType_View>, List<AnnualLeaveTypeDTO>>(dbItems);
        }       
        public void DTO2DB_AnnualLeaveTracking(int userID, DTO.AnnualLeaveTrackingDTO dtoItem, ref AnnualLeaveTracking dbItem)
        {
            // Mapping workcenter.
            AutoMapper.Mapper.Map<DTO.AnnualLeaveTrackingDTO, DAL.AnnualLeaveTracking>(dtoItem, dbItem);
            dbItem.StatusUpdatedDate = dtoItem.StatusUpdatedDate.ConvertStringToDateTime();
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();

            // Mapping workcenter details.
            if (dtoItem.AnnualLeaveTrackingDetailDTOs != null)
            {
                foreach (AnnualLeaveTrackingDetail dbWorkCenterDetail in dbItem.AnnualLeaveTrackingDetail.ToList())
                {
                    if (!dtoItem.AnnualLeaveTrackingDetailDTOs.Select(s => s.AnnualLeaveTrackingDetailID).Contains(dbWorkCenterDetail.AnnualLeaveTrackingDetailID))
                    {
                        dbItem.AnnualLeaveTrackingDetail.Remove(dbWorkCenterDetail);
                    }
                }

                foreach (DTO.AnnualLeaveTrackingDetailDTO dtoWorkCenterDetail in dtoItem.AnnualLeaveTrackingDetailDTOs.ToList())
                {
                    AnnualLeaveTrackingDetail dbWorkCenterDetail;

                    if (dtoWorkCenterDetail.AnnualLeaveTrackingDetailID < 0)
                    {
                        dbWorkCenterDetail = new AnnualLeaveTrackingDetail();
                        dbItem.AnnualLeaveTrackingDetail.Add(dbWorkCenterDetail);
                    }
                    else
                    {
                        dbWorkCenterDetail = dbItem.AnnualLeaveTrackingDetail.FirstOrDefault(o => o.AnnualLeaveTrackingDetailID == dtoWorkCenterDetail.AnnualLeaveTrackingDetailID);
                    }

                    if (dbWorkCenterDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.AnnualLeaveTrackingDetailDTO, AnnualLeaveTrackingDetail>(dtoWorkCenterDetail, dbWorkCenterDetail);
                        //dbWorkCenterDetail.FromDate = dtoWorkCenterDetail.FromDate.ConvertStringToDateTime();
                        //dbWorkCenterDetail.ToDate = dtoWorkCenterDetail.ToDate.ConvertStringToDateTime();
                        if (!string.IsNullOrEmpty(dtoWorkCenterDetail.ToDate) && !string.IsNullOrEmpty(dtoWorkCenterDetail.ToTime))
                        {
                            DateTime tmpDate;
                            if (DateTime.TryParse(dtoWorkCenterDetail.ToDate + " " + dtoWorkCenterDetail.ToTime, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                            {
                                dbWorkCenterDetail.ToDate = tmpDate;
                            }
                        }
                        if (!string.IsNullOrEmpty(dtoWorkCenterDetail.FromDate) && !string.IsNullOrEmpty(dtoWorkCenterDetail.FromTime))
                        {
                            DateTime tmpDate;
                            if (DateTime.TryParse(dtoWorkCenterDetail.FromDate + " " + dtoWorkCenterDetail.FromTime, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                            {
                                dbWorkCenterDetail.FromDate = tmpDate;
                            }
                        }

                        //dbWorkCenterDetail.UpdatedBy = userID;
                        //dbWorkCenterDetail.UpdatedDate = DateTime.Now;

                        //if (dtoWorkCenterDetail.WorkCenterDetailID <= 0)
                        //{
                        //    dbWorkCenterDetail.CreatedBy = userID;
                        //    dbWorkCenterDetail.CreatedDate = DateTime.Now;
                        //}
                    }
                }
            }
        }

        //public DTO.SampleTechnicalDrawing DB2DTO_SampleTechnicalDrawing(Sample2Mng_SampleTechnicalDrawing_View dbItem)
        //{
        //    return AutoMapper.Mapper.Map<Sample2Mng_SampleTechnicalDrawing_View, DTO.SampleTechnicalDrawing>(dbItem);
        //}

        //public void DTO2DB_SampleRemark(DTO.SampleRemark dtoItem, ref SampleRemark dbItem, int userId)
        //{
        //    string TmpFile = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";

        //    // remark
        //    AutoMapper.Mapper.Map<DTO.SampleRemark, SampleRemark>(dtoItem, dbItem);

        //    // remark image
        //    foreach (SampleRemarkImage dbImage in dbItem.SampleRemarkImage.ToArray())
        //    {
        //        if (!dtoItem.SampleRemarkImages.Select(o => o.SampleRemarkImageID).Contains(dbImage.SampleRemarkImageID))
        //        {
        //            if (!string.IsNullOrEmpty(dbImage.FileUD))
        //            {
        //                // remove image file
        //                fwFactory.RemoveImageFile(dbImage.FileUD);
        //            }
        //            dbItem.SampleRemarkImage.Remove(dbImage);
        //        }
        //    }
        //    foreach (DTO.SampleRemarkImage dtoImage in dtoItem.SampleRemarkImages)
        //    {
        //        SampleRemarkImage dbImage;
        //        if (dtoImage.SampleRemarkImageID <= 0)
        //        {
        //            dbImage = new SampleRemarkImage();
        //            dbItem.SampleRemarkImage.Add(dbImage);
        //        }
        //        else
        //        {
        //            dbImage = dbItem.SampleRemarkImage.FirstOrDefault(o => o.SampleRemarkImageID == dtoImage.SampleRemarkImageID);
        //        }

        //        if (dbImage != null)
        //        {
        //            // change or add images
        //            if (dtoImage.HasChanged)
        //            {
        //                dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD, dtoImage.FriendlyName);
        //            }
        //            AutoMapper.Mapper.Map<DTO.SampleRemarkImage, SampleRemarkImage>(dtoImage, dbImage);
        //        }
        //    }
        //}
    }
}
