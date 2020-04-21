using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng
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
            throw new NotImplementedException();
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
                case "getfilterdata":
                    return bll.GetFilterData(out notification);
                case "getdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("modelId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow model!" };
                        return null;
                    }
                    if (!input.ContainsKey("sampleProductId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow sample!" };
                        return null;
                    }
                    if (!input.ContainsKey("offerSeasonDetailId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offer item!" };
                        return null;
                    }
                    if (!input.ContainsKey("factoryId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory!" };
                        return null;
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["modelId"]), Convert.ToInt32(input["sampleProductId"]), Convert.ToInt32(input["offerSeasonDetailId"]), Convert.ToInt32(input["factoryId"]), out notification);

                case "getprice":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("articleCode"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow articleCode!" };
                        return null;
                    }
                    return bll.GetPriceData(userId, input["data"], Convert.ToInt32(input["id"]), input["articleCode"].ToString(), out notification);

                case "updateprice":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    if (!input.ContainsKey("articleCode"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow articleCode!" };
                        return null;
                    }
                    return bll.UpdatePrice(userId, input["data"], Convert.ToInt32(input["id"]), input["articleCode"].ToString(), out notification);

                case "getproductoption":
                    if (!input.ContainsKey("categoryId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow category!" };
                        return null;
                    }
                    if (!input.ContainsKey("modelId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow model!" };
                        return null;
                    }
                    if (!input.ContainsKey("productGroupId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow product group!" };
                        return null;
                    }
                    return bll.GetProductOption(userId, Convert.ToInt32(input["categoryId"]), Convert.ToInt32(input["modelId"]), Convert.ToInt32(input["productGroupId"]), out notification);

                case "updatecategoryoption":
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
                    if (bll.UpdateCategoryOption(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "quicksearchproductionitem":
                    if (!input.ContainsKey("query"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow query" };
                        return null;
                    }
                    if (!input.ContainsKey("type"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow type" };
                        return null;
                    }
                    return bll.QuickSearchProductionItem(input["query"].ToString(), input["type"].ToString(), out notification);

                case "get-default-production-item":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetDefaultProductionItem(Convert.ToInt32(input["id"]), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(out notification);

                case "getmodel":
                    return bll.GetModel(userId, input, out notification);

                case "getsampleproduct":
                    return bll.GetSampleProduct(userId, input, out notification);

                case "approve-option":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.ApproveOption(userId, Convert.ToInt32(input["id"]), out notification);

                case "un-approve-option":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.UnApproveOption(userId, Convert.ToInt32(input["id"]), out notification);

                case "approve-all-option":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.ApproveAllOption(userId, input["data"], out notification);

                case "un-approve-all-option":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.UnApproveAllOption(userId, input["data"], out notification);

                case "update-season-spec":
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
                    return bll.UpdateSeasonSpecPercent(userId, Convert.ToInt32(input["id"]), Convert.ToDecimal(input["data"]), out notification);

                case "getcaculation":
                    return bll.GetCaculation(userId, out notification);

                case "import-bom-data":
                    return bll.ImportBOMData(userId, out notification);

                case "import-bom-data-single":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id!" };
                        return null;
                    }
                    return bll.ImportBOMDataSingle(userId, Convert.ToInt32(input["id"]), out notification);

                case "export-to-excel":
                    return bll.GetExcelReportBreakdown(input, out notification);
                case "get-articlecode":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    if (!input.ContainsKey("modelID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow modelID!" };
                        return null;
                    }
                    if (!input.ContainsKey("offerSeasonDetailID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerSeasonDetailID!" };
                        return null;
                    }                  
              
                    return bll.GetArticleCode(Convert.ToInt32(input["offerSeasonDetailID"]), Convert.ToInt32(input["modelID"]), input["data"], out notification);                   

                case "get-purchasing-price":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetPurchasingPriceData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getpricequotation":                  
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }                 
                    data = input["data"];
                    return bll.GetPriceQuotation(userId, data, out notification);
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
