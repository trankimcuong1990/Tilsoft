using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryProformaInvoice2MngEntities CreateContext()
        {
            return new FactoryProformaInvoice2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryProformaInvoice2MngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryProformaInvoiceSearchResult>();
            data.SummaryData = new DTO.SummaryRow();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    int? FactoryID = null;
                    string ProformaInvoiceNo = null;
                    string Season = null;
                    string ClientUD = null;
                    int UserID = -1;
                    if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }                   
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    totalRows = context.FactoryProformaInvoice2Mng_function_SearchFactoryProformaInvoice(FactoryID, ProformaInvoiceNo, Season, ClientUD, UserID , orderBy, orderDirection).Count();
                    var result = context.FactoryProformaInvoice2Mng_function_SearchFactoryProformaInvoice(FactoryID, ProformaInvoiceNo, Season, ClientUD, UserID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryProformaInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    FactoryProformaInvoice2Mng_SummaryRow_View dbSummary = context.FactoryProformaInvoice2Mng_function_SearchFactoryProformaInvoiceSummary(FactoryID, ProformaInvoiceNo, Season, ClientUD, UserID).FirstOrDefault();
                    if (dbSummary != null)
                    {
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
            DTO.FactoryProformaInvoice dtoFactoryProformaInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryProformaInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    if (fwFactory.CheckFactoryPermission(userId, dtoFactoryProformaInvoice.FactoryID.Value) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected factory data");
                    }
                    if (id > 0 && fwFactory.CheckFactoryProformaInvoicePermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected proforma invoice data");
                    }    

                    FactoryProformaInvoice2 dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryProformaInvoice2();
                        context.FactoryProformaInvoice2.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryProformaInvoice2.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactoryProformaInvoice.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // check if item already in other proforma invoice or if item belong to the filter: client, factory, season
                        int itemID;
                        int clientID = dtoFactoryProformaInvoice.ClientID.Value;
                        int factoryID = dtoFactoryProformaInvoice.FactoryID.Value;
                        string season = dtoFactoryProformaInvoice.Season;
                        List<DTO.FactoryOrderItemSearchResult> checkList = converter.DB2DTO_FactoryOrderItemSearchResultList(context.FactoryProformaInvoice2Mng_FactoryOrderItemSearchResult_View.Where(o => o.ClientID == clientID && o.FactoryID == factoryID && o.Season == season).ToList());
                        foreach (DTO.FactoryProformaInvoiceDetail dtoDetail in dtoFactoryProformaInvoice.FactoryProformaInvoiceDetails)
                        {                            
                            if (dtoDetail.FactoryOrderDetailID.HasValue)
                            {
                                itemID = dtoDetail.FactoryOrderDetailID.Value;
                                if (checkList.FirstOrDefault(o => o.FactoryOrderDetailID == itemID) == null)
                                {
                                    throw new Exception(dtoDetail.Description + " is invalid");
                                }
                                if (context.FactoryProformaInvoiceDetail2.FirstOrDefault(o => o.FactoryOrderDetailID == itemID && o.FactoryProformaInvoiceID != id) != null)
                                {
                                    throw new Exception(dtoDetail.Description + "(" + dtoDetail.FactoryOrderUD + " - " + dtoDetail.LDS + ")" + " is already exists in other proforma invoice");
                                }
                            }
                            if (dtoDetail.FactoryOrderSparepartDetailID.HasValue)
                            {
                                itemID = dtoDetail.FactoryOrderSparepartDetailID.Value;
                                if (checkList.FirstOrDefault(o => o.FactoryOrderSparepartDetailID == itemID) == null)
                                {
                                    throw new Exception(dtoDetail.Description + " is invalid");
                                }
                                if (context.FactoryProformaInvoiceDetail2.FirstOrDefault(o => o.FactoryOrderSparepartDetailID == itemID && o.FactoryProformaInvoiceID != id) != null)
                                {
                                    throw new Exception(dtoDetail.Description + "(" + dtoDetail.FactoryOrderUD + " - " + dtoDetail.LDS + ")" + " is already exists in other proforma invoice");
                                }
                            }
                        }

                        if ((dbItem.IsFurnindoConfirmed.HasValue && dbItem.IsFurnindoConfirmed.Value) || (dbItem.IsFactoryConfirmed.HasValue && dbItem.IsFactoryConfirmed.Value))
                        {
                            converter.DTO2DB(dtoFactoryProformaInvoice, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", true);
                        }
                        else
                        {
                            converter.DTO2DB(dtoFactoryProformaInvoice, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", false);
                        }
                        
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphan
                        context.FactoryProformaInvoiceDetail2.Local.Where(o => o.FactoryProformaInvoice2 == null).ToList().ForEach(o => context.FactoryProformaInvoiceDetail2.Remove(o));

                        // generate p/i number
                        if (id <= 0)
                        {
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM FactoryProformaInvoice2 WITH (TABLOCKX, HOLDLOCK)");
                                context.Database.ExecuteSqlCommand("SELECT * FROM FactoryProformaInvoiceDetail2 WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    dbItem.ProformaInvoiceNo = context.FactoryProformaInvoice2Mng_function_GeneratePINumber(dbItem.FactoryID, dbItem.ClientID, dbItem.Season).FirstOrDefault();
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
                        else
                        {
                            context.SaveChanges();
                        }                       

                        dtoItem = GetData(userId, dbItem.FactoryProformaInvoiceID, -1, -1, string.Empty, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.Factories = supportFactory.GetFactory(userId);
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
            data.Seasons = new List<Support.DTO.Season>();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.Factories = supportFactory.GetFactory(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int clientID, int factoryID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactoryProformaInvoice();
            data.DeliveryTerms = new List<Support.DTO.DeliveryTerm>();
            data.PaymentTerms = new List<Support.DTO.PaymentTerm>();
            data.Data.FactoryProformaInvoiceDetails = new List<DTO.FactoryProformaInvoiceDetail>();

            //try to get data
            try
            {
                // check permission
                if (id <= 0 && fwFactory.CheckFactoryPermission(userId, factoryID) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected factory data");
                }
                if (id > 0 && fwFactory.CheckFactoryProformaInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected proforma invoice data");
                }  

                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        data.Data.Season = season;
                        data.Data.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
                        data.Data.ClientID = clientID;
                        data.Data.FactoryID = factoryID;
                        data.Data.ClientUD = supportFactory.GetClient().FirstOrDefault(o => o.ClientID == clientID).ClientUD;
                        data.Data.FactoryUD = supportFactory.GetFactory().FirstOrDefault(o => o.FactoryID == factoryID).FactoryUD;
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_FactoryProformaInvoice(context.FactoryProformaInvoice2Mng_FactoryProformaInvoice_View.Include("FactoryProformaInvoice2Mng_FactoryProformaInvoiceDetail_View").FirstOrDefault(o => o.FactoryProformaInvoiceID == id));
                    }
                }
                data.DeliveryTerms = supportFactory.GetDeliveryTerm();
                data.PaymentTerms = supportFactory.GetPaymentTerm();
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
                if (id > 0 && fwFactory.CheckFactoryProformaInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected proforma invoice data");
                } 

                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    // check if can delete
                    FactoryProformaInvoice2 dbItem = context.FactoryProformaInvoice2.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Factory proforma invoice not found");
                    }
                    if ((dbItem.IsFurnindoConfirmed.HasValue && dbItem.IsFurnindoConfirmed.Value) || (dbItem.IsFactoryConfirmed.HasValue && dbItem.IsFactoryConfirmed.Value))
                    {
                        throw new Exception("Can not delete the proforma invoice: it's already been confirmed by one party");
                    }

                    // everything ok, delete
                    // remove attached file
                    if (!string.IsNullOrEmpty(dbItem.AttachedFile))
                    {
                        fwFactory.RemoveImageFile(dbItem.AttachedFile);
                    }
                    context.FactoryProformaInvoice2.Remove(dbItem);
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

        public List<Support.DTO.Client> QuickSearchClient(int userId, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<Support.DTO.Client> data = new List<Support.DTO.Client>();

            //try to get data
            try
            {
                foreach (Support.DTO.Client dtoClient in supportFactory.GetClient().Where(o => o.ClientUD.Contains(query)).ToList())
                {
                    data.Add(new Support.DTO.Client() { ClientID = dtoClient.ClientID, ClientUD = dtoClient.ClientUD, Id = dtoClient.ClientID, Value = dtoClient.ClientUD, Label = dtoClient.ClientUD });
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.FactoryOrderItemSearchResult> QuickSearchItem(int userId, int clientId, int factoryId, int factoryProformaInvoiceId, string season, string description, string factoryOrderUd, string itemType, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderItemSearchResult> data = new List<DTO.FactoryOrderItemSearchResult>();
            
            // check factory permission
            if (fwFactory.CheckFactoryPermission(userId, factoryId) == 0)
            {
                return data;
            }

            //try to get data
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderItemSearchResultList(context.FactoryProformaInvoice2Mng_function_QuickSearchFactoryOrderItem(description, description, factoryOrderUd, itemType, clientId, userId, season, factoryId, factoryProformaInvoiceId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool FurnindoConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryProformaInvoice dtoFactoryProformaInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryProformaInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    FactoryProformaInvoice2 dbItem = context.FactoryProformaInvoice2.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }

                    // check if ok for furnindo confirm
                    if (dbItem.IsFurnindoConfirmed.HasValue && dbItem.IsFurnindoConfirmed.Value)
                    {
                        notification.Message = "The proforma invoice is already confirmed by Furnindo!";
                        return false;
                    }

                    dbItem.IsFurnindoConfirmed = true;
                    dbItem.FurnindoConfirmedBy = userId;
                    dbItem.FurnindoConfirmedDate = DateTime.Now;
                    dbItem.FurnindoConfirmedRemark = dtoFactoryProformaInvoice.FurnindoConfirmedRemark;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactoryProformaInvoiceID, -1, -1, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }
        public bool FurnindoUnConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    FactoryProformaInvoice2 dbItem = context.FactoryProformaInvoice2.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }

                    dbItem.IsFurnindoConfirmed = null;
                    dbItem.FurnindoConfirmedBy = null;
                    dbItem.FurnindoConfirmedDate = null;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactoryProformaInvoiceID, -1, -1, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }
        public bool FactoryConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryProformaInvoice dtoFactoryProformaInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryProformaInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    FactoryProformaInvoice2 dbItem = context.FactoryProformaInvoice2.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }

                    // check if ok for factory confirm
                    if (dbItem.IsFactoryConfirmed.HasValue && dbItem.IsFactoryConfirmed.Value)
                    {
                        notification.Message = "The proforma invoice is already confirmed by factory!";
                        return false;
                    }

                    dbItem.IsFactoryConfirmed = true;
                    dbItem.FactoryConfirmedBy = userId;
                    dbItem.FactoryConfirmedDate = DateTime.Now;
                    dbItem.FactoryConfirmedRemark = dtoFactoryProformaInvoice.FactoryConfirmedRemark;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactoryProformaInvoiceID, -1, -1, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }
        public bool FactoryUnConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryProformaInvoice2MngEntities context = CreateContext())
                {
                    FactoryProformaInvoice2 dbItem = context.FactoryProformaInvoice2.FirstOrDefault(o => o.FactoryProformaInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Factory proforma invoice not found!";
                        return false;
                    }

                    dbItem.IsFactoryConfirmed = null;
                    dbItem.FactoryConfirmedBy = null;
                    dbItem.FactoryConfirmedDate = null;
                    context.SaveChanges();

                    dtoItem = GetData(userId, dbItem.FactoryProformaInvoiceID, -1, -1, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public string GetReportData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckFactoryProformaInvoicePermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected proforma invoice data");
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryProformaInvoice2Mng_function_getPrintData", new SqlConnection(FrameworkSetting.Setting.SqlConnection));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryProformaInvoiceID", id);
                adap.TableMappings.Add("Table", "FactoryProformaInvoice2Mng_FactoryProformaInvoicePrintVersion_View");
                adap.TableMappings.Add("Table1", "FactoryProformaInvoice2Mng_FactoryProformaInvoiceDetailPrintVersion_View");
                adap.Fill(ds);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "FactoryProformaInvoice");
                //return string.Empty;

                // generate xml file
                return Library.Helper.CreateCOMReportFileImportDataOnly2(ds, "FactoryProformaInvoice");
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
    }
}
