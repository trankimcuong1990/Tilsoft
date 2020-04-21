using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryFinishedProductReceipt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryFinishedProductReceiptEntities CreateContext()
        {
            return new FactoryFinishedProductReceiptEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryFinishedProductReceiptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    var dbItem = context.FactoryFinishedProductReceipt.Where(o => o.FactoryFinishedProductReceiptID == id).FirstOrDefault();
                    foreach (var item in dbItem.FactoryFinishedProductReceiptDetail.ToArray())
                    {
                        context.FactoryFinishedProductReceiptDetail.Remove(item);
                    }
                    context.FactoryFinishedProductReceipt.Remove(dbItem);
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
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    
                    if (id > 0)
                    {
                        FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View dbItem;
                        dbItem = context.FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View.FirstOrDefault(o => o.FactoryFinishedProductReceiptID == id);
                        editFormData.Data = converter.DB2DTO_FactoryFinishedProductReceipt(dbItem);
                    }
                    else {
                        editFormData.Data = new DTO.FactoryFinishedProductReceipt();
                        editFormData.Data.Season = Library.Helper.GetCurrentSeason();
                        editFormData.Data.FactoryFinishedProductReceiptDetails = new List<DTO.FactoryFinishedProductReceiptDetail>();
                    }
                    editFormData.Seasons = support_factory.GetSeason();
                    editFormData.FactoryTeams = support_factory.GetFactoryTeam();
                    //editFormData.FactoryAreas = support_factory.GetFactoryArea();
                    //editFormData.FactoryGoodsProcedureDetails = support_factory.GetFactoryGoodsProcedureDetail();
                    editFormData.FactorySteps = support_factory.GetFactoryStep();
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

            string receiptNo = string.Empty;
            int? factoryTeamID = null;
            string articleCode = string.Empty;
            string description = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;

            if (filters.ContainsKey("receiptNo")) receiptNo = filters["receiptNo"].ToString();
            if (filters.ContainsKey("factoryTeamID") && filters["factoryTeamID"] != null) factoryTeamID = Convert.ToInt32(filters["factoryTeamID"].ToString());
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchFactoryFinishedProductReceipt(orderBy, orderDirection, receiptNo, factoryTeamID, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchFactoryFinishedProductReceipt(orderBy, orderDirection, receiptNo, factoryTeamID, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM);
                    searchFormData.Data = converter.DB2DTO_FactoryFinishedProductReceiptSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
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
            DTO.FactoryFinishedProductReceipt dtoFactoryFinishedProductReceipt = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryFinishedProductReceipt>();
            dtoFactoryFinishedProductReceipt.UpdatedBy = userId;
            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    FactoryFinishedProductReceipt dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryFinishedProductReceipt();
                        context.FactoryFinishedProductReceipt.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryFinishedProductReceipt.Where(o => o.FactoryFinishedProductReceiptID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        if (dtoFactoryFinishedProductReceipt.ReceiptTypeID == 1)
                        {
                            if (dtoFactoryFinishedProductReceipt.InProgressOrBuyDirectlyID == 1)
                            {
                                if (!dtoFactoryFinishedProductReceipt.ImportFromTeamID.HasValue)
                                {
                                    throw new Exception("Team is empty. You should fill-in Team");
                                }
                            }
                        }
                        else if (dtoFactoryFinishedProductReceipt.ReceiptTypeID == 2)
                        {
                            if (!dtoFactoryFinishedProductReceipt.ExportToTeamID.HasValue)
                            {
                                throw new Exception("Team is empty. You should fill-in Team");
                            }
                        }
                        if (!dtoFactoryFinishedProductReceipt.FactoryStepID.HasValue)
                        {
                            throw new Exception("Step is empty. You should fill-in Step");
                        }
                        if (!dtoFactoryFinishedProductReceipt.FactoryGoodsProcedureID.HasValue)
                        {
                            throw new Exception("Step is empty. You should fill-in goods procedure");
                        }

                        //validate quantity
                        //ValidateQuantity(dtoFactoryFinishedProductReceipt);

                        //convert dto to db
                        converter.DTO2DB_FactoryFinishedProductReceipt(dtoFactoryFinishedProductReceipt, ref dbItem);
                        //remove orphan item
                        context.FactoryFinishedProductReceiptDetail.Local.Where(o => o.FactoryFinishedProductReceipt == null).ToList().ForEach(o => context.FactoryFinishedProductReceiptDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //update receipt no
                        context.FactoryFinishedProductReceiptMng_function_GenerateReceipNo(dbItem.FactoryFinishedProductReceiptID, dbItem.Season, dbItem.ReceiptTypeID);
                        //get return data
                        dtoItem = GetData(dbItem.FactoryFinishedProductReceiptID, out notification).Data;
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

        public DTO.ComponentNeedToImports SearchComponentNeedToImport(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ComponentNeedToImports data = new DTO.ComponentNeedToImports();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string articleCode = string.Empty;
            string description = string.Empty;
            string proformaInvoiceNo = string.Empty;
            string clientUD = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            int? importFromTeamID = null;
            int? factoryStepID = null;
            int? factoryGoodsProcedureID = null;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("importFromTeamID") && filters["importFromTeamID"] != null) importFromTeamID = Convert.ToInt32(filters["importFromTeamID"]);
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"] != null) factoryStepID = Convert.ToInt32(filters["factoryStepID"]);
            if (filters.ContainsKey("factoryGoodsProcedureID") && filters["factoryGoodsProcedureID"] != null) factoryGoodsProcedureID = Convert.ToInt32(filters["factoryGoodsProcedureID"]);

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport(sortedBy, sortedDirection, articleCode, description, proformaInvoiceNo, clientUD, factoryFinishedProductUD, factoryFinishedProductNM, importFromTeamID, factoryStepID, factoryGoodsProcedureID).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport(sortedBy, sortedDirection, articleCode, description, proformaInvoiceNo, clientUD, factoryFinishedProductUD, factoryFinishedProductNM, importFromTeamID, factoryStepID, factoryGoodsProcedureID);
                    data.Data = converter.DB2DTO_ComponentNeedToImport(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
                return data;
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

        public DTO.ComponentNeedToExports SearchComponentNeedToExport(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ComponentNeedToExports data = new DTO.ComponentNeedToExports();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string articleCode = string.Empty;
            string description = string.Empty;
            string proformaInvoiceNo = string.Empty;
            string clientUD = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            int? factoryStepID = null;
            int? factoryGoodsProcedureID = null;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"] != null) factoryStepID = Convert.ToInt32(filters["factoryStepID"]);
            if (filters.ContainsKey("factoryGoodsProcedureID") && filters["factoryGoodsProcedureID"] != null) factoryGoodsProcedureID = Convert.ToInt32(filters["factoryGoodsProcedureID"]);

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToExport(sortedBy, sortedDirection, articleCode, description, proformaInvoiceNo, clientUD, factoryFinishedProductUD, factoryFinishedProductNM, factoryStepID, factoryGoodsProcedureID).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToExport(sortedBy, sortedDirection, articleCode, description, proformaInvoiceNo, clientUD, factoryFinishedProductUD, factoryFinishedProductNM, factoryStepID, factoryGoodsProcedureID);
                    data.Data = converter.DB2DTO_ComponentNeedToExport(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
                return data;
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

        public void ValidateQuantity(DTO.FactoryFinishedProductReceipt dtoReceipt)
        {
            using (FactoryFinishedProductReceiptEntities context = CreateContext())
            {
                List<string> ids = new List<string>();
                string stepProgress = context.FactoryFinishedProductReceiptMng_action_GetStepProgress(dtoReceipt.FactoryGoodsProcedureID, dtoReceipt.FactoryStepID).FirstOrDefault(); ;
                foreach (var item in dtoReceipt.FactoryFinishedProductReceiptDetails)
                {
                    if (dtoReceipt.ReceiptTypeID == 1)
                    {
                        ids.Add(dtoReceipt.ImportFromTeamID.ToString() + "_" + dtoReceipt.FactoryStepID.ToString() + "_" + dtoReceipt.FactoryGoodsProcedureID.ToString() + "_" + item.FactoryOrderDetailID + "_" + item.FactoryFinishedProductOrderNormID.ToString() + "_" + item.FactoryFinishedProductID.ToString());
                    }
                    else if (dtoReceipt.ReceiptTypeID == 2)
                    {
                        ids.Add(item.FactoryOrderDetailID.ToString() + "_" + item.FactoryFinishedProductOrderNormID.ToString() + "_" + item.FactoryFinishedProductID + "_" + item.FromGoodsProcedureID.ToString() + "_" + stepProgress);
                    }
                }
                
                List<FactoryFinishedProductReceiptMng_ComponentNeedToImport_View> dbNeedImports;
                List<FactoryFinishedProductReceiptMng_ComponentNeedToExport_View> dbNeedExports;
                dbNeedImports = (dtoReceipt.ReceiptTypeID == 1 ? context.FactoryFinishedProductReceiptMng_ComponentNeedToImport_View.Where(o => ids.Contains(o.FactoryTeamID.ToString() + "_"
                                                                                                                                                                + o.FactoryStepID.ToString() + "_"
                                                                                                                                                                + o.FactoryGoodsProcedureID.ToString() + "_"
                                                                                                                                                                + o.FactoryOrderDetailID + "_"
                                                                                                                                                                + o.FactoryFinishedProductOrderNormID.ToString() + "_"
                                                                                                                                                                + o.FactoryFinishedProductID.ToString())).ToList() : null);
                dbNeedExports = (dtoReceipt.ReceiptTypeID == 2 ? context.FactoryFinishedProductReceiptMng_ComponentNeedToExport_View.Where(o => ids.Contains(o.FactoryOrderDetailID.ToString() + "_"
                                                                                                                                                                + o.FactoryFinishedProductOrderNormID.ToString() + "_"
                                                                                                                                                                + o.FactoryFinishedProductID + "_"
                                                                                                                                                                + o.FromGoodsProcedureID.ToString() + "_" 
                                                                                                                                                                + o.StepProgress.ToString())).ToList() : null);
                foreach (var item in dtoReceipt.FactoryFinishedProductReceiptDetails)
                {
                    //take qnt to check
                    decimal? qntCheck = 0;
                    if (dtoReceipt.ReceiptTypeID == 1)
                    {
                        var needImport = dbNeedImports.Where(o => o.FactoryTeamID == dtoReceipt.ImportFromTeamID
                                                                    && o.FactoryStepID == dtoReceipt.FactoryStepID
                                                                    && o.FactoryGoodsProcedureID == dtoReceipt.FactoryGoodsProcedureID
                                                                    && o.FactoryOrderDetailID == item.FactoryOrderDetailID
                                                                    && o.FactoryFinishedProductOrderNormID == item.FactoryFinishedProductOrderNormID
                                                                    && o.FactoryFinishedProductID == item.FactoryFinishedProductID).FirstOrDefault();
                        qntCheck = (needImport == null ? 0 : needImport.Quantity);
                    }
                    else if (dtoReceipt.ReceiptTypeID == 2)
                    {
                        var needExport = dbNeedExports.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID
                                                                    && o.FactoryFinishedProductOrderNormID == item.FactoryFinishedProductOrderNormID
                                                                    && o.FactoryFinishedProductID == item.FactoryFinishedProductID
                                                                    && o.FromGoodsProcedureID == item.FromGoodsProcedureID
                                                                    && o.StepProgress == stepProgress).FirstOrDefault();
                        qntCheck = (needExport == null ? 0 : needExport.Quantity);
                    }

                    //check
                    if (item.FactoryFinishedProductReceiptDetailID > 0)
                    {
                        decimal? currentQnt = context.FactoryFinishedProductReceiptDetail.Where(o => o.FactoryFinishedProductReceiptDetailID == item.FactoryFinishedProductReceiptDetailID).FirstOrDefault().Quantity;
                        if (item.Quantity - (currentQnt.HasValue ? currentQnt.Value : 0) > (qntCheck.HasValue ? qntCheck.Value : 0))
                        {
                            throw new Exception("Quantity of component " + item.FactoryFinishedProductUD + "must be less than " + ((currentQnt.HasValue ? currentQnt.Value : 0) + (qntCheck.HasValue ? qntCheck.Value : 0)).ToString());
                        }
                    }
                    else
                    {
                        if (item.Quantity > (qntCheck.HasValue ? qntCheck.Value : 0))
                        {
                            throw new Exception("Quantity of component " + item.FactoryFinishedProductUD + "must be less than " + (qntCheck.HasValue ? qntCheck.Value : 0).ToString());
                        }
                    }
                }
            }
        }

        public DTO.ComponentNeedToImport_WithoutOrders_BuyDirectlies SearchComponentNeedToImportWithoutOrdesBuyDirectly(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ComponentNeedToImport_WithoutOrders_BuyDirectlies data = new DTO.ComponentNeedToImport_WithoutOrders_BuyDirectlies();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            
            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport_WithoutOrder_BuyDirectly(sortedBy, sortedDirection, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport_WithoutOrder_BuyDirectly(sortedBy, sortedDirection, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM);
                    data.Data = converter.DB2DTO_ComponentNeedToImport_WithoutOrdersBuyDirectly(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
                return data;
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

        public DTO.ComponentNeedToImport_WithoutOrders_InProgresses SearchComponentNeedToImportWithoutOrdesInProgress(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ComponentNeedToImport_WithoutOrders_InProgresses data = new DTO.ComponentNeedToImport_WithoutOrders_InProgresses();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;
            int? importFromTeamID = null;
            int? factoryStepID = null;
            int? factoryGoodsProcedureID = null;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("importFromTeamID") && filters["importFromTeamID"] != null) importFromTeamID = Convert.ToInt32(filters["importFromTeamID"]);
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"] != null) factoryStepID = Convert.ToInt32(filters["factoryStepID"]);
            if (filters.ContainsKey("factoryGoodsProcedureID") && filters["factoryGoodsProcedureID"] != null) factoryGoodsProcedureID = Convert.ToInt32(filters["factoryGoodsProcedureID"]);

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport_WithoutOrder_InProgress(sortedBy, sortedDirection, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM, importFromTeamID, factoryStepID, factoryGoodsProcedureID).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport_WithoutOrder_InProgress(sortedBy, sortedDirection, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM, importFromTeamID, factoryStepID, factoryGoodsProcedureID);
                    data.Data = converter.DB2DTO_ComponentNeedToImport_WithoutOrdersInProgress(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
                return data;
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

        public DTO.ComponentNeedToExport_WithoutOrders_InProgresses SearchComponentNeedToExportWithoutOrdesInProgress(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ComponentNeedToExport_WithoutOrders_InProgresses data = new DTO.ComponentNeedToExport_WithoutOrders_InProgresses();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string articleCode = string.Empty;
            string description = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            int? factoryStepID = null;
            int? factoryGoodsProcedureID = null;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("factoryStepID") && filters["factoryStepID"] != null) factoryStepID = Convert.ToInt32(filters["factoryStepID"]);
            if (filters.ContainsKey("factoryGoodsProcedureID") && filters["factoryGoodsProcedureID"] != null) factoryGoodsProcedureID = Convert.ToInt32(filters["factoryGoodsProcedureID"]);

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToExport_WithoutOrder_InProgress(sortedBy, sortedDirection, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM, factoryStepID, factoryGoodsProcedureID).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToExport_WithoutOrder_InProgress(sortedBy, sortedDirection, articleCode, description, factoryFinishedProductUD, factoryFinishedProductNM, factoryStepID, factoryGoodsProcedureID);
                    data.Data = converter.DB2DTO_ComponentNeedToExport_WithoutOrdersInProgress(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
                return data;
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

        public DTO.ComponentNeedToImport_Orders_BuyDirectlies SearchComponentNeedToImportOrdersBuyDirectlies(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ComponentNeedToImport_Orders_BuyDirectlies data = new DTO.ComponentNeedToImport_Orders_BuyDirectlies();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string articleCode = string.Empty;
            string description = string.Empty;
            string proformaInvoiceNo = string.Empty;
            string clientUD = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();

            //pager info
            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (FactoryFinishedProductReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport_Orders_BuyDirectly_View(sortedBy, sortedDirection, articleCode, description, proformaInvoiceNo, clientUD, factoryFinishedProductUD, factoryFinishedProductNM).Count();
                    var result = context.FactoryFinishedProductReceiptMng_function_SearchComponentNeedToImport_Orders_BuyDirectly_View(sortedBy, sortedDirection, articleCode, description, proformaInvoiceNo, clientUD, factoryFinishedProductUD, factoryFinishedProductNM);
                    data.Data = converter.DB2DTO_ComponentNeedToImportOrdersBuyDirectly(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.TotalRows = totalRows;
                }
                return data;
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
