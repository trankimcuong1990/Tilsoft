using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFSupplierMng.DAL
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
        private AVFSupplierMngEntities CreateContext()
        {
            return new AVFSupplierMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AVFSupplierMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.AVFSupplierSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (AVFSupplierMngEntities context = CreateContext())
                {
                    string AVFSupplierUD = null;
                    string AVFSupplierNM = null;
                    if (filters.ContainsKey("AVFSupplierUD") && !string.IsNullOrEmpty(filters["AVFSupplierUD"].ToString()))
                    {
                        AVFSupplierUD = filters["AVFSupplierUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("AVFSupplierNM") && !string.IsNullOrEmpty(filters["AVFSupplierNM"].ToString()))
                    {
                        AVFSupplierNM = filters["AVFSupplierNM"].ToString().Replace("'", "''");
                    }

                    totalRows = context.AVFSupplierMng_function_SearchSupplier(AVFSupplierUD, AVFSupplierNM, orderBy, orderDirection).Count();
                    var result = context.AVFSupplierMng_function_SearchSupplier(AVFSupplierUD, AVFSupplierNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_AVFSupplierSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.AVFSupplier();
            //data.Data.AVFSupplierDetailOverviews = new List<DTO.AVFSupplierDetailOverview>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (AVFSupplierMngEntities context = CreateContext())
                    {
                        var supplier = context.AVFSupplierMng_Supplier_View.FirstOrDefault(o => o.AVFSupplierID == id);
                        if (supplier == null)
                        {
                            throw new Exception("Can not found the supplier to edit");
                        }
                        data.Data = converter.DB2DTO_AVFSupplier(supplier);
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVFSupplierMngEntities context = CreateContext())
                {
                    AVFSupplier dbItem = context.AVFSupplier.FirstOrDefault(o => o.AVFSupplierID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        context.AVFSupplier.Remove(dbItem);
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
            DTO.AVFSupplier dtoAVFSupplier = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AVFSupplier>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVFSupplierMngEntities context = CreateContext())
                {
                    AVFSupplier dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new AVFSupplier();
                        context.AVFSupplier.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.AVFSupplier.FirstOrDefault(o => o.AVFSupplierID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoAVFSupplier.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoAVFSupplier, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.AVFSupplierID, out notification).Data;

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
                using (AVFSupplierMngEntities context = CreateContext())
                {
                    AVFSupplier dbItem = context.AVFSupplier.FirstOrDefault(o => o.AVFSupplierID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Supplier not found !");
                    }
                    AVFSupplierMng_SupplierSearchResult_View currentData = context.AVFSupplierMng_SupplierSearchResult_View.FirstOrDefault(o => o.AVFSupplierID == id);
                    //LedgerAccount newData = new LedgerAccount();
                    dbItem.OpeningCredit = currentData.ClosingCredit;
                    dbItem.OpeningDebit = currentData.ClosingDebit;
                    //context.LedgerAccount.Add(newData);

                    dbItem.UpdatedDate = DateTime.Now;
                    dbItem.UpdatedBy = userId;
                    context.SaveChanges();
                    dtoItem = GetData(dbItem.AVFSupplierID, out notification).Data;
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
            //DTO.AVFSupplier dtoAVFSupplier = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.AVFSupplier>();
            List<DTO.AVFSupplier> dtoAVFSupplier = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.AVFSupplier>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                foreach (DTO.AVFSupplier dtoSupplier in dtoAVFSupplier)
                {
                    using (AVFSupplierMngEntities context = CreateContext())
                    {
                        AVFSupplier dbItem = context.AVFSupplier.FirstOrDefault(o => o.AVFSupplierID == dtoSupplier.AVFSupplierID);
                        if (dbItem == null)
                        {
                            throw new Exception("Supplier not found !");
                        }
                        AVFSupplierMng_SupplierSearchResult_View currentData = context.AVFSupplierMng_SupplierSearchResult_View.FirstOrDefault(o => o.AVFSupplierID == dtoSupplier.AVFSupplierID);
                        dbItem.OpeningCredit = currentData.ClosingCredit;
                        dbItem.OpeningDebit = currentData.ClosingDebit;

                        dbItem.UpdatedDate = DateTime.Now;
                        context.SaveChanges();
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
                adap.SelectCommand = new SqlCommand("AVFSupplierMng_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.Fill(ds.AVFSupplierMng_function_GetReportData);

                // dev
                Library.Helper.DevCreateReportXMLSource(ds, "AVFSupplierOverview");
                //generate xml file
                return Library.Helper.CreateReportFiles(ds, "AVFSupplierOverview");
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
        //public IEnumerable<DTO.AVFSupplierDetailOverview> GetDetailOverview(int userId, string supplierID, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    try
        //    {
        //        using (AVFSupplierMngEntities context = CreateContext())
        //        {
        //            var data = context.AVFSupplierMng_AVFSupplierDetailOverview_View.Where(o => o.CreditAccountNo == supplierID);
        //            var result = converter.DB2DTO_AVFSupplierDetailOverview(data.ToList());
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
        //        {
        //            notification.DetailMessage.Add(ex.InnerException.Message);
        //        }
        //        return new List<DTO.AVFSupplierDetailOverview>();
        //    }
        //}
    }
}
