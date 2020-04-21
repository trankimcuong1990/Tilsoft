using Library;
using Library.DTO;
using Module.Sample2Mng.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Module.Sample2Mng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private string frontendURL = "http://app.tilsoft.bg/";

        private Sample2MngEntities CreateContext()
        {
            Sample2MngEntities context = new Sample2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.Sample2MngModel"));
            context.Database.CommandTimeout = 300;
            return context;
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.SampleOrderSearchResult>();
            totalRows = 0;

            string SampleOrderUD = null;
            string Season = null;
            string ClientUD = null;
            string ClientNM = null;
            int? PurposeID = null;
            int? TransportTypeID = null;
            int? SampleOrderStatusID = null;
            string SampleItemCode = null;
            string SampleItemName = null;
            int? FactoryID = null;
            int? AccountManagerID = null;

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
            if (filters.ContainsKey("AccountManagerID") && filters["AccountManagerID"] != null && !string.IsNullOrEmpty(filters["AccountManagerID"].ToString()))
            {
                AccountManagerID = Convert.ToInt32(filters["AccountManagerID"].ToString());
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
            if (filters.ContainsKey("SampleItemCode") && !string.IsNullOrEmpty(filters["SampleItemCode"].ToString()))
            {
                SampleItemCode = filters["SampleItemCode"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SampleItemName") && !string.IsNullOrEmpty(filters["SampleItemName"].ToString()))
            {
                SampleItemName = filters["SampleItemName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
            {
                FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
            }
            //try to get data
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    totalRows = context.Sample2Mng_function_SearchSampleOrder(SampleOrderUD, Season, ClientUD, ClientNM, PurposeID, TransportTypeID, SampleOrderStatusID, FactoryID, AccountManagerID, orderBy, orderDirection, SampleItemCode, SampleItemName).Count();
                    var result = context.Sample2Mng_function_SearchSampleOrder(SampleOrderUD, Season, ClientUD, ClientNM, PurposeID, TransportTypeID, SampleOrderStatusID, FactoryID, AccountManagerID, orderBy, orderDirection, SampleItemCode, SampleItemName);
                    data.Data = converter.DB2DTO_SampleOrderSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.SampleOrder();
            data.Data.SampleOrderStatusID = 1;
            data.Data.SampleOrderStatusNM = "PENDING";
            data.Data.SampleProducts = new List<DTO.SampleProduct>();
            data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

            data.Seasons = new List<Support.DTO.Season>();
            data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            data.Users = new List<Support.DTO.User>();
            data.Factories = new List<Support.DTO.Factory>();
            data.PackagingMethods = new List<PackagingMethodDTO>();
            
            data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
            data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();

            data.UnitDTOs = new List<UnitDTO>();
            data.SampleProductPackagingMaterialTypeDTOs = new List<SampleProductPackagingMaterialTypeDTO>();
            data.developmentTypeDTOs = new List<DevelopmentTypeDTO>();

            //try to get data
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
                            .Include("Sample2Mng_SampleProduct_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
                            .Include("Sample2Mng_SampleMonitor_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SamplePackaging_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductDimension_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductPackagingMaterial_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductCartonBox_View")
                            .FirstOrDefault(o => o.SampleOrderID == id));
                    }
                    else
                    {
                        // add monitor person
                        // VN: Long, Tinh, Viet, Alex
                        // NL: An, Dave, Jazwin, Pepjin, Luc
                        //int[] vnMonitorIds = { 2, 84, 63, 159 };
                        //int[] nlMonitorIds = { 36, 39, 303, 107 };
                        int[] vnMonitorIds = supportFactory.GetNotificationMember("SampleVN").Select(o => o.UserID).ToArray();
                        int[] nlMonitorIds = supportFactory.GetNotificationMember("SampleNL").Select(o => o.UserID).ToArray();
                        int index = -9999;
                        Support.DTO.User dtoUser;
                        List<Support.DTO.User> dtoUsers = supportFactory.GetUsers();
                        foreach (int monitorId in vnMonitorIds)
                        {
                            dtoUser = dtoUsers.FirstOrDefault(o => o.UserId == monitorId);
                            if (dtoUser != null)
                            {
                                data.Data.SampleMonitors.Add(new DTO.SampleMonitor() { UserID = monitorId, SampleMonitorGroupID = 1, FullName = dtoUser.FullName, OfficeNM = dtoUser.OfficeNM, SampleMonitorID = index });
                                index--;
                            }

                            //if (!dbItem.SampleMonitor.Select(o => o.UserID).ToArray().Contains(monitorId))
                            //{
                            //    dbItem.SampleMonitor.Add(new SampleMonitor() { UserID = monitorId, SampleMonitorGroupID = 1 });
                            //}
                        }
                        foreach (int monitorId in nlMonitorIds)
                        {
                            dtoUser = dtoUsers.FirstOrDefault(o => o.UserId == monitorId);
                            if (dtoUser != null)
                            {
                                data.Data.SampleMonitors.Add(new DTO.SampleMonitor() { UserID = monitorId, SampleMonitorGroupID = 2, FullName = dtoUser.FullName, OfficeNM = dtoUser.OfficeNM, SampleMonitorID = index });
                                index--;
                            }
                            //if (!dbItem.SampleMonitor.Select(o => o.UserID).ToArray().Contains(monitorId))
                            //{
                            //    dbItem.SampleMonitor.Add(new SampleMonitor() { UserID = monitorId, SampleMonitorGroupID = 2 });
                            //}
                        }
                    }

                    //set Description
                    foreach (var item in data.Data.SampleProducts.ToList())
                    {
                        if (item.ModelNM != null && item.ModelNM != "")
                        {
                            item.Description = item.ModelNM;
                        }
                        else
                        {
                            item.Description = item.ArticleDescription;
                        }
                        //Material 1
                        if (item.MaterialDescription != null && item.MaterialDescription != "")
                        {
                            item.Description += ", " + item.MaterialDescription;
                        }
                        if (item.MaterialTypeDescription != null && item.MaterialTypeDescription != "")
                        {
                            item.Description += ", " + item.MaterialTypeDescription;
                        }
                        if (item.MaterialColorDescription != null && item.MaterialColorDescription != "")
                        {
                            item.Description += ", " + item.MaterialColorDescription;
                        }
                        //Material 2
                        if (item.Material2Description != null && item.Material2Description != "")
                        {
                            item.Description += ", " + item.Material2Description;
                        }
                        if (item.Material2TypeDescription != null && item.Material2TypeDescription != "")
                        {
                            item.Description += ", " + item.Material2TypeDescription;
                        }
                        if (item.Material2ColorDescription != null && item.Material2ColorDescription != "")
                        {
                            item.Description += ", " + item.Material2ColorDescription;
                        }
                        //Material 3
                        if (item.Material3Description != null && item.Material3Description != "")
                        {
                            item.Description += ", " + item.Material3Description;
                        }
                        if (item.Material3TypeDescription != null && item.Material3TypeDescription != "")
                        {
                            item.Description += ", " + item.Material3TypeDescription;
                        }
                        if (item.Material3ColorDescription != null && item.Material3ColorDescription != "")
                        {
                            item.Description += ", " + item.Material3ColorDescription;
                        }
                        //Back Cushion
                        if (item.BackCushionDescription != null && item.BackCushionDescription != "")
                        {
                            item.Description += ", " + item.BackCushionDescription;
                        }
                        //Seat Cushion
                        if (item.SeatCushionDescription != null && item.SeatCushionDescription != "")
                        {
                            item.Description += ", " + item.SeatCushionDescription;
                        }
                        //Cushion Color
                        if (item.CushionColorDescription != null && item.CushionColorDescription != "")
                        {
                            item.Description += ", " + item.CushionColorDescription;
                        }
                    }

                    data.Seasons = supportFactory.GetSeason().ToList();
                    data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
                    data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
                    data.Users = supportFactory.GetUsers().ToList();
                    data.Factories = supportFactory.GetFactory().ToList();
                    data.PackagingMethods = AutoMapper.Mapper.Map<List<List_PackagingMethod_View>, List<DTO.PackagingMethodDTO>>(context.List_PackagingMethod_View.ToList());
                    data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
                    data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
                    data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
                    data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
                    data.UnitDTOs = AutoMapper.Mapper.Map<List<Sample2Mng_Unit_View>, List<UnitDTO>>(context.Sample2Mng_Unit_View.ToList());
                    data.SampleProductPackagingMaterialTypeDTOs = AutoMapper.Mapper.Map<List<Sample2Mng_SampleproductPackagingMaterialType_View>, List<SampleProductPackagingMaterialTypeDTO>>(context.Sample2Mng_SampleproductPackagingMaterialType_View.ToList());
                    data.developmentTypeDTOs = converter.DB2DTO_DevelopmentList(context.Sample2Mng_DevelopmentType_View.ToList());
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
            notification = new Notification();
            notification.Type = NotificationType.Success;

            bool isDeleted = false;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can't find Sample Order!";

                        return isDeleted;
                    }

                    if (dbItem.SampleProduct.Count() > 0)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can't delete Sample Order, because Sample Order contains Sample Product!";

                        return isDeleted;
                    }

                    context.SampleOrder.Remove(dbItem);

                    context.SaveChanges();

                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                isDeleted = false;
            }

            return isDeleted;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrder>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    // check for duplication
                    //int clientID = dtoOrder.ClientID.Value;
                    //string season = dtoOrder.Season;
                    //int primaryID = dtoOrder.SampleOrderID;
                    //SampleOrder dbDuplicate = context.SampleOrder.FirstOrDefault(o => o.ClientID == clientID && o.Season == season && o.SampleOrderID != primaryID);
                    //if (dbDuplicate != null)
                    //{
                    //    throw new Exception("Sample order for client: [" + dtoOrder.ClientUD + "] in season: [" + dtoOrder.Season + "] is already exist!");
                    //}

                    SampleOrder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new SampleOrder();
                        context.SampleOrder.Add(dbItem);
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
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

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoOrder.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                        if (id <= 0)
                        {
                            // add monitor person base on the client
                            if (dbItem.ClientID.HasValue)
                            {
                                List<int> vnMonitorIds = new List<int>();
                                List<int> nlMonitorIds = new List<int>();
                                int clientId = dbItem.ClientID.Value;

                                Sample2Mng_UserWithClient_View userClient = context.Sample2Mng_UserWithClient_View.FirstOrDefault(o => o.ClientID == clientId);
                                if (userClient != null)
                                {
                                    if (userClient.SaleID.HasValue)
                                    {
                                        nlMonitorIds.Add(userClient.SaleID.Value);
                                    }

                                    if (userClient.Sale2ID.HasValue)
                                    {
                                        nlMonitorIds.Add(userClient.Sale2ID.Value);
                                    }

                                    if (userClient.SaleVNID.HasValue)
                                    {
                                        vnMonitorIds.Add(userClient.SaleVNID.Value);
                                    }
                                }

                                foreach (int monitorId in vnMonitorIds)
                                {
                                    if (!dbItem.SampleMonitor.Select(o => o.UserID).ToArray().Contains(monitorId))
                                    {
                                        dbItem.SampleMonitor.Add(new SampleMonitor() { UserID = monitorId, SampleMonitorGroupID = 1 });
                                    }
                                }
                                foreach (int monitorId in nlMonitorIds)
                                {
                                    if (!dbItem.SampleMonitor.Select(o => o.UserID).ToArray().Contains(monitorId))
                                    {
                                        dbItem.SampleMonitor.Add(new SampleMonitor() { UserID = monitorId, SampleMonitorGroupID = 2 });
                                    }
                                }
                            }
                        }

                        // notification
                        SendNotification(context);

                        // generate order number
                        if (id <= 0)
                        {
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    context.SaveChanges();
                                    dbItem.SampleOrderUD = Library.Common.Helper.formatIndex(dbItem.SampleOrderID.ToString(), 8, "0");
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
                        else
                        {
                            context.SampleProductMinuteImage.Local.Where(o => o.SampleProductMinute == null).ToList().ForEach(o => context.SampleProductMinuteImage.Remove(o));
                            context.SampleProductMinute.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductMinute.Remove(o));
                            context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
                            context.SampleRemark.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleRemark.Remove(o));
                            context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
                            context.SampleTechnicalDrawing.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleTechnicalDrawing.Remove(o));
                            context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
                            context.SampleProgress.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProgress.Remove(o));
                            context.SampleReferenceImage.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleReferenceImage.Remove(o));
                            context.SampleProduct.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleProduct.Remove(o));
                            context.SampleMonitor.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleMonitor.Remove(o));
                            context.SamplePackaging.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SamplePackaging.Remove(o));
                            context.SampleProductDimension.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductDimension.Remove(o));
                            context.SampleProductPackagingMaterial.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductPackagingMaterial.Remove(o));
                            context.SampleProductCartonBox.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductCartonBox.Remove(o));
                            context.SaveChanges();
                        }

                        // update sample product code
                        SampleProduct dbProduct = null;
                        foreach (var dbProductCode in context.Sample2Mng_SampleProductCode_View.Where(o => o.SampleOrderID == dbItem.SampleOrderID))
                        {
                            dbProduct = dbItem.SampleProduct.FirstOrDefault(o => o.SampleProductID == dbProductCode.SampleProductID);
                            if (dbProduct != null)
                            {
                                dbProduct.SampleProductUD = dbProduct.CreatedDate.Value.ToString("yyyyMMdd") + "/" + dbProductCode.FactoryUD + "/" + dbProductCode.ClientUD + "/" + dbProductCode.SampleOrderUD + "/" + dbProductCode.DisplayIndex;
                                if (dbProductCode.FactoryUD.ToLower() != "xxx")
                                {
                                    dbProduct.IsCodeCompleted = true;
                                }
                            }
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SampleOrderID, out notification).Data;

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(dbItem.SampleOrderID, null); // table lockx and also check if item is available on sql server side

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
        public DTO.EditFormData GetDataDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.SampleOrder();
            data.Data.SampleOrderStatusID = 1;
            data.Data.SampleOrderStatusNM = "PENDING";
            data.Data.SampleProducts = new List<DTO.SampleProduct>();
            data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

            data.Seasons = new List<Support.DTO.Season>();
            data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            data.Users = new List<Support.DTO.User>();
            data.Factories = new List<Support.DTO.Factory>();
            data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
            data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


            //try to get data
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
                            .Include("Sample2Mng_SampleProduct_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
                            .Include("Sample2Mng_SampleMonitor_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
                            .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
                            .FirstOrDefault(o => o.SampleOrderID == id));
                    }

                    //set Description
                    foreach (var item in data.Data.SampleProducts.ToList())
                    {
                        if (item.ModelNM != null && item.ModelNM != "")
                        {
                            item.Description = item.ModelNM;
                        }
                        else
                        {
                            item.Description = item.ArticleDescription;
                        }
                        //Material 1
                        if (item.MaterialDescription != null && item.MaterialDescription != "")
                        {
                            item.Description += ", " + item.MaterialDescription;
                        }
                        if (item.MaterialTypeDescription != null && item.MaterialTypeDescription != "")
                        {
                            item.Description += ", " + item.MaterialTypeDescription;
                        }
                        if (item.MaterialColorDescription != null && item.MaterialColorDescription != "")
                        {
                            item.Description += ", " + item.MaterialColorDescription;
                        }
                        //Material 2
                        if (item.Material2Description != null && item.Material2Description != "")
                        {
                            item.Description += ", " + item.Material2Description;
                        }
                        if (item.Material2TypeDescription != null && item.Material2TypeDescription != "")
                        {
                            item.Description += ", " + item.Material2TypeDescription;
                        }
                        if (item.Material2ColorDescription != null && item.Material2ColorDescription != "")
                        {
                            item.Description += ", " + item.Material2ColorDescription;
                        }
                        //Material 3
                        if (item.Material3Description != null && item.Material3Description != "")
                        {
                            item.Description += ", " + item.Material3Description;
                        }
                        if (item.Material3TypeDescription != null && item.Material3TypeDescription != "")
                        {
                            item.Description += ", " + item.Material3TypeDescription;
                        }
                        if (item.Material3ColorDescription != null && item.Material3ColorDescription != "")
                        {
                            item.Description += ", " + item.Material3ColorDescription;
                        }
                        //Back Cushion
                        if (item.BackCushionDescription != null && item.BackCushionDescription != "")
                        {
                            item.Description += ", " + item.BackCushionDescription;
                        }
                        //Seat Cushion
                        if (item.SeatCushionDescription != null && item.SeatCushionDescription != "")
                        {
                            item.Description += ", " + item.SeatCushionDescription;
                        }
                        //Cushion Color
                        if (item.CushionColorDescription != null && item.CushionColorDescription != "")
                        {
                            item.Description += ", " + item.CushionColorDescription;
                        }
                    }

                    data.Seasons = supportFactory.GetSeason().ToList();
                    data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
                    data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
                    data.Users = supportFactory.GetUsers().ToList();
                    data.Factories = supportFactory.GetFactory().ToList();
                    data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();

                    data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
                    // only leave status which can be selected by QA
                    foreach (Support.DTO.SampleProductStatus status in data.SampleProductStatuses.Where(o => o.SampleProductStatusID != 4 && o.SampleProductStatusID != 6).ToArray())
                    {
                        data.SampleProductStatuses.Remove(status);
                    }


                    data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private void SendNotification(Sample2MngEntities context)
        {
            try
            {
                List<Library.DTO.EmailMessage> messages = new List<Library.DTO.EmailMessage>();
                foreach (var entry in context.ChangeTracker.Entries().Where(o => o.State != EntityState.Unchanged && o.State != EntityState.Detached))
                {
                    string detail = "";
                    int productId = 0;
                    Library.DTO.EmailMessage message = null;
                    Library.DTO.EmailMessage messagePAL = null;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (entry.Entity is SampleProductMinute)
                            {
                                // new minute added
                                SampleProductMinute minute = (SampleProductMinute)entry.Entity;
                                productId = minute.SampleProductID.Value;
                                SampleProduct product = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == productId);
                                if (product.SampleOrder.SampleOrderID > 0 && product.SampleProductID > 0)
                                {
                                    detail = "Meeting minute: " + minute.MeetingMinute;
                                    message = CreateMessage(context, product, "New meeting minute added for product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "]", detail, 0);
                                    if (message != null) messages.Add(message);
                                }
                            }
                            else if (entry.Entity is SampleRemark)
                            {
                                // new remark added
                                SampleRemark remark = (SampleRemark)entry.Entity;
                                productId = remark.SampleProductID.Value;
                                SampleProduct product = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == productId);
                                if (product.SampleOrder.SampleOrderID > 0 && product.SampleProductID > 0)
                                {
                                    detail = "Remark: " + remark.Remark;
                                    message = CreateMessage(context, product, "New internal remark added for product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "]", detail, 3);
                                    if (message != null) messages.Add(message);
                                }
                            }
                            else if (entry.Entity is SampleProgress)
                            {
                                // new progress added
                                SampleProgress progress = (SampleProgress)entry.Entity;
                                productId = progress.SampleProductID.Value;
                                SampleProduct product = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == productId);
                                if (product.SampleOrder.SampleOrderID > 0 && product.SampleProductID > 0)
                                {
                                    detail = "Remark: " + progress.Remark;
                                    message = CreateMessage(context, product, "New progress added for product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "]", detail, 1);
                                    if (message != null) messages.Add(message);
                                }
                            }
                            else if (entry.Entity is SampleTechnicalDrawing)
                            {
                                // new TD added
                                SampleTechnicalDrawing drawing = (SampleTechnicalDrawing)entry.Entity;
                                productId = drawing.SampleProductID.Value;
                                SampleProduct product = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == productId);
                                if (product.SampleOrder.SampleOrderID > 0 && product.SampleProductID > 0)
                                {
                                    message = CreateMessage(context, product, "New TD added for product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "]", string.Empty, 2);
                                    if (message != null) messages.Add(message);
                                }
                            }
                            else if (entry.Entity is SampleMonitor)
                            {
                                // new person add to the monitor list
                                SampleTechnicalDrawing drawing = (SampleTechnicalDrawing)entry.Entity;
                                productId = drawing.SampleProductID.Value;
                                SampleProduct product = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == productId);
                                if (product.SampleOrder.SampleOrderID > 0 && product.SampleProductID > 0)
                                {
                                    message = CreateMessage(context, product, "New TD added for product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "]", string.Empty, 2);
                                    if (message != null) messages.Add(message);
                                }
                            }
                            break;

                        case EntityState.Modified:
                            if (entry.Entity is SampleProduct)
                            {
                                // product info changed
                                SampleProduct product = (SampleProduct)entry.Entity;
                                foreach (var propName in entry.OriginalValues.PropertyNames)
                                {
                                    var originalValue = entry.OriginalValues[propName];
                                    var currentValue = entry.CurrentValues[propName];
                                    bool hasChanged = false;

                                    if (originalValue == null && currentValue != null)
                                    {
                                        hasChanged = true;
                                    }
                                    else if (originalValue != null && currentValue == null)
                                    {
                                        hasChanged = true;
                                    }
                                    else if (originalValue != null && currentValue != null)
                                    {
                                        if (originalValue.ToString() != currentValue.ToString())
                                        {
                                            hasChanged = true;
                                        }
                                    }

                                    if (hasChanged)
                                    {
                                        switch (propName)
                                        {
                                            case "IsTDNeeded":
                                                if (product.IsTDNeeded.HasValue && product.IsTDNeeded.Value)
                                                {
                                                    // send to Mr.Huy + Vu only - will create the notification setting for this later
                                                    message = CreateMessage(context, product, "TD is needed for product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "]", string.Empty, 2);
                                                    message.SendTo = "huy.nguyen@anvietthinh.vn; vu.pham@anvietthinh.vn";
                                                    if (message != null) messages.Add(message);
                                                    List<int> listuser = new List<int>();
                                                    listuser.Add(3);
                                                    listuser.Add(6);
                                                    foreach (var list in listuser)
                                                    {
                                                        // add to NotificationMessage table
                                                        NotificationMessage notification1 = new NotificationMessage();
                                                        notification1.UserID = list;
                                                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTDEVELOPMENT;
                                                        notification1.NotificationMessageTitle = message.EmailSubject;
                                                        notification1.NotificationMessageContent = message.EmailBody;
                                                        context.NotificationMessage.Add(notification1);
                                                    }
                                                }
                                                break;

                                            case "SampleProductStatusID":
                                                if (product.SampleProductStatusID.HasValue)
                                                {
                                                    string statusNM = supportFactory.GetSampleProductStatus().FirstOrDefault(o => o.SampleProductStatusID == product.SampleProductStatusID.Value).SampleProductStatusNM;
                                                    message = CreateMessage(context, product, "Sample product status of product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "] has changed to : " + statusNM, string.Empty, 2);
                                                    if (message != null) messages.Add(message);

                                                    if ((product.VNSuggestedFactoryID == 374043 || product.VNSuggestedFactoryID == 374072) && statusNM == "FINISHED")
                                                    {
                                                        messagePAL = CreateMessagePAL(context, product, "Sample product status of product: " + product.DisplayIndex.ToString() + " [" + product.ArticleDescription + "] has changed to : " + statusNM, string.Empty, 2);
                                                        if (messagePAL != null) messages.Add(messagePAL);
                                                    }

                                                    //if (product.SampleProductStatusID.Value >= 2 && product.VNSuggestedFactoryID.HasValue)
                                                    //{
                                                    //    // add factory users
                                                    //    foreach (DAL.Sample2Mng_FactoryUser_View dbFactoryUser in context.Sample2Mng_FactoryUser_View.Where(o => o.FactoryID == product.VNSuggestedFactoryID.Value))
                                                    //    {
                                                    //        message.SendTo += ";" + dbFactoryUser.Email1;
                                                    //    }
                                                    //    if (message != null) messages.Add(message);
                                                    //}

                                                    //switch (product.SampleProductStatusID.Value)
                                                    //{
                                                    //    case 1: // created
                                                    //        break;

                                                    //    case 2: // issued
                                                    //        break;

                                                    //    case 3: // revise
                                                    //        break;

                                                    //    case 4: // finished
                                                    //        break;

                                                    //    case 5: // sample approved
                                                    //        break;

                                                    //    case 6: // data finished
                                                    //        break;

                                                    //    case 7: // canceled
                                                    //        break;

                                                    //    case 9: // completed
                                                    //        break;

                                                    //    case 10: // sample rejected
                                                    //        break;

                                                    //    case 11: // data rejected
                                                    //        break;
                                                    //}
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }

                // add email message to database
                foreach (Library.DTO.EmailMessage message in messages)
                {
                    if (!string.IsNullOrEmpty(message.SendTo))
                    {                       
                        EmailNotificationMessage dbEmail = new EmailNotificationMessage();
                        dbEmail.EmailBody = message.EmailBody;
                        dbEmail.EmailSubject = message.EmailSubject;
                        dbEmail.SendTo = message.SendTo;
                        context.EmailNotificationMessage.Add(dbEmail);
                    }
                }
            }
            catch (Exception) { }
        }

        private Library.DTO.EmailMessage CreateMessage(Sample2MngEntities context, SampleProduct product, string description, string detail, int action)
        {
            string emailTitle = "";
            string emailBody = "";
            string sendTo = "";
            int clientID = 0;
            string url = "";
            int userId = 0;
            AccountMng_UserProfile_View profile;

            clientID = product.SampleOrder.ClientID.Value;
            SupportMng_Client_View client = context.SupportMng_Client_View.FirstOrDefault(o => o.ClientID == clientID);
            if (client != null)
            {
                url = this.frontendURL + "Sample2Mng/Detail/" + product.SampleOrder.SampleOrderID.ToString() + "?param=pi:" + product.SampleProductID.ToString() + ",a:" + action.ToString();
                emailTitle = "Sample order: [" + product.SampleOrder.SampleOrderUD + "]; Client: [" + client.ClientUD + " / " + client.ClientNM + "]; " + description;
                emailBody = "Sample order: [" + product.SampleOrder.SampleOrderUD + "]; Client: [" + client.ClientUD + " / " + client.ClientNM + "]; " + description;
                if (!string.IsNullOrEmpty(detail))
                {
                    emailBody += Environment.NewLine + detail;
                }
                emailBody += Environment.NewLine + "Click <a href='" + url + "'>here</a> to check or copy the following url and paste to your browser: " + url;
                sendTo = "";
                foreach (SampleMonitor person in product.SampleOrder.SampleMonitor)
                {
                    userId = person.UserID.Value;
                    profile = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                    if (profile != null && !string.IsNullOrEmpty(profile.Email))
                    {
                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + profile.Email : profile.Email;                       
                        // add to NotificationMessage table
                        NotificationMessage notification1 = new NotificationMessage();
                        notification1.UserID = profile.UserId;
                        notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTDEVELOPMENT;
                        notification1.NotificationMessageTitle = emailTitle;
                        notification1.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification1);
                                                
                    }
                }

                // send email also to the QA
                if (product.VNSuggestedFactoryID.HasValue)
                {
                    foreach (Sample2Mng_QAUser_View qaUser in context.Sample2Mng_QAUser_View.Where(o => o.FactoryID == product.VNSuggestedFactoryID.Value))
                    {
                        if (!string.IsNullOrEmpty(qaUser.Email))
                        {
                            sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + qaUser.Email : qaUser.Email;
                        }
                    }
                }

                return new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo };
            }

            return null;
        }

        private EmailMessage CreateMessagePAL(Sample2MngEntities context, SampleProduct product, string description, string detail, int action)
        {
            string emailTitle = "";
            string emailBody = "";
            string sendTo = "";
            int clientID = 0;
            string url = "";
            //int userId = 0;
            //AccountMng_UserProfile_View profile;

            clientID = product.SampleOrder.ClientID.Value;
            SupportMng_Client_View client = context.SupportMng_Client_View.FirstOrDefault(o => o.ClientID == clientID);
            if (client != null)
            {
                url = this.frontendURL + "BreakDownMng/Edit/0?modelId=0&sampleProductId=" + product.SampleProductID.ToString() + "&offerLineId=0";
                emailTitle = "Sample Item has finish, please login to update Breakdown [" + product.SampleOrder.SampleOrderUD + "]";
                emailBody = "Sample order: [" + product.SampleOrder.SampleOrderUD + "] </br> Client: [" + client.ClientUD + " / " + client.ClientNM + "] </br>" + description;
                if (!string.IsNullOrEmpty(detail))
                {
                    emailBody += Environment.NewLine + detail;
                }
                emailBody += Environment.NewLine + "</br>Click <a href='" + url + "'>here</a> to go to breakdown or copy the following url and paste to your browser: " + url;
                sendTo = "";

                List<string> emailAddress = new List<string>();

                var dbNotificationEmails = context.SupportMng_NotificationGroup_View.Where(o => o.NotificationGroupUD == "SamFi_PAL");
                foreach (var dbNotificationEmail in dbNotificationEmails.ToArray())
                {
                    if (!string.IsNullOrEmpty(dbNotificationEmail.Email1))
                    {
                        sendTo += !string.IsNullOrEmpty(sendTo) ? ";" + dbNotificationEmail.Email1 : dbNotificationEmail.Email1;
                    }
                }

                return new Library.DTO.EmailMessage() { EmailSubject = emailTitle, EmailBody = emailBody, SendTo = sendTo };
            }

            return null;
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();
            data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            data.Seasons = new List<Support.DTO.Season>();
            data.Factorys = new List<Support.DTO.Factory>();
            data.AccountManagerDTOs = new List<AccountManagerDTO>();

            try
            {               
                data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
                data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
                data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
                data.Seasons = supportFactory.GetSeason().ToList();
                data.Factorys = supportFactory.GetFactory().ToList();               
                using (var context = CreateContext())
                {                   
                    data.AccountManagerDTOs = AutoMapper.Mapper.Map<List<SupportMng_AccountManager_View>, List<DTO.AccountManagerDTO>>(context.SupportMng_AccountManager_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool UpdateOrderStatus(int userId, int id, int statusId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleOrder dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Order not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.SampleOrderStatusID = statusId;
                        dbItem.StatusUpdatedBy = userId;
                        dbItem.StatusUpdatedDate = DateTime.Now;
                        context.SaveChanges();

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(id, null); // table lockx and also check if item is available on sql server side

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

        public bool UpdateProductStatus(int userId, int id, int statusId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.SampleProductStatusID = statusId;
                        dbItem.StatusUpdatedBy = userId;
                        dbItem.StatusUpdatedDate = DateTime.Now;

                        // delete finished image if status is: 1,2,3,10 ~ CREATE,CONFIRMED,REVISED,REJECTED
                        int[] statuses = { 1, 2, 3, 10 };
                        if (statuses.Contains(statusId))
                        {
                            if (!string.IsNullOrEmpty(dbItem.FinishedImage))
                            {
                                fwFactory.RemoveImageFile(dbItem.FinishedImage);
                                dbItem.FinishedImage = string.Empty;
                            }
                        }

                        // notification
                        SendNotification(context);

                        context.SaveChanges();

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(null, id); // table lockx and also check if item is available on sql server side

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

        public bool UpdateProductStatus2(int userId, int id, int statusId, string file, out Library.DTO.Notification notification)
        {
            // FINISH STATUS
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        // validate
                        //if (dbItem.VNSuggestedFactoryID == 374043 || dbItem.VNSuggestedFactoryID == 374072) // PAL factory
                        //{
                        //    //Sample2Mng_Breakdown_View dbBreakdown = context.Sample2Mng_Breakdown_View.FirstOrDefault(o => o.SampleProductID == dbItem.SampleProductID);
                        //    //if (dbBreakdown == null || !dbBreakdown.FinalAmountUSD.HasValue || dbBreakdown.FinalAmountUSD <= 0)
                        //    //{
                        //    //    throw new Exception("Invalid breakdown info!");
                        //    //}
                        //}
                        //else
                        //{
                        //    FactoryBreakdown dbFactoryBreakdown = dbItem.FactoryBreakdown.FirstOrDefault();
                        //    if (dbFactoryBreakdown == null)
                        //    {
                        //        throw new Exception("Factory breakdown info not exists!");
                        //    }
                        //    if (!dbFactoryBreakdown.IndicatedPrice.HasValue || dbFactoryBreakdown.IndicatedPrice <= 0)
                        //    {
                        //        throw new Exception("Invalid indicated price!");
                        //    }
                        //    //FactoryBreakdownDetail dbFactoryBreakdownDetail = dbFactoryBreakdown.FactoryBreakdownDetail.FirstOrDefault(o=>o.FactoryBreakdownCategoryID == 11);
                        //    //if (dbFactoryBreakdownDetail == null || !dbFactoryBreakdownDetail.Quantity.HasValue || dbFactoryBreakdownDetail.Quantity <= 0)
                        //    //{
                        //    //    throw new Exception("Invalid load ability!");
                        //    //}
                        //}

                        dbItem.SampleProductStatusID = statusId;
                        dbItem.StatusUpdatedBy = userId;
                        dbItem.StatusUpdatedDate = DateTime.Now;

                        // update file
                        if (!string.IsNullOrEmpty(file))
                        {
                            dbItem.FinishedImage = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", file, dbItem.FinishedImage);
                        }

                        // add product item
                        int totalExistingItem = dbItem.SampleProductItem.Count();
                        for (int index = 1; index <= dbItem.Quantity.Value - totalExistingItem; index++)
                        {
                            SampleProductItem dbProductItem = new SampleProductItem();
                            dbProductItem.SampleProductItemUD = dbItem.SampleProductUD + "-" + index.ToString("D2");
                            dbProductItem.CreatedDate = DateTime.Now;
                            dbItem.SampleProductItem.Add(dbProductItem);
                        }

                        // notification
                        SendNotification(context);

                        context.SaveChanges();

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(null, id); // table lockx and also check if item is available on sql server side

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

        public bool UpdateProgressData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleProgress dtoProgress = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleProgress>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleProgress dbItem = null;
                    if (id <= 0)
                    {
                        dbItem = new SampleProgress();
                        context.SampleProgress.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SampleProgress.FirstOrDefault(o => o.SampleProgressID == id);
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Progress not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_SampleProgress(dtoProgress, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                        // notification
                        SendNotification(context);

                        // remove orphan
                        context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
                        context.SaveChanges();

                        int newID = dbItem.SampleProgressID;
                        dtoItem = converter.DB2DTO_SampleProgress(context.Sample2Mng_SampleProgress_View.Include("Sample2Mng_SampleProgressImage_View").FirstOrDefault(o => o.SampleProgressID == newID));
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

        public bool DeleteProgress(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if can delete
                    SampleProgress dbItem = context.SampleProgress.FirstOrDefault(o => o.SampleProgressID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Progress not found");
                    }

                    // everything ok, delete
                    // remove attached file
                    foreach (SampleProgressImage dbImage in dbItem.SampleProgressImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbImage.FileUD);
                        }
                        dbItem.SampleProgressImage.Remove(dbImage);
                    }
                    context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
                    context.SampleProgress.Remove(dbItem);
                    context.SaveChanges();
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

        public bool UpdateTechnicalDrawingData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleTechnicalDrawing dtoTechnicalDrawing = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleTechnicalDrawing>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleTechnicalDrawing dbItem = null;
                    if (id <= 0)
                    {
                        dbItem = new SampleTechnicalDrawing();
                        context.SampleTechnicalDrawing.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Technical Drawing not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_SampleTechnicalDrawing(dtoTechnicalDrawing, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                        // notification
                        SendNotification(context);

                        context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
                        context.SaveChanges();

                        int newID = dbItem.SampleTechnicalDrawingID;
                        dtoItem = converter.DB2DTO_SampleTechnicalDrawing(context.Sample2Mng_SampleTechnicalDrawing_View.FirstOrDefault(o => o.SampleTechnicalDrawingID == newID));
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

        public bool ApproveTechnicalDrawingData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Technical Drawing not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = userId;
                        dbItem.ConfirmedDate = DateTime.Now;
                        context.SaveChanges();
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

        public bool ResetTechnicalDrawingData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Technical Drawing not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.IsConfirmed = null;
                        dbItem.ConfirmedBy = null;
                        dbItem.ConfirmedDate = null;
                        context.SaveChanges();
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

        public bool DeleteTechnicalDrawing(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if can delete
                    SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Technical drawing not found");
                    }

                    // everything ok, delete
                    // remove attached file

                    // delete files if any
                    foreach (SampleTechnicalDrawingFile dbFile in dbItem.SampleTechnicalDrawingFile.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbFile.PreviewFileUD))
                        {
                            // remove image file
                            fwFactory.RemoveImageFile(dbFile.PreviewFileUD);
                        }
                        if (!string.IsNullOrEmpty(dbFile.SourceFileUD))
                        {
                            // remove image file
                            fwFactory.RemoveImageFile(dbFile.SourceFileUD);
                        }
                        dbItem.SampleTechnicalDrawingFile.Remove(dbFile);
                    }
                    context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
                    context.SampleTechnicalDrawing.Remove(dbItem);
                    context.SaveChanges();
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

        public bool UpdateRemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleRemark dtoRemark = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleRemark>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    //if (fwFactory.CheckSupplierPermission(userId, dtoBooking.SupplierID.Value) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected supplier data");
                    //}
                    //if (id > 0 && fwFactory.CheckBookingPermission(userId, id) == 0)
                    //{
                    //    throw new Exception("Current user don't have access permission for the selected booking data");
                    //}

                    SampleRemark dbItem = null;
                    if (id <= 0)
                    {
                        dbItem = new SampleRemark();
                        context.SampleRemark.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SampleRemark.FirstOrDefault(o => o.SampleRemarkID == id);
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Remark not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_SampleRemark(dtoRemark, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                        // notification
                        SendNotification(context);

                        // remove orphan
                        context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
                        context.SaveChanges();

                        int newID = dbItem.SampleRemarkID;
                        dtoItem = converter.DB2DTO_SampleRemark(context.Sample2Mng_SampleRemark_View.Include("Sample2Mng_SampleRemarkImage_View").FirstOrDefault(o => o.SampleRemarkID == newID));
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

        public bool DeleteRemark(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if can delete
                    SampleRemark dbItem = context.SampleRemark.FirstOrDefault(o => o.SampleRemarkID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Remark not found");
                    }

                    // everything ok, delete
                    // remove attached file
                    foreach (SampleRemarkImage dbImage in dbItem.SampleRemarkImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbImage.FileUD);
                        }
                        dbItem.SampleRemarkImage.Remove(dbImage);
                    }
                    context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
                    context.SampleRemark.Remove(dbItem);
                    context.SaveChanges();
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

        public bool UpdateQARemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleQARemark dtoRemark = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleQARemark>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    SampleProduct dbProduct = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == dtoRemark.SampleProductID);
                    if (dbProduct == null)
                    {
                        throw new Exception("Sample not found");
                    }

                    SampleQARemark dbItem = new SampleQARemark();
                    dbProduct.SampleQARemark.Add(dbItem);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    converter.DTO2DB_SampleQARemark(dtoRemark, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                    // notification
                    SendNotification(context);

                    // update
                    context.SaveChanges();

                    int newID = dbItem.SampleQARemarkID;
                    dtoItem = converter.DB2DTO_SampleQARemark(context.Sample2Mng_SampleQARemark_View.Include("Sample2Mng_SampleQARemarkImage_View").FirstOrDefault(o => o.SampleQARemarkID == newID));
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool DeleteQARemark(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    // check if can delete
                    SampleQARemark dbItem = context.SampleQARemark.FirstOrDefault(o => o.SampleQARemarkID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Remark not found");
                    }

                    // everything ok, delete
                    // remove attached file
                    foreach (SampleQARemarkImage dbImage in dbItem.SampleQARemarkImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbImage.FileUD);
                        }
                        dbItem.SampleQARemarkImage.Remove(dbImage);
                    }
                    context.SampleQARemarkImage.Local.Where(o => o.SampleQARemark == null).ToList().ForEach(o => context.SampleQARemarkImage.Remove(o));
                    context.SampleQARemark.Remove(dbItem);
                    context.SaveChanges();
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

        public bool UpdateItemInfo(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleProduct dtoProduct = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleProduct>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (Sample2MngEntities context = CreateContext())
                {
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    dbItem.ItemInfoUpdatedBy = userId;
                    dbItem.ItemInfoUpdatedDate = DateTime.Now;
                    if (dbItem == null)
                    {
                        notification.Message = "Sample product not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_SampleItemInfo(dtoProduct, ref dbItem, userId);
                        context.SaveChanges();
                        dtoItem = converter.DB2DTO_SampleRemark(context.Sample2Mng_SampleRemark_View.Include("Sample2Mng_SampleRemarkImage_View").FirstOrDefault(o => o.SampleRemarkID == id));

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(null, id); // table lockx and also check if item is available on sql server side

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

        public List<DTO.ModelList> GetModelList(string param, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.ModelList> data = new List<DTO.ModelList>();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ModelList(context.Sample2Mng_function_GetModelList(param).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetDataDetailFactoryBreakdown(int sampleProductID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.SampleProduct data = new DTO.SampleProduct();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.Sample2Mng_SampleProduct_View
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
                            //.Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
                            .FirstOrDefault(o => o.SampleProductID == sampleProductID);

                    data = AutoMapper.Mapper.Map<Sample2Mng_SampleProduct_View, DTO.SampleProduct>(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        #region Export excel sample order
        public string ExportExcelSampleOrder(string sampleOrderUD, string season, string clientUD, string clientNM, int? purposeID, int? transportTypeID, int? sampleOrderStatusID, int? factoryID, string sampleItemCode, string sampleItemName, int? accountManager, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = NotificationType.Success };
            var ds = new ReportDataObject();

            try
            {
                var da = new SqlDataAdapter()
                {
                    SelectCommand = new SqlCommand("Sample2Mng_function_ExportExcelSampleOrder", new SqlConnection(Helper.GetSQLConnectionString()))
                };

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@SampleOrderUD", sampleOrderUD);
                da.SelectCommand.Parameters.AddWithValue("@Season", season);
                da.SelectCommand.Parameters.AddWithValue("@ClientUD", clientUD);
                da.SelectCommand.Parameters.AddWithValue("@ClientNM", clientNM);
                da.SelectCommand.Parameters.AddWithValue("@PurposeID", purposeID);
                da.SelectCommand.Parameters.AddWithValue("@TransportTypeID", transportTypeID);
                da.SelectCommand.Parameters.AddWithValue("@SampleOrderStatusID", sampleOrderStatusID);
                da.SelectCommand.Parameters.AddWithValue("@FactoryID", factoryID);
                da.SelectCommand.Parameters.AddWithValue("@SampleItemCode", sampleItemCode);
                da.SelectCommand.Parameters.AddWithValue("@SampleItemName", sampleItemName);
                da.SelectCommand.Parameters.AddWithValue("@AccountManagerID", accountManager);
                ds.EnforceConstraints = false;
                da.Fill(ds.Sample2Mng_SampleOrderRpt_View);
             
                var sampleOrderRes = ds.Sample2Mng_SampleOrderRpt_View.ToList();

                var listSampleOrderFilter = ds.Sample2Mng_SampleOrderRpt_View.ToList()
                                        .Select(s => new { 
                                                           s.ClientUD
                                                           , s.ClientNM
                                                           , s.SampleOrderUD
                                                           , s.Deadline
                                                           , s.HardDeadline
                                                           , s.PurposeNM
                                                           , s.ShipmentDetail})
                                        .Distinct();

                ReportDataObject.ExportToExcelRow dr;

                foreach (var item in listSampleOrderFilter)
                {
                    dr = ds.ExportToExcel.NewExportToExcelRow();
                    dr.ClientUD = item.ClientUD;
                    dr.ClientNM = item.ClientNM;
                    dr.SampleOrderUD = item.SampleOrderUD;
                    dr.Deadline = item.Deadline.ToShortDateString();
                    dr.HardDeadline = item.HardDeadline.ToShortDateString();
                    dr.PurposeNM = item.PurposeNM;
                    dr.ShipmentDetail = item.ShipmentDetail;

                    //get scm
                    string listSCM = string.Empty;
                    foreach (var xItem in sampleOrderRes.Where(o => o.SampleOrderUD == item.SampleOrderUD))
                    {
                        if (!string.IsNullOrEmpty(xItem.SCMMonitor))
                        {
                            listSCM += xItem.SCMMonitor + ',' ;
                        }
                    }
                    dr.SCMMonitor = listSCM.TrimEnd(',').Substring(0);
                    ds.ExportToExcel.AddExportToExcelRow(dr);

                }
                ds.AcceptChanges();
                return Helper.CreateReportFileWithEPPlus2(ds, "SampleOrderReport");
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    notification.DetailMessage.Add(ex.InnerException.Message);
                return string.Empty;
            }
        }
        #endregion
    }
}