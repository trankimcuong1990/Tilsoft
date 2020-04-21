using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace Module.Agents.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.AgentsDTO, DTO.EditDTO>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private AgentsEntities CreateContext()
        {
            return new AgentsEntities(Library.Helper.CreateEntityConnectionString("DAL.AgentsModel"));
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
                using (AgentsEntities context = CreateContext())
                {
                    var dbItem = context.Agents.Where(o => o.AgentID == id).FirstOrDefault();
                    context.Agents.Remove(dbItem);
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

        public override DTO.EditDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditDTO dtoItem = new DTO.EditDTO();
            try
            {
                using (AgentsEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        var result = context.AgentMng_Agents_View.FirstOrDefault(o => o.AgentID == id);
                        dtoItem.data = converter.DB2DTO_Agents(result);
                        //Get Client
                        var clientItem = context.AngentMng_ClientAgent_View.Where(o => o.AgentID == id).ToList();
                        //Get Amount Data
                        var amountData = context.AgentsMng_AmountByClientAndSeason_View.ToList();
                        //Mapping Data
                        foreach (var item in clientItem.ToList())
                        {
                            List<DTO.AmountByClientAndSeason> items = new List<DTO.AmountByClientAndSeason>();
                            var itemDatas = amountData.Where(o=>o.ClientID == item.ClientID).ToList();
                            items = converter.DB2DTO_Amount(itemDatas);
                            dtoItem.amountByClientAndSeasons.AddRange(items);
                        }
                    }
                    dtoItem.clientCitiesDTOs = converter.DB2DTO_Clientcity(context.AgentMng_ClientCity_View.ToList());
                    dtoItem.clientCountryDTOs = converter.DB2DTO_ClientCountry(context.AgentMng_ClientCountry_View.ToList());
                    dtoItem.Seasons = supportFactory.GetSeason().ToList();
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
                return dtoItem;
            }
        }

        public override DTO.AgentsDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.AgentsDTO dtoAgents = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AgentsDTO>();
            try
            {
                using (AgentsEntities context = CreateContext())
                {
                    Agents dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.Agents.Where(o => o.AgentID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new Agents();
                        context.Agents.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_Agents(dtoAgents, ref dbItem);
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.AgentID, out notification);
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

        public List<DTO.AgentsDTO> Search(out Library.DTO.Notification notification)
        {            
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AgentsEntities context = CreateContext())
                {
                    var result = context.AgentMng_Agents_View.ToList();
                   return converter.DB2DTO_AgentsSearch(result);
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
                return new List<DTO.AgentsDTO>();
            }
        }

    }
}
