using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;

namespace Module.QCReportMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private string frontendURL = "http://app.tilsoft.bg/";
        private QCReportEntities CreateContext()
        {
            return new QCReportEntities(Library.Helper.CreateEntityConnectionString("DAL.QCReportModel"));
        }
        public DTO.SearchForm GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchForm searchForm = new DTO.SearchForm();
            searchForm.Factories = new List<Support.DTO.Factory>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string qcReportUD = null;
                int? factoryID = null;
                string clientUD = null;
                string articleCode = null;
                string proformaInvoiceNo = null;
                string updatedDate = null;

                if (filters.ContainsKey("QCReportUD") && !string.IsNullOrEmpty(filters["QCReportUD"].ToString()))
                {
                    qcReportUD = filters["QCReportUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                {
                    factoryID = Convert.ToInt32(filters["FactoryID"]);
                }

                if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                {
                    articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                {
                    proformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("UpdatedDate") && !string.IsNullOrEmpty(filters["UpdatedDate"].ToString()))
                {
                    updatedDate = filters["UpdatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    clientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }

                //if (filters.ContainsKey("TestDate") && !string.IsNullOrEmpty(filters["TestDate"].ToString()))
                //{
                //    testDate = filters["TestDate"].ToString().Replace("'", "''");
                //}
                DateTime? valTestDate = updatedDate.ConvertStringToDateTime();

                using (QCReportEntities context = CreateContext())
                {
                    totalRows = context.QCReportMng_function_SearchQCReportMng(qcReportUD, factoryID, clientUD, articleCode, proformaInvoiceNo, valTestDate, orderBy, orderDirection).Count();
                    var result = context.QCReportMng_function_SearchQCReportMng(qcReportUD, factoryID, clientUD, articleCode, proformaInvoiceNo, valTestDate, orderBy, orderDirection);
                    searchForm.Data = converter.DB2DTO_SearchQCReport(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                searchForm.Factories = supportFactory.GetFactory().ToList();
                return searchForm;
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
                return searchForm;
            }
        }
        public DTO.InitDataDTO GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitDataDTO data = new DTO.InitDataDTO();
            data.Factories = new List<Support.DTO.Factory>();
            try
            {
                data.Factories = supportFactory.GetFactory().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public List<DTO.QCReportSectionDTO> GetInspection(string param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.QCReportSectionDTO> data = new List<DTO.QCReportSectionDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.QCReportMng_QCReportSection_View.Where(s => s.QCReportSection == param).ToList();

                    if (dbItem == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                        return data;
                    }

                    data = converter.DB2DTO_GetSection(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public DTO.InitDataDTO SearchPI(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };
            DTO.InitDataDTO data = new DTO.InitDataDTO();
            data.Data = new List<DTO.SupportQCReportSetting>();
            try
            {
                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;
                int? factoryID = null;
                if (filters.ContainsKey("factoryID") && !string.IsNullOrEmpty(filters["factoryID"].ToString()))
                {
                    factoryID = Convert.ToInt32(filters["factoryID"].ToString());
                }
                using (var context = CreateContext())
                {
                    data.Data = converter.DB2DTO_SearchPI(context.QCReportMng_function_SearchQCReportSetting(factoryID, searchString).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return null;
            }
            return data;
        }

        public DTO.EditForm GetData(int id, int? saleOrderDetailID, int? factoryID, string itemFactoryOrderLink,int? clientID, string articleCode, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.EditForm data = new DTO.EditForm();
            data.Data = new DTO.QCReportDTO();
            data.Data.QCReportSummaryDTOs = new List<DTO.QCReportSummaryDTO>();
            data.Data.QCReportDetailDTOs = new List<DTO.QCReportDetailDTO>();
            data.Data.QCReportDefectDTOs = new List<DTO.QCReportDefectDTO>();
            data.Data.QCReportImageDTOs = new List<DTO.QCReportImageDTO>();
            data.SupportInspectionStages = new List<DTO.SupportInspectionStage>();
            data.SupportInspectionResults = new List<DTO.SupportInspectionResult>();
            data.SupportSummaryResults = new List<DTO.SupportSummaryResult>();
            data.SupportPackagingMethods = new List<DTO.SupportPackagingMethod>();
            data.QCReportTestEnvironmentCategoryDTOs = new List<DTO.QCReportTestEnvironmentCategoryDTO>();
            data.Data.QCReportFactoryOrderDetailDTOs = new List<DTO.QCReportFactoryOrderDetailDTO>();
            data.ListPIFromFactoryOrderDTOs = new List<DTO.ListPIFromFactoryOrderDTO>();
            try
            {
                using (var context = CreateContext())
                {
                    if (id != 0)
                    {
                        QCReportMng_QCReport_View2 dbItem = context.QCReportMng_QCReport_View2.FirstOrDefault(s => s.QCReportID == id);
                        if (dbItem == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                            return data;
                        }
                        data.Data = converter.DB2DTO_GetQCReport(dbItem);
                        data.SupportInspectionStages = converter.DB2DTO_GetInspectionStage(context.SupportMng_InspectionStage_View.ToList());
                        data.SupportInspectionResults = converter.DB2DTO_GetInspectionResult(context.SupportMng_InspectionResult_View.ToList());
                        data.SupportSummaryResults = converter.DB2DTO_GetSummaryResult(context.SupportMng_QCReportSummaryResult_View.ToList());
                        data.SupportPackagingMethods = converter.DB2DTO_GetPackagingMethod(context.QCReportMng_PackagingMethod_View.ToList());
                        data.QCReportTestEnvironmentCategoryDTOs = converter.DB2DTO_GetTestEnvironmentCategory(context.QCReportMng_QCReportTestEnvironmentCategory_View.ToList());

                        if (data.Data.QCReportTestEnvironmentDTOs.Count == 0)
                        {
                            data.QCReportTestEnvironmentCategoryDTOs = converter.DB2DTO_GetTestEnvironmentCategory(context.QCReportMng_QCReportTestEnvironmentCategory_View.ToList());
                            CopyQCReportTestEnvironmentDTOs(data.QCReportTestEnvironmentCategoryDTOs, data.Data);
                        }

                        //Load listFactoryOrderDetail
                        if(data.Data.QCReportFactoryOrderDetailDTOs.Count > 0)
                        {
                            var dbFirstItem = context.QCReportMng_QCReportFactoryOrderDetail_View.FirstOrDefault(o => o.QCReportID == id);
                            data.ListPIFromFactoryOrderDTOs = converter.DB2DTO_ListPIFromFactoryOrderDTO(context.QCReportMng_function_SearchListPIFromFactoryOrder(dbFirstItem.FactoryID, dbFirstItem.ClientID, dbFirstItem.ArticleCode).ToList());
                            var list = "";
                            for (var j = 0; j < data.Data.QCReportFactoryOrderDetailDTOs.Count; j++)
                            {
                                var jItem = data.Data.QCReportFactoryOrderDetailDTOs[j];
                                if (list != "")
                                {
                                    list += ',';
                                }
                                list += jItem.FactoryOrderDetailID;
                            }

                            string[] idFactoryOrderDetail = list.Split(',');
                            var index = -1;


                            foreach (var sItem in data.ListPIFromFactoryOrderDTOs.ToList())
                            {
                                var check = false;
                                foreach (var item in idFactoryOrderDetail)
                                {
                                    if (sItem.FactoryOrderDetailID == Convert.ToInt32(item))
                                    {
                                        check = true;
                                        break;
                                    }
                                }
                                if (check == false)
                                {
                                    DTO.QCReportFactoryOrderDetailDTO factoryOrderDetailDTO = new DTO.QCReportFactoryOrderDetailDTO();
                                    factoryOrderDetailDTO.AdditionalRemark = sItem.AdditionalRemark;
                                    factoryOrderDetailDTO.IsSelected = false;
                                    factoryOrderDetailDTO.LDS = sItem.LDS;
                                    factoryOrderDetailDTO.OrderQnt = sItem.OrderQnt;
                                    factoryOrderDetailDTO.ProductionStatus = sItem.ProductionStatus;
                                    factoryOrderDetailDTO.FactoryOrderUD = sItem.FactoryOrderUD;
                                    factoryOrderDetailDTO.ProformaInvoiceNo = sItem.ProformaInvoiceNo;
                                    factoryOrderDetailDTO.QCReportFactoryOrderDetailID = index;
                                    factoryOrderDetailDTO.FactoryID = sItem.FactoryID;
                                    factoryOrderDetailDTO.FactoryOrderDetailID = sItem.FactoryOrderDetailID;
                                    factoryOrderDetailDTO.FactoryOrderID = sItem.FactoryOrderID;
                                    data.Data.QCReportFactoryOrderDetailDTOs.Add(factoryOrderDetailDTO);
                                    index = index - 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        var dbInitItem = context.QCReportMng_QCReportSetting_SearchView.FirstOrDefault(o => o.SaleOrderDetailID == saleOrderDetailID && o.FactoryID == factoryID);
                        if (dbInitItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not find data init";

                            return data;
                        }
                        // show Init to Edit
                        data.Data.SaleOrderDetailID = dbInitItem.SaleOrderDetailID;
                        data.Data.FactoryID = dbInitItem.FactoryID;
                        data.Data.FactoryName = dbInitItem.FactoryName;
                        data.Data.FactoryUD = dbInitItem.FactoryUD;
                        data.Data.ClientID = dbInitItem.ClientID;
                        data.Data.ClientUD = dbInitItem.ClientUD;
                        data.Data.ProformaInvoiceNo = dbInitItem.ProformaInvoiceNo;
                        data.Data.ArticleCode = dbInitItem.ArticleCode;
                        data.Data.Description = dbInitItem.Description;
                        data.Data.Quantity = dbInitItem.Quantity;
                        data.Data.AQLCritical = 0;
                        data.Data.AQLMajor = Convert.ToDecimal(2.50);
                        data.Data.AQLMinor = 4;

                        //Add List FactoryOrderDetail
                        if(itemFactoryOrderLink != "" && itemFactoryOrderLink != null)
                        {
                            data.ListPIFromFactoryOrderDTOs = converter.DB2DTO_ListPIFromFactoryOrderDTO(context.QCReportMng_function_SearchListPIFromFactoryOrder(factoryID, clientID, articleCode).ToList());
                            string [] idFactoryOrderDetail = itemFactoryOrderLink.Split(',');
                            var index = -1;
                            var orderQnt = 0;
                            foreach (var sItem in data.ListPIFromFactoryOrderDTOs.ToList())
                            {
                                var check = false;

                                foreach (var item in idFactoryOrderDetail)
                                {
                                    if(sItem.FactoryOrderDetailID == Convert.ToInt32(item))
                                    {
                                        check = true;
                                        break;
                                       
                                    }
                                }

                                if(check == true)
                                {
                                    DTO.QCReportFactoryOrderDetailDTO factoryOrderDetailDTO = new DTO.QCReportFactoryOrderDetailDTO();
                                    factoryOrderDetailDTO.AdditionalRemark = sItem.AdditionalRemark;
                                    factoryOrderDetailDTO.IsSelected = true;
                                    factoryOrderDetailDTO.LDS = sItem.LDS;
                                    factoryOrderDetailDTO.OrderQnt = sItem.OrderQnt;
                                    factoryOrderDetailDTO.ProductionStatus = sItem.ProductionStatus;
                                    factoryOrderDetailDTO.FactoryOrderUD = sItem.FactoryOrderUD;
                                    factoryOrderDetailDTO.ProformaInvoiceNo = sItem.ProformaInvoiceNo;
                                    factoryOrderDetailDTO.QCReportFactoryOrderDetailID = index;
                                    factoryOrderDetailDTO.FactoryID = sItem.FactoryID;
                                    factoryOrderDetailDTO.FactoryOrderDetailID = sItem.FactoryOrderDetailID;
                                    factoryOrderDetailDTO.FactoryOrderID = sItem.FactoryOrderID;
                                    data.Data.QCReportFactoryOrderDetailDTOs.Add(factoryOrderDetailDTO);
                                    if (sItem.OrderQnt != null)
                                    {
                                        orderQnt += (int)sItem.OrderQnt;
                                    }

                                    index = index - 1;
                                }
                                else
                                {
                                    DTO.QCReportFactoryOrderDetailDTO factoryOrderDetailDTO = new DTO.QCReportFactoryOrderDetailDTO();
                                    factoryOrderDetailDTO.AdditionalRemark = sItem.AdditionalRemark;
                                    factoryOrderDetailDTO.IsSelected = false;
                                    factoryOrderDetailDTO.LDS = sItem.LDS;
                                    factoryOrderDetailDTO.OrderQnt = sItem.OrderQnt;
                                    factoryOrderDetailDTO.ProductionStatus = sItem.ProductionStatus;
                                    factoryOrderDetailDTO.FactoryOrderUD = sItem.FactoryOrderUD;
                                    factoryOrderDetailDTO.ProformaInvoiceNo = sItem.ProformaInvoiceNo;
                                    factoryOrderDetailDTO.QCReportFactoryOrderDetailID = index;
                                    factoryOrderDetailDTO.FactoryID = sItem.FactoryID;
                                    factoryOrderDetailDTO.FactoryOrderDetailID = sItem.FactoryOrderDetailID;
                                    factoryOrderDetailDTO.FactoryOrderID = sItem.FactoryOrderID;
                                    data.Data.QCReportFactoryOrderDetailDTOs.Add(factoryOrderDetailDTO);
                                    index = index - 1;
                                }
                            }
                            data.Data.Quantity = orderQnt;
                        }

                        data.SupportInspectionStages = converter.DB2DTO_GetInspectionStage(context.SupportMng_InspectionStage_View.ToList());
                        data.SupportInspectionResults = converter.DB2DTO_GetInspectionResult(context.SupportMng_InspectionResult_View.ToList());
                        data.SupportSummaryResults = converter.DB2DTO_GetSummaryResult(context.SupportMng_QCReportSummaryResult_View.ToList());
                        data.SupportPackagingMethods = converter.DB2DTO_GetPackagingMethod(context.QCReportMng_PackagingMethod_View.ToList());
                        data.QCReportTestEnvironmentCategoryDTOs = converter.DB2DTO_GetTestEnvironmentCategory(context.QCReportMng_QCReportTestEnvironmentCategory_View.ToList());
                        CopyQCReportTestEnvironmentDTOs(data.QCReportTestEnvironmentCategoryDTOs, data.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return data;
            }
            return data;
        }

        public bool Update(int userId, int id, ref object dto, out Notification notification)
        {
            DTO.QCReportDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dto).ToObject<DTO.QCReportDTO>();
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (QCReportEntities context = CreateContext())
                {
                    QCReport dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.QCReport.FirstOrDefault(s => s.QCReportID == id);

                        if (dbItem == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data" };
                            return false;
                        }
                    }
                    else
                    {
                        dbItem = new QCReport();
                        context.QCReport.Add(dbItem);
                    }

                    // add QCReportImage
                    foreach (var item in dtoItem.QCReportImageDTOs)
                    {
                        if (item.ScanHasChange)
                        {
                            if (string.IsNullOrEmpty(item.ScanNewFile))
                            {
                                fwFactory.RemoveFile(item.FileUD);
                            }
                            else
                            {
                                item.FileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", item.ScanNewFile, item.FileUD);
                            }
                        }
                    }

                    converter.DTO2DB_QCReport(dtoItem, ref dbItem);
                    // product image
                    if (dtoItem.ScanHasChange)
                    {
                        dbItem.ProductImage = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoItem.ScanNewFile, dtoItem.ProductImage);
                    }

                    if (dtoItem.ReportScanHasChange)
                    {
                        dbItem.ReportFile = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoItem.ReportScanNewFile, dtoItem.ReportFile);
                    }

                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    context.QCReportSummary.Local.Where(o => o.QCReport == null).ToList().ForEach(s => context.QCReportSummary.Remove(s));
                    context.QCReportDetail.Local.Where(o => o.QCReport == null).ToList().ForEach(s => context.QCReportDetail.Remove(s));
                    context.QCReportDefect.Local.Where(o => o.QCReport == null).ToList().ForEach(s => context.QCReportDefect.Remove(s));
                    context.QCReportImage.Local.Where(o => o.QCReport == null).ToList().ForEach(s => context.QCReportImage.Remove(s));
                    context.QCReportTestEnvironment.Local.Where(o => o.QCReport == null).ToList().ForEach(s => context.QCReportTestEnvironment.Remove(s));
                    context.QCReportFactoryOrderDetail.Local.Where(o => o.QCReport == null).ToList().ForEach(s => context.QCReportFactoryOrderDetail.Remove(s));
                    context.SaveChanges();

                    //Create code of id
                    dbItem.QCReportUD = "QC" + dbItem.QCReportID.ToString("D8");
                    context.SaveChanges();

                    if (id == 0)
                    {
                        string emailSubject = "";
                        string emailBody = "";
                        string url = "";
                        url = this.frontendURL + "QCReportMng/Edit/" + dbItem.QCReportID.ToString();
                        emailSubject += "New QC report - " + "[" + dtoItem.ClientUD + "]" + " / " + "[" + dtoItem.FactoryUD + "]" + " - " + dtoItem.ArticleCode + " - " + dtoItem.Description;
                        emailBody += "New QC Report:";
                        emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to link to QC Report:" + url;

                        var QCReportMenber = context.QCReportMng_QCReportGroupMember_View.ToList();
                        var ClientManager = context.QCReportMng_ClientManager_View.Where(o => o.ClientID == dtoItem.ClientID).FirstOrDefault();

                        List<int?> UserIDs = new List<int?>() ;
                        UserIDs.Add(ClientManager.AccManagerID);
                        UserIDs.Add(ClientManager.AccManagerAssitantID);
                        UserIDs.Add(ClientManager.VNMerchandiserID);
                        UserIDs.Add(ClientManager.VNMerchandiserAssitantID);

                        string sendToEmail = "";
                        if (QCReportMenber.Count() > 0)
                        {
                            foreach (var item in QCReportMenber)
                            {
                                if (sendToEmail != "")
                                {
                                    sendToEmail += ";" + item.Email;
                                }
                                else
                                {
                                    sendToEmail = item.Email;
                                }

                                UserIDs.Add(item.UserID);
                            }
                        }
                        if (ClientManager.AccManagerEmail != "")
                        {
                            if (sendToEmail != "")
                            {
                                sendToEmail += ";" + ClientManager.AccManagerEmail;
                            }
                            else
                            {
                                sendToEmail = ClientManager.AccManagerEmail;
                            }
                        }
                        if (ClientManager.AccManagerAssitantEmail != "")
                        {
                            if (sendToEmail != "")
                            {
                                sendToEmail += ";" + ClientManager.AccManagerAssitantEmail;
                            }
                            else
                            {
                                sendToEmail = ClientManager.AccManagerAssitantEmail;
                            }
                        }
                        if (ClientManager.VNMerchandiserEmail != "")
                        {
                            if (sendToEmail != "")
                            {
                                sendToEmail += ";" + ClientManager.VNMerchandiserEmail;
                            }
                            else
                            {
                                sendToEmail = ClientManager.VNMerchandiserEmail;
                            }
                        }
                        if (ClientManager.VNMerchandiserAssitantEmail != "")
                        {
                            if (sendToEmail != "")
                            {
                                sendToEmail += ";" + ClientManager.VNMerchandiserAssitantEmail;
                            }
                            else
                            {
                                sendToEmail = ClientManager.VNMerchandiserAssitantEmail;
                            }
                        }

                        SendNotification(emailSubject, emailBody, sendToEmail, UserIDs);
                    }


                    dto = GetData(dbItem.QCReportID, dbItem.SaleOrderDetailID, dbItem.FactoryID,null,null,null, out notification).Data;

                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public string GetExcelReportData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            QCReportDataObject ds = new QCReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("QCReportRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@QCReportID", id);
                adap.TableMappings.Add("Table", "QCReportDataView");
                adap.TableMappings.Add("Table1", "QCReportDefectDataView");
                adap.TableMappings.Add("Table2", "QCReportDetailDataView");
                adap.TableMappings.Add("Table3", "QCReportImageDataView");
                adap.TableMappings.Add("Table4", "QCReportSummaryDataView");
                adap.TableMappings.Add("Table5", "QCReportTestEnvironmentDataView");
                adap.TableMappings.Add("Table6", "QCReportFactoryOrderDetailReportDataView");
                adap.Fill(ds);

                var list = "";
                for (var j = 0; j < ds.QCReportFactoryOrderDetailReportDataView.Count; j++)
                {
                    var jItem = ds.QCReportFactoryOrderDetailReportDataView[j];
                    if (list != "")
                    {
                        list += ", ";
                    }
                    list += jItem.ProformaInvoiceNo;
                }

                ds.QCReportDataView[0].ProformaInvoiceNo = list;

                //Remove table not use
                ds.Tables.Remove("QCReportImageMatrix");
                ds.Tables.Remove("QCReportTestEnviromentMatrix");
                ds.Tables.Remove("QCReportFactoryOrderDetailReportDataView");
                ds.AcceptChanges();


                return Library.Helper.CreateReportFileWithEPPlus2(ds, "QCReport");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public void CopyQCReportTestEnvironmentDTOs(List<DTO.QCReportTestEnvironmentCategoryDTO> supportList, DTO.QCReportDTO data)
        {
            data.QCReportTestEnvironmentDTOs = new List<DTO.QCReportTestEnvironmentDTO>();
            int i = -1;

            foreach (var item in supportList)
            {
                foreach (var subItem in item.QCReportTestEnvironmentItemDTOs)
                {
                    DTO.QCReportTestEnvironmentDTO newItem = new DTO.QCReportTestEnvironmentDTO()
                    {
                        QCReportTestEnvironmentID = i,
                        QCReportTestEnvironmentItemNM = subItem.Description,
                        QCReportTestEnvironmentItemID = subItem.QCReportTestEnvironmentItemID,
                        QCReportTestEnvironmentCategoryID = subItem.QCReportTestEnvironmentCategoryID,
                    };

                    data.QCReportTestEnvironmentDTOs.Add(newItem);

                    i = i - 1;
                }
            }
        }

        public bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.QCReport.Where(o => o.QCReportID == id).FirstOrDefault();
                    context.QCReport.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
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

        private void SendNotification(string emailSubject, string emailBody, string sendToEmail, List<int?> UserIDs)
        {
            try
            {
                List<string> emailAddress = new List<string>();

                using (QCReportEntities context = CreateContext())
                {
                    //string sendToEmail = "thinh.nguyen@anvietthinh.vn";

                    //int userID = 142;

                    // add to NotificationMessage table
                    if (UserIDs.Count > 0)
                    {
                        foreach(var userID in UserIDs){
                            if(userID!= null)
                            {
                                NotificationMessage notification = new NotificationMessage();
                                notification.UserID = userID;
                                notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_QAQC;
                                notification.NotificationMessageTitle = emailSubject;
                                notification.NotificationMessageContent = emailBody;
                                context.NotificationMessage.Add(notification);
                            }
                        }
                    }

                    EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                    dbEmail.EmailBody = emailBody;
                    dbEmail.EmailSubject = emailSubject;
                    dbEmail.SendTo = sendToEmail;
                    context.EmailNotificationMessage.Add(dbEmail);
                    context.SaveChanges();
                }

            }
            catch { }
        }

        public string GetExportPDF(int id, out Notification notification) {
            notification = new Notification() { Type = NotificationType.Success };
            QCReportDataObject ds = new QCReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("QCReportRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@QCReportID", id);
                adap.TableMappings.Add("Table", "QCReportDataView");
                adap.TableMappings.Add("Table1", "QCReportDefectDataView");
                adap.TableMappings.Add("Table2", "QCReportDetailDataView");
                adap.TableMappings.Add("Table3", "QCReportImageDataView");
                adap.TableMappings.Add("Table4", "QCReportSummaryDataView");
                adap.TableMappings.Add("Table5", "QCReportTestEnvironmentDataView");
                adap.TableMappings.Add("Table6", "QCReportFactoryOrderDetailReportDataView");
                adap.Fill(ds);

                var list = "";
                for (var j = 0; j < ds.QCReportFactoryOrderDetailReportDataView.Count; j++)
                {
                    var jItem = ds.QCReportFactoryOrderDetailReportDataView[j];
                    if (list != "")
                    {
                        list += ", ";
                    }
                    list += jItem.ProformaInvoiceNo;
                }

                ds.QCReportDataView[0].ProformaInvoiceNo = list;
                ds.Tables.Remove("QCReportFactoryOrderDetailReportDataView");

                if (!ds.QCReportDataView[0].IsThumbnailLocationNull() && !string.IsNullOrEmpty(ds.QCReportDataView[0].ThumbnailLocation))
                {
                    ds.QCReportDataView[0].ThumbnailLocation = FrameworkSetting.Setting.MediaThumbnailUrl + ds.QCReportDataView[0].ThumbnailLocation;
                }

                int imagePosition = 1;
                DataRow imageDataRow = null;
                int totalRowsI = 0;
                int totalCountI = ds.QCReportImageDataView.Count();
                foreach (var item in ds.QCReportImageDataView)
                {
                    totalRowsI++;

                    if (imageDataRow == null)
                    {
                        imageDataRow = ds.QCReportImageMatrix.NewRow();
                    }
                    
                    if (imagePosition == 1)
                    {
                        if (!item.IsThumbnailLocationNull() && !string.IsNullOrEmpty(item.ThumbnailLocation))
                        {
                            imageDataRow["ThumbnailLocation1"] = FrameworkSetting.Setting.MediaThumbnailUrl + item.ThumbnailLocation;
                        }
                        if (!item.IsFileLocationNull() && !string.IsNullOrEmpty(item.FileLocation))
                        {
                            imageDataRow["FileLocation1"] = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileLocation;
                        }
                        if (!item.IsImageTitleNull() && !string.IsNullOrEmpty(item.ImageTitle))
                        {
                            imageDataRow["ImageTitle1"] = item.ImageTitle;
                        }

                        if (totalRowsI == totalCountI)
                        {
                            //Add data
                            ds.QCReportImageMatrix.Rows.Add(imageDataRow);
                        }
                        //Change position 
                        imagePosition++;
                    }
                    else if (imagePosition == 2)
                    {
                        if (!item.IsThumbnailLocationNull() && !string.IsNullOrEmpty(item.ThumbnailLocation))
                        {
                            imageDataRow["ThumbnailLocation2"] = FrameworkSetting.Setting.MediaThumbnailUrl + item.ThumbnailLocation;
                        }
                        if (!item.IsFileLocationNull() && !string.IsNullOrEmpty(item.FileLocation))
                        {
                            imageDataRow["FileLocation2"] = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileLocation;
                        }
                        if (!item.IsImageTitleNull() && !string.IsNullOrEmpty(item.ImageTitle))
                        {
                            imageDataRow["ImageTitle2"] = item.ImageTitle;
                        }

                        if (totalRowsI == totalCountI)
                        {
                            //Add data
                            ds.QCReportImageMatrix.Rows.Add(imageDataRow);
                        }
                        //Change position 
                        imagePosition++;
                    }
                    else if (imagePosition == 3)
                    {
                        if (!item.IsThumbnailLocationNull() && !string.IsNullOrEmpty(item.ThumbnailLocation))
                        {
                            imageDataRow["ThumbnailLocation3"] = FrameworkSetting.Setting.MediaThumbnailUrl + item.ThumbnailLocation;
                        }
                        if (!item.IsFileLocationNull() && !string.IsNullOrEmpty(item.FileLocation))
                        {
                            imageDataRow["FileLocation3"] = FrameworkSetting.Setting.MediaThumbnailUrl + item.FileLocation;
                        }
                        if (!item.IsImageTitleNull() && !string.IsNullOrEmpty(item.ImageTitle))
                        {
                            imageDataRow["ImageTitle3"] = item.ImageTitle;
                        }
                        //Add data
                        ds.QCReportImageMatrix.Rows.Add(imageDataRow);
                        //Change position 
                        imagePosition = imagePosition - 2;
                        //
                        imageDataRow = null;
                    }
                }

                int testEnviromentPosition = 1;
                DataRow enviromentDataRow = null;
                int totalRowsE = 0;
                int totalCountE = ds.QCReportTestEnvironmentDataView.Count();
                foreach (var item in ds.QCReportTestEnvironmentDataView)
                {
                    totalRowsE++;

                    if (enviromentDataRow == null)
                    {
                        enviromentDataRow = ds.QCReportTestEnviromentMatrix.NewRow();
                    }

                    if (testEnviromentPosition == 1)
                    {
                        if (!item.IsQCReportTestEnvironmentItemNMNull() && !string.IsNullOrEmpty(item.QCReportTestEnvironmentItemNM))
                        {
                            enviromentDataRow["QCReportTestEnvironmentItemNM1"] = item.QCReportTestEnvironmentItemNM;
                        }
                        if (!item.IsQCReportTestEnvironmentCategoryNMNull() && !string.IsNullOrEmpty(item.QCReportTestEnvironmentCategoryNM))
                        {
                            enviromentDataRow["QCReportTestEnvironmentCategoryNM1"] = item.QCReportTestEnvironmentCategoryNM;
                        }
                        if (!item.IsIsSelectedTextNull())
                        {
                            enviromentDataRow["IsSelectedText1"] = item.IsSelectedText;
                        }
                        if (totalRowsE == totalCountE)
                        {
                            //Add data
                            ds.QCReportTestEnviromentMatrix.Rows.Add(enviromentDataRow);
                        }
                        //Change position 
                        testEnviromentPosition++;
                    }
                    else if (testEnviromentPosition == 2)
                    {
                        if (!item.IsQCReportTestEnvironmentItemNMNull() && !string.IsNullOrEmpty(item.QCReportTestEnvironmentItemNM))
                        {
                            enviromentDataRow["QCReportTestEnvironmentItemNM2"] = item.QCReportTestEnvironmentItemNM;
                        }
                        if (!item.IsQCReportTestEnvironmentCategoryNMNull() && !string.IsNullOrEmpty(item.QCReportTestEnvironmentCategoryNM))
                        {
                            enviromentDataRow["QCReportTestEnvironmentCategoryNM2"] = item.QCReportTestEnvironmentCategoryNM;
                        }
                        if (!item.IsIsSelectedTextNull())
                        {
                            enviromentDataRow["IsSelectedText2"] = item.IsSelectedText;
                        }
                        if (totalRowsE == totalCountE)
                        {
                            //Add data
                            ds.QCReportTestEnviromentMatrix.Rows.Add(enviromentDataRow);
                        }
                        //Change position 
                        testEnviromentPosition++;
                    }
                    else if (testEnviromentPosition == 3)
                    {
                        if (!item.IsQCReportTestEnvironmentItemNMNull() && !string.IsNullOrEmpty(item.QCReportTestEnvironmentItemNM))
                        {
                            enviromentDataRow["QCReportTestEnvironmentItemNM3"] = item.QCReportTestEnvironmentItemNM;
                        }
                        if (!item.IsQCReportTestEnvironmentCategoryNMNull() && !string.IsNullOrEmpty(item.QCReportTestEnvironmentCategoryNM))
                        {
                            enviromentDataRow["QCReportTestEnvironmentCategoryNM3"] = item.QCReportTestEnvironmentCategoryNM;
                        }
                        if (!item.IsIsSelectedTextNull())
                        {
                            enviromentDataRow["IsSelectedText3"] = item.IsSelectedText;
                        }
                        //Add data
                        ds.QCReportTestEnviromentMatrix.Rows.Add(enviromentDataRow);
                        //Change position 
                        testEnviromentPosition = testEnviromentPosition - 2;
                        //
                        enviromentDataRow = null;
                    }
                }

                //Remove table not use
                ds.AcceptChanges();

                return Library.Helper.CreateReceiptPrintout2(ds, "QCReport_pdf.rdlc");
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

        public List<DTO.ListPIFromFactoryOrderDTO> GetListPIFromFactoryOrderDTO(string articleCode, int? client, int? factory, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ListPIFromFactoryOrderDTO> data = new List<DTO.ListPIFromFactoryOrderDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var item = context.QCReportMng_function_SearchListPIFromFactoryOrder(factory, client, articleCode);
                    data = converter.DB2DTO_ListPIFromFactoryOrderDTO(item.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

    }
}
