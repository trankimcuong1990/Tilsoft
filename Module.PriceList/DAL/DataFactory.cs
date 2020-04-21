using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.PriceList.DAL
{
    internal class DataFactory : Library.Base.DataFactory<List<DTO.PriceListDTO>, DTO.PriceListDTO>
    {
        private DataConverter converter = new DataConverter();

        private PriceListEntities CreateContext()
        {
            return new PriceListEntities(Library.Helper.CreateEntityConnectionString("DAL.PriceListModel"));
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
                using (PriceListEntities context = CreateContext())
                {
                    var dbItem = context.PriceList.Where(o => o.PriceListID == id).FirstOrDefault();
                    context.PriceList.Remove(dbItem);
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

        public override DTO.PriceListDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PriceListDTO dtoItem;
            try
            {
                using (PriceListEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        PriceListMng_PriceList_View dbItem;
                        dbItem = context.PriceListMng_PriceList_View.FirstOrDefault(o => o.PriceListID == id);
                        dtoItem = converter.DB2DTO_PriceList(dbItem);
                    }
                    else
                    {
                        dtoItem = new DTO.PriceListDTO();
                    }
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

        public override List<DTO.PriceListDTO> GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.PriceListDTO> data = new List<DTO.PriceListDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string articleCode = string.Empty;
            string description = string.Empty;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();

            try
            {
                using (PriceListEntities context = CreateContext())
                {
                    totalRows = context.PriceListMng_function_SearchPriceList(orderBy, orderDirection, articleCode, description).Count();
                    var result = context.PriceListMng_function_SearchPriceList(orderBy, orderDirection, articleCode, description);
                    data = converter.DB2DTO_PriceListSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
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
                return new List<DTO.PriceListDTO>();
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PriceListDTO dtoPriceList = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PriceListDTO>();
            try
            {
                using (PriceListEntities context = CreateContext())
                {
                    PriceList dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.PriceList.Where(o => o.PriceListID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new PriceList();
                        context.PriceList.Add(dbItem);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_PriceList(dtoPriceList, ref dbItem);
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.PriceListID, out notification);
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
    }
}
