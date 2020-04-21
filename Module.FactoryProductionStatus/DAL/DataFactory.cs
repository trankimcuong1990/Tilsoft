using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Library.DTO;
using Module.FactoryProductionStatus.DTO;

namespace Module.FactoryProductionStatus.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory() { }

        private FactoryProductionStatusEntities CreateContext()
        {
            return new FactoryProductionStatusEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryProductionStatusModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProductionStatusEntities context = CreateContext())
                {
                    var dbItem = context.FactoryProductionStatus.Where(o => o.FactoryProductionStatusID == id).FirstOrDefault();
                    context.FactoryProductionStatus.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }

        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProductionStatusEntities context = CreateContext())
                {
                    FactoryProductionStatusMng_FactoryProductionStatus_View dbItem;
                    dbItem = context.FactoryProductionStatusMng_FactoryProductionStatus_View
                        .Include("FactoryProductionStatusMng_FactoryProductionStatusDetail_View").FirstOrDefault(o => o.FactoryProductionStatusID == id);
                    editFormData.Data = converter.DB2DTO_FactoryProductionStatus(dbItem);
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return editFormData;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                int? factoryID = null;
                string season = null;
                int? weekNo = null;
                string updatedDate = null;
                string updatorName = null;
                int userId = Convert.ToInt32(filters["userId"]);
                if (filters.ContainsKey("factoryID") && filters["factoryID"] != null)
                {
                    factoryID = Convert.ToInt32(filters["factoryID"]);
                }
                if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("weekNo") && filters["weekNo"] != null)
                {
                    weekNo = Convert.ToInt32(filters["weekNo"]);
                }
                if (filters.ContainsKey("updatedDate") && !string.IsNullOrEmpty(filters["updatedDate"].ToString()))
                {
                    updatedDate = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatorName") && !string.IsNullOrEmpty(filters["updatorName"].ToString()))
                {
                    updatorName = filters["updatorName"].ToString().Replace("'", "''");
                }
                using (FactoryProductionStatusEntities context = CreateContext())
                {
                    totalRows = context.FactoryProductionStatusMng_function_SearchFactoryProductionStatus(orderBy, orderDirection, userId, factoryID, season, weekNo, updatedDate, updatorName).Count();
                    var result = context.FactoryProductionStatusMng_function_SearchFactoryProductionStatus(orderBy, orderDirection, userId, factoryID, season, weekNo, updatedDate, updatorName);
                    searchFormData.Data = converter.DB2DTO_FactoryProductionStatusSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                searchFormData.Factories = supportFactory.GetFactory(userId);
                searchFormData.Seasons = supportFactory.GetSeason();
                searchFormData.WeekSeasons = supportFactory.GetWeekInSeason(Library.Helper.GetCurrentSeason());
                return searchFormData;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return searchFormData;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryProductionStatusDTO dtoFactoryProductionStatus = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryProductionStatusDTO>();
            try
            {
                using (FactoryProductionStatusEntities context = CreateContext())
                {
                    FactoryProductionStatus dbItem = null;
                    if (id > 0)
                    {                        
                        dbItem = context.FactoryProductionStatus.Where(o => o.FactoryProductionStatusID == id).FirstOrDefault();
                        if (dbItem.WeekNo != dtoFactoryProductionStatus.WeekNo)
                        {
                            throw new Exception("You can not change week");
                        }
                    }
                    else
                    {
                        dbItem = new FactoryProductionStatus();
                        context.FactoryProductionStatus.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //calculate Produced Cont for every order
                        decimal totalCont = 0;
                        foreach (var item in dtoFactoryProductionStatus.FactoryProductionStatusDetailDTOs)
                        {
                            totalCont = 0;
                            foreach (var sItem in item.FactoryProductionStatusOrderDetailDTOs)
                            {
                                if (sItem.Qnt40HC.HasValue && sItem.Qnt40HC != 0 && sItem.ProducedQnt.HasValue)
                                {
                                    sItem.ProducedCont = Math.Round(Convert.ToDecimal(sItem.ProducedQnt.Value) / Convert.ToDecimal(sItem.Qnt40HC), 2);
                                    totalCont += Math.Round(Convert.ToDecimal(sItem.ProducedQnt.Value) / Convert.ToDecimal(sItem.Qnt40HC), 2);
                                }
                            }
                            item.ProducedContainerQnt = totalCont;
                        }
                        //convert dto to db
                        converter.DTO2DB_FactoryProductionStatus(dtoFactoryProductionStatus, ref dbItem);
                        dbItem.UpdatedBy = userId;
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(userId, dbItem.FactoryID.Value) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                        //check week
                        //int curentWeek = Library.Helper.GetCurrentWeek();
                        //if (curentWeek - dbItem.WeekNo.Value >= 2)
                        //{
                        //    throw new Exception("You can not update because this week is over 2 weeks compare with current week (" + curentWeek.ToString() + " )");
                        //}
                        //remove orphan
                        context.FactoryProductionStatusDetail.Local.Where(o => o.FactoryProductionStatus == null).ToList().ForEach(o => context.FactoryProductionStatusDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryProductionStatusID, out notification).Data;

                        //auto create production item
                        context.FactoryStockReceiptMng_function_CreateProductionItem();

                        //get total output procduce all week
                        //List<int> factoryOrderDetailIDs = new List<int>();
                        //foreach (var item in dbItem.FactoryProductionStatusDetail.ToList())
                        //{
                        //    foreach (var sItem in item.FactoryProductionStatusOrderDetail.Where(o =>o.FactoryOrderDetailID.HasValue).ToList())
                        //    {
                        //        factoryOrderDetailIDs.Add(sItem.FactoryOrderDetailID.Value);
                        //    }
                        //}
                        //var factoryOrderDetails = from item in context.FactoryProductionStatusOrderDetail.Where(o =>factoryOrderDetailIDs.Contains(o.FactoryOrderDetailID.Value)).ToList()
                        //                         group item by new { item.FactoryOrderDetailID } into g
                        //                         select new { g.Key.FactoryOrderDetailID, TotalOutputProduced = g.Sum(o => o.OutputProducedQnt) };
                        ////var factoryStock = context.FactoryStockReceiptDetail.Where(o => o.FactoryOrderDetailID.HasValue && factoryOrderDetails.Select(s => s.FactoryOrderDetailID).Contains(o.FactoryOrderDetailID));
                        //foreach (var item in context.FactoryStockReceiptDetail.Where(o => o.FactoryOrderDetailID.HasValue && factoryOrderDetailIDs.Contains(o.FactoryOrderDetailID.Value)))
                        //{
                        //    var x = factoryOrderDetails.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID).FirstOrDefault();
                        //    if (x != null)
                        //    {
                        //        item.ProducedQnt = x.TotalOutputProduced;
                        //    }
                        //}
                        //context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public EditFormData GetData(int userId, int id, int? factoryID, string season, out Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProductionStatusEntities context = CreateContext())
                {
                    editFormData.Factories = supportFactory.GetFactory(userId);
                    if (id > 0)
                    {                        
                        FactoryProductionStatusMng_FactoryProductionStatus_View dbItem;
                        dbItem = context.FactoryProductionStatusMng_FactoryProductionStatus_View
                            .Include("FactoryProductionStatusMng_FactoryProductionStatusDetail_View.FactoryProductionStatusMng_FactoryProductionStatusOrderDetail_View").FirstOrDefault(o => o.FactoryProductionStatusID == id);
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(userId, dbItem.FactoryID.Value) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                        editFormData.Data = converter.DB2DTO_FactoryProductionStatus(dbItem);
                    }
                    else
                    {                                               
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(userId, factoryID.Value) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                        editFormData.Data = new DTO.FactoryProductionStatusDTO();
                        editFormData.Data.FactoryID = factoryID;
                        editFormData.Data.Season = season;
                        editFormData.Data.WeekNo = Library.Helper.GetCurrentWeek();
                        editFormData.Data.FactoryUD = editFormData.Factories.Where(o => o.FactoryID == factoryID).FirstOrDefault().FactoryUD;
                        editFormData.Data.FactoryProductionStatusDetailDTOs = new List<FactoryProductionStatusDetailDTO>();

                        int i = -1;
                        DTO.FactoryProductionStatusDetailDTO detail;
                        DTO.FactoryProductionStatusOrderDetailDTO detailOrder;
                        var dbFactoryOrder = context.FactoryProductionStatusMng_Order_View.Where(o => o.FactoryID == factoryID && o.Season == season).OrderBy(s => s.LDS).ToList();
                        
                        int weekNo = Library.Helper.GetCurrentWeek();
                        var dbFactoryOrderDetail = context.FactoryProductionStatusMng_function_OrderDetail(weekNo, factoryID, season).ToList();

                        foreach (var item in dbFactoryOrder)
                        {
                            detail = new FactoryProductionStatusDetailDTO();
                            detail.FactoryProductionStatusDetailID = i;
                            detail.FactoryOrderID = item.FactoryOrderID;
                            detail.ClientUD = item.ClientUD;
                            detail.ProformaInvoiceNo = item.ProformaInvoiceNo;
                            detail.LDS = item.LDS.Value.ToString("dd/MM/yyyy");
                            detail.TotalContOrder = item.TotalContOrder;
                            detail.TotalContProducedLastWeeks = item.TotalProducedCont;
                            detail.FactoryProductionStatusOrderDetailDTOs = new List<FactoryProductionStatusOrderDetailDTO>();

                            int j = -1;
                            foreach (var sItem in dbFactoryOrderDetail.Where(o =>o.FactoryOrderID==item.FactoryOrderID).OrderBy(x =>x.Description))
                            {
                                detailOrder = new FactoryProductionStatusOrderDetailDTO();
                                detailOrder.FactoryProductionStatusOrderDetailID = j;
                                detailOrder.FactoryOrderDetailID = sItem.FactoryOrderDetailID;
                                detailOrder.ArticleCode = sItem.ArticleCode;
                                detailOrder.Description = sItem.Description;
                                detailOrder.OrderQnt = sItem.OrderQnt;
                                detailOrder.Qnt40HC = sItem.Qnt40HC;
                                detailOrder.OrderInCont = sItem.OrderInCont;
                                detailOrder.TotalProducedLastWeek = sItem.TotalProducedLastWeek;
                                detailOrder.TotalOutputProducedLastWeek = sItem.TotalOutputProducedLastWeek;
                                detail.FactoryProductionStatusOrderDetailDTOs.Add(detailOrder);

                                j--;
                            }
                            editFormData.Data.FactoryProductionStatusDetailDTOs.Add(detail);
                            i--;
                        }
                    }
                    editFormData.WeekSeasons = supportFactory.GetWeekInSeason(editFormData.Data.Season);
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return editFormData;
            }
        }

        public ProductionOverviewData GetProductionOverview(int userId, int factoryID, string season, bool isGetSupportData, out Notification notification)
        {
            DTO.ProductionOverviewData data = new DTO.ProductionOverviewData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProductionStatusEntities context = CreateContext())
                {                    
                    if (isGetSupportData)
                    {
                        data.Factories = supportFactory.GetFactory(userId);
                        data.Seasons = supportFactory.GetSeason();
                    }
                    if (factoryID > 0 && !string.IsNullOrEmpty(season))
                    {
                        if (fwFactory.CheckFactoryPermission(userId, factoryID) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                        //get overview data
                        data.CurrentWeek = Library.Helper.GetCurrentWeek();
                        data.WeekSeasons = supportFactory.GetWeekInSeason(season);
                        data.Orders = converter.DB2DTO_Order(context.FactoryProductionStatusMng_Order_View.Where(o =>o.FactoryID==factoryID&&o.Season==season).OrderBy(o =>o.LDS).ToList());
                        data.ProductionByWeeks = converter.DB2DTO_ProductionByWeek(context.FactoryProductionStatusMng_ProductionByWeek_View.Where(o => o.FactoryID == factoryID && o.Season == season).ToList());
                        data.FactoryCapacities = converter.DB2DTO_FactoryCapacity(context.FactoryProductionStatusMng_FactoryCapacity_View.Where(o => o.FactoryID == factoryID && o.Season == season).ToList());
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return data;
            }
        }

        public List<DTO.OrderDetail> GetOrderDetail(int factoryOrderID,string excludeFactoryOrderDetailIDs, out Notification notification)
        {
            List<DTO.OrderDetail> data = new List<OrderDetail>();            
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProductionStatusEntities context = CreateContext())
                {
                    if (string.IsNullOrEmpty(excludeFactoryOrderDetailIDs))
                    {
                        data = converter.DB2DTO_OrderDetail(context.FactoryProductionStatusMng_OrderDetail_View.Where(o => o.FactoryOrderID == factoryOrderID).ToList());
                    }
                    else
                    {
                        List<int> factoryOrderDetailIDs = new List<int>();
                        excludeFactoryOrderDetailIDs = excludeFactoryOrderDetailIDs.Substring(0, excludeFactoryOrderDetailIDs.Length - 1);
                        factoryOrderDetailIDs = excludeFactoryOrderDetailIDs.Split(',').Select(Int32.Parse).ToList();
                        data = converter.DB2DTO_OrderDetail(context.FactoryProductionStatusMng_OrderDetail_View.Where(o => o.FactoryOrderID == factoryOrderID&&!factoryOrderDetailIDs.Contains(o.FactoryOrderDetailID)).ToList());
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return data;
            }
        }


    }
}
