using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DAL.WarehouseImportMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.WarehouseImportMng.SearchFormData, DTO.WarehouseImportMng.EditFormData, DTO.WarehouseImportMng.WarehouseImport>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();

        private WarehouseImportMngEntities CreateContext()
        {
            return new WarehouseImportMngEntities(DALBase.Helper.CreateEntityConnectionString("WareHouseImportMngModel"));
        }

        public override DTO.WarehouseImportMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.WarehouseImportMng.SearchFormData data = new DTO.WarehouseImportMng.SearchFormData();
            data.Data = new List<DTO.WarehouseImportMng.WarehouseImportSearchResult>();
            data.ImportTypes = supportFactory.GetWarehouseImportType().ToList();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    string receiptNo = null;
                    string proformaInvoiceNo = null;
                    string clientUD = null;
                    string clientNM = null;
                    string blNo = null;
                    string containerNo = null;
                    string articleCode = null;
                    string description = null;
                    string refNo = null;
                    int? importTypeID = null;

                    if (filters.ContainsKey("ReceiptNo") && !string.IsNullOrEmpty(filters["ReceiptNo"].ToString()))
                    {
                        receiptNo = filters["ReceiptNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        proformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        clientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                    {
                        clientNM = filters["ClientNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
                    {
                        blNo = filters["BLNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ContainerNo") && !string.IsNullOrEmpty(filters["ContainerNo"].ToString()))
                    {
                        containerNo = filters["ContainerNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        description = filters["Description"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("RefNo") && !string.IsNullOrEmpty(filters["RefNo"].ToString()))
                    {
                        refNo = filters["RefNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ImportTypeID") && !string.IsNullOrEmpty(filters["ImportTypeID"].ToString()))
                    {
                        importTypeID = Convert.ToInt32(filters["ImportTypeID"]);
                    }

                    totalRows = context.WarehouseImportMng_function_SearchWarehouseImport(receiptNo, proformaInvoiceNo, clientUD, clientNM, blNo, refNo, containerNo, articleCode, description, importTypeID, orderBy, orderDirection).Count();
                    var result = context.WarehouseImportMng_function_SearchWarehouseImport(receiptNo, proformaInvoiceNo, clientUD, clientNM, blNo, refNo, containerNo, articleCode, description, importTypeID, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_WarehouseImportSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        public override DTO.WarehouseImportMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {            
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.WarehouseImportMng.EditFormData data = new DTO.WarehouseImportMng.EditFormData();
            //try to get data
            try
            {
                data.Data = new DTO.WarehouseImportMng.WarehouseImport();
                data.Data.Details = new List<DTO.WarehouseImportMng.WarehouseImportProductDetail>();
                data.Data.SparepartDetails = new List<DTO.WarehouseImportMng.WarehouseImportSparepartDetail>();
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_WarehouseImport(context.WarehouseImportMng_WarehouseImport_View
                        .Include("WarehouseImportMng_WarehouseImportProductDetail_View.WarehouseImportMng_WarehouseImportAreaDetail_View")
                        .Include("WarehouseImportMng_WarehouseImportProductDetail_View.WarehouseImportMng_WarehouseImportColliDetail_View")
                        .Include("WarehouseImportMng_WarehouseImportSparepartDetail_View.WarehouseImportMng_WarehouseImportAreaDetail_View")
                        .FirstOrDefault(o => o.WarehouseImportID == id));
                    }
                    data.ProductStatuses = supportFactory.GetProductStatus().ToList();
                    data.Season = supportFactory.GetSeason().ToList();
                    data.WareHouseAreas = supportFactory.GetAllWarehouseArea().ToList();
                    data.ProductCollis = converter.DB2DTO_ProductColli(context.WarehouseImportMng_ProductColli_View.ToList());
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
            }
            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    WarehouseImport dbItem = context.WarehouseImport.FirstOrDefault(o => o.WarehouseImportID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Import receipt: " + dbItem.ReceiptNo + " not found!");
                    }
                    else
                    {
                        if (!dbItem.IsConfirmed.HasValue || !dbItem.IsConfirmed.Value)
                        {
                            context.WarehouseImport.Remove(dbItem);
                            context.SaveChanges();
                            result = true;
                        }
                        else
                        {
                            throw new Exception("Can not delete the approved receipt");
                        }
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
            }

            return result;
        }

        public override bool UpdateData(int id, ref DTO.WarehouseImportMng.WarehouseImport dtoItem, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    WarehouseImport dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WarehouseImport();
                        context.WarehouseImport.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WarehouseImport.FirstOrDefault(o => o.WarehouseImportID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Import receipt not found!";
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // check if status = CONFIRMED
                        if (dbItem.IsConfirmed.HasValue)
                        {
                            throw new Exception("Receipt has been confirmed. You can not change");
                        }

                        //validate return quantity
                        string validateMessage = "";
                        //if (!ValidateQuantityReturn(dtoItem, out validateMessage))
                        //{
                        //    throw new Exception(validateMessage);
                        //}

                        // validate onsea quantity
                        if (!ValidateQuantityReserved(dtoItem, out validateMessage))
                        {
                            throw new Exception(validateMessage);
                        }

                        //ValidateQuantityOfArea(dtoItem);

                        //set quantity is wex quantity in case import from wex
                        foreach (var item in dtoItem.Details)
                        {
                            if (dtoItem.IsWexImported.HasValue && dtoItem.IsWexImported.Value)
                            {
                                item.Quantity = item.WexQnt;
                            }
                        }
                        foreach (var item in dtoItem.SparepartDetails)
                        {
                            if (dtoItem.IsWexImported.HasValue && dtoItem.IsWexImported.Value)
                            {
                                item.Quantity = item.WexQnt;
                            }
                        }

                        //set date time
                        DateTime dateValue;
                        if (DateTime.TryParse(dtoItem.ImportedDate, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out dateValue))
                        {
                            dbItem.ImportedDate = dateValue;
                        }

                        if (DateTime.TryParse(dtoItem.ContainerReceivedDate, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out dateValue))
                        {
                            dbItem.ContainerReceivedDate = dateValue;
                        }
                        dbItem.ContainerReceivedTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + dtoItem.ContainerReceivedTime);

                        converter.DTO2DB(dtoItem, ref dbItem);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphans
                        context.WarehouseImportAreaDetail.Local.Where(o => o.WarehouseImportProductDetail == null && o.WarehouseImportSparepartDetail == null).ToList().ForEach(o => context.WarehouseImportAreaDetail.Remove(o));
                        context.WarehouseImportColliDetail.Local.Where(o => o.WarehouseImportProductDetail == null).ToList().ForEach(o => context.WarehouseImportColliDetail.Remove(o));
                        context.WarehouseImportProductDetail.Local.Where(o => o.WarehouseImport == null).ToList().ForEach(o => context.WarehouseImportProductDetail.Remove(o));
                        context.WarehouseImportSparepartDetail.Local.Where(o => o.WarehouseImport == null).ToList().ForEach(o => context.WarehouseImportSparepartDetail.Remove(o));
                        context.SaveChanges();

                        if (id == 0) context.WarehouseImportMng_function_GenerateReceipNo(dbItem.WarehouseImportID, dbItem.Season);

                        dtoItem = GetData(dbItem.WarehouseImportID, out notification).Data;

                        result = true;
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
            }

            return result;
        }

        public override bool Approve(int id, ref DTO.WarehouseImportMng.WarehouseImport dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //validate quantity of every area in every product
                ValidateQuantityOfArea(dtoItem); // remove from use Wex

                // only update the current data if the status is different than CONFIRMED
                if (!dtoItem.IsConfirmed.HasValue || !dtoItem.IsConfirmed.Value)
                {
                    // update current data
                    if (!UpdateData(id, ref dtoItem, out notification))
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception(DALBase.Helper.TEXT_CONFIRMED_CONFLICT);
                }

                using (WarehouseImportMngEntities context = CreateContext())
                {
                    WarehouseImport dbItem = context.WarehouseImport.FirstOrDefault(o => o.WarehouseImportID == id);
                    if (dbItem != null)
                    {
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = dtoItem.UpdatedBy;
                        dbItem.ConfirmedDate = DateTime.Now;

                        context.SaveChanges();
                        dtoItem = GetData(id, out notification).Data;

                        //create reservation for onsea product
                        //context.WarehouseImportMng_function_CreateReservation(dtoItem.WarehouseImportID, dtoItem.UpdatedBy);
                        return true;
                    }
                    else
                    {
                        throw new Exception("Import receipt not found!");
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

        public override bool Reset(int id, ref DTO.WarehouseImportMng.WarehouseImport dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // only update the current data if the status is different than CONFIRMED
            if (dtoItem.IsConfirmed.HasValue && dtoItem.IsConfirmed.Value)
            {
                try
                {
                    //validate quantity of every area in every product
                    ValidateQuantityOfArea(dtoItem); // remove from use Wex

                    using (WarehouseImportMngEntities context = CreateContext())
                    {
                        WarehouseImport dbItem = context.WarehouseImport.FirstOrDefault(o => o.WarehouseImportID == id);
                        if (dbItem != null)
                        {
                            dbItem.IsConfirmed = false;
                            //dbItem.ConfirmedBy = null;
                            //dbItem.ConfirmedDate = null;
                            dbItem.UpdatedBy = dtoItem.UpdatedBy;
                            dbItem.UpdatedDate = DateTime.Now;
                            context.SaveChanges();
                            dtoItem = GetData(id, out notification).Data;
                            
                            return true;
                        }
                        else
                        {
                            throw new Exception("Import receipt not found!");
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
            else
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Import receipt is not confirmed, no need to reset!";


                return false;
            }   
        }

        //
        // CUSTOM FUNCTIONS
        //
        public List<DTO.WarehouseImportMng.OnSeaProduct> QuickSearchOnSeaProduct(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehouseImportMng.OnSeaProduct> data = new List<DTO.WarehouseImportMng.OnSeaProduct>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    string searchQuery = null;
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    totalRows = context.WarehouseImportMng_function_SearchOnSeaProduct(searchQuery, orderBy, orderDirection).Count();
                    var result = context.WarehouseImportMng_function_SearchOnSeaProduct(searchQuery, orderBy, orderDirection);

                    data = converter.DB2DTO_OnSeaProductList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        public List<DTO.WarehouseImportMng.OnSeaSparepart> QuickSearchOnSeaSparepart(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehouseImportMng.OnSeaSparepart> data = new List<DTO.WarehouseImportMng.OnSeaSparepart>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    string searchQuery = null;
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }
                    totalRows = context.WarehouseImportMng_function_SearchOnSeaSparepart(searchQuery, orderBy, orderDirection).Count();
                    var result = context.WarehouseImportMng_function_SearchOnSeaSparepart(searchQuery, orderBy, orderDirection);
                    data = converter.DB2DTO_OnSeaSparepartList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        public DTO.WarehouseImportMng.SearchFormData GetSearchSupport(out Library.DTO.Notification notification)
        {
            DTO.WarehouseImportMng.SearchFormData data = new DTO.WarehouseImportMng.SearchFormData();
            data.ImportTypes = new List<DTO.Support.WarehouseImportType>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                data.ImportTypes = supportFactory.GetWarehouseImportType().ToList();
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
            }

            return data;
        }

        public string GetStockOverview(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                ReportDataObject ds = new ReportDataObject();

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehouseImportMng_function_GetImportedOverview", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.TableMappings.Add("Table", "WarehouseImportMng_ImportedOverview_View");

                adap.Fill(ds);
                ds.AcceptChanges();

                //dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "StockOverview");
                //return string.Empty;

                // generate xml file
                string result = DALBase.Helper.CreateReportFiles(ds, "ImportOverview");
                if (string.IsNullOrEmpty(result))
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    return string.Empty;
                }
                return result;
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

        public List<DTO.WarehouseImportMng.CreditNoteProduct> QuickSearchCreditNoteProduct(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehouseImportMng.CreditNoteProduct> data = new List<DTO.WarehouseImportMng.CreditNoteProduct>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    string searchQuery = null;
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    totalRows = context.WarehouseImportMng_function_SearchCreditNoteProduct(searchQuery, orderBy, orderDirection).Count();
                    var result = context.WarehouseImportMng_function_SearchCreditNoteProduct(searchQuery, orderBy, orderDirection);

                    data = converter.DB2DTO_CreditNoteProduct(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        public List<DTO.WarehouseImportMng.CreditNoteSparepart> QuickSearchCreditNoteSparepart(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            List<DTO.WarehouseImportMng.CreditNoteSparepart> data = new List<DTO.WarehouseImportMng.CreditNoteSparepart>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    string searchQuery = null;
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }

                    totalRows = context.WarehouseImportMng_function_SearchCreditNoteSparepart(searchQuery, orderBy, orderDirection).Count();
                    var result = context.WarehouseImportMng_function_SearchCreditNoteSparepart(searchQuery, orderBy, orderDirection);
                    data = converter.DB2DTO_CreditNoteSparepart(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        private bool ValidateQuantityReturn(DTO.WarehouseImportMng.WarehouseImport dtoItem,out string errorMessage)
        {
            errorMessage = "Validate success";
            if (dtoItem.ImportTypeID == 3) //3 : IMPORT TYPE IS RETURN
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    int? needCompareQnt = 0;
                    int? currentQnt = 0;
                    int? remaingReturnQnt = 0;
                    foreach (var item in dtoItem.Details)
                    {
                        needCompareQnt = 0;
                        currentQnt = 0;
                        remaingReturnQnt = 0;
                        if (item.WarehouseImportProductDetailID > 0)
                        {
                            currentQnt = context.WarehouseImportProductDetail.Where(o => o.WarehouseImportProductDetailID == item.WarehouseImportProductDetailID).FirstOrDefault().Quantity;
                            needCompareQnt = item.Quantity - currentQnt;
                        }
                        else
                        {
                            needCompareQnt = item.Quantity;
                        }
                        var creditNoteItem = context.WarehouseImportMng_CreditNoteProduct_View.Where(o => o.ECommercialInvoiceDetailID == item.ECommercialInvoiceDetailID);
                        if (creditNoteItem != null && creditNoteItem.Count() > 0)
                        {
                            remaingReturnQnt = creditNoteItem.FirstOrDefault().RemaingReturnQnt;
                        }
                        if (needCompareQnt > remaingReturnQnt)
                        {
                            errorMessage = "The remaining quantity of product(" + item.ArticleCode + ") from credit note invoice is not enought to import. Remaining of this product is :" + remaingReturnQnt.ToString();
                            return false;
                        }
                    }
                    foreach (var item in dtoItem.SparepartDetails)
                    {
                        needCompareQnt = 0;
                        currentQnt = 0;
                        remaingReturnQnt = 0;
                        if (item.WarehouseImportSparepartDetailID > 0)
                        {
                            currentQnt = context.WarehouseImportSparepartDetail.Where(o => o.WarehouseImportSparepartDetailID == item.WarehouseImportSparepartDetailID).FirstOrDefault().Quantity;
                            needCompareQnt = item.Quantity - currentQnt;
                        }
                        else
                        {
                            needCompareQnt = item.Quantity;
                        }
                        var creditNoteItem = context.WarehouseImportMng_CreditNoteSparepart_View.Where(o => o.ECommercialInvoiceSparepartDetailID == item.ECommercialInvoiceSparepartDetailID);
                        if (creditNoteItem != null && creditNoteItem.Count() > 0)
                        {
                            remaingReturnQnt = creditNoteItem.FirstOrDefault().RemaingReturnQnt;
                        }
                        if (needCompareQnt > remaingReturnQnt)
                        {
                            errorMessage = "The remaining quantity of sparepart(" + item.ArticleCode + ") from credit note invoice is not enought to import. Remaining of this sparepart is :" + remaingReturnQnt.ToString();
                            return false;
                        }
                    }
                }
            }
            
            return true;
        }

        private bool ValidateQuantityReserved(DTO.WarehouseImportMng.WarehouseImport dtoItem, out string errorMessage)
        {
            errorMessage = "Validate success";
            if (dtoItem.ImportTypeID == 2) //2 : IMPORT TYPE IS RESERVATION
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    int? needCompareQnt = 0;
                    int? currentQnt = 0;
                    int? remaingReturnQnt = 0;
                    foreach (var item in dtoItem.Details)
                    {
                        needCompareQnt = 0;
                        currentQnt = 0;
                        remaingReturnQnt = 0;
                        if (item.WarehouseImportProductDetailID > 0)
                        {
                            currentQnt = context.WarehouseImportProductDetail.Where(o => o.WarehouseImportProductDetailID == item.WarehouseImportProductDetailID).FirstOrDefault().Quantity;
                            needCompareQnt = item.Quantity - currentQnt;
                        }
                        else
                        {
                            needCompareQnt = item.Quantity;
                        }
                        var onSeaItem = context.WarehouseImportMng_OnSeaProduct_View.Where(o => o.LoadingPlanDetailID == item.LoadingPlanDetailID);
                        if (onSeaItem != null && onSeaItem.Count() > 0)
                        {
                            remaingReturnQnt = onSeaItem.FirstOrDefault().Quantity;
                        }
                        if (needCompareQnt > remaingReturnQnt)
                        {
                            errorMessage = "The remaining quantity of product(" + item.ArticleCode + ") from on sea is not enought to import. Remaining of this product is :" + remaingReturnQnt.ToString();
                            return false;
                        }
                    }
                    foreach (var item in dtoItem.SparepartDetails)
                    {
                        needCompareQnt = 0;
                        currentQnt = 0;
                        remaingReturnQnt = 0;
                        if (item.WarehouseImportSparepartDetailID > 0)
                        {
                            currentQnt = context.WarehouseImportSparepartDetail.Where(o => o.WarehouseImportSparepartDetailID == item.WarehouseImportSparepartDetailID).FirstOrDefault().Quantity;
                            needCompareQnt = item.Quantity - currentQnt;
                        }
                        else
                        {
                            needCompareQnt = item.Quantity;
                        }
                        var onSeaItem = context.WarehouseImportMng_OnSeaSparepart_View.Where(o => o.LoadingPlanSparepartDetailID == item.LoadingPlanSparepartDetailID);
                        if (onSeaItem != null && onSeaItem.Count() > 0)
                        {
                            remaingReturnQnt = onSeaItem.FirstOrDefault().Quantity;
                        }
                        if (needCompareQnt > remaingReturnQnt)
                        {
                            errorMessage = "The remaining quantity of sparepart(" + item.ArticleCode + ") from on sea is not enought to import. Remaining of this sparepart is :" + remaingReturnQnt.ToString();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public string GetImportPrintout(int werehouseImportID, int version, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehouseImportMng_function_GetPrintout", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WarehouseImportID", werehouseImportID);

                adap.TableMappings.Add("Table", "WarehouseImportMng_WarehouseImport_ReportView");
                adap.TableMappings.Add("Table1", "WarehouseImportMng_WarehouseImportProductDetail_ReportView");
                adap.TableMappings.Add("Table2", "WarehouseImportMng_WarehouseImportSparepartDetail_ReportView");
                adap.TableMappings.Add("Table3", "WarehouseImportMng_WarehouseImportAreaDetail_ReportView");
                adap.Fill(ds);

                //compite data
                ReportDataObject.WarehouseImportMng_WarehouseImport_ReportViewRow dr = ds.WarehouseImportMng_WarehouseImport_ReportView.FirstOrDefault();
                ReportDataObject.ImportRow drImport = ds.Import.NewImportRow();
                drImport["ReceiptNo"] = dr["ReceiptNo"];
                drImport["Season"] = dr["Season"];
                drImport["ImportedDate"] = dr["ImportedDate"];
                drImport["RefNo"] = dr["RefNo"];
                drImport["Remark"] = dr["Remark"];
                drImport["ImportTypeNM"] = dr["ImportTypeNM"];
                drImport["UpdatorName"] = dr["UpdatorName"];
                drImport["UpdatedDate"] = dr["UpdatedDate"];
                drImport["ConfirmerName"] = dr["ConfirmerName"];
                drImport["ConfirmedDate"] = dr["ConfirmedDate"];
                drImport["Version"] = version;
                ds.Import.AddImportRow(drImport);

                //product detail
                ReportDataObject.ImportDetailRow drDetail;
                if (version == 1)
                {
                    foreach (var item in ds.WarehouseImportMng_WarehouseImportProductDetail_ReportView)
                    {
                        drDetail = ds.ImportDetail.NewImportDetailRow();
                        ds.ImportDetail.AddImportDetailRow(drDetail);

                        drDetail.ArticleCode = item.ArticleCode;
                        drDetail.Description = item.Description;
                        drDetail.Quantity = item.Quantity;
                        drDetail.ProductStatusNM = item.ProductStatusNM;
                    }

                    //sparepart detail
                    foreach (var item in ds.WarehouseImportMng_WarehouseImportSparepartDetail_ReportView)
                    {
                        drDetail = ds.ImportDetail.NewImportDetailRow();
                        ds.ImportDetail.AddImportDetailRow(drDetail);

                        drDetail.ArticleCode = item.ArticleCode;
                        drDetail.Description = item.Description;
                        drDetail.Quantity = item.Quantity;
                        drDetail.ProductStatusNM = item.ProductStatusNM;
                    }
                }
                else if (version == 2)
                {
                    if (dr.IsIsConfirmedNull() || !dr.IsConfirmed)
                    {
                        throw new Exception("Import receipt must be confirmed before print version 2");
                    }
                    foreach (var item in ds.WarehouseImportMng_WarehouseImportAreaDetail_ReportView)
                    {
                        drDetail = ds.ImportDetail.NewImportDetailRow();
                        ds.ImportDetail.AddImportDetailRow(drDetail);

                        drDetail.ArticleCode = item.ArticleCode;
                        drDetail.Description = item.Description;
                        drDetail.AreaUD = item.WarehouseAreaUD;
                        drDetail.AreaQnt = item.Quantity;
                    }
                }
                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "WarehouseImportPrintout");

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "WarehouseImportPrintout" + version.ToString());
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public bool ValidateQuantityOfArea(DTO.WarehouseImportMng.WarehouseImport dtoItem)
        {
            //validate quanity & location for every product
            foreach (var product in dtoItem.Details)
            {
                int? areaQnt = 0;
                if ((product.WarehouseImportAreaDetails != null && product.WarehouseImportAreaDetails.Count() > 0) || (product.Quantity.HasValue && product.Quantity.Value!=0))
                {
                    foreach (var item in product.WarehouseImportAreaDetails)
                    {
                        if (!item.WarehouseAreaID.HasValue)
                        {
                            throw new Exception("You have to fill-in area for product: " + product.ArticleCode + "(" + product.Description + ")");
                        }
                        else if (!item.Quantity.HasValue)
                        {
                            throw new Exception("You have to fill-in quantity for every area of product: " + product.ArticleCode + "(" + product.Description + ")");
                        }
                        else
                        {
                            areaQnt += item.Quantity;
                        }
                    }
                    if (areaQnt != product.Quantity)
                    {
                        throw new Exception("The quanity of product: " + product.ArticleCode + "(" + product.Description + ") have to equal to total quantity in every area");
                    }
                }
                
            }

            //validate quanity & location for every sparepart
            foreach (var sparepart in dtoItem.SparepartDetails)
            {
                int? areaQnt = 0;
                if ((sparepart.WarehouseImportAreaDetails != null && sparepart.WarehouseImportAreaDetails.Count() > 0) || (sparepart.Quantity.HasValue && sparepart.Quantity.Value != 0))
                {
                    foreach (var item in sparepart.WarehouseImportAreaDetails)
                    {
                        if (!item.WarehouseAreaID.HasValue)
                        {
                            throw new Exception("You have to fill-in area for sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ")");
                        }
                        else if (!item.Quantity.HasValue)
                        {
                            throw new Exception("You have to fill-in quantity for every area of sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ")");
                        }
                        else
                        {
                            areaQnt += item.Quantity;
                        }
                    }
                }
                if (areaQnt != sparepart.Quantity)
                {
                    throw new Exception("The quanity of sparepart: " + sparepart.ArticleCode + "(" + sparepart.Description + ") have to equal to total quantity in every area");
                }
            }
            return true;
        }

        public List<DTO.WarehouseImportMng.ExportToWexData> GetExportWexData(int warehouseImportID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.WarehouseImportMng.ExportToWexData> data = new List<DTO.WarehouseImportMng.ExportToWexData>();
            try
            {
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ShowDataExportToWex(context.WarehouseImportMng_function_CreateWexCSVFile(warehouseImportID).ToList());
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
            }
            return data;
        }

        public string CreateWexCSVFile(int warehouseImportID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WarehouseImportMng_function_CreateWexCSVFile", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WarehouseImportID", warehouseImportID);
                adap.TableMappings.Add("Table", "ExportToWexData");
                adap.Fill(ds);

                // generate xml file
                System.Data.DataTable dtData = ds.ExportToWexData;
                return Library.Helper.CreateCSVFile(dtData);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public int? ImportCSVFileToWarehouseImport(List<DTO.WarehouseImportMng.WexInputData> wexData, int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                var dtoItem = wexData.FirstOrDefault();
                var dtoDetailItem = wexData.Select(o => new { o.ARTICLECODE, o.CONTAINERNO, o.CLIENTUD, o.PROFORMAINVOICENO }).Distinct().ToList();
                using (WarehouseImportMngEntities context = CreateContext())
                {
                    WarehouseImportColliDetail dbColliDetail;
                    WarehouseImportProductDetail dbProductDetail;
                    WarehouseImport dbItem = new WarehouseImport();
                    dbItem.ImportTypeID = 1; //free import
                    dbItem.ImportedDate = DateTime.Now;
                    dbItem.Remark = "Import from CSV file of Wex";
                    dbItem.UpdatedBy = userID;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.Season = Library.Helper.GetCurrentSeason();

                    DateTime dateValue;
                    if (DateTime.TryParse(dtoItem.CONTAINERRECEIVEDDATE, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out dateValue))
                    {
                        dbItem.ContainerReceivedDate = dateValue;
                    }
                    if (DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd") + " " + dtoItem.CONTAINERRECEIVEDTIME, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out dateValue))
                    {
                        dbItem.ContainerReceivedTime = dateValue;
                    }

                    foreach (var item in dtoDetailItem)
                    {
                        dbProductDetail = new WarehouseImportProductDetail();
                        dbItem.WarehouseImportProductDetail.Add(dbProductDetail);

                        var p = context.WarehouseImportMng_Product_View.Where(o => o.ArticleCode == item.ARTICLECODE).FirstOrDefault();
                        if (p == null)
                        {
                            throw new Exception("Cound not found the product " + item.ARTICLECODE + " in Tilsoft");
                        }
                        else
                        {
                            dbProductDetail.ProductID = p.ProductID;
                        }

                        if (!string.IsNullOrEmpty(item.CLIENTUD))
                        {
                            var x = context.WarehouseImportMng_Client_View.Where(o => o.ClientUD == item.CLIENTUD).FirstOrDefault();
                            if (x == null)
                            {
                                throw new Exception("Cound not found the client " + item.CLIENTUD + " in Tilsoft");
                            }
                            else
                            {
                                dbProductDetail.ClientID = x.ClientID;
                            }
                        }

                        if (!string.IsNullOrEmpty(item.PROFORMAINVOICENO))
                        {
                            var y = context.WarehouseImportMng_SaleOrder_View.Where(o => o.ProformaInvoiceNo == item.PROFORMAINVOICENO).FirstOrDefault();
                            if (y == null)
                            {
                                throw new Exception("Cound not found the proforma invoice " + item.PROFORMAINVOICENO + " in Tilsoft");
                            }
                            else
                            {
                                dbProductDetail.SaleOrderID = y.SaleOrderID;
                            }
                        }
                        if (!string.IsNullOrEmpty(item.CONTAINERNO))
                        {
                            var z = context.WarehouseImportMng_LoadingPlan_View.Where(o => o.ContainerNo == item.CONTAINERNO).FirstOrDefault();
                            if (z == null)
                            {
                                throw new Exception("Cound not found the container no. " + item.CONTAINERNO + " in Tilsoft");
                            }
                            else
                            {
                                dbProductDetail.LoadingPlanID = z.LoadingPlanID;
                            }
                        }

                        var q = wexData.Where(o => o.ARTICLECODE == item.ARTICLECODE && o.CLIENTUD == item.CLIENTUD && o.PROFORMAINVOICENO == item.PROFORMAINVOICENO && o.CONTAINERNO == item.CONTAINERNO).FirstOrDefault();
                        if (q != null)
                        {
                            string sQnt = q.SUM;
                            int iQnt;
                            if (!string.IsNullOrEmpty(sQnt))
                            {
                                if (Int32.TryParse(sQnt, out iQnt))
                                {
                                    dbProductDetail.Quantity = iQnt;
                                }
                                else
                                {
                                    throw new Exception("The quantity is invalid");
                                }
                            }
                        }
                        dbProductDetail.ProductStatusID = 1;

                        foreach (var sItem in wexData.Where(o => o.ARTICLECODE == item.ARTICLECODE && o.CLIENTUD == item.CLIENTUD && o.PROFORMAINVOICENO == item.PROFORMAINVOICENO && o.CONTAINERNO == item.CONTAINERNO))
                        {
                            dbColliDetail = new WarehouseImportColliDetail();
                            dbProductDetail.WarehouseImportColliDetail.Add(dbColliDetail);

                            if (!string.IsNullOrEmpty(sItem.COLLIEANCODE))
                            {
                                var colli = context.WarehouseImportMng_ProductColli_View.Where(o => o.ColliEANCode == sItem.COLLIEANCODE).FirstOrDefault();
                                if (colli == null)
                                {
                                    throw new Exception("Cound not found the Colli EANCode " + sItem.COLLIEANCODE + " in Tilsoft");
                                }
                                else
                                {
                                    dbColliDetail.ProductColliID = colli.ProductColliID;
                                }
                            }

                            if (!string.IsNullOrEmpty(sItem.SUM))
                            {
                                int iColliQnt;
                                if (Int32.TryParse(sItem.SUM, out iColliQnt))
                                {
                                    dbColliDetail.Quantity = Convert.ToInt32(sItem.SUM);
                                }
                                else
                                {
                                    throw new Exception("The quantity is invalid");
                                }
                            }
                        }
                    }
                    context.WarehouseImport.Add(dbItem);
                    context.SaveChanges();
                    context.WarehouseImportMng_function_GenerateReceipNo(dbItem.WarehouseImportID, dbItem.Season);
                    return dbItem.WarehouseImportID;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return -1;
            }
        }


    }
}
