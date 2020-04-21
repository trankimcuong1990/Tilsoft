using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.SCMAgendaMng.DAL
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
                AutoMapper.Mapper.CreateMap<SCMAgendaMng_SCMAppointmentSearchResult_View, DTO.SCMAppointmentSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_SCMAppointment_View, DTO.SCMAppointmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.RemindDate, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SCMAppointmentAttachedFileDTOs, o => o.MapFrom(s => s.SCMAgendaMng_SCMAppointmentAttachedFile_View))
                    .ForMember(d => d.SCMAppointmentUserDTOs, o => o.MapFrom(s => s.SCMAgendaMng_SCMAppointmentUser_View))
                    .ForMember(d => d.SCMInspectionDTOs, o => o.MapFrom(s => s.SCMAgendaMng_SCMInspection_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_Sale_View, DTO.Sale>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_SCMAppointmentUser_View, DTO.SCMAppointmentUserDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_SCMAppointmentAttachedFile_View, DTO.SCMAppointmentAttachedFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.FileLocation) ? FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation : "")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_ClientSearchResult_View, DTO.ClientSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_SCMInspection_View, DTO.SCMInspectionDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InspectedDate, o => o.ResolveUsing(s => s.InspectedDate.HasValue ? s.InspectedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ReportFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ReportFileLocation) ? s.ReportFileLocation : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_QCReport_View, DTO.QCReportDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.InspectedDate, o => o.ResolveUsing(s => s.InspectedDate.HasValue ? s.InspectedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.SCMAppointmentDTO, SCMAppointment>()
                    .ForMember(d => d.SCMAppointmentID, o => o.Ignore())
                    .ForMember(d => d.SCMAppointmentAttachedFile, o => o.Ignore())
                    .ForMember(d => d.SCMAppointmentUser, o => o.Ignore())
                    .ForMember(d => d.SCMInspection, o => o.Ignore())
                    .ForMember(d => d.UserID, o => o.Ignore())
                    .ForMember(d => d.StartTime, o => o.Ignore())
                    .ForMember(d => d.EndTime, o => o.Ignore())
                    .ForMember(d => d.RemindTime, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SCMAppointmentAttachedFileDTO, SCMAppointmentAttachedFile>()
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SCMInspectionDTO, SCMInspection>()
                    .ForMember(d => d.SCMInspectionID, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SCMAppointmentUserDTO, SCMAppointmentUser>()
                   .ForMember(d => d.SCMAppointmentUserID, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SCMAgendaMng_Employee_View, DTO.EmployeeDepartmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SCMAppointmentSearchResultDTO> DB2DTO_SCMAppointmentSearchResultDTOList(List<SCMAgendaMng_SCMAppointmentSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SCMAgendaMng_SCMAppointmentSearchResult_View>, List<DTO.SCMAppointmentSearchResultDTO>>(dbItems);
        }

        public DTO.SCMAppointmentDTO DB2DTO_SCMAppointmentDTO(SCMAgendaMng_SCMAppointment_View dbItem)
        {
            return AutoMapper.Mapper.Map<SCMAgendaMng_SCMAppointment_View, DTO.SCMAppointmentDTO>(dbItem);
        }

        public List<DTO.ClientSearchResult> DB2DTO_ClientSearchResultList(List<SCMAgendaMng_ClientSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SCMAgendaMng_ClientSearchResult_View>, List<DTO.ClientSearchResult>>(dbItem);
        }

        public List<DTO.Sale> DB2DTO_SaleList(List<SupportMng_Sale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Sale_View>, List<DTO.Sale>>(dbItems);
        }
        public List<DTO.EmployeeDepartmentDTO> DB2DTO_EmployeeDepartment(List<SCMAgendaMng_Employee_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<SCMAgendaMng_Employee_View>, List<DTO.EmployeeDepartmentDTO>>(dbitems);
        }
        public void DTO2DB(DTO.SCMAppointmentDTO dtoItem, ref SCMAppointment dbItem, string TmpFile, int userId)
        {
            if (!string.IsNullOrEmpty(dtoItem.StartDate) && !string.IsNullOrEmpty(dtoItem.StartTime))
            {
                if (DateTime.TryParse(dtoItem.StartDate + " " + dtoItem.StartTime, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.StartTime = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.EndDate) && !string.IsNullOrEmpty(dtoItem.EndTime))
            {
                if (DateTime.TryParse(dtoItem.EndDate + " " + dtoItem.EndTime, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.EndTime = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.RemindDate) && !string.IsNullOrEmpty(dtoItem.RemindTime))
            {
                if (DateTime.TryParse(dtoItem.RemindDate + " " + dtoItem.RemindTime, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.RemindTime = tmpDate;
                }
            }

            // attached files
            foreach (SCMAppointmentAttachedFile dbFile in dbItem.SCMAppointmentAttachedFile.ToArray())
            {
                if (!dtoItem.SCMAppointmentAttachedFileDTOs.Select(o => o.SCMAppointmentAttachedFileID).Contains(dbFile.SCMAppointmentAttachedFileID))
                {
                    if (!string.IsNullOrEmpty(dbFile.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbFile.FileUD);
                    }
                    dbItem.SCMAppointmentAttachedFile.Remove(dbFile);
                }
            }

            foreach (DTO.SCMAppointmentAttachedFileDTO dtoFile in dtoItem.SCMAppointmentAttachedFileDTOs)
            {
                SCMAppointmentAttachedFile dbFile;
                if (dtoFile.SCMAppointmentAttachedFileID <= 0)
                {
                    dbFile = new SCMAppointmentAttachedFile();
                    dbItem.SCMAppointmentAttachedFile.Add(dbFile);
                }
                else
                {
                    dbFile = dbItem.SCMAppointmentAttachedFile.FirstOrDefault(o => o.SCMAppointmentAttachedFileID == dtoFile.SCMAppointmentAttachedFileID);
                }

                if (dbFile != null)
                {
                    // change or add images
                    if (dtoFile.HasChanged)
                    {
                        dbFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }

                    AutoMapper.Mapper.Map<DTO.SCMAppointmentAttachedFileDTO, SCMAppointmentAttachedFile>(dtoFile, dbFile);

                    // updated by, updated date.
                    if (dtoFile.HasChanged)
                    {
                        dbFile.UpdatedBy = userId;
                        dbFile.UpdatedDate = DateTime.Now;
                    }
                }
            }
            if (dtoItem.SCMAppointmentUserDTOs != null)
            {
                foreach (var item in dbItem.SCMAppointmentUser.ToArray())
                {
                    if (!dtoItem.SCMAppointmentUserDTOs.Select(o => o.SCMAppointmentUserID).Contains(item.SCMAppointmentUserID))
                    {
                        dbItem.SCMAppointmentUser.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.SCMAppointmentUserDTOs)
                {
                    SCMAppointmentUser dbSCMAppointmentUser;
                    if (item.SCMAppointmentUserID <= 0)
                    {
                        dbSCMAppointmentUser = new SCMAppointmentUser();
                        dbItem.SCMAppointmentUser.Add(dbSCMAppointmentUser);
                    }
                    else
                    {
                        dbSCMAppointmentUser = dbItem.SCMAppointmentUser.FirstOrDefault(o => o.SCMAppointmentUserID == item.SCMAppointmentUserID);
                    }
                    if (dbSCMAppointmentUser != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SCMAppointmentUserDTO, SCMAppointmentUser>(item, dbSCMAppointmentUser);
                    }
                }
            }

            if (dtoItem.SCMInspectionDTOs != null)
            {
                // Inspection report
                foreach (SCMInspection dbInspection in dbItem.SCMInspection.ToArray())
                {
                    if (!dtoItem.SCMInspectionDTOs.Select(o => o.SCMInspectionID).Contains(dbInspection.SCMInspectionID))
                    {
                        dbItem.SCMInspection.Remove(dbInspection);
                    }
                }
                //map child row
                foreach (var item in dtoItem.SCMInspectionDTOs)
                {
                    SCMInspection dbSCMInspection;
                    if (item.SCMInspectionID <= 0)
                    {
                        dbSCMInspection = new SCMInspection();
                        dbItem.SCMInspection.Add(dbSCMInspection);
                    }
                    else
                    {
                        dbSCMInspection = dbItem.SCMInspection.FirstOrDefault(o => o.SCMInspectionID == item.SCMInspectionID);
                    }
                    if (dbSCMInspection != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SCMInspectionDTO, SCMInspection>(item, dbSCMInspection);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.SCMAppointmentDTO, SCMAppointment>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
