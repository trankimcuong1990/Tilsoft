using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Module.LeaveRequestMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private LeaveRequestMngEntities CreateContext()
        {
            return new LeaveRequestMngEntities(Library.Helper.CreateEntityConnectionString("DAL.LeaveRequestMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.LeaveRequestSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (LeaveRequestMngEntities context = CreateContext())
                {
                    string RequestorUD = null;
                    string RequestorNM = null;
                    bool? IsApproved = null;
                    bool? IsDenied = null;
                    bool? HasLeft = false;

                    if (filters.ContainsKey("RequestorUD") && !string.IsNullOrEmpty(filters["RequestorUD"].ToString()))
                    {
                        RequestorUD = filters["LeaveRequestUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("RequestorNM") && !string.IsNullOrEmpty(filters["RequestorNM"].ToString()))
                    {
                        RequestorNM = filters["RequestorNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsApproved") && !string.IsNullOrEmpty(filters["IsApproved"].ToString()))
                    {
                        IsApproved = Convert.ToBoolean(filters["IsApproved"].ToString());
                    }
                    if (filters.ContainsKey("IsDenied") && !string.IsNullOrEmpty(filters["IsDenied"].ToString()))
                    {
                        IsDenied = Convert.ToBoolean(filters["IsDenied"].ToString());
                    }
                    if (filters.ContainsKey("HasLeft") && filters["HasLeft"] != null && !string.IsNullOrEmpty(filters["HasLeft"].ToString()))
                    {
                        HasLeft = (filters["HasLeft"].ToString() == "true") ? true : false;
                    }

                    totalRows = context.LeaveRequestMng_function_SearchLeaveRequest(RequestorUD, RequestorNM, IsApproved, IsDenied, HasLeft, orderBy, orderDirection).Count();
                    var result = context.LeaveRequestMng_function_SearchLeaveRequest(RequestorUD, RequestorNM, IsApproved, IsDenied, HasLeft, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_LeaveRequestSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.LeaveRequest();
            data.LeaveTypes = new List<Module.Support.DTO.LeaveType>();
            data.LeaveRequestTimes = new List<Module.Support.DTO.LeaveRequestTime>();
            data.Managers = new List<DTO.Manager>();
            data.Directors = new List<DTO.Director>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (LeaveRequestMngEntities context = CreateContext())
                    {
                        var LeaveRequest = context.LeaveRequestMng_LeaveRequest_View.FirstOrDefault(o => o.LeaveRequestID == id);
                        if (LeaveRequest == null)
                        {
                            throw new Exception("Can not found the LeaveRequest to edit");
                        }
                        data.Data = converter.DB2DTO_LeaveRequest(LeaveRequest);
                    }
                }
                data.LeaveTypes = supportFactory.GetLeaveType().ToList();
                data.LeaveRequestTimes = supportFactory.GetLeaveRequestTime().ToList();
                data.Managers = GetListManager().ToList();
                data.Directors = GetListDirector().ToList();
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
                using (LeaveRequestMngEntities context = CreateContext())
                {
                    LeaveRequest dbItem = context.LeaveRequest.FirstOrDefault(o => o.LeaveRequestID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "LeaveRequest not found!";
                        return false;
                    }
                    else
                    {
                        context.LeaveRequest.Remove(dbItem);
                        context.SaveChanges();

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
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.LeaveRequest dtoLeaveRequest = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LeaveRequest>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LeaveRequestMngEntities context = CreateContext())
                {
                    LeaveRequest dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new LeaveRequest();
                        context.LeaveRequest.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.LeaveRequest.FirstOrDefault(o => o.LeaveRequestID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "LeaveRequest not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoLeaveRequest.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoLeaveRequest, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();
                        dtoItem = GetData(dbItem.LeaveRequestID, out notification).Data;

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

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //int i = 0;
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.YesNoValues = new List<Module.Support.DTO.YesNo>();

            //try to get data
            try
            {
                data.YesNoValues = supportFactory.GetYesNo().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public List<DTO.Manager> GetListManager()
        {
            List<DTO.Manager> result = new List<DTO.Manager>();
            using (LeaveRequestMngEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_Manager(context.LeaveRequestMng_Manager_View.ToList());
            }
            return result;
        }
        public List<DTO.Director> GetListDirector()
        {
            List<DTO.Director> result = new List<DTO.Director>();
            using (LeaveRequestMngEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_Director(context.LeaveRequestMng_Director_View.ToList());
            }
            return result;
        }
    }
}
