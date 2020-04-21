using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FactoryProformaInvoiceMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.FactoryProformaInvoiceMng.SearchFormData, DTO.FactoryProformaInvoiceMng.EditFormData, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private FactoryProformaInvoiceMngEntities CreateContext()
        {
            return new FactoryProformaInvoiceMngEntities(DALBase.Helper.CreateEntityConnectionString("FactoryProformaInvoiceMngModel"));
        }

        public override DTO.FactoryProformaInvoiceMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryProformaInvoiceMng.SearchFormData data = new DTO.FactoryProformaInvoiceMng.SearchFormData();
            data.Data = new List<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceSearchResult>();
            totalRows = 0;

            string FactoryUD = null;
            string ProformaInvoiceNo = null;
            string Season = null;
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    totalRows = context.FactoryProformaInvoiceMng_function_SearchFactoryProformaInvoice(FactoryUD, ProformaInvoiceNo, Season, orderBy, orderDirection).Count();
                    var result = context.FactoryProformaInvoiceMng_function_SearchFactoryProformaInvoice(FactoryUD, ProformaInvoiceNo, Season, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryProformaInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.FactoryProformaInvoiceMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int id, ref DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    FactoryProformaInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryProformaInvoice();
                        context.FactoryProformaInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryProformaInvoice.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check if detail is valid
                        int factoryOrderID = dtoItem.FactoryOrderID.Value;
                        var dtoCheckInfo = context.FactoryProformaInvoiceMng_FactoryOrderInfo_View.Where(o => o.FactoryOrderID == factoryOrderID).ToList();
                        foreach (DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail dtoDetail in dtoItem.Details)
                        {
                            if (dtoDetail.FactoryOrderDetailID.HasValue && dtoDetail.FactoryOrderDetailID > 0 && dtoCheckInfo.Count(o => o.FactoryOrderDetailID == dtoDetail.FactoryOrderDetailID.Value) == 0)
                            {
                                throw new Exception("Invalid detail: " + dtoDetail.ArticleCode + " not exists in factory order "+dtoItem.FactoryUD+" !");
                            }
                        }

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.FactoryProformaInvoiceDetail.Local.Where(o => o.FactoryProformaInvoice == null).ToList().ForEach(o => context.FactoryProformaInvoiceDetail.Remove(o));
                        context.SaveChanges();

                        // processing file
                        if (dtoItem.AttachedFile_HasChange)
                        {
                            dbItem.AttachedFile = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoItem.AttachedFile_NewFile, dtoItem.AttachedFile);
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dtoItem.UpdatedBy.Value, dbItem.FactoryProformaInvoiceID, 0, string.Empty, 0, out notification).Data;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override bool Approve(int id, ref DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    FactoryProformaInvoice dbItem = context.FactoryProformaInvoice.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }

                    // check if proforma invoice has attached file
                    if (string.IsNullOrEmpty(dbItem.AttachedFile))
                    {
                        throw new Exception("Attached scaned file is required in order to approve the proforma invoice!");
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = dtoItem.UpdatedBy;
                    dbItem.ConfirmedDate = DateTime.Now;
                    context.SaveChanges();

                    dtoItem = GetData(dtoItem.UpdatedBy.Value, dbItem.FactoryProformaInvoiceID, 0, string.Empty, 0, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override bool Reset(int id, ref DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    FactoryProformaInvoice dbItem = context.FactoryProformaInvoice.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }

                    dbItem.IsConfirmed = null;
                    dbItem.ConfirmedBy = null;
                    dbItem.ConfirmedDate = null;
                    context.SaveChanges();

                    dtoItem = GetData(dtoItem.UpdatedBy.Value, dbItem.FactoryProformaInvoiceID, 0, string.Empty, 0, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        //
        // CUSTOM MODULES
        //
        public DTO.FactoryProformaInvoiceMng.EditFormData GetData(int userId, int id, int factoryId, string season, int factoryOrderId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryProformaInvoiceMng.EditFormData data = new DTO.FactoryProformaInvoiceMng.EditFormData();
            data.Data = new DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice();
            data.Data.Details = new List<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail>();

            //try to get data
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckFactoryProformaInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected proforma invoice data!");
                }

                if (id <= 0 && fwFactory.CheckFactoryPermission(userId, factoryId) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected factory data!");
                }

                if (id <= 0 && fwFactory.CheckFactoryOrderPermission(userId, factoryOrderId) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected factory order data!");
                }

                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        data.Data.Season = season;
                        data.Data.FactoryID = factoryId;
                        DTO.Support.Factory dtoFactory = supportFactory.GetFactory().FirstOrDefault(o => o.FactoryID == factoryId);
                        FactoryProformaInvoiceMng_FactoryOrderSearchResult_View dbFactoryOrder = context.FactoryProformaInvoiceMng_FactoryOrderSearchResult_View.FirstOrDefault(o => o.FactoryOrderID == factoryOrderId);
                        if (dtoFactory != null)
                        {
                            data.Data.FactoryUD = dtoFactory.FactoryUD;
                        }
                        else
                        {
                            throw new Exception("Factory not found!");
                        }

                        if (dbFactoryOrder != null)
                        {
                            data.Data.FactoryOrderID = factoryOrderId;
                            data.Data.FactoryOrderUD = dbFactoryOrder.FactoryOrderUD;

                            // get products
                            int index = -1;
                            foreach (FactoryProformaInvoiceMng_FactoryOrderItemSearchResult_View dbDetail in context.FactoryProformaInvoiceMng_FactoryOrderItemSearchResult_View.Where(o => o.FactoryOrderID == factoryOrderId))
                            {
                                data.Data.Details.Add(new DTO.FactoryProformaInvoiceMng.FactoryProformaInvoiceDetail() { 
                                    FactoryProformaInvoiceDetailID = index, 
                                    FactoryOrderDetailID = dbDetail.FactoryOrderDetailID,
                                    FactoryOrderSparepartDetailID= dbDetail.FactoryOrderSparepartDetailID,
                                    ArticleCode = dbDetail.ArticleCode,
                                    Description = dbDetail.Description,
                                    ClientUD = dbDetail.ClientUD,
                                    FactoryOrderUD = dbDetail.FactoryOrderUD,
                                    LDS = dbDetail.LDS.Value.ToString("dd/MM/yyyy"),
                                    OrderQnt = dbDetail.OrderQnt,
                                    ProductionStatus = dbDetail.ProductionStatus,
                                    PurchasingPrice = dbDetail.PurchasingPrice,
                                    Remark = string.Empty,
                                    UnitPrice = dbDetail.PurchasingPrice
                                });
                                index--;
                            }
                        }
                        else
                        {
                            throw new Exception("Factory order not found!");
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_FactoryProformaInvoice(context.FactoryProformaInvoiceMng_FactoryProformaInvoice_View.FirstOrDefault(o => o.FactoryProformaInvoiceID == id));
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.FactoryProformaInvoiceMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryProformaInvoiceMng.SearchFilterData data = new DTO.FactoryProformaInvoiceMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();

            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.FactoryProformaInvoiceMng.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryProformaInvoiceMng.InitFormData data = new DTO.FactoryProformaInvoiceMng.InitFormData();
            data.Factories = new List<DTO.Support.Factory>();
            data.Seasons = new List<DTO.Support.Season>();

            try
            {
                data.Factories = supportFactory.GetAuthorizedFactories(userId).ToList();
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult> QuickSearchItem(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult> data = new List<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult>();

            //try to get data
            try
            {
                // validate search query
                if (!filters.ContainsKey("Season") || !filters.ContainsKey("FactoryID"))
                {
                    throw new Exception("Invalid search query!");
                }

                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    string ArticleCode = string.Empty;
                    string Description = string.Empty;
                    string FactoryOrderUD = string.Empty;
                    string Season = string.Empty;
                    string ClientUD = string.Empty;
                    int FactoryID = -1;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        ArticleCode = filters["searchQuery"].ToString().Replace("'", "''");
                        Description = ArticleCode;
                        FactoryOrderUD = ArticleCode;
                        ClientUD = ArticleCode;
                    }
                    Season = filters["Season"].ToString().Replace("'", "''");
                    FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());

                    totalRows = context.FactoryProformaInvoiceMng_function_QuickSearchFactoryOrderItem(ArticleCode, Description, FactoryOrderUD, ClientUD, Season, FactoryID, orderBy, orderDirection).Count();
                    var result = context.FactoryProformaInvoiceMng_function_QuickSearchFactoryOrderItem(ArticleCode, Description, FactoryOrderUD, ClientUD, Season, FactoryID, orderBy, orderDirection);
                    data = converter.DB2DTO_FactoryOrderItemSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult> QuickSearchFactoryOrder(int userId, string season, int factoryId, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult> data = new List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult>();

            //try to get data
            try
            {
                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderSearchResultList(context.FactoryProformaInvoiceMng_function_SearchFactoryOrder(userId, season, factoryId, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool DeleteData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckFactoryProformaInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected proforma invoice data!");
                }

                using (FactoryProformaInvoiceMngEntities context = CreateContext())
                {
                    FactoryProformaInvoice dbItem = context.FactoryProformaInvoice.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check if the proforma invoice has been approved
                        if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                        {
                            throw new Exception("Can not delete the confirmed proforma invoice!");
                        }

                        context.FactoryProformaInvoice.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }
    }
}
