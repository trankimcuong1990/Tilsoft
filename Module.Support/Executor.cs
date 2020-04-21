using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support
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

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim())
            {
                case "GetClient2":
                    return bll.GetClient2(userId, filters, out notification);

                case "GetFactory2":
                    return bll.GetFactory2(userId, filters, out notification);

                case "GetProductionItem":
                    return bll.GetProductionItem(userId, filters);

                case "GetClient":
                    return bll.GetClient(filters);

                case "GetFactoryOrderDetail":
                    return bll.GetFactoryOrderDetail(userId, filters);

                case "GetSampleProduct":
                    return bll.GetSampleProduct(filters);

                case "GetModel":
                    return bll.GetModel(filters);

                case "GetProductionItemToDeliveryFromTeamToTeam":
                    return bll.GetProductionItemToDeliveryFromTeamToTeam(userId, filters);

                case "GetProductionItemToDeliveryFromWarehouseToTeam":
                    return bll.GetProductionItemToDeliveryFromWarehouseToTeam(userId, filters);

                case "GetProductionItemToDeliveryFromTeamToTeamToAmend":
                    return bll.GetProductionItemToDeliveryFromTeamToTeamToAmend(userId, filters);

                case "GetProductionItemToDeliveryFromWarehouseToTeamToAmend":
                    return bll.GetProductionItemToDeliveryFromWarehouseToTeamToAmend(userId, filters);

                case "QuickSearchFactoryMaterial":
                   return bll.QuickSearchFactoryMaterial(userId, filters, out notification);

                case "QuickSearchFactoryOrder":
                   return bll.QuickSearchFactoryOrder(userId, filters, out notification);

                case "QuickSearchFactoryFinishedProduct":
                   return bll.QuickSearchFactoryFinishedProduct(userId, filters, out notification);

                case "GetFactory":
                   return bll.GetFactory();

                case "GetFactoryByUser":
                   return bll.GetFactory(userId);

                case "searchclient":
                    if (filters.ContainsKey("query"))
                        return bll.QuickSearchClient(filters["query"].ToString(), out notification);
                    else
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }

                case "QuickSearchAVFSupplier":
                   return bll.QuickSearchAVFSupplier(userId, filters, out notification);

                case "QuickSearchEmployee":
                   return bll.QuickSearchEmployee(userId, filters, out notification);

                case "quicksearchmodel2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchModel(filters["query"].ToString(), out notification);

                case "quicksearchclient2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchClient2(filters["query"].ToString(), out notification);

                case "quicksearchmaterial2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchMaterial(filters["query"].ToString(), out notification);

                case "quicksearchmaterialtype2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchMaterialType(filters["query"].ToString(), out notification);

                case "quicksearchmaterialcolor2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchMaterialColor(filters["query"].ToString(), out notification);

                case "quicksearchframematerial2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchFrameMaterial(filters["query"].ToString(), out notification);

                case "quicksearchframematerialcolor2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchFrameMaterialColor(filters["query"].ToString(), out notification);

                case "quicksearchsubmaterial2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchSubMaterial(filters["query"].ToString(), out notification);

                case "quicksearchsubmaterialcolor2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchSubMaterialColor(filters["query"].ToString(), out notification);

                case "quicksearchbackcushion2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchBackCushion(filters["query"].ToString(), out notification);

                case "quicksearchseatcushion2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchSeatCushion(filters["query"].ToString(), out notification);

                case "quicksearchcushioncolor2":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchCushionColor(filters["query"].ToString(), out notification);

                case "getmodelimage":
                   if (!filters.ContainsKey("id"))
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                       return null;
                   }
                   return bll.GetModelImage(Convert.ToInt32(filters["id"]), out notification);

                case "getproductinfo":
                   if (!filters.ContainsKey("articleCode"))
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow code" };
                       return null;
                   }
                   return bll.GetProductInfo(filters["articleCode"].ToString(), out notification);

                case "quicksearchclient3":
                   if (!filters.ContainsKey("query") || filters["query"] == null)
                   {
                       notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                       return null;
                   }
                   return bll.QuickSearchClient3(filters["query"].ToString(), out notification);

                case "quicksearchforwarder":
                   if (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null)
                       return bll.QuickSearchForwarder(filters["searchQuery"].ToString(), out notification);

                    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                    return null;

                case "quicksearchproductionitem2":
                    if (!filters.ContainsKey("searchQuery") || filters["searchQuery"] == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }
                    return bll.GetProductionItem2(userId, filters["searchQuery"].ToString(), out notification);

                case "getProductionItemWithFilters":
                    if (!filters.ContainsKey("searchQuery") || filters["searchQuery"] == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }
                    return bll.GetProductionItemWithFilters(userId, filters["searchQuery"].ToString(), Convert.ToInt32(filters["branchID"]), out notification);
                case "getWorkOrderApproved":
                    if (!filters.ContainsKey("searchQuery") || filters["searchQuery"] == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown search query";
                        return null;
                    }
                    return bll.GetWorkOrderApproved(userId, filters["searchQuery"].ToString(), out notification);

                case "getPurchaseOrderApprove":
                    if (!filters.ContainsKey("searchQuery") || filters["searchQuery"] == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknown search query";
                        return null;
                    }
                    return bll.GetPurchaseOrderApprove(userId, filters["searchQuery"].ToString(), out notification);

                case "getModel2":
                    if (!filters.ContainsKey("searchQuery") || filters["searchQuery"] == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }

                    return bll.GetModel2(userId, filters["searchQuery"].ToString(), out notification);

                case "getWorkOrder":
                    if (!filters.ContainsKey("searchQuery") || filters["searchQuery"] == null)
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }

                    return bll.GetWorkOrder(userId, filters["searchQuery"].ToString(), out notification);

                case "GetUserProfiles":
                    return bll.GetUserProfiles(userId, filters, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
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
