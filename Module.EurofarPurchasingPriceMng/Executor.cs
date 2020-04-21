using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng
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
            throw new NotImplementedException();
            //return bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.UpdateData(userId, id, ref dtoItem, out notification);
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
            throw new NotImplementedException();
            //return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "updatetarget":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    DTO.SearchFormData totalRowData = new DTO.SearchFormData();
                    return bll.UpdateData(userId, input["data"], out notification);

                case "confirmprice":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    return bll.ConfirmPrice(userId, input["data"], out notification);

                case "unconfirmprice":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    return bll.UnConfirmPrice(userId, input["data"], out notification);

                case "gethistory":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetHistoryData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getpurchasinghistory":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetPurchasingPriceHistoryData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getgeneralbreakdown":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetGeneralBreakDownData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getpalbreakdown":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.GetPALBreakDownData(userId, Convert.ToInt32(input["id"]), out notification);

                case "get-total-row":
                    if (!input.ContainsKey("filters"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow filters!" };
                        return null;
                    }
                    return bll.GetTotalRow(userId, (System.Collections.Hashtable)input["filters"], out notification);

                case "get-search-result-additional-data":
                    if (!input.ContainsKey("season") || input["season"] == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season!" };
                        return null;
                    }
                    if (!input.ContainsKey("ids") || input["ids"] == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow ids!" };
                        return null;
                    }
                    return bll.GetDataWithFilterAdditional(input["season"].ToString(), input["ids"].ToString(), out notification);

                //case "get-conclusion":
                //    if (!input.ContainsKey("season") || input["season"] == null)
                //    {
                //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season!" };
                //        return null;
                //    }
                //    return bll.GetConclusion(input["season"].ToString(), out notification);

                case "getreportdata":
                    return bll.GetreportData(input, out notification);
                case "export":
                    return bll.Export(input, out notification);
                case "import":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    return bll.ImportQuotationDetail(userId, input["data"], out notification);
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched!" };
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
