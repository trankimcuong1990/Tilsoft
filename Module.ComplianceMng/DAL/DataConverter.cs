using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using Module.ComplianceMng.DTO;

namespace Module.ComplianceMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ComplianceMng_ComplianceSearchResult_View, DTO.ComplianceSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ExpiredDate, o => o.ResolveUsing(s => s.ExpiredDate.HasValue ? s.ExpiredDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RenewDate, o => o.ResolveUsing(s => s.RenewDate.HasValue ? s.RenewDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FictiveDate, o => o.ResolveUsing(s => s.FictiveDate.HasValue ? s.FictiveDate.Value.ToString("dd/MM/yyyy") : ""))                   
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_CalendarResult_View, DTO.CalendarSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))               
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_Factory_View, DTO.FactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ComplianceMng_Employee_View, DTO.EmployeeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_Client_View, DTO.ClientDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_ComplianceCertificateType_View, DTO.ComplianceCertificateTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_AuditStatus_View, DTO.AuditStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_Compliance_View, DTO.ComplianceProcessDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ComplianceAttachedFileDTOs, o => o.MapFrom(s => s.ComplianceMng_ComplianceAttachedFile_View))
                    .ForMember(d => d.CompliancePICDTOs, o => o.MapFrom(s => s.ComplianceMng_CompliancePIC_View))
                    .ForMember(d => d.ExpiredDate, o => o.ResolveUsing(s => s.ExpiredDate.HasValue ? s.ExpiredDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RenewDate, o => o.ResolveUsing(s => s.RenewDate.HasValue ? s.RenewDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FictiveDate, o => o.ResolveUsing(s => s.FictiveDate.HasValue ? s.FictiveDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_ComplianceAttachedFile_View, ComplianceAttachedFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileUDUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileUDUrl) ? s.FileUDUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceMng_CompliancePIC_View, CompliancePICDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileUDUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileUDUrl) ? s.FileUDUrl : "no-image.jpg")))
                    .ForMember(d => d.PreparationTimeFrom, o => o.ResolveUsing(s => s.PreparationTimeFrom.HasValue ? s.PreparationTimeFrom.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.PreparationTimeTo, o => o.ResolveUsing(s => s.PreparationTimeTo.HasValue ? s.PreparationTimeTo.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceAttachedFile, DTO.ComplianceAttachedFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ComplianceAttachedFileDTO, ComplianceAttachedFile>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CompliancePIC, DTO.CompliancePICDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<CompliancePICDTO, CompliancePIC>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PreparationTimeFrom, o => o.ResolveUsing(s => s.PreparationTimeFrom.ConvertStringToDateTime()))
                    .ForMember(d => d.PreparationTimeTo, o => o.ResolveUsing(s => s.PreparationTimeTo.ConvertStringToDateTime()))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ComplianceProcessDTO, ComplianceProcess>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ExpiredDate, o => o.ResolveUsing(s => s.ExpiredDate.ConvertStringToDateTime()))
                    .ForMember(d => d.RenewDate, o => o.ResolveUsing(s => s.RenewDate.ConvertStringToDateTime()))
                    .ForMember(d => d.FictiveDate, o => o.ResolveUsing(s => s.FictiveDate.ConvertStringToDateTime()))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.ConvertStringToDateTime()))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ComplianceProcess, ComplianceProcessDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<CalendarSearchDTO> DB2DTO_Calendar(List<ComplianceMng_CalendarResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_CalendarResult_View>, List<DTO.CalendarSearchDTO>>(dbItems);
        }
        public void DTO2DB_ComplianceProcess(ComplianceProcessDTO dtoItem, ref ComplianceProcess dbItem, int userId)
        {
            /*
             * MAP & CHECK  ATTACHED FILE
             */
            List<ComplianceAttachedFile> toBeDeletedItems = new List<ComplianceAttachedFile>();
            if (dtoItem.ComplianceAttachedFileDTOs != null)
            {
                //CHECK
                foreach (ComplianceAttachedFile dbSample in dbItem.ComplianceAttachedFile.Where(o => !dtoItem.ComplianceAttachedFileDTOs.Select(s => s.ComplianceAttachedFileID).Contains(o.ComplianceAttachedFileID)).ToArray())
                {
                    dbItem.ComplianceAttachedFile.Remove(dbSample);
                }
                //MAP
                foreach (ComplianceAttachedFileDTO dtoSample in dtoItem.ComplianceAttachedFileDTOs)
                {
                    ComplianceAttachedFile dbSample = null;
                    if (dtoSample.ComplianceAttachedFileID <= 0)
                    {
                        dbSample = new ComplianceAttachedFile();
                        dbItem.ComplianceAttachedFile.Add(dbSample);
                    }
                    else
                    {
                        dbSample = dbItem.ComplianceAttachedFile.FirstOrDefault(o => o.ComplianceAttachedFileID == dtoSample.ComplianceAttachedFileID);
                        if (dbSample == null)
                        {
                            throw new Exception("Sample item not found!");
                        }
                    }
                    AutoMapper.Mapper.Map<ComplianceAttachedFileDTO, ComplianceAttachedFile>(dtoSample, dbSample);
                }
            }

            /*
            * MAP & CHECK  PIC
            */
            List<CompliancePIC> toBeDeletedCompliancePICItems = new List<CompliancePIC>();
            if (dtoItem.CompliancePICDTOs != null)
            {
                //CHECK
                foreach (CompliancePIC dbSample in dbItem.CompliancePIC.Where(o => !dtoItem.CompliancePICDTOs.Select(s => s.CompliancePICID).Contains(o.CompliancePICID)).ToArray())
                {
                    dbItem.CompliancePIC.Remove(dbSample);
                }
                //MAP
                foreach (CompliancePICDTO dtoSample in dtoItem.CompliancePICDTOs)
                {
                    CompliancePIC dbSample = null;
                    if (dtoSample.CompliancePICID <= 0)
                    {
                        dbSample = new CompliancePIC();
                        dbItem.CompliancePIC.Add(dbSample);
                    }
                    else
                    {
                        dbSample = dbItem.CompliancePIC.FirstOrDefault(o => o.CompliancePICID == dtoSample.CompliancePICID);
                        if (dbSample == null)
                        {
                            throw new Exception("Sample item not found!");
                        }
                    }
                    AutoMapper.Mapper.Map<CompliancePICDTO, CompliancePIC>(dtoSample, dbSample);
                }
            }


            /*
             * SETUP FORMATED FIELD
             */
            //dbItem.ExpiredDate = Library.Helper.ConvertStringToDateTime(dtoItem.ExpiredDate, new System.Globalization.CultureInfo("vi-VN"));
            //dbItem.RenewDate = Library.Helper.ConvertStringToDateTime(dtoItem.RenewDate, new System.Globalization.CultureInfo("vi-VN"));
            //dbItem.FictiveDate = Library.Helper.ConvertStringToDateTime(dtoItem.FictiveDate, new System.Globalization.CultureInfo("vi-VN"));

            Mapper.Map(dtoItem, dbItem);
        }
        public ComplianceProcessDTO DB2DTO_ComplianceProcessDTO(ComplianceMng_Compliance_View dbItems)
        {
            return AutoMapper.Mapper.Map<ComplianceMng_Compliance_View, ComplianceProcessDTO>(dbItems);
        }
        public List<DTO.ComplianceSearchDTO> DB2DTO_Search(List<ComplianceMng_ComplianceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_ComplianceSearchResult_View>, List<DTO.ComplianceSearchDTO>>(dbItems);
        }
        public List<EmployeeDTO> DB2DTO_Employee(List<ComplianceMng_Employee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_Employee_View>, List<EmployeeDTO>>(dbItems);
        }
        public List<DTO.FactoryDTO> DB2DTO_Factory(List<ComplianceMng_Factory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_Factory_View>, List<DTO.FactoryDTO>>(dbItems);
        }
        public List<DTO.ClientDTO> DB2DTO_Client(List<ComplianceMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_Client_View>, List<DTO.ClientDTO>>(dbItems);
        }
        public List<DTO.ComplianceCertificateTypeDTO> DB2DTO_ComplianceCertificateType(List<ComplianceMng_ComplianceCertificateType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_ComplianceCertificateType_View>, List<DTO.ComplianceCertificateTypeDTO>>(dbItems);
        }        
        public List<DTO.AuditStatusDTO> DB2DTO_AuditStatus(List<ComplianceMng_AuditStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ComplianceMng_AuditStatus_View>, List<DTO.AuditStatusDTO>>(dbItems);
        }


        //public List<DTO.SampleOrderSearchResult> DB2DTO_SampleOrderSearchResult(List<Sample2Mng_SampleOrderSearchResult_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<Sample2Mng_SampleOrderSearchResult_View>, List<DTO.SampleOrderSearchResult>>(dbItems);
        //}

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
