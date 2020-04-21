using Module.PurchasingCalendarMng.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Module.PurchasingCalendarMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<SearchFormData, EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private PurchasingCalendarMngEntities CreateContext()
        {
            return new PurchasingCalendarMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchasingCalendarMngModel"));
        }

        public override SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            SearchFormData data = new SearchFormData();
            data.Data = new List<PurchasingCalendarAppointmentSearchResultDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (PurchasingCalendarMngEntities context = CreateContext())
                {
                    string MeetingLocations = null;                  
                    int Month = Convert.ToInt32(filters["month"]);
                    int Year = Convert.ToInt32(filters["year"]);
                    if (filters.ContainsKey("locations") && !string.IsNullOrEmpty(filters["locations"].ToString()))
                    {
                        MeetingLocations = string.Empty;                       
                        foreach (int locationID in Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(filters["locations"].ToString().Replace("'", "''")))
                        {
                            if (string.IsNullOrEmpty(MeetingLocations))
                            {
                                MeetingLocations += locationID.ToString();
                            }
                            else
                            {
                                MeetingLocations += "," + locationID.ToString(); ;
                            }
                        }
                    }
                    
                    data.Data = converter.DB2DTO_AppointmentSearchResultDTOList(context.PurchasingCalendarMng_function_SearchPurchasingCalendarAppointment(MeetingLocations, Month, Year).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public override EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            EditFormData data = new EditFormData();
            data.Data = new PurchasingCalendarAppointmentDTO();
            data.Data.PurchasingCalendarAppointmentAttachedFileDTOs = new List<PurchasingCalendarAppointmentAttachedFileDTO>();
            data.EmployeeDepartmentDTOs = new List<EmployeeDepartmentDTO>();
            data.Data.PurchasingCalendarUserDTOs = new List<PurchasingCalendarUserDTO>();
            //try to get data
            try
            {
                if (id >= 0)
                {
                    using (PurchasingCalendarMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_AppointmentDTO(context.PurchasingCalendarMng_PurchasingCalendarAppointment_View.Include("PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View").FirstOrDefault(o => o.PurchasingCalendarAppointmentID == id));
                    }
                }
                data.EmployeeDepartmentDTOs = GetListEmployee();
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
            throw new NotImplementedException();
        }
        public List<EmployeeDepartmentDTO> GetListEmployee()
        {
            List<EmployeeDepartmentDTO> result = new List<EmployeeDepartmentDTO>();
            try
            {
                using (PurchasingCalendarMngEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_EmployeeDepartment(context.PurchasingCalendarMng_Employee_View.ToList());
                }
            }
            catch { }

            return result;
        }
        public bool DeleteAppointment(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchasingCalendarMngEntities context = CreateContext())
                {
                    PurchasingCalendarAppointment dbItem = context.PurchasingCalendarAppointment.FirstOrDefault(o => o.PurchasingCalendarAppointmentID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Event not found!");
                    }
                    if (dbItem.MeetingLocationID.Value != 5 && dbItem.UserID.Value != userId)
                    {
                        throw new Exception("You can not delete this event, please contact the event owner!");
                    }

                    // delete the event
                    foreach (PurchasingCalendarAppointmentAttachedFile dbFile in dbItem.PurchasingCalendarAppointmentAttachedFile.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbFile.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbFile.FileUD);
                        }
                        dbItem.PurchasingCalendarAppointmentAttachedFile.Remove(dbFile);
                        context.PurchasingCalendarAppointmentAttachedFile.Remove(dbFile);
                    }
                    context.PurchasingCalendarAppointment.Remove(dbItem);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            PurchasingCalendarAppointmentDTO dtoAppointment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<PurchasingCalendarAppointmentDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PurchasingCalendarMngEntities context = CreateContext())
                {
                    PurchasingCalendarAppointment dbItem = null;
                    if (id <= 0)
                    {
                        dbItem = new PurchasingCalendarAppointment();
                        if (dtoAppointment.MeetingLocationID == 5) // trip to VN
                        {
                            dbItem.UserID = dtoAppointment.UserID;
                        }
                        else
                        {
                            dbItem.UserID = userId;
                        }
                        context.PurchasingCalendarAppointment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PurchasingCalendarAppointment.FirstOrDefault(o => o.PurchasingCalendarAppointmentID == id);
                        if (dbItem == null)
                        {
                            throw new Exception("Event not found!");
                        }
                        if (dtoAppointment.MeetingLocationID == 5) // trip to VN
                        {
                            dbItem.UserID = dtoAppointment.UserID;
                        }
                        //if (dbItem.UserID.Value != userId)
                        //{
                        //    throw new Exception("You can not delete this event, please contact the event owner!");
                        //}
                    }
                    if (dtoAppointment.MeetingLocationID == 5) // trip to VN
                    {
                        dtoAppointment.StartTime = "00:00";
                        dtoAppointment.EndTime = "23:59";
                    }

                    converter.DTO2DB(dtoAppointment, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                    context.PurchasingCalendarUser.Local.Where(o => o.PurchasingCalendarAppointment == null).ToList().ForEach(o => context.PurchasingCalendarUser.Remove(o));
                    // updated by, updated date, meeting minute.
                    dbItem.MeetingMinute = dtoAppointment.MeetingMinute;
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    // generate agenda code
                    if (id <= 0)
                    {
                        using (DbContextTransaction scope = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlCommand("SELECT * FROM Appointment WITH (TABLOCKX, HOLDLOCK)");

                            try
                            {
                                dbItem.PurchasingCalendarAppointmentUD = dbItem.PurchasingCalendarAppointmentID.ToString("D10");
                                context.SaveChanges();
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
                    }
                    else
                    {
                        context.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
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

        public SupportFormData GetSupportData(string formName, out Library.DTO.Notification notification)
        {
            SupportFormData data = new SupportFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
         
            data.MeetingLocationDTOs = new List<MeetingLocationDTO>();
            data.Users = new List<Support.DTO.User2>();
            data.Sales = new List<SaleDTO>();
            data.EmployeeDepartmentDTOs = new List<EmployeeDepartmentDTO>();
            data.TimeRange = new List<string>();
            data.AppointmentAttachedFileTypes = new List<Support.DTO.AppointmentAttachedFileType>();

            try
            {
                using (PurchasingCalendarMngEntities context = CreateContext())
                {
                    switch (formName)
                    {
                        case "calendar":
                            foreach (PurchasingCalendarMng_MeetingLocation_View location in context.PurchasingCalendarMng_MeetingLocation_View.ToList())
                            {
                                data.MeetingLocationDTOs.Add(new MeetingLocationDTO() { MeetingLocationID = location.MeetingLocationID.Value, MeetingLocationNM = location.MeetingLocationNM, IsSelected = false });
                            }                         
                            break;
                        case "editEvent":
                            data.Users = supportFactory.GetUsers2();
                            data.EmployeeDepartmentDTOs = converter.DB2DTO_EmployeeDepartment(context.PurchasingCalendarMng_Employee_View.ToList());
                            data.TimeRange = supportFactory.GetTimeRange();
                            data.AppointmentAttachedFileTypes = supportFactory.GetAppointmentAttachedFileType();
                            data.Sales = converter.DB2DTO_SaleList(context.PurchasingCalendarMng_Sale_View.Where(o => o.CompanyID.Value != 3).ToList()); // company different than AVT
                            break;

                        default:
                            break;
                    }
                }                
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }
            return data;
        }


        public List<FactoryRawMaterialSearchResultDTO> QuickSearchFactoryRawMaterial(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<FactoryRawMaterialSearchResultDTO> data = new List<FactoryRawMaterialSearchResultDTO>();

            //try to get data
            try
            {
                using (PurchasingCalendarMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ClientSearchResultList(context.PurchasingCalendarMng_function_SearchFactoryRawMaterial(query).ToList());
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
