using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.SCMAgendaMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private SCMAgendaMngEntities CreateContext()
        {
            return new SCMAgendaMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SCMAgendaMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.SCMAppointmentSearchResultDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (SCMAgendaMngEntities context = CreateContext())
                {
                    string MeetingLocations = null;
                    string Factories = null;
                    int Month = Convert.ToInt32(filters["month"]);
                    int Year = Convert.ToInt32(filters["year"]);
                    if (filters.ContainsKey("locations") && !string.IsNullOrEmpty(filters["locations"].ToString()))
                    {
                        MeetingLocations = string.Empty;
                        //((Newtonsoft.Json.Linq.JArray)MeetingLocations).ToList
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
                    if (filters.ContainsKey("factories") && !string.IsNullOrEmpty(filters["factories"].ToString()))
                    {
                        Factories = string.Empty;
                        //((Newtonsoft.Json.Linq.JArray)MeetingLocations).ToList
                        foreach (int factoryID in Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(filters["factories"].ToString().Replace("'", "''")))
                        {
                            if (string.IsNullOrEmpty(Factories))
                            {
                                Factories += factoryID;
                            }
                            else
                            {
                                Factories += "," + factoryID;
                            }
                        }
                    }
                    data.Data = converter.DB2DTO_SCMAppointmentSearchResultDTOList(context.SCMAgendaMng_function_SearchSCMAppointment(MeetingLocations, Factories, Month, Year).ToList());
                }
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
            data.Data = new DTO.SCMAppointmentDTO();
            data.Data.SCMAppointmentAttachedFileDTOs = new List<DTO.SCMAppointmentAttachedFileDTO>();
            data.employeeDepartmentDTOs = new List<DTO.EmployeeDepartmentDTO>();
            data.Data.SCMAppointmentUserDTOs = new List<DTO.SCMAppointmentUserDTO>();
            //try to get data
            try
            {
                if (id >= 0)
                {
                    using (SCMAgendaMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_SCMAppointmentDTO(context.SCMAgendaMng_SCMAppointment_View.Include("SCMAgendaMng_SCMAppointmentAttachedFile_View").Include("SCMAgendaMng_SCMInspection_View").FirstOrDefault(o => o.SCMAppointmentID == id));
                    }
                }
                data.employeeDepartmentDTOs = GetListEmployee();
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
        public List<DTO.EmployeeDepartmentDTO> GetListEmployee()
        {
            List<DTO.EmployeeDepartmentDTO> result = new List<DTO.EmployeeDepartmentDTO>();
            try
            {
                using (SCMAgendaMngEntities context = this.CreateContext())
                {
                    result = converter.DB2DTO_EmployeeDepartment(context.SCMAgendaMng_Employee_View.ToList());
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
                using (SCMAgendaMngEntities context = CreateContext())
                {
                    SCMAppointment dbItem = context.SCMAppointment.FirstOrDefault(o => o.SCMAppointmentID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Event not found!");
                    }
                    if (dbItem.MeetingLocationID.Value != 5 && dbItem.UserID.Value != userId)
                    {
                        throw new Exception("You can not delete this event, please contact the event owner!");
                    }

                    // delete the event
                    foreach (SCMAppointmentAttachedFile dbFile in dbItem.SCMAppointmentAttachedFile.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbFile.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbFile.FileUD);
                        }
                        dbItem.SCMAppointmentAttachedFile.Remove(dbFile);
                        context.SCMAppointmentAttachedFile.Remove(dbFile);
                    }
                    // delete the user
                    foreach (SCMAppointmentUser dbUser in dbItem.SCMAppointmentUser.ToArray())
                    {
                        dbItem.SCMAppointmentUser.Remove(dbUser);
                        context.SCMAppointmentUser.Remove(dbUser);
                    }
                    context.SCMAppointment.Remove(dbItem);
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
            DTO.SCMAppointmentDTO dtoAppointment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SCMAppointmentDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SCMAgendaMngEntities context = CreateContext())
                {
                    SCMAppointment dbItem = null;
                    if (id <= 0)
                    {
                        dbItem = new SCMAppointment();
                        if (dtoAppointment.MeetingLocationID == 5) // trip to VN
                        {
                            dbItem.UserID = dtoAppointment.UserID;
                        }
                        else
                        {
                            dbItem.UserID = userId;
                        }
                        context.SCMAppointment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SCMAppointment.FirstOrDefault(o => o.SCMAppointmentID == id);
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
                    context.SCMAppointmentUser.Local.Where(o => o.SCMAppointment == null).ToList().ForEach(o => context.SCMAppointmentUser.Remove(o));
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
                            context.Database.ExecuteSqlCommand("SELECT * FROM SCMAppointment WITH (TABLOCKX, HOLDLOCK)");

                            try
                            {
                                dbItem.SCMAppointmentUD = dbItem.SCMAppointmentID.ToString("D10");
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

        public DTO.SupportData GetSupportData(string formName, out Library.DTO.Notification notification)
        {
            DTO.SupportData data = new DTO.SupportData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            data.Factories = new List<Support.DTO.Factory>();
            data.MeetingLocations = new List<DTO.MeetingLocation>();
            data.Users = new List<Support.DTO.User2>();
            data.Sales = new List<DTO.Sale>();
            data.employeeDepartmentDTOs = new List<Support.DTO.EmployeeDepartmentDTO>();
            data.TimeRange = new List<string>();
            data.SCMAppointmentAttachedFileTypes = new List<Support.DTO.AppointmentAttachedFileType>();

            try
            {
                switch (formName)
                {
                    case "calendar":
                        foreach (Support.DTO.MeetingLocation location in supportFactory.GetMeetingLocation())
                        {
                            if (location.MeetingLocationID == 1 || location.MeetingLocationID == 4) //request Mr Luc AVT VIETNAM SHOWROOM AND AT FACTORY
                            {
                                data.MeetingLocations.Add(new DTO.MeetingLocation() { MeetingLocationID = location.MeetingLocationID, MeetingLocationNM = location.MeetingLocationNM, IsSelected = false });
                            }
                        }
                        data.Factories = supportFactory.GetFactory();
                        break;

                    case "editEvent":
                        data.Users = supportFactory.GetUsers2();
                        data.employeeDepartmentDTOs = supportFactory.GetDepartmentDTOs();
                        data.TimeRange = supportFactory.GetTimeRange();
                        data.SCMAppointmentAttachedFileTypes = supportFactory.GetAppointmentAttachedFileType();
                        using (SCMAgendaMngEntities context = CreateContext())
                        {
                            data.Sales = converter.DB2DTO_SaleList(context.SupportMng_Sale_View.Where(o => o.CompanyID.Value != 3).ToList()); // company different than AVT
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }
            return data;
        }

        public DTO.QCReportSearchFormData ListQCReport(string qcReportUD, int? factoryID, string clientUD, string articleCode, string proformaInvoiceNo, out Library.DTO.Notification notification)
        {
            DTO.QCReportSearchFormData data = new DTO.QCReportSearchFormData();
            data.Factories = new List<Support.DTO.Factory>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SCMAgendaMngEntities context = CreateContext())
                {
                    var result = context.SCMAgendaMng_function_QCReport(qcReportUD, factoryID, clientUD, articleCode, proformaInvoiceNo);
                    data.Data = AutoMapper.Mapper.Map<List<SCMAgendaMng_QCReport_View>, List<DTO.QCReportDTO>>(result.ToList());
                }
                data.Factories = supportFactory.GetFactory().ToList();
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }
            return data;
        }

        public List<DTO.ClientSearchResult> QuickSearchClient(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientSearchResult> data = new List<DTO.ClientSearchResult>();

            //try to get data
            try
            {
                using (SCMAgendaMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ClientSearchResultList(context.SCMAgendaMng_function_SearchClient(query).ToList());
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
