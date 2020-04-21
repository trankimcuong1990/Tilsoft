using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using System.Globalization;

namespace Module.EmployeeMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<EmployeeMng_EmployeeSearchResult_View, DTO.EmployeeSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PersonalPhoto_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.PersonalPhoto_DisplayURL) ? s.PersonalPhoto_DisplayURL : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateEnd, o => o.ResolveUsing(s => s.DateEnd.HasValue ? s.DateEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Employee_View, DTO.Employee>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.PersonalPhoto_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.PersonalPhoto_DisplayURL) ? s.PersonalPhoto_DisplayURL : "no-image.jpg")))
                    .ForMember(d => d.PersonalPhoto, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.PersonalPhoto) ? s.PersonalPhoto : "no-image.jpg")))
                    .ForMember(d => d.ResumeFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ResumeFileLocation) ? s.ResumeFileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ContractPeriod, o => o.ResolveUsing(s => s.ContractPeriod.HasValue ? s.ContractPeriod.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ContractStartDate, o => o.ResolveUsing(s => s.ContractStartDate.HasValue ? s.ContractStartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.AnnualLeaveSettings, o => o.MapFrom(s => s.EmployeeMng_AnnualLeaveSetting_View))
                    .ForMember(d => d.EmployeeFactorys, o => o.MapFrom(s => s.EmployeeMng_EmployeeFactory_View))
                    .ForMember(d => d.factoryAccesses, o => o.MapFrom(s => s.EmployeeMng_QAQCFactoryAccess_View))
                    .ForMember(d => d.ManagerNote, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_EmployeeFile_View, DTO.EmployeeFileDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<EmployeeMng_EmployeeContract_View, DTO.EmployeeContractDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<EmployeeMng_EmployeeFactory_View, DTO.EmployeeFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.Employee, Employee>()
                   .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.DateStart, o => o.Ignore())
                    .ForMember(d => d.DateEnd, o => o.Ignore())
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.PersonalPhoto, o => o.Ignore())
                    .ForMember(d => d.AnnualLeaveSetting, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.EmployeeFile, o => o.Ignore())
                    .ForMember(d => d.ManagerNote, o => o.Ignore())
                    .ForMember(d => d.ContractPeriod, o => o.Ignore())
                    .ForMember(d => d.ContractStartDate, o => o.Ignore())
                    .ForMember(d => d.EmployeeID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.EmployeeFileDTO, EmployeeFile>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.EmployeeFileID, o => o.Ignore())
                   .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.EmployeeContractDTO, EmployeeContract>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.EmployeeContractID, o => o.Ignore())
                   .ForMember(d => d.ValidFrom, o => o.Ignore())
                   .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<EmployeeMng_AnnualLeaveSetting_View, DTO.AnnualLeaveSetting>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_EmployeeFactory_View, DTO.EmployeeFactory>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.AnnualLeaveSetting, AnnualLeaveSetting>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.AnnualLeaveSettingID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.EmployeeFactory, EmployeeFactory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.EmployeeFactoryID, o => o.Ignore());

                //job level
                AutoMapper.Mapper.CreateMap<EmployeeMng_Director_View, DTO.Director>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_function_TilsoftAverageUsage_Result, DTO.TilsoftUsage>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_function_SearchUnSelectedUser_Result, DTO.UnSelectedUser>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // usage tracking
                AutoMapper.Mapper.CreateMap<EmployeeMng_UserActionDetailWeeklyDetail_View, DTO.UserDetailWeeklyDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_UserActionDetailWeeklyOverview_View, DTO.UserDetailWeeklyOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
           
                AutoMapper.Mapper.CreateMap<EmployeeMng_UserActionDetail_View, DTO.UserDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // 
                // overview
                //
                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_Employee_View, DTO.Overview.EmployeeDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DateStart, o => o.ResolveUsing(s => s.DateStart.HasValue ? s.DateStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.LicensedDate, o => o.ResolveUsing(s => s.LicensedDate.HasValue ? s.LicensedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EmployeeFactoryDTOs, o => o.MapFrom(s => s.EmployeeMng_Overview_EmployeeFactory_View))
                    .ForMember(d => d.ResumeFileFriendlyName, o => o.Ignore())
                    .ForMember(d => d.ResumeFileLocation, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_EmployeeFile_View, DTO.Overview.EmployeeFileDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_EmployeeContract_View, DTO.Overview.EmployeeContractDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_EmployeeFactory_View, DTO.Overview.EmployeeFactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_UserActionDetail_View, DTO.Overview.UserDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View, DTO.Overview.UserDetailWeeklyDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View, DTO.Overview.UserDetailWeeklyOverviewDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_TypeOfContract_View, DTO.TypeOfContractDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_AccountManagerType_View, DTO.AccountManagerTypeData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Company_View, DTO.CompanyDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_Branch_View, DTO.BranchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_ResposibleFor_View, DTO.EmployeeResponsibleForDTO>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.EmployeeResponsibleForDTO,EmployeeResponsibleFor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_QAQCFactoryAccess_View, DTO.QAQCFactoryAccess>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QAQCFactoryAccess, QAQCFactoryAccess>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDataRpt_View, DTO.Overview.UserDataRpt > ()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EmployeeMng_HitCountDataRpt_View, DTO.Overview.HitCountDataRpt>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.EmployeeSearchResult> DB2DTO_EmployeeSearchResultList(List<EmployeeMng_EmployeeSearchResult_View> dbItems)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (EmployeeMng_EmployeeSearchResult_View dbItem in dbItems)
            {
                dbItem.EmployeeNM = textInfo.ToTitleCase(dbItem.EmployeeNM.ToLower());
            }
            return AutoMapper.Mapper.Map<List<EmployeeMng_EmployeeSearchResult_View>, List<DTO.EmployeeSearchResult>>(dbItems);
        }

        public DTO.Employee DB2DTO_Employee(EmployeeMng_Employee_View dbItem, int userId)
        {
            DTO.Employee dtoItem =  AutoMapper.Mapper.Map<EmployeeMng_Employee_View, DTO.Employee>(dbItem);

            return dtoItem;
        }
        public DTO.EmployeeResponsibleForDTO DB2DTO_EmployeeResponsibleFor(EmployeeMng_Employee_View dbItem, int userId)
        {
            DTO.EmployeeResponsibleForDTO dtoItem = AutoMapper.Mapper.Map<EmployeeMng_Employee_View, DTO.EmployeeResponsibleForDTO>(dbItem);
            return dtoItem;
        }
        public List<DTO.Employee> DB2DTO_EmployeeList(List<EmployeeMng_Employee_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Employee_View>, List<DTO.Employee>>(dbItems);
        }

        public List<DTO.Overview.UserDataRpt> DB2DTO_UserDataRpt(List<DashboardMng_UserDataRpt_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDataRpt_View>, List<DTO.Overview.UserDataRpt>>(dbItems);
        }

        public List<DTO.Overview.HitCountDataRpt> DB2DTO_HitCountDataRpt(List<EmployeeMng_HitCountDataRpt_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_HitCountDataRpt_View>, List<DTO.Overview.HitCountDataRpt>>(dbItems);
        }

        public void DTO2BD(DTO.Employee dtoItem, ref Employee dbItem, string TmpFile, int userId)
        {
            //AutoMapper.Mapper.Map<DTO.Employee, Employee>(dtoItem, dbItem);
            if (dtoItem.AnnualLeaveSettings != null)
            {
                //check for child rows deleted
                foreach (AnnualLeaveSetting dbSetting in dbItem.AnnualLeaveSetting.ToArray())
                {
                    if (!dtoItem.AnnualLeaveSettings.Select(o => o.AnnualLeaveSettingID).Contains(dbSetting.AnnualLeaveSettingID))
                    {
                        dbItem.AnnualLeaveSetting.Remove(dbSetting);
                    }
                }
                //map child row
                foreach (DTO.AnnualLeaveSetting dtoSetting in dtoItem.AnnualLeaveSettings)
                {
                    AnnualLeaveSetting dbSetting;
                    if (dtoSetting.AnnualLeaveSettingID <= 0)
                    {
                        dbSetting = new AnnualLeaveSetting();
                        dbItem.AnnualLeaveSetting.Add(dbSetting);
                    }
                    else
                    {
                        dbSetting = dbItem.AnnualLeaveSetting.FirstOrDefault(o => o.AnnualLeaveSettingID == dtoSetting.AnnualLeaveSettingID);
                    }
                    if (dbSetting != null)
                    {
                        AutoMapper.Mapper.Map<DTO.AnnualLeaveSetting, AnnualLeaveSetting>(dtoSetting, dbSetting);
                    }
                }                
            }

            if (dtoItem.EmployeeFactorys != null)
            {
                foreach (var item in dbItem.EmployeeFactory.ToArray())
                {
                    if (!dtoItem.EmployeeFactorys.Select(o => o.EmployeeFactoryID).Contains(item.EmployeeFactoryID))
                    {
                        dbItem.EmployeeFactory.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.EmployeeFactorys)
                {
                    EmployeeFactory dbEmpFactory;
                    if (item.EmployeeFactoryID <= 0)
                    {
                        dbEmpFactory = new EmployeeFactory();
                        dbItem.EmployeeFactory.Add(dbEmpFactory);
                    }
                    else
                    {
                        dbEmpFactory = dbItem.EmployeeFactory.FirstOrDefault(o => o.EmployeeFactoryID == item.EmployeeFactoryID);
                    }
                    if (dbEmpFactory != null)
                    {
                        AutoMapper.Mapper.Map<DTO.EmployeeFactory, EmployeeFactory>(item, dbEmpFactory);
                    }
                }         
            }

            if (dtoItem.EmployeeResponsibleForDTOs != null)
            {
                foreach (var item in dbItem.EmployeeResponsibleFor.ToArray())
                {
                    if (!dtoItem.EmployeeResponsibleForDTOs.Select(o => o.ResposibleForID).Contains(item.ResposibleForID))
                    {
                        dbItem.EmployeeResponsibleFor.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.EmployeeResponsibleForDTOs)
                {
                    EmployeeResponsibleFor dbEmployeeResponsibleFor;
                    if (item.ResposibleForID <= 0 || item.ResposibleForID==null)
                    {
                        dbEmployeeResponsibleFor = new EmployeeResponsibleFor();
                        dbItem.EmployeeResponsibleFor.Add(dbEmployeeResponsibleFor);
                    }
                    else
                    {
                        dbEmployeeResponsibleFor = dbItem.EmployeeResponsibleFor.FirstOrDefault(o => o.ResposibleForID == item.ResposibleForID);
                    }
                    if (dbEmployeeResponsibleFor != null)
                    {
                        Mapper.Map(item, dbEmployeeResponsibleFor);
                    }
                }
            }
            Mapper.Map(dtoItem, dbItem);
            dbItem.DateStart = dtoItem.DateStart.ConvertStringToDateTime();
            dbItem.DateEnd = dtoItem.DateEnd.ConvertStringToDateTime();
            dbItem.DateOfBirth = dtoItem.DateOfBirth.ConvertStringToDateTime();
            dbItem.ContractPeriod = dtoItem.ContractPeriod.ConvertStringToDateTime();
            dbItem.ContractStartDate = dtoItem.ContractStartDate.ConvertStringToDateTime();

            //
            // map the field only for user with special permission
            //
            if (dtoItem.NeedToUpdateManagerData && fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_EMPLOYEE_MANAGER_NOTE))
            {
                dbItem.ManagerNote = dtoItem.ManagerNote;

                // manager attached files
                if (dtoItem.EmployeeFileDTOs != null)
                {
                    foreach (var item in dbItem.EmployeeFile.ToArray())
                    {
                        if (!dtoItem.EmployeeFileDTOs.Select(o => o.EmployeeFileID).Contains(item.EmployeeFileID))
                        {
                            if (!string.IsNullOrEmpty(item.FileUD))
                            {
                                fwFactory.RemoveImageFile(item.FileUD);
                            }

                            dbItem.EmployeeFile.Remove(item);
                        }
                    }
                    //map child row
                    foreach (var item in dtoItem.EmployeeFileDTOs)
                    {
                        EmployeeFile dbFile;
                        if (item.EmployeeFileID <= 0)
                        {
                            dbFile = new EmployeeFile();
                            dbItem.EmployeeFile.Add(dbFile);
                        }
                        else
                        {
                            dbFile = dbItem.EmployeeFile.FirstOrDefault(o => o.EmployeeFileID == item.EmployeeFileID);
                        }

                        if (dbFile != null)
                        {
                            AutoMapper.Mapper.Map<DTO.EmployeeFileDTO, EmployeeFile>(item, dbFile);
                            if (item.HasChanged)
                            {
                                dbFile.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, item.NewFile, dbFile.FileUD, item.FriendlyName);
                            }
                        }
                    }
                }

                // contract files
                if (dtoItem.EmployeeContractDTOs != null)
                {
                    foreach (var item in dbItem.EmployeeContract.ToArray())
                    {
                        if (!dtoItem.EmployeeContractDTOs.Select(o => o.EmployeeContractID).Contains(item.EmployeeContractID))
                        {
                            if (!string.IsNullOrEmpty(item.FileUD))
                            {
                                fwFactory.RemoveImageFile(item.FileUD);
                            }

                            dbItem.EmployeeContract.Remove(item);
                        }
                    }
                    //map child row
                    foreach (var item in dtoItem.EmployeeContractDTOs)
                    {
                        EmployeeContract dbContract;
                        if (item.EmployeeContractID <= 0)
                        {
                            dbContract = new EmployeeContract();
                            dbItem.EmployeeContract.Add(dbContract);
                        }
                        else
                        {
                            dbContract = dbItem.EmployeeContract.FirstOrDefault(o => o.EmployeeContractID == item.EmployeeContractID);
                        }

                        if (dbContract != null)
                        {
                            AutoMapper.Mapper.Map<DTO.EmployeeContractDTO, EmployeeContract>(item, dbContract);
                            dbContract.ValidFrom = item.ValidFrom.ConvertStringToDateTime();
                            if (item.HasChanged)
                            {
                                dbContract.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, item.NewFile, dbContract.FileUD, item.FriendlyName);
                            }
                        }
                    }
                }
                
            }
            if (dtoItem.factoryAccesses != null)
            {
                foreach (var item in dbItem.QAQCFactoryAccess.ToArray())
                {
                    if (!dtoItem.factoryAccesses.Select(o => o.QAQCFactoryAccessID).Contains(item.QAQCFactoryAccessID))
                    {
                        dbItem.QAQCFactoryAccess.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.factoryAccesses)
                {
                    QAQCFactoryAccess dbfac;
                    if (item.QAQCFactoryAccessID <= 0)
                    {
                        dbfac = new QAQCFactoryAccess();
                        dbItem.QAQCFactoryAccess.Add(dbfac);
                    }
                    else
                    {
                        dbfac = dbItem.QAQCFactoryAccess.FirstOrDefault(o => o.QAQCFactoryAccessID == item.QAQCFactoryAccessID);
                    }
                    if (dbfac != null)
                    {
                        Mapper.Map(item, dbfac);
                    }
                }
            }
        }

        public List<DTO.Director> DB2DTO_Director(List<EmployeeMng_Director_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Director_View>, List<DTO.Director>>(dbItems);
        }

        public List<DTO.UnSelectedUser> DB2DTO_UnSelectedUser(List<EmployeeMng_function_SearchUnSelectedUser_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_function_SearchUnSelectedUser_Result>, List<DTO.UnSelectedUser>>(dbItems);
        }

        public DTO.TilsoftUsage DB2DTO_TilsoftUsage(EmployeeMng_function_TilsoftAverageUsage_Result dbItems)
        {
            return AutoMapper.Mapper.Map<EmployeeMng_function_TilsoftAverageUsage_Result, DTO.TilsoftUsage>(dbItems);
        }

        public List<DTO.UserDetail> DB2DTO_UserDetail(List<EmployeeMng_UserActionDetail_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_UserActionDetail_View>, List<DTO.UserDetail>>(dbitems);
        }
        public List<DTO.UserDetailWeeklyDetail> DB2DTO_UserDetailWeeklyDetaill(List<EmployeeMng_UserActionDetailWeeklyDetail_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_UserActionDetailWeeklyDetail_View>, List<DTO.UserDetailWeeklyDetail>>(dbitems);
        }
        public List<DTO.UserDetailWeeklyOverview> DB2DTO_UserDetailWeeklyOverview(List<EmployeeMng_UserActionDetailWeeklyOverview_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_UserActionDetailWeeklyOverview_View>, List<DTO.UserDetailWeeklyOverview>>(dbitems);
        }

        //
        // overview
        //
        public DTO.Overview.EmployeeDTO DB2DTO_Overview_Employee(EmployeeMng_Overview_Employee_View dbItem)
        {
            return AutoMapper.Mapper.Map<EmployeeMng_Overview_Employee_View, DTO.Overview.EmployeeDTO>(dbItem);
        }
        public List<DTO.Overview.EmployeeFileDTO> DB2DTO_Overview_EmployeeFile(List<EmployeeMng_Overview_EmployeeFile_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Overview_EmployeeFile_View>, List<DTO.Overview.EmployeeFileDTO>>(dbitems);
        }
        public List<DTO.Overview.EmployeeContractDTO> DB2DTO_Overview_EmployeeContract(List<EmployeeMng_Overview_EmployeeContract_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Overview_EmployeeContract_View>, List<DTO.Overview.EmployeeContractDTO>>(dbitems);
        }
        public List<DTO.Overview.UserDetailDTO> DB2DTO_Overview_UserDetail(List<EmployeeMng_Overview_UserActionDetail_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Overview_UserActionDetail_View>, List<DTO.Overview.UserDetailDTO>>(dbitems);
        }
        public List<DTO.Overview.UserDetailWeeklyDetailDTO> DB2DTO_Overview_UserDetailWeeklyDetaill(List<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View>, List<DTO.Overview.UserDetailWeeklyDetailDTO>>(dbitems);
        }
        public List<DTO.Overview.UserDetailWeeklyOverviewDTO> DB2DTO_Overview_UserDetailWeeklyOverview(List<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View>, List<DTO.Overview.UserDetailWeeklyOverviewDTO>>(dbitems);
        }

        public List<DTO.TypeOfContractDTO> DB2DTO_TypeOfContract(List<EmployeeMng_TypeOfContract_View> items)
        {
            return AutoMapper.Mapper.Map<List<EmployeeMng_TypeOfContract_View>, List<DTO.TypeOfContractDTO>>(items);
        }
    }
}
