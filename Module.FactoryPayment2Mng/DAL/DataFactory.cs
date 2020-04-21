using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryPayment2MngEntities CreateContext()
        {
            return new FactoryPayment2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryPayment2MngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryPaymentSearchResult>();
            data.SummaryData = new DTO.SummaryRow();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    string Season = null;
                    int? SupplierID = null;
                    int UserID = -1;
                    string ReceiptNo = null;
                    string BankReceiptNo = null;
                    string InvoiceNo = null;
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
                    if (filters.ContainsKey("ReceiptNo") && !string.IsNullOrEmpty(filters["ReceiptNo"].ToString()))
                    {
                        ReceiptNo = filters["ReceiptNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("BankReceiptNo") && !string.IsNullOrEmpty(filters["BankReceiptNo"].ToString()))
                    {
                        BankReceiptNo = filters["BankReceiptNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("InvoiceNo") && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
                    {
                        InvoiceNo = filters["InvoiceNo"].ToString().Replace("'", "''");
                    }

                    totalRows = context.FactoryPayment2Mng_function_SearchPayment(Season, SupplierID, UserID, ReceiptNo, BankReceiptNo, InvoiceNo, orderBy, orderDirection).Count();
                    var result = context.FactoryPayment2Mng_function_SearchPayment(Season, SupplierID, UserID, ReceiptNo, BankReceiptNo, InvoiceNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryPaymentSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    FactoryPayment2Mng_SummaryRow_View dbSummary = context.FactoryPayment2Mng_function_SearchPaymentSummary(Season, SupplierID, UserID, ReceiptNo, BankReceiptNo, InvoiceNo).FirstOrDefault();
                    if (dbSummary != null)
                    {
                        data.SummaryData.SumConfirmedAmount = dbSummary.SumConfirmedAmount.Value;
                        data.SummaryData.SumDPAmount = dbSummary.SumDPAmount.Value;
                        data.SummaryData.SumTotalAmount = dbSummary.SumTotalAmount.Value;
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
            DTO.FactoryPayment dtoFactoryPayment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryPayment>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                // check permission
                if (fwFactory.CheckSupplierPermission(userId, dtoFactoryPayment.SupplierID.Value) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected supplier data");
                }
                if (id > 0 && fwFactory.CheckFactoryPaymentPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected factory payment data");
                }

                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    FactoryPayment2 dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryPayment2();
                        context.FactoryPayment2.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryPayment2.FirstOrDefault(o => o.FactoryPayment2ID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory payment not found!";
                        return false;
                    }
                    else
                    {
                        // check if payment is confirmed
                        if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                        {
                            throw new Exception("Can not edit the confirmed payment!");
                        }

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactoryPayment.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // validata data
                        if (dbItem.ConfirmedAmount.HasValue && dbItem.TotalAmount.HasValue && dbItem.TotalAmount < dbItem.ConfirmedAmount)
                        {
                            throw new Exception("Confirmed received amount greater than total paid amount!");
                        }
                        if (fwFactory.IsDPBalanceClosed(userId, dtoFactoryPayment.SupplierID.Value, dtoFactoryPayment.Season))
                        {
                            throw new Exception("Balance for season: " + dbItem.Season + " is closed or you dont have access permission for the balance data!");
                        }

                        using (DbContextTransaction scope = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlCommand("SELECT * FROM FactoryPayment2 WITH (TABLOCKX, HOLDLOCK); SELECT * FROM FactoryPayment2Balance WITH (TABLOCKX, HOLDLOCK); SELECT * FROM FactoryPayment2Detail WITH (TABLOCKX, HOLDLOCK);");

                            try
                            {
                                converter.DTO2DB(dtoFactoryPayment, ref dbItem);

                                decimal remainDPAmount = context.FactoryPayment2Mng_function_GetRemainDPAmount(dbItem.Season, dbItem.SupplierID).FirstOrDefault().Value;
                                decimal totalDPDeductedAmount = dbItem.FactoryPayment2Detail.Where(o => o.DPDeductedAmount.HasValue).Sum(o => o.DPDeductedAmount.Value);
                                if (totalDPDeductedAmount > 0 && totalDPDeductedAmount > remainDPAmount)
                                {
                                    throw new Exception("DP deducted amount (" + totalDPDeductedAmount.ToString() + ") larger than remain DP amount (" + remainDPAmount.ToString() + ")!");
                                }
                                dbItem.UpdatedBy = userId;
                                dbItem.UpdatedDate = DateTime.Now;

                                // remove orphan
                                context.FactoryPayment2Detail.Local.Where(o => o.FactoryPayment2 == null).ToList().ForEach(o => context.FactoryPayment2Detail.Remove(o));
                                context.SaveChanges();

                                dbItem.ReceiptNo = Library.Common.Helper.formatIndex(dbItem.FactoryPayment2ID.ToString(), 8, "0");
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                                scope.Commit();
                            }
                        }                       
                    }
                    dtoItem = GetData(userId, dbItem.FactoryPayment2ID, -1, string.Empty, out notification).Data;
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
                        case "ReceipNoUnique":
                            notification.Message = "Duplicate payment receipt number (payment receipt number must be unique)!";
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
            DTO.FactoryPayment dtoFactoryPayment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryPayment>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    FactoryPayment2 dbItem = context.FactoryPayment2.FirstOrDefault(o => o.FactoryPayment2ID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Factory payment not found!");
                    }

                    // validate before approve
                    if (!dbItem.PaymentDate.HasValue)
                    {
                        throw new Exception("Payment date is required!");
                    }
                    if (dbItem.ConfirmedAmount.HasValue && dbItem.TotalAmount.HasValue && dbItem.TotalAmount < dbItem.ConfirmedAmount)
                    {
                        throw new Exception("Confirmed received amount greater than total paid amount!");
                    }
                    if (fwFactory.IsDPBalanceClosed(userId, dbItem.SupplierID.Value, dbItem.Season))
                    {
                        throw new Exception("Balance for season: " + dbItem.Season + " is closed or you dont have access permission for the balance data!");
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = userId;
                    dbItem.ConfirmedRemark = dtoFactoryPayment.ConfirmedRemark;
                    dbItem.ConfirmedAmount = dtoFactoryPayment.ConfirmedAmount;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryPayment2ID, -1, string.Empty, out notification).Data;
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
            DTO.FactoryPayment dtoFactoryCreditNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryPayment>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    FactoryPayment2 dbItem = context.FactoryPayment2.FirstOrDefault(o => o.FactoryPayment2ID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Factory payment not found!");
                    }
                    dbItem.IsConfirmed = null;
                    dbItem.ConfirmedDate = null;
                    dbItem.ConfirmedBy = null;
                    dbItem.ConfirmedRemark = null;
                    dbItem.ConfirmedAmount = null;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryPayment2ID, -1, string.Empty, out notification).Data;
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
                Task task1 = Task.Factory.StartNew(() => { data.Seasons = supportFactory.GetSeason(); });
                Task task2 = Task.Factory.StartNew(() => { data.Suppliers = supportFactory.GetSupplier(userId); });
                Task.WaitAll(task1, task2);
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
                Task task1 = Task.Factory.StartNew(() => { data.Suppliers = supportFactory.GetSupplier(userId); });
                Task task2 = Task.Factory.StartNew(() => { data.Seasons = supportFactory.GetSeason(); });
                Task.WaitAll(task1, task2);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.PurchasingInvoiceSearchResult> QuickSearchInvoice(int userId, int supplierId, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PurchasingInvoiceSearchResult> data = new List<DTO.PurchasingInvoiceSearchResult>();

            //try to get data
            try
            {
                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_PurchasingInvoiceSearchResultList(context.FactoryPayment2Mng_function_SearchPurchasingInvoice(userId, supplierId, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int supplierID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryPayment();
            data.Data.FactoryPaymentDetails = new List<DTO.FactoryPaymentDetail>();

            //try to get data
            try
            {
                // check permission
                if (id > 0)
                {
                    if (fwFactory.CheckFactoryPaymentPermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected payment data");
                    }
                }
                else
                {
                    // check supplier permission for invoice with product
                    if (fwFactory.CheckSupplierPermission(userId, supplierID) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected supplier data");
                    }
                }

                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
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
                        data.Data = converter.DB2DTO_FactoryPayment(context.FactoryPayment2Mng_FactoryPayment2_View
                            .Include("FactoryPayment2Mng_FactoryPayment2Detail_View")
                            .FirstOrDefault(o => o.FactoryPayment2ID == id));
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

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckFactoryPaymentPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected payment data");
                }

                using (FactoryPayment2MngEntities context = CreateContext())
                {
                    FactoryPayment2 dbItem = context.FactoryPayment2.FirstOrDefault(o => o.FactoryPayment2ID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Payment not found!");
                    }

                    // check if invoice already confirmed
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not delete the confirmed payment!");
                    }

                    // everything ok, delete the invoice
                    context.FactoryPayment2.Remove(dbItem);
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
