using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LedgerAccountMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private LedgerAccountMngEntities CreateContext()
        {
            return new LedgerAccountMngEntities(Library.Helper.CreateEntityConnectionString("DAL.LedgerAccountMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.LedgerAccountSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (LedgerAccountMngEntities context = CreateContext())
                {
                    string accountNo = null;
                    string vietnameseNM = null;
                    string englishNM = null;

                    if (filters.ContainsKey("AccountNo") && !string.IsNullOrEmpty(filters["AccountNo"].ToString()))
                    {
                        accountNo = filters["AccountNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("VietnameseNM") && !string.IsNullOrEmpty(filters["VietnameseNM"].ToString()))
                    {
                        vietnameseNM = filters["VietnameseNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("EnglishNM") && !string.IsNullOrEmpty(filters["EnglishNM"].ToString()))
                    {
                        englishNM = filters["EnglishNM"].ToString().Replace("'", "''");
                    }

                    var dataTest = context.LedgerAccountMng_function_SearchLedgerAccount(accountNo, vietnameseNM, englishNM, orderBy, orderDirection);
                    totalRows = dataTest.Count();
                    var result = context.LedgerAccountMng_function_SearchLedgerAccount(accountNo, vietnameseNM, englishNM, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_LedgerAccountSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.LedgerAccount();
            data.Data.LedgerAccountDetailOverviews = new List<DTO.LedgerAccountDetailOverview>();
            data.LedgerAccounts = new List<Module.Support.DTO.LedgerAccount>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (LedgerAccountMngEntities context = CreateContext())
                    {
                        var profile = context.LedgerAccountMng_LedgerAccount_View.FirstOrDefault(o => o.LedgerAccountID == id);
                        if (profile == null)
                        {
                            throw new Exception("Can not found the account to edit");
                        }
                        data.Data = converter.DB2DTO_LedgerAccount(profile);
                    }
                }
                data.LedgerAccounts = supportFactory.GetLedgerAccount().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LedgerAccountMngEntities context = CreateContext())
                {
                    LedgerAccount dbItem = context.LedgerAccount.FirstOrDefault(o => o.LedgerAccountID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        context.LedgerAccount.Remove(dbItem);
                        context.SaveChanges();

                        return true;
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
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.LedgerAccount dtoLedgerAccount = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LedgerAccount>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LedgerAccountMngEntities context = CreateContext())
                {
                    LedgerAccount dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new LedgerAccount();
                        context.LedgerAccount.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.LedgerAccount.FirstOrDefault(o => o.LedgerAccountID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoLedgerAccount.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoLedgerAccount, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.LedgerAccountID, out notification).Data;

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

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LedgerAccountMngEntities context = CreateContext())
                {
                    LedgerAccount dbItem = context.LedgerAccount.FirstOrDefault(o => o.LedgerAccountID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Account not found !");
                    }
                    LedgerAccountMng_LedgerAccountSearchResult_View currentData = context.LedgerAccountMng_LedgerAccountSearchResult_View.FirstOrDefault(o => o.LedgerAccountID == id);
                    //LedgerAccount newData = new LedgerAccount();
                    dbItem.OpeningCredit = currentData.ClosingCredit;
                    dbItem.OpeningDebit = currentData.ClosingDebit;
                    //context.LedgerAccount.Add(newData);

                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.UpdatedBy = userId;
                    context.SaveChanges();
                    dtoItem = GetData(dbItem.LedgerAccountID, out notification).Data;
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

        public bool CloseEntry(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            //DTO.LedgerAccount dtoLedgerAccount = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LedgerAccount>();
            List<DTO.LedgerAccount> dtoLedgerAccount = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.LedgerAccount>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                foreach (DTO.LedgerAccount dtoAccount in dtoLedgerAccount)
                {
                    using (LedgerAccountMngEntities context = CreateContext())
                    {
                        LedgerAccount dbItem = context.LedgerAccount.FirstOrDefault(o => o.LedgerAccountID == dtoAccount.LedgerAccountID);
                        if (dbItem == null)
                        {
                            throw new Exception("Account not found !");
                        }
                        if (dbItem.AccountNo != "911")
                        {
                            LedgerAccountMng_LedgerAccountSearchResult_View currentData = context.LedgerAccountMng_LedgerAccountSearchResult_View.FirstOrDefault(o => o.LedgerAccountID == dtoAccount.LedgerAccountID);
                            dbItem.OpeningCredit = currentData.ClosingCredit;
                            dbItem.OpeningDebit = currentData.ClosingDebit;

                            dbItem.UpdatedDate = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public string GetExcelReport(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LedgerAccountMng_function_GetReportAccountData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.Fill(ds.LedgerAccountMng_function_GetReportAccountData);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "LedgerAccountOverview");
                //generate xml file
                return Library.Helper.CreateReportFiles(ds, "LedgerAccountOverview");
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
        public IEnumerable<DTO.LedgerAccountDetailOverview> GetDetailOverview(int userId, string accountNo, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LedgerAccountMngEntities context = CreateContext())
                {
                    var data = context.LedgerAccountMng_LedgerAccountDetailOverview_View.Where(o => o.CreditAccountNo == accountNo || o.DebitAccountNo == accountNo);
                    var result = converter.DB2DTO_LedgerAccountDetailOverview(data.ToList());
                    return result;
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
                return new List<DTO.LedgerAccountDetailOverview>();
            }
        }
    }
}
