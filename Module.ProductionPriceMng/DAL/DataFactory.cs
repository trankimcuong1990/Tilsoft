using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Module.ProductionPriceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();

        public DataFactory() { }

        private ProductionPriceMngEntities CreateContext()
        {
            return new ProductionPriceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionPriceMngModel"));
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
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    var dbItem = context.ProductionPrice.Where(o => o.ProductionPriceID == id).FirstOrDefault();
                    context.ProductionPrice.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return false;
            }

        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    ProductionPriceMng_ProductionPrice_View dbItem;
                    dbItem = context.ProductionPriceMng_ProductionPrice_View.FirstOrDefault(o => o.ProductionPriceID == id);
                    editFormData.Data = converter.DB2DTO_ProductionPrice(dbItem);
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return editFormData;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new Exception();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductionPriceDTO dtoProductionPrice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionPriceDTO>();
            try
            {
                int? companyID = fw_factory.GetCompanyID(userId);
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    ProductionPrice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionPrice();
                        context.ProductionPrice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionPrice.Where(o => o.ProductionPriceID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_ProductionPrice(dtoProductionPrice, ref dbItem, userId, companyID);
                        //remove orphan
                        context.ProductionPriceDetail.Local.Where(o => o.ProductionPrice == null).ToList().ForEach(o => context.ProductionPriceDetail.Remove(o));

                        //save data
                        context.SaveChanges();

                        // Run automatic update unit price
                        if (dtoProductionPrice.UpdateTypeID.HasValue && dtoProductionPrice.UpdateTypeID.Value == 2)
                        {
                            context.ProductionPriceMng_function_UpdatePriceReceivingNote(dbItem.ProductionPriceID);
                        }

                        //get return data
                        dtoItem = GetData(dbItem.ProductionPriceID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return false;
            }
        }

        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        ProductionPriceMng_ProductionPrice_View dbItem;
                        dbItem = context.ProductionPriceMng_ProductionPrice_View.FirstOrDefault(o => o.ProductionPriceID == id);
                        editFormData.Data = converter.DB2DTO_ProductionPrice(dbItem);
                    }
                    else
                    {
                        //initialize item data base on selected workorder
                        int productionItemTypeID = Convert.ToInt32(param["productionItemTypeID"]);
                        int month = Convert.ToInt32(param["month"]);
                        int year = Convert.ToInt32(param["year"]);

                        editFormData.Data.ProductionItemTypeID = productionItemTypeID;
                        editFormData.Data.Month = month;
                        editFormData.Data.Year = year;
                        editFormData.Data.ProductionItemTypeNM = new Module.Support.DAL.DataFactory().GetProductionItemType().Where(o => o.ProductionItemTypeID == productionItemTypeID).FirstOrDefault().ProductionItemTypeNM;

                        //calculate average price of all receiving receipt
                        editFormData.Data.ProductionPriceID = this.CalculateAvgPrice(userId, productionItemTypeID, month, year, out notification);                        
                    }
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return editFormData;
            }
        }

        public DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                int? companyID = fw_factory.GetCompanyID(userId);
                int? productionItemTypeID = null;
                int? month = null;
                int? year = null;
                bool? isLocked = null;

                if (filters.ContainsKey("productionItemTypeID") && filters["productionItemTypeID"] != null)
                {
                    productionItemTypeID = Convert.ToInt32(filters["productionItemTypeID"]);
                }
                if (filters.ContainsKey("month") && filters["month"] != null)
                {
                    month = Convert.ToInt32(filters["month"]);
                }
                if (filters.ContainsKey("year") && filters["year"] != null)
                {
                    year = Convert.ToInt32(filters["year"]);
                }
                if (filters.ContainsKey("isLocked") && filters["isLocked"] != null && !string.IsNullOrEmpty(filters["isLocked"].ToString()))
                {
                    switch (filters["isLocked"].ToString().ToLower())
                    {
                        case "true":
                            isLocked = true;
                            break;
                        case "false":
                            isLocked = false;
                            break;
                        default:
                            isLocked = null;
                            break;
                    }
                }
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    totalRows = context.ProductionPriceMng_function_SearchProductionPrice(orderBy, orderDirection, companyID, productionItemTypeID, month, year, isLocked).Count();
                    var result = context.ProductionPriceMng_function_SearchProductionPrice(orderBy, orderDirection, companyID, productionItemTypeID, month, year, isLocked);
                    searchFormData.Data = converter.DB2DTO_ProductionPriceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return searchFormData;
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return searchFormData;
            }
        }

        public DTO.SearchFormFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormFilterData data = new DTO.SearchFormFilterData();
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                data.ProductionItemTypes = support_factory.GetProductionItemType();
                data.Months = Library.Helper.GetMonths();
                data.Years = Library.Helper.GetYears();
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
            }
            return data;
        }

        public int? CalculateAvgPrice(int userId, int productionItemTypeID, int month, int year, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                DateTime currentDate = new DateTime(year, month, 1);
                DateTime previousDate = currentDate.AddMonths(-1);

                using (ProductionPriceMngEntities context = CreateContext())
                {
                    int? companyID = fw_factory.GetCompanyID(userId);
                    List<DTO.AvgPriceDTO> data = new List<DTO.AvgPriceDTO>();
                    var x = context.ProductionPriceMng_function_CalculateAvgPrice(userId, companyID, productionItemTypeID, currentDate.Month, currentDate.Year, previousDate.Month, previousDate.Year).ToList();
                    return x.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return null;
            }
        }

        private bool SaveDataBeforeLock(int userId, int id, DTO.ProductionPriceDTO dtoProductionPrice, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                int? companyID = fw_factory.GetCompanyID(userId);
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    ProductionPrice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionPrice();
                        context.ProductionPrice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionPrice.Where(o => o.ProductionPriceID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_ProductionPrice(dtoProductionPrice, ref dbItem, userId, companyID);
                        //remove orphan
                        context.ProductionPriceDetail.Local.Where(o => o.ProductionPrice == null).ToList().ForEach(o => context.ProductionPriceDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return false;
            }
        }

        public bool LockPrice(int userId, int id, bool isLocked, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ProductionPrice.FirstOrDefault(o => o.ProductionPriceID == id);
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    dbItem.IsLocked = isLocked;
                    dbItem.LockedBy = userId;
                    dbItem.LockedDate = DateTime.Now;

                    if (isLocked)
                    {
                        //get current price
                        //DTO.ProductionPriceDTO curentPrice = this.GetData(id, out notification).Data;

                        //get price after calculate avg price
                        //List<DTO.AvgPriceDTO> avgPrices = this.CalculateAvgPrice(userId, dbItem.ProductionItemTypeID.Value, dbItem.Month.Value, dbItem.Year.Value, out notification);
                        //curentPrice.ProductionPriceDetailDTOs = AutoMapper.Mapper.Map<List<DTO.AvgPriceDTO>, List<DTO.ProductionPriceDetailDTO>>(avgPrices);

                        //update new price
                        //if (this.SaveDataBeforeLock(userId, id, curentPrice, out notification)) {
                        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Locked price success" };
                        //    //lock price
                        //    context.SaveChanges();
                        //}
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Locked price success" };
                        //lock price
                        context.SaveChanges();
                    }
                    else
                    {
                        //lock price
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "UnLocked price success" };
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
            }
            return true;
        }

        public List<DTO.ReceiptByProductionItem> GetReceiptByProductionItem(int userId, int productionItemID, int month, int year, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                int? companyID = fw_factory.GetCompanyID(userId);
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    List<DTO.ReceiptByProductionItem> data = new List<DTO.ReceiptByProductionItem>();
                    var x = context.ProductionPriceMng_function_GetReceiptByProductionItem(companyID, productionItemID, month, year).ToList();
                    data = AutoMapper.Mapper.Map<List<ProductionPriceMng_ReceiptByProductionItem_View>, List<DTO.ReceiptByProductionItem>>(x);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return null;
            }
        }

        public int? ApplyPriceOnReceipt(int productionPriceID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProductionPriceMngEntities context = CreateContext())
                {
                    int result = context.ProductionPriceMng_function_UpdatePriceReceivingNote(productionPriceID);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Type = NotificationType.Error;
                notification.Message = iEx.Message;
                return null;
            }
        }

    }

}
