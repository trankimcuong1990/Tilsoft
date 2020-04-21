using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.ClientLPMng.DTO;

namespace Module.ClientLPMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        private ClientLPMngEntities CreateContext()
        {
            return new ClientLPMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ClientLPMngModel"));
        }

        public EditFormData GetEditData(int userId, int id, out Notification notification)
        {
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new ClientLPDTO();
            data.Data.ClientLPNotificationDTOs = new List<ClientLPNotificationDTO>();
            data.SupportEmployeeDTOs = new List<SupportEmployeeDTO>();
            data.SupportLPManagingDTOs = new List<SupportLPManagingDTO>();
            notification = new Notification();
            try
            {
                using (ClientLPMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_ClientLP(context.ClientLPMng_ClientLPMng_View.FirstOrDefault(o => o.ClientLPID == id));
                    }
                    data.SupportLPManagingDTOs = converter.DB2DTO_SupportLPManaging(context.SupportMng_LPManagingTeam_View.ToList());
                    data.SupportEmployeeDTOs = converter.DB2DTO_SupportEmployee(context.ClientLPMng_SupportEmployee_View.ToList());
                }
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
            }
            return data;
        }

        public bool Update(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientLPDTO dtoClientLP = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ClientLPDTO>();
            try
            {
                ////get companyID
                //Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //int? companyID = fw_factory.GetCompanyID(userId);
                using (ClientLPMngEntities context = CreateContext())
                {
                    ClientLP dbItem = null;
                    dbItem = context.ClientLP.Where(o => o.ClientLPID == id).FirstOrDefault();
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {                       
                        //
                        //convert dto to db
                        converter.DTO2DB_ClientLP(dtoClientLP, ref dbItem);
                        //dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        
                        //remove orphan
                        context.ClientLPNotification.Local.Where(o => o.ClientLP == null).ToList().ForEach(o => context.ClientLPNotification.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetEditData(userId, dbItem.ClientLPID, out notification).Data;
                        return true;
                    }
                }
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
                return false;
            }
        }

        public List<DTO.SupportEmployeeDTO> GetEmployee(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;
                using (var context = CreateContext())
                {
                    return converter.DB2DTO_SupportEmployee(context.ClientLPMng_function_SupportUser(searchString).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return null;
            }
        }

        public DTO.SearchFormData GetDataWithFilters(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string clientUD = "";
                bool? isAutoUpdateSimilarLP = null;
                string clientNM = "";
                int? lpManagingTeamID = null;


                if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
                {
                    clientUD = filters["clientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("isAutoUpdateSimilarLP") && filters["isAutoUpdateSimilarLP"] != null && !string.IsNullOrEmpty(filters["isAutoUpdateSimilarLP"].ToString()))
                {
                    isAutoUpdateSimilarLP = (filters["isAutoUpdateSimilarLP"].ToString() == "true") ? true : false;
                }
                if (filters.ContainsKey("clientNM") && !string.IsNullOrEmpty(filters["clientNM"].ToString()))
                {
                    clientNM = filters["clientNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("lpManagingTeamID") && !string.IsNullOrEmpty(filters["lpManagingTeamID"].ToString()))
                {
                    lpManagingTeamID = Convert.ToInt32(filters["lpManagingTeamID"]);
                }

                using (ClientLPMngEntities context = CreateContext())
                {
                    //Add new Client
                    context.ClientLPMng_function_AddNewClientSetting();

                    totalRows = context.ClientLPMng_function_SearchClientLP(clientUD, clientNM, lpManagingTeamID, isAutoUpdateSimilarLP, orderBy, orderDirection).Count();
                    var result = context.ClientLPMng_function_SearchClientLP(clientUD, clientNM, lpManagingTeamID, isAutoUpdateSimilarLP, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_ClientLPSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return searchFormData;
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
                return searchFormData;
            }
        }

        public DTO.InitData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitData data = new DTO.InitData();
            data.SupportLPManagingDTOs = new List<SupportLPManagingDTO>();
            try
            {
                using (ClientLPMngEntities context = CreateContext())
                {                   
                    data.SupportLPManagingDTOs = converter.DB2DTO_SupportLPManaging(context.SupportMng_LPManagingTeam_View.ToList());              
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public bool Delete(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ClientLP.Where(o => o.ClientLPID == id).FirstOrDefault();
                    context.ClientLP.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
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
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        
    }
}
