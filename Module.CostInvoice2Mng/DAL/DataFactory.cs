using Library;
using Library.Base;
using Library.DTO;
using Module.CostInvoice2Mng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DAL
{
    internal class DataFactory : DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    CostInvoice2 dbItem = context.CostInvoice2.FirstOrDefault(s => s.CostInvoice2ID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data.";

                        return false;
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = userId;
                    dbItem.ConfirmedDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_CostInvoice2(context.CostInvoice2Mng_CostInvoice2_View.FirstOrDefault(o => o.CostInvoice2ID == id));
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }

                return false;
            }
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    CostInvoice2 dbItem = context.CostInvoice2.FirstOrDefault(s => s.CostInvoice2ID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find cost invoice.";

                        return false;
                    }

                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Cost invoice is confirmed. You don't delete it";

                        return false;
                    }

                    context.CostInvoice2.Remove(dbItem);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }

                return false;
            }
        }

        public override EditForm GetData(int id, out Notification notification)
        {
            EditForm editForm = new EditForm();
            editForm.CostInvoice2 = new DTO.CostInvoice2();
            editForm.CostInvoice2.CostInvoice2Clients = new List<DTO.CostInvoice2Client>();
            editForm.CostInvoice2.CostInvoice2Factories = new List<DTO.CostInvoice2Factory>();

            // Support data
            editForm.CostInvoice2Creditors = new List<DTO.CostInvoice2Creditor>();
            editForm.CostInvoice2Types = new List<DTO.CostInvoice2Type>();
            editForm.Currencies = new List<Support.DTO.Currency>();
            editForm.Seasons = new List<Support.DTO.Season>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        editForm.CostInvoice2 = converter.DB2DTO_CostInvoice2(context.CostInvoice2Mng_CostInvoice2_View.FirstOrDefault(o => o.CostInvoice2ID == id));
                    }

                    editForm.CostInvoice2Creditors = converter.DB2DTO_CostInvoice2Creditors(context.CostInvoice2Mng_CostInvoice2Creditor_View.ToList());
                    editForm.CostInvoice2Types = converter.DB2DTO_CostInvoice2Types(context.CostInvoice2Mng_CostInvoice2Type_View.ToList());
                }

                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                editForm.Currencies = supportFactory.GetCurrency();
                editForm.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return editForm;
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.CostInvoice2SearchResults = new List<CostInvoice2SearchResult>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            totalRows = 0;

            try
            {
                string season = (filters.ContainsKey("season") && filters["season"] != null && !string.IsNullOrEmpty(filters["season"].ToString().Replace("'", "''"))) ? filters["season"].ToString().Trim() : null;//filters["season"]?.ToString().Replace("'", "''");
                string creditorCode = (filters.ContainsKey("creditorCode") && filters["creditorCode"] != null && !string.IsNullOrEmpty(filters["creditorCode"].ToString().Replace("'", "''"))) ? filters["creditorCode"].ToString().Trim() : null;//filters["creditorCode"]?.ToString().Replace("'", "''");
                int? typeOfPayment = (filters.ContainsKey("costInvoice2TypeID") && filters["costInvoice2TypeID"] != null && !string.IsNullOrEmpty(filters["costInvoice2TypeID"].ToString().Replace("'", "''"))) ? Convert.ToInt32(filters["costInvoice2TypeID"]?.ToString().Replace("'", "''")) : (int?) null;//Convert.ToInt32(filters["costInvoice2TypeID"]?.ToString().Replace("'", "''"));
                string relatedDocumentNo = (filters.ContainsKey("relatedDocumentNo") && filters["relatedDocumentNo"] != null && !string.IsNullOrEmpty(filters["relatedDocumentNo"].ToString().Replace("'", "''"))) ? filters["relatedDocumentNo"].ToString().Trim() : null;//filters["relatedDocumentNo"]?.ToString().Replace("'", "''");
                string invoiceNo = (filters.ContainsKey("relatedDocumentNo") && filters["relatedDocumentNo"] != null && !string.IsNullOrEmpty(filters["relatedDocumentNo"].ToString().Replace("'", "''"))) ? filters["relatedDocumentNo"].ToString().Trim() : null;//filters["invoiceNo"]?.ToString().Replace("'", "''");
                DateTime? invoiceDate = (filters.ContainsKey("invoiceDate") && filters["invoiceDate"] != null && !string.IsNullOrEmpty(filters["invoiceDate"].ToString().Replace("'", "''"))) ? (filters["invoiceDate"]?.ToString().Replace("'", "''")).ConvertStringToDateTime() : null;//(filters["invoiceDate"]?.ToString().Replace("'", "''")).ConvertStringToDateTime();
                DateTime? dueDate = (filters.ContainsKey("dueDate") && filters["dueDate"] != null && !string.IsNullOrEmpty(filters["dueDate"].ToString().Replace("'", "''"))) ? (filters["dueDate"]?.ToString().Replace("'", "''")).ConvertStringToDateTime() : null;//(filters["dueDate"]?.ToString().Replace("'", "''")).ConvertStringToDateTime();
                DateTime? paidDate = (filters.ContainsKey("paidDate") && filters["paidDate"] != null && !string.IsNullOrEmpty(filters["paidDate"].ToString().Replace("'", "''"))) ? (filters["paidDate"]?.ToString().Replace("'", "''")).ConvertStringToDateTime() : null;//(filters["paidDate"]?.ToString().Replace("'", "''")).ConvertStringToDateTime();
                int? isPaid = (filters.ContainsKey("isPaid") && filters["isPaid"] != null && !string.IsNullOrEmpty(filters["isPaid"].ToString().Replace("'", "''"))) ? Convert.ToInt32(filters["isPaid"]?.ToString().Replace("'", "''")) : (int?)null; //Convert.ToInt32(filters["isPaid"]?.ToString().Replace("'", "''"));

                using (var context = CreateContext())
                {                  
                    //get total
                    searchForm.TotalAmountValue = context.CostInvoice2Mng_CostInvoice2SearchResult_View.Sum(o => (o.TotalAmount.HasValue ? o.TotalAmount.Value : 0));
                    //get sub total
                    var sumData = context.CostInvoice2Mng_function_SearchResultCostInvoice2(season, creditorCode, typeOfPayment, relatedDocumentNo, invoiceNo, invoiceDate, dueDate, paidDate, isPaid, orderBy, orderDirection).ToList();
                    //totalRows = sumData.Count();
                    searchForm.SubTotalAmountValue = sumData.Sum(o => (o.TotalAmount.HasValue ? o.TotalAmount.Value : 0));
                    //search
                    totalRows = context.CostInvoice2Mng_function_SearchResultCostInvoice2(season, creditorCode, typeOfPayment, relatedDocumentNo, invoiceNo, invoiceDate, dueDate, paidDate, isPaid, orderBy, orderDirection).Count();
                    searchForm.CostInvoice2SearchResults = converter.DB2DTO_CostInvoice2SearchResults(context.CostInvoice2Mng_function_SearchResultCostInvoice2(season, creditorCode, typeOfPayment, relatedDocumentNo, invoiceNo, invoiceDate, dueDate, paidDate, isPaid, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error; 
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return searchForm;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem1, out Notification notification)
        {
            DTO.CostInvoice2 dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoItem1).ToObject<DTO.CostInvoice2>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    CostInvoice2 dbItem;

                    if (id > 0)
                    {
                        dbItem = context.CostInvoice2.FirstOrDefault(s => s.CostInvoice2ID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not find data";

                            return false;
                        }
                    }
                    else
                    {
                        dbItem = new CostInvoice2();
                        dbItem.CostInvoice2UD = (context.CostInvoice2.Count() + 1).ToString().PadLeft(10, '0');

                        context.CostInvoice2.Add(dbItem);
                    }
                    //upload file
                    Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                    string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                    if (dtoItem.File_HasChange.HasValue && dtoItem.File_HasChange.Value)
                    {
                        dtoItem.RelatedDocumentFile = fwFactory.CreateFilePointer(tempFolder, dtoItem.File_NewFile, dtoItem.RelatedDocumentFile, dtoItem.FriendlyName);
                    }
                    //
                    converter.DTO2DB_CostInvoice2(dtoItem, ref dbItem);

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    dbItem.InvoiceDate = dtoItem.InvoiceDate.ConvertStringToDateTime();
                    dbItem.DueDate = dtoItem.DueDate.ConvertStringToDateTime();
                    dbItem.PaidDate = dtoItem.PaidDate.ConvertStringToDateTime();

                    context.CostInvoice2Client.Local.Where(o => o.CostInvoice2 == null).ToList().ForEach(o => context.CostInvoice2Client.Remove(o));
                    context.CostInvoice2Factory.Local.Where(o => o.CostInvoice2 == null).ToList().ForEach(o => context.CostInvoice2Factory.Remove(o));

                    context.SaveChanges();

                    dtoItem1 = GetData(dbItem.CostInvoice2ID, out notification);
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }

                return false;
            }
        }

        public InitForm GetInitData(out Notification notification)
        {
            InitForm initForm = new InitForm();
            initForm.Seasons = new List<Support.DTO.Season>();
            initForm.CostInvoice2Types = new List<DTO.CostInvoice2Type>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    initForm.CostInvoice2Types = converter.DB2DTO_CostInvoice2Types(context.CostInvoice2Mng_CostInvoice2Type_View.ToList());
                }

                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                initForm.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return initForm;
        }

        //========================================================================================================//

        public string ExportCostInvoice2(string season,/* string endDate,*/ out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            CostInvoice2MngObject ds = new CostInvoice2MngObject();
            string fileName = string.Empty;

            //DateTime? valStartDate = season.ConvertStringToDateTime();
            //DateTime? valEndDate = endDate.ConvertStringToDateTime();

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                // Call stored procedure by name.
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("CostInvoice2Mng_function_ReportWithSeason", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                // Add parameter value of stored procedure.
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                //adap.SelectCommand.Parameters.AddWithValue("@StartDate", valStartDate.Value.Date);
                //adap.SelectCommand.Parameters.AddWithValue("@EndDate", valEndDate.Value.Date);
                // Mapping all views get from stored procedure.
                adap.TableMappings.Add("Table", "CostInvoice");
                adap.TableMappings.Add("Table1", "CostInvoiceClient");
                adap.TableMappings.Add("Table2", "CostInvoiceFactory");
                adap.Fill(ds);

                //ReportObjectData.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                //hRow.StartDate = season;
                //hRow.EndDate = endDate;
                //ds.ReportHeader.AddReportHeaderRow(hRow);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "CostInvoice2Rpt");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return fileName;
        }



        //========================================================================================================//

        private CostInvoice2MngEntities CreateContext()
        {
            return new CostInvoice2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.CostInvoice2MngModel"));
        }
    }
}
