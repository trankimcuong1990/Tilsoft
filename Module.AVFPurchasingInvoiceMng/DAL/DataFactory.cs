using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.AVFPurchasingInvoiceMng.DAL
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
        private AVFPurchasingInvoiceMngEntities CreateContext()
        {
            return new AVFPurchasingInvoiceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AVFPurchasingInvoiceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.AVFPurchasingInvoiceSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (AVFPurchasingInvoiceMngEntities context = CreateContext())
                {
                    string AVFSupplierNM = null;
                    string InvoiceNo = null;

                    if (filters.ContainsKey("AVFSupplierNM") && !string.IsNullOrEmpty(filters["AVFSupplierNM"].ToString()))
                    {
                        AVFSupplierNM = filters["AVFSupplierNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("InvoiceNo") && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
                    {
                        InvoiceNo = filters["InvoiceNo"].ToString().Replace("'", "''");
                    }

                    var dataTest = context.AVFPurchasingInvoiceMng_function_SearchAVFPurchasingInvoice(AVFSupplierNM, InvoiceNo, orderBy, orderDirection);
                    totalRows = dataTest.Count();
                    var result = context.AVFPurchasingInvoiceMng_function_SearchAVFPurchasingInvoice(AVFSupplierNM, InvoiceNo, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_AVFPurchasingInvoiceSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.AVFPurchasingInvoice();
            data.Data.AVFPurchasingInvoiceDetails = new List<DTO.AVFPurchasingInvoiceDetail>();
            data.LedgerAccounts = new List<Module.Support.DTO.LedgerAccount>();
            data.Seasons = new List<Module.Support.DTO.Season>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (AVFPurchasingInvoiceMngEntities context = CreateContext())
                    {
                        var invoice = context.AVFPurchasingInvoiceMng_AVFPurchasingInvoice_View.FirstOrDefault(o => o.AVFPurchasingInvoiceID == id);
                        if (invoice == null)
                        {
                            throw new Exception("Can not found the invoice to edit");
                        }
                        data.Data = converter.DB2DTO_AVFPurchasingInvoice(invoice);
                    }
                }
                data.LedgerAccounts = supportFactory.GetLedgerAccount().ToList();
                data.Seasons = supportFactory.GetSeason().ToList();
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
                using (AVFPurchasingInvoiceMngEntities context = CreateContext())
                {
                    AVFPurchasingInvoice dbItem = context.AVFPurchasingInvoice.FirstOrDefault(o => o.AVFPurchasingInvoiceID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        foreach (AVFPurchasingInvoiceDetail detail in dbItem.AVFPurchasingInvoiceDetail.ToArray())
                        {
                            context.AVFPurchasingInvoiceDetail.Remove(detail);
                        }

                        context.AVFPurchasingInvoice.Remove(dbItem);
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
            DTO.AVFPurchasingInvoice dtoAVFPurchasingInvoice = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AVFPurchasingInvoice>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVFPurchasingInvoiceMngEntities context = CreateContext())
                {
                    AVFPurchasingInvoice dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new AVFPurchasingInvoice();
                        context.AVFPurchasingInvoice.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.AVFPurchasingInvoice.FirstOrDefault(o => o.AVFPurchasingInvoiceID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoAVFPurchasingInvoice.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //convert dto to db
                        converter.DTO2BD(dtoAVFPurchasingInvoice, ref dbItem);
                        //remove orphan item
                        context.AVFPurchasingInvoiceDetail.Local.Where(o => o.AVFPurchasingInvoice == null).ToList().ForEach(o => context.AVFPurchasingInvoiceDetail.Remove(o));

                        // processing file
                        if (dtoAVFPurchasingInvoice.PDFFileScan_HasChange)
                        {
                            dbItem.PDFFileScan = fwFactory.CreateNoneImageFilePointer(this._tempFolder, dtoAVFPurchasingInvoice.PDFFileScan_NewFile, dtoAVFPurchasingInvoice.PDFFileScan);
                        }

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();
                        dtoItem = GetData(dbItem.AVFPurchasingInvoiceID, out notification).Data;

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
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData result = new DTO.SearchFilterData();
            result.LedgerAccounts = new List<Support.DTO.LedgerAccount>();

            try
            {
                result.LedgerAccounts = supportFactory.GetLedgerAccount();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }
        public string GetExcelReportData(string From, string To, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            DateTime fromDt = DateTime.ParseExact(From, "d/M/yyyy", CultureInfo.InvariantCulture);
            string fromDate = fromDt.ToString("MM/dd/yyyy");

            DateTime toDt = DateTime.ParseExact(To, "d/M/yyyy", CultureInfo.InvariantCulture);
            string toDate = toDt.ToString("MM/dd/yyyy");

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("AVFPurchasingInvoiceMng_function_getExcelOverview", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@From", fromDate);
                adap.SelectCommand.Parameters.AddWithValue("@To", toDate);
                adap.Fill(ds);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.From = From;
                pRow.To = To;
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "AVFPurchasingInvoiceRptOverview");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "AVFPurchasingInvoiceRptOverview");
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
