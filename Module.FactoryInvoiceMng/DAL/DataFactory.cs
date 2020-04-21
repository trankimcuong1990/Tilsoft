using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryInvoiceMngEntities CreateContext()
        {
            return new FactoryInvoiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryInvoiceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryInvoiceSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    string Season = null;
                    int? SupplierID = null;
                    int UserID = -1;
                    string InvoiceNo = null;
                    string BookingUD = null;
                    string BLNo = null;
                    string ClientUD = null;
                    string ArticleCode = null;
                    string Description = null;
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("SupplierID") && !string.IsNullOrEmpty(filters["SupplierID"].ToString()))
                    {
                        SupplierID = Convert.ToInt32(filters["SupplierID"].ToString());
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    if (filters.ContainsKey("InvoiceNo") && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
                    {
                        InvoiceNo = filters["InvoiceNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("BookingUD") && !string.IsNullOrEmpty(filters["BookingUD"].ToString()))
                    {
                        Description = filters["BookingUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
                    {
                        BLNo = filters["BLNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
                    }
                    totalRows = context.FactoryInvoiceMng_function_SearchInvoice(Season, SupplierID, UserID, InvoiceNo, BookingUD, BLNo, ClientUD, ArticleCode, Description, orderBy, orderDirection).Count();
                    var result = context.FactoryInvoiceMng_function_SearchInvoice(Season, SupplierID, UserID, InvoiceNo, BookingUD, BLNo, ClientUD, ArticleCode, Description, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryInvoice dtoFactoryInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                // check permission
                if (dtoFactoryInvoice.BookingID.HasValue && dtoFactoryInvoice.BookingID.Value > 0 && fwFactory.CheckBookingPermission(userId, dtoFactoryInvoice.BookingID.Value) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected booking data");
                }
                if (fwFactory.CheckSupplierPermission(userId, dtoFactoryInvoice.SupplierID.Value) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected supplier data");
                }
                if (id > 0 && fwFactory.CheckFactoryInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected invoice data");
                }      

                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    FactoryInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryInvoice();
                        context.FactoryInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryInvoice.FirstOrDefault(o => o.FactoryInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check if invoice is confirmed
                        if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                        {
                            throw new Exception("Can not edit the confirmed invoice!");
                        }

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactoryInvoice.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // check remain quantity
                        if (dtoFactoryInvoice.BookingID.HasValue)
                        {
                            int bookingID = dtoFactoryInvoice.BookingID.Value;
                            int supplierID = dtoFactoryInvoice.SupplierID.Value;
                            foreach (DTO.FactoryInvoiceDetail dtoDetail in dtoFactoryInvoice.FactoryInvoiceDetails)
                            {
                                int factoryOrderDetailID = dtoDetail.FactoryOrderDetailID.Value;

                                // check total order
                                FactoryInvoiceMng_FactoryOrderDetailSearchResult_View dtoDetailOrder = context.FactoryInvoiceMng_FactoryOrderDetailSearchResult_View.FirstOrDefault(o => o.FactoryOrderDetailID == factoryOrderDetailID && o.BookingID == bookingID && o.SupplierID == supplierID);
                                if (dtoDetailOrder == null || !dtoDetailOrder.TotalQuantity.HasValue)
                                {
                                    throw new Exception("Booking: " + dtoFactoryInvoice.BookingUD + " not contain item: " + dtoDetail.Description);
                                }

                                FactoryInvoiceMng_IssuedFactoryOrderDetail_View dtoDetailQnt = context.FactoryInvoiceMng_function_CheckOrderDetailQuantity(dtoDetail.FactoryOrderDetailID.Value, dtoFactoryInvoice.BookingID.Value, dtoFactoryInvoice.SupplierID.Value, dtoFactoryInvoice.FactoryInvoiceID).FirstOrDefault();
                                if (dtoDetailQnt != null && dtoDetailQnt.TotalQuantity.HasValue)
                                {
                                    if (dtoDetailOrder.TotalQuantity.Value < dtoDetailQnt.TotalQuantity.Value + dtoDetail.Quantity.Value)
                                    {
                                        throw new Exception("Quantity exceed shipped quantity<br/><u>Product</u>: " + dtoDetail.Description + "<br/><u>Total shipped</u>: " + dtoDetailOrder.TotalQuantity.Value.ToString() + "<br/><u>Total issued</u>: " + dtoDetailQnt.TotalQuantity.Value.ToString() + "<br/><u>Previous invoice</u>: " + dtoDetailQnt.InvoiceNo + "<br/><u>To be issued in this invoice</u>: " + dtoDetail.Quantity.Value.ToString());
                                    }
                                }
                            }

                            foreach (DTO.FactoryInvoiceSparepartDetail dtoSparepartDetail in dtoFactoryInvoice.FactoryInvoiceSparepartDetails)
                            {
                                int factoryOrderSparepartDetailID = dtoSparepartDetail.FactoryOrderSparepartDetailID.Value;

                                // check total order
                                FactoryInvoiceMng_FactoryOrderSparepartDetailSearchResult_View dtoSparepartDetailOrder = context.FactoryInvoiceMng_FactoryOrderSparepartDetailSearchResult_View.FirstOrDefault(o => o.FactoryOrderSparepartDetailID == factoryOrderSparepartDetailID && o.BookingID == bookingID && o.SupplierID == supplierID);
                                if (dtoSparepartDetailOrder == null || !dtoSparepartDetailOrder.TotalQuantity.HasValue)
                                {
                                    throw new Exception("Booking: " + dtoFactoryInvoice.BookingUD + " not contain sparepart: " + dtoSparepartDetailOrder.Description);
                                }

                                FactoryInvoiceMng_IssuedFactoryOrderSparepartDetail_View dtoSparepartDetailQnt = context.FactoryInvoiceMng_function_CheckOrderSparepartDetailQuantity(dtoSparepartDetail.FactoryOrderSparepartDetailID.Value, dtoFactoryInvoice.BookingID.Value, dtoFactoryInvoice.SupplierID.Value, dtoFactoryInvoice.FactoryInvoiceID).FirstOrDefault();
                                if (dtoSparepartDetailQnt != null && dtoSparepartDetailQnt.TotalQuantity.HasValue)
                                {
                                    if (dtoSparepartDetailOrder.TotalQuantity.Value < dtoSparepartDetailQnt.TotalQuantity.Value + dtoSparepartDetail.Quantity.Value)
                                    {
                                        throw new Exception("Quantity exceed shipped quantity<br/><u>Sparepart</u>: " + dtoSparepartDetail.Description + "<br/><u>Total shipped</u>: " + dtoSparepartDetailOrder.TotalQuantity.Value.ToString() + "<br/><u>Total issued</u>: " + dtoSparepartDetailQnt.TotalQuantity.Value.ToString() + "<br/><u>Previous invoice</u>: " + dtoSparepartDetailQnt.InvoiceNo + "<br/><u>To be issued in this invoice</u>: " + dtoSparepartDetail.Quantity.Value.ToString());
                                    }
                                }
                            }
                        }                        

                        converter.DTO2DB(dtoFactoryInvoice, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphan
                        context.FactoryInvoiceDetail.Local.Where(o => o.FactoryInvoice == null).ToList().ForEach(o => context.FactoryInvoiceDetail.Remove(o));
                        context.FactoryInvoiceSparepartDetail.Local.Where(o => o.FactoryInvoice == null).ToList().ForEach(o => context.FactoryInvoiceSparepartDetail.Remove(o));
                        context.FactoryInvoiceExtra.Local.Where(o => o.FactoryInvoice == null).ToList().ForEach(o => context.FactoryInvoiceExtra.Remove(o));

                        context.SaveChanges();
                    }
                    dtoItem = GetData(userId, dbItem.FactoryInvoiceID, -1, -1, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    switch (indexName)
                    {
                        case "InvoiceNoUnique":
                            notification.Message = "Duplicate invoice number (invoice no must be unique)!";
                            break;

                        default:
                            notification.Message = dEx.Message;
                            break;
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryInvoice dtoFactoryInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    FactoryInvoice dbItem = context.FactoryInvoice.FirstOrDefault(o => o.FactoryInvoiceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Invoice not found!");
                    }

                    // validate before approve
                    if (string.IsNullOrEmpty(dbItem.ScanFile))
                    {
                        throw new Exception("Scan file is required!");
                    }
                    if (!dbItem.InvoiceDate.HasValue)
                    {
                        throw new Exception("Invoice date is required!");
                    }
                    if (string.IsNullOrEmpty(dbItem.InvoiceNo))
                    {
                        throw new Exception("Invoice number is required!");
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = userId;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryInvoiceID, -1, -1, string.Empty, out notification).Data;
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

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryInvoice dtoFactoryInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    FactoryInvoice dbItem = context.FactoryInvoice.FirstOrDefault(o => o.FactoryInvoiceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Invoice not found!");
                    }
                    dbItem.IsConfirmed = null;
                    dbItem.ConfirmedDate = null;
                    dbItem.ConfirmedBy = null;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryInvoiceID, -1, -1, string.Empty, out notification).Data;
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
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Suppliers = new List<Support.DTO.Supplier>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.Suppliers = supportFactory.GetSupplier(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Suppliers = new List<Support.DTO.Supplier>();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                data.Suppliers = supportFactory.GetSupplier(userId);
                data.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.BookingSearchResult> QuickSearchBooking(int userId, int supplierId, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.BookingSearchResult> data = new List<DTO.BookingSearchResult>();

            //try to get data
            try
            {
                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_BookingSearchResultList(context.FactoryInvoiceMng_function_SearchBooking(userId, supplierId, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int bookingID, int supplierID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryInvoice();
            data.Data.FactoryInvoiceDetails = new List<DTO.FactoryInvoiceDetail>();
            data.Data.FactoryInvoiceSparepartDetails = new List<DTO.FactoryInvoiceSparepartDetail>();
            data.Data.FactoryInvoiceExtras = new List<DTO.FactoryInvoiceExtra>();

            data.FactoryOrderDetails = new List<DTO.FactoryOrderDetailSearchResult>();
            data.FactoryOrderSparepartDetails = new List<DTO.FactoryOrderSparepartDetailSearchResult>();

            //try to get data
            try
            {
                // check permission
                if (id > 0)
                {
                    if (fwFactory.CheckFactoryInvoicePermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected invoice data");
                    }                   
                }
                else
                {
                    // check booking permission for invoice with product
                    if (bookingID > 0 && fwFactory.CheckBookingPermission(userId, bookingID) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected booking data");
                    }
                    if (fwFactory.CheckSupplierPermission(userId, supplierID) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected supplier data");
                    }
                }

                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        if (bookingID > 0)
                        {
                            // get booking info
                            FactoryInvoiceMng_BookingSearchResult_View dbBooking = context.FactoryInvoiceMng_BookingSearchResult_View.FirstOrDefault(o => o.BookingID == bookingID);
                            if (dbBooking != null)
                            {
                                DTO.BookingSearchResult dtoBooking = converter.DB2DTO_BookingSearchResult(dbBooking);
                                data.Data.BookingID = bookingID;
                                data.Data.BookingUD = dtoBooking.BookingUD;
                                data.Data.BLNo = dtoBooking.BLNo;
                                data.Data.Season = dtoBooking.Season;
                            }
                            else
                            {
                                throw new Exception("Booking not exists!");
                            }                            
                        }                        

                        // get supplier info
                        data.Data.SupplierID = supplierID;
                        Module.Support.DTO.Supplier dtoSupplier = supportFactory.GetSupplier(userId).FirstOrDefault(o => o.SupplierID == supplierID);
                        if (dtoSupplier != null)
                        {
                            data.Data.SupplierUD = dtoSupplier.SupplierUD;
                            data.Data.SupplierNM = dtoSupplier.SupplierNM;
                        }
                        else
                        {
                            throw new Exception("Supplier not exists!");
                        }

                        data.Data.Season = season;
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_FactoryInvoice(context.FactoryInvoiceMng_FactoryInvoice_View
                            .Include("FactoryInvoiceMng_FactoryInvoiceExtra_View")
                            .Include("FactoryInvoiceMng_FactoryInvoiceDetail_View")
                            .Include("FactoryInvoiceMng_FactoryInvoiceSparepartDetail_View")
                            .FirstOrDefault(o => o.FactoryInvoiceID == id));

                        supplierID = data.Data.SupplierID.Value;
                        if (data.Data.BookingID.HasValue)
                        {
                            bookingID = data.Data.BookingID.Value;
                        }                        
                    }

                    data.FactoryOrderDetails = converter.DB2DTO_FactoryOrderDetailSearchResultList(context.FactoryInvoiceMng_FactoryOrderDetailSearchResult_View.Where(o => o.BookingID == bookingID && o.SupplierID == supplierID).ToList());
                    data.FactoryOrderSparepartDetails = converter.DB2DTO_FactoryOrderSparepartDetailSearchResultList(context.FactoryInvoiceMng_FactoryOrderSparepartDetailSearchResult_View.Where(o => o.BookingID == bookingID && o.SupplierID == supplierID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckFactoryInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected invoice data");
                }

                using (FactoryInvoiceMngEntities context = CreateContext())
                {
                    FactoryInvoice dbItem = context.FactoryInvoice.FirstOrDefault(o => o.FactoryInvoiceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Invoice not found!");
                    }

                    // check if invoice already confirmed
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not delete the confirmed invoice!");
                    }

                    // everything ok, delete the invoice
                    if (dbItem.ScanFile != string.Empty) fwFactory.RemoveImageFile(dbItem.ScanFile);
                    context.FactoryInvoice.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }
    }
}
