using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.ReportFormData GetConclusion(string fromDate, string toDate, string employee, string company, string module, string location, out Library.DTO.Notification notification)
        {
            return factory.GetConclusion(fromDate, toDate, employee, company, module, location, out notification);
        }

        public List<DTO.ActiveBrowser> GetAllBrowserData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            return factory.GetAllBrowserData(fromDate, toDate, employee, company, module, out notification);
        }

        public List<DTO.ActiveCompany> GetAllCompanyData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            return factory.GetAllCompanyData(fromDate, toDate, employee, company, module, out notification);
        }

        public List<DTO.ActiveIP> GetAllIPData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            return factory.GetAllIPData(fromDate, toDate, employee, company, module, out notification);
        }

        public List<DTO.ActiveModule> GetAllModuleData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            return factory.GetAllModuleData(fromDate, toDate, employee, company, module, out notification);
        }

        public List<DTO.ActiveUser> GetAllUserData(string fromDate, string toDate, string employee, string company, string module, out Library.DTO.Notification notification)
        {
            return factory.GetAllUserData(fromDate, toDate, employee, company, module, out notification);
        }

        public DTO.UserDetailData GetUserActionDetail(int userId, string fromDate, string toDate, string module, out Library.DTO.Notification notification)
        {
            return factory.GetUserActionDetail(userId, fromDate, toDate, module, out notification);
        }

        public DTO.CompanyDetailData GetCompanyActionDetail(int companyId, string fromDate, string toDate, string module, out Library.DTO.Notification notification)
        {
            return factory.GetCompanyActionDetail(companyId, fromDate, toDate, module, out notification);
        }

        public List<DTO.ModuleDetail> GetModuleActionDetail(string controllerName, string fromDate, string toDate, string employee, string company, out Library.DTO.Notification notification)
        {
            return factory.GetModuleActionDetail(controllerName, fromDate, toDate, employee, company, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public bool PrepareCacheData(out Library.DTO.Notification notification)
        {
            return factory.PrepareCacheData(out notification);
        }
    }
}
