using Module.QAQCMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.QAQCMng;
using Library;
using System.IO;
using System.Web;

namespace Module.QAQCMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        private QAQCMngEntities CreateContext()
        {
            return new QAQCMngEntities(Library.Helper.CreateEntityConnectionString("DAL.QAQCMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.QAQCSearchResultDTOs = new List<DTO.QAQCSearchResultDTO>();
            totalRows = 0;

            int? statusID = null;
            string clientUD = null;
            string factoryOrderUD = null;
            string articleCode = null;
            string description = null;
            string factoryUD = null;
            string statusNM = null;

            if (filters.ContainsKey("statusID") && filters["statusID"] != null && !string.IsNullOrEmpty(filters["statusID"].ToString()))
            {
                statusID = Convert.ToInt32(filters["statusID"].ToString());
            }
            if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
            {
                clientUD = filters["clientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("factoryOrderUD") && !string.IsNullOrEmpty(filters["factoryOrderUD"].ToString()))
            {
                factoryOrderUD = filters["factoryOrderUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
            {
                articleCode = filters["articleCode"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
            {
                description = filters["description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("factoryUD") && !string.IsNullOrEmpty(filters["factoryUD"].ToString()))
            {
                factoryUD = filters["factoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("statusNM") && !string.IsNullOrEmpty(filters["statusNM"].ToString()))
            {
                statusNM = filters["statusNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (QAQCMngEntities context = CreateContext())
                {
                    totalRows = context.QAQCMng_Function_SearchResult(statusID, clientUD, factoryOrderUD, articleCode, description, factoryUD, statusNM, orderBy, orderDirection).Count();
                    var result = context.QAQCMng_Function_SearchResult(statusID, clientUD, factoryOrderUD, articleCode, description, factoryUD, statusNM, orderBy, orderDirection).ToList();
                    data.QAQCSearchResultDTOs = converter.DB2DTO_QAQCSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.EditFormData data = new DTO.EditFormData();
            //data.Data = new DTO.SampleOrder();
            //data.Data.SampleOrderStatusID = 1;
            //data.Data.SampleOrderStatusNM = "PENDING";
            //data.Data.SampleProducts = new List<DTO.SampleProduct>();
            //data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

            //data.Seasons = new List<Support.DTO.Season>();
            //data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            //data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            //data.Users = new List<Support.DTO.User>();
            //data.Factories = new List<Support.DTO.Factory>();
            //data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
            //data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            //data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


            ////try to get data
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        if (id > 0)
            //        {
            //            data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
            //                .Include("Sample2Mng_SampleProduct_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
            //                .Include("Sample2Mng_SampleMonitor_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
            //                .FirstOrDefault(o => o.SampleOrderID == id));
            //        }

            //        data.Seasons = supportFactory.GetSeason().ToList();
            //        data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
            //        data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
            //        data.Users = supportFactory.GetUsers().ToList();
            //        data.Factories = supportFactory.GetFactory().ToList();
            //        data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
            //        data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
            //        data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
            //        data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //DTO.SampleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrder>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleOrder dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new SampleOrder();
            //            context.SampleOrder.Add(dbItem);
            //            dbItem.CreatedBy = userId;
            //            dbItem.CreatedDate = DateTime.Now;
            //        }
            //        else
            //        {
            //            dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Order not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.UpdatedBy = userId;
            //            dbItem.UpdatedDate = DateTime.Now;
            //            converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
            //            context.SampleProductMinuteImage.Local.Where(o => o.SampleProductMinute == null).ToList().ForEach(o => context.SampleProductMinuteImage.Remove(o));
            //            context.SampleProductMinute.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductMinute.Remove(o));
            //            context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
            //            context.SampleRemark.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleRemark.Remove(o));
            //            context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
            //            context.SampleTechnicalDrawing.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleTechnicalDrawing.Remove(o));
            //            context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
            //            context.SampleProgress.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProgress.Remove(o));
            //            context.SampleReferenceImage.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleReferenceImage.Remove(o));
            //            context.SampleProduct.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleProduct.Remove(o));
            //            context.SampleMonitor.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleMonitor.Remove(o));
            //            context.SaveChanges();

            //            // generate order number
            //            if (id <= 0)
            //            {
            //                using (DbContextTransaction scope = context.Database.BeginTransaction())
            //                {
            //                    context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");

            //                    try
            //                    {
            //                        context.SaveChanges();
            //                        dbItem.SampleOrderUD = Library.Common.Helper.formatIndex(dbItem.SampleOrderID.ToString(), 8, "0");
            //                        context.SaveChanges();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw ex;
            //                    }
            //                    finally
            //                    {
            //                        scope.Commit();
            //                    }
            //                }
            //            }
            //            dtoItem = GetData(dbItem.SampleOrderID, out notification).Data;
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //DTO.SupportFormData data = new DTO.SupportFormData();
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }

        public List<QAQCSearchResultDTO> GetQAQCByUserID(int userID, out Library.DTO.Notification notification)
        {

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<QAQCSearchResultDTO> data = new List<QAQCSearchResultDTO>();

            try
            {
                using (QAQCMngEntities context = CreateContext())
                {
                    var result = context.QAQCMng_function_SearchQAQC(userID).ToList();
                    data = converter.DB2DTO_QAQCSearchResultDTOs(result);                    
                    foreach (var item in data)
                    {
                        item.ModelCheckListGroupDTOs = converter.DB2DTO_ModelCheckListGroupDTOs(context.QAQCMng_ModelCheckListGroup_View.Where(s => s.ModelID == item.ModelID).ToList());
                    }

                    //write log
                    //context.QAQCMng_function_InsertLogInspector(userID);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool SetTrackingFacoryOrDer(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<int> IDs = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<int>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    foreach (var qaqcID in IDs)
                    {
                        int? factoryID = context.QAQCMng_GetFactory_View.Where(o => o.QAQCID == qaqcID).Select(s=>s.FactoryID).FirstOrDefault();
                        int? approvalID = context.QAQCFactoryAccess.Where(o => o.FactoryID == factoryID && o.UserAccessID == userId).Select(s=>s.ApprovalID).FirstOrDefault();
                        LogInspector logInspector = new LogInspector();

                        logInspector.QAQCID = qaqcID;
                        logInspector.SysDate = DateTime.Now;
                        logInspector.Approval = approvalID;
                        logInspector.InspectorID = userId;
                        //Save
                        context.LogInspector.Add(logInspector);
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

        public CategoryDTO GetCategory(out Library.DTO.Notification notification)
        {

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            CategoryDTO data = new CategoryDTO();
            data.CheckListDTOs = new List<CheckListDTO>();
            data.DefectDTOs = new List<DefectDTO>();
            try
            {
                using (QAQCMngEntities context = CreateContext())
                {
                    data.CheckListDTOs = converter.DB2DTO_CheckListDTOs( context.QAQCMng_CheckList_View.ToList());
                    data.DefectDTOs = converter.DB2DTO_DefectDTOs(context.QAQCMng_Defects_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool UpdateReportFromMobile(int userID, object dtoItem, out Library.DTO.Notification notification)
        {
            ReportMobileInputDTO temp = (ReportMobileInputDTO)dtoItem;           
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
          
            try
            {
                using (QAQCMngEntities context = CreateContext())
                {
                    foreach (var item in temp.ReportQAQCDTOs)
                    {                       
                        int? reportQAQCID = context.QAQCMng_function_UpdateReportQAQC(item.QAQCID, item.TypeOfInspectionID, item.FinishedDate.ConvertStringToDateTime(), item.Remark, item.ReadyQty,
                                item.StatusID, item.ManagementStringID, item.IsSynced, item.SyncedDate.ConvertStringToDateTime(), item.CreatedDate.ConvertStringToDateTime(), item.CheckedQty, userID,
                                userID, CreateImageFromName(item.ImageName), item.LatitudeC, item.LongitudeC, item.LatitudeF, item.LongitudeF).FirstOrDefault();

                        if (reportQAQCID.HasValue)
                        {
                            if (item.ReportCheckListDTOs != null)
                            {
                                foreach (var reportCheckListDTO in item.ReportCheckListDTOs)
                                {
                                    int? reportCheckListID = context.QAQCMng_function_UpdateReportCheckList(reportQAQCID, reportCheckListDTO.CheckListID, reportCheckListDTO.Remark,
                                        reportCheckListDTO.StatusID, reportCheckListDTO.CheckListMobileStringID, userID, userID).FirstOrDefault();

                                    if (reportCheckListID != null && reportCheckListID.HasValue)
                                    {
                                        foreach (var reportCheckListImageDTO in reportCheckListDTO.ReportCheckListImageDTOs)
                                        {
                                            int? reportCheckListImageID = context.QAQCMng_function_UpdateReportCheckListImage(reportCheckListID, CreateImageFromName(reportCheckListImageDTO.ImageBase64),
                                                reportCheckListImageDTO.CheckListImageStringID, userID, userID).FirstOrDefault();
                                        }

                                    }
                                }
                            }

                            if (item.ReportDefectDTOs != null)
                            {
                                foreach (var reportDefectDTO in item.ReportDefectDTOs)
                                {
                                    int? reportDefectID = context.QAQCMng_function_UpdateReportDefect(reportQAQCID, reportDefectDTO.DefectID, reportDefectDTO.DefectAQty,
                                        reportDefectDTO.DefectBQty, reportDefectDTO.DefectCQty, reportDefectDTO.DefectMobileStringID, userID, userID).FirstOrDefault();

                                    if (reportDefectID != null && reportDefectID.HasValue)
                                    {
                                        foreach (var reportDefectImage in reportDefectDTO.ReportDefectImageDTOs)
                                        {
                                            int? reportDefectImageID = context.QAQCMng_function_UpdateReportDefectImage(reportDefectID, CreateImageFromName(reportDefectImage.ImageBase64),
                                                reportDefectImage.DefectImageMobileStringID, userID, userID).FirstOrDefault();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }

            return true; 
        }

        public List<QAQCReportStarusDTO> GetStatusQAQC(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<QAQCReportStarusDTO> datas = new List<QAQCReportStarusDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var result = context.QAQCMng_ReportQAQC_GetStatus_View.ToList();
                    datas = AutoMapper.Mapper.Map<List<QAQCMng_ReportQAQC_GetStatus_View>, List<QAQCReportStarusDTO>>(result).ToList();
                }
                return datas;
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return datas;
            }
        }

        public Int32? MakeToProcess(int userID, string managementStringID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Int32? statusID = 1; //Open Status
            try
            {
                using (var context = CreateContext())
                {
                    ReportQAQC report;
                    report = context.ReportQAQC.Where(o => o.ManagementStringID == managementStringID).FirstOrDefault();
                    if (report != null)
                    {
                        report.StatusID = statusID;
                        context.SaveChanges();
                        return statusID;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return null;
            }

        }

        public DTO.ReportQAQCDTO GetReportQAQC(int userId, int id, out Library.DTO.Notification notification )
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportQAQCDTO data = new DTO.ReportQAQCDTO();

            try
            {
                using (var context = CreateContext()) {
                    var result = context.ReportQAQCMng_ReportQAQC_View.Where(o => o.ReportQAQCID == id).FirstOrDefault();
                    data = converter.BD2DTO_Report(result);
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public DTO.ReportDataDTO SearchReportData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportDataDTO data = new ReportDataDTO();

            try
            {
                int? TypeOfInspectionID = null;
                int? StatusID = null;
                string CreatedDateStr = null;
                string CreatedNM = null;
                string ReportDateStr = null;
                string ApprovalNM = null;
                string ModelUD = null;
                string ModelNM = null;
                string ArticleCode = null;


                if (filters.ContainsKey("typeOfInspectionID") && filters["typeOfInspectionID"] != null)
                {
                    TypeOfInspectionID = Convert.ToInt32(filters["typeOfInspectionID"]);
                }
                if (filters.ContainsKey("statusID") && filters["statusID"] != null)
                {
                    StatusID = Convert.ToInt32(filters["statusID"]);
                }
                if (filters.ContainsKey("createdDate") && !string.IsNullOrEmpty(filters["createdDate"].ToString()))
                {
                    CreatedDateStr = filters["createdDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("createdNM") && !string.IsNullOrEmpty(filters["createdNM"].ToString()))
                {
                    CreatedNM = filters["createdNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("reportDate") && !string.IsNullOrEmpty(filters["reportDate"].ToString()))
                {
                    ReportDateStr = filters["reportDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("approvalNM") && !string.IsNullOrEmpty(filters["approvalNM"].ToString()))
                {
                    ApprovalNM = filters["approvalNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    ModelUD = filters["modelUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("modelNM") && !string.IsNullOrEmpty(filters["modelNM"].ToString()))
                {
                    ModelNM = filters["modelNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
                {
                    ArticleCode = filters["articleCode"].ToString().Replace("'", "''");
                }
                DateTime? CreatedDate = CreatedDateStr.ConvertStringToDateTime();
                DateTime? ReportDate = ReportDateStr.ConvertStringToDateTime();

                using (var context = CreateContext()) {
                    data.totalRows = context.ReportQAQCMng_Function_SearchResult(userId, TypeOfInspectionID, StatusID, CreatedDate, CreatedNM, ReportDate, ApprovalNM, ModelUD, ModelNM, ArticleCode, orderBy, orderDirection).Count();
                    var result = context.ReportQAQCMng_Function_SearchResult(userId, TypeOfInspectionID, StatusID, CreatedDate, CreatedNM, ReportDate, ApprovalNM, ModelUD, ModelNM, ArticleCode, orderBy, orderDirection);
                    data.reportQAQCSearchDTOs = converter.DB2DTO_ReportSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    return data;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }

        public bool SetStatusReport(int userId, int id, int statusId, string comment, out Library.DTO.Notification notification) {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (var context = CreateContext()) {
                    var item = context.ReportQAQCMng_ReportQAQC_View.FirstOrDefault(o => o.ReportQAQCID == id);
                    if (item.ApprovalID == null)
                    {
                        throw new Exception("Unable to find approver.");
                    }
                    if (item.ApprovalID != userId)
                    {
                        throw new Exception("You are not an approver.");
                    }
                    //Pass check User
                    ReportQAQC db;
                    db = context.ReportQAQC.Where(o => o.ReportQAQCID == id).FirstOrDefault();
                    if (db != null)
                    {
                        db.StatusID = statusId;
                        db.ApprovalBy = userId;
                        db.ApprovalDate = DateTime.Now;
                        db.Comment = comment;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Could not find data.");
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error };
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        private string CreateImge(string byteString, string imgStr)
        {
            if (byteString == null || byteString.Equals("")) return string.Empty;

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/"));
            }
            var uploadPath = HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/");
            string fileName = imgStr + ".jpeg";
            if (getExistsFile(fileName) == false)
            {
                using (var fs = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                {
                    byte[] buffer = System.Convert.FromBase64String(byteString);
                    fs.Write(buffer, 0, buffer.Length);
                }
                return fwFactory.CreateFilePointer(uploadPath, fileName, null);
            }
            else {
                return getFileUD(fileName);
            }
        }

        private bool getExistsFile(string fileName) {
            bool result = false;
            using (var context = CreateContext()) {
                var fileUD = context.Files.Where(o => o.FriendlyName == fileName).Select(o=>o.FileUD).FirstOrDefault();
                if (fileUD != null && !fileUD.Equals("")) {
                    result = true;
                }
            }
            return result;
        }

        private string getFileUD(string fileName) {
            using (var context = CreateContext()) {
                return context.Files.Where(o => o.FriendlyName == fileName).Select(o => o.FileUD).FirstOrDefault();
            }
        }

        public List<DTO.LogInspectorDTO> GetLogInspector(int userId, int qaqcId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.LogInspectorDTO> datas = new List<LogInspectorDTO>();
            try
            {
                using (var context = CreateContext()) {
                    var result = context.QAQCMng_LogInspector_SearchView.Where(o => o.QAQCID == qaqcId).ToList();
                    datas = converter.DB2DTO_Loginspec(result).ToList();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return datas;
            }
        }

        public DTO.LoggedInUser LoginUser(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LoggedInUser user = new LoggedInUser();

            try
            {
                using (var context = CreateContext()) {
                    var dbItem = context.Employee.FirstOrDefault(o => o.UserID == userId);
                    if (dbItem != null)
                    {
                        user.UserId = "";
                        user.UserName = dbItem.EmployeeNM;
                        user.DisplayName = dbItem.EmployeeNM;
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return null;
            }
        }

        private string CreateImageFromName(string fileName)
        {
            var uploadPath = HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/");
            return fwFactory.CreateFilePointer(uploadPath, fileName, null);
        }
    }
}
