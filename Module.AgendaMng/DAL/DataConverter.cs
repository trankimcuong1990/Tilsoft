using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.AgendaMng.DAL
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
                AutoMapper.Mapper.CreateMap<AgendaMng_AppointmentSearchResult_View, DTO.AppointmentSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgendaMng_Appointment_View, DTO.AppointmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.RemindDate, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.AppointmentAttachedFileDTOs, o => o.MapFrom(s => s.AgendaMng_AppointmentAttachedFile_View))
                    .ForMember(d => d.agendaMngUsers, o => o.MapFrom(s => s.AgendaMng_AgendaMngUser_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_Sale_View, DTO.Sale>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgendaMng_AgendaMngUser_View, DTO.AgendaMngUser>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgendaMng_AppointmentAttachedFile_View, DTO.AppointmentAttachedFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.FileLocation) ? FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation : "")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgendaMng_ClientSearchResult_View, DTO.ClientSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.AppointmentDTO, Appointment>()
                    .ForMember(d => d.AppointmentID, o => o.Ignore())
                    .ForMember(d => d.AppointmentAttachedFile, o => o.Ignore())
                    .ForMember(d => d.AgendaMngUser, o => o.Ignore())
                    .ForMember(d => d.UserID, o => o.Ignore())
                    .ForMember(d => d.StartTime, o => o.Ignore())
                    .ForMember(d => d.EndTime, o => o.Ignore())
                    .ForMember(d => d.RemindTime, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AppointmentAttachedFileDTO, AppointmentAttachedFile>()
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AgendaMngUser, AgendaMngUser>()
                   .ForMember(d => d.ManagerUserID, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgendaMng_Employee_View, DTO.EmployeeDepartmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.AppointmentSearchResultDTO> DB2DTO_AppointmentSearchResultDTOList(List<AgendaMng_AppointmentSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AgendaMng_AppointmentSearchResult_View>, List<DTO.AppointmentSearchResultDTO>>(dbItems);
        }

        public DTO.AppointmentDTO DB2DTO_AppointmentDTO(AgendaMng_Appointment_View dbItem)
        {
            return AutoMapper.Mapper.Map<AgendaMng_Appointment_View, DTO.AppointmentDTO>(dbItem);
        }

        public List<DTO.ClientSearchResult> DB2DTO_ClientSearchResultList(List<AgendaMng_ClientSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<AgendaMng_ClientSearchResult_View>, List<DTO.ClientSearchResult>>(dbItem);
        }

        public List<DTO.Sale> DB2DTO_SaleList(List<SupportMng_Sale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_Sale_View>, List<DTO.Sale>>(dbItems);
        }
        public List<DTO.EmployeeDepartmentDTO> DB2DTO_EmployeeDepartment(List<AgendaMng_Employee_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<AgendaMng_Employee_View>, List<DTO.EmployeeDepartmentDTO>>(dbitems);
        }
        public void DTO2DB(DTO.AppointmentDTO dtoItem, ref Appointment dbItem, string TmpFile, int userId)
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
            foreach (AppointmentAttachedFile dbFile in dbItem.AppointmentAttachedFile.ToArray())
            {
                if (!dtoItem.AppointmentAttachedFileDTOs.Select(o => o.AppointmentAttachedFileID).Contains(dbFile.AppointmentAttachedFileID))
                {
                    if (!string.IsNullOrEmpty(dbFile.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbFile.FileUD);
                    }
                    dbItem.AppointmentAttachedFile.Remove(dbFile);
                }
            }
            foreach (DTO.AppointmentAttachedFileDTO dtoFile in dtoItem.AppointmentAttachedFileDTOs)
            {
                AppointmentAttachedFile dbFile;
                if (dtoFile.AppointmentAttachedFileID <= 0)
                {
                    dbFile = new AppointmentAttachedFile();
                    dbItem.AppointmentAttachedFile.Add(dbFile);
                }
                else
                {
                    dbFile = dbItem.AppointmentAttachedFile.FirstOrDefault(o => o.AppointmentAttachedFileID == dtoFile.AppointmentAttachedFileID);
                }

                if (dbFile != null)
                {
                    // change or add images
                    if (dtoFile.HasChanged)
                    {
                        dbFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }

                    AutoMapper.Mapper.Map<DTO.AppointmentAttachedFileDTO, AppointmentAttachedFile>(dtoFile, dbFile);

                    // updated by, updated date.
                    if (dtoFile.HasChanged)
                    {
                        dbFile.UpdatedBy = userId;
                        dbFile.UpdatedDate = DateTime.Now;
                    }
                }
            }
            if (dtoItem.agendaMngUsers != null)
            {
                foreach (var item in dbItem.AgendaMngUser.ToArray())
                {
                    if (!dtoItem.agendaMngUsers.Select(o => o.ManagerUserID).Contains(item.ManagerUserID))
                    {
                        dbItem.AgendaMngUser.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.agendaMngUsers)
                {
                    AgendaMngUser dbAgendaMngUser;
                    if (item.ManagerUserID <= 0)
                    {
                        dbAgendaMngUser = new AgendaMngUser();
                        dbItem.AgendaMngUser.Add(dbAgendaMngUser);
                    }
                    else
                    {
                        dbAgendaMngUser = dbItem.AgendaMngUser.FirstOrDefault(o => o.ManagerUserID == item.ManagerUserID);
                    }
                    if (dbAgendaMngUser != null)
                    {
                        AutoMapper.Mapper.Map<DTO.AgendaMngUser, AgendaMngUser>(item, dbAgendaMngUser);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.AppointmentDTO, Appointment>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
