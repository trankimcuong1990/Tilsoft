using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private ClientPaymentMngEntities CreateContext()
        {
            return new ClientPaymentMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ClientPaymentMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.ClientPaymentSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    string ClientPaymentUD = null;
                    int? ClientPaymentMethodID = null;
                    string ClientUD = null;
                    string ProformaInvoiceNo = null;
                    string InvoiceNo = null;
                    string TransactionNo = null;
                    string LCNo = null;
                    string Currency = null;
                    string ClientPaymentBallanceUD = null;
                    bool? IsConfirmed = null;
                    string Remark = null;
                    if (filters.ContainsKey("ClientPaymentUD") && !string.IsNullOrEmpty(filters["ClientPaymentUD"].ToString()))
                    {
                        ClientPaymentUD = filters["ClientPaymentUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientPaymentMethodID") && !string.IsNullOrEmpty(filters["ClientPaymentMethodID"].ToString()))
                    {
                        ClientPaymentMethodID = Convert.ToInt32(filters["ClientPaymentMethodID"].ToString());
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("InvoiceNo") && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
                    {
                        InvoiceNo = filters["InvoiceNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TransactionNo") && !string.IsNullOrEmpty(filters["TransactionNo"].ToString()))
                    {
                        TransactionNo = filters["TransactionNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("LCNo") && !string.IsNullOrEmpty(filters["LCNo"].ToString()))
                    {
                        LCNo = filters["LCNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Currency") && !string.IsNullOrEmpty(filters["Currency"].ToString()))
                    {
                        Currency = filters["Currency"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientPaymentBallanceUD") && !string.IsNullOrEmpty(filters["ClientPaymentBallanceUD"].ToString()))
                    {
                        ClientPaymentBallanceUD = filters["ClientPaymentBallanceUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsConfirmed") && filters["IsConfirmed"] != null && !string.IsNullOrEmpty(filters["IsConfirmed"].ToString()))
                    {
                        IsConfirmed = Convert.ToBoolean(filters["IsConfirmed"].ToString());
                    }
                    if (filters.ContainsKey("Remark") && !string.IsNullOrEmpty(filters["Remark"].ToString()))
                    {
                        Remark = filters["Remark"].ToString().Replace("'", "''");
                    }
                    totalRows = context.ClientPaymentMng_function_SearchClientPayment(ClientPaymentUD, ClientPaymentMethodID, ClientUD, ProformaInvoiceNo, InvoiceNo, TransactionNo, LCNo, Currency, ClientPaymentBallanceUD, IsConfirmed, Remark, orderBy, orderDirection).Count();
                    var result = context.ClientPaymentMng_function_SearchClientPayment(ClientPaymentUD, ClientPaymentMethodID, ClientUD, ProformaInvoiceNo, InvoiceNo, TransactionNo, LCNo, Currency, ClientPaymentBallanceUD, IsConfirmed, Remark, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ClientPaymentSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    ClientPayment dbItem = context.ClientPayment.FirstOrDefault(o => o.ClientPaymentID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Payment not found!");
                    }
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not delete the confirmed payment!");
                    }

                    context.ClientPayment.Remove(dbItem);
                    context.SaveChanges();
                    //// remove child
                    //foreach (ClientPaymentDetail dbDetail in dbItem.ClientPaymentDetail.ToArray())
                    //{
                    //    foreach (ClientPaymentDeduction dbDeduction in dbDetail.ClientPaymentDeduction.ToArray())
                    //    {
                    //        dbDetail.ClientPaymentDeduction.Remove(dbDeduction);
                    //    }
                    //    dbItem.ClientPaymentDetail.Remove(dbDetail);
                    //}
                    //foreach (ClientPaymentBallance dbBallance in dbItem.ClientPaymentBallance.ToArray())
                    //{
                    //    dbItem.ClientPaymentBallance.Remove(dbBallance);
                    //}
                    //context.ClientPaymentDeduction.Local.Where(o => o.ClientPaymentDetail == null).ToList().ForEach(o => context.ClientPaymentDeduction.Remove(o));
                    //context.ClientPaymentDetail.Local.Where(o => o.ClientPayment == null).ToList().ForEach(o => context.ClientPaymentDetail.Remove(o));
                    //context.ClientPaymentBallance.Local.Where(o => o.ClientPayment == null).ToList().ForEach(o => context.ClientPaymentBallance.Remove(o));
                    //context.ClientPayment.Remove(dbItem);
                    //context.SaveChanges();
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ClientPayment dtoClientPayment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ClientPayment>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    ClientPayment dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientPayment();
                        context.ClientPayment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientPayment.FirstOrDefault(o => o.ClientPaymentID == id);
                    }

                    if (dbItem == null)
                    {
                        throw new Exception("Payment not found!");
                    }
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not update the confirmed payment!");
                    }

                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM ClientPayment WITH (TABLOCKX, HOLDLOCK)");

                        try
                        {
                            converter.DTO2DB(dtoClientPayment, ref dbItem);
                            // remove orphan
                            context.ClientPaymentDetail.Local.Where(o => o.ClientPayment == null).ToList().ForEach(o => context.ClientPaymentDetail.Remove(o));
                            context.ClientPaymentDeduction.Local.Where(o => o.ClientPaymentDetail == null).ToList().ForEach(o => context.ClientPaymentDeduction.Remove(o));

                            dbItem.UpdatedDate = DateTime.Now;
                            dbItem.UpdatedBy = userId;
                            context.SaveChanges();
                            if (id == 0)
                            {
                                dbItem.ClientPaymentUD = Library.Common.Helper.formatIndex(dbItem.ClientPaymentID.ToString(), 8, "0");
                                context.SaveChanges();
                            }
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

                    // check if there's difference in total payment and detail
                    decimal totalPayment = 0;
                    decimal totalDetailPayment = 0;
                    if (dbItem.Amount.HasValue) totalPayment = dbItem.Amount.Value;
                    totalDetailPayment = dbItem.ClientPaymentDetail.Where(o => o.Amount.HasValue).Sum(o => o.Amount.Value);
                    if (totalPayment != totalDetailPayment)
                    {
                        ClientPaymentBallance dbBallance = dbItem.ClientPaymentBallance.FirstOrDefault();
                        if (dbBallance == null)
                        {
                            dbBallance = new ClientPaymentBallance();
                            dbBallance.ClientPayment = dbItem;
                            context.ClientPaymentBallance.Add(dbBallance);
                        }
                        dbBallance.Amount = totalPayment - totalDetailPayment;
                        dbBallance.Currency = dbItem.Currency;
                        dbBallance.BallanceDate = DateTime.Now;
                        dbBallance.UpdatedBy = userId;
                        dbBallance.UpdatedDate = DateTime.Now;
                        context.SaveChanges();

                        dbBallance.ClientPaymentBallanceUD = Library.Common.Helper.formatIndex(dbBallance.ClientPaymentBallanceID.ToString(), 8, "0");
                        context.SaveChanges();
                    }
                    else
                    {
                        ClientPaymentBallance dbBallance = dbItem.ClientPaymentBallance.FirstOrDefault();
                        context.ClientPaymentBallance.Remove(dbBallance);
                        context.SaveChanges();
                    }

                    dtoItem = GetData(string.Empty, 0, dbItem.ClientID.Value, dbItem.ClientPaymentID, out notification).Data;
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

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ClientPayment dtoClientPayment = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ClientPayment>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    ClientPayment dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ClientPayment();
                        context.ClientPayment.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientPayment.FirstOrDefault(o => o.ClientPaymentID == id);
                    }

                    if (dbItem == null)
                    {
                        throw new Exception("Payment not found!");
                    }
                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = userId;

                    // check if there's difference in total payment and detail
                    decimal totalPayment = 0;
                    decimal totalDetailPayment = 0;
                    if (dbItem.Amount.HasValue) totalPayment = dbItem.Amount.Value;
                    totalDetailPayment = dbItem.ClientPaymentDetail.Where(o => o.Amount.HasValue).Sum(o => o.Amount.Value);
                    if (totalPayment != totalDetailPayment) {
                        ClientPaymentBallance dbBallance = dbItem.ClientPaymentBallance.FirstOrDefault();
                        if (dbBallance == null)
                        {
                            dbBallance = new ClientPaymentBallance();
                            dbBallance.ClientPayment = dbItem;
                            context.ClientPaymentBallance.Add(dbBallance);
                        }
                        dbBallance.Amount = totalPayment - totalDetailPayment;
                        dbBallance.Currency = dbItem.Currency;
                        dbBallance.BallanceDate = DateTime.Now;
                        dbBallance.UpdatedBy = userId;
                        dbBallance.UpdatedDate = DateTime.Now;
                        context.SaveChanges();

                        dbBallance.ClientPaymentBallanceUD = Library.Common.Helper.formatIndex(dbBallance.ClientPaymentBallanceID.ToString(), 8, "0");
                        context.SaveChanges();
                    }

                    context.SaveChanges();
                    dtoItem = GetData(string.Empty, 0, dbItem.ClientID.Value, dbItem.ClientPaymentID, out notification).Data;
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
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData result = new DTO.SearchFilterData();
            result.YesNos = new List<Support.DTO.YesNo>();
            result.ClientPaymentMethods = new List<Support.DTO.ClientPaymentMethod>();

            try
            {
                result.YesNos = supportFactory.GetYesNo();
                result.ClientPaymentMethods = supportFactory.GetClientPaymentMethod();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }

        public DTO.InitFormData GetInit(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData result = new DTO.InitFormData();
            result.Clients = new List<Support.DTO.Client>();
            result.ClientPaymentMethods = new List<Support.DTO.ClientPaymentMethod>();
            try
            {
                result.Clients = supportFactory.GetClient();
                result.ClientPaymentMethods = supportFactory.GetClientPaymentMethod();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }

        public DTO.EditFormData GetData(string currency, int methodId, int clientId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.ClientPayment();
            data.Invoices = new List<DTO.ECommercialInvoiceSearchResult>();
            data.Orders = new List<DTO.SaleOrderSearchResult>();
            data.OrderForDeductions = new List<DTO.SaleOrderForDeductionSearchResult>();

            //try to get data
            try
            {
                using (ClientPaymentMngEntities context = CreateContext())
                {
                    // add new case
                    if (id == 0)
                    {
                        Module.Support.DTO.Client dtoClient = supportFactory.GetClient().FirstOrDefault(o => o.ClientID == clientId);
                        var dtoBallance = context.ClientPaymentMng_ClientBallace_View.FirstOrDefault(o => o.ClientID == clientId);
                        data.Data.ClientID = clientId;
                        data.Data.ClientUD = dtoClient.ClientUD;
                        data.Data.ClientNM = dtoClient.ClientNM;
                        data.Data.TotalBallanceEURAmount = 0;
                        data.Data.TotalBallanceUSDAmount = 0;
                        if (dtoBallance != null)
                        {
                            data.Data.TotalBallanceEURAmount = dtoBallance.TotalBallanceEURAmount;
                            data.Data.TotalBallanceUSDAmount = dtoBallance.TotalBallanceUSDAmount;                                
                        }
                        data.Data.Currency = currency;
                        data.Data.Amount = 0;
                        data.Data.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                        data.Data.ClientPaymentMethodID = methodId;
                        data.Data.ClientPaymentMethodNM = supportFactory.GetClientPaymentMethod().FirstOrDefault(o=>o.ClientPaymentMethodID == methodId).ClientPaymentMethodNM;
                        data.Invoices = GetInvoice(clientId, currency);
                        data.Orders = GetSaleOrder(clientId, currency);
                        data.OrderForDeductions = GetSaleOrderForDeduction(clientId, currency);
                        data.Data.ClientPaymentDetails = new List<DTO.ClientPaymentDetail>();
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_ClientPayment(context.ClientPaymentMng_ClientPayment_View
                            .Include("ClientPaymentMng_ClientPaymentDetail_View")
                            .Include("ClientPaymentMng_ClientPaymentDetail_View.ClientPaymentMng_ClientPaymentDeduction_View")
                            .FirstOrDefault(o => o.ClientPaymentID == id));
                        data.Invoices = GetInvoice(data.Data.ClientID.Value, data.Data.Currency);
                        data.Orders = GetSaleOrder(data.Data.ClientID.Value, data.Data.Currency);
                        data.OrderForDeductions = GetSaleOrderForDeduction(data.Data.ClientID.Value, data.Data.Currency);
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

        private List<DTO.ECommercialInvoiceSearchResult> GetInvoice(int ClientID, string Currency)
        {
            List<DTO.ECommercialInvoiceSearchResult> data = new List<DTO.ECommercialInvoiceSearchResult>();
            using (ClientPaymentMngEntities context = CreateContext())
            {
                data = converter.DB2DTO_ECommercialInvoiceSearchResultList(context.ClientPaymentMng_function_SearchECommercialInvoice(ClientID, Currency).ToList());
            }
            return data;
        }

        private List<DTO.SaleOrderSearchResult> GetSaleOrder(int ClientID, string Currency)
        {
            List<DTO.SaleOrderSearchResult> data = new List<DTO.SaleOrderSearchResult>();
            using (ClientPaymentMngEntities context = CreateContext())
            {
                data = converter.DB2DTO_SaleOrderSearchResultList(context.ClientPaymentMng_function_SearchSaleOrder(ClientID, Currency).ToList());
            }
            return data;
        }

        private List<DTO.SaleOrderForDeductionSearchResult> GetSaleOrderForDeduction(int ClientID, string Currency)
        {
            List<DTO.SaleOrderForDeductionSearchResult> data = new List<DTO.SaleOrderForDeductionSearchResult>();
            using (ClientPaymentMngEntities context = CreateContext())
            {
                data = converter.DB2DTO_SaleOrderForDeductionSearchResultList(context.ClientPaymentMng_function_SearchSaleOrderForDeduction(ClientID, Currency).ToList());
            }
            return data;
        }
    }
}
