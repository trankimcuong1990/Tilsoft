using Library.Base;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.InventoryRpt
{
    public class Executor : IExecutor
    {
        // Variables
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification();

            int? branchID = null;

            switch (identifier.ToLower())
            {
                case "exportinventoryreport":
                    if (!input.ContainsKey("factoryWarehouseID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow factory warehouse";

                        return null;
                    }

                    if (!input.ContainsKey("productionTeamID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow production team";

                        return null;
                    }

                    if (!input.ContainsKey("startDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow start date";

                        return null;
                    }

                    if (!input.ContainsKey("endDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow end date";

                        return null;
                    }

                    return bll.ExportInventoryReport(userId, Convert.ToInt32(input["factoryWarehouseID"].ToString()), Convert.ToInt32(input["productionTeamID"].ToString()), input["startDate"].ToString().Trim(), input["endDate"].ToString().Trim(), out notification);

                case "getdatawithfilters":
                    if (!input.ContainsKey("factoryWarehouseID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow factory warehouse";

                        return null;
                    }

                    if (!input.ContainsKey("productionTeamID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow production team";

                        return null;
                    }

                    if (!input.ContainsKey("startDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow start date";

                        return null;
                    }

                    if (!input.ContainsKey("endDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow end date";

                        return null;
                    }

                    return bll.GetDataWithFilters(userId, Convert.ToInt32(input["factoryWarehouseID"].ToString()), Convert.ToInt32(input["productionTeamID"].ToString()), input["startDate"].ToString().Trim(), input["endDate"].ToString().Trim(), out notification);

                case "exportinventoryreportdetail":
                    if (!input.ContainsKey("factoryWarehouseID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow factory warehouse";

                        return null;
                    }

                    if (!input.ContainsKey("productionTeamID"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow production team";

                        return null;
                    }

                    if (!input.ContainsKey("startDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow start date";

                        return null;
                    }

                    if (!input.ContainsKey("endDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow end date";

                        return null;
                    }

                    return bll.ExportInventoryReportDetail(userId, Convert.ToInt32(input["factoryWarehouseID"].ToString()), Convert.ToInt32(input["productionTeamID"].ToString()), input["startDate"].ToString().Trim(), input["endDate"].ToString().Trim(), out notification);

                case "getinitcustom":
                    if (input.ContainsKey("BranchID") && input["BranchID"] != null && !string.IsNullOrEmpty(input["BranchID"].ToString()))
                    {
                        branchID = (int?)Convert.ToInt32(input["BranchID"].ToString());
                    }

                    return bll.GetInitData(userId, branchID, out notification);

                case "customgetdatafilter":
                    return bll.CustomGetDataWithFilter(userId, input, out notification);

                
                case "customexportdatafilter":
                    return bll.CustomExportDataWithFilter(userId, input, out notification);
            }
            
            notification.Type = NotificationType.Error;
            notification.Message = "Custom function's identifier not matched";

            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
