using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
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
            object data;

            switch (identifier.ToLower())
            {
                //
                // order
                //
                case "getorderdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("clientId"))
                    {
                        if (Convert.ToInt32(input["id"]) > 0)
                        {
                            input["clientId"] = 0;
                        }
                        else
                        {
                            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow client!" };
                            return null;
                        }                        
                    }
                    if (!input.ContainsKey("season"))
                    {
                        if (Convert.ToInt32(input["id"]) > 0)
                        {
                            input["season"] = string.Empty;
                        }
                        else
                        {
                            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season!" };
                            return null;
                        }
                    }
                    return bll.GetOrderData(Convert.ToInt32(input["id"]), Convert.ToInt32(input["clientId"]), input["season"].ToString(), out notification);

                case "getorderoverviewdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetOrderOverviewData(userId, Convert.ToInt32(input["id"]), out notification);

                case "updateorderdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateOrderData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                //
                // product
                //
                case "getproductoverviewdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetProductOverviewData(userId, Convert.ToInt32(input["id"]), out notification);

                //
                // factory assignment
                //
                case "getfactoryassignmentdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetFactoryAssignmentData(Convert.ToInt32(input["id"]), out notification);

                case "updatefactoryassignmentdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateFactoryAssignmentData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                #region INTERNAL REMARK

                case "getinternalremarkdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetInternalRemarkData(Convert.ToInt32(input["id"]), out notification);

                case "updateinternalremarkdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateInternalRemarkData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "deleteinternalremarkdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.DeleteInternalRemarkData(userId, Convert.ToInt32(input["id"]), out notification);

                #endregion

                #region QA REMARK

                case "getqaremarkdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetQARemarkData(Convert.ToInt32(input["id"]), out notification);

                case "updateqaremarkdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateQARemarkData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "deleteqaremarkdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.DeleteQARemarkData(userId, Convert.ToInt32(input["id"]), out notification);

                #endregion

                #region BUILDING PROCESS

                case "getbuildingprocessdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetBuildingProcessData(Convert.ToInt32(input["id"]), out notification);

                case "updatebuildingprocessdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateBuildingProcessData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "deletebuildingprocessdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.DeleteBuildingProcessData(userId, Convert.ToInt32(input["id"]), out notification);

                #endregion

                #region ITEM DATA

                case "getitemdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetItemData(Convert.ToInt32(input["id"]), out notification);

                case "updateitemdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateItemData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                #endregion

                #region PRODUCT INFO

                case "getproductinfodata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetProductInfoData(Convert.ToInt32(input["id"]), out notification);

                case "updateproductinfodata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    data = input["data"];
                    if (bll.UpdateProductInfoData(userId, Convert.ToInt32(input["id"]), ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                #endregion

                //
                // other
                //
                case "getsearchfilter":
                    return bll.GetOrderSearchFilter(out notification);

                case "quicksearchnlmonitor":
                    if (!input.ContainsKey("query"))
                    {
                        input["query"] = string.Empty;
                    }
                    return bll.QuickSearchEurofarUsers(input["query"].ToString(), out notification);

                case "quicksearchvnmonitor":
                    if (!input.ContainsKey("query"))
                    {
                        input["query"] = string.Empty;
                    }
                    return bll.QuickSearchAVTUsers(input["query"].ToString(), out notification);
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
