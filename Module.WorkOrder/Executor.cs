using System;

namespace Module.WorkOrder
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
            throw new NotImplementedException();//return bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim())
            {
                case "GetProduct":
                    return bll.GetProduct(param, out notification);

                case "GetSaleOrder":
                    return bll.GetSaleOrder(param, out notification);

                case "GetFactoryOrderDetail":
                    return bll.GetFactoryOrderDetail(userId, param, out notification);

                case "SetWorkOrderStatus":
                    return bll.SetWorkOrderStatus(userId, param, out notification);

                case "ChangeOPSequence":
                    return bll.ChangeOPSequence(userId, param, out notification);

                case "GetSampleOrder":
                    return bll.GetSampleOrder(userId, param, out notification);

                case "GetPreOrderWorkOrder":
                    return bll.GetPreOrderWorkOrder(Convert.ToString(param["workOrderUD"]), out notification);

                case "GetWorkCenter":
                    return bll.GetWorkCenter(out notification);

                case "GetExcelWorkOrder":
                    return bll.GetProductionItemExportToExcelList(Convert.ToString(param["workOrders"]), Convert.ToString(param["workCenters"]), out notification);

                case "OpenWorkOrderStatus":
                    return bll.OpenWorkOrderStatus(userId, param, out notification);

                case "GetData":
                    return bll.GetData(userId, Convert.ToInt32(param["id"]), Convert.ToInt32(param["branchID"]), out notification);

                case "GetOPSequenceDetails":
                    int? opSequenceID = null;
                    int? preWorkOrderID = null;

                    if (param.ContainsKey("OPSequenceID") && param["OPSequenceID"] != null && !string.IsNullOrEmpty(param["OPSequenceID"].ToString()))
                    {
                        opSequenceID = Convert.ToInt32(param["OPSequenceID"]);
                    }

                    if (param.ContainsKey("PreWorkOrderID") && param["PreWorkOrderID"] != null && !string.IsNullOrEmpty(param["PreWorkOrderID"].ToString()))
                    {
                        preWorkOrderID = Convert.ToInt32(param["PreWorkOrderID"]);
                    }


                    return bll.GetOPSequenceDetails(userId, opSequenceID, preWorkOrderID, out notification);

                case "GetPreOrderWorkOrderManagement":
                    return bll.GetPreOrderWorkOrderManagements(userId, Convert.ToInt32(param["PreOrderWorkOrderID"]), out notification);

                case "GetWorkOrderBaseOnManagement":
                    return bll.GetWorkOrderBaseOnManagements(userId, Convert.ToInt32(param["WorkOrderID"]), Convert.ToInt32(param["PreOrderWorkOrderID"]), out notification);

                case "UpdateTransferPre2Work":
                    return bll.UpdateTransferPre2Work(userId, param, out notification);

                case "GetHistoryTransferPreOrderToWorkOrder":
                    return bll.GetHistoryTransferPreOrderToWorkOrders(userId, Convert.ToInt32(param["WorkOrderID"]), Convert.ToInt32(param["PreOrderWorkOrderID"]), out notification);

                case "GetTransferWorkOrder":
                    return bll.GetTransferWorkOrder(userId, Convert.ToInt32(param["id"]), Convert.ToInt32(param["workOrderID"]), Convert.ToInt32(param["preOrderWorkOrderID"]), out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
