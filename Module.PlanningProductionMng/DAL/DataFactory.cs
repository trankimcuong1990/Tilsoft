using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library;

namespace Module.PlanningProductionMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private PlanningProductionEntities CreateContext()
        {
            return new PlanningProductionEntities(Library.Helper.CreateEntityConnectionString("DAL.PlanningProductionModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string workOrderUD = null;
                string productArticleCode = null;
                string productDescription = null;
                string clientUD = null;
                string proformaInvoiceNo = null;
                bool? hasBOM = null;
                int? workOrderStatusID = null;
                string startDate = null;
                string finishDate = null;

                int userId = Convert.ToInt32(filters["userId"]);
                int? companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("workOrderUD") && !string.IsNullOrEmpty(filters["workOrderUD"].ToString()))
                {
                    workOrderUD = filters["workOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productArticleCode") && !string.IsNullOrEmpty(filters["productArticleCode"].ToString()))
                {
                    productArticleCode = filters["productArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("productDescription") && !string.IsNullOrEmpty(filters["productDescription"].ToString()))
                {
                    productDescription = filters["productDescription"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
                {
                    clientUD = filters["clientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("proformaInvoiceNo") && !string.IsNullOrEmpty(filters["proformaInvoiceNo"].ToString()))
                {
                    proformaInvoiceNo = filters["proformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("hasBOM") && filters["hasBOM"] != null && !string.IsNullOrEmpty(filters["hasBOM"].ToString()))
                {
                    switch (filters["hasBOM"].ToString().ToLower())
                    {
                        case "true":
                            hasBOM = true;
                            break;
                        case "false":
                            hasBOM = false;
                            break;
                        default:
                            hasBOM = null;
                            break;
                    }
                }
                if (filters.ContainsKey("workOrderStatusID") && filters["workOrderStatusID"] != null)
                {
                    workOrderStatusID = Convert.ToInt32(filters["workOrderStatusID"]);
                }
                if (filters.ContainsKey("startDate") && !string.IsNullOrEmpty(filters["startDate"].ToString()))
                {
                    startDate = filters["startDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("finishDate") && !string.IsNullOrEmpty(filters["finishDate"].ToString()))
                {
                    finishDate = filters["finishDate"].ToString().Replace("'", "''");
                }
                using (PlanningProductionEntities context = CreateContext())
                {
                    totalRows = context.PlanningProductionMng_function_SearchPlanningProduction(orderBy, orderDirection, companyID, workOrderUD, productArticleCode, productDescription, clientUD, proformaInvoiceNo, hasBOM, workOrderStatusID, startDate, finishDate).Count();
                    var result = context.PlanningProductionMng_function_SearchPlanningProduction(orderBy, orderDirection, companyID, workOrderUD, productArticleCode, productDescription, clientUD, proformaInvoiceNo, hasBOM, workOrderStatusID, startDate, finishDate);
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());                    
                }                
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return searchFormData;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.PlanningProductionDTO();
            try
            {
                using (PlanningProductionEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        var x = context.PlanningProductionMng_PlanningProduction_View.Where(o => o.PlanningProductionID == id).FirstOrDefault();
                        data.Data = converter.DB2DTO_PlanningProduction(x);

                        //get work order info
                        var wo = context.PlanningProductionMng_WorkOrder_View.Where(o => o.PlanningProductionID == id).FirstOrDefault();
                        data.WorkOrder = AutoMapper.Mapper.Map<PlanningProductionMng_WorkOrder_View, DTO.WorkOrderDTO>(wo);

                        //get work virtual workcenter
                        var wc = context.PlanningProductionMng_WorkCenter_View.ToList();
                        data.WorkCenters = AutoMapper.Mapper.Map<List<PlanningProductionMng_WorkCenter_View>, List<DTO.WorkCenterDTO>>(wc);
                    }                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PlanningProductionDTO dtoPlanning = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PlanningProductionDTO>();
            try
            {
                int? companyID = fw_factory.GetCompanyID(userId);
                using (PlanningProductionEntities context = CreateContext())
                {
                    //delete item is deleted
                    context.PlanningProductionMng_function_DeleteIsDeletedItem();
                    //get bom
                    PlanningProduction dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.PlanningProduction.Where(o => o.PlanningProductionID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new PlanningProduction();
                        context.PlanningProduction.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //create production item for workcenter which is virtual
                        List<ProductionItem> dbVirualProductionItem = new List<ProductionItem>();
                        List<ProductionItem> dbProductionItemResult = new List<ProductionItem>();
                        CreateVirtualProductionItem(dtoPlanning, ref dbVirualProductionItem, ref dbProductionItemResult, companyID, userId);
                        context.ProductionItem.AddRange(dbVirualProductionItem);
                        context.SaveChanges();

                        //convert dto to db
                        converter.DTO2DB_PlanningProduction(dtoPlanning, ref dbItem, dbProductionItemResult);

                        //remove orphan
                        context.PlanningProductionTeam.Local.Where(o => o.PlanningProduction == null).ToList().ForEach(o => context.PlanningProductionTeam.Remove(o));

                        //update workorder
                        int? workOrderID = dtoPlanning.WorkOrderID;
                        var dbWorkOrder = context.WorkOrder.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                        dbWorkOrder.StartDate = dtoPlanning.WorkOrderStartDate.ConvertStringToDateTime();
                        dbWorkOrder.FinishDate = dtoPlanning.WorkOrderFinishDate.ConvertStringToDateTime();

                        //save data
                        context.SaveChanges();

                        //get return data
                        dtoItem = GetData(dbItem.PlanningProductionID, out notification).Data;

                        //delete item is deleted 
                        context.PlanningProductionMng_function_DeleteIsDeletedItem();
                        
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //DTO.SupportFormData data = new DTO.SupportFormData();
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }


        public int CreatePlanningProductionFromBOM(int bomID, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PlanningProductionEntities context = CreateContext())
                {
                    var planningItem = context.PlanningProduction.Where(o => o.BOMID == bomID).FirstOrDefault();
                    if (planningItem == null)
                    {
                        context.PlanningProductionMng_function_DeleteIsDeletedItem();
                        //copy data from BOM to Planning Production
                        PlanningProduction dbPlanning = new PlanningProduction();
                        PlanningProductionMng_BOM_View dbBOM = context.PlanningProductionMng_BOM_View.Where(o => o.BOMID == bomID).FirstOrDefault();
                        if (dbBOM == null)
                        {
                            throw new Exception("Can not find BOM");
                        }
                        AutoMapper.Mapper.Map<PlanningProductionMng_BOM_View, PlanningProduction>(dbBOM, dbPlanning);
                        context.PlanningProduction.Add(dbPlanning);
                        context.SaveChanges();
                        return dbPlanning.PlanningProductionID;
                    }
                    else {
                        return planningItem.PlanningProductionID;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return -1; ;
            }
        }

        //public DTO.EditFormData GetData(int id, Hashtable param, int userId, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    DTO.EditFormData data = new DTO.EditFormData();
        //    data.Data = new DTO.PlanningProductionDTO();
        //    try
        //    {
        //        using (PlanningProductionEntities context = CreateContext())
        //        {
        //            PlanningProductionMng_PlanningProduction_View dbPlanning = null;
        //            if (id > 0)
        //            {
        //                dbPlanning = context.PlanningProductionMng_PlanningProduction_View.Where(o => o.PlanningProductionID == id).FirstOrDefault();                        
        //            }
        //            else
        //            {
        //                //copy bom to planning production
        //                int bomID = Convert.ToInt32(param["bomID"]);
        //                this.CreatePlanningProductionFromBOM(bomID, userId, out notification);

        //                //get planning after copy from bom
        //                int planningProductionID = context.PlanningProduction.Where(o => o.BOMID == bomID && !o.ParentPlanningProductionID.HasValue).FirstOrDefault().PlanningProductionID;
        //                dbPlanning = context.PlanningProductionMng_PlanningProduction_View.Where(o => o.PlanningProductionID == planningProductionID).FirstOrDefault();
        //            }
        //            data.Data = converter.DB2DTO_PlanningProduction(dbPlanning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.GetInnerException(ex).Message;
        //    }
        //    return data;
        //}

        public List<DTO.ProductionTeamDTO> GetProductionTeam(string searchQuery, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PlanningProductionEntities context = CreateContext())
                {
                    List<DTO.ProductionTeamDTO> data = new List<DTO.ProductionTeamDTO>();
                    var teams = context.PlanningProductionMng_ProductionTeam_View.Where(o =>o.ProductionTeamNM.Contains(searchQuery) || o.ProductionTeamUD.Contains(searchQuery)).ToList();
                    data = AutoMapper.Mapper.Map<List<PlanningProductionMng_ProductionTeam_View>, List<DTO.ProductionTeamDTO>>(teams);
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new List<DTO.ProductionTeamDTO>();
            }
        }

        public DTO.GrantChart.GrantChartData GetGrantChart(int planningProductionID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PlanningProductionEntities context = CreateContext())
                {
                    DTO.GrantChart.GrantChartData data = new DTO.GrantChart.GrantChartData();
                    DTO.GrantChart.Planning dtoPlanning;

                    //get data
                    var dbPlanning = context.PlanningProductionMng_function_GetGrantChartPlanningProduction(planningProductionID).ToList();
                    int? workOrderID = dbPlanning.Where(o => o.WorkOrderID.HasValue).FirstOrDefault().WorkOrderID;
                    var dbWorkCenters = dbPlanning.Select(s => new { s.WorkCenterID, s.WorkCenterNM }).Distinct().ToList();

                    //get workorder info
                    var woInfo = context.PlanningProductionMng_WorkOrder_View.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                    data.WorkOrderInfo = AutoMapper.Mapper.Map<PlanningProductionMng_WorkOrder_View, DTO.GrantChart.WorkOrderInfo>(woInfo);

                    //get arising data 
                    var dbReceivingDeliveryArisings = context.PlanningProductionMng_GrantChart_ReceivingDeliveryArisingByDate_View.Where(o =>o.WorkOrderID == workOrderID).ToList();
                    data.ArisingByDates = AutoMapper.Mapper.Map<List<PlanningProductionMng_GrantChart_ReceivingDeliveryArisingByDate_View>, List<DTO.GrantChart.ArisingByDate>>(dbReceivingDeliveryArisings);

                    //get planing team
                    List<int> planningProductionIDs = dbPlanning.Select(s => s.PlanningProductionID).ToList();
                    var dbPlanningTeam = context.PlanningProductionMng_GrantChart_PlanningProductionTeam_View.Where(o => planningProductionIDs.Contains(o.PlanningProductionID.Value)).ToList();

                    //get produced of virtual workcenter (get from table ProductionStatisticsDetail in module ProductionStatistics)
                    var dbStatistics = context.PlanningProductionMng_GrantChart_ProductionStatisticsDetail_View.Where(o => o.WorkOrderID == workOrderID).ToList();
                    data.StatisticsByDates = AutoMapper.Mapper.Map<List<PlanningProductionMng_GrantChart_ProductionStatisticsDetail_View>, List<DTO.GrantChart.StatisticsByDate>>(dbStatistics);

                    //mark group workcenter
                    int workCenterIndex = 1;
                    foreach (var wcItem in dbWorkCenters)
                    {
                        //generate all component of this workcenter (base on planning that mean group is WorkOrder-WorkCenter-ProductionItem)
                        foreach (var planItem in dbPlanning.Where(o => o.WorkCenterID == wcItem.WorkCenterID).OrderBy(s=>s.ProductionItemUD).ToList())
                        {
                            dtoPlanning = new DTO.GrantChart.Planning();
                            data.Plannings.Add(dtoPlanning);

                            dtoPlanning.PlanningProductionID = planItem.PlanningProductionID;
                            dtoPlanning.BOMID = planItem.BOMID;
                            dtoPlanning.WorkOrderID = workOrderID;
                            dtoPlanning.WorkCenterID = wcItem.WorkCenterID;
                            dtoPlanning.ProductionItemID = planItem.ProductionItemID;
                            dtoPlanning.ProductionTeamID = null;
                            dtoPlanning.WorkCenterNM = wcItem.WorkCenterNM;
                            dtoPlanning.ProductionTeamNM = "";
                            dtoPlanning.ProductionItemUD = planItem.ProductionItemUD;
                            dtoPlanning.ProductionItemNM = planItem.ProductionItemNM;
                            dtoPlanning.StartDate = planItem.StartDate.Value.ToString("dd/MM/yyyy");
                            dtoPlanning.FinishDate = planItem.FinishDate.Value.ToString("dd/MM/yyyy");
                            dtoPlanning.PercentComplete = null;
                            dtoPlanning.PlanQnt = planItem.PlanQnt; // plan qnt take from bom (bomQnt * workOrderQnt) if has bom, otherwise take from planningteam (this value calculated in sql)
                            dtoPlanning.ReceivedQnt = dbReceivingDeliveryArisings.Where(o =>o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID && o.ProductionItemID == planItem.ProductionItemID).Sum(s => s.ReceivedQnt);
                            if (planItem.BOMID.HasValue) //workcenter in bom (not virtual workcenter)
                            {
                                dtoPlanning.ProducedQnt = dbReceivingDeliveryArisings.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID && o.ProductionItemID == planItem.ProductionItemID).Sum(s => s.ProducedQnt);
                            }
                            else
                            { //virtual workcenter
                                dtoPlanning.ProducedQnt = dbStatistics.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID && o.ProductionItemID == planItem.ProductionItemID).Sum(s => s.ProducedQnt);
                            }
                            dtoPlanning.RemainQnt = dtoPlanning.PlanQnt - dtoPlanning.ProducedQnt;

                            //tracking row
                            dtoPlanning.GroupIndex = 1; //group is only have workcenter-component
                            dtoPlanning.HasBOM = planItem.BOMID.HasValue;
                            dtoPlanning.WorkCenterIndex = workCenterIndex; //mark the group that group is WorkOrder-WorkCenter-ProductionItem
                            dtoPlanning.TeamIndex = null;

                            //mark group of report                            
                            workCenterIndex++;
                        }

                        //generate all component of this workcenter an also include team (base on planning team that mean group is WorkOrder-WorkCenter-ProductionItem-Team)
                        var planningTeams = dbPlanningTeam.Where(o => o.WorkCenterID == wcItem.WorkCenterID).ToList();
                        planningTeams = planningTeams.OrderBy(s =>s.ProductionTeamNM).ThenBy(s =>s.ProductionItemUD).ToList();

                        foreach (var teamItem in planningTeams)
                        {
                            dtoPlanning = new DTO.GrantChart.Planning();
                            data.Plannings.Add(dtoPlanning);
                            var planItem = dbPlanning.Where(o => o.PlanningProductionID == teamItem.PlanningProductionID).FirstOrDefault();
                            
                            dtoPlanning.PlanningProductionID = planItem.PlanningProductionID;
                            dtoPlanning.BOMID = planItem.BOMID;
                            dtoPlanning.WorkOrderID = workOrderID;
                            dtoPlanning.WorkCenterID = wcItem.WorkCenterID;
                            dtoPlanning.ProductionItemID = planItem.ProductionItemID;
                            dtoPlanning.ProductionTeamID = teamItem.ProductionTeamID;
                            dtoPlanning.WorkCenterNM = wcItem.WorkCenterNM;
                            dtoPlanning.ProductionTeamNM = teamItem.ProductionTeamNM;
                            dtoPlanning.ProductionItemUD = planItem.ProductionItemUD;
                            dtoPlanning.ProductionItemNM = planItem.ProductionItemNM;
                            dtoPlanning.StartDate = planItem.StartDate.Value.ToString("dd/MM/yyyy");
                            dtoPlanning.FinishDate = planItem.FinishDate.Value.ToString("dd/MM/yyyy");
                            dtoPlanning.PercentComplete = null;
                            dtoPlanning.PlanQnt = teamItem.PlanQnt; //plan take from planning that user assign for team in module planning production
                            dtoPlanning.ReceivedQnt = dbReceivingDeliveryArisings.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID && o.ProductionItemID == planItem.ProductionItemID && o.ProductionTeamID == teamItem.ProductionTeamID).Sum(s => s.ReceivedQnt);
                            if (teamItem.BOMID.HasValue) //workcenter in bom (not virtual workcenter)
                            {
                                dtoPlanning.ProducedQnt = dbReceivingDeliveryArisings.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID && o.ProductionItemID == planItem.ProductionItemID && o.ProductionTeamID == teamItem.ProductionTeamID).Sum(s => s.ProducedQnt);
                            }
                            else { //virtual workcenter
                                dtoPlanning.ProducedQnt = dbStatistics.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID && o.ProductionItemID == planItem.ProductionItemID && o.ProductionTeamID == teamItem.ProductionTeamID).Sum(s => s.ProducedQnt);
                            }
                            dtoPlanning.RemainQnt = dtoPlanning.PlanQnt - dtoPlanning.ProducedQnt;

                            //tracking row
                            dtoPlanning.GroupIndex = 2;//group is  have workcenter-component-team
                            dtoPlanning.HasBOM = planItem.BOMID.HasValue;
                            dtoPlanning.WorkCenterIndex = workCenterIndex; //mark the group that group is WorkOrder-WorkCenter-ProductionItem
                            dtoPlanning.TeamIndex = teamItem.RowIndex;

                            //mark group of report
                            workCenterIndex++;
                        }

                        //generate for all components that are not assigned teams but they are arising in delivery note & receiving note
                        var arisingTeams = from item in dbReceivingDeliveryArisings.Where(o => o.WorkOrderID == workOrderID && o.WorkCenterID == wcItem.WorkCenterID)
                                           group item by new { item.ProductionTeamID, item.ProductionItemID, item.ProductionTeamNM, item.ProductionItemUD, item.ProductionItemNM } into g
                                           select new { g.Key.ProductionTeamID, g.Key.ProductionItemID, g.Key.ProductionTeamNM, g.Key.ProductionItemUD, g.Key.ProductionItemNM, TotalReceivedQnt = g.Sum(o => o.ReceivedQnt), TotalProducedQnt = g.Sum(o => o.ProducedQnt) };
                        //arisingTeams = arisingTeams.OrderBy(s => s.ProductionTeamNM).ThenBy(s => s.ProductionItemUD).ToList();
                        var x = arisingTeams
                                     .GroupBy(g => g.ProductionTeamID)
                                     .Select(c => c.OrderBy(o => o.ProductionTeamNM).ThenBy(s => s.ProductionItemUD).Select((v, i) => new { v, i }).ToList())
                                     .SelectMany(c => c)
                                     .Select(c => new { c.v.ProductionTeamID,c.v.ProductionItemID, c.v.ProductionTeamNM, c.v.ProductionItemUD, c.v.ProductionItemNM, c.v.TotalReceivedQnt, c.v.TotalProducedQnt, TeamIndex  = c.i + 1})
                                     .ToList();
                        foreach (var arisingItem in x)
                        {
                            if (!planningTeams.Select(s => s.ProductionTeamID).Contains(arisingItem.ProductionTeamID))
                            {
                                dtoPlanning = new DTO.GrantChart.Planning();
                                data.Plannings.Add(dtoPlanning);

                                dtoPlanning.PlanningProductionID = null;
                                dtoPlanning.WorkOrderID = workOrderID;
                                dtoPlanning.WorkCenterID = wcItem.WorkCenterID;
                                dtoPlanning.ProductionItemID = arisingItem.ProductionItemID;
                                dtoPlanning.ProductionTeamID = arisingItem.ProductionTeamID;
                                dtoPlanning.WorkCenterNM = wcItem.WorkCenterNM;
                                dtoPlanning.ProductionTeamNM = arisingItem.ProductionTeamNM;
                                dtoPlanning.ProductionItemUD = arisingItem.ProductionItemUD;
                                dtoPlanning.ProductionItemNM = arisingItem.ProductionItemNM;
                                dtoPlanning.StartDate = "";
                                dtoPlanning.FinishDate = "";
                                dtoPlanning.PercentComplete = null;
                                dtoPlanning.PlanQnt = null;
                                dtoPlanning.ReceivedQnt = arisingItem.TotalReceivedQnt;
                                dtoPlanning.ProducedQnt = arisingItem.TotalProducedQnt;
                                dtoPlanning.RemainQnt = dtoPlanning.PlanQnt - dtoPlanning.ProducedQnt;

                                //tracking row
                                dtoPlanning.GroupIndex = 2;//group is  have workcenter-component-team
                                dtoPlanning.HasBOM = true; //alway equal true because it are arising in delivery note & receiving note
                                dtoPlanning.WorkCenterIndex = workCenterIndex; //mark the group that group is WorkOrder-WorkCenter-ProductionItem
                                dtoPlanning.TeamIndex = arisingItem.TeamIndex;

                                //mark group of report
                                workCenterIndex++;
                            }
                        }

                        workCenterIndex = 1;
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return new DTO.GrantChart.GrantChartData();
            }
        }

        private void CreateVirtualProductionItem(DTO.PlanningProductionDTO dtoPlanning, ref List<ProductionItem> dbProductionItems, ref List<ProductionItem> dbProductionItemResult, int? companyID, int? userId)
        {
            using (PlanningProductionEntities context = CreateContext()) {
                if (dtoPlanning.PlanningProductionID < 0)
                {
                    var x = context.ProductionItem.Where(o => o.ProductionItemUD == dtoPlanning.ProductionItemUD).FirstOrDefault();
                    if (x == null)
                    {
                        ProductionItem dbProductionItem = new ProductionItem();
                        dbProductionItems.Add(dbProductionItem);
                        dbProductionItemResult.Add(dbProductionItem);

                        dbProductionItem.ProductionItemUD = dtoPlanning.ProductionItemUD;
                        dbProductionItem.ProductionItemNM = dtoPlanning.ProductionItemNM;
                        dbProductionItem.ProductionItemTypeID = 1; //component
                        dbProductionItem.UnitID = 25;
                        dbProductionItem.CompanyID = companyID;
                        dbProductionItem.UpdatedBy = userId;
                        dbProductionItem.UpdatedDate = DateTime.Now;
                    }
                    else {
                        dbProductionItemResult.Add(x);
                    }
                }
            }
            foreach (var item in dtoPlanning.PlanningProductionDTOs)
            {
                CreateVirtualProductionItem(item, ref dbProductionItems, ref dbProductionItemResult, companyID, userId);
            }
        }

    }
}
