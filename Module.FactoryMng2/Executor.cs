using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }
        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            bll = new BLL(identifier);
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getdata":
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["ID"]), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);   

                case "getdetail":
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetDetail(userId, Convert.ToInt32(input["ID"]), out notification);

                case "getfactoryorderturnover":
                    int pageSize = input.ContainsKey("PageSize") && input["PageSize"] != null && !string.IsNullOrEmpty(input["PageSize"].ToString()) ? Convert.ToInt32(input["PageSize"].ToString()) : 0;
                    int pageIndex = input.ContainsKey("PageIndex") && input["PageIndex"] != null && !string.IsNullOrEmpty(input["PageIndex"].ToString()) ? Convert.ToInt32(input["PageIndex"].ToString()) : 0;
                    string orderBy = input.ContainsKey("OrderBy") && input["OrderBy"] != null && !string.IsNullOrEmpty(input["OrderBy"].ToString()) ? input["OrderBy"].ToString() : null;
                    string orderDirection = input.ContainsKey("OrderDirection") && input["OrderDirection"] != null && !string.IsNullOrEmpty(input["OrderDirection"].ToString()) ? input["OrderDirection"].ToString() : null;
                    int totalRows = input.ContainsKey("TotalRows") && input["TotalRows"] != null && !string.IsNullOrEmpty(input["TotalRows"].ToString()) ? Convert.ToInt32(input["TotalRows"].ToString()) : 0;
                    return bll.GetFactoryOrderTurnover(userId, input, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);

                case "exportexcelfactory":
                    return bll.ExportExcelFactory(userId, input, out notification);
                case "getpersonincharge":
                    return bll.GetPersonInCharge(userId, input, out totalRows, out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }
    }
}
