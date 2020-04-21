using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceOtherMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryInvoiceOtherMngEntities CreateContext()
        {
            return new FactoryInvoiceOtherMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryInvoiceOtherMngModel"));
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
                using (FactoryInvoiceOtherMngEntities context = CreateContext())
                {
                    string Season = null;
                    int? SupplierID = null;
                    int UserID = -1;
                    string InvoiceNo = null;
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
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
                    }
                    totalRows = context.FactoryInvoiceOtherMng_function_SearchInvoice(Season, SupplierID, UserID, InvoiceNo, Description, orderBy, orderDirection).Count();
                    var result = context.FactoryInvoiceOtherMng_function_SearchInvoice(Season, SupplierID, UserID, InvoiceNo, Description, orderBy, orderDirection);
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
                if (fwFactory.CheckSupplierPermission(userId, dtoFactoryInvoice.SupplierID.Value) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected supplier data");
                }
                if (id > 0 && fwFactory.CheckFactoryInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected invoice data");
                }

                using (FactoryInvoiceOtherMngEntities context = CreateContext())
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

                        converter.DTO2DB(dtoFactoryInvoice, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        context.SaveChanges();
                    }
                    dtoItem = GetData(userId, dbItem.FactoryInvoiceID, -1, out notification).Data;
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
                using (FactoryInvoiceOtherMngEntities context = CreateContext())
                {
                    FactoryInvoice dbItem = context.FactoryInvoice.FirstOrDefault(o => o.FactoryInvoiceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Invoice not found!");
                    }

                    // validate before approve
                    if (string.IsNullOrEmpty(dbItem.ScanFile))
                    {
                        throw new Exception("Scan file is missing!");
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = userId;
                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.FactoryInvoiceID, -1, out notification).Data;
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
                using (FactoryInvoiceOtherMngEntities context = CreateContext())
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
                    dtoItem = GetData(userId, dbItem.FactoryInvoiceID, -1, out notification).Data;
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

            try
            {
                data.Suppliers = supportFactory.GetSupplier(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int supplierID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryInvoice();
            data.Data.FactoryInvoiceExtras = new List<DTO.FactoryInvoiceExtra>();

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
                    if (fwFactory.CheckSupplierPermission(userId, supplierID) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected supplier data");
                    }
                }

                using (FactoryInvoiceOtherMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        data.Data.SupplierID = supplierID;

                        // get supplier info
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
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_FactoryInvoice(context.FactoryInvoiceOtherMng_FactoryInvoice_View
                            .Include("FactoryInvoiceOtherMng_FactoryInvoiceExtra_View")
                            .FirstOrDefault(o => o.FactoryInvoiceID == id));
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
                if (id > 0 && fwFactory.CheckFactoryInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected invoice data");
                }

                using (FactoryInvoiceOtherMngEntities context = CreateContext())
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
