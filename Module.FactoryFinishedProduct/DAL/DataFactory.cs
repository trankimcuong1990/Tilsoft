using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryFinishedProduct.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormDataDTO, DTO.EditFormDataDTO>
    {
        private DataConverter converter = new DataConverter();

        private FactoryFinishedProductEntities CreateContext()
        {
            return new FactoryFinishedProductEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryFinishedProductModel"));
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
                using (FactoryFinishedProductEntities context = CreateContext())
                {
                    var dbItem = context.FactoryFinishedProduct.Where(o => o.FactoryFinishedProductID == id).FirstOrDefault();
                    context.FactoryFinishedProduct.Remove(dbItem);
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
            Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormDataDTO dtoItem = new DTO.EditFormDataDTO();
            try
            {
                using (FactoryFinishedProductEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryFinishedProduct dbItem;
                        dbItem = context.FactoryFinishedProduct.FirstOrDefault(o => o.FactoryFinishedProductID == id);
                        dtoItem.Data = converter.DB2DTO_FactoryFinishedProduct(dbItem);
                    }
                    else
                    {
                        dtoItem.Data = new DTO.FactoryFinishedProductDTO();
                        dtoItem.Data.FactoryFinishedProductPriceDTOs = new List<DTO.FactoryFinishedProductPriceDTO>();
                    }
                    dtoItem.FactoryTeams = supportFactory.GetFactoryTeam();
                    dtoItem.FactorySteps = supportFactory.GetFactoryStep();
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

            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            int? factoryTeamID = null;
            int? factoryStepID = null;

            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("factoryTeamID") && filters["factoryTeamID"]!=null) factoryTeamID = Convert.ToInt32(filters["factoryTeamID"].ToString());
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"]!=null) factoryStepID = Convert.ToInt32(filters["factoryStepID"].ToString());

            try
            {
                using (FactoryFinishedProductEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductMng_function_SearchFactoryFinishedProduct(orderBy, orderDirection, factoryFinishedProductUD, factoryFinishedProductNM, factoryTeamID, factoryStepID).Count();
                    var result = context.FactoryFinishedProductMng_function_SearchFactoryFinishedProduct(orderBy, orderDirection, factoryFinishedProductUD, factoryFinishedProductNM, factoryTeamID, factoryStepID);
                    data.Data = converter.DB2DTO_FactoryFinishedProductSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                data.FactoryTeams = supportFactory.GetFactoryTeam();
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
            DTO.FactoryFinishedProductDTO dtoFactoryFinishedProduct = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryFinishedProductDTO>();
            try
            {
                using (FactoryFinishedProductEntities context = CreateContext())
                {
                    FactoryFinishedProduct dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.FactoryFinishedProduct.Where(o => o.FactoryFinishedProductID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryFinishedProduct();
                        context.FactoryFinishedProduct.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryFinishedProduct(dtoFactoryFinishedProduct, ref dbItem);
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryFinishedProductID, out notification);
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

        public int CreateFactoryFinishedProductPrice(int factoryFinishedProductID, object dtoObjectItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Update success" };
            DTO.FactoryFinishedProductPriceDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoObjectItem).ToObject<DTO.FactoryFinishedProductPriceDTO>();
            try
            {
                if (!dtoItem.FactoryTeamID.HasValue)
                {
                    throw new Exception("You have to select team");
                }
                if (!dtoItem.FactoryStepID.HasValue)
                {
                    throw new Exception("You have to select step");
                }
                using (FactoryFinishedProductEntities context = CreateContext())
                {
                    FactoryFinishedProductPrice dbItem;
                    if (dtoItem.FactoryFinishedProductPriceID > 0)
                    {
                        dbItem = context.FactoryFinishedProductPrice.Where(o => o.FactoryFinishedProductPriceID == dtoItem.FactoryFinishedProductPriceID).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new FactoryFinishedProductPrice();
                        dbItem.FactoryFinishedProductID = factoryFinishedProductID;
                        context.FactoryFinishedProductPrice.Add(dbItem);
                    }
                    if (dbItem != null)
                    {
                        dbItem.FactoryTeamID = dtoItem.FactoryTeamID;
                        dbItem.FactoryStepID = dtoItem.FactoryStepID;
                        dbItem.Price = dtoItem.Price;
                        dbItem.HalfRoundPrice = dtoItem.HalfRoundPrice;
                        dbItem.DoubleRoundPrice = dtoItem.DoubleRoundPrice;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update component price");
                    }
                    context.SaveChanges();
                    return dbItem.FactoryFinishedProductPriceID;
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

        public bool DeleteFactoryFinishedProductPrice(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryFinishedProductEntities context = CreateContext())
                {
                    var dbItem = context.FactoryFinishedProductPrice.Where(o => o.FactoryFinishedProductPriceID == id).FirstOrDefault();
                    context.FactoryFinishedProductPrice.Remove(dbItem);
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
