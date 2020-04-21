namespace Module.InventoryCostRpt
{
    using Library.DTO;
    using System;
    using System.Collections;

    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            int? factoryWarehouseID = (input.ContainsKey("factoryWarehouseID") && input["factoryWarehouseID"] != null && !string.IsNullOrEmpty(input["factoryWarehouseID"].ToString()) ? (int?)Convert.ToInt32(input["factoryWarehouseID"]) : null);
            int? productionItemTypeID = (input.ContainsKey("productionItemTypeID") && input["productionItemTypeID"] != null && !string.IsNullOrEmpty(input["productionItemTypeID"].ToString()) ? (int?)Convert.ToInt32(input["productionItemTypeID"]) : null);

            switch (identifier.Trim())
            {
                case "getdatawithfilters":
                    if (!input.ContainsKey("startDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow start date";

                        return null;
                    }

                    return bll.GetDataWithFilters(userId, factoryWarehouseID, productionItemTypeID, input["startDate"].ToString().Trim(), out notification);
                case "getexcelreport":
                    return bll.GetExcelReportData(userId, factoryWarehouseID, productionItemTypeID, input["startDate"].ToString().Trim(), out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
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
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
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
