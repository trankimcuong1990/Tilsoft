using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.PurchasingCreditNote.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        private PurchasingCreditNoteEntities CreateContext()
        {
            return new PurchasingCreditNoteEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchasingCreditNoteModel"));
        }

        public List<DTO.PurchasingCreditNote> GetPurchasingCreditNoteSearchResult(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string invoiceNo = string.Empty;
            string blNo = string.Empty;
            string supplierNM = string.Empty;
            string creditNoteNo = string.Empty;
            int? factoryID = null;

            if (filters.ContainsKey("invoiceNo")) invoiceNo = filters["invoiceNo"].ToString();
            if (filters.ContainsKey("blNo")) blNo = filters["blNo"].ToString();
            if (filters.ContainsKey("supplierNM")) supplierNM = filters["supplierNM"].ToString();
            if (filters.ContainsKey("creditNoteNo")) creditNoteNo = filters["creditNoteNo"].ToString();
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null) factoryID = Convert.ToInt32(filters["factoryID"].ToString());

            try
            {
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    totalRows = context.PurchasingCreditNoteMng_function_SearchPurchasingCreditNote(userId, orderBy, orderDirection, invoiceNo, blNo, supplierNM, creditNoteNo, factoryID).Count();
                    var result = context.PurchasingCreditNoteMng_function_SearchPurchasingCreditNote(userId, orderBy, orderDirection, invoiceNo, blNo, supplierNM, creditNoteNo, factoryID);
                    return converter.DB2DTO_PurchasingCreditNoteSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.PurchasingCreditNote>();
            }
        }

        public  DTO.PurchasingCreditNote GetPurchasingCreditNote(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on booking
                if (fwFactory.CheckPurchasingCreditNotePermission(userId, id) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    DTO.PurchasingCreditNote dtoItem = new DTO.PurchasingCreditNote();
                    var dbItem = context.PurchasingCreditNoteMng_PurchasingCreditNote_View
                                    .Include("PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View")
                                    .Include("PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View")
                                    .Include("PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View")
                                    .FirstOrDefault(o => o.PurchasingCreditNoteID == id);
                    dtoItem = converter.DB2DTO_PurchasingCreditNote(dbItem);
                    return dtoItem;
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
                return new DTO.PurchasingCreditNote();
            }
        }

        public DTO.PurchasingInvoice GetPurchasingInvoice(int userId, int? id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on booking
                if (fwFactory.CheckPurchasingInvoicePermission(userId, id.Value) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    DTO.PurchasingInvoice dtoItem = new DTO.PurchasingInvoice();
                    var dbItem = context.PurchasingCreditNoteMng_PurchasingInvoice_View
                                    .Include("PurchasingInvoiceDetail_View")
                                    .Include("PurchasingInvoiceSparepartDetail_View")
                                    .FirstOrDefault(o => o.PurchasingInvoiceID == id);
                    dtoItem = converter.DB2DTO_PurchasingInvoice(dbItem);
                    return dtoItem;
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
                return new DTO.PurchasingInvoice();
            }
        }

        public bool UpdatePurchasingCreditNote(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchasingCreditNote dtoCreditNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PurchasingCreditNote>();
            dtoCreditNote.UpdatedBy = userId;
            try
            {
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    PurchasingCreditNote dbItem;
                    if (id > 0)
                    {
                        //check permission on booking
                        if (fwFactory.CheckPurchasingCreditNotePermission(userId, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this invoice");
                        }

                        dbItem = context.PurchasingCreditNote.Where(o => o.PurchasingCreditNoteID == id).FirstOrDefault();

                        //check credit note is confirmed
                        if (dbItem!=null && dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed == true)
                        {
                            throw new Exception("Credit note has been confirmed. You can not change.");
                        }
                    }
                    else
                    {
                        //check permission
                        if (dtoCreditNote.PurchasingInvoiceID.HasValue || dtoCreditNote.SupplierID.HasValue)
                        {
                            //check permission on invoice
                            if (dtoCreditNote.PurchasingInvoiceID.HasValue && fwFactory.CheckPurchasingInvoicePermission(userId, dtoCreditNote.PurchasingInvoiceID.Value) == 0)
                            {
                                throw new Exception("You do not have access permission on this invoice");
                            }
                            if (dtoCreditNote.SupplierID.HasValue && fwFactory.CheckSupplierPermission(userId, dtoCreditNote.SupplierID.Value) == 0)
                            {
                                throw new Exception("You do not have access permission on this factory");
                            }
                        }
                        else
                        {
                            throw new Exception("There are not invoice or supplier to check permission to create credit note");
                        }                        
                        dbItem = new PurchasingCreditNote();
                        context.PurchasingCreditNote.Add(dbItem);
                    }

                    if (dbItem != null)
                    {
                        //check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoCreditNote.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //check put in factory balance ?
                        if (string.IsNullOrEmpty(dtoCreditNote.Season))
                        {
                            throw new Exception("You need fill-in season to create credit note");
                        }
                        if (context.PurchasingCreditNoteMng_function_CheckCreditNotePutInBalance(dtoCreditNote.PurchasingInvoiceID, dtoCreditNote.FactoryID, dtoCreditNote.Season).FirstOrDefault().Value > 0)
                        {
                            throw new Exception("This supplier was make balance sheet in season " + dtoCreditNote.Season + ". You can not create credit note");
                        }

                        //check permission product item
                        List<int?> supplierIDs = fwFactory.GetListSupplierByUser(userId);

                        List<int?> dtoInvoiceDetailIDs = dtoCreditNote.PurchasingCreditNoteDetails.Where(o => o.PurchasingCreditNoteDetailID < 0).Select(s => s.PurchasingInvoiceDetailID).ToList();
                        List<int?> dtoCreditNoteDetailIDs = dtoCreditNote.PurchasingCreditNoteDetails.Where(o => o.PurchasingCreditNoteDetailID > 0).Select(s => s.PurchasingCreditNoteDetailID).ToList();
                        if (dtoInvoiceDetailIDs.Count() > 0 && context.PurchasingCreditNoteMng_PurchasingInvoiceDetail_View.Where(o => dtoInvoiceDetailIDs.Contains(o.PurchasingInvoiceDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                        {
                            throw new Exception("You do not have access permission on these products that you added to credit note");
                        }
                        if (dtoCreditNoteDetailIDs.Count() > 0 && context.PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View.Where(o => dtoCreditNoteDetailIDs.Contains(o.PurchasingCreditNoteDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                        {
                            throw new Exception("You do not have access permission on these products that you added to credit note");
                        }

                        //check permission sparepart detail
                        List<int?> dtoInvoiceSparepartDetailIDs    = dtoCreditNote.PurchasingCreditNoteSparepartDetails.Where(o => o.PurchasingCreditNoteSparepartDetailID < 0).Select(s => s.PurchasingInvoiceSparepartDetailID).ToList();
                        List<int?> dtoCreditNoteSparepartDetailIDs = dtoCreditNote.PurchasingCreditNoteSparepartDetails.Where(o => o.PurchasingCreditNoteSparepartDetailID > 0).Select(s => s.PurchasingCreditNoteSparepartDetailID).ToList();
                        if (dtoInvoiceSparepartDetailIDs.Count() > 0 && context.PurchasingCreditNoteMng_PurchasingInvoiceSparepartDetail_View.Where(o => dtoInvoiceSparepartDetailIDs.Contains(o.PurchasingInvoiceSparepartDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                        {
                            throw new Exception("You do not have access permission on these sparepart that you added to credit note");
                        }
                        if (dtoCreditNoteSparepartDetailIDs.Count() > 0 && context.PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View.Where(o => dtoCreditNoteSparepartDetailIDs.Contains(o.PurchasingCreditNoteSparepartDetailID)).Select(s => s.SupplierID).ToList().Except(supplierIDs).Count() > 0)
                        {
                            throw new Exception("You do not have access permission on these sparepart that you added to credit note");
                        }

                        //check quantity
                        foreach (var item in dtoCreditNote.PurchasingCreditNoteDetails.Where(o => o.Quantity.HasValue))
                        {
                            if (item.UnitPrice.HasValue && item.UnitPrice.Value < 0)
                            {
                                throw new Exception("Price must be not negavitve");
                            }
                            if (item.Quantity > 0)
                            {
                                throw new Exception("Quantity in credit note must be negative");
                            }
                            else
                            {
                                int? compareValue = 0;
                                if (item.PurchasingCreditNoteDetailID < 0)
                                {
                                     compareValue = item.Quantity; // -2
                                }
                                else
                                {
                                    var objOldValue = context.PurchasingCreditNoteDetail.Where(o => o.PurchasingCreditNoteDetailID == item.PurchasingCreditNoteDetailID).FirstOrDefault(); //-2
                                    compareValue = item.Quantity - objOldValue.Quantity; // -10 - (-2) = -8
                                }

                                var objRemaining = context.PurchasingCreditNoteMng_PurchasingInvoiceDetail_View.Where(o => o.PurchasingInvoiceDetailID == item.PurchasingInvoiceDetailID);
                                int? remaintQnt = (objRemaining != null && objRemaining.Count() > 0 ? objRemaining.FirstOrDefault().Quantity : 0);
                                if ( -compareValue > remaintQnt)
                                {
                                    throw new Exception("Credit note quantity must be less than purchasing invoice quantity remaining");
                                }
                            }
                        }

                        //check quantity
                        foreach (var item in dtoCreditNote.PurchasingCreditNoteSparepartDetails.Where(o => o.Quantity.HasValue))
                        {
                            if (item.UnitPrice.HasValue && item.UnitPrice.Value < 0)
                            {
                                throw new Exception("Price must be not negavitve");
                            }
                            if (item.Quantity > 0)
                            {
                                throw new Exception("Quantity in credit note must be negative");
                            }
                            else
                            {
                                int? compareValue = 0;
                                if (item.PurchasingCreditNoteSparepartDetailID < 0)
                                {
                                     compareValue = item.Quantity; // -2
                                }
                                else
                                {
                                    var objOldValue = context.PurchasingCreditNoteSparepartDetail.Where(o => o.PurchasingCreditNoteSparepartDetailID == item.PurchasingCreditNoteSparepartDetailID).FirstOrDefault(); //-2
                                    compareValue = item.Quantity - objOldValue.Quantity; // -10 - (-2) = -8
                                }

                                var objRemaining = context.PurchasingCreditNoteMng_PurchasingInvoiceSparepartDetail_View.Where(o => o.PurchasingInvoiceSparepartDetailID == item.PurchasingInvoiceSparepartDetailID);
                                int? remaintQnt  = (objRemaining != null && objRemaining.Count() > 0 ? objRemaining.FirstOrDefault().Quantity : 0);
                                if ( -compareValue > remaintQnt)
                                {
                                    throw new Exception("Credit note quantity must be less than purchasing invoice quantity remaining");
                                }
                            }
                        }

                        foreach (var item in dtoCreditNote.PurchasingCreditNoteExtendDetails.Where(o => o.Amount.HasValue))
                        {
                            if (item.Amount > 0)
                            {
                                throw new Exception("Amount in credit note must be negative");
                            }
                        }

                        converter.DTO2DB_PurchasingCreditNote(dtoCreditNote, ref dbItem);

                        //remove orphan item
                        context.PurchasingCreditNoteDetail.Local.Where(o => o.PurchasingCreditNote == null).ToList().ForEach(o => context.PurchasingCreditNoteDetail.Remove(o));
                        context.PurchasingCreditNoteSparepartDetail.Local.Where(o => o.PurchasingCreditNote == null).ToList().ForEach(o => context.PurchasingCreditNoteSparepartDetail.Remove(o));
                        context.PurchasingCreditNoteExtendDetail.Local.Where(o => o.PurchasingCreditNote == null).ToList().ForEach(o => context.PurchasingCreditNoteExtendDetail.Remove(o));

                        context.SaveChanges();
                        dtoItem = GetPurchasingCreditNote(userId, dbItem.PurchasingCreditNoteID, out notification);
                    }
                    else
                    {
                        throw new Exception(Library.Helper.TEXT_UPDATED_UNSUCCESS_NOTFOUND);
                    }
                    return true;
                }
            }
            catch (Exception ex) {
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

        public List<DTO.CreditNoteType> GetCreditNoteType(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success};
            List<DTO.CreditNoteType> data = new List<DTO.CreditNoteType>();
            data.Add(new DTO.CreditNoteType { Value = 1, Name = "CREDIT NOTE ON FOB PURCHASING INVOICE"});
            data.Add(new DTO.CreditNoteType { Value = 2, Name = "CREDIT NOTE ON SUPPLIER" });
            return data;
        }

        public DTO.PurchasingInvoiceSearchData GetPurchasingInvoiceSearchResult(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PurchasingInvoiceSearchData data = new DTO.PurchasingInvoiceSearchData();

            string invoiceQuerySearch = string.Empty;

            int pageSize = 20;
            int pageIndex = 1;
            string sortedBy = string.Empty;
            string sortedDirection = string.Empty;

            //search info
            if (filters.ContainsKey("invoiceQuerySearch")) invoiceQuerySearch = filters["invoiceQuerySearch"].ToString();

            //pager info
            if (filters.ContainsKey("pageSize")) pageSize = Convert.ToInt32(filters["pageSize"].ToString());
            if (filters.ContainsKey("pageIndex")) pageIndex = Convert.ToInt32(filters["pageIndex"].ToString());
            if (filters.ContainsKey("sortedBy")) sortedBy = filters["sortedBy"].ToString();
            if (filters.ContainsKey("sortedDirection")) sortedDirection = filters["sortedDirection"].ToString();

            try
            {
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    data.TotalRows = context.PurchasingCreditNoteMng_function_SearchPurchasingInvoice(userId, sortedBy, sortedDirection, invoiceQuerySearch).Count();
                    var result = context.PurchasingCreditNoteMng_function_SearchPurchasingInvoice(userId, sortedBy, sortedDirection, invoiceQuerySearch);
                    data.Data = converter.DB2DTO_PurchasingInvoiceSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    return data;
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
                return new DTO.PurchasingInvoiceSearchData();
            }
        }

        public  bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on booking
                if (fwFactory.CheckPurchasingCreditNotePermission(userId, id) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    var dbItem = context.PurchasingCreditNote.Where(o => o.PurchasingCreditNoteID == id).FirstOrDefault();
                    foreach (var item in dbItem.PurchasingCreditNoteDetail.ToArray())
                    {
                        context.PurchasingCreditNoteDetail.Remove(item);
                    }
                    foreach (var item in dbItem.PurchasingCreditNoteSparepartDetail.ToArray())
                    {
                        context.PurchasingCreditNoteSparepartDetail.Remove(item);
                    }
                    foreach (var item in dbItem.PurchasingCreditNoteExtendDetail.ToArray())
                    {
                        context.PurchasingCreditNoteExtendDetail.Remove(item);
                    }
                    context.PurchasingCreditNote.Remove(dbItem);
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

        public bool SetStatus(int id,int SetstatusBy,bool status, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on invoice
                if (fwFactory.CheckPurchasingCreditNotePermission(SetstatusBy, id) == 0)
                {
                    throw new Exception("You do not have access permission on this invoice");
                }
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    context.PurchasingCreditNoteMng_function_SetStatus(id, SetstatusBy, status);
                    return true;
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

        public DTO.PurchasingCreditNoteSearchData GetPurchasingCreditNoteSearchResult2(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            DTO.PurchasingCreditNoteSearchData data = new DTO.PurchasingCreditNoteSearchData();
            data.Data = new List<DTO.PurchasingCreditNote>();
            data.DataFactory = new List<Support.DTO.Factory>();
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();

            string invoiceNo = string.Empty;
            string blNo = string.Empty;
            string supplierNM = string.Empty;
            string creditNoteNo = string.Empty;
            int? factoryID = null;

            if (filters.ContainsKey("invoiceNo")) invoiceNo = filters["invoiceNo"].ToString();
            if (filters.ContainsKey("blNo")) blNo = filters["blNo"].ToString();
            if (filters.ContainsKey("supplierNM")) supplierNM = filters["supplierNM"].ToString();
            if (filters.ContainsKey("creditNoteNo")) creditNoteNo = filters["creditNoteNo"].ToString();
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null) factoryID = Convert.ToInt32(filters["factoryID"].ToString());

            try
            {
                using (PurchasingCreditNoteEntities context = CreateContext())
                {
                    totalRows = context.PurchasingCreditNoteMng_function_SearchPurchasingCreditNote(userId, orderBy, orderDirection, invoiceNo, blNo, supplierNM, creditNoteNo, factoryID).Count();
                    var result = context.PurchasingCreditNoteMng_function_SearchPurchasingCreditNote(userId, orderBy, orderDirection, invoiceNo, blNo, supplierNM, creditNoteNo, factoryID);

                    data.Data = converter.DB2DTO_PurchasingCreditNoteSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.DataFactory = support_factory.GetFactory(userId);
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
    }
}
