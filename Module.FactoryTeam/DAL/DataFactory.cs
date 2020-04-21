using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryTeam.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormDataDTO, DTO.EditFormDataDTO>
    {
        private DataConverter converter = new DataConverter();

        private FactoryTeamEntities CreateContext()
        {
            return new FactoryTeamEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryTeamModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    var dbItem = context.FactoryTeam.Where(o => o.FactoryTeamID == id).FirstOrDefault();
                    context.FactoryTeam.Remove(dbItem);
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

        public override DTO.EditFormDataDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
            DTO.EditFormDataDTO dtoItem = new DTO.EditFormDataDTO();
            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryTeam dbItem;
                        dbItem = context.FactoryTeam.FirstOrDefault(o => o.FactoryTeamID == id);
                        dtoItem.Data = converter.DB2DTO_FactoryTeam(dbItem);
                    }
                    else
                    {
                        dtoItem.Data = new DTO.FactoryTeamDTO();
                        dtoItem.Data.FactoryMaterialGroupTeamDTOs = new List<DTO.FactoryMaterialGroupTeamDTO>();
                        dtoItem.Data.FactoryTeamStepDTOs = new List<DTO.FactoryTeamStepDTO>();
                    }
                    dtoItem.FactorySteps = supportFactory.GetFactoryStep();
                    dtoItem.FactoryMaterialGroups = supportFactory.GetFactoryMaterialGroup();
                    return dtoItem;
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
                return null;
            }
        }

        public override DTO.SearchFormDataDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormDataDTO data = new DTO.SearchFormDataDTO();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

            string FactoryTeamUD = string.Empty;
            string FactoryTeamNM = string.Empty;
            int? factoryStepID = null;

            if (filters.ContainsKey("factoryTeamUD")) FactoryTeamUD = filters["factoryTeamUD"].ToString();
            if (filters.ContainsKey("factoryTeamNM")) FactoryTeamNM = filters["factoryTeamNM"].ToString();
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"]!=null) factoryStepID = Convert.ToInt32(filters["factoryStepID"].ToString());

            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    totalRows = context.FactoryTeamMng_function_SearchFactoryTeam(orderBy, orderDirection, FactoryTeamUD, FactoryTeamNM, factoryStepID).Count();
                    var result = context.FactoryTeamMng_function_SearchFactoryTeam(orderBy, orderDirection, FactoryTeamUD, FactoryTeamNM, factoryStepID);
                    data.Data = converter.DB2DTO_FactoryTeamSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                data.FactorySteps = supportFactory.GetFactoryStep();
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

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryTeamDTO dtoFactoryTeam = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryTeamDTO>();
            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    FactoryTeam dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.FactoryTeam.Where(o => o.FactoryTeamID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryTeam();
                        context.FactoryTeam.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryTeam(dtoFactoryTeam, ref dbItem);
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryTeamID, out notification);
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

        public int CreateFactoryTeamStep(int factoryTeamID,object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update success" };
            DTO.FactoryTeamStepDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.FactoryTeamStepDTO>();
            try
            {
                if (!dtoItem.FactoryStepID.HasValue)
                {
                    throw new Exception("You have to select step");
                }
                if (!dtoItem.StepIndex.HasValue)
                {
                    throw new Exception("You have to fill-in index");
                }
                using (FactoryTeamEntities context = CreateContext())
                {
                    FactoryTeamStep dbItem;
                    if (dtoItem.FactoryTeamStepID > 0)
                    {
                        dbItem = context.FactoryTeamStep.Where(o =>o.FactoryTeamStepID == dtoItem.FactoryTeamStepID).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryTeamStep();
                        dbItem.FactoryTeamID = factoryTeamID;                        
                        context.FactoryTeamStep.Add(dbItem);
                    }
                    if (dbItem != null)
                    {
                        dbItem.FactoryStepID = dtoItem.FactoryStepID;
                        dbItem.StepIndex = dtoItem.StepIndex;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update team step");
                    }
                    context.SaveChanges();
                    return dbItem.FactoryTeamStepID;
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
                return -1;
            }
        }

        public bool DeleteTeamStep(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    var dbItem = context.FactoryTeamStep.Where(o => o.FactoryTeamStepID == id).FirstOrDefault();
                    context.FactoryTeamStep.Remove(dbItem);
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

        public int CreateMaterialGroupTeam(int factoryTeamID, object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update success" };
            DTO.FactoryMaterialGroupTeamDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.FactoryMaterialGroupTeamDTO>();
            try
            {
                if (!dtoItem.FactoryMaterialGroupID.HasValue)
                {
                    throw new Exception("You have to select material group");
                }
                using (FactoryTeamEntities context = CreateContext())
                {
                    FactoryMaterialGroupTeam dbItem;
                    if (dtoItem.FactoryMaterialGroupTeamID > 0)
                    {
                        dbItem = context.FactoryMaterialGroupTeam.Where(o => o.FactoryMaterialGroupTeamID == dtoItem.FactoryMaterialGroupTeamID).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryMaterialGroupTeam();
                        dbItem.FactoryTeamID = factoryTeamID;
                        context.FactoryMaterialGroupTeam.Add(dbItem);
                    }
                    if (dbItem != null)
                    {
                        dbItem.FactoryMaterialGroupID = dtoItem.FactoryMaterialGroupID;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update material group");
                    }
                    context.SaveChanges();
                    return dbItem.FactoryMaterialGroupTeamID;
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
                return -1;
            }
        }

        public bool DeleteMaterialGroupTeam(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryTeamEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterialGroupTeam.Where(o => o.FactoryMaterialGroupTeamID == id).FirstOrDefault();
                    context.FactoryMaterialGroupTeam.Remove(dbItem);
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


    }
}
