using Library;
using Library.DTO;
using Module.TransportationServiceMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportationServiceMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private TransportationServiceMngEntities CreateContext()
        {
            return new TransportationServiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.TransportationServiceMngModel"));
        }

        public DTO.SearchFormData GetDataWithFilters(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.TransportationServiceSearchResultData>();
            totalRows = 0;

            string TransportationServiceNM = null;
            string PlateNumber = null;
            string DriverName = null;
            string MobileNumber = null;
            if (filters.ContainsKey("TransportationServiceNM") && !string.IsNullOrEmpty(filters["TransportationServiceNM"].ToString()))
            {
                TransportationServiceNM = filters["TransportationServiceNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PlateNumber") && !string.IsNullOrEmpty(filters["PlateNumber"].ToString()))
            {
                PlateNumber = filters["PlateNumber"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("DriverName") && !string.IsNullOrEmpty(filters["DriverName"].ToString()))
            {
                DriverName = filters["DriverName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("MobileNumber") && !string.IsNullOrEmpty(filters["MobileNumber"].ToString()))
            {
                MobileNumber = filters["MobileNumber"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (TransportationServiceMngEntities context = CreateContext())
                {
                    totalRows = context.TransportationServiceMng_function_SearchTransportationService(TransportationServiceNM, PlateNumber, DriverName, MobileNumber, orderBy, orderDirection).Count();
                    var result = context.TransportationServiceMng_function_SearchTransportationService(TransportationServiceNM, PlateNumber, DriverName, MobileNumber, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_TransportationServiceResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetEditData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.TransportationServiceData();

            //try to get data
            try
            {
                using (TransportationServiceMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_TransportationService(context.TransportationServiceMng_TransportationService_View.FirstOrDefault(o => o.TransportationServiceID == id));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool Delete(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (TransportationServiceMngEntities context = CreateContext())
                {
                    TransportationService dbItem = context.TransportationService.FirstOrDefault(o => o.TransportationServiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Transportation Service not found!";
                        return false;
                    }
                    else
                    {
                        context.TransportationService.Remove(dbItem);
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

        public bool Update(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            DTO.TransportationServiceData dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.TransportationServiceData>();
            
            try
            {
                using (var context = CreateContext())
                {
                    TransportationService transportationService = null;

                    if (id == 0)
                    {
                        transportationService = new TransportationService();

                        context.TransportationService.Add(transportationService);
                    }
                    else
                    {
                        transportationService = context.TransportationService.FirstOrDefault(o => o.TransportationServiceID == id);
                    }

                    if (transportationService == null)
                    {
                        notification.Message = "Transportation Service not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_TransportationService(dtoItems, ref transportationService);
                        transportationService.UpdatedDate = DateTime.Now;
                        transportationService.UpdatedBy = iRequesterID;
                        context.SaveChanges();

                        dtoItem = GetEditData(transportationService.TransportationServiceID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
