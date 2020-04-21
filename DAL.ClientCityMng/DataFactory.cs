using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ClientCityMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ClientCityMng.SearchFormData, DTO.ClientCityMng.EditFormData, DTO.ClientCityMng.ClientCity>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        //public DataFactory(string tempFolder)
        //{
        //    _TempFolder = tempFolder + @"\";
        //}

        private ClientCityMngEntities CreateContext()
        {
            return new ClientCityMngEntities(DALBase.Helper.CreateEntityConnectionString("ClientCityMngModel"));
        }

        public override DTO.ClientCityMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ClientCityMng.SearchFormData data = new DTO.ClientCityMng.SearchFormData();
            //DTO.ClientCountryMng.SearchFormData ClientCountrys = new DTO.ClientCountryMng.SearchFormData();
            data.ClientCountrys = new List<DTO.Support.ClientCountry>();

            //data.ClientCountrys = new List<DTO.ClientCityMng.ClientCitySearchResult>().ToList(); 

            data.Data = new List<DTO.ClientCityMng.ClientCitySearchResult>();

            totalRows = 0;

            string ClientCityUD = null;
            string ClientCityNM = null;
            int? ClientCountryID = null;
            if (filters.ContainsKey("ClientCityUD") && !string.IsNullOrEmpty(filters["ClientCityUD"].ToString()))
            {
                ClientCityUD = filters["ClientCityUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientCityNM") && !string.IsNullOrEmpty(filters["ClientCityNM"].ToString()))
            {
                ClientCityNM = filters["ClientCityNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientCountryID") && filters["ClientCountryID"] != null && !string.IsNullOrEmpty(filters["ClientCountryID"].ToString()))
            {
                ClientCountryID = Convert.ToInt32(filters["ClientCountryID"]);
            }

            //try to get data
            try
            {
                using (ClientCityMngEntities context = CreateContext())
                {
                    totalRows = context.ClientCityMng_function_SearchCity(ClientCityUD, ClientCityNM, ClientCountryID, orderBy, orderDirection).Count();
                    var result = context.ClientCityMng_function_SearchCity(ClientCityUD, ClientCityNM, ClientCountryID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ClientCitySearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    data.ClientCountrys = supportFactory.GetClientCountry().ToList();

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ClientCityMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.ClientCityMng.EditFormData data = new DTO.ClientCityMng.EditFormData();
            data.Data = new DTO.ClientCityMng.ClientCity();
            data.ClientCountrys = new List<DTO.Support.ClientCountry>();

            //raboteshtoto
            try
            {
                using (ClientCityMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_ClientCity(context.ClientCityMng_ClientCity_View.FirstOrDefault(o => o.ClientCityID == id));
                    data.ClientCountrys = supportFactory.GetClientCountry().ToList();
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
                using (ClientCityMngEntities context = CreateContext())
                {
                    ClientCity dbItem = context.ClientCities.FirstOrDefault(o => o.ClientCityID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "ClientCity not found!";
                        return false;
                    }
                    else
                    {
                        context.ClientCities.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ClientCityMng.ClientCity dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (ClientCityMngEntities context = CreateContext())
                {
                    ClientCity dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientCity();
                        context.ClientCities.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientCities.FirstOrDefault(o => o.ClientCityID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "ClientCity not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_ClientCity(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ClientCityID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "ClientCityUDUnique")
                    {
                        notification.Message = "The ClientCity Code is already exists";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.ClientCityMng.ClientCity dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ClientCityMng.ClientCity dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //    public DTO.ClientCityMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        //    {
        //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //        DTO.ClientCityMng.SearchFilterData data = new DTO.ClientCityMng.SearchFilterData();



        //        data.ClientCountrys  = new List<DTO.Support.ClientCountry>();

        //        //try to get data
        //        try
        //        {

        //            data.ClientCountrys = supportFactory.GetClientCountry().ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            notification.Type = Library.DTO.NotificationType.Error;
        //            notification.Message = ex.Message;
        //        }

        //        return data;
        //    }

        public DTO.ClientCityMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.ClientCityMng.EditFormData data = new DTO.ClientCityMng.EditFormData();
            data.Data = new DTO.ClientCityMng.ClientCity();
            data.ClientCountrys = new List<DTO.Support.ClientCountry>();

            //raboteshtoto
            try
            {
                using (ClientCityMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_ClientCity(context.ClientCityMng_ClientCity_View.FirstOrDefault(o => o.ClientCityID == id));

                    if (data.Data == null)
                    {
                        data.Data = new DTO.ClientCityMng.ClientCity();

                        data.Data.CreatedBy = userId;
                        data.Data.CreatedDate = DateTime.Now;
                    }

                    data.Data.UpdatedBy = userId;
                    data.Data.UpdatedDate = DateTime.Now;

                    data.ClientCountrys = supportFactory.GetClientCountry().ToList();
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
