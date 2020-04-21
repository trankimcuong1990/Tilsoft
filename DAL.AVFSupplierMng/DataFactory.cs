using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AVFSupplierMng
{
    //public class DataFactory : DALBase.FactoryBase2<DTO.AVFSupplierMng.SearchFormData, DTO.AVFSupplierMng.EditFormData, DTO.AVFSupplierMng.AVFSupplier>
    //{
    //    private DataConverter converter = new DataConverter();
    //    private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

    //    private AVFSupplierMngEntities CreateContext()
    //    {
    //        return new AVFSupplierMngEntities(DALBase.Helper.CreateEntityConnectionString("AVFSupplierMngModel"));
    //    }

    //    public override DTO.AVFSupplierMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
    //    {
    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
    //        DTO.AVFSupplierMng.SearchFormData data = new DTO.AVFSupplierMng.SearchFormData();
    //        data.Data = new List<DTO.AVFSupplierMng.SupplierSearchResult>();

    //        totalRows = 0;

    //        string AVFSupplierUD = null;
    //        string AVFSupplierNM = null;
    //        if (filters.ContainsKey("AVFSupplierUD") && !string.IsNullOrEmpty(filters["AVFSupplierUD"].ToString()))
    //        {
    //            AVFSupplierUD = filters["AVFSupplierUD"].ToString().Replace("'", "''");
    //        }
    //        if (filters.ContainsKey("AVFSupplierNM") && !string.IsNullOrEmpty(filters["AVFSupplierNM"].ToString()))
    //        {
    //            AVFSupplierNM = filters["AVFSupplierNM"].ToString().Replace("'", "''");
    //        }
    //        //try to get data
    //        try
    //        {
    //            using (AVFSupplierMngEntities context = CreateContext())
    //            {
    //                totalRows = context.AVFSupplierMng_function_SearchSupplier(AVFSupplierUD, AVFSupplierNM, orderBy, orderDirection).Count();
    //                var result = context.AVFSupplierMng_function_SearchSupplier(AVFSupplierUD, AVFSupplierNM, orderBy, orderDirection);
    //                data.Data = converter.DB2DTO_SupplierSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            notification.Type = Library.DTO.NotificationType.Error;
    //            notification.Message = ex.Message;
    //        }

    //        return data;
    //    }

    //    public override DTO.AVFSupplierMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
    //    {
    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
    //        DTO.AVFSupplierMng.EditFormData data = new DTO.AVFSupplierMng.EditFormData();
    //        data.Data = new DTO.AVFSupplierMng.AVFSupplier();
    //        data.LedgerAccounts = new List<DTO.Support.LedgerAccount>();

    //        //try to get data
    //        try
    //        {
    //            using (AVFSupplierMngEntities context = CreateContext())
    //            {
    //                if (id > 0)
    //                {
    //                    data.Data = converter.DB2DTO_AVFSupplier(context.AVFSupplierMng_Supplier_View
    //                        .Include("AVFSupplierMng_SupplierPurchasingInvoice_View.AVFSupplierMng_SupplierPurchasingInvoiceDetail_View")
    //                        .FirstOrDefault(o => o.AVFSupplierID == id));
    //                    data.LedgerAccounts = supportFactory.GetLedgerAccount().ToList();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            notification.Type = Library.DTO.NotificationType.Error;
    //            notification.Message = ex.Message;
    //        }

    //        return data;
    //    }

    //    public override bool DeleteData(int id, out Library.DTO.Notification notification)
    //    {
    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
    //        try
    //        {
    //            using (AVFSupplierMngEntities context = CreateContext())
    //            {
    //                AVFSupplier dbItem = context.AVFSupplier.FirstOrDefault(o => o.AVFSupplierID == id);
    //                if (dbItem == null)
    //                {
    //                    notification.Message = "Supplier not found!";
    //                    return false;
    //                }
    //                else
    //                {
    //                    context.AVFSupplier.Remove(dbItem);
    //                    context.SaveChanges();

    //                    return true;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            notification.Type = Library.DTO.NotificationType.Error;
    //            notification.Message = ex.Message;
    //            return false;
    //        }
    //    }

    //    public override bool UpdateData(int id, ref DTO.AVFSupplierMng.AVFSupplier dtoItem, out Library.DTO.Notification notification)
    //    {
    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
    //        try
    //        {
    //            using (AVFSupplierMngEntities context = CreateContext())
    //            {
    //                AVFSupplier dbItem = null;
    //                if (id == 0)
    //                {
    //                    dbItem = new AVFSupplier();
    //                    context.AVFSupplier.Add(dbItem);
    //                }
    //                else
    //                {
    //                    dbItem = context.AVFSupplier.FirstOrDefault(o => o.AVFSupplierID == id);
    //                }

    //                if (dbItem == null)
    //                {
    //                    notification.Message = "Supplier not found!";
    //                    return false;
    //                }
    //                else
    //                {
    //                    // check concurrency
    //                    if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
    //                    {
    //                        throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
    //                    }

    //                    converter.DTO2BD(dtoItem, ref dbItem);
    //                    context.SaveChanges();

    //                    dtoItem = GetData(dbItem.AVFSupplierID, out notification).Data;

    //                    return true;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            notification.Type = Library.DTO.NotificationType.Error;
    //            notification.Message = ex.Message;
    //            return false;
    //        }
    //    }

    //    public override bool Approve(int id, ref DTO.AVFSupplierMng.AVFSupplier dtoItem, out Library.DTO.Notification notification)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool Reset(int id, ref DTO.AVFSupplierMng.AVFSupplier dtoItem, out Library.DTO.Notification notification)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    public bool CloseEntry(int userId, object dtoItem, out Library.DTO.Notification notification)
    //    {
    //        //DTO.LedgerAccount dtoLedgerAccount = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LedgerAccount>();
    //        List<DTO.AVFSupplierMng.AVFSupplier> dtoSupplier = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.AVFSupplierMng.AVFSupplier>>();
    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

    //        try
    //        {
    //            foreach (DTO.LedgerAccount dtoAccount in dtoLedgerAccount)
    //            {
    //                using (LedgerAccountMngEntities context = CreateContext())
    //                {
    //                    LedgerAccount dbItem = context.LedgerAccount.FirstOrDefault(o => o.LedgerAccountID == dtoAccount.LedgerAccountID);
    //                    if (dbItem == null)
    //                    {
    //                        throw new Exception("Account not found !");
    //                    }
    //                    if (dbItem.AccountNo != "911")
    //                    {
    //                        LedgerAccountMng_LedgerAccountSearchResult_View currentData = context.LedgerAccountMng_LedgerAccountSearchResult_View.FirstOrDefault(o => o.LedgerAccountID == dtoAccount.LedgerAccountID);
    //                        dbItem.OpeningCredit = currentData.ClosingCredit;
    //                        dbItem.OpeningDebit = currentData.ClosingDebit;

    //                        dbItem.UpdatedDate = DateTime.Now;
    //                        context.SaveChanges();
    //                    }
    //                }
    //            }
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            notification.Type = Library.DTO.NotificationType.Error;
    //            notification.Message = ex.Message;
    //            return false;
    //        }
    //    }

    //    public string GetExcelReport(out Library.DTO.Notification notification)
    //    {
    //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
    //        ReportDataObject ds = new ReportDataObject();

    //        try
    //        {
    //            SqlDataAdapter adap = new SqlDataAdapter();
    //            adap.SelectCommand = new SqlCommand("LedgerAccountMng_function_GetReportAccountData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
    //            adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
    //            adap.Fill(ds.LedgerAccountMng_function_GetReportAccountData);

    //            // dev
    //            //Library.Helper.DevCreateReportXMLSource(ds, "LedgerAccountOverview");
    //            //generate xml file
    //            return Library.Helper.CreateReportFiles(ds, "LedgerAccountOverview");
    //        }
    //        catch (Exception ex)
    //        {
    //            notification.Type = Library.DTO.NotificationType.Error;
    //            notification.Message = ex.Message;
    //            if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
    //            {
    //                notification.DetailMessage.Add(ex.InnerException.Message);
    //            }
    //            return string.Empty;
    //        }
    //    }
    //}
}
