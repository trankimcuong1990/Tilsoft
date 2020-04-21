using AutoMapper;
using Library;
using Module.PurchasingCalendarMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.PurchasingCalendarMng.DAL
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
                Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarAppointmentSearchResult_View,PurchasingCalendarAppointmentSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarAppointment_View, PurchasingCalendarAppointmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.RemindDate, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PurchasingCalendarAppointmentAttachedFileDTOs, o => o.MapFrom(s => s.PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View))
                    .ForMember(d => d.PurchasingCalendarUserDTOs, o => o.MapFrom(s => s.PurchasingCalendarMng_PurchasingCalendarUser_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarMng_Sale_View, SaleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarUser_View, PurchasingCalendarUserDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View, PurchasingCalendarAppointmentAttachedFileDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (!string.IsNullOrEmpty(s.FileLocation) ? FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation : "")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarMng_FactoryRawMaterial_View, FactoryRawMaterialSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarAppointmentDTO, PurchasingCalendarAppointment>()
                    .ForMember(d => d.PurchasingCalendarAppointmentID, o => o.Ignore())
                    .ForMember(d => d.PurchasingCalendarAppointmentAttachedFile, o => o.Ignore())
                    .ForMember(d => d.PurchasingCalendarUser, o => o.Ignore())
                    .ForMember(d => d.UserID, o => o.Ignore())
                    .ForMember(d => d.StartTime, o => o.Ignore())
                    .ForMember(d => d.EndTime, o => o.Ignore())
                    .ForMember(d => d.RemindTime, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarAppointmentAttachedFileDTO, PurchasingCalendarAppointmentAttachedFile>()
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarUserDTO, PurchasingCalendarUser>()
                   .ForMember(d => d.PurchasingCalendarUserID, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<PurchasingCalendarMng_Employee_View, EmployeeDepartmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<PurchasingCalendarAppointmentSearchResultDTO> DB2DTO_AppointmentSearchResultDTOList(List<PurchasingCalendarMng_PurchasingCalendarAppointmentSearchResult_View> dbItems)
        {
            return Mapper.Map<List<PurchasingCalendarMng_PurchasingCalendarAppointmentSearchResult_View>, List<PurchasingCalendarAppointmentSearchResultDTO>>(dbItems);
        }
        public PurchasingCalendarAppointmentDTO DB2DTO_AppointmentDTO(PurchasingCalendarMng_PurchasingCalendarAppointment_View dbItem)
        {
            return Mapper.Map<PurchasingCalendarMng_PurchasingCalendarAppointment_View, PurchasingCalendarAppointmentDTO>(dbItem);
        }
        public List<FactoryRawMaterialSearchResultDTO> DB2DTO_ClientSearchResultList(List<PurchasingCalendarMng_FactoryRawMaterial_View> dbItem)
        {
            return Mapper.Map<List<PurchasingCalendarMng_FactoryRawMaterial_View>, List<FactoryRawMaterialSearchResultDTO>>(dbItem);
        }
        public List<SaleDTO> DB2DTO_SaleList(List<PurchasingCalendarMng_Sale_View> dbItems)
        {
            return Mapper.Map<List<PurchasingCalendarMng_Sale_View>, List<SaleDTO>>(dbItems);
        }
        public List<EmployeeDepartmentDTO> DB2DTO_EmployeeDepartment(List<PurchasingCalendarMng_Employee_View> dbitems)
        {
            return Mapper.Map<List<PurchasingCalendarMng_Employee_View>, List<EmployeeDepartmentDTO>>(dbitems);
        }
        public void DTO2DB(PurchasingCalendarAppointmentDTO dtoItem, ref PurchasingCalendarAppointment dbItem, string TmpFile, int userId)
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
            foreach (PurchasingCalendarAppointmentAttachedFile dbFile in dbItem.PurchasingCalendarAppointmentAttachedFile.ToArray())
            {
                if (!dtoItem.PurchasingCalendarAppointmentAttachedFileDTOs.Select(o => o.PurchasingCalendarAppointmentAttachedFileID).Contains(dbFile.PurchasingCalendarAppointmentAttachedFileID))
                {
                    if (!string.IsNullOrEmpty(dbFile.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbFile.FileUD);
                    }
                    dbItem.PurchasingCalendarAppointmentAttachedFile.Remove(dbFile);
                }
            }
            foreach (PurchasingCalendarAppointmentAttachedFileDTO dtoFile in dtoItem.PurchasingCalendarAppointmentAttachedFileDTOs)
            {
                PurchasingCalendarAppointmentAttachedFile dbFile;
                if (dtoFile.PurchasingCalendarAppointmentAttachedFileID <= 0)
                {
                    dbFile = new PurchasingCalendarAppointmentAttachedFile();
                    dbItem.PurchasingCalendarAppointmentAttachedFile.Add(dbFile);
                }
                else
                {
                    dbFile = dbItem.PurchasingCalendarAppointmentAttachedFile.FirstOrDefault(o => o.PurchasingCalendarAppointmentAttachedFileID == dtoFile.PurchasingCalendarAppointmentAttachedFileID);
                }

                if (dbFile != null)
                {
                    // change or add images
                    if (dtoFile.HasChanged)
                    {
                        dbFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                    }

                    Mapper.Map<PurchasingCalendarAppointmentAttachedFileDTO, PurchasingCalendarAppointmentAttachedFile>(dtoFile, dbFile);

                    // updated by, updated date.
                    if (dtoFile.HasChanged)
                    {
                        dbFile.UpdatedBy = userId;
                        dbFile.UpdatedDate = DateTime.Now;
                    }
                }
            }
            if (dtoItem.PurchasingCalendarUserDTOs != null)
            {
                foreach (var item in dbItem.PurchasingCalendarUser.ToArray())
                {
                    if (!dtoItem.PurchasingCalendarUserDTOs.Select(o => o.PurchasingCalendarUserID).Contains(item.PurchasingCalendarUserID))
                    {
                        dbItem.PurchasingCalendarUser.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.PurchasingCalendarUserDTOs)
                {
                    PurchasingCalendarUser dbAgendaMngUser;
                    if (item.PurchasingCalendarUserID <= 0)
                    {
                        dbAgendaMngUser = new PurchasingCalendarUser();
                        dbItem.PurchasingCalendarUser.Add(dbAgendaMngUser);
                    }
                    else
                    {
                        dbAgendaMngUser = dbItem.PurchasingCalendarUser.FirstOrDefault(o => o.PurchasingCalendarUserID == item.PurchasingCalendarUserID);
                    }
                    if (dbAgendaMngUser != null)
                    {
                        Mapper.Map<PurchasingCalendarUserDTO, PurchasingCalendarUser>(item, dbAgendaMngUser);
                    }
                }
            }
            Mapper.Map<PurchasingCalendarAppointmentDTO, PurchasingCalendarAppointment>(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
        }
    }
}
