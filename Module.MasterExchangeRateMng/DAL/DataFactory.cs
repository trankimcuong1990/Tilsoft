using Library;
using Library.DTO;
using Module.MasterExchangeRateMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.MasterExchangeRateMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();

        private MasterExchangeRateMngEntities CreateContext()
        {
            return new MasterExchangeRateMngEntities(Library.Helper.CreateEntityConnectionString("DAL.MasterExchangeRateMngModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditForm GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.MasterExchangeRateEdit dtodb = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.MasterExchangeRateEdit>();
            try
            {
                using (MasterExchangeRateMngEntities context = CreateContext())
                {
                    MasterExchangeRate dbItem;
                    if (id == 0)
                    {
                        dbItem = new MasterExchangeRate();
                        context.MasterExchangeRate.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.MasterExchangeRate.Where(o => o.MasterExchangeRateID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "Data Not Found !";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_UpdateData(dtodb, ref dbItem);
                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.MasterExchangeRateID, out notification);
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

        //Custom 

        public DTO.SearchForm GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.SearchForm data = new DTO.SearchForm();
            data.Data = new List<MasterExchangeRateSearch>();

            totalRows = 0;

            string validDates = null;
            string currency = null;

            try
            {
                if (filters.ContainsKey("validDate") && !string.IsNullOrEmpty(filters["validDate"].ToString()))
                {
                    validDates = filters["validDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("currency") && !string.IsNullOrEmpty(filters["currency"].ToString()))
                {
                    currency = filters["currency"].ToString().Replace("'", "''");
                }

                DateTime? validDate = validDates.ConvertStringToDateTime();


                using (MasterExchangeRateMngEntities context = CreateContext())
                {
                    context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD");
                    context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "EUR");
                    context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "CNY");

                    totalRows = context.MasterExchangeRateMng_Function_SearchView(validDate, currency, orderBy, orderDirection).Count();
                    System.Data.Entity.Core.Objects.ObjectResult<MasterExchangeRateMng_MasterExchangeRate_SearchView> result = context.MasterExchangeRateMng_Function_SearchView(validDate, currency, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

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
                return data;
            }
        }

        public DTO.EditForm GetData(int userId, int id, out Notification notification)
        {

            DTO.EditForm data = new EditForm();
            data.Data = new DTO.MasterExchangeRateEdit();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (MasterExchangeRateMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_EditView(context.MasterExchangeRateMng_MasterExchangeRate_View.FirstOrDefault(o => o.MasterExchangeRateID == id));
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
            }
            return data;
        }

        public object UpdateIndexData(int userId, object dtoItems, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<DTO.UpdateIndexForm> parseData = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.UpdateIndexForm>>();

            try
            {
                using (MasterExchangeRateMngEntities context = CreateContext())
                {
                    foreach (DTO.UpdateIndexForm item in parseData)
                    {
                        MasterExchangeRate masterData = context.MasterExchangeRate.FirstOrDefault(o => o.MasterExchangeRateID == item.MasterExchangeRateID);
                        if (masterData != null)
                        {

                            masterData.ValidDate = item.ValidDate.ConvertStringToDateTime();
                            masterData.Currency = item.Currency;
                            masterData.ExchangeRate = item.ExchangeRate;
                        }
                    }
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
