using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.GetData(userId, id, out notification);
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
            int id;
            object data;

            switch (identifier.ToLower())
            {
                case "getdatadetail":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetDataDetail(userId, Convert.ToInt32(input["id"]), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "updateorderstatus":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("statusId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow status id" };
                        return null;
                    }
                    return bll.UpdateOrderStatus(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["statusId"]), out notification);

                case "updateproductstatus":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("statusId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow status id" };
                        return null;
                    }
                    return bll.UpdateProductStatus(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["statusId"]), out notification);

                case "updateproductstatus2":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("statusId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow status id" };
                        return null;
                    }
                    if (!input.ContainsKey("file"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow file" };
                        return null;
                    }
                    return bll.UpdateProductStatus2(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["statusId"]), input["file"].ToString(), out notification);

                case "updateprogress":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.UpdateProgressData(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "deleteprogress":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.DeleteProgress(userId, Convert.ToInt32(input["id"]), out notification);

                case "updatetechnicaldrawing":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.UpdateTechnicalDrawingData(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "approvetechnicaldrawing":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.ApproveTechnicalDrawingData(userId, Convert.ToInt32(input["id"]), out notification);

                case "resettechnicaldrawing":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.ResetTechnicalDrawingData(userId, Convert.ToInt32(input["id"]), out notification);

                case "deletetechnicaldrawing":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.DeleteTechnicalDrawing(userId, Convert.ToInt32(input["id"]), out notification);

                case "updateremark":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.UpdateRemarkData(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "deleteremark":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.DeleteRemark(userId, Convert.ToInt32(input["id"]), out notification);

                case "updateqaremark":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.UpdateQARemarkData(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "deleteqaremark":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.DeleteQARemark(userId, Convert.ToInt32(input["id"]), out notification);

                case "updateitem":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.UpdateItemInfo(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "getmodellist":
                    if (!input.ContainsKey("searchQuery") || input["searchQuery"] == null)
                    {
                        notification = new Notification();
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow search query";

                        return null;
                    }

                    return bll.GetModelList(userId, input["searchQuery"].ToString(), out notification);

                case "getdetailfactorybreakdown":
                    return bll.GetDataDetailFactoryBreakdown(userId, Convert.ToInt32(input["sampleProductID"]), out notification);

                case "exportexcelsampleorder":

                    string sampleOrderUD = input["sampleOrderUD"] == null ? null : input["sampleOrderUD"].ToString();
                    string season = input["season"] == null ? null : input["season"].ToString();
                    string clientUD = input["clientUD"] == null ? null : input["clientUD"].ToString();
                    string clientNM = input["clientNM"] == null ? null : input["clientNM"].ToString();
                    int? purposeID = input["purposeID"] == null ? (int?)null : Convert.ToInt32(input["purposeID"].ToString());
                    int? transportTypeID = input["transportTypeID"] == null ? (int?)null : Convert.ToInt32(input["transportTypeID"].ToString());
                    int? sampleOrderStatusID = input["sampleOrderStatusID"] == null ? (int?)null : Convert.ToInt32(input["sampleOrderStatusID"].ToString());
                    string sampleItemCode = input["sampleItemCode"] == null ? null : input["sampleItemCode"].ToString();
                    string sampleItemName = input["sampleItemName"] == null ? null : input["sampleItemName"].ToString();
                    int? factoryID = input["sampleItemName"] == null ? (int?)null : Convert.ToInt32(input["factoryID"].ToString());
                    int? accountManager = input["accountManager"] == null ? (int?)null : Convert.ToInt32(input["accountManager"].ToString());

                    return bll.ExportExcelSampleOrder(userId, sampleOrderUD, season, clientUD, clientNM, purposeID, transportTypeID, sampleOrderStatusID, factoryID, sampleItemCode, sampleItemName, accountManager, out notification);
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
