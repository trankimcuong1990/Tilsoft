using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private DateTime tmpDate;
        private int? DefaultNumberOfItem = null;

        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private UsageTrackingRptEntities CreateContext()
        {
            return new UsageTrackingRptEntities(Library.Helper.CreateEntityConnectionString("DAL.UsageTrackingRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
        public DTO.ReportFormData GetConclusion(string fromDate, string toDate, string employee, string company, string module, string location, out Library.DTO.Notification notification)
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    fromDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fromDate = null;
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    toDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                toDate = null;
            }
            DTO.ReportFormData data = new DTO.ReportFormData();
            data.ActiveBrowsers = new List<DTO.ActiveBrowser>();
            data.ActiveCompanies = new List<DTO.ActiveCompany>();
            data.ActiveCompanyWeeklyHits = new List<DTO.ActiveCompanyWeeklyHit>();
            data.ActiveIPs = new List<DTO.ActiveIP>();
            data.ActiveModules = new List<DTO.ActiveModule>();
            data.ActiveUsers = new List<DTO.ActiveUser>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {                
                Task task1 = Task.Factory.StartNew(() =>
                {
                    using (UsageTrackingRptEntities context = CreateContext())
                    {
                        data.ActiveCompanies = converter.DB2DTO_ActiveCompanyList(context.UsageTrackingRpt_function_getTop5ActiveCompany(fromDate, toDate, DefaultNumberOfItem, employee, location, company, module).ToList());
                        data.ActiveCompanyWeeklyHits = converter.DB2DTO_ActiveCompanyWeeklyHitList(context.UsageTrackingRpt_function_getActiveCompanyWeeklyHit(fromDate, toDate, DefaultNumberOfItem, employee, location, company, module).ToList());
                    }
                });               
                Task task2 = Task.Factory.StartNew(() =>
                {
                    using (UsageTrackingRptEntities context = CreateContext())
                    {
                        data.ActiveModules = converter.DB2DTO_ActiveModuleList(context.UsageTrackingRpt_function_getTop5ActiveModule(fromDate, toDate, DefaultNumberOfItem, employee, location, company, module).ToList());
                    }
                });
                Task task3 = Task.Factory.StartNew(() =>
                {
                    using (UsageTrackingRptEntities context = CreateContext())
                    {
                        data.ActiveUsers = converter.DB2DTO_ActiveUserList(context.UsageTrackingRpt_function_getTop5ActiveUser(fromDate, toDate, DefaultNumberOfItem, employee, location, company, module).ToList());
                    }
                });
                Task task4 = Task.Factory.StartNew(() =>
                {
                    using (UsageTrackingRptEntities context = CreateContext())
                    {
                        data.HitCounts = converter.DB2DTO_HitCountList(context.UsageTrackingRpt_HitCountByWeek_View.Where(o => o.WeekStart.Value.Year == DateTime.Now.Year).ToList());
                    }
                });
                Task task5 = Task.Factory.StartNew(() =>
                {
                    using (UsageTrackingRptEntities context = CreateContext())
                    {
                        data.UserMutations = converter.DB2DTO_UserMutationList(context.UsageTrackingRpt_UserIncreasingByWeek_View.Where(o => o.WeekStart.Value.Year == DateTime.Now.Year).ToList());

                        // create cumulative                        
                        int totalUserCum = 0;
                        int? preTotalUser = context.UsageTrackingRpt_UserIncreasingByWeek_View.Where(o => o.WeekStart.Value.Year < DateTime.Now.Year).Sum(o => o.TotalUser);
                        if (preTotalUser.HasValue)
                        {
                            totalUserCum = preTotalUser.Value;
                        }

                        foreach (DTO.UserMutation dtoUserMutation in data.UserMutations)
                        {
                            dtoUserMutation.TotalUser += totalUserCum;
                            totalUserCum = dtoUserMutation.TotalUser;
                        }
                    }
                });
                //Task task1 = Task.Factory.StartNew(() =>
                //{
                //    using (UsageTrackingRptEntities context = CreateContext())
                //    {
                //        data.ActiveBrowsers = converter.DB2DTO_ActiveBrowserList(context.UsageTrackingRpt_function_getTop5ActiveBrowser(fromDate, toDate, DefaultNumberOfItem, employee, company, module).ToList());
                //    }
                //});
                //Task task3 = Task.Factory.StartNew(() =>
                //{
                //    using (UsageTrackingRptEntities context = CreateContext())
                //    {
                //        data.ActiveIPs = converter.DB2DTO_ActiveIPList(context.UsageTrackingRpt_function_getTop5ActiveIPAddress(fromDate, toDate, DefaultNumberOfItem, employee, company, module).ToList());
                //    }
                //});
                Task.WaitAll(task1, task2, task3, task4, task5);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;        
        }

        public List<DTO.ActiveBrowser> GetAllBrowserData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    fromDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fromDate = null;
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    toDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                toDate = null;
            }
            List<DTO.ActiveBrowser> data = new List<DTO.ActiveBrowser>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ActiveBrowserList(context.UsageTrackingRpt_function_getTop5ActiveBrowser(fromDate, toDate, null, employee, company, module).ToList());                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.ActiveCompany> GetAllCompanyData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            //if (!string.IsNullOrEmpty(fromDate))
            //{
            //    if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //        fromDate = tmpDate.ToString("yyyy-MM-dd");
            //    }
            //}
            //else
            //{
            //    fromDate = null;
            //}
            //if (!string.IsNullOrEmpty(toDate))
            //{
            //    if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //        toDate = tmpDate.ToString("yyyy-MM-dd");
            //    }
            //}
            //else
            //{
            //    toDate = null;
            //}
            //List<DTO.ActiveCompany> data = new List<DTO.ActiveCompany>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            //try
            //{
            //    using (UsageTrackingRptEntities context = CreateContext())
            //    {
            //        data = converter.DB2DTO_ActiveCompanyList(context.UsageTrackingRpt_function_getTop5ActiveCompany(fromDate, toDate, null, employee, location, company, module).ToList());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
            throw new NotImplementedException();
        }

        public DTO.CompanyDetailData GetCompanyActionDetail(int companyId, string fromDate, string toDate, string module, out Library.DTO.Notification notification)
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    fromDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fromDate = null;
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    toDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                toDate = null;
            }

            DTO.CompanyDetailData data = new DTO.CompanyDetailData();
            data.CompanyDetails = new List<DTO.CompanyDetail>();
            data.CompanyDetailWeeklyDetails = new List<DTO.CompanyDetailWeeklyDetail>();
            data.CompanyDetailWeeklyOverviews = new List<DTO.CompanyDetailWeeklyOverview>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    data.CompanyDetails = converter.DB2DTO_CompanyDetailList(context.UsageTrackingRpt_function_getCompanyActionDetail(companyId, fromDate, toDate, module).ToList());
                    data.CompanyDetailWeeklyDetails = converter.DB2DTO_CompanyDetailWeeklyDetailList(context.UsageTrackingRpt_function_getCompanyActionDetailWeeklyDetail(companyId, fromDate, toDate, module).ToList());
                    data.CompanyDetailWeeklyOverviews = converter.DB2DTO_CompanyDetailWeeklyOverviewList(context.UsageTrackingRpt_function_getCompanyActionDetailWeeklyOverview(companyId, fromDate, toDate, module).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.ActiveIP> GetAllIPData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    fromDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fromDate = null;
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    toDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                toDate = null;
            }
            List<DTO.ActiveIP> data = new List<DTO.ActiveIP>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ActiveIPList(context.UsageTrackingRpt_function_getTop5ActiveIPAddress(fromDate, toDate, null, employee, company, module).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.ActiveModule> GetAllModuleData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //if (!string.IsNullOrEmpty(fromDate))
            //{
            //    if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //        fromDate = tmpDate.ToString("yyyy-MM-dd");
            //    }
            //}
            //else
            //{
            //    fromDate = null;
            //}
            //if (!string.IsNullOrEmpty(toDate))
            //{
            //    if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //        toDate = tmpDate.ToString("yyyy-MM-dd");
            //    }
            //}
            //else
            //{
            //    toDate = null;
            //}
            //List<DTO.ActiveModule> data = new List<DTO.ActiveModule>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            //try
            //{
            //    using (UsageTrackingRptEntities context = CreateContext())
            //    {
            //        data = converter.DB2DTO_ActiveModuleList(context.UsageTrackingRpt_function_getTop5ActiveModule(fromDate, toDate, null, employee, company, module).ToList());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
        }

        public List<DTO.ActiveUser> GetAllUserData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //if (!string.IsNullOrEmpty(fromDate))
            //{
            //    if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //        fromDate = tmpDate.ToString("yyyy-MM-dd");
            //    }
            //}
            //else
            //{
            //    fromDate = null;
            //}
            //if (!string.IsNullOrEmpty(toDate))
            //{
            //    if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            //    {
            //        toDate = tmpDate.ToString("yyyy-MM-dd");
            //    }
            //}
            //else
            //{
            //    toDate = null;
            //}
            //List<DTO.ActiveUser> data = new List<DTO.ActiveUser>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            //try
            //{
            //    using (UsageTrackingRptEntities context = CreateContext())
            //    {
            //        data = converter.DB2DTO_ActiveUserList(context.UsageTrackingRpt_function_getTop5ActiveUser(fromDate, toDate, null, employee, company, module).ToList());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;  
        }

        public DTO.UserDetailData GetUserActionDetail(int userId, string fromDate, string toDate, string module, out Library.DTO.Notification notification)
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    fromDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fromDate = null;
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    toDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                toDate = null;
            }

            DTO.UserDetailData data = new DTO.UserDetailData();
            data.UserDetails = new List<DTO.UserDetail>();
            data.UserDetailWeeklyDetails = new List<DTO.UserDetailWeeklyDetail>();
            data.UserDetailWeeklyOverviews = new List<DTO.UserDetailWeeklyOverview>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    data.UserDetails = converter.DB2DTO_UserDetailList(context.UsageTrackingRpt_function_getUserActionDetail(userId, fromDate, toDate, module).ToList());
                    data.UserDetailWeeklyDetails = converter.DB2DTO_UserDetailWeeklyDetailList(context.UsageTrackingRpt_function_getUserActionDetailWeeklyDetail(userId, fromDate, toDate, module).ToList());
                    data.UserDetailWeeklyOverviews = converter.DB2DTO_UserDetailWeeklyOverviewList(context.UsageTrackingRpt_function_getUserActionDetailWeeklyOverview(userId, fromDate, toDate, module).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.ModuleDetail> GetModuleActionDetail(string controllerName, string fromDate, string toDate, string employee, string company, out Library.DTO.Notification notification)
        {
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    fromDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fromDate = null;
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    toDate = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                toDate = null;
            }
            List<DTO.ModuleDetail> data = new List<DTO.ModuleDetail>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = string.Empty };
            try
            {
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ModuleDetailList(context.UsageTrackingRpt_function_getModuleActionDetail(controllerName, fromDate, toDate, employee, company).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Employees = new List<Support.DTO.Employee>();
            data.InternalCompanies = new List<Support.DTO.InternalCompany>();
            data.Modules = new List<DTO.Module>();
            data.Locations = new List<Support.DTO.Location>();

            //try to get data
            try
            {
                data.Employees = supportFactory.GetEmployee().ToList();
                data.InternalCompanies = supportFactory.GetInternalCompany().ToList();
                data.Locations = supportFactory.GetLocation().ToList();
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    data.Modules = converter.DB2DTO_ModuleList(context.UsageTrackingMng_Module_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool PrepareCacheData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success};
            try
            {
                using (UsageTrackingRptEntities context = CreateContext())
                {
                    context.UsageTrackingRpt_function_prepareCacheData();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = ex;
                while (iEx.InnerException != null)
                {
                    iEx = iEx.InnerException;
                }
                notification.Message = iEx.Message;
            }
            return false;
        }
    }
}
