using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private Sample3MngEntities CreateContext()
        {
            return new Sample3MngEntities(Library.Helper.CreateEntityConnectionString("DAL.Sample3MngModel"));
        }

        #region SAMPLE ORDER 

        public List<DTO.SampleOrderSearchResultDTO> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SampleOrderSearchResultDTO> result = new List<DTO.SampleOrderSearchResultDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    string SampleOrderUD = null;
                    string Season = null;
                    string ClientUD = null;
                    string ClientNM = null;
                    int? PurposeID = null;
                    int? TransportTypeID = null;
                    int? SampleOrderStatusID = null;
                    if (filters.ContainsKey("SampleOrderUD") && !string.IsNullOrEmpty(filters["SampleOrderUD"].ToString()))
                    {
                        SampleOrderUD = filters["SampleOrderUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                    {
                        ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PurposeID") && filters["PurposeID"] != null && !string.IsNullOrEmpty(filters["PurposeID"].ToString()))
                    {
                        PurposeID = Convert.ToInt32(filters["PurposeID"].ToString());
                    }
                    if (filters.ContainsKey("TransportTypeID") && filters["TransportTypeID"] != null && !string.IsNullOrEmpty(filters["TransportTypeID"].ToString()))
                    {
                        TransportTypeID = Convert.ToInt32(filters["TransportTypeID"].ToString());
                    }
                    if (filters.ContainsKey("SampleOrderStatusID") && filters["SampleOrderStatusID"] != null && !string.IsNullOrEmpty(filters["SampleOrderStatusID"].ToString()))
                    {
                        SampleOrderStatusID = Convert.ToInt32(filters["SampleOrderStatusID"].ToString());
                    }
                    totalRows = context.Sample3Mng_function_SearchSampleOrder(SampleOrderUD, Season, ClientUD, ClientNM, PurposeID, TransportTypeID, SampleOrderStatusID, orderBy, orderDirection).Count();
                    var data = context.Sample3Mng_function_SearchSampleOrder(SampleOrderUD, Season, ClientUD, ClientNM, PurposeID, TransportTypeID, SampleOrderStatusID, orderBy, orderDirection);
                    result = converter.DB2DTO_SampleOrderSearchResultList(data.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;

        }

        public DTO.OrderEditFormData GetOrderData(int id, int clientId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OrderEditFormData data = new DTO.OrderEditFormData();
            data.SupportList = new DTO.SupportData();
            data.SupportList.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            data.SupportList.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();

            data.Data = new DTO.SampleOrderDTO();
            data.Data.SampleMonitorDTOs = new List<DTO.SampleMonitorDTO>();

            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_SampleOrder(context.Sample3Mng_SampleOrder_View.Include("Sample3Mng_SampleMonitor_View").FirstOrDefault(o => o.SampleOrderID == id));
                    }
                    else
                    {
                        var selectedClient = context.Sample3Mng_Client_View.FirstOrDefault(o => o.ClientID == clientId);
                        if (selectedClient == null)
                        {
                            throw new Exception("Client info is not valid!");
                        }
                        data.Data.ClientID = clientId;
                        data.Data.ClientUD = selectedClient.ClientUD;
                        data.Data.Season = season;
                        data.Data.SampleOrderStatusID = 1;
                        data.Data.SampleOrderStatusNM = "PENDING";

                        int SampleMonitorIndex = 0;
                        // add default monitor person
                        foreach (Module.Support.DTO.NotificationMember nMember in supportFactory.GetNotificationMember("SampleVN_M"))
                        {
                            SampleMonitorIndex--;
                            data.Data.SampleMonitorDTOs.Add(new DTO.SampleMonitorDTO() { SampleMonitorID = SampleMonitorIndex, UserID = nMember.UserID, SampleMonitorGroupID = 1, FullName = nMember.EmployeeNM, InternalCompanyNM = nMember.InternalCompanyNM });
                        }
                        foreach (Module.Support.DTO.NotificationMember nMember in supportFactory.GetNotificationMember("SampleNL_M"))
                        {
                            SampleMonitorIndex--;
                            data.Data.SampleMonitorDTOs.Add(new DTO.SampleMonitorDTO() { SampleMonitorID = SampleMonitorIndex, UserID = nMember.UserID, SampleMonitorGroupID = 2, FullName = nMember.EmployeeNM, InternalCompanyNM = nMember.InternalCompanyNM });
                        }

                        // add monitor person base on the client
                        Sample3Mng_UserWithClient_View userClient = context.Sample3Mng_UserWithClient_View.FirstOrDefault(o => o.ClientID == clientId);
                        if (userClient != null)
                        {
                            if (userClient.SaleID.HasValue)
                            {
                                if (!data.Data.SampleMonitorDTOs.Select(o => o.UserID).ToArray().Contains(userClient.SaleID.Value))
                                {
                                    SampleMonitorIndex--;
                                    data.Data.SampleMonitorDTOs.Add(new DTO.SampleMonitorDTO() { SampleMonitorID = SampleMonitorIndex, UserID = userClient.SaleID.Value, SampleMonitorGroupID = 2, FullName = userClient.SaleNM, InternalCompanyNM = userClient.SaleCompNM });
                                }
                            }
                            if (userClient.Sale2ID.HasValue)
                            {
                                if (!data.Data.SampleMonitorDTOs.Select(o => o.UserID).ToArray().Contains(userClient.Sale2ID.Value))
                                {
                                    SampleMonitorIndex--;
                                    data.Data.SampleMonitorDTOs.Add(new DTO.SampleMonitorDTO() { SampleMonitorID = SampleMonitorIndex, UserID = userClient.Sale2ID.Value, SampleMonitorGroupID = 2, FullName = userClient.Sale2NM, InternalCompanyNM = userClient.SaleComp2NM });
                                }
                            }
                            if (userClient.SaleVNID.HasValue)
                            {
                                if (!data.Data.SampleMonitorDTOs.Select(o => o.UserID).ToArray().Contains(userClient.SaleVNID.Value))
                                {
                                    SampleMonitorIndex--;
                                    data.Data.SampleMonitorDTOs.Add(new DTO.SampleMonitorDTO() { SampleMonitorID = SampleMonitorIndex, UserID = userClient.SaleVNID.Value, SampleMonitorGroupID = 1, FullName = userClient.SaleVNNM, InternalCompanyNM = userClient.SaleCompVNNM });
                                }
                            }
                        }
                    }
                }

                data.SupportList.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
                data.SupportList.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool UpdateOrderData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleOrderDTO dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrderDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleOrder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new SampleOrder();
                        context.SampleOrder.Add(dbItem);
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                        dbItem.SampleOrderStatusID = 1; // pending status
                    }
                    else
                    {
                        dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Order not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem);
                        context.SaveChanges();

                        if (id <= 0)
                        {
                            // generate order number
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");
                                try
                                {
                                    dbItem.SampleOrderUD = dbItem.SampleOrderID.ToString("D8");
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    scope.Commit();
                                }
                            }
                        }
                    }

                    dtoItem = GetOrderData(dbItem.SampleOrderID, 0, string.Empty, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public DTO.SampleOrderOverview.OrderDTO GetOrderOverviewData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SampleOrderOverview.OrderDTO data = new DTO.SampleOrderOverview.OrderDTO();
            data.ProductDTOs = new List<DTO.SampleOrderOverview.ProductDTO>();
            data.MonitorDTOs = new List<DTO.SampleOrderOverview.MonitorDTO>();
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    var dbItem = context.Sample3Mng_SampleOrderOverview_Order_View
                        .Include("Sample3Mng_SampleOrderOverview_Monitor_View")
                        .Include("Sample3Mng_SampleOrderOverview_Product_View")
                        .Include("Sample3Mng_SampleOrderOverview_Product_View.Sample3Mng_SampleOrderOverview_ProductSubFactory_View")
                        .Include("Sample3Mng_SampleOrderOverview_Product_View.Sample3Mng_SampleOrderOverview_ProductLocation_View")
                        .FirstOrDefault(o => o.SampleOrderID == id);

                    if (dbItem == null)
                    {
                        throw new Exception("Sample Order not found!");
                    }
                    data = converter.DB2DTO_SampleOrderOverview(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        #endregion


        #region SAMPLE PRODUCT

        public DTO.SampleProductOverview.ProductDTO GetProductOverviewData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SampleProductOverview.ProductDTO data = new DTO.SampleProductOverview.ProductDTO();
            data.InternalRemarkDTOs = new List<DTO.SampleProductOverview.InternalRemarkDTO>();
            data.ItemLocationDTOs = new List<DTO.SampleProductOverview.ItemLocationDTO>();
            data.ProgressDTOs = new List<DTO.SampleProductOverview.ProgressDTO>();
            data.QARemarkDTOs = new List<DTO.SampleProductOverview.QARemarkDTO>();
            data.ReferenceImageDTOs = new List<DTO.SampleProductOverview.ReferenceImageDTO>();
            data.SubFactoryDTOs = new List<DTO.SampleProductOverview.SubFactoryDTO>();
            data.TechnicalDrawingDTOs = new List<DTO.SampleProductOverview.TechnicalDrawingDTO>();
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    var dbItem = context.Sample3Mng_SampleProductOverview_Product_View
                        .Include("Sample3Mng_SampleProductOverview_InternalRemark_View")
                        .Include("Sample3Mng_SampleProductOverview_InternalRemark_View.Sample3Mng_SampleProductOverview_InternalRemarkImage_View")
                        .Include("Sample3Mng_SampleProductOverview_ItemLocation_View")
                        .Include("Sample3Mng_SampleProductOverview_Progress_View")
                        .Include("Sample3Mng_SampleProductOverview_Progress_View.Sample3Mng_SampleProductOverview_ProgressImage_View")
                        .Include("Sample3Mng_SampleProductOverview_QARemark_View")
                        .Include("Sample3Mng_SampleProductOverview_QARemark_View.Sample3Mng_SampleProductOverview_QARemarkImage_View")
                        .Include("Sample3Mng_SampleProductOverview_ReferenceImage_View")
                        .Include("Sample3Mng_SampleProductOverview_SubFactory_View")
                        .Include("Sample3Mng_SampleProductOverview_TechnicalDrawing_View")
                        .FirstOrDefault(o => o.SampleProductID == id);

                    if (dbItem == null)
                    {
                        throw new Exception("Sample Product not found!");
                    }
                    data = converter.DB2DTO_SampleProductOverview(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        #endregion

        #region FACTORY ASSIGNMENT

        public DTO.FactoryAssignment.FormData GetFactoryAssignmentData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryAssignment.FormData data = new DTO.FactoryAssignment.FormData();
            data.SupportList = new DTO.SupportData();
            data.SupportList.Factories = new List<Support.DTO.Factory>();
            data.Data = new DTO.FactoryAssignment.ProductDTO();
            data.Data.SubFactoryDTOs = new List<DTO.FactoryAssignment.SubFactoryDTO>();

            try
            {
                if (id > 0)
                {
                    using (Sample3MngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_FactoryAssignment_Product(context.Sample3Mng_FactoryAssignment_Product_View.Include("Sample3Mng_FactoryAssignment_SubFactory_View").FirstOrDefault(o => o.SampleProductID == id));
                    }
                }
                data.SupportList.Factories = supportFactory.GetFactory();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public bool UpdateFactoryAssignmentData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactoryAssignment.ProductDTO dtoProduct = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryAssignment.ProductDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_FactoryAssignment(dtoProduct, ref dbItem);
                        context.SaveChanges();
                    }

                    dtoItem = GetFactoryAssignmentData(dbItem.SampleProductID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        #endregion

        #region INTERNAL REMARK

        public DTO.InternalRemark.FormData GetInternalRemarkData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InternalRemark.FormData data = new DTO.InternalRemark.FormData();
            data.Data = new DTO.InternalRemark.ProductDTO();
            data.Data.RemarkDTOs = new List<DTO.InternalRemark.RemarkDTO>();

            try
            {
                if (id > 0)
                {
                    using (Sample3MngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_InternalRemark_Product(context.Sample3Mng_InternalRemark_Product_View
                            .Include("Sample3Mng_InternalRemark_Remark_View")
                            .Include("Sample3Mng_InternalRemark_Remark_View.Sample3Mng_InternalRemark_RemarkImage_View")
                            .FirstOrDefault(o => o.SampleProductID == id));
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
        public bool UpdateInternalRemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.InternalRemark.RemarkDTO dtoRemark = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.InternalRemark.RemarkDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleRemark dbItem;
                    if (id <= 0)
                    {
                        dbItem = new SampleRemark();
                        context.SampleRemark.Add(dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.SampleRemark.FirstOrDefault(o => o.SampleRemarkID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Internal Remark not found!";
                        return false;
                    }
                    else
                    {
                        // check permission
                        if(userId != dbItem.UpdatedBy)
                        {
                            throw new Exception("Can not edit remark from other user!");
                        }
                        if ((DateTime.Now - dbItem.UpdatedDate.Value).TotalDays > 1)
                        {
                            throw new Exception("Too late to edit this remark, remark can only be updatable within 2 days from the last update!");
                        }
                        converter.DTO2DB_InternalRemark(dtoRemark, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        context.SaveChanges();
                    }

                    dtoItem = converter.DB2DTO_InternalRemark_Remark(context.Sample3Mng_InternalRemark_Remark_View
                        .Include("Sample3Mng_InternalRemark_RemarkImage_View")
                        .FirstOrDefault(o => o.SampleRemarkID == dbItem.SampleRemarkID));

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool DeleteInternalRemarkData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleRemark dbItem = context.SampleRemark.FirstOrDefault(o => o.SampleRemarkID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Internal Remark not found!");
                    }
                    else
                    {
                        // check permission
                        if (userId != dbItem.UpdatedBy)
                        {
                            throw new Exception("Can not delete remark from other user!");
                        }
                        if ((DateTime.Now - dbItem.UpdatedDate.Value).TotalDays > 1)
                        {
                            throw new Exception("Too late to delete this remark, remark can only be deleted within 2 days from the last update!");
                        }

                        foreach (SampleRemarkImage dbImage in dbItem.SampleRemarkImage.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbImage.FileUD);
                            }
                            context.SampleRemarkImage.Remove(dbImage);
                        }
                        context.SampleRemark.Remove(dbItem);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        #endregion

        #region QA REMARK

        public DTO.QARemark.FormData GetQARemarkData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.QARemark.FormData data = new DTO.QARemark.FormData();
            data.Data = new DTO.QARemark.ProductDTO();
            data.Data.RemarkDTOs = new List<DTO.QARemark.RemarkDTO>();

            try
            {
                if (id > 0)
                {
                    using (Sample3MngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_QARemark_Product(context.Sample3Mng_QARemark_Product_View
                            .Include("Sample3Mng_QARemark_Remark_View")
                            .Include("Sample3Mng_QARemark_Remark_View.Sample3Mng_QARemark_RemarkImage_View")
                            .FirstOrDefault(o => o.SampleProductID == id));
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
        public bool UpdateQARemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.QARemark.RemarkDTO dtoRemark = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.QARemark.RemarkDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleQARemark dbItem;
                    if (id <= 0)
                    {
                        dbItem = new SampleQARemark();
                        context.SampleQARemark.Add(dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.SampleQARemark.FirstOrDefault(o => o.SampleQARemarkID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "QA Remark not found!";
                        return false;
                    }
                    else
                    {
                        // check permission
                        if (userId != dbItem.UpdatedBy)
                        {
                            throw new Exception("Can not edit remark from other user!");
                        }
                        if ((DateTime.Now - dbItem.UpdatedDate.Value).TotalDays > 1)
                        {
                            throw new Exception("Too late to edit this remark, remark can only be updatable within 2 days from the last update!");
                        }
                        converter.DTO2DB_QARemark(dtoRemark, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        context.SaveChanges();
                    }

                    dtoItem = converter.DB2DTO_QARemark_Remark(context.Sample3Mng_QARemark_Remark_View
                        .Include("Sample3Mng_QARemark_RemarkImage_View")
                        .FirstOrDefault(o => o.SampleQARemarkID == dbItem.SampleQARemarkID));

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool DeleteQARemarkData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleQARemark dbItem = context.SampleQARemark.FirstOrDefault(o => o.SampleQARemarkID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("QA Remark not found!");
                    }
                    else
                    {
                        // check permission
                        if (userId != dbItem.UpdatedBy)
                        {
                            throw new Exception("Can not delete remark from other user!");
                        }
                        if ((DateTime.Now - dbItem.UpdatedDate.Value).TotalDays > 1)
                        {
                            throw new Exception("Too late to delete this remark, remark can only be deleted within 2 days from the last update!");
                        }

                        foreach (SampleQARemarkImage dbImage in dbItem.SampleQARemarkImage.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbImage.FileUD);
                            }
                            context.SampleQARemarkImage.Remove(dbImage);
                        }
                        context.SampleQARemark.Remove(dbItem);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        #endregion

        #region BUILDING PROCESS

        public DTO.BuildingProcess.FormData GetBuildingProcessData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.BuildingProcess.FormData data = new DTO.BuildingProcess.FormData();
            data.Data = new DTO.BuildingProcess.ProductDTO();
            data.Data.ProgressDTOs = new List<DTO.BuildingProcess.ProgressDTO>();

            try
            {
                if (id > 0)
                {
                    using (Sample3MngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_BuildingProcess_Product(context.Sample3Mng_BuildingProcess_Product_View
                            .Include("Sample3Mng_BuildingProcess_Progress_View")
                            .Include("Sample3Mng_BuildingProcess_Progress_View.Sample3Mng_BuildingProcess_ProgressImage_View")
                            .FirstOrDefault(o => o.SampleProductID == id));
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
        public bool UpdateBuildingProcessData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.BuildingProcess.ProgressDTO dtoProgress = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.BuildingProcess.ProgressDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleProgress dbItem;
                    if (id <= 0)
                    {
                        dbItem = new SampleProgress();
                        context.SampleProgress.Add(dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        dbItem = context.SampleProgress.FirstOrDefault(o => o.SampleProgressID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Progress not found!";
                        return false;
                    }
                    else
                    {
                        // check permission
                        if (userId != dbItem.UpdatedBy)
                        {
                            throw new Exception("Can not edit progress created by other user!");
                        }
                        if ((DateTime.Now - dbItem.UpdatedDate.Value).TotalDays > 1)
                        {
                            throw new Exception("Too late to edit this progress, progress can only be updatable within 2 days from the last update!");
                        }
                        converter.DTO2DB_BuildingProcess(dtoProgress, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        context.SaveChanges();
                    }

                    dtoItem = converter.DB2DTO_BuildingProcess_Progress(context.Sample3Mng_BuildingProcess_Progress_View
                        .Include("Sample3Mng_BuildingProcess_ProgressImage_View")
                        .FirstOrDefault(o => o.SampleProgressID == dbItem.SampleProgressID));

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool DeleteBuildingProcessData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleProgress dbItem = context.SampleProgress.FirstOrDefault(o => o.SampleProgressID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Progress not found!");
                    }
                    else
                    {
                        // check permission
                        if (userId != dbItem.UpdatedBy)
                        {
                            throw new Exception("Can not delete progress created by other user!");
                        }
                        if ((DateTime.Now - dbItem.UpdatedDate.Value).TotalDays > 1)
                        {
                            throw new Exception("Too late to delete this progress, remark can only be deleted within 2 days from the last update!");
                        }

                        foreach (SampleProgressImage dbImage in dbItem.SampleProgressImage.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbImage.FileUD);
                            }
                            context.SampleProgressImage.Remove(dbImage);
                        }
                        context.SampleProgress.Remove(dbItem);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        #endregion

        #region ITEM DATA

        public DTO.ItemData.FormData GetItemData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ItemData.FormData data = new DTO.ItemData.FormData();
            data.Data = new DTO.ItemData.ProductDTO();

            try
            {
                if (id > 0)
                {
                    using (Sample3MngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_ItemData_Product(context.Sample3Mng_ItemData_Product_View.FirstOrDefault(o => o.SampleProductID == id));
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
        public bool UpdateItemData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ItemData.ProductDTO dtoProduct = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ItemData.ProductDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_ItemData(dtoProduct, ref dbItem);
                        context.SaveChanges();
                    }

                    dtoItem = GetItemData(dbItem.SampleProductID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        #endregion

        #region PRODUCT INFO

        public DTO.ProductInfo.FormData GetProductInfoData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductInfo.FormData data = new DTO.ProductInfo.FormData();
            data.Data = new DTO.ProductInfo.ProductDTO();
            data.Data.ReferenceImageDTOs = new List<DTO.ProductInfo.ReferenceImageDTO>();

            try
            {
                if (id > 0)
                {
                    using (Sample3MngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_ProductInfo_Product(context.Sample3Mng_ProductInfo_Product_View
                            .Include("Sample3Mng_ProductInfo_ReferenceImage_View")
                            .FirstOrDefault(o => o.SampleProductID == id));
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
        public bool UpdateProductInfoData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ProductInfo.ProductDTO dtoProduct = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductInfo.ProductDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    SampleProduct dbItem;
                    if (id <= 0)
                    {
                        dbItem = new SampleProduct();
                        context.SampleProduct.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_ProductInfo(dtoProduct, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        context.SaveChanges();
                    }

                    dtoItem = GetProductInfoData(dbItem.SampleProductID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        #endregion

        #region OTHER FUNCTIONS

        public DTO.SupportData GetOrderSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SupportData result = new DTO.SupportData();
            result.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();
            result.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            result.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();

            try
            {
                result.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
                result.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
                result.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return result;
        }

        public List<DTO.MonitorUserDTO> QuickSearchAVTUsers(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MonitorUserDTO> result = new List<DTO.MonitorUserDTO>();
            int AVTCompanyID = 3;
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    if (string.IsNullOrEmpty(query))
                    {
                        result = converter.DB2DTO_MonitorUserList(context.Sample3Mng_MonitorUser_View.Where(o => o.CompanyID.Value == AVTCompanyID).ToList());
                    }
                    else
                    {
                        result = converter.DB2DTO_MonitorUserList(context.Sample3Mng_MonitorUser_View.Where(o => o.EmployeeNM.Contains(query) && o.CompanyID.Value == AVTCompanyID).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }

        public List<DTO.MonitorUserDTO> QuickSearchEurofarUsers(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MonitorUserDTO> result = new List<DTO.MonitorUserDTO>();
            int EurofarCompanyID = 4;
            try
            {
                using (Sample3MngEntities context = CreateContext())
                {
                    if (string.IsNullOrEmpty(query))
                    {
                        result = converter.DB2DTO_MonitorUserList(context.Sample3Mng_MonitorUser_View.Where(o => o.CompanyID.Value == EurofarCompanyID).ToList());
                    }
                    else
                    {
                        result = converter.DB2DTO_MonitorUserList(context.Sample3Mng_MonitorUser_View.Where(o => o.EmployeeNM.Contains(query) && o.CompanyID.Value == EurofarCompanyID).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return result;
        }

        #endregion



    }
}
