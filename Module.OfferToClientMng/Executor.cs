using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferToClientMng
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
            return bll.DeleteOffer(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {            
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {           
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {                
                case "getofferseason":
                    if (!input.ContainsKey("clientID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow clientID" };
                        return null;
                    }
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }                    

                    return bll.GetDataByClient(Convert.ToInt32(input["clientID"].ToString()),
                        input["season"].ToString(), out notification);
                case "quicksearchofferline":
                    if (!input.ContainsKey("articleCode"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow articleCode" };
                        return null;
                    }
                    if (!input.ContainsKey("description"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow description" };
                        return null;
                    }
                    if (!input.ContainsKey("clientID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow clientID" };
                        return null;
                    }
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    if (!input.ContainsKey("currency"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow currency" };
                        return null;
                    }
                    if (!input.ContainsKey("offerLineType"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerLineType" };
                        return null;
                    }
                    if (!input.ContainsKey("sortedBy"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow sortedBy" };
                        return null;
                    }
                    if (!input.ContainsKey("sortedDirection"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow sortedDirection" };
                        return null;
                    }
                    if (!input.ContainsKey("pageSize"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow pageSize" };
                        return null;
                    }
                    if (!input.ContainsKey("pageIndex"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow pageIndex" };
                        return null;
                    }
                    
                    return bll.QuickSearchOfferLine(input["articleCode"].ToString(), input["description"].ToString(), Convert.ToInt32(input["clientID"].ToString())
                        , input["season"].ToString(), input["currency"].ToString(), Convert.ToInt32(input["offerLineType"])
                        , Convert.ToInt32(input["pageSize"]), Convert.ToInt32(input["pageIndex"])
                        , input["sortedBy"].ToString(), input["sortedDirection"].ToString(), out notification);

                case "getprintoffer5":
                    if (!input.ContainsKey("offerID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerID" };
                        return null;
                    }
                    if (!input.ContainsKey("IsSendEmail"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow IsSendEmail" };
                        return null;
                    }
                    if (!input.ContainsKey("IsGetFullSizeImage"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow IsGetFullSizeImage" };
                        return null;
                    }
                    if (!input.ContainsKey("imageOption"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow imageOption" };
                        return null;
                    }

                    return bll.GetPrintDataOffer5(Convert.ToInt32(input["offerID"].ToString()), Convert.ToBoolean(input["IsSendEmail"].ToString()), Convert.ToBoolean(input["IsGetFullSizeImage"].ToString()), Convert.ToInt32(input["imageOption"].ToString()), userId, out notification);
                case "getprintofferpp2013":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerID" };
                        return null;
                    }                   

                    return bll.GetPrintDataOfferPP2013(Convert.ToInt32(input["id"].ToString()), userId, out notification);
                case "getexportexcel":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerID" };
                        return null;
                    }

                    return bll.GetExportNewVersion(Convert.ToInt32(input["id"].ToString()), out notification);
                case "get-fobitemonly":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerID" };
                        return null;
                    }

                    return bll.GetExcelFOBItemOnlyReportData(Convert.ToInt32(input["id"].ToString()), out notification);
                case "update-clientinfo":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerID" };
                        return null;
                    }

                    return bll.UpdateClientInfomation(Convert.ToInt32(input["id"].ToString()), out notification);
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
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
