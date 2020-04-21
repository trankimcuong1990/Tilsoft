using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace Module.FactoryMaterialReceipt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryMaterialReceiptEntities CreateContext()
        {
            return new FactoryMaterialReceiptEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryMaterialReceiptModel"));
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
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterialReceipt.Where(o => o.FactoryMaterialReceiptID == id).FirstOrDefault();
                    context.FactoryMaterialReceipt.Remove(dbItem);
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
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    
                    if (id > 0)
                    {
                        FactoryMaterialReceiptMng_FactoryMaterialReceipt_View dbItem;
                        dbItem = context.FactoryMaterialReceiptMng_FactoryMaterialReceipt_View.FirstOrDefault(o => o.FactoryMaterialReceiptID == id);
                        editFormData.Data = converter.DB2DTO_FactoryMaterialReceipt(dbItem);
                    }
                    else {
                        editFormData.Data = new DTO.FactoryMaterialReceipt();
                        editFormData.Data.Season = Library.Helper.GetCurrentSeason();
                        editFormData.Data.FactoryMaterialReceiptDetails = new List<DTO.FactoryMaterialReceiptDetail>();
                    }
                    editFormData.Seasons = support_factory.GetSeason();
                    editFormData.FactoryTeams = support_factory.GetFactoryTeam();
                    editFormData.FactoryAreas = support_factory.GetFactoryArea();
                    editFormData.FactoryGoodsProcedureDetails = support_factory.GetFactoryGoodsProcedureDetail();
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
            string deliverName = string.Empty;
            string deliverAddress = string.Empty;
            string stockName = string.Empty;
            string stockAddress = string.Empty;
            string receiverName = string.Empty;
            string receiverAddress = string.Empty;

            if (filters.ContainsKey("receiptNo")) receiptNo = filters["receiptNo"].ToString();
            if (filters.ContainsKey("deliverName")) deliverName = filters["deliverName"].ToString();
            if (filters.ContainsKey("deliverAddress")) deliverAddress = filters["deliverAddress"].ToString();
            if (filters.ContainsKey("stockName")) stockName = filters["stockName"].ToString();
            if (filters.ContainsKey("stockAddress")) stockAddress = filters["stockAddress"].ToString();
            if (filters.ContainsKey("receiverName")) receiverName = filters["receiverName"].ToString();
            if (filters.ContainsKey("receiverAddress")) receiverAddress = filters["receiverAddress"].ToString();

            try
            {
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialReceiptMng_function_SearchFactoryMaterialReceipt(orderBy, orderDirection, receiptNo, deliverName, deliverName, stockName, stockAddress, receiverName, receiverAddress).Count();
                    var result = context.FactoryMaterialReceiptMng_function_SearchFactoryMaterialReceipt(orderBy, orderDirection, receiptNo, deliverName, deliverName, stockName, stockAddress, receiverName, receiverAddress);
                    searchFormData.Data = converter.DB2DTO_FactoryMaterialReceiptSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.FactoryMaterialReceipt dtoFactoryMaterialReceipt = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryMaterialReceipt>();
            dtoFactoryMaterialReceipt.UpdatedBy = userId;
            try
            {
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    FactoryMaterialReceipt dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryMaterialReceipt();
                        context.FactoryMaterialReceipt.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryMaterialReceipt.Where(o => o.FactoryMaterialReceiptID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //
                        if (dtoFactoryMaterialReceipt.ReceiptTypeID == 2 && !dtoFactoryMaterialReceipt.FactoryTeamID.HasValue) // 2 : Export
                        {
                            throw new Exception("Team is empty. You should fill-in Team");
                        }

                        if (dtoFactoryMaterialReceipt.FactoryMaterialReceiptDetails.Where(o => !o.FactoryAreaID.HasValue).Count() > 0)
                        {
                            throw new Exception("Area is empty. All product must be fill-in Area");
                        }

                        //validate quantity
                        if (dtoFactoryMaterialReceipt.ReceiptTypeID == 1) // import receipt
                        {
                            ValidateQntImport(dtoFactoryMaterialReceipt);
                        }
                        else if (dtoFactoryMaterialReceipt.ReceiptTypeID == 2) // export receipt
                        {
                            ValidateQntExport(dtoFactoryMaterialReceipt);
                        }

                        //convert dto to db
                        converter.DTO2DB_FactoryMaterialReceipt(dtoFactoryMaterialReceipt, ref dbItem);
                        //remove orphan item
                        context.FactoryMaterialReceiptDetail.Local.Where(o => o.FactoryMaterialReceipt == null).ToList().ForEach(o => context.FactoryMaterialReceiptDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //update receipt no
                        //context.FactoryMaterialReceiptMng_function_GenerateReceipNo(dbItem.FactoryMaterialReceiptID, dbItem.Season, dbItem.ReceiptTypeID);
                        //get return data
                        dtoItem = GetData(dbItem.FactoryMaterialReceiptID, out notification).Data;
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

        public DTO.StockRemains QuickSearchStockRemain(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.StockRemains data = new DTO.StockRemains();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string factoryMaterialUD = string.Empty;
            string factoryMaterialNM = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;
            string clientUD = string.Empty;
            string proformaInvoiceNo = string.Empty;
            int? factoryMaterialID = null;
            int? factoryAreaID = null;
            int? factoryTeamID = null;

            if (filters.ContainsKey("factoryMaterialUD")) factoryMaterialUD = filters["factoryMaterialUD"].ToString();
            if (filters.ContainsKey("factoryMaterialNM")) factoryMaterialNM = filters["factoryMaterialNM"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();

            if (filters.ContainsKey("factoryMaterialID") && filters["factoryMaterialID"] != null) factoryMaterialID = Convert.ToInt32(filters["factoryMaterialID"]);
            if (filters.ContainsKey("factoryAreaID") && filters["factoryAreaID"] != null) factoryAreaID = Convert.ToInt32(filters["factoryAreaID"]);
            if (filters.ContainsKey("factoryTeamID") && filters["factoryTeamID"] != null) factoryTeamID = Convert.ToInt32(filters["factoryTeamID"]); // factory team allway set when type is export

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
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialReceiptMng_function_QuickSearchStockRemain(sortedBy, sortedDirection, factoryMaterialUD, factoryMaterialNM, factoryFinishedProductUD, factoryFinishedProductNM, articleCode, description, clientUD, proformaInvoiceNo,factoryMaterialID, factoryAreaID, factoryTeamID).Count();
                    var result = context.FactoryMaterialReceiptMng_function_QuickSearchStockRemain(sortedBy, sortedDirection, factoryMaterialUD, factoryMaterialNM, factoryFinishedProductUD, factoryFinishedProductNM, articleCode, description, clientUD, proformaInvoiceNo, factoryMaterialID, factoryAreaID, factoryTeamID);
                    data.Data = converter.DB2DTO_StockRemain(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public void ValidateQntImport(DTO.FactoryMaterialReceipt dtoReceipt)
        {
            if (dtoReceipt.FactoryMaterialReceiptDetails != null && dtoReceipt.FactoryMaterialReceiptDetails.Where(o =>o.FactoryMaterialReceiptDetailID > 0).Count() > 0)
            {
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    /* 
                     * check import free item
                     */
                    List<DTO.FactoryMaterialReceiptDetail> dtoDetail_Free = dtoReceipt.FactoryMaterialReceiptDetails.Where(o => o.FactoryOrderDetailID.HasValue == false && o.FactoryMaterialReceiptDetailID > 0).ToList();
                    List<int?> receiptDetailIDs_Free = dtoDetail_Free.Select(x => x.FactoryMaterialReceiptDetailID).ToList();
                    List<string> keyIDs = dtoDetail_Free.Select(x => x.FactoryMaterialID.ToString() + "_" + x.FactoryAreaID.ToString()).ToList();
                    var dbReceiptDetail_Free = context.FactoryMaterialReceiptDetail.Where(o => receiptDetailIDs_Free.Contains(o.FactoryMaterialReceiptDetailID)).ToList();
                    var dbStock_Free = context.FactoryMaterialReceiptMng_StockFree_View.Where(o => keyIDs.Contains(o.FactoryMaterialID.ToString() + "_" + o.FactoryAreaID.ToString())).ToList();

                    //begin compare to check
                    decimal? stockRemainQnt = 0;
                    decimal? totalImportedQnt = 0;
                    foreach (var item in dtoDetail_Free)
                    {
                        var receiptDetailFreeItem = dbReceiptDetail_Free.Where(o => o.FactoryMaterialReceiptDetailID == item.FactoryMaterialReceiptDetailID).FirstOrDefault();
                        var stockFreeItem = dbStock_Free.Where(o => o.FactoryMaterialID == item.FactoryMaterialID && o.FactoryAreaID == item.FactoryAreaID).FirstOrDefault();
                        stockRemainQnt = (stockFreeItem == null ? 0 : stockFreeItem.TotalStockQnt);
                        if (stockRemainQnt == 0) // stock is empting
                        {
                            if (item.Quantity < receiptDetailFreeItem.Quantity)
                            {
                                throw new Exception("You can not edit quantity of material : " + item.FactoryMaterialUD + ". Because material was exported");
                            }
                        }
                        else
                        {
                            totalImportedQnt = stockFreeItem.TotalImportedQnt - receiptDetailFreeItem.Quantity + item.Quantity;
                            if (totalImportedQnt < stockFreeItem.TotalExportedQnt)
                            {
                                throw new Exception("You can not edit quantity of material : " + item.FactoryMaterialUD + ". Because material was exported");
                            }
                        }
                    }

                    /* 
                     * check import order item
                     */
                    List<DTO.FactoryMaterialReceiptDetail> dtoDetail_Order = dtoReceipt.FactoryMaterialReceiptDetails.Where(o => o.FactoryOrderDetailID.HasValue == true && o.FactoryMaterialReceiptDetailID > 0).ToList();
                    List<int?> receiptDetailIDs_Order = dtoDetail_Order.Select(x => x.FactoryMaterialReceiptDetailID).ToList();
                    List<string> keyIDs_Order = dtoDetail_Order.Select(o => o.FactoryOrderDetailID.Value.ToString() + "_" + o.FactoryFinishedProductID.Value.ToString() + "_ " + o.FactoryMaterialID.Value.ToString() + "_" + o.FactoryMaterialOrderNormID.Value.ToString() + "_" + o.FactoryAreaID.Value.ToString()).ToList();
                    var dbReceiptDetail_Order = context.FactoryMaterialReceiptDetail.Where(o => receiptDetailIDs_Order.Contains(o.FactoryMaterialReceiptDetailID)).ToList();
                    var dbStock_Order = context.FactoryMaterialReceiptMng_StockRemain_View.Where(o => keyIDs_Order.Contains(o.FactoryOrderDetailID.Value.ToString() + "_" + o.FactoryFinishedProductID.Value.ToString() + "_ " + o.FactoryMaterialID.Value.ToString() + "_" + o.FactoryMaterialOrderNormID.Value.ToString() + "_" + o.FactoryAreaID.Value.ToString())).ToList();

                    //begin compare to check
                    decimal? stockQnt_Order = 0;
                    decimal? totalImportedQnt_Order = 0;
                    foreach (var item in dtoDetail_Order)
                    {
                        var receiptDetailOrderItem = dbReceiptDetail_Order.Where(o => o.FactoryMaterialReceiptDetailID == item.FactoryMaterialReceiptDetailID).FirstOrDefault();
                        var stockOrderItem = dbStock_Order.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID &&
                                                                        o.FactoryFinishedProductID == item.FactoryFinishedProductID &&
                                                                        o.FactoryMaterialID == item.FactoryMaterialID &&
                                                                        o.FactoryMaterialOrderNormID == item.FactoryMaterialOrderNormID &&
                                                                        o.FactoryAreaID == item.FactoryAreaID).FirstOrDefault();

                        stockQnt_Order = (stockOrderItem == null ? 0 : stockOrderItem.TotalStockQnt);
                        if (stockQnt_Order == 0) // stock is empting
                        {
                            if (item.Quantity < receiptDetailOrderItem.Quantity)
                            {
                                throw new Exception("You can not edit quantity of material : " + item.ClientUD + " - " + item.ProformaInvoiceNo + " - " + item.FactoryMaterialUD + ". Because material was exported");
                            }
                        }
                        else
                        {
                            totalImportedQnt_Order = stockOrderItem.TotalImportedQnt - receiptDetailOrderItem.Quantity + item.Quantity;
                            if (totalImportedQnt_Order < stockOrderItem.TotalExportedQnt)
                            {
                                throw new Exception("You can not edit quantity of material : " + item.ClientUD + " - " + item.ProformaInvoiceNo + " - " + item.FactoryMaterialUD + ". Because material was exported");
                            }
                        }
                    }
                }
            }
        }

        public void ValidateQntExport(DTO.FactoryMaterialReceipt dtoReceipt) 
        {
            if (dtoReceipt.FactoryMaterialReceiptDetails != null && dtoReceipt.FactoryMaterialReceiptDetails.Count() > 0)
            { 
                //create a copy of dto detail
                List<DTO.FactoryMaterialReceiptDetail> dtoDetails = new List<DTO.FactoryMaterialReceiptDetail>();
                foreach (var item in dtoReceipt.FactoryMaterialReceiptDetails)
                {
                    dtoDetails.Add(new DTO.FactoryMaterialReceiptDetail()
                    {
                        FactoryMaterialReceiptDetailID = item.FactoryMaterialReceiptDetailID,
                        FactoryOrderDetailID = item.FactoryOrderDetailID,
                        FactoryFinishedProductID = item.FactoryFinishedProductID,
                        FactoryMaterialID = item.FactoryMaterialID,
                        FactoryMaterialOrderNormID = item.FactoryMaterialOrderNormID,
                        FactoryAreaID = item.FactoryAreaID,
                        Quantity = item.Quantity,
                    });
                    
                    //add child item
                    foreach (var sItem in item.FactoryMaterialReceiptDetails)
                    {
                        dtoDetails.Add(new DTO.FactoryMaterialReceiptDetail()
                        {
                            FactoryMaterialReceiptDetailID = sItem.FactoryMaterialReceiptDetailID,
                            FactoryOrderDetailID = sItem.FactoryOrderDetailID,
                            FactoryFinishedProductID = sItem.FactoryFinishedProductID,
                            FactoryMaterialID = sItem.FactoryMaterialID,
                            FactoryMaterialOrderNormID = sItem.FactoryMaterialOrderNormID,
                            FactoryAreaID = sItem.FactoryAreaID,
                            Quantity = sItem.Quantity,
                        });
                    }
                }
                //
                using (FactoryMaterialReceiptEntities context = CreateContext()) 
                {
                    //adjust dto quantity in case factoryMaterialReceiptDetailID > 0 
                    List<int?> factoryMaterialReceiptDetailIDs = dtoDetails.Where(o => o.FactoryMaterialReceiptDetailID > 0).Select(s => s.FactoryMaterialReceiptDetailID).ToList();
                    var dbDetails = context.FactoryMaterialReceiptDetail.Where(o => factoryMaterialReceiptDetailIDs.Contains(o.FactoryMaterialReceiptDetailID)).ToList();
                    foreach (var item in dtoDetails.Where(o => o.FactoryMaterialReceiptDetailID > 0))
                    {
                        var s = dbDetails.Where(x => x.FactoryMaterialReceiptDetailID == item.FactoryMaterialReceiptDetailID).FirstOrDefault();
                        item.Quantity = item.Quantity - (s.Quantity.HasValue ? s.Quantity.Value : 0);
                    }
                    //get free items
                    var dtoDetail_Free = from item in dtoDetails.Where(o => o.FactoryOrderDetailID.HasValue == false)
                                         group item by new { item.FactoryMaterialID, item.FactoryAreaID } into g
                                         select new { g.Key.FactoryMaterialID, g.Key.FactoryAreaID, TotalExportQnt = g.Sum(o => o.Quantity) };

                    //check with free stock
                    List<string> keyIDs = dtoDetail_Free.Select(o => o.FactoryMaterialID.Value.ToString() + "_" + o.FactoryAreaID.Value.ToString()).ToList();
                    var dbStockFree = context.FactoryMaterialReceiptMng_StockFree_View.Where(o => keyIDs.Contains(o.FactoryMaterialID.ToString() + "_" + o.FactoryAreaID.ToString())).ToList();
                    foreach (var item in dtoDetail_Free)
                    {
                        var x = dbStockFree.Where(o => o.FactoryMaterialID == item.FactoryMaterialID && o.FactoryAreaID == item.FactoryAreaID).FirstOrDefault();
                        if (item.TotalExportQnt > (x == null ? 0 : x.TotalStockQnt))
                        {
                            var s = dtoReceipt.FactoryMaterialReceiptDetails.Where(o => o.FactoryMaterialID == item.FactoryMaterialID && o.FactoryAreaID == item.FactoryAreaID).FirstOrDefault();
                            throw new Exception("The total quantity of material must be less than total quantity in stock." + (s == null ? "" : s.FactoryMaterialUD + " - " + s.FactoryAreaNM));
                        }
                    }

                    //get order items
                    var dtoDetail_Order = from item in dtoDetails.Where(o => o.FactoryOrderDetailID.HasValue == true)
                                          group item by new { item.FactoryMaterialID, item.FactoryAreaID, item.FactoryOrderDetailID, item.FactoryFinishedProductID, item.FactoryMaterialOrderNormID } into g
                                          select new { g.Key.FactoryOrderDetailID, g.Key.FactoryFinishedProductID, g.Key.FactoryMaterialID, g.Key.FactoryMaterialOrderNormID, g.Key.FactoryAreaID, TotalExportQnt = g.Sum(o => o.Quantity) };
                    //check with order stock remain
                    keyIDs = dtoDetail_Order.Select(o => o.FactoryOrderDetailID.Value.ToString() + "_" + o.FactoryFinishedProductID.Value.ToString() + "_ " + o.FactoryMaterialID.Value.ToString() + "_" + o.FactoryMaterialOrderNormID.Value.ToString() + "_" + o.FactoryAreaID.Value.ToString()).ToList();
                    var dbStockOrder = context.FactoryMaterialReceiptMng_StockRemain_View.Where(o => keyIDs.Contains(o.FactoryOrderDetailID.Value.ToString() + "_" 
                                                                                                                        + o.FactoryFinishedProductID.Value.ToString() + "_ " 
                                                                                                                        + o.FactoryMaterialID.Value.ToString() + "_" 
                                                                                                                        + o.FactoryMaterialOrderNormID.Value.ToString() + "_" 
                                                                                                                        + o.FactoryAreaID.Value.ToString())).ToList();
                    foreach (var item in dtoDetail_Order)
                    {
                        var x = dbStockOrder.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID && 
                                                        o.FactoryFinishedProductID == item.FactoryFinishedProductID && 
                                                        o.FactoryMaterialID == item.FactoryMaterialID && 
                                                        o.FactoryMaterialOrderNormID == item.FactoryMaterialOrderNormID  && 
                                                        o.FactoryAreaID == item.FactoryAreaID).FirstOrDefault();
                        if (item.TotalExportQnt > (x == null ? 0 : x.TotalStockQnt))
                        {
                            var s = dtoReceipt.FactoryMaterialReceiptDetails.Where(o => o.FactoryOrderDetailID == item.FactoryOrderDetailID &&
                                                                                        o.FactoryFinishedProductID == item.FactoryFinishedProductID &&
                                                                                        o.FactoryMaterialID == item.FactoryMaterialID &&
                                                                                        o.FactoryMaterialOrderNormID == item.FactoryMaterialOrderNormID &&
                                                                                        o.FactoryAreaID == item.FactoryAreaID).FirstOrDefault();
                            throw new Exception("The total quantity of material must be less than total quantity in stock." + (s == null ? "" : s.FactoryUD + " - " + s.ProformaInvoiceNo + " - " +  s.FactoryMaterialUD + " - " + s.FactoryAreaNM));
                        }
                    }
                
                }
            }
        }

        public DTO.NeedImportByOrders SearchToImportByOrder(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.NeedImportByOrders data = new DTO.NeedImportByOrders();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string factoryMaterialUD = string.Empty;
            string factoryMaterialNM = string.Empty;
            string factoryFinishedProductUD = string.Empty;
            string factoryFinishedProductNM = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;
            string clientUD = string.Empty;
            string proformaInvoiceNo = string.Empty;

            if (filters.ContainsKey("factoryMaterialUD")) factoryMaterialUD = filters["factoryMaterialUD"].ToString();
            if (filters.ContainsKey("factoryMaterialNM")) factoryMaterialNM = filters["factoryMaterialNM"].ToString();
            if (filters.ContainsKey("factoryFinishedProductUD")) factoryFinishedProductUD = filters["factoryFinishedProductUD"].ToString();
            if (filters.ContainsKey("factoryFinishedProductNM")) factoryFinishedProductNM = filters["factoryFinishedProductNM"].ToString();
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();

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
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialReceiptMng_function_SearchToImportByOrder(sortedBy, sortedDirection, factoryMaterialUD, factoryMaterialNM, factoryFinishedProductUD, factoryFinishedProductNM, articleCode, description, clientUD, proformaInvoiceNo).Count();
                    var result = context.FactoryMaterialReceiptMng_function_SearchToImportByOrder(sortedBy, sortedDirection, factoryMaterialUD, factoryMaterialNM, factoryFinishedProductUD, factoryFinishedProductNM, articleCode, description, clientUD, proformaInvoiceNo);
                    data.Data = converter.DB2DTO_NeedImportByOrder(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public DTO.StockFrees SearchStockFree(Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.StockFrees data = new DTO.StockFrees();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string factoryMaterialUD = string.Empty;
            string factoryMaterialNM = string.Empty;
            int? factoryMaterialID = null;
            int? factoryAreaID = null;
            int? factoryTeamID = null;

            if (filters.ContainsKey("factoryMaterialUD")) factoryMaterialUD = filters["factoryMaterialUD"].ToString();
            if (filters.ContainsKey("factoryMaterialNM")) factoryMaterialNM = filters["factoryMaterialNM"].ToString();

            if (filters.ContainsKey("factoryMaterialID") && filters["factoryMaterialID"] != null) factoryMaterialID = Convert.ToInt32(filters["factoryMaterialID"]);
            if (filters.ContainsKey("factoryAreaID") && filters["factoryAreaID"] != null) factoryAreaID = Convert.ToInt32(filters["factoryAreaID"]);
            if (filters.ContainsKey("factoryTeamID") && filters["factoryTeamID"] != null) factoryTeamID = Convert.ToInt32(filters["factoryTeamID"]); // factory team allway set when type is export

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
                using (FactoryMaterialReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialReceiptMng_function_action_SearchStockFree(sortedBy, sortedDirection, factoryMaterialUD, factoryMaterialNM, factoryMaterialID, factoryAreaID, factoryTeamID).Count();
                    var result = context.FactoryMaterialReceiptMng_function_action_SearchStockFree(sortedBy, sortedDirection, factoryMaterialUD, factoryMaterialNM, factoryMaterialID, factoryAreaID, factoryTeamID);
                    data.Data = converter.DB2DTO_StockFree(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public string GetReceiptPrintout(int factoryMaterialReceiptID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryMaterialReceiptMng_function_GetReceiptPrintout", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryMaterialReceiptID", factoryMaterialReceiptID);
                adap.TableMappings.Add("Table", "Receipt");
                adap.TableMappings.Add("Table1", "ReceiptDetail");
                adap.Fill(ds);

                return Library.Helper.CreateReceiptPrintout(ds.Tables["Receipt"], ds.Tables["ReceiptDetail"], "FactoryMaterialReceiptPrintout.rdlc");
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
                return string.Empty;
            }
        }

    }
}
