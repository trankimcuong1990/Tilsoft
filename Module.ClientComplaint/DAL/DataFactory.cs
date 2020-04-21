using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AutoMapper;
using Library;
using Module.Support.DAL;
using System.Data;

namespace Module.ClientComplaint.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private readonly DataConverter _converter = new DataConverter();
        private readonly Support.DAL.DataFactory _supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private static readonly Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private readonly string frontendURL = "http://app.tilsoft.bg/";
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            _tempFolder = tempFolder + @"\";
        }
        private ClientComplaintEntities CreateContext()
        {
            return new ClientComplaintEntities(Helper.CreateEntityConnectionString("DAL.ClientComplaintModel"));
        }

        public DTO.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var data = new DTO.InitFormData { Seasons = new List<Support.DTO.Season>() };

            try
            {
                data.Seasons = _supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.FactoryOrderDetailItems QuickSearchFactoryOrderDetailItems(Hashtable filters, out Library.DTO.Notification notification)
        {
            var data = new DTO.FactoryOrderDetailItems() { TotalRows = 0 };
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //search info
            int? clientId;
            string season;
            string factoryOrderUd;

            TryParseFilter(filters, "clientId", out clientId);
            TryParseFilter(filters, "season", out season);
            TryParseFilter(filters, "searchQuery", out factoryOrderUd);

            //pager info
            int pageSize;
            int pageIndex;
            string sortedBy;
            string sortedDirection;

            if (!TryParseFilter(filters, "pageSize", out pageSize)) pageSize = 20;
            if (!TryParseFilter(filters, "pageIndex", out pageIndex)) pageIndex = 1;
            TryParseFilter(filters, "sortedBy", out sortedBy);
            TryParseFilter(filters, "sortedDirection", out sortedDirection);

            try
            {
                using (var context = CreateContext())
                {
                    data.TotalRows = context.ClientComplaint_function_FactoryOrderDetailItemsByClientSeason(sortedBy, sortedDirection, clientId, season, factoryOrderUd).Count();

                    var result = context.ClientComplaint_function_FactoryOrderDetailItemsByClientSeason(sortedBy, sortedDirection, clientId, season, factoryOrderUd);

                    data.Data = _converter.DB2DTO_ClientComplaintFactoryOrderDetailItemList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return data;
            }
        }

        public DTO.ProformaInvoiceItems QuickSearchPIs(Hashtable filters, out Library.DTO.Notification notification)
        {
            var data = new DTO.ProformaInvoiceItems() { TotalRows = 0 };
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //search info
            int? clientId;
            string piNumber;

            TryParseFilter(filters, "clientId", out clientId);
            TryParseFilter(filters, "searchQuery", out piNumber);

            //pager info
            int pageSize;
            int pageIndex;
            string sortedBy;
            string sortedDirection;

            if (!TryParseFilter(filters, "pageSize", out pageSize)) pageSize = 20;
            if (!TryParseFilter(filters, "pageIndex", out pageIndex)) pageIndex = 1;
            TryParseFilter(filters, "sortedBy", out sortedBy);
            TryParseFilter(filters, "sortedDirection", out sortedDirection);

            try
            {
                using (var context = CreateContext())
                {
                    data.TotalRows = context.ClientComplaint_function_SearchProformaInvoiceByClient(sortedBy, sortedDirection, clientId, piNumber).Count();

                    var result = context.ClientComplaint_function_SearchProformaInvoiceByClient(sortedBy, sortedDirection, clientId, piNumber);
                    data.Data = _converter.DB2DTO_ClientComplaintProformaInvoiceItemList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return data;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var data = new DTO.SearchFormData { Data = new List<DTO.ClientComplaintSearchResult>() };
            totalRows = 0;

            try
            {
                using (var context = CreateContext())
                {
                    string complaintNumber;
                    string clientUd;
                    string clientNm;
                    string season;
                    int? complaintStatus;
                    int? complaintType;
                    int? saleId;


                    TryParseFilter(filters, "complaintNumber", out complaintNumber);
                    TryParseFilter(filters, "clientUD", out clientUd);
                    TryParseFilter(filters, "clientNM", out clientNm);
                    TryParseFilter(filters, "saleID", out saleId);
                    TryParseFilter(filters, "season", out season);
                    TryParseFilter(filters, "complaintType", out complaintType);
                    TryParseFilter(filters, "complaintStatus", out complaintStatus);


                    data.TotalRows = context.ClientComplaint_function_SearchClientComplaint(complaintNumber, clientUd, clientNm, saleId, season, complaintType, complaintStatus, orderBy, orderDirection).Count();

                    var result = context.ClientComplaint_function_SearchClientComplaint(complaintNumber, clientUd,
                        clientNm, saleId, season, complaintType, complaintStatus, orderBy, orderDirection);

                    data.Data = _converter.DB2DTO_ClientComplaintSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }

                data.Seasons = _supportFactory.GetSeason();
                data.Sales = _supportFactory.GetSaler();
                data.ComplaintStatuses = _supportFactory.GetConstantEntries(EntryGroupType.ComplaintStatus);
                data.ComplaintTypes = _supportFactory.GetConstantEntries(EntryGroupType.ComplaintType);
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

        public DTO.EditFormData GetData(int userId, int id, int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var data = new DTO.EditFormData { Data = new DTO.ClientComplaint() };

            try
            {

                using (var context = CreateContext())
                {
                    if (id <= 0)
                    {
                        // create new
                        data.Data.YearSeason = season;
                        data.Data.ClientID = clientId;
                        data.Data.ReceivedDate = DateTime.Now.ToShortDateString();
                        data.Data.ClientComplaintItems = new List<DTO.ClientComplaintItem>();
                        data.Data.ClientComplaintUsers = new List<DTO.ClientComplaintUser>();

                        var client = _supportFactory.GetClient().FirstOrDefault(o => o.ClientID == clientId);
                        if (client != null)
                        {
                            data.Data.ClientUD = client.ClientUD;
                            data.Data.ClientNM = client.ClientNM;

                            var saleMn = _supportFactory.GetSaler().FirstOrDefault(s => s.SaleID == client.SaleID);
                            if (saleMn != null)
                            {
                                data.Data.SaleNM = saleMn.SaleNM;
                            }
                        }
                    }
                    else
                    {
                        // access data from db
                        var item = context.ClientComplaint_ClientComplaint_View.FirstOrDefault(o => o.ClientComplaintID == id);
                        data.Data = _converter.DB2DTO_ClientComplaint(item);
                    }
                }

                data.Employees = _supportFactory.GetEmployee().ToList();
                data.ComplaintStatuses = _supportFactory.GetConstantEntries(EntryGroupType.ComplaintStatus);
                data.ComplaintTypes = _supportFactory.GetConstantEntries(EntryGroupType.ComplaintType);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private static string GenerateClientComplaintNumber(ClientComplaintEntities context)
        {
            // get max id of new item
            var maxId = 0;
            var clientComplaint = context.ClientComplaints.OrderByDescending(c => c.ClientComplaintID).FirstOrDefault();
            if (clientComplaint != null)
                maxId = clientComplaint.ClientComplaintID;

            return (++maxId).ToString("D6");
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ClientComplaints.FirstOrDefault(o => o.ClientComplaintID == id);
                    if (dbItem == null) return true;

                    foreach (ClientComplaintItem dbComplaintItem in dbItem.ClientComplaintItems.ToArray())
                    {
                        foreach (ClientComplaintItemImage dbImage in dbComplaintItem.ClientComplaintItemImages.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbImage.ImageFile))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbImage.ImageFile);
                            }
                        }
                    }
                    context.ClientComplaints.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return false;
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            var dtoClientComplaint = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ClientComplaint>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    ClientComplaint dbItem;
                    if (id <= 0)
                    {
                        dbItem = new ClientComplaint
                        {
                            ReceivedDate = DateTime.Now,
                            CreatedBy = userId
                        };

                        context.ClientComplaints.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ClientComplaints.SingleOrDefault(o => o.ClientComplaintID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = string.Format("ClientComplaint [id={0}] not found!", id);
                        return false;
                    }

                    _converter.DTO2DB_ClientComplaint(dtoClientComplaint, ref dbItem, userId, _tempFolder);

                    // remove orphan items
                    context.ClientComplaintItems.Local.Where(o => o.ClientComplaint == null).ToList().ForEach(o => context.ClientComplaintItems.Remove(o));
                    context.ClientComplaintUsers.Local.Where(o => o.ClientComplaint == null).ToList().ForEach(o => context.ClientComplaintUsers.Remove(o));
                    context.ClientComplaintCommunications.Local.Where(o => o.ClientComplaint == null).ToList().ForEach(o => context.ClientComplaintCommunications.Remove(o));
                    context.ClientComplaintItemImages.Local.Where(o => o.ClientComplaintItem == null).ToList().ForEach(o => context.ClientComplaintItemImages.Remove(o));

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    SendEmailNotification(context);
                    context.SaveChanges();

                    if (id <= 0)
                    {
                        dbItem.ComplaintNumber = dbItem.ClientComplaintID.ToString("D6");
                        context.SaveChanges();
                    }

                    dtoItem = GetData(userId, dbItem.ClientComplaintID, dbItem.ClientID, dbItem.YearSeason, out notification);
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

        private static void SendEmailNotification(ClientComplaintEntities context)
        {
            try
            {
                var messageBody = new StringBuilder();
                var messageTitle = "";
                var messageSendTo = "";
                ClientComplaint complaint = null;
                string description = string.Empty;
                int? LoadingPlanDetailID = null;
                foreach (var entry in context.ChangeTracker.Entries().Where(o => o.State != EntityState.Unchanged && o.State != EntityState.Detached))
                {
                    ClientComplaintItem item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (entry.Entity is ClientComplaint)
                            {
                                // new complaint added
                                complaint = entry.Entity as ClientComplaint;
                                messageTitle = string.Format("Client complaint added, number: [{0}]. Received Date: [{1}]", complaint.ComplaintNumber, complaint.ReceivedDate);
                            }
                            else if (entry.Entity is ClientComplaintItem)
                            {
                                item = entry.Entity as ClientComplaintItem;
                                complaint = context.ClientComplaints.FirstOrDefault(o => o.ClientComplaintID == item.ClientComplaintID);
                                description = context.ClientComplaint_ItemInfo_View.FirstOrDefault(o => o.LoadingPlanDetailID == item.LoadingPlanDetailID.Value).ModelNM;
                                messageTitle = string.Format("Some changes on client complaint number: [{0}]. Received Date: [{1}]", complaint.ComplaintNumber, complaint.ReceivedDate);
                                messageBody.AppendFormat("Add new complaint item [{0}] description: [{1}]<br/>", description, item.ComplaintDescription);
                            }
                            break;

                        case EntityState.Deleted:
                        case EntityState.Modified:
                            if (entry.Entity is ClientComplaint)
                            {
                                // new complaint added
                                complaint = entry.Entity as ClientComplaint;
                                messageTitle = string.Format("Some changes on client complaint number: [{0}]. Received Date: [{1}]", complaint.ComplaintNumber, complaint.ReceivedDate);
                            }
                            else if (entry.Entity is ClientComplaintItem)
                            {
                                item = entry.Entity as ClientComplaintItem;
                                if (item != null)
                                {
                                    complaint = context.ClientComplaints.FirstOrDefault(o => o.ClientComplaintID == item.ClientComplaintID);
                                    if (complaint != null && complaint.ClientComplaintID > 0)
                                    {
                                        messageTitle = string.Format("Some changes on client complaint number: [{0}]. Received Date: [{1}]", complaint.ComplaintNumber, complaint.ReceivedDate);

                                        if (entry.State == EntityState.Added)
                                        {
                                            description = context.ClientComplaint_ItemInfo_View.FirstOrDefault(o => o.LoadingPlanDetailID == item.LoadingPlanDetailID.Value).ModelNM;
                                            messageBody.AppendFormat("Add new complaint item [{0}] description: [{1}]<br/>", description, item.ComplaintDescription);
                                        }
                                        else if (entry.State == EntityState.Deleted)
                                        {
                                            LoadingPlanDetailID = (int?)entry.OriginalValues["LoadingPlanDetailID"];
                                            description = context.ClientComplaint_ItemInfo_View.FirstOrDefault(o => o.LoadingPlanDetailID == LoadingPlanDetailID.Value).ModelNM;
                                            messageBody.AppendFormat("Delete a complaint item: [{0}]<br/>", description);
                                        }
                                        else if (entry.State == EntityState.Modified)
                                        {
                                            description = context.ClientComplaint_ItemInfo_View.FirstOrDefault(o => o.LoadingPlanDetailID == item.LoadingPlanDetailID.Value).ModelNM;
                                            messageBody.AppendFormat("Update a complaint item [{0}] description: [{1}]<br/>", description, item.ComplaintDescription);
                                        }
                                    }
                                }
                            }
                            break;

                    }
                }

                if (string.IsNullOrEmpty(messageTitle) || complaint == null) return;

                foreach (var employee in complaint.ClientComplaintUsers)
                {
                    var emp = context.Employees.FirstOrDefault(o => o.EmployeeID == employee.EmployeeID);
                    var profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == emp.UserID);

                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                    {
                        messageSendTo += !string.IsNullOrEmpty(messageSendTo) ? ";" + profile.Email : profile.Email;
                    }
                }

                // add email message to database
                if (!string.IsNullOrEmpty(messageSendTo))
                {
                    var dbEmail = new EmailNotificationMessage
                    {
                        EmailBody = messageBody.ToString(),
                        EmailSubject = messageTitle,
                        SendTo = messageSendTo
                    };
                    context.EmailNotificationMessages.Add(dbEmail);
                }


            }
            catch (Exception ex) { }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ClientComplaints.FirstOrDefault(o => o.ClientComplaintID == id);
                    if (dbItem == null)
                    {
                        notification.Message = string.Format("ClientComplaint [id={0}] not found!", id);
                        return false;
                    }


                    dbItem.ApprovedBy = userId;
                    dbItem.ApprovedDate = DateTime.Now;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.UpdatedBy = userId;

                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.ClientComplaintID, dbItem.ClientID, dbItem.YearSeason, out notification).Data;

                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return false;
            }
        }


        public DTO.PIShipmentStatusData GetShipmentStatusOfPiSolution(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var data = new DTO.PIShipmentStatusData();

            int? clientId;
            string proformaInvoice;

            TryParseFilter(filters, "clientID", out clientId);
            TryParseFilter(filters, "proformaInvoice", out proformaInvoice);

            try
            {

                using (var context = CreateContext())
                {
                    var result = context.ClientComplaint_function_SearchProformaInvoiceShipmentStatus(clientId, proformaInvoice, null, null).ToList();

                    data.IsShipped = result.All(it => it.IsShipped.HasValue && it.IsShipped.Value);
                    data.IsLoaded = result.All(it => it.IsLoaded.HasValue && it.IsLoaded.Value);
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ClientComplaints.FirstOrDefault(o => o.ClientComplaintID == id);
                    if (dbItem == null)
                    {
                        notification.Message = string.Format("ClientComplaint [id={0}] not found!", id);
                        return false;
                    }
                    dbItem.ApprovedBy = null;
                    dbItem.ApprovedDate = null;
                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.UpdatedBy = userId;

                    context.SaveChanges();
                    dtoItem = GetData(userId, dbItem.ClientComplaintID, dbItem.ClientID, dbItem.YearSeason, out notification).Data;

                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                return false;
            }
        }

        public string ExportExcelClientComplaint(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var ds = new ReportDataObject();

            try
            {
                var da = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand("ClientComplaint_function_ExportExcelOverview", new SqlConnection(Helper.GetSQLConnectionString()))
                };

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "ClientComplaintOverview");

                foreach (DataRow dr in ds.Tables["ClientComplaintOverview"].Rows)
                {
                    if (dr["PicturesOfComplaint"].ToString() != "NONE")
                    {
                        var arrImageURL = dr["PicturesOfComplaint"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var listImageURL = arrImageURL.Select(a => FrameworkSetting.Setting.MediaThumbnailUrl + a.ToString()).ToList();

                        dr["PicturesOfComplaint"] = String.Join(",", listImageURL);
                    }
                }
                return Helper.CreateReportFileWithEPPlus2(ds, "ClientComplaintOverview");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    notification.DetailMessage.Add(ex.InnerException.Message);

                return string.Empty;
            }
        }

        public string ExportExcelClientComplaintItem(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            var ds = new ReportDataObject();

            try
            {
                var da = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand("ClientComplaint_function_ExportExcelDetailItem", new SqlConnection(Helper.GetSQLConnectionString()))
                };

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ClientComplaintItemID", filters["clientComplaintItemID"]);
                da.TableMappings.Add("Table", "Data1");
                da.TableMappings.Add("Table1", "SRFData");
                da.TableMappings.Add("Table2", "SRFDataImages");


                da.Fill(ds);

                foreach (DataRow dr in ds.Tables["Data1"].Rows)
                {
                    if (dr["ModelThumbnail"].ToString() != "")
                    {
                        dr["ModelThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["ModelThumbnail"].ToString();
                    }

                    if (dr["MaterialThumbnail"].ToString() != "")
                    {
                        dr["MaterialThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["MaterialThumbnail"].ToString();
                    }

                    if (dr["SubMaterialThumbnail"].ToString() != "")
                    {
                        dr["SubMaterialThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["SubMaterialThumbnail"].ToString();
                    }

                    if (dr["SignatureThumbnail"].ToString() != "")
                    {
                        dr["SignatureThumbnail"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["SignatureThumbnail"].ToString();
                    }
                }


                foreach (DataRow dr in ds.Tables["SRFDataImages"].Rows)
                {
                    if (dr["FileLocation"].ToString() != "")
                    {
                        dr["FileLocation"] = FrameworkSetting.Setting.MediaThumbnailUrl + dr["FileLocation"].ToString();
                    }
                }

                return Helper.CreateReportFileWithEPPlus2(ds, "ClientComplaintItemDetail");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    notification.DetailMessage.Add(ex.InnerException.Message);

                return string.Empty;
            }
        }

        #region private methods
        private static bool TryParseFilter<T>(IDictionary filters, string key, out T outValue)
        {
            try
            {

                if (filters[key] == null)
                    outValue = default(T);
                else
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    outValue = (T)conv.ConvertFrom(filters[key].ToString().Trim());
                }
                return true;
            }
            catch
            {
                outValue = default(T);
                return false;
            }
        }
        #endregion
    }
}
