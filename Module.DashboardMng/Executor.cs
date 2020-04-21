using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();
        private DAL.DataFactory factory = new DAL.DataFactory();

        public string identifier { get; set; }
        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            //return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            //return bll.DeleteData(userId, id, out notification);
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            //bll = new BLL(identifier);
            //return bll.UpdateData(userId, id, ref dtoItem, out notification);
            throw new NotImplementedException();
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            //return bll.Approve(userId, id, ref dtoItem, out notification);
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            //return bll.Reset(userId, id, ref dtoItem, out notification);
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getreportdata":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetReportData(userId, Convert.ToString(input["season"]) , out notification);

                case "getdataforuserdashboard":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetDataForUserDashBoard(userId, Convert.ToString(input["season"]), out notification);
                case "getproductiondata":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    return bll.GetProductionData(userId, Convert.ToString(input["season"]), out notification);
                case "getpendingpricedata":
                   
                    return bll.GetPendingPriceData(userId, out notification);

                case "getdataforproductionoverviewdetail":
                    if (!input.ContainsKey("factoryOrderID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory order id" };
                        return null;
                    }

                    return bll.GetDataForProductionOverviewDetail(Convert.ToInt32(input["factoryOrderID"]), out notification);

                case "getweeklyproductiondata":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }
                    return bll.GetWeeklyProductionData(userId, Convert.ToString(input["season"]), Convert.ToInt32(input["factoryId"]), out notification);

                case "getfinanceoverviewbyfactory":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    if (!input.ContainsKey("factoryID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory id" };
                        return null;
                    }

                    int? factoryID = null;
                    if (input["factoryID"] != null && Convert.ToInt32(input["factoryID"]) != -1)
                    {
                        factoryID = Convert.ToInt32(input["factoryID"]);
                    }

                    return bll.GetFinanceOverviewByFactory(userId, Convert.ToString(input["season"]), factoryID, out notification);
                case "getoffernotapprovedyet":     
                    return bll.GetAdminDashboardOfferNotApprovedYetDTOs(userId, Convert.ToString(input["season"]), out notification);

                //case "getoffernotapprovedyetuserdashboard":
                //    return bll.GetUserDashboardOfferNotApprovedYetDTOs(out notification);

                case "get-delta-compare-offer-with-pi-last-year":
                    return bll.GetOfferAndPIDeltaComparison(userId, out notification);

                case "get-delta-by-client-compare":
                    string season = input["season"].ToString();
                    return bll.GetDeltaByClientCompare(userId, season, out notification);

                case "get-pending-offer-item-price":                    
                    return bll.GetPendingOfferItemPrice(userId, input["season"].ToString(), out notification);

                case "get-item-delta-need-attention":
                    if (!input.ContainsKey("pageIndex"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow page index!" };
                        return null;
                    }
                    string orderBy = null;
                    string orderDirection = null;
                    if (input.ContainsKey("orderBy") && input["orderBy"] != null)
                    {
                        orderBy = input["orderBy"].ToString();
                    }
                    if (input.ContainsKey("orderDirection") && input["orderDirection"] != null)
                    {
                        orderDirection = input["orderDirection"].ToString();
                    }
                    return factory.GetItemDeltaNeedAttention(input, 50, Library.Helper.ConvertStringToInt(input["pageIndex"].ToString()).Value, orderBy, orderDirection, out notification);


                case "gethtmlreport":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    return bll.GetHTMLReportData(userId, input["season"].ToString(), out notification);
                case "dashboarddetal":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    return bll.GetDashboardDetal(userId, input["season"].ToString(), out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }
    }
}
