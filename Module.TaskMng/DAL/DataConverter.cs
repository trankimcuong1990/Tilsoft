using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.TaskMng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "TaskMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<TaskMng_TaskSearchResult_View, DTO.TaskSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EstEndDate, o => o.ResolveUsing(s => s.EstEndDate.HasValue ? s.EstEndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TaskMng_Task_View, DTO.Task>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EstEndDate, o => o.ResolveUsing(s => s.EstEndDate.HasValue ? s.EstEndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.TaskSteps, o => o.MapFrom(s => s.TaskMng_TaskStep_View))
                    .ForMember(d => d.TaskFiles, o => o.MapFrom(s => s.TaskMng_TaskFile_View))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TaskMng_TaskFile_View, DTO.TaskFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TaskMng_TaskStep_View, DTO.TaskStep>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskStepUsers, o => o.MapFrom(s => s.TaskMng_TaskStepUser_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TaskMng_TaskStepUser_View, DTO.TaskStepUser>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TaskMng_TaskStepComment_View, DTO.TaskStepComment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.TaskStepCommentFiles, o => o.MapFrom(s => s.TaskMng_TaskStepCommentFile_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TaskMng_TaskStepCommentFile_View, DTO.TaskStepCommentFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.Task, Task>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskID, o => o.Ignore())
                    .ForMember(d => d.TaskUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.StartDate, o => o.Ignore())
                    .ForMember(d => d.EndDate, o => o.Ignore())
                    .ForMember(d => d.EstEndDate, o => o.Ignore())
                    .ForMember(d => d.TaskStep, o => o.Ignore())
                    .ForMember(d => d.TaskFile, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TaskFile, TaskFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskFileID, o => o.Ignore())
                    .ForMember(d => d.TaskID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TaskStep, TaskStep>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskStepID, o => o.Ignore())
                    .ForMember(d => d.TaskID, o => o.Ignore())
                    .ForMember(d => d.TaskStepUser, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TaskStepUser, TaskStepUser>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskStepUserID, o => o.Ignore())
                    .ForMember(d => d.TaskStepID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TaskStepComment, TaskStepComment>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskStepCommentID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.TaskStepCommentFile, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.TaskStepCommentFile, TaskStepCommentFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TaskStepCommentFileID, o => o.Ignore())
                    .ForMember(d => d.TaskStepCommentID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TaskSearchResult> DB2DTO_TaskSearchResultList(List<TaskMng_TaskSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TaskMng_TaskSearchResult_View>, List<DTO.TaskSearchResult>>(dbItems);
        }

        public DTO.Task DB2DTO_Task(TaskMng_Task_View dbItem)
        {
            return AutoMapper.Mapper.Map<TaskMng_Task_View, DTO.Task>(dbItem);
        }

        public DTO.TaskStep DB2DTO_TaskStep(TaskMng_TaskStep_View dbItem)
        {
            return AutoMapper.Mapper.Map<TaskMng_TaskStep_View, DTO.TaskStep>(dbItem);
        }

        public List<DTO.TaskStepComment> DB2DTO_TaskStepCommentList(List<TaskMng_TaskStepComment_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<TaskMng_TaskStepComment_View>, List<DTO.TaskStepComment>>(dbItem);
        }

        public void DTO2DB_TaskStepComment(DTO.TaskStepComment dtoItem, ref TaskStepComment dbItem, string _tempFolder)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.TaskStepComment, TaskStepComment>(dtoItem, dbItem);

            // map file
            if (dtoItem.TaskStepCommentFiles != null)
            {
                // check for child rows deleted
                foreach (TaskStepCommentFile dbFile in dbItem.TaskStepCommentFile.ToArray())
                {
                    if (!dtoItem.TaskStepCommentFiles.Select(o => o.TaskStepCommentFileID).Contains(dbFile.TaskStepCommentFileID))
                    {
                        if (!string.IsNullOrEmpty(dbFile.FileUD))
                        {
                            fwFactory.RemoveFile(dbFile.FileUD);
                        }

                        dbItem.TaskStepCommentFile.Remove(dbFile);
                    }
                }

                // map child rows
                foreach (DTO.TaskStepCommentFile dtoFile in dtoItem.TaskStepCommentFiles)
                {
                    TaskStepCommentFile dbFile;
                    if (dtoFile.TaskStepCommentFileID <= 0)
                    {
                        dbFile = new TaskStepCommentFile();
                        dbItem.TaskStepCommentFile.Add(dbFile);
                    }
                    else
                    {
                        dbFile = dbItem.TaskStepCommentFile.FirstOrDefault(o => o.TaskStepCommentFileID == dtoFile.TaskStepCommentFileID);
                    }

                    if (dbFile != null)
                    {
                        if (dtoFile.HasChanged)
                        {
                            dbFile.FileUD = fwFactory.CreateFilePointer(_tempFolder, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                        }
                        AutoMapper.Mapper.Map<DTO.TaskStepCommentFile, TaskStepCommentFile>(dtoFile, dbFile);
                    }
                }
            }
        }

        public void DTO2DB(DTO.Task dtoItem, ref Task dbItem, string _tempFolder)
        {
            // map fields
            AutoMapper.Mapper.Map<DTO.Task, Task>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.StartDate))
            {
                if (DateTime.TryParse(dtoItem.StartDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.StartDate = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.EstEndDate))
            {
                if (DateTime.TryParse(dtoItem.EstEndDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.EstEndDate = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.EndDate))
            {
                if (DateTime.TryParse(dtoItem.EndDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.EndDate = tmpDate;
                }
            }

            // map file
            if (dtoItem.TaskFiles != null)
            {
                // check for child rows deleted
                foreach (TaskFile dbFile in dbItem.TaskFile.ToArray())
                {
                    if (!dtoItem.TaskFiles.Select(o => o.TaskFileID).Contains(dbFile.TaskFileID))
                    {
                        if (!string.IsNullOrEmpty(dbFile.FileUD))
                        {
                            fwFactory.RemoveFile(dbFile.FileUD);
                        }

                        dbItem.TaskFile.Remove(dbFile);
                    }
                }

                // map child rows
                foreach (DTO.TaskFile dtoFile in dtoItem.TaskFiles)
                {
                    TaskFile dbFile;
                    if (dtoFile.TaskFileID <= 0)
                    {
                        dbFile = new TaskFile();
                        dbItem.TaskFile.Add(dbFile);
                    }
                    else
                    {
                        dbFile = dbItem.TaskFile.FirstOrDefault(o => o.TaskFileID == dtoFile.TaskFileID);
                    }

                    if (dbFile != null)
                    {
                        if (dtoFile.HasChanged)
                        {
                            dbFile.FileUD = fwFactory.CreateFilePointer(_tempFolder, dtoFile.NewFile, dtoFile.FileUD, dtoFile.FriendlyName);
                        }
                        AutoMapper.Mapper.Map<DTO.TaskFile, TaskFile>(dtoFile, dbFile);
                    }
                }
            }

            // map step detail
            if (dtoItem.TaskSteps != null)
            {
                // check for child rows deleted
                foreach (TaskStep dbStep in dbItem.TaskStep.ToArray())
                {
                    if (!dtoItem.TaskSteps.Select(o => o.TaskStepID).Contains(dbStep.TaskStepID))
                    {
                        dbItem.TaskStep.Remove(dbStep);
                    }
                }

                // map child rows
                foreach (DTO.TaskStep dtoStep in dtoItem.TaskSteps)
                {
                    TaskStep dbStep;
                    if (dtoStep.TaskStepID <= 0)
                    {
                        dbStep = new TaskStep();
                        dbItem.TaskStep.Add(dbStep);
                    }
                    else
                    {
                        dbStep = dbItem.TaskStep.FirstOrDefault(o => o.TaskStepID == dtoStep.TaskStepID);
                    }

                    if (dbStep != null)
                    {
                        AutoMapper.Mapper.Map<DTO.TaskStep, TaskStep>(dtoStep, dbStep);

                        // map step user
                        if (dtoStep.TaskStepUsers != null)
                        {
                            // check for child rows deleted
                            foreach (TaskStepUser dbStepUser in dbStep.TaskStepUser.ToArray())
                            {
                                if (!dtoStep.TaskStepUsers.Select(o => o.TaskStepUserID).Contains(dbStepUser.TaskStepUserID))
                                {
                                    dbStep.TaskStepUser.Remove(dbStepUser);
                                }
                            }

                            // map child rows
                            foreach (DTO.TaskStepUser dtoStepUser in dtoStep.TaskStepUsers)
                            {
                                TaskStepUser dbStepUser;
                                if (dtoStepUser.TaskStepUserID <= 0)
                                {
                                    dbStepUser = new TaskStepUser();
                                    dbStep.TaskStepUser.Add(dbStepUser);
                                }
                                else
                                {
                                    dbStepUser = dbStep.TaskStepUser.FirstOrDefault(o => o.TaskStepUserID == dtoStepUser.TaskStepUserID);
                                }

                                if (dbStepUser != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.TaskStepUser, TaskStepUser>(dtoStepUser, dbStepUser);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
