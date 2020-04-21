using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AppointmentMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private AppointmentMngEntities CreateContext()
        {
            return new AppointmentMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AppointmentMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.AppointmentDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (AppointmentMngEntities context = CreateContext())
                {
                    var result = context.AppointmentMng_Appointment_View.ToList();
                    totalRows = result.Count();
                    data.Data = converter.DB2DTO_AppointmentList(result);
                }

                data.MeetingLocations = supportFactory.GetMeetingLocation();
                data.TimeRange = supportFactory.GetTimeRange();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.AppointmentDTO();

            //try to get data
            try
            {
                using (AppointmentMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_Appointment(context.AppointmentMng_Appointment_View.FirstOrDefault(o => o.AppointmentID == id));
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
                using (AppointmentMngEntities context = CreateContext())
                {
                    Appointment dbItem = context.Appointment.FirstOrDefault(o => o.AppointmentID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Appointment not found!");
                    }
                    context.Appointment.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.AppointmentDTO dtoAppointment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AppointmentDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AppointmentMngEntities context = CreateContext())
                {
                    Appointment dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Appointment();
                        context.Appointment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Appointment.FirstOrDefault(o => o.AppointmentID == id);
                    }

                    if (dbItem == null)
                    {
                        throw new Exception("Appointment not found!");
                    }

                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM Appointment WITH (TABLOCKX, HOLDLOCK)");

                        try
                        {
                            converter.DTO2DB(dtoAppointment, ref dbItem);
                            dbItem.UserID = userId;
                            context.SaveChanges();
                            if (id == 0)
                            {
                                dbItem.AppointmentUD = Library.Common.Helper.formatIndex(dbItem.AppointmentID.ToString(), 10, "0");
                                context.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }
                    }

                    dtoItem = GetData(dbItem.AppointmentID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public List<DTO.ClientSearchResult> QuickSearchClient(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientSearchResult> data = new List<DTO.ClientSearchResult>();

            //try to get data
            try
            {
                using (AppointmentMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ClientSearchResultList(context.AppointmentMng_function_SearchClient(query).ToList());
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
