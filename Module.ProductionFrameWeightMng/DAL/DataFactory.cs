using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.ProductionFrameWeightMng.DAL
{
    internal class DataFactory
    {
        private readonly DataConverter converter = new DataConverter();

        public ProductionFrameWeightEntities CreateContext()
        {
            return new ProductionFrameWeightEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionFrameWeightModel"));
        }

        public DTO.ProductionFrameWeightData GetData(int userID, int id, out Notification notification)
        {
            DTO.ProductionFrameWeightData data = new DTO.ProductionFrameWeightData();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ProductionFrameWeightMng_ProductionFrameWeight_View.FirstOrDefault(o => o.ProductionFrameWeightID == id);
                    data = converter.DB2DTO_ProductionFrameWeight(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ProductionFrameWeightData UpdateData(int userID, int id, Hashtable filters, out Notification notification)
        {
            DTO.ProductionFrameWeightData dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.ProductionFrameWeightData>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    ProductionFrameWeight dbItem;

                    if (id <= 0)
                    {
                        dbItem = new ProductionFrameWeight();
                        context.ProductionFrameWeight.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionFrameWeight.FirstOrDefault(o => o.ProductionFrameWeightID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can't found data of ProductionFrameWeight!";
                    }
                    else
                    {
                        converter.DTO2DB_ProductionFrameWeight(userID, dtoItem, ref dbItem);

                        context.SaveChanges();

                        dtoItem = GetData(userID, dbItem.ProductionFrameWeightID, out notification);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }

        public bool DeleteData(int userID, int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    ProductionFrameWeight dbItem = context.ProductionFrameWeight.FirstOrDefault(o => o.ProductionFrameWeightID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can't found data of ProductionFrameWeight!";

                        return false;
                    }
                    else
                    {
                        context.ProductionFrameWeight.Remove(dbItem);

                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public List<DTO.ProductionFrameWeightSearchResultData> GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification();
            notification.Type = NotificationType.Success;
            List<DTO.ProductionFrameWeightSearchResultData> data = new List<DTO.ProductionFrameWeightSearchResultData>();

            try
            {
                string clientUD = filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString()) ? filters["ClientUD"].ToString() : null;
                string proformaInvoice = filters.ContainsKey("ProformaInvoiceNo") && filters["ProformaInvoiceNo"] != null && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()) ? filters["ProformaInvoiceNo"].ToString() : null;
                string workOrderUD = filters.ContainsKey("WorkOrderUD") && filters["WorkOrderUD"] != null && !string.IsNullOrEmpty(filters["WorkOrderUD"].ToString()) ? filters["WorkOrderUD"].ToString() : null;
                string productionItem = filters.ContainsKey("ProductionItem") && filters["ProductionItem"] != null && !string.IsNullOrEmpty(filters["ProductionItem"].ToString()) ? filters["ProductionItem"].ToString() : null;

                using (var context = CreateContext())
                {
                    var listMaterial = context.MaterialType.ToList();

                    totalRows = context.ProductionFrameWeightMng_function_ProductionFrameWeightSearchResult(clientUD, proformaInvoice, workOrderUD, productionItem, orderBy, orderDirection).Count();
                    var dbItem = context.ProductionFrameWeightMng_function_ProductionFrameWeightSearchResult(clientUD, proformaInvoice, workOrderUD, productionItem, orderBy, orderDirection);
                    data = converter.DB2DTO_ProductionFrameWeightSearchResult(dbItem.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //if(data != null)
                    //{
                    //    foreach(var item in data)
                    //    {
                    //        if (!string.IsNullOrEmpty(item.ProductionItemUD))
                    //        {
                    //            var materialtype = "";
                    //            if (item.ProductionItemUD.LastIndexOf('-') > 0 && item.ProductionItemUD.Substring(item.ProductionItemUD.LastIndexOf('-') + 1) == "WEAVING")
                    //            {
                    //                materialtype = item.ProductionItemUD.Substring(0, item.ProductionItemUD.LastIndexOf('-'));
                    //            }
                    //            if (materialtype.LastIndexOf('-') > 0)
                    //            {
                    //                materialtype = materialtype.Substring(materialtype.LastIndexOf('-') + 1);
                    //            }
                    //            if (materialtype.Length >= 12)
                    //            {
                    //                materialtype = materialtype.Substring(10, 2);
                    //                foreach (var type in listMaterial)
                    //                {
                    //                    if (materialtype == type.MaterialTypeUD)
                    //                    {
                    //                        item.MaterialType = type.MaterialTypeNM;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        #region Export Excel
        public string GetExcelReportData(string workOrderUD, string clientUD, string proformaInvoiceNo, string productionItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ProductionFrameWeightMng_function_ExportExcel", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", clientUD);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", proformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderUD", workOrderUD);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItem", productionItem);

                adap.TableMappings.Add("Table", "FrameWeightExcel");
                adap.Fill(ds);

                // generate xml file
              
                return Library.Helper.CreateReportFileWithEPPlus2(ds,"FrameWeight");
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
        #endregion
    }
}
