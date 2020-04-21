using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLocationMng.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        public DTO.InitForm GetInitForm(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitForm data = new DTO.InitForm();
            data.Countries = new List<Support.DTO.ManufacturerCountry>();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Countries = supportFactory.GetManufacturerCountry();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public DTO.SearchForm GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();
            data.FactoryLocations = new List<DTO.FactoryLocation>();

            try
            {
                using (var context = CreateContext())
                {
                    string locationUD = filters.ContainsKey("locationUD") && filters["locationUD"] != null && !string.IsNullOrEmpty(filters["locationUD"].ToString().Replace("'", "''")) ? filters["locationUD"].ToString().Trim() : null;
                    string locationNM = filters.ContainsKey("locationNM") && filters["locationNM"] != null && !string.IsNullOrEmpty(filters["locationNM"].ToString().Replace("'", "''")) ? filters["locationNM"].ToString().Trim() : null;
                    int? manufacturerCountryID = filters.ContainsKey("manufacturerCountryID") && filters["manufacturerCountryID"] != null && !string.IsNullOrEmpty(filters["manufacturerCountryID"].ToString().Replace("'", "''")) ? (int?)Convert.ToInt32(filters["manufacturerCountryID"].ToString().Trim()) : null;

                    totalRows = context.FactoryLocationMng_function_LocationSearchResult(locationUD, locationNM, manufacturerCountryID, orderBy, orderDirection).Count();
                    data.FactoryLocations = converter.DB2DTO_FactoryLocationSearchResult(context.FactoryLocationMng_function_LocationSearchResult(locationUD, locationNM, manufacturerCountryID, orderBy, orderDirection).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.FactoryLocation factoryLocation = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryLocation>();
            bool resultUpdate = false;

            // Check location code is null.
            if (string.IsNullOrEmpty(factoryLocation.LocationUD.Trim()))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Please input Location Code!";

                return resultUpdate;
            }

            try
            {
                using (var context = CreateContext())
                {
                    Location location = null;

                    if (id < 0)
                    {
                        // Check location code exist.
                        location = context.Location.FirstOrDefault(o => o.LocationUD == factoryLocation.LocationUD);
                        if (location != null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Location already in database!";

                            return resultUpdate;
                        }
                        else
                        {
                            location = new Location();
                        }

                        context.Location.Add(location);
                    }
                    else
                    {
                        location = context.Location.FirstOrDefault(o => o.LocationID == id);

                        if (location == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Cannot found data!";

                            return resultUpdate;
                        }
                    }

                    converter.DTO2DB_UpdateData(factoryLocation, ref location);

                    if (id < 0)
                    {
                        location.CreatedBy = userId;
                        location.CreatedDate = DateTime.Now;
                    }

                    location.UpdatedBy = userId;
                    location.UpdatedDate = DateTime.Now;

                    location.LocationUD = factoryLocation.LocationUD;

                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_FactoryLocation(context.FactoryLocationMng_Location_View.FirstOrDefault(o => o.LocationID == location.LocationID));

                    resultUpdate = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return resultUpdate;
        }

        public bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            bool resultDelete = false;

            try
            {
                using (var context = CreateContext())
                {
                    Location location = context.Location.FirstOrDefault(o => o.LocationID == id);

                    if (location == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Cannot found Location data!";

                        return resultDelete;
                    }

                    context.Location.Remove(location);
                    context.SaveChanges();

                    resultDelete = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return resultDelete;
        }

        public FactoryLocationMngEntities CreateContext()
        {
            return new FactoryLocationMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryLocationMngModel"));
        }
    }
}
