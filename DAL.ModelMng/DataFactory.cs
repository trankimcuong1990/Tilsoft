using DTO.ModelMng;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.ModelMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ModelMng.SearchFormData, DTO.ModelMng.EditFormData, DTO.ModelMng.Model>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private ModelMngEntities CreateContext()
        {
            ModelMngEntities context = new ModelMngEntities(DALBase.Helper.CreateEntityConnectionString("ModelMngModel"));
            context.Database.CommandTimeout = 300;
            return context;
        }

        public override DTO.ModelMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ModelMng.SearchFormData data = new DTO.ModelMng.SearchFormData();
            data.Data = new List<DTO.ModelMng.ModelSearchResult>();
            totalRows = 0;

            string ModelUD = null;
            string ModelNM = null;
            string UpdatorName = null;
            string Season = null;
            string FactoryUD = null;
            bool? IsStandard = null;
            bool? HasCushion = null;
            bool? HasFrameMaterial = null;
            bool? HasSubMaterial = null;
            int? ProductTypeID = null;
            int? ProductGroupID = null;
            int? ManufacturerCountryID = null;
            int? ModelStatusID = null;
            int? ProductCategoryID = null;
            int? CatalogPage = null;
            bool? IsExcludedInNotification = null;
            bool? IsArchived = false;
            int UserID = -1;

            if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }
            if (filters.ContainsKey("ModelUD"))
            {
                ModelUD = filters["ModelUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD"))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ModelNM"))
            {
                ModelNM = filters["ModelNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("UpdatorName"))
            {
                UpdatorName = filters["UpdatorName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsStandard") && filters["IsStandard"] != null && !string.IsNullOrEmpty(filters["IsStandard"].ToString()))
            {
                IsStandard = (filters["IsStandard"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("ProductTypeID") && filters["ProductTypeID"] != null && !string.IsNullOrEmpty(filters["ProductTypeID"].ToString()))
            {
                ProductTypeID = Convert.ToInt32(filters["ProductTypeID"]);
            }
            if (filters.ContainsKey("ProductGroupID") && filters["ProductGroupID"] != null && !string.IsNullOrEmpty(filters["ProductGroupID"].ToString()))
            {
                ProductGroupID = Convert.ToInt32(filters["ProductGroupID"]);
            }
            if (filters.ContainsKey("HasCushion") && filters["HasCushion"] != null && !string.IsNullOrEmpty(filters["HasCushion"].ToString()))
            {
                HasCushion = (filters["HasCushion"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("HasFrameMaterial") && filters["HasFrameMaterial"] != null && !string.IsNullOrEmpty(filters["HasFrameMaterial"].ToString()))
            {
                HasFrameMaterial = (filters["HasFrameMaterial"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("HasSubMaterial") && filters["HasSubMaterial"] != null && !string.IsNullOrEmpty(filters["HasSubMaterial"].ToString()))
            {
                HasSubMaterial = (filters["HasSubMaterial"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("ManufacturerCountryID") && filters["ManufacturerCountryID"] != null && !string.IsNullOrEmpty(filters["ManufacturerCountryID"].ToString()))
            {
                ManufacturerCountryID = Convert.ToInt32(filters["ManufacturerCountryID"]);
            }
            if (filters.ContainsKey("ModelStatusID") && filters["ModelStatusID"] != null && !string.IsNullOrEmpty(filters["ModelStatusID"].ToString()))
            {
                ModelStatusID = Convert.ToInt32(filters["ModelStatusID"]);
            }
            if (filters.ContainsKey("ProductCategoryID") && filters["ProductCategoryID"] != null && !string.IsNullOrEmpty(filters["ProductCategoryID"].ToString()))
            {
                ProductCategoryID = Convert.ToInt32(filters["ProductCategoryID"]);
            }
            if (filters.ContainsKey("CatalogPage") && filters["CatalogPage"] != null && !string.IsNullOrEmpty(filters["CatalogPage"].ToString()))
            {
                CatalogPage = Convert.ToInt32(filters["CatalogPage"]);
            }
            if (filters.ContainsKey("IsExcludedInNotification") && filters["IsExcludedInNotification"] != null && !string.IsNullOrEmpty(filters["IsExcludedInNotification"].ToString()))
            {
                IsExcludedInNotification = (filters["IsExcludedInNotification"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("IsArchived") && filters["IsArchived"] != null && !string.IsNullOrEmpty(filters["IsArchived"].ToString()))
            {
                IsArchived = (filters["IsArchived"].ToString() == "true") ? true : false;
            }
            //try to get data
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    // add fix price for last, current and next season if needed
                    try
                    {
                        context.ModelMng_function_AddFixPrice();
                    }
                    catch { }

                    totalRows = context.ModelMng_function_SearchModel(ModelUD, ModelNM, UpdatorName, IsStandard, Season, ProductTypeID, ProductGroupID, HasCushion, HasFrameMaterial, HasSubMaterial, ManufacturerCountryID, ModelStatusID, ProductCategoryID, FactoryUD, CatalogPage, IsExcludedInNotification, UserID, IsArchived, orderBy, orderDirection).Count();
                    var result = context.ModelMng_function_SearchModel(ModelUD, ModelNM, UpdatorName, IsStandard, Season, ProductTypeID, ProductGroupID, HasCushion, HasFrameMaterial, HasSubMaterial, ManufacturerCountryID, ModelStatusID, ProductCategoryID, FactoryUD, CatalogPage, IsExcludedInNotification, UserID, IsArchived, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ModelSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public List<DTO.ModelMng.CheckListGroupDTO> QuyeryCheckList(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ModelMng.CheckListGroupDTO> data = new List<DTO.ModelMng.CheckListGroupDTO>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.CheckListMng_CheckListGroup_View.Where(o => o.CheckListGroupUD.Contains(query) || o.CheckListGroupNM.Contains(query)).ToList();
                    data = converter.DB2DTO_ModelCheckListGroup(result).ToList();
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }
        public override DTO.ModelMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.ModelMng.EditFormData data = new DTO.ModelMng.EditFormData();

            //data.Data = new DTO.ModelMng.Model();
            //data.Data.CushionOptions = new List<DTO.ModelMng.CushionOption>();
            //data.Data.Pieces = new List<DTO.ModelMng.Piece>();
            //data.Data.Spareparts = new List<DTO.ModelMng.Sparepart>();
            //data.Data.PackagingMethodOptions = new List<DTO.ModelMng.PackagingMethodOption>();
            //data.Data.SubMaterialOptions = new List<DTO.ModelMng.SubMaterialOption>();
            //data.Data.ImageGalleries = new List<DTO.ModelMng.ImageGallery>();
            //data.Data.TestReports = new List<DTO.ModelMng.TestReport>();

            //data.Seasons = new List<DTO.Support.Season>();
            //data.PackagingMethods = new List<DTO.Support.PackagingMethod>();
            //data.ProductTypes = new List<DTO.Support.ProductType>();
            //data.ProductGroups = new List<DTO.Support.ProductGroup>();
            //data.ManufacturerCountries = new List<DTO.Support.ManufacturerCountry>();
            //data.ModelStatuses = new List<DTO.Support.ModelStatus>();
            //data.ProductCategories = new List<DTO.Support.ProductCategory>();
            //data.Factories = new List<DTO.Support.Factory>();
            //data.GalleryItemTypes = new List<DTO.Support.GalleryItemType>();

            ////try to get data
            //try
            //{
            //    if (id > 0 && fwFactory.CheckModelPermission(userId, id) == 0)
            //    {
            //        throw new Exception("Current user don't have access permission for the selected loading plan data");
            //    }

            //    using (ModelMngEntities context = CreateContext())
            //    {
            //        if (id > 0)
            //        {
            //            data.Data = converter.DB2DTO_Model(context.ModelMng_Model_View
            //                .Include("ModelMng_ModelPiece_View")
            //                .Include("ModelMng_ModelSparepart_View")
            //                .Include("ModelMng_ModelCushionOption_View")
            //                .Include("ModelMng_ModelSubMaterialOption_View")
            //                .Include("ModelMng_ModelPackagingMethodOption_View")
            //                .Include("ModelMng_ImageGallery_View.ModelMng_ImageGalleryVersion_View")
            //                .FirstOrDefault(o => o.ModelID == id));
            //        }

            //        data.ProductTypes = supportFactory.GetProductType().ToList();
            //        data.PackagingMethods = supportFactory.GetPackagingMethod().ToList();
            //        data.Seasons = supportFactory.GetSeason().ToList();
            //        data.ProductGroups = supportFactory.GetProductGroup().ToList();
            //        data.ManufacturerCountries = supportFactory.GetManufacturerCountry().ToList();
            //        data.ModelStatuses = supportFactory.GetModelStatus().ToList();
            //        data.ProductCategories = supportFactory.GetProductCategories().ToList();
            //        data.Factories = supportFactory.GetFactory().ToList();
            //        data.GalleryItemTypes = supportFactory.GetGalleryItemType().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    Model dbItem = context.Model.FirstOrDefault(o => o.ModelID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Model not found!";
                        return false;
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(dbItem.ImageFile))
                        //{
                        //    // remove image
                        //    fwFactory.RemoveImageFile(dbItem.ImageFile);
                        //}
                        //context.Model.Remove(dbItem);
                        dbItem.IsArchived = true;
                        context.SaveChanges();
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

        public bool Restore(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    Model dbItem = context.Model.FirstOrDefault(o => o.ModelID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Model not found!";
                        return false;
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(dbItem.ImageFile))
                        //{
                        //    // remove image
                        //    fwFactory.RemoveImageFile(dbItem.ImageFile);
                        //}
                        //context.Model.Remove(dbItem);
                        dbItem.IsArchived = false;
                        context.SaveChanges();

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddModelDefaultOption(id); // table lockx and also check if item is available on sql server side

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

        public override bool UpdateData(int id, ref DTO.ModelMng.Model dtoItem, out Library.DTO.Notification notification)
        {
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //int number;
            //string indexName;
            //List<Module.Framework.DTO.EntityTracking> newlyCreateDataTracking;

            //try
            //{
            //    using (ModelMngEntities context = CreateContext())
            //    {
            //        Model dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new Model();
            //            context.Model.Add(dbItem);
            //        }
            //        else
            //        {
            //            //check default options
            //            CheckDefaultOption(dtoItem);

            //            //get data
            //            dbItem = context.Model.FirstOrDefault(o => o.ModelID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Model not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            // check concurrency
            //            if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
            //            {
            //                throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
            //            }

            //            // processing image before update the model
            //            foreach (DTO.ModelMng.ImageGallery dtoImageGallery in dtoItem.ImageGalleries.Where(o => o.FileHasChange))
            //            {
            //                if (dtoImageGallery.GalleryItemTypeID == 3)
            //                {
            //                    dtoImageGallery.FileUD = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoImageGallery.NewFile, dtoImageGallery.FileUD);
            //                }
            //                else
            //                {
            //                    dtoImageGallery.FileUD = fwFactory.CreateFilePointer(this._TempFolder, dtoImageGallery.NewFile, dtoImageGallery.FileUD);
            //                }
            //            }

            //            //process report
            //            foreach (DTO.ModelMng.TestReport dtoReport in dtoItem.TestReports.Where(o => o.ScanHasChange))
            //            {
            //                dtoReport.ScanFile = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoReport.ScanNewFile, dtoReport.ScanFile);
            //            }

            //            // process model issue track
            //            foreach (var dtoIssueTrack in dtoItem.ModelIssueTracks.Where(o => o.QualityReport_HasChange))
            //            {
            //                dtoIssueTrack.QualityReport = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoIssueTrack.QualityReport_NewFile, dtoIssueTrack.QualityReport);
            //            }

            //            //read dto to db
            //            converter.DTO2DB(dtoItem, this, ref dbItem);

            //            // convert data to db and remove orphan
            //            context.ModelSubMaterialOption.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelSubMaterialOption.Remove(o));
            //            context.ModelCushionOption.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelCushionOption.Remove(o));
            //            context.ModelPiece.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelPiece.Remove(o));
            //            context.ModelPackagingMethodOption.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelPackagingMethodOption.Remove(o));
            //            context.ModelSparepart.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelSparepart.Remove(o));
            //            context.ImageGalleryVersion.Local.Where(o => o.ImageGallery == null).ToList().ForEach(o => context.ImageGalleryVersion.Remove(o));
            //            context.TestReport.Local.Where(o => o.Model == null).ToList().ForEach(o => context.TestReport.Remove(o));
            //            context.ModelIssueTrack.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelIssueTrack.Remove(o));

            //            context.ModelFixPriceConfiguration.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelFixPriceConfiguration.Remove(o));
            //            context.ModelPriceConfiguration.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelPriceConfiguration.Remove(o));
            //            context.ModelPriceConfigurationDetail.Local.Where(o => o.ModelPriceConfiguration == null).ToList().ForEach(o => context.ModelPriceConfigurationDetail.Remove(o));

            //            foreach (ImageGallery dbImageGallery in context.ImageGallery.Local.Where(o => o.Model == null).ToList())
            //            {
            //                fwFactory.RemoveImageFile(dbImageGallery.FileUD);
            //                context.ImageGallery.Remove(dbImageGallery);
            //            }

            //            // remove Test Report
            //            foreach (TestReport dbReport in context.TestReport.Local.Where(o => o.Model == null).ToList())
            //            {
            //                if (!string.IsNullOrEmpty(dbReport.ScanFile))
            //                {
            //                    fwFactory.RemoveImageFile(dbReport.ScanFile);
            //                }
            //            }

            //            foreach (ModelIssueTrack dbIssueTrack in context.ModelIssueTrack.Local.Where(o => o.Model == null).ToList())
            //            {
            //                if (!string.IsNullOrEmpty(dbIssueTrack.QualityReport))
            //                {
            //                    fwFactory.RemoveImageFile(dbIssueTrack.QualityReport);
            //                }
            //            }

            //            if (id == 0)
            //            {
            //                // generate latest code - table locks required
            //                using (DbContextTransaction scope = context.Database.BeginTransaction())
            //                {
            //                    context.Database.ExecuteSqlCommand("SELECT TOP 1 * FROM Model WITH (TABLOCKX, HOLDLOCK)");

            //                    try
            //                    {
            //                        Model lastModel = context.Model.OrderByDescending(o => o.ModelID).FirstOrDefault();
            //                        string lastModelUD = Library.Common.Helper.getNextCode(lastModel.ModelUD);
            //                        while (context.Model.FirstOrDefault(o => o.ModelUD == lastModelUD) != null)
            //                        {
            //                            lastModelUD = Library.Common.Helper.getNextCode(lastModelUD);
            //                        }
            //                        dbItem.ModelUD = lastModelUD;

            //                        // track data                                    
            //                        fwFactory.TrackingDataChange(context, "ModelMng", dtoItem.UpdatedBy.Value, out newlyCreateDataTracking);

            //                        context.SaveChanges();

            //                        // update track data
            //                        fwFactory.UpdateDataTracking(context, newlyCreateDataTracking);
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

            //                // Send email notification
            //                string emailSubject = "NEW MODEL HAS CREATED: Model Code " + dbItem.ModelUD;
            //                string emailBody = "";
            //                emailBody += "CODE: " + dbItem.ModelUD + "<br/>";
            //                emailBody += "NAME: " + dbItem.ModelNM + "<br/>";
            //                emailBody += "RANGE NAME: " + dbItem.RangeName + "<br/>";
            //                emailBody += "SEASON: " + dbItem.Season + "<br/>";

            //                SendNotification(dbItem, emailSubject, emailBody);
            //            }

            //            // processing image - will be remove soon
            //            if (dtoItem.ImageFile_HasChange)
            //            {
            //                dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
            //            }

            //            // track data
            //            fwFactory.TrackingDataChange(context, "ModelMng", dtoItem.UpdatedBy.Value, out newlyCreateDataTracking);
            //            context.SaveChanges();
            //            // update track data
            //            fwFactory.UpdateDataTracking(context, newlyCreateDataTracking);

            //            dtoItem = GetData(dtoItem.UpdatedBy.Value, dbItem.ModelID, out notification).Data;
            //            return true;
            //        }
            //    }
            //}
            //catch (System.Data.DataException dEx)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
            //    if (number == 2601 && !string.IsNullOrEmpty(indexName))
            //    {
            //        switch (indexName)
            //        {
            //            case "ModelMaterialOptionUnique":
            //                notification.Message = "Duplicate material option";
            //                break;

            //            case "ModelFrameMaterialUnique":
            //                notification.Message = "Duplicate frame material option";
            //                break;

            //            case "ModelSubMaterialUnique":
            //                notification.Message = "Duplicate sub material option";
            //                break;

            //            case "ModelPackagingMethodUnique":
            //                notification.Message = "Duplicate packaging method option";
            //                break;

            //            case "ModelCushionUnique":
            //                notification.Message = "Duplicate cushion specification option";
            //                break;

            //            case "ModelCushionColorUnique":
            //                notification.Message = "Duplicate cushion color option";
            //                break;
            //        }
            //    }
            //    else if (number == 2627)
            //    {
            //        switch (indexName)
            //        {
            //            case "ModelUDUnique":
            //                notification.Message = "Duplicate model code";
            //                break;

            //            default:
            //                notification.Message = dEx.Message;
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        notification.Message = dEx.Message;
            //    }

            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public bool Approve(int userId, int id, int packagingID, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            try
            {
                using (var context = CreateContext())
                {
                    ModelDefaultOption dbItem = context.ModelDefaultOption.FirstOrDefault(o => o.ModelDefaultOptionID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    else
                    {
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = userId;
                        dbItem.ConfirmedDate = DateTime.Now;
                        dbItem.PackagingMethodID = packagingID;
                        context.SaveChanges();

                        context.ModelMng_function_ApplyDefaultOptionToPiece(id);
                        context.SaveChanges();
                    }

                    // add item to quotation if needed
                    context.FW_Quotation_function_AddModelDefaultOption(id); // table lockx and also check if item is available on sql server side

                    return true;
                }
            }

            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            try
            {
                using (var context = CreateContext())
                {
                    ModelDefaultOption dbItem = context.ModelDefaultOption.FirstOrDefault(o => o.ModelDefaultOptionID == id);

                    ModelPriceStatus modelPriceStatus = context.ModelPriceStatus.FirstOrDefault(ml => (ml.ModelID == dbItem.ModelID) && (ml.Season == dbItem.Season));

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    //else if (((modelPriceStatus != null) && (modelPriceStatus.IsConfirmed is true)))
                    //{
                    //    notification.Type = NotificationType.Error;
                    //    notification.Message = "Price is confirmeds!";
                    //    return false;
                    //}
                    if (modelPriceStatus != null)
                    {
                        modelPriceStatus.IsConfirmed = null;
                        modelPriceStatus.ConfirmedBy = null;
                        modelPriceStatus.ConfirmedDate = null;
                    }
                    dbItem.IsConfirmed = false;
                    dbItem.ConfirmedBy = null;
                    dbItem.ConfirmedDate = null;
                    context.SaveChanges();

                    // add item to quotation if needed
                    context.FW_Quotation_function_AddModelDefaultOption(id); // table lockx and also check if item is available on sql server side

                    return true;
                }
            }

            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }        
        public override bool Reset(int id, ref DTO.ModelMng.Model dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ModelMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ModelMng.SearchFilterData data = new DTO.ModelMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();
            data.StandardValues = new List<DTO.Support.YesNo>();
            data.ProductTypes = new List<DTO.Support.ProductType>();
            data.ProductGroups = new List<DTO.Support.ProductGroup>();
            data.ManufacturerCountries = new List<DTO.Support.ManufacturerCountry>();
            data.ModelStatuses = new List<DTO.Support.ModelStatus>();
            data.ProductCategories = new List<DTO.Support.ProductCategory>();
            data.YesNoValues = new List<DTO.Support.YesNo>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.StandardValues = supportFactory.GetYesNo().ToList();
                data.ProductTypes = supportFactory.GetProductType().ToList();
                data.ProductGroups = supportFactory.GetProductGroup().ToList();
                data.ManufacturerCountries = supportFactory.GetManufacturerCountry().ToList();
                data.ModelStatuses = supportFactory.GetModelStatus().ToList();
                data.ProductCategories = supportFactory.GetProductCategories().ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        // check data constrain
        public bool IsOKToDeleteSubMaterial(int subMaterialOptionID, int modelID)
        {
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    if (context.ModelMng_Product_View.Count(o => o.SubMaterialOptionID == subMaterialOptionID && o.ModelID == modelID) > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool IsOKToDeleteCushion(int cushionID, int modelID)
        {
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    if (context.ModelMng_Product_View.Count(o => o.CushionID == cushionID && o.ModelID == modelID) > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool IsOKToDeletePackagingMethod(int packagingMethodID, int modelID)
        {
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    if (context.ModelMng_Product_View.Count(o => o.PackagingMethodID == packagingMethodID && o.ModelID == modelID) > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public DTO.ModelMng.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Notification() { Type = Library.DTO.NotificationType.Success };
            EditFormData data = new EditFormData();

            data.Data = new DTO.ModelMng.Model();
            data.Data.CushionOptions = new List<CushionOption>();
            data.Data.Pieces = new List<Piece>();
            data.Data.Spareparts = new List<Sparepart>();
            data.Data.PackagingMethodOptions = new List<DTO.ModelMng.PackagingMethodOption>();
            data.Data.SubMaterialOptions = new List<DTO.ModelMng.SubMaterialOption>();
            data.Data.ImageGalleries = new List<DTO.ModelMng.ImageGallery>();
            data.Data.TestReports = new List<DTO.ModelMng.TestReport>();
            data.Data.Tdfiles = new List<DTO.ModelMng.tdFile>();
            data.Data.AIFiles = new List<DTO.ModelMng.AIFile>();
            data.Data.ModelPriceConfigurations = new List<DTO.ModelMng.ModelPriceConfiguration>();
            data.Data.ModelPurchasingPriceConfigurationDTOs = new List<DTO.ModelMng.ModelPurchasingPriceConfigurationDTO>();
            data.Data.ModelFixPriceConfigurations = new List<DTO.ModelMng.ModelFixPriceConfiguration>();
            data.Data.ModelPurchasingFixPriceConfigurationDTOs = new List<DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO>();
            data.Data.ModelPriceStatusDTOs = new List<ModelPriceStatusDTO>();
            data.Data.ModelPurchasingPriceStatusDTOs = new List<ModelPurchasingPriceStatusDTO>();
            //data.Data.sampleProductViews = new List<DTO.ModelMng.SampleProductView>();
            //data.Data.productsViews = new List<DTO.ModelMng.productsView>();
            data.Data.ModelDefaultFactories = new List<DTO.ModelMng.ModelDefaultFactory>();
            data.Data.ModelDefaultOptionDTOs = new List<ModelDefaultOptionDTO>();
            data.Seasons = new List<DTO.Support.Season>();
            data.PackagingMethods = new List<DTO.Support.PackagingMethod>();
            data.ProductTypes = new List<DTO.Support.ProductType>();
            data.ProductGroups = new List<DTO.Support.ProductGroup>();
            data.ManufacturerCountries = new List<DTO.Support.ManufacturerCountry>();
            data.ModelStatuses = new List<DTO.Support.ModelStatus>();
            data.ProductCategories = new List<DTO.Support.ProductCategory>();
            data.Factories = new List<DTO.Support.Factory>();
            data.GalleryItemTypes = new List<DTO.Support.GalleryItemType>();
            data.ProductElements = new List<DTO.Support.ProductElement>();
            data.ProductElementOptions = new List<DTO.ModelMng.ProductElementOption>();
            data.MaterialTypes = new List<DTO.Support.MaterialType>();
            data.ModelPriceConfigurationDefault = new List<DTO.ModelMng.ModelPriceConfiguration>();
            data.FactoryDTOs = new List<FactoryDTO>();
            data.Data.ModelCheckListGroupDTO = new List<DTO.ModelMng.ModelCheckListGroupDTO>();
            data.CheckListGroupDTO = new List<DTO.ModelMng.CheckListGroupDTO>();
            data.SupportQCTrackingStatus = new List<DTO.ModelMng.QCTrackingStatus>()
            {
                new QCTrackingStatus() {TrackingStatusID=1, TrackingStatusUD="Finished", TrackingStatusNM="Finished" },
                new QCTrackingStatus() {TrackingStatusID=2, TrackingStatusUD="On Process", TrackingStatusNM="On Process" },
                new QCTrackingStatus() {TrackingStatusID=3, TrackingStatusUD="Follow Up", TrackingStatusNM="Follow Up" }
            };

            data.SupportQCTrackingType = new List<DTO.ModelMng.QCTrackingType>()
            {
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=1, TrackingTypeUD="Weakness", TrackingTypeNM="Weakness" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=2, TrackingTypeUD="Client complaint", TrackingTypeNM="Client complaint" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=3, TrackingTypeUD="Testing failure", TrackingTypeNM="Testing failure" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=4, TrackingTypeUD="Note in mass production", TrackingTypeNM="Note in mass production" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=5, TrackingTypeUD="Improvement", TrackingTypeNM="Improvement" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=6, TrackingTypeUD="Other", TrackingTypeNM="Other" }
            };
            data.ClientSpecialPackagingMethods = new List<ClientSpecialPackagingMethod>();
            data.Clients = new List<DTO.Support.Client>();
            data.ConfigPriceDTOs = new List<ConfigPriceDTO>();

            //try to get data
            try
            {
                if (id > 0 && fwFactory.IsInternalUser(userId) == 0 && fwFactory.CheckModelPermission(userId, id) == 0) // only check permission if user office = 4 (factory office)
                {
                    throw new Exception("Current user don't have access permission for the selected model data");
                }

                using (ModelMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        //data.Data = converter.DB2DTO_Model(context.ModelMng_Model_View
                        //    .Include("ModelMng_ModelPiece_View")
                        //    .Include("ModelMng_ModelSparepart_View")
                        //    .Include("ModelMng_ModelCushionOption_View")
                        //    .Include("ModelMng_ModelSubMaterialOption_View")
                        //    .Include("ModelMng_ModelPackagingMethodOption_View")
                        //    .Include("ModelMng_ImageGallery_View.ModelMng_ImageGalleryVersion_View")
                        //    .Include("ModelMng_ModelIssueTrack_View")
                        //    .Include("ModelMng_ModelFixPriceConfiguration_View")
                        //    .Include("ModelMng_ModelPriceConfiguration_View")
                        //    .Include("ModelMng_ModelPriceConfiguration_View.ModelMng_ModelPriceConfigurationDetail_View")
                        //    .Include("ModelMng_ModelPurchasingFixPriceConfiguration_View")
                        //    .Include("ModelMng_ModelPurchasingPriceConfiguration_View")
                        //    .Include("ModelMng_ModelPurchasingPriceConfiguration_View.ModelMng_ModelPurchasingPriceConfigurationDetail_View")
                        //    //.Include("ModelMng_SampleProduct_View")
                        //    //.Include("ModelMng_Product2_view")
                        //    .Include("ModelDefaultOptionMng_ModelDefaultOption_View")
                        //    .FirstOrDefault(o => o.ModelID == id));

                        var temp = context.ModelMng_function_Model(id).FirstOrDefault();

                        temp.ModelMng_ModelPiece_View = context.ModelMng_ModelPiece_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelSparepart_View = context.ModelMng_ModelSparepart_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelCushionOption_View = context.ModelMng_ModelCushionOption_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelSubMaterialOption_View = context.ModelMng_ModelSubMaterialOption_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelPackagingMethodOption_View = context.ModelMng_ModelPackagingMethodOption_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ImageGallery_View = context.ModelMng_ImageGallery_View.Where(s => s.ModelID == id).ToList();
                        foreach (ModelMng_ImageGallery_View itemModelMng_ImageGallery_View in temp.ModelMng_ImageGallery_View)
                        {
                            itemModelMng_ImageGallery_View.ModelMng_ImageGalleryVersion_View = context.ModelMng_ImageGalleryVersion_View
                                .Where(s => s.ImageGalleryID == itemModelMng_ImageGallery_View.ImageGalleryID).ToList();
                        }
                        temp.ModelMng_ModelIssueTrack_View = context.ModelMng_ModelIssueTrack_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelFixPriceConfiguration_View = context.ModelMng_ModelFixPriceConfiguration_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelPriceConfiguration_View = context.ModelMng_ModelPriceConfiguration_View.Where(s => s.ModelID == id).ToList();
                        foreach (ModelMng_ModelPriceConfiguration_View itemModelMng_ModelPriceConfiguration_View in temp.ModelMng_ModelPriceConfiguration_View)
                        {
                            itemModelMng_ModelPriceConfiguration_View.ModelMng_ModelPriceConfigurationDetail_View = context.ModelMng_ModelPriceConfigurationDetail_View
                                .Where(s => s.ModelPriceConfigurationID == itemModelMng_ModelPriceConfiguration_View.ModelPriceConfigurationID).ToList();
                        }

                        temp.ModelMng_ModelPurchasingFixPriceConfiguration_View = context.ModelMng_ModelPurchasingFixPriceConfiguration_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelMng_ModelPurchasingPriceConfiguration_View = context.ModelMng_ModelPurchasingPriceConfiguration_View.Where(s => s.ModelID == id).ToList();
                        foreach (ModelMng_ModelPurchasingPriceConfiguration_View itemModelMng_ModelPurchasingPriceConfiguration_View in temp.ModelMng_ModelPurchasingPriceConfiguration_View)
                        {
                            itemModelMng_ModelPurchasingPriceConfiguration_View.ModelMng_ModelPurchasingPriceConfigurationDetail_View = context.ModelMng_ModelPurchasingPriceConfigurationDetail_View
                                .Where(s => s.ModelPurchasingPriceConfigurationID == itemModelMng_ModelPurchasingPriceConfiguration_View.ModelPurchasingPriceConfigurationID).ToList();
                        }
                        temp.ModelCheckListGroupMng_ModelCheckListGroup_View = context.ModelCheckListGroupMng_ModelCheckListGroup_View.Where(s => s.ModelID == id).ToList();
                        temp.ModelDefaultOptionMng_ModelDefaultOption_View = context.ModelDefaultOptionMng_ModelDefaultOption_View.Where(s => s.ModelID == id).ToList();

                        data.Data = converter.DB2DTO_Model(temp);

                        Module.Framework.BLL fwBll = new Module.Framework.BLL();
                        if (fwBll.CanPerformAction(userId, "ModelMng", ModuleAction.CanApprove))
                        {
                            data.Data.TotalFactoryOrderItem = 0; // allow edit modelNM if user has approve permission
                        }
                    }
                    else
                    {
                        for (int seasonIndex = 2005; seasonIndex <= (DateTime.Now.Year + 1); seasonIndex++)
                        {
                            data.Data.ModelFixPriceConfigurations.Add(new DTO.ModelMng.ModelFixPriceConfiguration()
                            {
                                MaterialTypeID = -1,
                                Season = seasonIndex.ToString() + "/" + (seasonIndex + 1).ToString()
                            });

                            data.Data.ModelPurchasingFixPriceConfigurationDTOs.Add(new DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO()
                            {
                                MaterialTypeID = -1,
                                Season = seasonIndex.ToString() + "/" + (seasonIndex + 1).ToString()
                            });
                        }
                    }

                    for (int seasonIndex = 2005; seasonIndex <= (DateTime.Now.Year + 1); seasonIndex++)
                    {
                        string tempSeason = seasonIndex.ToString() + "/" + (seasonIndex + 1).ToString();
                        if (data.Data.ModelFixPriceConfigurations.Where(s=>s.Season == tempSeason).Count() == 0)
                        {
                            data.Data.ModelFixPriceConfigurations.Add(new DTO.ModelMng.ModelFixPriceConfiguration()
                            {
                                MaterialTypeID = -1,
                                Season = tempSeason
                            });

                            data.Data.ModelPurchasingFixPriceConfigurationDTOs.Add(new DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO()
                            {
                                MaterialTypeID = -1,
                                Season = tempSeason
                            });
                        }
                    }

                    data.ProductTypes = supportFactory.GetProductType().ToList();
                    data.PackagingMethods = supportFactory.GetPackagingMethod().ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
                    data.ProductGroups = supportFactory.GetProductGroup().ToList();
                    data.ManufacturerCountries = supportFactory.GetManufacturerCountry().ToList();
                    data.ModelStatuses = supportFactory.GetModelStatus().ToList();
                    data.ProductCategories = supportFactory.GetProductCategories().ToList();
                    data.Factories = supportFactory.GetFactory().ToList();
                    data.GalleryItemTypes = supportFactory.GetGalleryItemType().ToList();
                    data.ProductElements = supportFactory.GetProductElement().ToList();
                    data.ProductElementOptions = converter.DB2DTO_ProductElementOptionList(context.ModelMng_ProductElementOption_View.ToList());
                    data.MaterialTypes = supportFactory.GetMaterialType().ToList();
                    data.ModelPriceConfigurationDefault = converter.DB2DTO_ModelPriceConfigurationList(context.ModelMng_ModelPriceConfigurationDefault_View.Include("ModelMng_ModelPriceConfigurationDefaultDetail_View").ToList());
                    data.FactoryDTOs = converter.DB2DTO_Factory(context.ModelMng_Factory_View.ToList());
                    data.ClientSpecialPackagingMethods = converter.DB2DTO_ClientSpecialPackingMethod(context.ModelMng_ClientSpecialPackagingMethod_View.ToList());
                    data.Clients = supportFactory.GetClient().ToList();
                    data.CheckListGroupDTO = converter.DB2DTO_ModelCheckListGroup(context.CheckListMng_CheckListGroup_View.ToList());
                    //bind ConfigPrice FactoryBreakdown
                    var FactoryBreakdowns = context.ModelMng_function_GetFactoryBreakdown(id);
                    foreach (var item in FactoryBreakdowns)
                    {
                        data.ConfigPriceDTOs.Add(new ConfigPriceDTO()
                        {
                            Season = item.Season,
                            ModelID = item.ModelID,
                            Remark = item.Remark
                 
                        });                    
                    }
                    var FactoryBreakdownDetails = context.ModelMng_function_GetFactoryBreakdownDetail(id);
                    foreach (var item in FactoryBreakdownDetails)
                    {
                        foreach (var configPriceDTO in data.ConfigPriceDTOs)
                        {
                            if(item.Season == configPriceDTO.Season)
                            {
                                switch (item.FactoryBreakdownCategoryID)
                                {
                                    case 1:
                                        configPriceDTO.FactoryBreakdownCategoryID1 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM1 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM1 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID1 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity1 = item.Quantity;
                                        break;
                                    case 2:
                                        configPriceDTO.FactoryBreakdownCategoryID2 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM2 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM2 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID2 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity2 = item.Quantity;
                                        break;
                                    case 3:
                                        configPriceDTO.FactoryBreakdownCategoryID3 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM3 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM3 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID3 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity3 = item.Quantity;
                                        break;
                                    case 4:
                                        configPriceDTO.FactoryBreakdownCategoryID4 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM4 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM4 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID4 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity4 = item.Quantity;
                                        break;
                                    case 5:
                                        configPriceDTO.FactoryBreakdownCategoryID5 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM5 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM5 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID5 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity5 = item.Quantity;
                                        break;
                                    case 6:
                                        configPriceDTO.FactoryBreakdownCategoryID6 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM6 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM6 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID6 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity6 = item.Quantity;
                                        break;
                                    case 7:
                                        configPriceDTO.FactoryBreakdownCategoryID7 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM7 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM7 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID7 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity7 = item.Quantity;
                                        break;
                                    case 8:
                                        configPriceDTO.FactoryBreakdownCategoryID8 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM8 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM8 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID8 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity8 = item.Quantity;
                                        break;
                                    case 9:
                                        configPriceDTO.FactoryBreakdownCategoryID9 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM9 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM9 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID9 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity9 = item.Quantity;
                                        break;
                                    case 10:
                                        configPriceDTO.FactoryBreakdownCategoryID10 = item.FactoryBreakdownCategoryID;
                                        configPriceDTO.BreakdownCategoryNM10 = item.BreakdownCategoryNM;
                                        configPriceDTO.UnitNM10 = item.UnitNM;
                                        configPriceDTO.FactoryBreakdownDetailID10 = item.FactoryBreakdownDetailID;
                                        configPriceDTO.Quantity10 = item.Quantity;
                                        break;
                                    default:
                                        break;
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
            }

            return data;
        }

        //
        // Testing EAN code auto generate
        //
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public DTO.ModelMng.EditFormData2 GetData2(int userId, int id, int mdlId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ModelMng.EditFormData2 data = new DTO.ModelMng.EditFormData2();

            data.Data = new DTO.ModelMng.ModelIssueTrack();
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_ModelIssueTrack(context.ModelMng_ModelIssueTrack_View
                            .FirstOrDefault(o => o.ModelIssueTrackID == id));
                    }
                    else
                    {
                        data.Data.ModelIssueTrackImagesError = new List<DTO.ModelMng.ModelIssueTrackImageError>();
                        data.Data.ModelIssueTrackImagesFinish = new List<DTO.ModelMng.ModelIssueTrackImageFinish>();
                    }

                    data.SupportQCTrackingStatus = new List<DTO.ModelMng.QCTrackingStatus>()
                    {
                        new DTO.ModelMng.QCTrackingStatus() {TrackingStatusID=1, TrackingStatusUD="Finished", TrackingStatusNM="Finished" },
                        new DTO.ModelMng.QCTrackingStatus() {TrackingStatusID=2, TrackingStatusUD="On Process", TrackingStatusNM="On Process" },
                        new DTO.ModelMng.QCTrackingStatus() {TrackingStatusID=3, TrackingStatusUD="Follow Up", TrackingStatusNM="Follow Up" }
                    };

                    data.SupportQCTrackingType = new List<DTO.ModelMng.QCTrackingType>()
                    {
                        new DTO.ModelMng.QCTrackingType() { TrackingTypeID=1, TrackingTypeUD="Weakness", TrackingTypeNM="Weakness" },
                        new DTO.ModelMng.QCTrackingType() { TrackingTypeID=2, TrackingTypeUD="Client complaint", TrackingTypeNM="Client complaint" },
                        new DTO.ModelMng.QCTrackingType() { TrackingTypeID=3, TrackingTypeUD="Testing failure", TrackingTypeNM="Testing failure" },
                        new DTO.ModelMng.QCTrackingType() { TrackingTypeID=4, TrackingTypeUD="Note in mass production", TrackingTypeNM="Note in mass production" },
                        new DTO.ModelMng.QCTrackingType() { TrackingTypeID=5, TrackingTypeUD="Improvement", TrackingTypeNM="Improvement" },
                        new DTO.ModelMng.QCTrackingType() { TrackingTypeID=6, TrackingTypeUD="Other", TrackingTypeNM="Other" }
                    };
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id">ModelIssueTrackID</param>
        /// <param name="idIssue">ModelID</param>
        /// <param name="dtoItem"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool UpdateDetailTracking(int userId, int id, int idIssue, ref DTO.ModelMng.ModelIssueTrack dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    ModelIssueTrack dbItem = null;

                    if (id <= 0)
                    {
                        dbItem = new ModelIssueTrack();
                        context.ModelIssueTrack.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ModelIssueTrack.Where(o => o.ModelIssueTrackID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_ModelIssueTrack(dtoItem, ref dbItem, this._TempFolder);

                        if (id <= 0)
                            dbItem.ModelID = idIssue;

                        // set db item
                        dbItem.Status = dtoItem.StatusBy;

                        // parse issue date
                        DateTime issueDate;
                        if (DateTime.TryParse(dtoItem.IssueDateFormatted, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out issueDate))
                            dbItem.IssueDate = issueDate;

                        // comment
                        if (string.IsNullOrEmpty(dtoItem.Comment))
                        {
                            dbItem.Comment = dtoItem.Comment;
                        }

                        // follow up
                        if (dbItem.FollowUp == 0)
                        {
                            dbItem.FollowUp = userId;
                        }

                        // other content
                        dbItem.OtherContent = dtoItem.OtherContent;

                        // remove orphan image
                        context.ModelIssueTrackImageError.Local.Where(o => o.ModelIssueTrack == null).ToList().ForEach(o => context.ModelIssueTrackImageError.Remove(o));
                        context.ModelIssueTrackImageFinish.Local.Where(o => o.ModelIssueTrack == null).ToList().ForEach(o => context.ModelIssueTrackImageFinish.Remove(o));

                        context.SaveChanges();

                        //dtoItem = GetData2(userId, dbItem.ModelIssueTrackID, id, out notification).Data;

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
                    notification.DetailMessage.Add(ex.GetBaseException().Message);

                return false;
            }
        }

        public bool DeleteDetailTracking(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    ModelIssueTrack dbItem = context.ModelIssueTrack.FirstOrDefault(o => o.ModelIssueTrackID == id);

                    if (dbItem == null)
                    {
                        notification.Message = "Model Issue Track is not found!";
                        return false;
                    }
                    else
                    {
                        foreach (var item in dbItem.ModelIssueTrackImageError.ToArray())
                            context.ModelIssueTrackImageError.Remove(item);

                        foreach (var item in dbItem.ModelIssueTrackImageFinish.ToArray())
                            context.ModelIssueTrackImageFinish.Remove(item);

                        context.ModelIssueTrack.Remove(dbItem);
                        context.SaveChanges();
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

        //Send Notification
        private void SendNotification(Model dbModel, string emailSubject, string emailBody)
        {
            try
            {
                // Thao and Alex
                List<int> idList = new List<int>();
                idList.Add(59);
                idList.Add(74);

                List<string> emailAddress = new List<string>();

                using (ModelMngEntities context = CreateContext())
                {
                    foreach (int id in idList)
                    {
                        var dbEmployee = context.Employee.FirstOrDefault(o => o.EmployeeID == id);
                        if (dbEmployee.Email1 != "")
                        {
                            emailAddress.Add(dbEmployee.Email1);
                        }
                        else if (dbEmployee.Email2 != "")
                        {
                            emailAddress.Add(dbEmployee.Email2);
                        }
                        // add to NotificationMessage table
                        NotificationMessage notification = new NotificationMessage();
                        notification.UserID = id;
                        notification.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_PRODUCTDEVELOPMENT;
                        notification.NotificationMessageTitle = emailSubject;
                        notification.NotificationMessageContent = emailBody;
                        context.NotificationMessage.Add(notification);
                    }

                    string sendToEmail = string.Empty;
                    foreach (string eAddress in emailAddress)
                    {
                        if (string.IsNullOrEmpty(sendToEmail))
                        {
                            sendToEmail += eAddress;
                        }
                        else
                        {
                            sendToEmail += ";" + eAddress;
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

        private void CheckDefaultOption(DTO.ModelMng.Model dtoItem)
        {
            bool result = true;
            result = dtoItem.MaterialID.HasValue || dtoItem.MaterialTypeID.HasValue || dtoItem.MaterialColorID.HasValue
                        || dtoItem.FrameMaterialID.HasValue || dtoItem.FrameMaterialColorID.HasValue
                        || dtoItem.SubMaterialID.HasValue || dtoItem.SubMaterialColorID.HasValue
                        || dtoItem.SeatCushionID.HasValue || dtoItem.BackCushionID.HasValue || dtoItem.CushionColorID.HasValue
                        || dtoItem.FSCTypeID.HasValue || dtoItem.FSCPercentID.HasValue;
            if (result)
            {
                if (!dtoItem.MaterialID.HasValue)
                {
                    throw new Exception("You should select material");
                }
                if (!dtoItem.MaterialTypeID.HasValue)
                {
                    throw new Exception("You should select material type");
                }
                if (!dtoItem.MaterialColorID.HasValue)
                {
                    throw new Exception("You should select material color");
                }
                if (!dtoItem.FrameMaterialID.HasValue)
                {
                    throw new Exception("You should select frame material color");
                }
                if (!dtoItem.FrameMaterialColorID.HasValue)
                {
                    throw new Exception("You should select frame material color");
                }
                if (!dtoItem.SubMaterialID.HasValue)
                {
                    throw new Exception("You should select sub material color");
                }
                if (!dtoItem.SubMaterialColorID.HasValue)
                {
                    throw new Exception("You should select sub material color");
                }
                if (!dtoItem.SeatCushionID.HasValue)
                {
                    throw new Exception("You should select seat cushion");
                }
                if (!dtoItem.BackCushionID.HasValue)
                {
                    throw new Exception("You should select back cushion");
                }
                if (!dtoItem.CushionColorID.HasValue)
                {
                    throw new Exception("You should select cushion color");
                }
                if (!dtoItem.FSCTypeID.HasValue)
                {
                    throw new Exception("You should select FST Type");
                }
                if (!dtoItem.FSCPercentID.HasValue)
                {
                    throw new Exception("You should select FST Percent");
                }
            }
        }

        // new update with userId
        public bool UpdateData(int id, ref DTO.ModelMng.Model dtoItem, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;
            int? packagingMethodID = null;
            List<Module.Framework.DTO.EntityTracking> newlyCreateDataTracking;

            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    Model dbItem = null;


                    foreach (var item in dtoItem.ModelDefaultOptionDTOs)
                    {
                        if (item.PackagingMethodID < 0)
                        {
                            item.PackagingMethodID = packagingMethodID;
                        }
                        if (dtoItem.MaterialThumbnailLocation != item.MaterialThumbnailLocation)
                        {
                            dtoItem.MaterialThumbnailLocation = item.MaterialThumbnailLocation;
                        }
                        if (dtoItem.FrameMaterialThumbnailLocation != item.FrameMaterialThumbnailLocation)
                        {
                            dtoItem.FrameMaterialThumbnailLocation = item.FrameMaterialThumbnailLocation;
                        }
                        if (dtoItem.FrameMaterialThumbnailLocation != item.FrameMaterialThumbnailLocation)
                        {
                            dtoItem.FrameMaterialThumbnailLocation = item.FrameMaterialThumbnailLocation;
                        }
                        if (dtoItem.SubMaterialColorThumbnailLocation != item.SubMaterialColorThumbnailLocation)
                        {
                            dtoItem.SubMaterialColorThumbnailLocation = item.SubMaterialColorThumbnailLocation;
                        }
                        if (dtoItem.CushionColorThumbnailLocation != item.CushionColorThumbnailLocation)
                        {
                            dtoItem.CushionColorThumbnailLocation = item.CushionColorThumbnailLocation;
                        }
                        if (dtoItem.BackCushionThumbnailLocation != item.BackCushionThumbnailLocation)
                        {
                            dtoItem.BackCushionThumbnailLocation = item.BackCushionThumbnailLocation;
                        }
                        if (dtoItem.SeatCushionThumbnailLocation != item.SeatCushionThumbnailLocation)
                        {
                            dtoItem.SeatCushionThumbnailLocation = item.SeatCushionThumbnailLocation;
                        }

                    }

                    if (id == 0)
                    {
                        dbItem = new Model();
                        context.Model.Add(dbItem);
                    }
                    else
                    {
                        //check default options
                        CheckDefaultOption(dtoItem);

                        //get data
                        dbItem = context.Model.FirstOrDefault(o => o.ModelID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Model not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // processing image before update the model
                        foreach (DTO.ModelMng.ImageGallery dtoImageGallery in dtoItem.ImageGalleries.Where(o => o.FileHasChange))
                        {
                            if (dtoImageGallery.GalleryItemTypeID == 3)
                            {
                                dtoImageGallery.FileUD = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoImageGallery.NewFile, dtoImageGallery.FileUD);
                            }
                            else
                            {
                                dtoImageGallery.FileUD = fwFactory.CreateFilePointer(this._TempFolder, dtoImageGallery.NewFile, dtoImageGallery.FileUD);
                            }
                        }

                        //process report
                        foreach (DTO.ModelMng.TestReport dtoReport in dtoItem.TestReports.Where(o => o.ScanHasChange))
                        {
                            dtoReport.ScanFile = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoReport.ScanNewFile, dtoReport.ScanFile);
                        }
                        //process tdfile
                        foreach (DTO.ModelMng.tdFile dtotdfile in dtoItem.Tdfiles.Where(o => o.ScanHasChange))
                        {
                            dtotdfile.ScanFile = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtotdfile.ScanNewFile, dtotdfile.ScanFile);
                        }

                        foreach (DTO.ModelMng.AIFile dtoaifile in dtoItem.AIFiles.Where(o => o.ScanHasChange))
                        {
                            dtoaifile.ScanFile = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoaifile.ScanNewFile, dtoaifile.ScanFile);
                        }

                        // process model issue track
                        if (dtoItem.ModelIssueTracks != null)
                        {
                            foreach (var dtoIssueTrack in dtoItem.ModelIssueTracks.Where(o => o.QualityReport_HasChange))
                            {
                                dtoIssueTrack.QualityReport = fwFactory.CreateNoneImageFilePointer(this._TempFolder, dtoIssueTrack.QualityReport_NewFile, dtoIssueTrack.QualityReport);
                            }
                        }

                        //read dto to db
                        converter.DTO2DB(userId, dtoItem, this, ref dbItem);

                        // map the attribute manually for those user who has the create rights
                        if (fwFactory.CanPerformAction(userId, "ModelMng", Library.DTO.ModuleAction.CanCreate))
                        {
                            dbItem.ModelNM = dtoItem.ModelNM;
                            dbItem.ProductTypeID = dtoItem.ProductTypeID;
                            dbItem.ManufacturerCountryID = dtoItem.ManufacturerCountryID;
                            dbItem.RangeName = dtoItem.RangeName;
                            dbItem.Collection = dtoItem.Collection;
                            dbItem.ModelStatusID = dtoItem.ModelStatusID;
                            dbItem.Season = dtoItem.Season;
                            dbItem.FactoryID = dtoItem.FactoryID;
                            dbItem.ProductGroupID = dtoItem.ProductGroupID;
                            dbItem.ProductCategoryID = dtoItem.ProductCategoryID;
                            dbItem.CataloguePageNo = dtoItem.CataloguePageNo;
                        }

                        // map the attribute manually for those user who has the create rights
                        if (fwFactory.CanPerformAction(userId, "FactoryOrderMng", Library.DTO.ModuleAction.CanUpdate))
                        {
                            dbItem.DefaultFactoryID = dtoItem.DefaultFactoryID;
                        }

                        // convert data to db and remove orphan
                        context.ModelSubMaterialOption.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelSubMaterialOption.Remove(o));
                        context.ModelCushionOption.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelCushionOption.Remove(o));
                        context.ModelPiece.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelPiece.Remove(o));
                        context.ModelPackagingMethodOption.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelPackagingMethodOption.Remove(o));
                        context.ModelSparepart.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelSparepart.Remove(o));
                        context.ImageGalleryVersion.Local.Where(o => o.ImageGallery == null).ToList().ForEach(o => context.ImageGalleryVersion.Remove(o));
                        context.TestReport.Local.Where(o => o.Model == null).ToList().ForEach(o => context.TestReport.Remove(o));
                        context.TDFile.Local.Where(o => o.Model == null).ToList().ForEach(o => context.TDFile.Remove(o));
                        context.AIFile.Local.Where(o => o.Model == null).ToList().ForEach(o => context.AIFile.Remove(o));
                        context.ModelIssueTrack.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelIssueTrack.Remove(o));
                        context.ModelCheckListGroup.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelCheckListGroup.Remove(o));
                        context.ModelFixPriceConfiguration.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelFixPriceConfiguration.Remove(o));
                        context.ModelPriceConfiguration.Local.Where(o => o.Model == null).ToList().ForEach(o => context.ModelPriceConfiguration.Remove(o));
                        context.ModelPriceConfigurationDetail.Local.Where(o => o.ModelPriceConfiguration == null).ToList().ForEach(o => context.ModelPriceConfigurationDetail.Remove(o));

                        foreach (ImageGallery dbImageGallery in context.ImageGallery.Local.Where(o => o.Model == null).ToList())
                        {
                            fwFactory.RemoveImageFile(dbImageGallery.FileUD);
                            context.ImageGallery.Remove(dbImageGallery);
                        }

                        // remove Test Report
                        foreach (TestReport dbReport in context.TestReport.Local.Where(o => o.Model == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbReport.ScanFile))
                            {
                                fwFactory.RemoveImageFile(dbReport.ScanFile);
                            }
                        }

                        //remove tdfile
                        foreach (TDFile dbtd in context.TDFile.Local.Where(o => o.Model == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbtd.ScanFile))
                            {
                                fwFactory.RemoveImageFile(dbtd.ScanFile);
                            }
                        }

                        //remove aifile
                        foreach (AIFile dbai in context.AIFile.Local.Where(o => o.Model == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbai.ScanFile))
                            {
                                fwFactory.RemoveImageFile(dbai.ScanFile);
                            }
                        }

                        foreach (ModelIssueTrack dbIssueTrack in context.ModelIssueTrack.Local.Where(o => o.Model == null).ToList())
                        {
                            if (!string.IsNullOrEmpty(dbIssueTrack.QualityReport))
                            {
                                fwFactory.RemoveImageFile(dbIssueTrack.QualityReport);
                            }
                        }

                        if (id == 0)
                        {
                            // generate latest code - table locks required
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT TOP 1 * FROM Model WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    string lastModelUD = "0001";
                                    Model lastModel = context.Model.OrderByDescending(o => o.ModelID).FirstOrDefault();
                                    if (lastModel != null)
                                    {
                                        lastModelUD = Library.Common.Helper.getNextCode(lastModel.ModelUD);
                                        while (context.Model.FirstOrDefault(o => o.ModelUD == lastModelUD) != null)
                                        {
                                            lastModelUD = Library.Common.Helper.getNextCode(lastModelUD);
                                        }
                                    }
                                    dbItem.ModelUD = lastModelUD;

                                    // track data                                    
                                    fwFactory.TrackingDataChange(context, "ModelMng", dtoItem.UpdatedBy.Value, out newlyCreateDataTracking);

                                    context.SaveChanges();

                                    // update track data
                                    fwFactory.UpdateDataTracking(context, newlyCreateDataTracking);
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

                            // Send email notification
                            string emailSubject = "NEW MODEL HAS CREATED: Model Code " + dbItem.ModelUD;
                            string emailBody = "";
                            emailBody += "CODE: " + dbItem.ModelUD + "<br/>";
                            emailBody += "NAME: " + dbItem.ModelNM + "<br/>";
                            emailBody += "RANGE NAME: " + dbItem.RangeName + "<br/>";
                            emailBody += "SEASON: " + dbItem.Season + "<br/>";

                            SendNotification(dbItem, emailSubject, emailBody);
                        }

                        // processing image - will be remove soon
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                        }

                        // track data
                        fwFactory.TrackingDataChange(context, "ModelMng", dtoItem.UpdatedBy.Value, out newlyCreateDataTracking);
                        
                        //update configPrice FactoryBreakdownDetail
                        foreach (var item in dtoItem.ConfigPriceDTOs)
                        {
                            //FactoryBreakdownCategory 1
                            FactoryBreakdownDetail FactoryBreakdownDetail1 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID1);
                            FactoryBreakdownDetail1.Quantity = item.Quantity1;
                            //FactoryBreakdownCategory 2
                            FactoryBreakdownDetail FactoryBreakdownDetail2 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID2);
                            FactoryBreakdownDetail2.Quantity = item.Quantity2;
                            //FactoryBreakdownCategory 3
                            FactoryBreakdownDetail FactoryBreakdownDetail3 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID3);
                            FactoryBreakdownDetail3.Quantity = item.Quantity3;
                            //FactoryBreakdownCategory 4
                            FactoryBreakdownDetail FactoryBreakdownDetail4 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID4);
                            FactoryBreakdownDetail4.Quantity = item.Quantity4;
                            //FactoryBreakdownCategory 5
                            FactoryBreakdownDetail FactoryBreakdownDetail5 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID5);
                            FactoryBreakdownDetail5.Quantity = item.Quantity5;
                            //FactoryBreakdownCategory 6
                            FactoryBreakdownDetail FactoryBreakdownDetail6 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID6);
                            FactoryBreakdownDetail6.Quantity = item.Quantity6;
                            //FactoryBreakdownCategory 7
                            FactoryBreakdownDetail FactoryBreakdownDetail7 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID7);
                            FactoryBreakdownDetail7.Quantity = item.Quantity7;
                            //FactoryBreakdownCategory 8
                            FactoryBreakdownDetail FactoryBreakdownDetail8 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID8);
                            FactoryBreakdownDetail8.Quantity = item.Quantity8;
                            //FactoryBreakdownCategory 9
                            FactoryBreakdownDetail FactoryBreakdownDetail9 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID9);
                            FactoryBreakdownDetail9.Quantity = item.Quantity9;
                            //FactoryBreakdownCategory 10
                            FactoryBreakdownDetail FactoryBreakdownDetail10 = context.FactoryBreakdownDetail.FirstOrDefault(o => o.FactoryBreakdownDetailID == item.FactoryBreakdownDetailID10);
                            FactoryBreakdownDetail10.Quantity = item.Quantity10;
                        }

                        context.SaveChanges();
                        // update track data
                        fwFactory.UpdateDataTracking(context, newlyCreateDataTracking);

                        dtoItem = GetData(dtoItem.UpdatedBy.Value, dbItem.ModelID, out notification).Data;

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddModelDefaultOption(dbItem.ModelID); // table lockx and also check if item is available on sql server side

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    switch (indexName)
                    {
                        case "ModelMaterialOptionUnique":
                            notification.Message = "Duplicate material option";
                            break;

                        case "ModelFrameMaterialUnique":
                            notification.Message = "Duplicate frame material option";
                            break;

                        case "ModelSubMaterialUnique":
                            notification.Message = "Duplicate sub material option";
                            break;

                        case "ModelPackagingMethodUnique":
                            notification.Message = "Duplicate packaging method option";
                            break;

                        case "ModelCushionUnique":
                            notification.Message = "Duplicate cushion specification option";
                            break;

                        case "ModelCushionColorUnique":
                            notification.Message = "Duplicate cushion color option";
                            break;
                    }
                }
                else if (number == 2627)
                {
                    switch (indexName)
                    {
                        case "ModelUDUnique":
                            notification.Message = "Duplicate model code";
                            break;

                        default:
                            notification.Message = dEx.Message;
                            break;
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public List<DTO.ModelMng.ModelDefaultFactoryDetail> GetModelDefaultFactoryDetail(int modelID, int factoryID, out Library.DTO.Notification notification)
        {
            List<DTO.ModelMng.ModelDefaultFactoryDetail> modelDefaultFactoryDetails = new List<DTO.ModelMng.ModelDefaultFactoryDetail>();

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var modelDefaultFactoryDetail = context.ModelMng_ModelDefaultFactoryDetail_View.Where(o => o.ModelID == modelID && o.FactoryID == factoryID);
                    modelDefaultFactoryDetails = converter.DB2DTO_ModelDefaultFactoryDetail(modelDefaultFactoryDetail.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message.Trim()))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return modelDefaultFactoryDetails;
        }

        public string ExportExcelModel(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ModelMngObject ds = new ModelMngObject();

            string ModelUD = null;
            string ModelNM = null;
            string UpdatorName = null;
            string Season = null;
            string FactoryUD = null;
            bool? IsStandard = null;
            bool? HasCushion = null;
            bool? HasFrameMaterial = null;
            bool? HasSubMaterial = null;
            int? ProductTypeID = null;
            int? ProductGroupID = null;
            int? ManufacturerCountryID = null;
            int? ModelStatusID = null;
            int? ProductCategoryID = null;
            bool? IsExcludedInNotification = null;
            bool? IsArchived = false;
            int UserID = -1;
            int? CatalogPage = null;


            if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }
            if (filters.ContainsKey("ModelUD"))
            {
                ModelUD = filters["ModelUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD"))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ModelNM"))
            {
                ModelNM = filters["ModelNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("UpdatorName"))
            {
                UpdatorName = filters["UpdatorName"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsStandard") && filters["IsStandard"] != null && !string.IsNullOrEmpty(filters["IsStandard"].ToString()))
            {
                IsStandard = (filters["IsStandard"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("ProductTypeID") && filters["ProductTypeID"] != null && !string.IsNullOrEmpty(filters["ProductTypeID"].ToString()))
            {
                ProductTypeID = Convert.ToInt32(filters["ProductTypeID"]);
            }
            if (filters.ContainsKey("ProductGroupID") && filters["ProductGroupID"] != null && !string.IsNullOrEmpty(filters["ProductGroupID"].ToString()))
            {
                ProductGroupID = Convert.ToInt32(filters["ProductGroupID"]);
            }
            if (filters.ContainsKey("HasCushion") && filters["HasCushion"] != null && !string.IsNullOrEmpty(filters["HasCushion"].ToString()))
            {
                HasCushion = (filters["HasCushion"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("HasFrameMaterial") && filters["HasFrameMaterial"] != null && !string.IsNullOrEmpty(filters["HasFrameMaterial"].ToString()))
            {
                HasFrameMaterial = (filters["HasFrameMaterial"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("HasSubMaterial") && filters["HasSubMaterial"] != null && !string.IsNullOrEmpty(filters["HasSubMaterial"].ToString()))
            {
                HasSubMaterial = (filters["HasSubMaterial"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("ManufacturerCountryID") && filters["ManufacturerCountryID"] != null && !string.IsNullOrEmpty(filters["ManufacturerCountryID"].ToString()))
            {
                ManufacturerCountryID = Convert.ToInt32(filters["ManufacturerCountryID"]);
            }
            if (filters.ContainsKey("ModelStatusID") && filters["ModelStatusID"] != null && !string.IsNullOrEmpty(filters["ModelStatusID"].ToString()))
            {
                ModelStatusID = Convert.ToInt32(filters["ModelStatusID"]);
            }
            if (filters.ContainsKey("CatalogPage") && filters["CatalogPage"] != null && !string.IsNullOrEmpty(filters["CatalogPage"].ToString()))
            {
                CatalogPage = Convert.ToInt32(filters["CatalogPage"]);
            }
            if (filters.ContainsKey("ProductCategoryID") && filters["ProductCategoryID"] != null && !string.IsNullOrEmpty(filters["ProductCategoryID"].ToString()))
            {
                ProductCategoryID = Convert.ToInt32(filters["ProductCategoryID"]);
            }
            if (filters.ContainsKey("IsExcludedInNotification") && filters["IsExcludedInNotification"] != null && !string.IsNullOrEmpty(filters["IsExcludedInNotification"].ToString()))
            {
                IsExcludedInNotification = (filters["IsExcludedInNotification"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("IsArchived") && filters["IsArchived"] != null && !string.IsNullOrEmpty(filters["IsArchived"].ToString()))
            {
                IsArchived = (filters["IsArchived"].ToString() == "true") ? true : false;
            }

            try
            {

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ModelMng_function_ReportWithFilters", new System.Data.SqlClient.SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@ModelUD", ModelUD);
                adap.SelectCommand.Parameters.AddWithValue("@ModelNM", ModelNM);
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ModelStatusID", ModelStatusID);
                adap.SelectCommand.Parameters.AddWithValue("@CatalogPage", CatalogPage);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@ManufacturerCountryID", ManufacturerCountryID);
                adap.SelectCommand.Parameters.AddWithValue("@ProductTypeID", ProductTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                adap.SelectCommand.Parameters.AddWithValue("@ProductCategoryID", ProductCategoryID);
                adap.SelectCommand.Parameters.AddWithValue("@HasCushion", HasCushion);
                adap.SelectCommand.Parameters.AddWithValue("@HasFrameMaterial", HasFrameMaterial);
                adap.SelectCommand.Parameters.AddWithValue("@HasSubMaterial", HasSubMaterial);
                adap.SelectCommand.Parameters.AddWithValue("@IsExcludedInNotification", IsExcludedInNotification);
                adap.SelectCommand.Parameters.AddWithValue("@IsArchived", IsArchived);


                adap.TableMappings.Add("Table", "ModelMng_ModelReport_View");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "ModelRpt");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        //Get SampleProduct, Product
        public DTO.ModelMng.EditFormData GetSampleProductData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            DTO.ModelMng.EditFormData data = new DTO.ModelMng.EditFormData();
            data.Data = new DTO.ModelMng.Model();
            data.Data.sampleProductViews = new List<DTO.ModelMng.SampleProductView>();
            try
            {
                using (var context = CreateContext())
                {
                    data.Data.sampleProductViews = converter.DB2DTO_SampleProduct(context.ModelMng_SampleProduct_View.Where(o => o.ModelID == id).ToList());
                }
            }
            catch (Exception ex)
            {

                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = ex.Message };
            }
            return data;
        }

        public DTO.ModelMng.EditFormData GetProductData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ModelMng.EditFormData data = new DTO.ModelMng.EditFormData();
            data.Data = new DTO.ModelMng.Model();
            data.Data.productsViews = new List<DTO.ModelMng.productsView>();

            try
            {
                using (var context = CreateContext())
                {
                    data.Data.productsViews = converter.DB2DTO_Product(context.ModelMng_Product2_view.Where(o => o.ModelID == id).ToList());
                }

            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = ex.Message };
            }
            return data;
        }

        public DTO.ModelMng.EditFormData GetProductBreakDown(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.ModelMng.EditFormData data = new DTO.ModelMng.EditFormData();
            data.Data = new DTO.ModelMng.Model();
            data.Data.productBreakDowns = new List<DTO.ModelMng.ProductBreakDown>();

            int? companyID = null;
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Module.Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data.productBreakDowns = converter.BD2DTO_ProductBreakDown(context.ModelMng_ProductBreakDown_View.Where(o => o.ModelID == id).ToList());
                    }
                }

            }
            catch (Exception ex)
            {

                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }

            return data;
        }

        #region Create Model

        public CreateModelInitData GetInitDataCreateModel(int userId, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            CreateModelInitData data = new CreateModelInitData();
            data.Seasons = new List<DTO.Support.Season>();
            data.PackagingMethods = new List<DTO.Support.PackagingMethod>();
            data.ProductTypes = new List<DTO.Support.ProductType>();
            data.ProductGroups = new List<DTO.Support.ProductGroup>();
            data.ManufacturerCountries = new List<DTO.Support.ManufacturerCountry>();
            data.ModelStatuses = new List<DTO.Support.ModelStatus>();
            data.ProductCategories = new List<DTO.Support.ProductCategory>();
            data.Factories = new List<DTO.Support.Factory>();
            data.GalleryItemTypes = new List<DTO.Support.GalleryItemType>();
            data.ProductElements = new List<DTO.Support.ProductElement>();
            data.ProductElementOptions = new List<ProductElementOption>();
            data.MaterialTypes = new List<DTO.Support.MaterialType>();
            data.ModelCheckListGroupDTOs = new List<DTO.ModelMng.ModelCheckListGroupDTO>();
            data.SupportQCTrackingStatus = new List<QCTrackingStatus>()
            {
                new DTO.ModelMng.QCTrackingStatus() {TrackingStatusID=1, TrackingStatusUD="Finished", TrackingStatusNM="Finished" },
                new DTO.ModelMng.QCTrackingStatus() {TrackingStatusID=2, TrackingStatusUD="On Process", TrackingStatusNM="On Process" },
                new DTO.ModelMng.QCTrackingStatus() {TrackingStatusID=3, TrackingStatusUD="Follow Up", TrackingStatusNM="Follow Up" }
            };
            data.SupportQCTrackingType = new List<QCTrackingType>()
            {
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=1, TrackingTypeUD="Weakness", TrackingTypeNM="Weakness" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=2, TrackingTypeUD="Client complaint", TrackingTypeNM="Client complaint" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=3, TrackingTypeUD="Testing failure", TrackingTypeNM="Testing failure" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=4, TrackingTypeUD="Note in mass production", TrackingTypeNM="Note in mass production" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=5, TrackingTypeUD="Improvement", TrackingTypeNM="Improvement" },
                new DTO.ModelMng.QCTrackingType() { TrackingTypeID=6, TrackingTypeUD="Other", TrackingTypeNM="Other" }
            };

            try
            {
                using (var context = CreateContext())
                {

                    data.ProductTypes = supportFactory.GetProductType().ToList();
                    data.PackagingMethods = supportFactory.GetPackagingMethod().ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
                    data.ProductGroups = supportFactory.GetProductGroup().ToList();
                    data.ManufacturerCountries = supportFactory.GetManufacturerCountry().ToList();
                    data.ModelStatuses = supportFactory.GetModelStatus().ToList();
                    data.ProductCategories = supportFactory.GetProductCategories().ToList();
                    data.Factories = supportFactory.GetFactory().ToList();
                    data.GalleryItemTypes = supportFactory.GetGalleryItemType().ToList();
                    data.ProductElements = supportFactory.GetProductElement().ToList();
                    data.ProductElementOptions = converter.DB2DTO_ProductElementOptionList(context.ModelMng_ProductElementOption_View.ToList());
                    data.MaterialTypes = supportFactory.GetMaterialType().ToList();
                    data.ModelCheckListGroupDTOs = converter.DB2DTO_ModelCheckListGroupDTOs(context.ModelCheckListGroupMng_ModelCheckListGroup_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public CreateModelEditData GetDataCreateModel(int userId, int id, int sampleProductID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            CreateModelEditData data = new CreateModelEditData();
            data.Data = new DTO.ModelMng.Model();
            data.ModelPriceConfigurationDefault = new List<DTO.ModelMng.ModelPriceConfiguration>();

            try
            {
                if (id > 0 && fwFactory.IsInternalUser(userId) == 0 && fwFactory.CheckModelPermission(userId, id) == 0) // only check permission if user office = 4 (factory office)
                {
                    throw new Exception("Current user don't have access permission for the selected model data");
                }

                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Model(context.ModelMng_Model_View
                            .Include("ModelMng_ModelPiece_View")
                            .Include("ModelMng_ModelSparepart_View")
                            .Include("ModelMng_ModelCushionOption_View")
                            .Include("ModelMng_ModelSubMaterialOption_View")
                            .Include("ModelMng_ModelPackagingMethodOption_View")
                            .Include("ModelMng_ImageGallery_View.ModelMng_ImageGalleryVersion_View")
                            .Include("ModelMng_ModelIssueTrack_View")
                            .Include("ModelMng_ModelFixPriceConfiguration_View")
                            .Include("ModelMng_ModelPriceConfiguration_View")
                            .Include("ModelMng_ModelPriceConfiguration_View.ModelMng_ModelPriceConfigurationDetail_View")
                            .FirstOrDefault(o => o.ModelID == id));

                        Module.Framework.BLL fwBll = new Module.Framework.BLL();
                        if (fwBll.CanPerformAction(userId, "ModelMng", Library.DTO.ModuleAction.CanApprove))
                        {
                            data.Data.TotalFactoryOrderItem = 0; // allow edit modelNM if user has approve permission
                        }
                    }
                    else
                    {
                        for (int seasonIndex = 2005; seasonIndex <= (DateTime.Now.Year + 1); seasonIndex++)
                        {
                            data.Data.ModelFixPriceConfigurations.Add(new DTO.ModelMng.ModelFixPriceConfiguration()
                            {
                                MaterialTypeID = -1,
                                Season = seasonIndex.ToString() + "/" + (seasonIndex + 1).ToString()
                            });
                        }
                    }

                    for (int seasonIndex = 2005; seasonIndex <= (DateTime.Now.Year + 1); seasonIndex++)
                    {
                        string tempSeason = seasonIndex.ToString() + "/" + (seasonIndex + 1).ToString();
                        if(data.Data.ModelFixPriceConfigurations.Where(s=>s.Season == tempSeason).Count() == 0)
                        {
                            data.Data.ModelFixPriceConfigurations.Add(new DTO.ModelMng.ModelFixPriceConfiguration()
                            {
                                MaterialTypeID = -1,
                                Season = tempSeason
                            });
                        }                       
                    }

                    data.ModelPriceConfigurationDefault = converter.DB2DTO_ModelPriceConfigurationList(context.ModelMng_ModelPriceConfigurationDefault_View.Include("ModelMng_ModelPriceConfigurationDefaultDetail_View").ToList());
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

        #region Get default option by season
        public List<ModelDefaultOptionDTO> GetDefaultOptionBySeason(int userId, int id, string season, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            List<ModelDefaultOptionDTO> data = new List<ModelDefaultOptionDTO>();

            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    var result = context.ModelMng_function_GetDefaultOptionBySeason(id, season);
                    data = converter.DB2DTO_ModelDefaultOptionList(result.ToList());
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

        #region Get default option by previous season
        public List<ModelDefaultOptionDTO> GetDefaultOptionByPreviousSeason(int userId, int id, string season, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<ModelDefaultOptionDTO> data = new List<ModelDefaultOptionDTO>();

            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    var oldSeason = Library.Helper.GetPreviousSeason(season);
                    var result = context.ModelMng_function_GetDefaultOptionBySeason(id, oldSeason);
                    data = converter.DB2DTO_ModelDefaultOptionList(result.ToList());
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
        public override bool Approve(int id, ref DTO.ModelMng.Model dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public List<ClientSaleData> GetClientSaleData (string season, int clientID, int modelID, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<ClientSaleData> data = new List<ClientSaleData>();
            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    var result = context.ModelMng_function_GetClientSaleData(season,clientID,modelID, null, null);
                    data = converter.DB2DTO_ClientSale(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
        public bool SelfCalculationPrice(int userId, int id, string season, ref List<DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO> dtoItem, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            try
            {
                using (var context = CreateContext())
                {
                    foreach (var item in dtoItem)
                    {
                        decimal? price = context.ModelMng_function_GetPriceCushion(id,season,item.OptionID).FirstOrDefault();
                        if(price.HasValue && price > 0)
                            item.PurchasingUSDAmount = price;
                    }

                    return true;
                }
            }

            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public List<DTO.ModelMng.Model> GetModelNoFactory(int userId, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            List<DTO.ModelMng.Model> data = new List<DTO.ModelMng.Model>();

            try
            {
                using (ModelMngEntities context = CreateContext())
                {
                    var result = context.ModelMng_ModelNoDefaultFactory_View;
                    data = converter.DB2DTO_ModelNoFactoryList(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
    }
}