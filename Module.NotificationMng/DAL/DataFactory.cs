using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library.DTO;
using Module.NotificationMng.DTO;

namespace Module.NotificationMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormDataDTO, DTO.EditFormDataDTO>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private NotificationMngEntities CreateContext()
        {
            return new NotificationMngEntities(Library.Helper.CreateEntityConnectionString("DAL.NotificationMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormDataDTO GetData(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormDataDTO data = new DTO.EditFormDataDTO()
            {
                Data = new NotificationGroupDTO()
                {
                    NotificationGroupMemberData = new List<NotificationGroupMemberDTO>()
                }
            };

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.NotificationMng_NotificationGroup_View.FirstOrDefault(o => o.NotificationGroupID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not found data!";
                            return data;
                        }

                        data.Data = converter.DB2DTO_NotificationGroup(dbItem);
                    }

                    data.Modules = converter.DB2DTO_Module(context.NotificationMng_Module_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public override SearchFormDataDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchFormDataDTO data = new DTO.SearchFormDataDTO();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

            string NotificationGroupUD = null;
            string NotificationGroupNM = null;
            string Description = null;


            if (filters.ContainsKey("NotificationGroupUD") && filters["NotificationGroupUD"] != null && !string.IsNullOrEmpty(filters["NotificationGroupUD"].ToString().Trim().Replace("'", "''")))
            {
                NotificationGroupUD = filters["NotificationGroupUD"].ToString();
            }
            if (filters.ContainsKey("NotificationGroupNM") && filters["NotificationGroupNM"] != null && !string.IsNullOrEmpty(filters["NotificationGroupNM"].ToString().Trim().Replace("'", "''")))
            {
                NotificationGroupNM = filters["NotificationGroupNM"].ToString();
            }
            if (filters.ContainsKey("Description") && filters["Description"] != null && !string.IsNullOrEmpty(filters["Description"].ToString().Trim().Replace("'", "''")))
            {
                Description = filters["Description"].ToString();
            }


            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.NotificationMng_function_SearchResult(NotificationGroupUD, NotificationGroupNM, Description, orderBy, orderDirection).Count();
                    var result = context.NotificationMng_function_SearchResult(NotificationGroupUD, NotificationGroupNM, Description, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                //data.FactorySteps = supportFactory.GetFactoryStep();
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new DTO.SearchFormDataDTO();
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }


        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification()
            {
                Type = Library.DTO.NotificationType.Success
            };

            DTO.NotificationGroupDTO dataItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.NotificationGroupDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    NotificationGroup dbItem;

                    if (id > 0)
                    {
                        dbItem = context.NotificationGroup.FirstOrDefault(s => s.NotificationGroupID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not find data";
                            return false;
                        }
                    }
                    else
                    {
                        dbItem = context.NotificationGroup.FirstOrDefault(s => s.NotificationGroupUD.Equals(dataItem.NotificationGroupUD));
                        if (dbItem != null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = dbItem.NotificationGroupUD + " is existed in database!";
                            return false;
                        }

                        dbItem = new NotificationGroup();
                        context.NotificationGroup.Add(dbItem);
                    }

                    converter.DTO2DB_Update(dataItem, ref dbItem);

                    context.NotificationGroupMember.Local.Where(o => o.NotificationGroup == null).ToList().ForEach(o => context.NotificationGroupMember.Remove(o));
                    context.NotificationGroupStatus.Local.Where(o => o.NotificationGroup == null).ToList().ForEach(o => context.NotificationGroupStatus.Remove(o));
                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_NotificationGroup(context.NotificationMng_NotificationGroup_View.FirstOrDefault(o => o.NotificationGroupID == dbItem.NotificationGroupID));

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }
        public ListModuleStatusDTO GetModuleStatuses(int moduleID, int notificationGroupID, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            ListModuleStatusDTO data = new ListModuleStatusDTO();
            data.ModuleStatusDTOs = new List<ModuleStatusDTO>();
            data.NotificationGroupStatusDTOs = new List<NotificationGroupStatusDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.NotificationMng_ModuleStatus_View.Where(o => o.ModuleID == moduleID);
                    data.ModuleStatusDTOs = converter.DB2DTO_ModuleStatus(dbItem.ToList());
                    if (notificationGroupID > 0)
                    {
                        data.NotificationGroupStatusDTOs = AutoMapper.Mapper.Map<List<NotificationMng_NotificationGroupStatus_View>, List<NotificationGroupStatusDTO>>(context.NotificationMng_NotificationGroupStatus_View.Where(o => o.ModuleID == moduleID && o.NotificationGroupID == notificationGroupID).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public List<DTO.Users> GetUser(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            List<DTO.Users> result = new List<Users>();

            try
            {
                using (var context = this.CreateContext())
                {
                    string searchQuery = filters["searchQuery"].ToString();
                    var dbItems = context.NotificationMng_NotificationUser_View.Where(o => o.EmployeeNM.Contains(searchQuery) || o.FullName.Contains(searchQuery)).ToList();
                    //AutoMapper.Mapper.Map<List<NotificationMng_NotificationUser_View>, List<DTO.Users>>(dbItems);
                    return AutoMapper.Mapper.Map<List<NotificationMng_NotificationUser_View>, List<DTO.Users>>(dbItems);

                }

                return result;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return result;
            }
        }

        public ListModuleStatusDTO UpdateModuleStatus(int id, int moduleID, string statusUD, string statusNM, out Notification notification)
        {
            notification = new Library.DTO.Notification()
            {
                Type = Library.DTO.NotificationType.Success
            };
            ListModuleStatusDTO data = new ListModuleStatusDTO();
            if (statusUD == "" || statusUD == null || statusUD == "undefined")
            {

                notification.Type = NotificationType.Error;
                notification.Message = "Please fill code status!";
                return data;
            }
            if (statusNM == "" || statusNM == null || statusNM == "undefined")
            {

                notification.Type = NotificationType.Error;
                notification.Message = "Please fill name status!";
                return data;
            }
            if (moduleID == null)
            {

                notification.Type = NotificationType.Error;
                notification.Message = "Please choose module!";
                return data;
            }

            try
            {
                using (var context = CreateContext())
                {
                    ModuleStatus dbItem = null;

                    int? listItem = context.ModuleStatus.Where(s => s.ModuleID == moduleID).Max(s => s.StatusID);
                    dbItem = context.ModuleStatus.Where(s => s.ModuleID == moduleID && s.StatusUD == statusUD && s.StatusNM == statusNM).FirstOrDefault();
                    if (dbItem != null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "This module is existed " + dbItem.StatusNM;
                        return data;
                    }

                    dbItem = new ModuleStatus();
                    context.ModuleStatus.Add(dbItem);

                    ModuleStatusDTO dtoItem = new ModuleStatusDTO();
                    if (listItem == null)
                    {
                        dtoItem.StatusID = 1;
                    }
                    else
                    {
                        dtoItem.StatusID = listItem + 1;
                    }
                    dtoItem.ModuleStatusID = -1;
                    dtoItem.ModuleID = moduleID;
                    dtoItem.StatusUD = statusUD;
                    dtoItem.StatusNM = statusNM;

                    AutoMapper.Mapper.Map<DTO.ModuleStatusDTO, ModuleStatus>(dtoItem, dbItem);

                    context.SaveChanges();
                    return GetModuleStatuses(id, moduleID, out notification);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return data;
            }
        }
    }
}
