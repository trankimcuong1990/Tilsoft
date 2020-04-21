using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace Module.FactoryStockListReport.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.SearchFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryStockListReportEntities CreateContext()
        {
            return new FactoryStockListReportEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryStockListReportModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

        }

        public override DTO.SearchFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public string GetFactoryStockList(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                //pare object to list string of factoryid
                List<string> factoryIDs = ((Newtonsoft.Json.Linq.JArray)filters["factoryIDs"]).ToObject<List<string>>();
                string factoryId = "";
                foreach (var item in factoryIDs)
                {
                    factoryId += item + ",";
                }

                bool isIncludeImage = Convert.ToBoolean(filters["isIncludeImage"]);
                //
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_function_GetFactoryStockList", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryIDs", factoryId);
                adap.TableMappings.Add("Table", "ReportMng_FactoryStockList_View");
                adap.TableMappings.Add("Table1", "ParamTable");
                adap.Fill(ds);

                foreach (var item in ds.ReportMng_FactoryStockList_View)
                {
                    //asign image
                    if (isIncludeImage && System.IO.File.Exists(FrameworkSetting.Setting.AbsoluteThumbnailFolder + item.ThumbnailLocation))
                    {
                        item.ThumbnailLocation = FrameworkSetting.Setting.ThumbnailUrl + item.ThumbnailLocation;
                    }
                    else
                    {
                        item.ThumbnailLocation = "NONE";
                    }
                }
                //add param value
                DAL.ReportDataObject.ParamTableRow pRow = ds.ParamTable.NewParamTableRow();
                pRow.IsIncludeImage = isIncludeImage;
                pRow.FactoryUD = "";
                ds.ParamTable.AddParamTableRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "FactoryStockList");
                //return string.Empty;

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "FactoryStockList");
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

        public  DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string factoryUD = string.Empty;
            string clientUD = string.Empty;
            string proformaInvoiceNo = string.Empty;
            string lds = string.Empty;
            string articleCode = string.Empty;
            string description = string.Empty;
            int? factoryID = null;

            if (filters.ContainsKey("factoryUD")) factoryUD = filters["factoryUD"].ToString();
            if (filters.ContainsKey("clientUD")) clientUD = filters["clientUD"].ToString();
            if (filters.ContainsKey("proformaInvoiceNo")) proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
            if (filters.ContainsKey("lds")) lds = filters["lds"].ToString();
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();
            if (filters.ContainsKey("factoryID") && filters["factoryID"] != null) factoryID = Convert.ToInt32(filters["factoryID"]);

            //if (filters.ContainsKey("factoryIDs") && filters["factoryIDs"] != null)
            //{
            //    //pare object to list string of factoryid
            //    List<string> fID = ((Newtonsoft.Json.Linq.JArray)filters["factoryIDs"]).ToObject<List<string>>();
            //    foreach (var item in fID)
            //    {
            //        factoryIDs += item + ",";
            //    }
            //}
            Module.Support.DAL.DataFactory sFactory = new Support.DAL.DataFactory();
            searchFormData.Factories = sFactory.GetFactory(userId);
            try
            {
                using (FactoryStockListReportEntities context = CreateContext())
                {
                    List<int?> listFactoryID = context.UserFactoryAccess.Where(o => o.UserID == userId && o.CanAccess.HasValue && o.CanAccess.Value).Select(s => s.FactoryID).ToList();
                    //get total
                    searchFormData.TotalStockQnt = context.FactoryStockListReportMng_FactoryStockList_View.Where(x =>listFactoryID.Contains(x.FactoryID)).Sum(o => (o.TotalStockQnt.HasValue ? o.TotalStockQnt.Value : 0));
                    searchFormData.TotalStockQntIn40HC = context.FactoryStockListReportMng_FactoryStockList_View.Where(x => listFactoryID.Contains(x.FactoryID)).Sum(o => (o.TotalStockQntIn40HC.HasValue ? o.TotalStockQntIn40HC.Value : 0));
                    searchFormData.TotalProducedQnt = context.FactoryStockListReportMng_FactoryStockList_View.Where(x => listFactoryID.Contains(x.FactoryID)).Sum(o => (o.TotalProducedQnt.HasValue ? o.TotalProducedQnt.Value : 0));
                    searchFormData.TotalNotPackedQnt = context.FactoryStockListReportMng_FactoryStockList_View.Where(x => listFactoryID.Contains(x.FactoryID)).Sum(o => (o.TotalNotPackedQnt.HasValue ? o.TotalNotPackedQnt.Value : 0));
                    searchFormData.TotalPackedQnt = context.FactoryStockListReportMng_FactoryStockList_View.Where(x => listFactoryID.Contains(x.FactoryID)).Sum(o => (o.TotalPackedQnt.HasValue ? o.TotalPackedQnt.Value : 0));

                    //search                    
                    var result = context.FactoryStockListReportMng_function_GetFactoryStockList(orderBy, orderDirection, factoryUD, clientUD, proformaInvoiceNo, lds, articleCode, description, factoryID, userId);
                    searchFormData.Data = converter.DB2DTO_FactoryStockList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //get sub total
                    var sumData = context.FactoryStockListReportMng_function_GetFactoryStockList(orderBy, orderDirection, factoryUD, clientUD, proformaInvoiceNo, lds, articleCode, description, factoryID, userId).ToList();
                    totalRows = sumData.Count();
                    searchFormData.SubTotalStockQnt = sumData.Sum(o => (o.TotalStockQnt.HasValue ? o.TotalStockQnt.Value : 0));
                    searchFormData.SubTotalStockQntIn40HC = sumData.Sum(o => (o.TotalStockQntIn40HC.HasValue ? o.TotalStockQntIn40HC.Value : 0));
                    searchFormData.SubTotalProducedQnt = sumData.Sum(o => (o.TotalProducedQnt.HasValue ? o.TotalProducedQnt.Value : 0));
                    searchFormData.SubTotalNotPackedQnt = sumData.Sum(o => (o.TotalNotPackedQnt.HasValue ? o.TotalNotPackedQnt.Value : 0));
                    searchFormData.SubTotalPackedQnt = sumData.Sum(o => (o.TotalPackedQnt.HasValue ? o.TotalPackedQnt.Value : 0));
                   
                }
                return searchFormData;
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
                return searchFormData;
            }
        }
    }
}
