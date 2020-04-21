using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryStockReceipt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryStockReceiptEntities CreateContext()
        {
            return new FactoryStockReceiptEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryStockReceiptModel"));
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
                using (FactoryStockReceiptEntities context = CreateContext())
                {
                    var dbItem = context.FactoryStockReceipt.Where(o => o.FactoryStockReceiptID == id).FirstOrDefault();
                    context.FactoryStockReceipt.Remove(dbItem);
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
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryStockReceipt dtoFactoryStockReceipt = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryStockReceipt>();
            dtoFactoryStockReceipt.UpdatedBy = userId;
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            try
            {
                using (FactoryStockReceiptEntities context = CreateContext())
                {
                    FactoryStockReceipt dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryStockReceipt();
                        context.FactoryStockReceipt.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryStockReceipt.Where(o => o.FactoryStockReceiptID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //
                        if (!dtoFactoryStockReceipt.FactoryID.HasValue) // 2 : Export
                        {
                            throw new Exception("Factory is empty. You should fill-in factory");
                        }
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(userId, dtoFactoryStockReceipt.FactoryID.Value) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }

                        //validate quantity
                        if (dtoFactoryStockReceipt.ReceiptTypeID == 1) //import
                        {
                            ValidateQntImport(dtoFactoryStockReceipt);
                        }
                        else if(dtoFactoryStockReceipt.ReceiptTypeID == 2)  //export
                        {
                            ValidateQntExport(dtoFactoryStockReceipt);
                        }

                        //convert dto to db
                        converter.DTO2DB_FactoryStockReceipt(dtoFactoryStockReceipt, ref dbItem);
                        //remove orphan item
                        context.FactoryStockReceiptDetail.Local.Where(o => o.FactoryStockReceipt == null).ToList().ForEach(o => context.FactoryStockReceiptDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //update receipt no
                        context.FactoryStockReceiptMng_function_GenerateReceipNo(dbItem.FactoryStockReceiptID, dbItem.ReceiptTypeID);
                        //get return data
                        dtoItem = GetData(userId,dbItem.FactoryStockReceiptID, out notification).Data;
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

        public DTO.StockRemains QuickSearchStockRemain(int userID, Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.StockRemains data = new DTO.StockRemains();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int totalRows = 0;

            //search info
            string articleCode = string.Empty;
            string description = string.Empty;
            int? factoryID = null;

            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null) factoryID = Convert.ToInt32(filters["factoryID"].ToString());

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
                using (FactoryStockReceiptEntities context = CreateContext())
                {
                    totalRows = context.FactoryStockReceiptMng_function_QuickSearchStockRemain(sortedBy, sortedDirection, articleCode, description, factoryID, userID).Count();
                    var result = context.FactoryStockReceiptMng_function_QuickSearchStockRemain(sortedBy, sortedDirection, articleCode, description, factoryID, userID);
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

        public void ValidateQntImport(DTO.FactoryStockReceipt dtoReceipt)
        {
            int? factoryID = dtoReceipt.FactoryID;
            int? stockQnt = 0;
            int? inputQnt = 0;
            int? delta = 0;
            using (FactoryStockReceiptEntities context = CreateContext())
            {
                foreach (var item in dtoReceipt.FactoryStockReceiptDetails.Where(o => o.FactoryStockReceiptDetailID > 0))
                {
                    inputQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                    var dbCurrent = context.FactoryStockReceiptDetail.Where(o => o.FactoryStockReceiptDetailID == item.FactoryStockReceiptDetailID).FirstOrDefault();
                    delta = inputQnt - (dbCurrent == null ? 0 : (dbCurrent.Quantity.HasValue ? dbCurrent.Quantity.Value : 0));
                    if (delta < 0) // input < current
                    {
                        delta = -delta;
                        var dbStock = context.FactoryStockReceiptMng_StockRemain_View.Where(o => o.ProductID == item.ProductID && o.FactoryID == factoryID).FirstOrDefault();
                        stockQnt = (dbStock == null ? 0 : (dbStock.TotalStockQnt.HasValue ? dbStock.TotalStockQnt.Value : 0));
                        if (delta > stockQnt)
                        {
                            throw new Exception("You can not save data with negative stock beause when you are editing quantity for product: " + item.ArticleCode + "(" + item.Description + ") to " + inputQnt.ToString() + " then stock quantity = " + (stockQnt - delta).ToString());
                        }
                    }
                }
            }            
        }

        public void ValidateQntExport(DTO.FactoryStockReceipt dtoReceipt)
        {
            int? factoryID = dtoReceipt.FactoryID;
            int? stockQnt = 0;
            int? inputQnt = 0;
            using (FactoryStockReceiptEntities context = CreateContext())
            {
                //validate product qnt
                foreach (var item in dtoReceipt.FactoryStockReceiptDetails)
                {
                    inputQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                    if (item.FactoryStockReceiptDetailID > 0)
                    {
                        var dbCurrent = context.FactoryStockReceiptDetail.Where(o => o.FactoryStockReceiptDetailID == item.FactoryStockReceiptDetailID).FirstOrDefault();
                        inputQnt = inputQnt - (dbCurrent == null ? 0 : (dbCurrent.Quantity.HasValue ? dbCurrent.Quantity.Value : 0));
                    }
                    var dbStock = context.FactoryStockReceiptMng_StockRemain_View.Where(o =>o.ProductID == item.ProductID && o.FactoryID == factoryID).FirstOrDefault();
                    stockQnt = (dbStock == null ? 0 : (dbStock.TotalStockQnt.HasValue ? dbStock.TotalStockQnt.Value : 0));
                    if (inputQnt > stockQnt)
                    {
                        throw new Exception("You can not save because quantity (" + (item.Quantity.HasValue ? item.Quantity : 0) + " pieces) of product " + item.ArticleCode + " (" + item.Description + " )" + " is greater than current physical stock (" + stockQnt.ToString() + " pieces)");
                    }
                }
            }                        
        }

        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            try
            {
                using (FactoryStockReceiptEntities context = CreateContext())
                {
                    //auto create production item
                    context.FactoryStockReceiptMng_function_CreateProductionItem();
                    if (id > 0)
                    {                        
                        FactoryStockReceiptMng_FactoryStockReceipt_View dbItem;
                        dbItem = context.FactoryStockReceiptMng_FactoryStockReceipt_View.FirstOrDefault(o => o.FactoryStockReceiptID == id);
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(userId, dbItem.FactoryID.Value) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                        editFormData.Data = converter.DB2DTO_FactoryStockReceipt(dbItem);                        
                    }
                    else
                    {
                        editFormData.Data = new DTO.FactoryStockReceipt();
                        editFormData.Data.FactoryStockReceiptDetails = new List<DTO.FactoryStockReceiptDetail>();
                    }
                    editFormData.Factories = support_factory.GetFactory(userId);

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

        public  DTO.SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string receiptNo = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;
            int? factoryID = null;

            if (filters.ContainsKey("receiptNo")) receiptNo = filters["receiptNo"].ToString();
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null) factoryID = Convert.ToInt32(filters["factoryID"]);

            try
            {
                using (FactoryStockReceiptEntities context = CreateContext())
                {                    
                    //search
                    totalRows = context.FactoryStockReceiptMng_function_SearchFactoryStockReceipt(orderBy, orderDirection, receiptNo, articleCode, description, factoryID,userID).Count();
                    var result = context.FactoryStockReceiptMng_function_SearchFactoryStockReceipt(orderBy, orderDirection, receiptNo, articleCode, description, factoryID, userID);
                    searchFormData.Data = converter.DB2DTO_FactoryStockReceiptSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
    }
}
