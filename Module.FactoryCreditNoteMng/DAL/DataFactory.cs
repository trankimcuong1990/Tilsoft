using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryCreditNoteMngEntities CreateContext()
        {
            return new FactoryCreditNoteMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryCreditNoteMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryCreditNoteSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryCreditNoteMngEntities context = CreateContext())
                {
                    string Season = null;
                    int? SupplierID = null;
                    int UserID = -1;
                    string CreditNoteNo = null;
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
                    if (filters.ContainsKey("CreditNoteNo") && !string.IsNullOrEmpty(filters["CreditNoteNo"].ToString()))
                    {
                        CreditNoteNo = filters["CreditNoteNo"].ToString().Replace("'", "''");
                    }
                    totalRows = context.FactoryCreditNoteMng_function_SearchCreditNote(Season, SupplierID, UserID, CreditNoteNo, orderBy, orderDirection).Count();
                    var result = context.FactoryCreditNoteMng_function_SearchCreditNote(Season, SupplierID, UserID, CreditNoteNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryCreditNoteSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.FactoryCreditNote dtoFactoryCreditNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryCreditNote>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                // check permission
                if (fwFactory.CheckSupplierPermission(userId, dtoFactoryCreditNote.SupplierID.Value) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected supplier data");
                }
                if (id > 0 && fwFactory.CheckFactoryCreditNotePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected credit note data");
                }

                using (FactoryCreditNoteMngEntities context = CreateContext())
                {
                    FactoryCreditNote dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryCreditNote();
                        context.FactoryCreditNote.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryCreditNote.FirstOrDefault(o => o.FactoryCreditNoteID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory credit note not found!";
                        return false;
                    }
                    else
                    {
                        // check if credit note is confirmed
                        if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                        {
                            throw new Exception("Can not edit the confirmed credit note!");
                        }

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactoryCreditNote.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoFactoryCreditNote, ref dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;                        

                        // remove orphan
                        context.FactoryCreditNoteDetail.Local.Where(o => o.FactoryCreditNote == null).ToList().ForEach(o => context.FactoryCreditNoteDetail.Remove(o));

                        context.SaveChanges();
                    }
                    dtoItem = GetData(userId, dbItem.FactoryCreditNoteID, -1, string.Empty, out notification).Data;
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
                        case "CreditNoteNoUnique":
                            notification.Message = "Duplicate credit note number (credit note number must be unique)!";
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
            DTO.FactoryCreditNote dtoFactoryCreditNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryCreditNote>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryCreditNoteMngEntities context = CreateContext())
                {
                    FactoryCreditNote dbItem = context.FactoryCreditNote.FirstOrDefault(o => o.FactoryCreditNoteID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Credit note not found!");
                    }

                    // validate before approve
                    if (string.IsNullOrEmpty(dbItem.CreditNoteNo))
                    {
                        throw new Exception("Credit note number is required!");
                    }
                    if (!dbItem.IssuedDate.HasValue)
                    {
                        throw new Exception("Credit note date is required!");
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = userId;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryCreditNoteID, -1, string.Empty, out notification).Data;
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
            DTO.FactoryCreditNote dtoFactoryCreditNote = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryCreditNote>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryCreditNoteMngEntities context = CreateContext())
                {
                    FactoryCreditNote dbItem = context.FactoryCreditNote.FirstOrDefault(o => o.FactoryCreditNoteID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Credit note not found!");
                    }
                    dbItem.IsConfirmed = null;
                    dbItem.ConfirmedDate = null;
                    dbItem.ConfirmedBy = null;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryCreditNoteID, -1, string.Empty, out notification).Data;
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

        public List<DTO.FactoryInvoiceSearchResult> QuickSearchInvoice(int userId, int supplierId, string season, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryInvoiceSearchResult> data = new List<DTO.FactoryInvoiceSearchResult>();

            //try to get data
            try
            {
                using (FactoryCreditNoteMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryInvoiceSearchResultList(context.FactoryCreditNoteMng_function_SearchFactoryInvoice(userId, supplierId, season, query).ToList());
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
            data.Data = new DTO.FactoryCreditNote();
            data.Data.FactoryCreditNoteDetails = new List<DTO.FactoryCreditNoteDetail>();

            //try to get data
            try
            {
                // check permission
                if (id > 0)
                {
                    if (fwFactory.CheckFactoryCreditNotePermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected credit note data");
                    }
                }
                else
                {
                    // check booking permission for invoice with product
                    if (fwFactory.CheckSupplierPermission(userId, supplierID) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected supplier data");
                    }
                }

                using (FactoryCreditNoteMngEntities context = CreateContext())
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
                        data.Data = converter.DB2DTO_FactoryCreditNote(context.FactoryCreditNoteMng_FactoryCreditNote_View
                            .Include("FactoryCreditNoteMng_FactoryCreditNoteDetail_View")
                            .FirstOrDefault(o => o.FactoryCreditNoteID == id));
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
                if (id > 0 && fwFactory.CheckFactoryCreditNotePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected credit note data");
                }

                using (FactoryCreditNoteMngEntities context = CreateContext())
                {
                    FactoryCreditNote dbItem = context.FactoryCreditNote.FirstOrDefault(o => o.FactoryCreditNoteID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Credit note not found!");
                    }

                    // check if invoice already confirmed
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not delete the confirmed credit note!");
                    }

                    // everything ok, delete the invoice
                    context.FactoryCreditNote.Remove(dbItem);
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
