using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            string employee, company, module, location;
            employee = company = module = location = null;

            switch (identifier.ToLower())
            {
                case "getconclusion":
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    if (input.ContainsKey("location") && input["location"] != null)
                    {
                        location = input["location"].ToString();
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    return bll.GetConclusion(input["fromdate"].ToString(), input["todate"].ToString(), employee, company, module, location, out notification);

                case "getbrowser":
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    return bll.GetAllBrowserData(input["fromdate"].ToString(), input["todate"].ToString(), employee, company, module, out notification);

                case "getcompany":
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    return bll.GetAllCompanyData(input["fromdate"].ToString(), input["todate"].ToString(), employee, company, module, out notification);

                case "getip":
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    return bll.GetAllIPData(input["fromdate"].ToString(), input["todate"].ToString(), employee, company, module, out notification);

                case "getmodule":
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    return bll.GetAllModuleData(input["fromdate"].ToString(), input["todate"].ToString(), employee, company, module, out notification);

                case "getuser":
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    return bll.GetAllUserData(input["fromdate"].ToString(), input["todate"].ToString(), employee, company, module, out notification);

                case "getuserdetail":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    return bll.GetUserActionDetail(Convert.ToInt32(input["id"]), input["fromdate"].ToString(), input["todate"].ToString(), module, out notification);

                case "getcompanydetail":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    if (input.ContainsKey("module") && input["module"] != null)
                    {
                        module = input["module"].ToString();
                    }
                    return bll.GetCompanyActionDetail(Convert.ToInt32(input["id"]), input["fromdate"].ToString(), input["todate"].ToString(), module, out notification);

                case "getmoduledetail":
                    if (!input.ContainsKey("controllername") || input["controllername"] == null || string.IsNullOrEmpty(input["controllername"].ToString()))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow controller name" };
                        return null;
                    }
                    if (input["fromdate"] == null)
                    {
                        input["fromdate"] = string.Empty;
                    }
                    if (input["todate"] == null)
                    {
                        input["todate"] = string.Empty;
                    }
                    if (input.ContainsKey("employee") && input["employee"] != null)
                    {
                        employee = input["employee"].ToString();
                    }
                    if (input.ContainsKey("company") && input["company"] != null)
                    {
                        company = input["company"].ToString();
                    }
                    return bll.GetModuleActionDetail(input["controllername"].ToString(), input["fromdate"].ToString(), input["todate"].ToString(), employee, company, out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "preparecachedata":
                    return bll.PrepareCacheData(out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public string identifier
        {
            get
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
            }
        }
        private string _identifier = string.Empty;
    }
}
