using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using AutoMapper;
using FrameworkSetting;
using System.Data;
using System.Xml;
using System.IO;

namespace Module.QCReport.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private QCReportMngEntities CreateContext()
        {
            return new QCReportMngEntities(Library.Helper.CreateEntityConnectionString("DAL.QCReportMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.QCReportSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (QCReportMngEntities context = CreateContext())
                {
                    int? FactoryID = null;
                    string ClientUD = null;
                    string Season = null;
                    string ProformaInvoiceNo = null;

                    if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }

                    totalRows = context.QCReportMng_function_SearchQCReport(FactoryID, ClientUD, Season, ProformaInvoiceNo, orderBy, orderDirection).Count();
                    var result = context.QCReportMng_function_SearchQCReport(FactoryID, ClientUD, Season, ProformaInvoiceNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_QCReportSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (QCReportMngEntities context = CreateContext())
                {
                    QCReport dbItem = context.QCReport.FirstOrDefault(o => o.QCReportID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Report not found!";
                        return false;
                    }
                    else
                    {
                        context.QCReport.Remove(dbItem);
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
            DTO.QCReport dtoQCReport = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.QCReport>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (QCReportMngEntities context = CreateContext())
                {
                    QCReport dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new QCReport();
                        context.QCReport.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.QCReport.FirstOrDefault(o => o.QCReportID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "QC Report not found!";
                        return false;
                    }
                    else
                    {

                        // process image
                        foreach (DTO.QCReportImage dtoImage in dtoQCReport.QCReportImages)
                        {
                            if (dtoImage.HasChange)
                            {
                                dtoImage.FileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoImage.NewFile, dtoImage.FileUD);
                            }

                        }

                        // process qc report document
                        foreach (DTO.QCReportDocument dtoDocument in dtoQCReport.QCReportDocuments)
                        {
                            if (dtoDocument.QCReportDocument_HasChange)
                            {
                                dtoDocument.FileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDocument.QCReportDocument_NewFile, dtoDocument.FileUD);
                            }

                        }

                        // remove Technical Image
                        foreach (QCReportImage dbImage in context.QCReportImage.Local.Where(o => o.QCReport == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbImage.FileUD);
                            }
                        }

                        // remove Technical Document
                        foreach (QCReportDocument dbDocument in context.QCReportDocument.Local.Where(o => o.QCReport == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbDocument.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbDocument.FileUD);
                            }
                        }

                        context.QCReportImage.Local.Where(o => o.QCReport == null).ToList().ForEach(o => context.QCReportImage.Remove(o));
                        context.QCReportDocument.Local.Where(o => o.QCReport == null).ToList().ForEach(o => context.QCReportDocument.Remove(o));

                        //convert dto to db
                        converter.DTO2DB(dtoQCReport, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        context.SaveChanges();
                        dtoItem = GetData(userId, dbItem.QCReportID, dbItem.FactoryOrderDetailID, out notification).Data;
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

        public DTO.InitFormData GetInitSearchFilter(int userId, out Library.DTO.Notification notification)
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

        public DTO.InitSearchForm GetInitDataWithFilter(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitSearchForm data = new DTO.InitSearchForm();
            data.Data = new List<DTO.FactoryOrderDetailSearchResult>();
            int pageSize = 40;
            int pageIndex = 1;

            //pager info
            if (filters.ContainsKey("PageIndex")) pageIndex = Convert.ToInt32(filters["PageIndex"].ToString());

            //try to get data
            try
            {
                using (QCReportMngEntities context = CreateContext())
                {
                    string ArticleCode = null;
                    string Season = null;
                    string ClientUD = null;
                    int? FactoryID = null;
                    string ProformaInvoiceNo = null;

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }

                    data.TotalRows = context.QCReportMng_function_SearchFactoryOrderDetail(ArticleCode, Season, ClientUD, FactoryID, ProformaInvoiceNo).Count();
                    var result = context.QCReportMng_function_SearchFactoryOrderDetail(ArticleCode, Season, ClientUD, FactoryID, ProformaInvoiceNo);
                    data.Data = converter.DB2DTO_FactoryOrderDetailSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int? factoryOrderDetailID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.QCReport();
            DTO.FactoryOrderDetail factoryOrderDetailDTO = new DTO.FactoryOrderDetail();

            //Child data
            data.Data.QCReportDetails = new List<DTO.QCReportDetail>();
            data.Data.QCReportDefects = new List<DTO.QCReportDefect>();
            data.Data.QCReportImages = new List<DTO.QCReportImage>();
            data.Data.QCReportDocuments = new List<DTO.QCReportDocument>();

            //Support
            data.AvailableReports = new List<DTO.QCReport>();
            data.QCReportImageTitles = new List<DTO.QCReportImageTitleDTO>();
            data.InspectionTypes = new List<Support.DTO.InspectionType>();
            data.PackagingTypes = new List<Support.DTO.PackagingType>();
            data.QCReportDocumentTypes = new List<DTO.QCReportDocumentType>(); // qc report document type | tran.cuong | 20180228

            //try to get data
            try
            {
                using (QCReportMngEntities context = CreateContext())
                {
                    if (id <= 0)
                    {
                        //Get Factory Order Detail Info
                        var factoryOrderDetail = context.QCReportMng_FactoryOrderDetail_View.FirstOrDefault(o => o.FactoryOrderDetailID == factoryOrderDetailID);
                        factoryOrderDetailDTO = converter.DB2DTO_FactoryOrderDetail(factoryOrderDetail);
                        data.Data.FactoryOrderDetailID = factoryOrderDetailDTO.FactoryOrderDetailID;
                        data.Data.ProformaInvoiceNo = factoryOrderDetailDTO.ProformaInvoiceNo;
                        data.Data.OrderQnt = factoryOrderDetailDTO.OrderQnt;
                        data.Data.Season = factoryOrderDetailDTO.Season;
                        data.Data.ArticleCode = factoryOrderDetailDTO.ArticleCode;
                        data.Data.Description = factoryOrderDetailDTO.Description;
                        data.Data.ClientUD = factoryOrderDetailDTO.ClientUD;
                        data.Data.FactoryUD = factoryOrderDetailDTO.FactoryUD;

                        //Get Available QC Report Info
                        var availableReport = context.QCReportMng_QCReport_View.Where(o => o.FactoryOrderDetailID == factoryOrderDetailID).ToList();
                        data.AvailableReports = converter.DB2DTO_ListQCReport(availableReport);
                    }
                    else
                    {
                        var report = context.QCReportMng_QCReport_View.FirstOrDefault(o => o.QCReportID == id);
                        if (report == null)
                        {
                            throw new Exception("Can not found the report to edit");
                        }
                        //Get Available QC Report Info
                        var availableReport = context.QCReportMng_QCReport_View.Where(o => o.FactoryOrderDetailID == report.FactoryOrderDetailID).ToList();
                        data.AvailableReports = converter.DB2DTO_ListQCReport(availableReport);
                        data.Data = converter.DB2DTO_QCReport(report);
                    }
                    //Get Available QC Report Info
                    data.QCReportImageTitles = converter.DB2DTO_QCReportImageTitles(context.QCReportImageTitle.ToList());
                    data.InspectionTypes = supportFactory.GetInspectionType().ToList();
                    data.PackagingTypes = supportFactory.GetPackagingType().ToList();

                    // qc report document type | tran.cuong | 20180228
                    data.QCReportDocumentTypes = converter.DB2DTO_QCReportDocumentType(context.QCReportMng_QCReportDocumentType_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public string GetExcelData(int qcReportId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("QCReportMng_function_GetExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@qcReportID", qcReportId);
                adap.TableMappings.Add("Table", "QCReportMng_QCReport_View");
                adap.TableMappings.Add("Table1", "QCReportMng_QCReportDefect_View");
                adap.TableMappings.Add("Table2", "QCReportMng_QCReportDetail_View");
                adap.TableMappings.Add("Table3", "QCReportMng_QCReportImage_View");
                adap.Fill(ds);
                
                return Library.Helper.CreateReportFileWithEPPlus(ds, "QCReportOverview");
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

        // Add extra image title
        public bool Add_QCReportImageTitle(string QCReportImageTitleNM, out int QCReportImageTitleID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            QCReportImageTitleID = -1;
            try
            {
                using (QCReportMngEntities context = CreateContext())
                {
                    QCReportImageTitle dbItem = null;
                    if (!string.IsNullOrEmpty(QCReportImageTitleNM))
                    {
                        dbItem = new QCReportImageTitle();
                        dbItem.QCReportImageTitleNM = QCReportImageTitleNM;
                        context.QCReportImageTitle.Add(dbItem);
                        context.SaveChanges();
                        QCReportImageTitleID = dbItem.QCReportImageTitleID;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return false;
            }
        }
    }
}
