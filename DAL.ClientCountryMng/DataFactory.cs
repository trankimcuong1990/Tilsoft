using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ClientCountryMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ClientCountryMng.SearchFormData, DTO.ClientCountryMng.EditFormData, DTO.ClientCountryMng.ClientCountry>
    {
        private DataConverter converter = new DataConverter();
        private ClientCountryMngEntities CreateContext()
        {
            return new ClientCountryMngEntities(DALBase.Helper.CreateEntityConnectionString("ClientCountryMngModel"));
        }

        public override DTO.ClientCountryMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientCountryMng.SearchFormData data = new DTO.ClientCountryMng.SearchFormData();
            data.Data = new List<DTO.ClientCountryMng.ClientCountrySearchResult>();
            totalRows = 0;

            string ClientCountryUD = null;
            string ClientCountryNM = null;
            if (filters.ContainsKey("ClientCountryUD") && !string.IsNullOrEmpty(filters["ClientCountryUD"].ToString()))
            {
                ClientCountryUD = filters["ClientCountryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientCountryNM") && !string.IsNullOrEmpty(filters["ClientCountryNM"].ToString()))
            {
                ClientCountryNM = filters["ClientCountryNM"].ToString().Replace("'", "''");
            }
            //if (filters.ContainsKey("ClientCountryID") && filters["ClientCountryID"] != null && !string.IsNullOrEmpty(filters["ClientCountryID"].ToString()))
            //{
            //    ClientCountryID = Convert.ToInt32(filters["ClientCountryID"]);
            //}

            //try to get data
            try
            {
                using (ClientCountryMngEntities context = CreateContext())
                {
                    totalRows = context.ClientCountryMng_function_SearchCountry(ClientCountryUD, ClientCountryNM,  orderBy, orderDirection).Count();
                    var result = context.ClientCountryMng_function_SearchCountry(ClientCountryUD, ClientCountryNM,  orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ClientCountrySearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ClientCountryMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientCountryMng.EditFormData data = new DTO.ClientCountryMng.EditFormData();
            data.Data = new DTO.ClientCountryMng.ClientCountry();

            //try to get data
            try
            {
                using (ClientCountryMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_ClientCountry(context.ClientCountryMng_ClientCountry_View.FirstOrDefault(o => o.ClientCountryID == id));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientCountryMngEntities context = CreateContext())
                {
                    ClientCountry dbItem = context.ClientCountry.FirstOrDefault(o => o.ClientCountryID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "ClientCountry not found!";
                        return false;
                    }
                    else
                    {
                        context.ClientCountry.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ClientCountryMng.ClientCountry dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientCountryMngEntities context = CreateContext())
                {
                    ClientCountry dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientCountry();
                        context.ClientCountry.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientCountry.FirstOrDefault(o => o.ClientCountryID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "ClientCountry not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_ClientCountry(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ClientCountryID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.ClientCountryMng.ClientCountry dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ClientCountryMng.ClientCountry dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ClientCountryMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientCountryMng.EditFormData data = new DTO.ClientCountryMng.EditFormData();
            data.Data = new DTO.ClientCountryMng.ClientCountry();

            //try to get data
            try
            {
                using (ClientCountryMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_ClientCountry(context.ClientCountryMng_ClientCountry_View.FirstOrDefault(o => o.ClientCountryID == id));

                    if (data.Data == null)
                    {
                        data.Data = new DTO.ClientCountryMng.ClientCountry();
                        data.Data.CreatedBy = userId;
                        data.Data.CreatedDate = DateTime.Now;
                    }
                    data.Data.UpdatedBy = userId;
                    data.Data.UpdatedDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
