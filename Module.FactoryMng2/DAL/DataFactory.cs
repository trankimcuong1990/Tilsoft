using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.FactoryMng2.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private FactoryMng2Entities CreateContext()
        {
            return new FactoryMng2Entities(Library.Helper.CreateEntityConnectionString("DAL.FactoryMng2Model"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactorySearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryMng2Entities context = CreateContext())
                {
                    int? FactoryID = null;
                    int UserID = -1;
                    string FactoryOfficialName = null;
                    string FactoryName = null;
                    //string SupplierIDs = "";
                    int? FactoryRawMaterialID = null;
                    int? LocationID = null;
                    bool? IsActive = null;
                    bool? IsPotential = null;

                    if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    if (filters.ContainsKey("FactoryOfficialName") && !string.IsNullOrEmpty(filters["FactoryOfficialName"].ToString()))
                    {
                        FactoryOfficialName = filters["FactoryOfficialName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryName") && !string.IsNullOrEmpty(filters["FactoryName"].ToString()))
                    {
                        FactoryName = filters["FactoryName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryRawMaterialID") && !string.IsNullOrEmpty(filters["FactoryRawMaterialID"].ToString()))
                    {
                        FactoryRawMaterialID = Convert.ToInt32(filters["FactoryRawMaterialID"]);
                    }
                    if (filters.ContainsKey("LocationID") && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
                    {
                        LocationID = Convert.ToInt32(filters["LocationID"]);
                    }
                    if (filters.ContainsKey("IsActive") && filters["IsActive"] != null && !string.IsNullOrEmpty(filters["IsActive"].ToString()))
                    {
                        IsActive = (filters["IsActive"].ToString() == "true") ? true : false;
                    }
                    if (filters.ContainsKey("IsPotential") && filters["IsPotential"] != null && !string.IsNullOrEmpty(filters["IsPotential"].ToString()))
                    {
                        IsPotential = (filters["IsPotential"].ToString() == "true") ? true : false;
                    }

                    totalRows = context.FactoryMng2_function_SearchFactory(FactoryID, FactoryOfficialName, FactoryName, FactoryRawMaterialID, LocationID, UserID, IsActive, IsPotential, orderBy, orderDirection).Count();
                    var result = context.FactoryMng2_function_SearchFactory(FactoryID, FactoryOfficialName, FactoryName, FactoryRawMaterialID, LocationID, UserID, IsActive, IsPotential, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactorySearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //quick contact info
                    List<int> factoryIDs = new List<int>();
                    factoryIDs = data.Data.Select(s => s.FactoryID).ToList();
                    var expectedCapacity = context.FactoryMng2_FactoryExpectedCapacitySearch_View.Where(o => o.FactoryID.HasValue && factoryIDs.Contains(o.FactoryID.Value)).ToList();
                    foreach (var item in data.Data)
                    {
                        var x = expectedCapacity.Where(o => o.FactoryID == item.FactoryID).ToList();
                        item.FactoryExpectedCapacitySearches = AutoMapper.Mapper.Map<List<FactoryMng2_FactoryExpectedCapacitySearch_View>,List<DTO.FactoryExpectedCapacitySearch>>(x);
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Factory dtoFactory = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Factory>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryMng2Entities context = CreateContext())
                {
                    Factory dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Factory();
                        context.Factory.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Factory.FirstOrDefault(o => o.FactoryID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactory.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // process image
                        foreach (DTO.FactoryImage dtoImage in dtoFactory.FactoryImages)
                        {
                            if (dtoImage.HasChange)
                            {
                                dtoImage.FileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoImage.NewFile, dtoImage.FileUD);
                            }
                        }

                        // Process business card image
                        foreach (DTO.FactoryBusinessCard dtoCard in dtoFactory.FactoryBusinessCard)
                        {
                            if (dtoCard.FrontHasChange)
                            {
                                dtoCard.FrontFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCard.FrontNewFile, dtoCard.FrontFileUD);
                            }

                            if (dtoCard.BehindHasChange)
                            {
                                dtoCard.BehindFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCard.BehindNewFile, dtoCard.BehindFileUD);
                            }
                        }

                        // processing certificate file
                        foreach (DTO.FactoryCertificate dtoCertificate in dtoFactory.FactoryCertificates)
                        {
                            if (dtoCertificate.CertificateFileHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoCertificate.NewCertificateFile))
                                {
                                    fwFactory.RemoveImageFile(dtoCertificate.CertificateFile);
                                }
                                else
                                {
                                    dtoCertificate.CertificateFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCertificate.NewCertificateFile, dtoCertificate.CertificateFile);
                                }
                            }
                        }

                        // remove Technical Image
                        foreach (FactoryImage dbImage in context.FactoryImage.Local.Where(o => o.Factory == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbImage.FileUD))
                            {
                                fwFactory.RemoveImageFile(dbImage.FileUD);
                            }
                        }

                        // processing image
                        if (dtoFactory.LogoImage_HasChange)
                        {
                            dtoFactory.LogoImage = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoFactory.LogoImage_NewFile, dtoFactory.LogoImage);
                        }

                        if(dtoFactory.FactoryGalleries!= null)
                        {
                            // Pre-event update FactoryGallery
                            foreach (DTO.FactoryGalleryDTO dtoFactoryGallery in dtoFactory.FactoryGalleries.Where(o => o.FactoryGalleryHasChange))
                            {
                                dtoFactoryGallery.FactoryGalleryUD = fwFactory.CreateNoneImageFilePointer(this._tempFolder, dtoFactoryGallery.FactoryGalleryNewFile, dtoFactoryGallery.FactoryGalleryUD);
                            }
                        }

                        //foreach (DTO.FactoryDocumentDTO dtoDocumentFile in dtoFactory.factoryDocuments)
                        //{
                        //    if (dtoDocumentFile.FactoryDocumentHasChange)
                        //    {
                        //        if (string.IsNullOrEmpty(dtoDocumentFile.FactoryDocumentNewFile))
                        //        {
                        //            fwFactory.RemoveFile(dtoDocumentFile.FactoryDocumentFile);
                        //        }
                        //        else
                        //        {
                        //            dtoDocumentFile.FactoryDocumentFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDocumentFile.FactoryDocumentNewFile, dtoDocumentFile.FactoryDocumentFile, dtoDocumentFile.FriendlyName);
                        //        }
                        //    }
                        //}

                        converter.DTO2BD(dtoFactory, ref dbItem, userId);

                        //
                        //remove orphan item
                        //

                        // FactoryImage
                        context.FactoryImage.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryImage.Remove(o));

                        // FactoryBusinessCard
                        context.FactoryBusinessCard.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryBusinessCard.Remove(o));

                        // FactoryDirector
                        context.FactoryDirector.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryDirector.Remove(o));

                        // FactoryManager
                        context.FactoryManager.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryManager.Remove(o));

                        // FactoryPricing
                        context.FactoryPricing.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryPricing.Remove(o));

                        // FactoryResponsiblePerson
                        context.FactoryResponsiblePerson.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryResponsiblePerson.Remove(o));

                        // SampleTechnical
                        context.FactorySampleTechnical.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactorySampleTechnical.Remove(o));

                        // FactorySupplier
                        //context.FactoryRawMaterialSupplier.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryRawMaterialSupplier.Remove(o));
                        context.FactorySupplier.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactorySupplier.Remove(o));

                        // FactoryInHouseTest
                        context.FactoryInHouseTest.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryInHouseTest.Remove(o));

                        // FactoryCertificate
                        context.FactoryCertificate.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryCertificate.Remove(o));

                        //FactoryCapacity By Weeks
                        context.FactoryCapacity.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryCapacity.Remove(o));

                        //FactoryContactQuickInfo
                        context.FactoryContactQuickInfo.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryContactQuickInfo.Remove(o));

                        //FactoryProductGroup
                        context.FactoryProductGroup.Local.Where(o => o.Factory == null).ToList().ForEach(o => context.FactoryProductGroup.Remove(o));

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        // FactoryGallery for Factory(remove value Factory is null)
                        foreach (FactoryGallery dbFactoryGallery in context.FactoryGallery.Where(o => o.Factory == null).ToList())
                        {
                            fwFactory.RemoveImageFile(dbFactoryGallery.FactoryGalleryUD);
                            context.FactoryGallery.Remove(dbFactoryGallery);
                        }

                        context.SaveChanges();

                        //Update Permission Factory
                        if (id == 0)
                        {
                            context.Factory2Mng_function_AddFactoryPermissionNew(dbItem.FactoryID);
                            dtoItem = GetData(userId, dbItem.FactoryID, out notification).Data;
                        }
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

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {

            using (var context = CreateContext())
            {
                context.FactoryMng2_InsertDataForFactoryCaspacity_Function();
            }

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Factory();
            data.Data.FactoryImages = new List<DTO.FactoryImage>();
            data.Data.FactoryBusinessCard = new List<DTO.FactoryBusinessCard>();
            data.Data.FactoryCertificates = new List<DTO.FactoryCertificate>();
            data.Data.FactoryTurnovers = new List<DTO.FactoryTurnover>();
            data.Data.FactoryExpectedCapacities = new List<DTO.FactoryExpectedCapacity>();
            data.Data.factoryCapacityByWeeks = new List<DTO.FactoryCapacityByWeek>();
            data.Employees = new List<Support.DTO.Employee>();
            data.EmployeeDepartmentDTOs = new List<DTO.EmployeeDepartmentDTO>();
            data.FactoryRawMaterialSupplierList = new List<DTO.FactoryRawMaterialSupplier>();
            data.SupplierList = new List<DTO.Supplier>();
            data.Locations = new List<Support.DTO.FactoryLocation>();
            data.Data.AppointmentDTOs = new List<DTO.AppointmentDTO>();
            data.Data.weekInforRangeDTOs = new List<DTO.WeekInforRangeDTO>();
            data.Data.factoryDocuments = new List<DTO.FactoryDocumentDTO>();
            data.ProductGroupDTOs = new List<DTO.ProductGroupDTO>();
            data.Data.FactoryProductGroupDTOs = new List<DTO.FactoryProductGroupDTO>();
            data.UsersDTO = new List<DTO.UsersDTO>();
            //try to get data
            try
            {
                if (id > 0)
                {

                    //check permission on factory
                    if (!fwFactory.CanPerformAction(iRequesterID, "FactoryMng2", Library.DTO.ModuleAction.CanCreate))
                    {
                        if (fwFactory.CheckFactoryPermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                    }

                    using (FactoryMng2Entities context = CreateContext())
                    {
                        var factory = context.FactoryMng2_Factory_View.FirstOrDefault(o => o.FactoryID == id);
                        if (factory == null)
                        {
                            throw new Exception("Can not found the factory to edit");
                        }

                        data.Data = converter.DB2DTO_Factory(factory);

                        data.Data.factoryCapacityByWeeks = converter.DB2DTO_GetSeasonByWeeks(context.FactoryMng2_function_FactoryCapacityByWeek(id).ToList());
                    }

                }
                else
                {
                    // Set default status option = yes.
                    data.Data.IsActive = true;
                }

                data.Employees = supportFactory.GetEmployee().ToList();
                data.FactoryRawMaterialSupplierList = GetFactoryRawMaterialSupplierList();
                data.SupplierList = GetSupplierList();
                data.EmployeeDepartmentDTOs = GetEmployeeDepartmentDTOs();
                data.Locations = supportFactory.GetFactoryLocation();
                data.ProductGroupDTOs = GetProductGroupList();
                

                using
                    (var context = CreateContext())
                {
                    data.Data.weekInforRangeDTOs = converter.DB2DTO_WeeKInforRange(context.FactoryMng2_WeekInforRange_View.ToList());
                    data.UsersDTO = converter.DB2DTO_User2(context.SupportMng_User2_View.ToList());
                }

            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Employees = new List<Support.DTO.Employee>();
            data.FactoryRawMaterialSupplierList = new List<DTO.FactoryRawMaterialSupplier>();
            //data.SupplierList = new List<DTO.Supplier>();
            data.Locations = new List<Support.DTO.FactoryLocation>();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.Employees = supportFactory.GetEmployee().ToList();
                data.Locations = supportFactory.GetFactoryLocation();
                data.Factories = supportFactory.GetFactory(userId);
                data.FactoryRawMaterialSupplierList = GetFactoryRawMaterialSupplierList();
                //data.SupplierList = GetSupplierList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Factory();
            data.Employees = new List<Support.DTO.Employee>();
            data.Locations = new List<Support.DTO.FactoryLocation>();
            data.Data.AppointmentDTOs = new List<DTO.AppointmentDTO>();
            //try to get data
            try
            {
                if (id > 0)
                {
                    using (FactoryMng2Entities context = CreateContext())
                    {
                        var factory = context.FactoryMng2_Factory_View.FirstOrDefault(o => o.FactoryID == id);
                        if (factory == null)
                        {
                            throw new Exception("Can not found the factory to edit");
                        }

                        data.Data = converter.DB2DTO_Factory(factory);
                    }
                }
                data.Employees = supportFactory.GetEmployee().ToList();
                data.Locations = supportFactory.GetFactoryLocation();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        //Get FactoryRawMaterialSupplier List
        public List<DTO.FactoryRawMaterialSupplier> GetFactoryRawMaterialSupplierList()
        {
            List<DTO.FactoryRawMaterialSupplier> result = new List<DTO.FactoryRawMaterialSupplier>();
            try
            {
                using (FactoryMng2Entities context = this.CreateContext())
                {
                    //result = converter.DB2DTO_FactoryRawMaterialSupplier(context.FactoryMng2_List_FactoryRawMaterialSupplier.ToList());
                    result = converter.DB2DTO_FactoryRawMaterialSupplier(context.FactoryMng2_List_FactoryRawMaterialSupplier.ToList());
                }
            }
            catch { }

            return result;
        }

        public List<DTO.Supplier> GetSupplierList()
        {
            List<DTO.Supplier> result = new List<DTO.Supplier>();
            try
            {
                using (FactoryMng2Entities context = this.CreateContext())
                {
                    //result = converter.DB2DTO_FactoryRawMaterialSupplier(context.FactoryMng2_List_FactoryRawMaterialSupplier.ToList());
                    result = converter.DB2DTO_Supplier(context.FactoryMng2_List_Supplier.ToList());
                }
            }
            catch { }

            return result;
        }
        public List<DTO.EmployeeDepartmentDTO> GetEmployeeDepartmentDTOs()
        {
            List<DTO.EmployeeDepartmentDTO> result = new List<DTO.EmployeeDepartmentDTO>();
            try
            {
                using (FactoryMng2Entities context = this.CreateContext())
                {
                    //result = converter.DB2DTO_FactoryRawMaterialSupplier(context.FactoryMng2_List_FactoryRawMaterialSupplier.ToList());
                    result = converter.DB2DTO_EmployeeDepartment(context.FactoryMng2_Employee_View.ToList());
                }
            }
            catch { }

            return result;
        }
        public List<DTO.ProductGroupDTO> GetProductGroupList()
        {
            List<DTO.ProductGroupDTO> result = new List<DTO.ProductGroupDTO>();
            try
            {
                using (FactoryMng2Entities context = this.CreateContext())
                {                  
                    result = converter.DB2DTO_ProductGroup(context.SupportMng_ProductGroup_View.ToList());
                }
            }
            catch { }

            return result;
        }
        public List<DTO.FactoryOrderTurnover> GetFactoryOrderTurnover(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            List<DTO.FactoryOrderTurnover> data = new List<DTO.FactoryOrderTurnover>();

            totalRows = 0;

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                // Get filter condition such as: FactoryID, Season, ClientUD, FactoryOrderUD, TrackingStatusNM.
                int? factoryID = filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()) ? (int?)Convert.ToInt32(filters["FactoryID"].ToString()) : null;
                string season = filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()) ? filters["Season"].ToString() : null;
                string clientUD = filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString()) ? filters["ClientUD"].ToString() : null;
                string factoryOrderUD = filters.ContainsKey("FactoryOrderUD") && filters["FactoryOrderUD"] != null && !string.IsNullOrEmpty(filters["FactoryOrderUD"].ToString()) ? filters["FactoryOrderUD"].ToString() : null;
                string trackingStatusNM = filters.ContainsKey("TrackingStatusNM") && filters["TrackingStatusNM"] != null && !string.IsNullOrEmpty(filters["TrackingStatusNM"].ToString()) ? filters["TrackingStatusNM"].ToString() : null;

                using (var context = CreateContext())
                {
                    totalRows = context.FactoryMng2_function_GetFactoryOrderTurnover(factoryID, season, clientUD, factoryOrderUD, trackingStatusNM, orderBy, orderDirection).Count();
                    var dbItem = context.FactoryMng2_function_GetFactoryOrderTurnover(factoryID, season, clientUD, factoryOrderUD, trackingStatusNM, orderBy, orderDirection);
                    //data = converter.DB2DTO_FactoryOrderTurnover(dbItem.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data = converter.DB2DTO_FactoryOrderTurnover(dbItem.ToList());
                    // Return total rows.
                    filters["TotalRows"] = totalRows;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public string ExportExcelFactory(Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            FactoryListObject ds = new FactoryListObject();
            FactoryListCapacityObject dsCap = new FactoryListCapacityObject();

            int? FactoryID = null;
            int UserID = -1;
            string FactoryOfficialName = null;
            string FactoryName = null;
            //string SupplierIDs = "";
            int? FactoryRawMaterialID = null;
            int? LocationID = null;
            bool? IsActive = null;
            bool? IsPotential = null;
            int? Type = null;

            if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
            {
                FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
            }
            if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }
            if (filters.ContainsKey("FactoryOfficialName") && !string.IsNullOrEmpty(filters["FactoryOfficialName"].ToString()))
            {
                FactoryOfficialName = filters["FactoryOfficialName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryName") && !string.IsNullOrEmpty(filters["FactoryName"].ToString()))
            {
                FactoryName = filters["FactoryName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryRawMaterialID") && !string.IsNullOrEmpty(filters["FactoryRawMaterialID"].ToString()))
            {
                FactoryRawMaterialID = Convert.ToInt32(filters["FactoryRawMaterialID"]);
            }
            if (filters.ContainsKey("LocationID") && !string.IsNullOrEmpty(filters["LocationID"].ToString()))
            {
                LocationID = Convert.ToInt32(filters["LocationID"]);
            }
            if (filters.ContainsKey("IsActive") && filters["IsActive"] != null && !string.IsNullOrEmpty(filters["IsActive"].ToString()))
            {
                IsActive = (filters["IsActive"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("IsPotential") && filters["IsPotential"] != null && !string.IsNullOrEmpty(filters["IsPotential"].ToString()))
            {
                IsPotential = (filters["IsPotential"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("Type") && !string.IsNullOrEmpty(filters["Type"].ToString()))
            {
                Type = Convert.ToInt32(filters["Type"].ToString());
            }

            if (Type == 1)
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryMng2_function_ExportToExcelList", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryID", FactoryID);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryOfficialName", FactoryOfficialName);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryName", FactoryName);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialID", FactoryRawMaterialID);
                adap.SelectCommand.Parameters.AddWithValue("@LocationID", LocationID);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                adap.SelectCommand.Parameters.AddWithValue("@IsActive", IsActive);
                adap.SelectCommand.Parameters.AddWithValue("@IsPotential", IsPotential);
                adap.TableMappings.Add("Table", "FactoryExportToExcelList");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListFactory");
            }
            else
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryMng2_function_ExportToExcelListCapacity", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryID", FactoryID);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryOfficialName", FactoryOfficialName);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryName", FactoryName);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialID", FactoryRawMaterialID);
                adap.SelectCommand.Parameters.AddWithValue("@LocationID", LocationID);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                adap.SelectCommand.Parameters.AddWithValue("@IsActive", IsActive);
                adap.SelectCommand.Parameters.AddWithValue("@IsPotential", IsPotential);
                adap.TableMappings.Add("Table", "FactoryExportToExcelListCap");
                adap.Fill(dsCap);

                return Library.Helper.CreateReportFileWithEPPlus2(dsCap, "ListFactoryCapacity");
            }
        }

        #region Get person in charge
        public List<DTO.PersonInChargeDTO> GetPersonInCharge(int userID, Hashtable filters, out int totalRows, out Notification notification)
        {
            List<DTO.PersonInChargeDTO> data = new List<DTO.PersonInChargeDTO>();
            totalRows = 0;
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                int? supplierID = filters.ContainsKey("supplierID") && filters["supplierID"] != null && !string.IsNullOrEmpty(filters["supplierID"].ToString()) ? (int?)Convert.ToInt32(filters["supplierID"].ToString()) : null;
                using (var context = CreateContext())
                {
                    totalRows = context.Factory2Mng_function_getEmployeeBySupplier(supplierID).Count();
                    var dbItem = context.Factory2Mng_function_getEmployeeBySupplier(supplierID);
                    data = converter.DB2DTO_PersonInCharge(dbItem.ToList());
                    filters["TotalRows"] = totalRows;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
        #endregion
    }
}