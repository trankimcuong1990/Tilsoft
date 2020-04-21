using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAL.LoadingPlanMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.LoadingPlanMng.SearchFormData, DTO.LoadingPlanMng.EditFormData, DTO.LoadingPlanMng.LoadingPlan>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private string _TempFolder;
        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private LoadingPlanMngEntities CreateContext()
        {
            return new LoadingPlanMngEntities(DALBase.Helper.CreateEntityConnectionString("LoadingPlanMngModel"));
        }

        public override DTO.LoadingPlanMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LoadingPlanMng.SearchFormData data = new DTO.LoadingPlanMng.SearchFormData();
            data.Data = new List<DTO.LoadingPlanMng.LoadingPlanSearchResult>();
            totalRows = 0;

            string LoadingPlanUD = null;
            string ClientUD = null;
            string ProformaInvoiceNo = null;
            string FactoryUD = null;
            string BookingUD = null;
            string BLNo = null;
            string ContainerNo = null;
            string ArticleCode = null;
            string Description = null;
            bool? IsUploadingImageFinish = null;
            string fromDate = null;
            string toDate = null;

            int? iRequesterID = (int)filters["iRequesterID"];

            if (filters.ContainsKey("LoadingPlanUD") && !string.IsNullOrEmpty(filters["LoadingPlanUD"].ToString()))
            {
                LoadingPlanUD = filters["LoadingPlanUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BookingUD") && !string.IsNullOrEmpty(filters["BookingUD"].ToString()))
            {
                BookingUD = filters["BookingUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
            {
                BLNo = filters["BLNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ContainerNo") && !string.IsNullOrEmpty(filters["ContainerNo"].ToString()))
            {
                ContainerNo = filters["ContainerNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
            {
                ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsUploadingImageFinish") && filters["IsUploadingImageFinish"] != null && !string.IsNullOrEmpty(filters["IsUploadingImageFinish"].ToString()))
            {
                IsUploadingImageFinish = (filters["IsUploadingImageFinish"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("FromDate") && !string.IsNullOrEmpty(filters["FromDate"].ToString()))
            {
                fromDate = filters["FromDate"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ToDate") && !string.IsNullOrEmpty(filters["ToDate"].ToString()))
            {
                toDate = filters["ToDate"].ToString().Replace("'", "''");
            }

            DateTime? _fromDate = fromDate.ConvertStringToDateTime();
            DateTime? _toDate = toDate.ConvertStringToDateTime();
            //try to get data
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    var resultSet = context.LoadingPlanMng_function_SearchLoadingPlan(iRequesterID, LoadingPlanUD, ClientUD, ProformaInvoiceNo, FactoryUD, BookingUD, BLNo, ContainerNo, ArticleCode, Description, IsUploadingImageFinish, _fromDate, _toDate, orderBy, orderDirection).ToList();
                    totalRows = resultSet.Count();
                    data.Data = converter.DB2DTO_LoadingPlanSearchResultList(resultSet.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //
                    // Get P/I No 
                    //
                    string LoadingPlanIDs = string.Join(",", data.Data.Select(o => o.LoadingPlanID).ToArray());
                    foreach (var extraData in context.LoadingPlanMng_function_GetSearchResultExtra(LoadingPlanIDs))
                    {
                        data.Data.FirstOrDefault(o => o.LoadingPlanID == extraData.LoadingPlanID).PINo = extraData.PINo;
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
            }

            return data;
        }

        public override DTO.LoadingPlanMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new Exception("do not install function");
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on loading plan
                if (fwFactory.CheckLoadingPlanPermission(iRequesterID, id) == 0)
                {
                    throw new Exception("You do not have access permission on this loading plan");
                }

                using (LoadingPlanMngEntities context = CreateContext())
                {
                    LoadingPlan dbItem = context.LoadingPlan.FirstOrDefault(o => o.LoadingPlanID == id);
                    foreach (var item in dbItem.DocumentClient.ToArray())
                    {
                        context.DocumentClient.Remove(item);
                    }
                    foreach (var item in dbItem.LoadingPlanDetail.ToArray())
                    {
                        context.LoadingPlanDetail.Remove(item);
                    }
                    foreach (var item in dbItem.LoadingPlanSparepartDetail.ToArray())
                    {
                        context.LoadingPlanSparepartDetail.Remove(item);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Loading plan not found!";
                        return false;
                    }
                    else
                    {
                        context.LoadingPlan.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException("does not install");
        }

        public bool UpdateData(int iRequesterID, int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    LoadingPlan dbItem = null;
                    if (id == 0)
                    {
                        if (dtoItem.FactoryID.HasValue || dtoItem.BookingID.HasValue)
                        {
                            //check permission on factory
                            if (dtoItem.FactoryID.HasValue && fwFactory.CheckFactoryPermission(iRequesterID, dtoItem.FactoryID.Value) == 0)
                            {
                                throw new Exception("You do not have access permission on this factory");
                            }
                            //check permission on booking
                            if (dtoItem.ParentID == 0)
                            {
                                if (dtoItem.BookingID.HasValue && fwFactory.CheckBookingPermission(iRequesterID, dtoItem.BookingID.Value) == 0)
                                {
                                    throw new Exception("You do not have access permission on this booking");
                                }
                            }                            
                        }
                        else
                        {
                            throw new Exception("Please fill-in factory or booking before create loading plan");
                        }

                        dbItem = new LoadingPlan();
                        context.LoadingPlan.Add(dbItem);
                    }
                    else
                    {
                        //check permission on loading plan
                        if (fwFactory.CheckLoadingPlanPermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this loading plan");
                        }

                        DateTime? loadingDate = dtoItem.LoadingDate.ConvertStringToDateTime();
                        DateTime? shipmentDate = dtoItem.ShipmentDate.ConvertStringToDateTime();

                        if (loadingDate.HasValue && shipmentDate.HasValue)
                        {
                            if (loadingDate.Value.CompareTo(shipmentDate.Value) > 0)
                            {
                                throw new Exception("Container Loading Date must be earlier than ETD");
                            }
                        }
                        dbItem = context.LoadingPlan.FirstOrDefault(o => o.LoadingPlanID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Loading plan not found!";
                        return false;
                    }
                    else
                    {
                        //check laoding plan is shipped and confirmed
                        //if ((dbItem.IsShipped.HasValue && dbItem.IsShipped.Value) || (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value))
                        //{
                        //    throw new Exception("Loading plan was shipped/confirmed. you can not change. You need reset loading plan to change and mark as shipped/confirmed again");
                        //}

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // processing product image 1
                        if (dtoItem.ProductPicture1_HasChange)
                        {
                            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

                            // delete phisycal files 
                            string toBeDeletedFileLocation = "";
                            string toBeDeletedThumbnailLocation = "";
                            fwFactory.GetDBFileLocation(dtoItem.ProductPicture1, out toBeDeletedFileLocation, out toBeDeletedThumbnailLocation);

                            if (!string.IsNullOrEmpty(toBeDeletedFileLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedFileLocation);
                                }
                                catch { }
                            }

                            if (!string.IsNullOrEmpty(toBeDeletedThumbnailLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedThumbnailLocation);
                                }
                                catch { }
                            }

                            if (string.IsNullOrEmpty(dtoItem.ProductPicture1_NewFile))
                            {
                                // remove file registration in database
                                fwFactory.RemoveFile(dtoItem.ProductPicture1);

                                // reset file database pointer
                                dtoItem.ProductPicture1 = string.Empty;
                            }
                            else
                            {

                                string outFileFullPath, outThumbnailFullPath, outFilePointer, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile;
                                outDBThumbnailLocation = "";

                                // copy the new file
                                imgMng.StoreFile(this._TempFolder + dtoItem.ProductPicture1_NewFile, out outDBFileLocation, out outFileFullPath);

                                // copy the thumbnail   
                                imgMng.CreateThumbnail(this._TempFolder + dtoItem.ProductPicture1_NewFile, out outThumbnailFile, 350, 350);
                                if (!string.IsNullOrEmpty(outThumbnailFile))
                                {
                                    imgMng.StoreThumbnail(outThumbnailFile, out outDBThumbnailLocation, out outThumbnailFullPath);
                                }

                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(dtoItem.ProductPicture1, dtoItem.ProductPicture1_NewFile, outDBFileLocation, info.Extension, outDBThumbnailLocation, (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    dtoItem.ProductPicture1 = outFilePointer;
                                }
                            }
                        }

                        // processing product image 2
                        if (dtoItem.ProductPicture2_HasChange)
                        {
                            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

                            // delete phisycal files 
                            string toBeDeletedFileLocation = "";
                            string toBeDeletedThumbnailLocation = "";
                            fwFactory.GetDBFileLocation(dtoItem.ProductPicture2, out toBeDeletedFileLocation, out toBeDeletedThumbnailLocation);

                            if (!string.IsNullOrEmpty(toBeDeletedFileLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedFileLocation);
                                }
                                catch { }
                            }

                            if (!string.IsNullOrEmpty(toBeDeletedThumbnailLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedThumbnailLocation);
                                }
                                catch { }
                            }

                            if (string.IsNullOrEmpty(dtoItem.ProductPicture2_NewFile))
                            {
                                // remove file registration in database
                                fwFactory.RemoveFile(dtoItem.ProductPicture2);

                                // reset file database pointer
                                dtoItem.ProductPicture2 = string.Empty;
                            }
                            else
                            {

                                string outFileFullPath, outThumbnailFullPath, outFilePointer, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile;
                                outDBThumbnailLocation = "";

                                // copy the new file
                                imgMng.StoreFile(this._TempFolder + dtoItem.ProductPicture2_NewFile, out outDBFileLocation, out outFileFullPath);

                                // copy the thumbnail   
                                imgMng.CreateThumbnail(this._TempFolder + dtoItem.ProductPicture2_NewFile, out outThumbnailFile, 350, 350);
                                if (!string.IsNullOrEmpty(outThumbnailFile))
                                {
                                    imgMng.StoreThumbnail(outThumbnailFile, out outDBThumbnailLocation, out outThumbnailFullPath);
                                }

                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(dtoItem.ProductPicture2, dtoItem.ProductPicture2_NewFile, outDBFileLocation, info.Extension, outDBThumbnailLocation, (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    dtoItem.ProductPicture2 = outFilePointer;
                                }
                            }
                        }

                        // processing container image 1
                        if (dtoItem.ContainerPicture1_HasChange)
                        {
                            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

                            // delete phisycal files 
                            string toBeDeletedFileLocation = "";
                            string toBeDeletedThumbnailLocation = "";
                            fwFactory.GetDBFileLocation(dtoItem.ContainerPicture1, out toBeDeletedFileLocation, out toBeDeletedThumbnailLocation);

                            if (!string.IsNullOrEmpty(toBeDeletedFileLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedFileLocation);
                                }
                                catch { }
                            }

                            if (!string.IsNullOrEmpty(toBeDeletedThumbnailLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedThumbnailLocation);
                                }
                                catch { }
                            }

                            if (string.IsNullOrEmpty(dtoItem.ContainerPicture1_NewFile))
                            {
                                // remove file registration in database
                                fwFactory.RemoveFile(dtoItem.ContainerPicture1);

                                // reset file database pointer
                                dtoItem.ContainerPicture1 = string.Empty;
                            }
                            else
                            {

                                string outFileFullPath, outThumbnailFullPath, outFilePointer, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile;
                                outDBThumbnailLocation = "";

                                // copy the new file
                                imgMng.StoreFile(this._TempFolder + dtoItem.ContainerPicture1_NewFile, out outDBFileLocation, out outFileFullPath);

                                // copy the thumbnail   
                                imgMng.CreateThumbnail(this._TempFolder + dtoItem.ContainerPicture1_NewFile, out outThumbnailFile, 350, 350);
                                if (!string.IsNullOrEmpty(outThumbnailFile))
                                {
                                    imgMng.StoreThumbnail(outThumbnailFile, out outDBThumbnailLocation, out outThumbnailFullPath);
                                }

                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(dtoItem.ContainerPicture1, dtoItem.ContainerPicture1_NewFile, outDBFileLocation, info.Extension, outDBThumbnailLocation, (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    dtoItem.ContainerPicture1 = outFilePointer;
                                }
                            }
                        }

                        // processing container image 2
                        if (dtoItem.ContainerPicture2_HasChange)
                        {
                            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

                            // delete phisycal files 
                            string toBeDeletedFileLocation = "";
                            string toBeDeletedThumbnailLocation = "";
                            fwFactory.GetDBFileLocation(dtoItem.ContainerPicture2, out toBeDeletedFileLocation, out toBeDeletedThumbnailLocation);

                            if (!string.IsNullOrEmpty(toBeDeletedFileLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedFileLocation);
                                }
                                catch { }
                            }

                            if (!string.IsNullOrEmpty(toBeDeletedThumbnailLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedThumbnailLocation);
                                }
                                catch { }
                            }

                            if (string.IsNullOrEmpty(dtoItem.ContainerPicture2_NewFile))
                            {
                                // remove file registration in database
                                fwFactory.RemoveFile(dtoItem.ContainerPicture2);

                                // reset file database pointer
                                dtoItem.ContainerPicture2 = string.Empty;
                            }
                            else
                            {

                                string outFileFullPath, outThumbnailFullPath, outFilePointer, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile;
                                outDBThumbnailLocation = "";

                                // copy the new file
                                imgMng.StoreFile(this._TempFolder + dtoItem.ContainerPicture2_NewFile, out outDBFileLocation, out outFileFullPath);

                                // copy the thumbnail   
                                imgMng.CreateThumbnail(this._TempFolder + dtoItem.ContainerPicture2_NewFile, out outThumbnailFile, 350, 350);
                                if (!string.IsNullOrEmpty(outThumbnailFile))
                                {
                                    imgMng.StoreThumbnail(outThumbnailFile, out outDBThumbnailLocation, out outThumbnailFullPath);
                                }

                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);

                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(dtoItem.ContainerPicture2, dtoItem.ContainerPicture2_NewFile, outDBFileLocation, info.Extension, outDBThumbnailLocation, (int)info.Length, out outFilePointer);

                                    // set file database pointer
                                    dtoItem.ContainerPicture2 = outFilePointer;
                                }
                            }
                        }

                        // processing container image 3
                        if (dtoItem.ContainerPicture3_HasChange)
                        {
                            dtoItem.ContainerPicture3 = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ContainerPicture3_NewFile, dtoItem.ContainerPicture3);
                        }

                        // processing container image 4
                        if (dtoItem.ContainerPicture4_HasChange)
                        {
                            dtoItem.ContainerPicture4 = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ContainerPicture4_NewFile, dtoItem.ContainerPicture4);
                        }

                        // processing container image 5
                        if (dtoItem.ContainerPicture5_HasChange)
                        {
                            dtoItem.ContainerPicture5 = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ContainerPicture5_NewFile, dtoItem.ContainerPicture5);
                        }

                        // processing container image 6
                        if (dtoItem.ContainerPicture6_HasChange)
                        {
                            dtoItem.ContainerPicture6 = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ContainerPicture6_NewFile, dtoItem.ContainerPicture6);
                        }

                        // processing MerchandisesInside1Per3ContImage
                        if (dtoItem.MerchandisesInside1Per3ContImage_HasChange)
                        {
                            dtoItem.MerchandisesInside1Per3ContImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.MerchandisesInside1Per3ContImage_NewFile, dtoItem.MerchandisesInside1Per3ContImage);
                        }

                        // processing MerchandisesInside1Per2ContImage
                        if (dtoItem.MerchandisesInside1Per2ContImage_HasChange)
                        {
                            dtoItem.MerchandisesInside1Per2ContImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.MerchandisesInside1Per2ContImage_NewFile, dtoItem.MerchandisesInside1Per2ContImage);
                        }

                        // processing MerchandisesInsideFullContImage
                        if (dtoItem.MerchandisesInsideFullContImage_HasChange)
                        {
                            dtoItem.MerchandisesInsideFullContImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.MerchandisesInsideFullContImage_NewFile, dtoItem.MerchandisesInsideFullContImage);
                        }

                        // processing ContainerDoorAndLockImage
                        if (dtoItem.ContainerDoorAndLockImage_HasChange)
                        {
                            dtoItem.ContainerDoorAndLockImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ContainerDoorAndLockImage_NewFile, dtoItem.ContainerDoorAndLockImage);
                        }

                        // processing ContainerSealImage
                        if (dtoItem.ContainerSealImage_HasChange)
                        {
                            dtoItem.ContainerSealImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ContainerSealImage_NewFile, dtoItem.ContainerSealImage);
                        }

                        // processing DryPackImage
                        if (dtoItem.DryPackImage_HasChange)
                        {
                            dtoItem.DryPackImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.DryPackImage_NewFile, dtoItem.DryPackImage);
                        }

                        // processing DryPackImage
                        if (dtoItem.ApprovedImage_HasChange)
                        {
                            dtoItem.ApprovedImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ApprovedImage_NewFile, dtoItem.ApprovedImage);
                        }

                        if (id == 0)
                        {
                            dtoItem.LoadingPlanUD = context.LoadingPlanMng_function_GenerateLoadingPlanCode(dtoItem.FactoryID).FirstOrDefault();
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);

                        //remove orphan
                        context.LoadingPlanDetail.Local.Where(o => o.LoadingPlan == null).ToList().ForEach(o => context.LoadingPlanDetail.Remove(o));
                        context.LoadingPlanSparepartDetail.Local.Where(o => o.LoadingPlan == null).ToList().ForEach(o => context.LoadingPlanSparepartDetail.Remove(o));

                        //remove sub loading plan
                        var dbSubLoadingPlan = context.LoadingPlan.Where(o => o.ParentID == id).ToList();
                        if (dtoItem.ChildLoadingPlans != null && dtoItem.ChildLoadingPlans.Count > 0)
                        {
                            foreach (LoadingPlan item in dbSubLoadingPlan)
                            {
                                int i = 0;
                                foreach (DTO.LoadingPlanMng.ChildLoadingPlan subItem in dtoItem.ChildLoadingPlans)
                                {
                                    if (subItem.LoadingPlanID == item.LoadingPlanID)
                                    {
                                        i++;
                                    }
                                }
                                if (i == 0)
                                {
                                    item.ParentID = 0;
                                }
                            }
                        }
                        else
                        {
                            foreach (LoadingPlan item in dbSubLoadingPlan)
                            {
                                item.ParentID = 0;
                            }
                        }

                       
                        //
                        // author: themy
                        // description: add notification when loading plan created
                        //
                        if (id == 0)
                        {
                            string emailSubject = "Loading Plan " + dbItem.LoadingPlanUD + " has Created!!";
                            fwFactory.SendEmailNotificationBasedOn("LoadingPlanMng", dbItem.LoadingPlanID, emailSubject, 1);

                            // Get information notification with status
                            var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == "LoadingPlanMng" && o.StatusID == 1).ToList();
                            foreach (var status in groupStatuses)
                            {
                                // Create email body
                                var EmailBody = FrameworkSetting.Setting.FrontendUrl + status.URLLink + "/Edit/" + dbItem.LoadingPlanID.ToString();

                                if (!string.IsNullOrEmpty(status.EmailTemplate))
                                {
                                    EmailBody = EmailBody + Environment.NewLine + status.EmailTemplate + dbItem.LoadingPlanID.ToString();
                                }
                                var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == status.NotificationGroupID);
                                foreach (var user in dbGroupMember)
                                {
                                    // add to NotificationMessage table
                                    NotificationMessage notification1 = new NotificationMessage();
                                    notification1.UserID = user.UserID;
                                    notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_LOGISTICS;
                                    notification1.NotificationMessageTitle = emailSubject;
                                    notification1.NotificationMessageContent = EmailBody;
                                    context.NotificationMessage.Add(notification1);                                   
                                }
                            }
                        }
                        //
                        // end
                        //
                        //save data
                        context.SaveChanges();

                        //re-get data
                        dtoItem = GetData(iRequesterID, dbItem.LoadingPlanID, 0, 0, 0, out notification).Data;

                        //CREATE DocumentClient
                        context.LoadingPlanMng_function_CreateDocumentClient(dbItem.LoadingPlanID);

                        //
                        // generate factory order info cach
                        //
                        context.FactoryOrderInfoCacheMng_function_BuildCacheForLoadingPlan(dbItem.LoadingPlanID);

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

        public override bool Approve(int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.LoadingPlanMng.LoadingPlan dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public DTO.LoadingPlanMng.EditFormData GetData(int iRequesterID, int id, int factoryID, int bookingID, int parentLoadingPlanID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LoadingPlanMng.EditFormData data = new DTO.LoadingPlanMng.EditFormData();
            data.Data = new DTO.LoadingPlanMng.LoadingPlan();
            data.Data.Details = new List<DTO.LoadingPlanMng.LoadingPlanDetail>();
            data.Data.Spareparts = new List<DTO.LoadingPlanMng.LoadingPlanSparepartDetail>();
            data.Data.SampleProducts = new List<DTO.LoadingPlanMng.LoadingPlanSampleProductDetail>();
            data.ContainerTypes = new List<DTO.Support.ContainerType>();
            data.Users = new List<DTO.LoadingPlanMng.User2>();

            //try to get data
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        //check permission on loading plan
                        if (fwFactory.CheckLoadingPlanPermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this loading plan");
                        }
                        data.Data = converter.DB2DTO_LoadingPlan(context.LoadingPlanMng_LoadingPlan_View
                            .Include("LoadingPlanMng_LoadingPlanDetail_View")
                            .Include("LoadingPlanMng_LoadingPlanSparepartDetail_View")
                            .Include("LoadingPlanMng_LoadingPlanSampleDetail_View").FirstOrDefault(o => o.LoadingPlanID == id));
                    }
                    else
                    {
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(iRequesterID, factoryID) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }

                        DTO.Support.Factory dtoFactory = supportFactory.GetFactory().FirstOrDefault(o => o.FactoryID == factoryID);
                        data.Data.FactoryID = factoryID;
                        data.Data.FactoryUD = dtoFactory.FactoryUD;

                        if (bookingID > 0)
                        {
                            //check permission on booking
                            if (fwFactory.CheckBookingPermission(iRequesterID, bookingID) == 0)
                            {
                                throw new Exception("You do not have access permission on this booking");
                            }

                            LoadingPlanMng_Booking_View dbBooking = context.LoadingPlanMng_Booking_View.FirstOrDefault(o => o.BookingID == bookingID);
                            data.Data.BookingID = bookingID;
                            data.Data.ClientUD = dbBooking.ClientUD;
                            data.Data.ClientID = dbBooking.ClientID;
                            data.Data.BookingUD = dbBooking.BookingUD;
                            data.Data.BLNo = dbBooking.BLNo;
                            if (dbBooking.ETD.HasValue) data.Data.ShipmentDate = dbBooking.ETD.Value.ToString("dd/MM/yyyy");
                        }
                        else if (parentLoadingPlanID > 0)
                        {
                            LoadingPlan dbLoadingPlan = context.LoadingPlan.Where(o => o.LoadingPlanID == parentLoadingPlanID).FirstOrDefault();
                            data.Data.ParentID = parentLoadingPlanID;
                            data.Data.BookingID = dbLoadingPlan.BookingID;
                            data.Data.ContainerNo = dbLoadingPlan.ContainerNo;
                            data.Data.SealNo = dbLoadingPlan.SealNo;
                            data.Data.ContainerTypeID = dbLoadingPlan.ContainerTypeID;

                            data.Data.ParentLoadingPlanUD = dbLoadingPlan.LoadingPlanUD;
                            data.Data.ParentContainerNo = dbLoadingPlan.ContainerNo;
                            data.Data.ParentSealNo = dbLoadingPlan.SealNo;
                            if (dbLoadingPlan.ShipmentDate.HasValue) data.Data.ShipmentDate = dbLoadingPlan.ShipmentDate.Value.ToString("dd/MM/yyyy");

                        }
                    }

                    data.ContainerTypes = supportFactory.GetContainerType().ToList();

                    int? currentUserCompanyID = fwFactory.GetCompanyID(iRequesterID);
                    if (currentUserCompanyID.HasValue)
                    {
                        data.Users = converter.DB2DTO_User2List(context.SupportMng_User2_View.Where(o => o.CompanyID.HasValue && o.CompanyID.Value == currentUserCompanyID.Value).ToList());
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
            }

            return data;
        }

        public DTO.LoadingPlanMng.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            DTO.LoadingPlanMng.InitFormData data = new DTO.LoadingPlanMng.InitFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            data.Factories = new List<DTO.LoadingPlanMng.Factory>();

            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    foreach (LoadingPlanMng_Factory_View dbFactory in context.LoadingPlanMng_Factory_View.ToList())
                    {
                        DTO.LoadingPlanMng.Factory dtoFactory = new DTO.LoadingPlanMng.Factory();
                        dtoFactory.FactoryID = dbFactory.FactoryID;
                        dtoFactory.FactoryUD = dbFactory.FactoryUD;
                        dtoFactory.Bookings = new List<DTO.LoadingPlanMng.Booking>();
                        foreach (LoadingPlanMng_Booking_View dbBooking in context.LoadingPlanMng_Booking_View.Where(o => o.SupplierID == dbFactory.SupplierID).ToList())
                        {
                            DTO.LoadingPlanMng.Booking dtoBooking = new DTO.LoadingPlanMng.Booking();
                            dtoBooking.BookingID = dbBooking.BookingID;
                            dtoBooking.DisplayText = dbBooking.DisplayText;
                            dtoFactory.Bookings.Add(dtoBooking);
                        }
                        data.Factories.Add(dtoFactory);
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
            }

            return data;
        }

        public List<DTO.LoadingPlanMng.ProductSearchResult> QuicksearchProduct(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.LoadingPlanMng.ProductSearchResult> data = new List<DTO.LoadingPlanMng.ProductSearchResult>();
            totalRows = 0;

            string ProformaInvoiceNo = null;
            string ClientUD = null;
            string FactoryUD = null;
            string ArticleCode = null;
            string Description = null;
            int FactoryID = 0;
            if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
            {
                FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
            }
            if (filters.ContainsKey("SearchQuery") && !string.IsNullOrEmpty(filters["SearchQuery"].ToString()))
            {
                ProformaInvoiceNo = ClientUD = FactoryUD = ArticleCode = Description = filters["SearchQuery"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    totalRows = context.LoadingPlanMng_function_SearchProduct(ProformaInvoiceNo, ClientUD, FactoryUD, ArticleCode, Description, FactoryID, orderBy, orderDirection).Count();
                    var result = context.LoadingPlanMng_function_SearchProduct(ProformaInvoiceNo, ClientUD, FactoryUD, ArticleCode, Description, FactoryID, orderBy, orderDirection);
                    data = converter.DB2DTO_ProductSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        public List<DTO.LoadingPlanMng.SparepartSearchResult> QuicksearchSparepart(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.LoadingPlanMng.SparepartSearchResult> data = new List<DTO.LoadingPlanMng.SparepartSearchResult>();
            totalRows = 0;

            string ProformaInvoiceNo = null;
            string ClientUD = null;
            string FactoryUD = null;
            string ArticleCode = null;
            string Description = null;
            int FactoryID = 0;
            if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
            {
                FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
            }
            if (filters.ContainsKey("SearchQuery") && !string.IsNullOrEmpty(filters["SearchQuery"].ToString()))
            {
                ProformaInvoiceNo = ClientUD = FactoryUD = ArticleCode = Description = filters["SearchQuery"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    totalRows = context.LoadingPlanMng_function_SearchSparepart(ProformaInvoiceNo, ClientUD, FactoryUD, ArticleCode, Description, FactoryID, orderBy, orderDirection).Count();
                    var result = context.LoadingPlanMng_function_SearchSparepart(ProformaInvoiceNo, ClientUD, FactoryUD, ArticleCode, Description, FactoryID, orderBy, orderDirection);
                    data = converter.DB2DTO_SparepartSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public bool SetLoadingPlanStatus(int branchID, int id, int updatedBy, bool IsConfirmed, bool IsLoaded, bool IsShipped, out Library.DTO.Notification notification)
        {
            string message = "";
            if (IsConfirmed) message = "Loading plan has been mark as confirmed";
            if (IsLoaded) message = "Loading plan has been mark as loaded";
            if (IsShipped) message = "Loading plan has been mark as shipped";           
            notification = new Library.DTO.Notification() { Message = message, Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    LoadingPlan dbLoadingPlan = context.LoadingPlan.Where(o => o.LoadingPlanID == id).FirstOrDefault();
                    //check exist shipping info
                    if (!dbLoadingPlan.BookingID.HasValue)
                    {
                        message = "Shipping info (Booking Ref, BL No, Forwarder ...) is missing. You can reslove it by attatch with other loading plan";
                        notification = new Library.DTO.Notification() { Message = message, Type = Library.DTO.NotificationType.Warning };
                        return false;
                    }

                    //check in case confirmed, loaded, shipped
                    if (!dbLoadingPlan.ParentID.HasValue)
                    {
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerNo) || string.IsNullOrEmpty(dbLoadingPlan.SealNo) || dbLoadingPlan.ContainerTypeID == null)
                        {
                            if (IsConfirmed) message = "You can not mark as confirmed. You need fill-in ContainerNo, Seal No, Container Type before confirmed";
                            if (IsLoaded) message = "You can not mark as loaded. You need fill-in ContainerNo, Seal No, Container Type before loaded";
                            if (IsShipped) message = "You can not mark as confirmed. You need fill-in ContainerNo, Seal No, Container Type before shipped";

                            notification = new Library.DTO.Notification() { Message = message, Type = Library.DTO.NotificationType.Warning };
                            return false;
                        }
                    }

                    //check item was made plc?
                    message = (IsLoaded ? "loaded" : (IsShipped ? "shipped" : (IsConfirmed ? "confirmed" : "")));
                    foreach (var item in context.LoadingPlanMng_LoadingPlanDetail_View.Where(o => o.LoadingPlanID == id && o.Quantity.HasValue && o.Quantity != 0))
                    {
                        if (!item.IsCreatedPLC.Value)
                        {
                            throw new Exception("You can not mark as " + message + ". All product in loading plan must be created PLC before mark as " + message + ". Click button Make PLC on every item to create PLC");
                        }
                    }


                    foreach (var item in context.LoadingPlanMng_LoadingPlanSparepartDetail_View.Where(o => o.LoadingPlanID == id && o.Quantity.HasValue && o.Quantity != 0))
                    {
                        if (!item.IsCreatedPLC.Value)
                        {
                            throw new Exception("You can not mark as " + message + ". All sparepart in loading plan must be created PLC before mark as " + message + ". Click button Make PLC on every item to create PLC");
                        }
                    }

                    foreach (var item in context.LoadingPlanMng_LoadingPlanSampleDetail_View.Where(o => o.LoadingPlanID == id && o.Quantity.HasValue && o.Quantity != 0))
                    {
                        if (!item.IsCreatedPLC.Value)
                        {
                            throw new Exception("You can not mark as " + message + ". All product in loading plan must be created PLC before mark as " + message + ". Click button Make PLC on every item to create PLC");
                        }
                    }

                    // Check product image for mark as Shipped, Confirmed
                    if (IsShipped || IsConfirmed)
                    {
                        foreach (var item in context.LoadingPlanMng_LoadingPlanDetail_View.Where(o => o.LoadingPlanID == id && o.Quantity.HasValue && o.Quantity != 0))
                        {
                            // Fix case image file can is ' ' or '' or null.
                            if (string.IsNullOrEmpty(item.ImageFile) || string.IsNullOrEmpty(item.ImageFile.Trim()))
                            {
                                throw new Exception("You can not mark as " + message + ". All product in loading plan must have image before mark as " + message);
                            }
                        }
                    }

                    //check uploaded image & quantity
                    if (IsLoaded || IsShipped || IsConfirmed)
                    {
                        if (string.IsNullOrEmpty(dbLoadingPlan.ProductPicture1))
                        {
                            throw new Exception("You have to upload product image 1 before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ProductPicture2))
                        {
                            throw new Exception("You have to upload product image 2 before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerPicture1))
                        {
                            throw new Exception("You have to upload moisture content of wood floor image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerPicture2))
                        {
                            throw new Exception("You have to upload Checking for Leakage-Left side image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerPicture3))
                        {
                            throw new Exception("You have to upload Checking for Leakage-Right side image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerPicture4))
                        {
                            throw new Exception("You have to upload Checking for Leakage-Ceiling image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerPicture5))
                        {
                            throw new Exception("You have to upload Checking for Leakage-Inside door image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerPicture6))
                        {
                            throw new Exception("You have to upload Checking for Leakage-Corner image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.MerchandisesInside1Per3ContImage))
                        {
                            throw new Exception("You have to upload Merchandises inside 1/3 container image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.MerchandisesInside1Per2ContImage))
                        {
                            throw new Exception("You have to upload Merchandises inside 1/2 container image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.MerchandisesInsideFullContImage))
                        {
                            throw new Exception("You have to upload Merchandises inside full container image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerDoorAndLockImage))
                        {
                            throw new Exception("You have to upload Container door and lock image before mark as loaded");
                        }
                        if (string.IsNullOrEmpty(dbLoadingPlan.ContainerSealImage))
                        {
                            throw new Exception("You have to upload Container seal image before mark as loaded");
                        }

                        //check loaded quantity
                        if ((!dbLoadingPlan.IsLoaded.HasValue || !dbLoadingPlan.IsLoaded.Value) && (!dbLoadingPlan.IsShipped.HasValue || !dbLoadingPlan.IsShipped.Value) && (!dbLoadingPlan.IsConfirmed.HasValue || !dbLoadingPlan.IsConfirmed.Value))
                        {
                            this.CheckLoadQnt(id);
                        }
                    }

                    //check in case is un-confirmed
                    if (IsConfirmed==true)
                    {
                        var loadingPlan = context.LoadingPlanMng_LoadingPlan_View.Where(o => o.LoadingPlanID == id).FirstOrDefault();
                        if (loadingPlan.IsBookingConfirmed.HasValue && loadingPlan.IsBookingConfirmed.Value)
                        {
                            throw new Exception("Booking has been confirmed. You can not un-confirm loading plan");
                        }
                        //
                        // send notification
                        //
                        string sendTo = string.Empty;
                        string subject = "Loading plan: [" + loadingPlan.LoadingPlanUD + "] has been confirmed by: " + loadingPlan.FactoryUD;
                        fwFactory.SendEmailNotificationBasedOn("LoadingPlanMng", dbLoadingPlan.LoadingPlanID, subject, 4);

                        // Get information notification with status
                        var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == "LoadingPlanMng" && o.StatusID == 4).ToList();
                        foreach (var status in groupStatuses)
                        {
                            // Create email body
                            var EmailBody = FrameworkSetting.Setting.FrontendUrl + status.URLLink + "/Edit/" + dbLoadingPlan.LoadingPlanID.ToString();

                            if (!string.IsNullOrEmpty(status.EmailTemplate))
                            {
                                EmailBody = EmailBody + Environment.NewLine + status.EmailTemplate + dbLoadingPlan.LoadingPlanID.ToString();
                            }
                            var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == status.NotificationGroupID);
                            foreach (var user in dbGroupMember)
                            {
                                // add to NotificationMessage table
                                NotificationMessage notification1 = new NotificationMessage();
                                notification1.UserID = user.UserID;
                                notification1.NotificationMessageTag = Module.Framework.ConstantIdentifier.MOBILE_APP_MESSAGE_TAG_LOGISTICS;
                                notification1.NotificationMessageTitle = subject;
                                notification1.NotificationMessageContent = EmailBody;
                                context.NotificationMessage.Add(notification1);
                                
                            }
                        }
                        //create bookingPlan
                        context.LoadingPlanMng_function_CreateBookingPlan(id, dbLoadingPlan.BookingID);

                        context.SaveChanges();
                    }

                    // Check LoadingPlan created DeliveryNote can not reset
                    //if (dbLoadingPlan.IsShipped.HasValue && dbLoadingPlan.IsShipped.Value && dbLoadingPlan.FactoryID.HasValue && (dbLoadingPlan.FactoryID.Value == 374043 || dbLoadingPlan.FactoryID.Value == 374072))
                    //{
                    //    var dbDeliveryNote = context.DeliveryNote.FirstOrDefault(o => o.Description.Contains(dbLoadingPlan.LoadingPlanUD));
                    //    if (dbDeliveryNote != null)
                    //    {
                    //        notification.Type = Library.DTO.NotificationType.Error;
                    //        notification.Message = "Loading Plan can't delete because [" + dbLoadingPlan.LoadingPlanUD + "] is created Delivery Note!";
                    //        return false;
                    //    }
                    //}

                    //set status
                    context.LoadingPlanMng_function_SetStatus(id, updatedBy, IsConfirmed, IsLoaded, IsShipped);

                    var isLoaded = context.LoadingPlan.Where(o => o.IsShipped == true && o.LoadingPlanID == id);
                    if (isLoaded.Count() > 0)
                    {
                        foreach (var loadingPlanItem in context.LoadingPlanMng_LoadingPlanDetail_View.Where(o => o.LoadingPlanID == id && o.RemainQnt == 0 && o.FactoryID == 374043))
                        {
                            using (var context2 = CreateContext())
                            {
                                var wodb = context2.WorkOrderMng_WorkOrder_View.FirstOrDefault(o => o.ProformaInvoiceNo == loadingPlanItem.ProformaInvoiceNo && o.ProductArticleCode == loadingPlanItem.ArticleCode);
                                if (wodb != null)
                                {
                                    if (wodb.ProformaInvoiceNo == loadingPlanItem.ProformaInvoiceNo && wodb.ProductArticleCode == loadingPlanItem.ArticleCode)
                                    {
                                        var dbWorkorder = context2.WorkOrder.FirstOrDefault(o => o.WorkOrderID == wodb.WorkOrderID);
                                        dbWorkorder.WorkOrderStatusID = 3;
                                        context2.SaveChanges();
                                    }
                                }
                            }
                        }
                    }

                    // Create delivery note.
                    DTO.LoadingPlanMng.LoadingPlan dtoLoadingPlan = AutoMapper.Mapper.Map<LoadingPlanMng_LoadingPlan_View, DTO.LoadingPlanMng.LoadingPlan>(context.LoadingPlanMng_LoadingPlan_View.Where(o => o.LoadingPlanID == id).FirstOrDefault());
                    if (IsConfirmed && dtoLoadingPlan.FactoryID.HasValue && (dtoLoadingPlan.FactoryID.Value == 374043 || dtoLoadingPlan.FactoryID.Value == 374072))
                    {
                        var dbDeliveryNote_check = context.DeliveryNote.Where(o => o.Description.Contains(dtoLoadingPlan.LoadingPlanUD)).ToList();
                        if (dbDeliveryNote_check != null && dbDeliveryNote_check.Count > 0)
                        {
                            foreach(var item in dbDeliveryNote_check)
                            {
                                context.DeliveryNote.Remove(item);
                            }
                            context.SaveChanges();
                        }

                        // Get company login user
                        Module.Framework.DAL.DataFactory frameworkFactory = new Module.Framework.DAL.DataFactory();
                        int? companyID = frameworkFactory.GetCompanyID(updatedBy);

                        var dbFactoryWarehouseList = context.FactoryWarehouse.Where(o => o.BranchID == branchID);
                        var dbFactoryWarehouse = branchID == 1 ? dbFactoryWarehouseList.FirstOrDefault(o => o.FactoryWarehouseID == 46) : dbFactoryWarehouseList.FirstOrDefault(o => o.FactoryWarehouseID == 46);

                        // For LoadingPlanDetail
                        foreach (var dbLoadingPlanDetail in dtoLoadingPlan.Details.Where(o => o.Quantity.HasValue && o.Quantity.Value != 0).ToList())
                        {
                            var workOrderDetail = context.WorkOrderDetail.FirstOrDefault(o => o.FactoryOrderDetailID == dbLoadingPlanDetail.FactoryOrderDetailID);

                            // Only create DeliveryNote has WorkOrder
                            if (workOrderDetail != null)
                            {
                                // New variable DeliveryNote.
                                var dbDeliveryNote = new DeliveryNote();
                                dbDeliveryNote.WorkCenterID = 16; // Default workcenter: 16 - Loading.
                                dbDeliveryNote.DeliveryNoteDate = dtoLoadingPlan.LoadingDate.ConvertStringToDateTime();
                                dbDeliveryNote.UpdatedBy = updatedBy; //dbFactoryWarehouse?.ResponsibleBy;
                                dbDeliveryNote.UpdatedDate = DateTime.Now;
                                dbDeliveryNote.Description = "Automatically Delivery Note from Loading Plan [" + dtoLoadingPlan.LoadingPlanUD + "]";
                                dbDeliveryNote.CompanyID = companyID;
                                dbDeliveryNote.ViewName = "Warehouse2Team";
                                dbDeliveryNote.IsApproved = true;
                                dbDeliveryNote.CreatedBy = updatedBy;
                                dbDeliveryNote.CreatedDate = DateTime.Now;
                                dbDeliveryNote.StatusTypeID = 6; // type: From Factory Order
                                // Add row DeliveryNote.
                                context.DeliveryNote.Add(dbDeliveryNote);

                                // Create DeliveryNoteDetail
                                var productionItemWithBOM = context.BOMMng_BOM_View.Where(o => o.WorkOrderID == workOrderDetail.WorkOrderID && o.ProductID == workOrderDetail.ProductID && o.ProductionItemTypeID == 3 && o.WorkCenterID == null);
                                foreach (var dbProductionItem in productionItemWithBOM.ToList())
                                {
                                    var dbDeliveryNoteDetail = new DeliveryNoteDetail();
                                    dbDeliveryNoteDetail.ProductionItemID = dbProductionItem.ProductionItemID;
                                    dbDeliveryNoteDetail.UnitID = dbProductionItem.UnitID;
                                    dbDeliveryNoteDetail.Qty = dbLoadingPlanDetail.Quantity.Value * dbProductionItem.Quantity;
                                    dbDeliveryNoteDetail.QtyByUnit = dbProductionItem.ConversionFactor.HasValue ? (dbDeliveryNoteDetail.Qty / dbProductionItem.ConversionFactor.Value) : dbDeliveryNoteDetail.Qty;
                                    dbDeliveryNoteDetail.FromFactoryWarehouseID = dbLoadingPlanDetail.FactoryID == 374043 ? 46 : 68;
                                    dbDeliveryNoteDetail.BOMID = dbProductionItem.BOMID;
                                    dbDeliveryNoteDetail.WorkOrderID = dbProductionItem.WorkOrderID;

                                    dbDeliveryNote.DeliveryNoteDetail.Add(dbDeliveryNoteDetail);
                                }

                                context.SaveChanges();

                                if (dbDeliveryNote != null)
                                {
                                    context.DeliveryNoteMng_function_GenerateDeliveryNoteUD(dbDeliveryNote.DeliveryNoteID, dbDeliveryNote.CompanyID, dbDeliveryNote.DeliveryNoteDate.Value.Year, dbDeliveryNote.DeliveryNoteDate.Value.Month);
                                }
                            }
                        }
                    }
                    return true;
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

        public IEnumerable<DTO.LoadingPlanMng.LoadingPlanSearchResult> QuickSearchLoadingPlan(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.LoadingPlanMng.LoadingPlanSearchResult> data = new List<DTO.LoadingPlanMng.LoadingPlanSearchResult>();

            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    string searchQuery = string.Empty;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }
                    totalRows = context.LoadingPlanMng_function_QuickSearchLoadingPlan(searchQuery, orderBy, orderDirection).Count();
                    var result = context.LoadingPlanMng_function_QuickSearchLoadingPlan(searchQuery, orderBy, orderDirection);
                    data = converter.DB2DTO_LoadingPlanSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }

        //public bool ValidateMakingPLCForItem(DTO.LoadingPlanMng.LoadingPlan dtoItem)
        //{
        //    using (var context = CreateContext())
        //    {
        //        int? bookingID = dtoItem.BookingID;
        //        List<string> productKeys = dtoItem.Details.Select(o => bookingID.ToString() + "_" + o.FactoryID.ToString() + "_" + o.OfferLineID.ToString()).ToList();
        //        List<string> sparepartKeys = dtoItem.Spareparts.Select(o => bookingID.ToString() + "_" + o.FactoryID.ToString() + "_" + o.OfferLineSparePartID.ToString()).ToList();
        //        var dbPLC_Product = context.PLC.Where(o => productKeys.Contains(bookingID.ToString() + "_" + o.FactoryID.ToString() + "_" + o.OfferLineID.ToString())).Select(o => new { o.PLCID, o.BookingID, o.FactoryID, o.OfferLineID, o.OfferLineSparepartID }).ToList();
        //        var dbPLC_Sparepart = context.PLC.Where(o => sparepartKeys.Contains(bookingID.ToString() + "_" + o.FactoryID.ToString() + "_" + o.OfferLineSparepartID.ToString())).Select(o => new { o.PLCID, o.BookingID, o.FactoryID, o.OfferLineID, o.OfferLineSparepartID }).ToList();
        //        //check product
        //        foreach (var item in dtoItem.Details)
        //        {
        //            var s = dbPLC_Product.Where(o => o.BookingID == bookingID && o.FactoryID == item.FactoryID && o.OfferLineID == item.OfferLineID);
        //            if (s.Count() == 0)
        //            {
        //                throw new Exception("Product: " + item.ArticleCode + " - " + item.Description + " does not make PLC. You must make PLC before add to making loading plan");
        //            }
        //        }
        //        //check sparepart
        //        foreach (var item in dtoItem.Spareparts)
        //        {
        //            var s = dbPLC_Sparepart.Where(o => o.BookingID == bookingID && o.FactoryID == item.FactoryID && o.OfferLineSparepartID == item.OfferLineSparePartID);
        //            if (s.Count() == 0)
        //            {
        //                throw new Exception("Sparepart: " + item.ArticleCode + " - " + item.Description + " does not make PLC. You must make PLC before add to making loading plan");
        //            }
        //        }
        //    }
        //    return true;    
        //}

        public DTO.LoadingPlanMng.OverviewData GetViewData(int iRequesterID, int id, int factoryID, int bookingID, int parentLoadingPlanID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LoadingPlanMng.OverviewData data = new DTO.LoadingPlanMng.OverviewData();
            data.Data = new DTO.LoadingPlanMng.LoadingPlanOverView();
            data.Data.Details = new List<DTO.LoadingPlanMng.LoadingPlanDetailOverview>();
            data.Data.Spareparts = new List<DTO.LoadingPlanMng.LoadingPlanSparepartDetailOverview>();
            data.Users = new List<DTO.LoadingPlanMng.User2>();

            //try to get data
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        //check permission on loading plan
                        if (fwFactory.CheckLoadingPlanPermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this loading plan");
                        }
                        var dataItems = context.LoadingPlanMng_LoadingPlan_OverView.Include("LoadingPlanMng_LoadingPlanDetail_OverView").Include("LoadingPlanMng_LoadingPlanSparepartDetail_OverView").Include("LoadingPlanMng_ChildLoadingPlan_OverView").FirstOrDefault(o => o.LoadingPlanID == id);
                        data.Data = converter.DB2DTO_LoadingPlanOverView(dataItems);
                    }
                    else
                    {
                        //check permission on factory
                        if (fwFactory.CheckFactoryPermission(iRequesterID, factoryID) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }

                        DTO.Support.Factory dtoFactory = supportFactory.GetFactory().FirstOrDefault(o => o.FactoryID == factoryID);
                        data.Data.FactoryID = factoryID;
                        data.Data.FactoryUD = dtoFactory.FactoryUD;

                        if (bookingID > 0)
                        {
                            //check permission on booking
                            if (fwFactory.CheckBookingPermission(iRequesterID, bookingID) == 0)
                            {
                                throw new Exception("You do not have access permission on this booking");
                            }

                            LoadingPlanMng_Booking_View dbBooking = context.LoadingPlanMng_Booking_View.FirstOrDefault(o => o.BookingID == bookingID);
                            data.Data.BookingID = bookingID;
                            data.Data.ClientUD = dbBooking.ClientUD;
                            data.Data.ClientID = dbBooking.ClientID;
                            data.Data.BookingUD = dbBooking.BookingUD;
                            data.Data.BLNo = dbBooking.BLNo;
                            if (dbBooking.ETD.HasValue) data.Data.ShipmentDate = dbBooking.ETD.Value.ToString("dd/MM/yyyy");
                        }
                        else if (parentLoadingPlanID > 0)
                        {
                            LoadingPlan dbLoadingPlan = context.LoadingPlan.Where(o => o.LoadingPlanID == parentLoadingPlanID).FirstOrDefault();
                            data.Data.ParentID = parentLoadingPlanID;
                            data.Data.BookingID = dbLoadingPlan.BookingID;
                            data.Data.ContainerNo = dbLoadingPlan.ContainerNo;
                            data.Data.SealNo = dbLoadingPlan.SealNo;
                            data.Data.ContainerTypeID = dbLoadingPlan.ContainerTypeID;

                            data.Data.ParentLoadingPlanUD = dbLoadingPlan.LoadingPlanUD;
                            data.Data.ParentContainerNo = dbLoadingPlan.ContainerNo;
                            data.Data.ParentSealNo = dbLoadingPlan.SealNo;
                            if (dbLoadingPlan.ShipmentDate.HasValue) data.Data.ShipmentDate = dbLoadingPlan.ShipmentDate.Value.ToString("dd/MM/yyyy");

                        }
                    }

                    int? currentUserCompanyID = fwFactory.GetCompanyID(iRequesterID);
                    if (currentUserCompanyID.HasValue)
                    {
                        data.Users = converter.DB2DTO_User2List(context.SupportMng_User2_View.Where(o => o.CompanyID.HasValue && o.CompanyID.Value == currentUserCompanyID.Value).ToList());
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
            }

            return data;
        }

        //private void CheckLoadQnt(DTO.LoadingPlanMng.LoadingPlan dtoItem)
        //{
        //    using (LoadingPlanMngEntities context = CreateContext())
        //    {
        //        //get product info
        //        List<int?> factoryOrderDetailIDs = dtoItem.Details.Where(o => o.FactoryOrderDetailID.HasValue).Select(o => o.FactoryOrderDetailID).Distinct().ToList();
        //        var factoryOrderDetailItems = context.LoadingPlanMng_ProductSearchResult_View.Where(o => factoryOrderDetailIDs.Contains(o.FactoryOrderDetailID)).ToList();

        //        //get current deliveryNote detail info
        //        List<int> loadingPlanDetailIDs = dtoItem.Details.Where(o => o.LoadingPlanDetailID > 0).Select(s => s.LoadingPlanDetailID).ToList();
        //        var loadingPlanDetailItems = context.LoadingPlanDetail.Where(o => loadingPlanDetailIDs.Contains(o.LoadingPlanDetailID)).ToList();

        //        //find item overload stock
        //        foreach (var pId in factoryOrderDetailIDs)
        //        {
        //            //get remaint qnt
        //            var pItem = factoryOrderDetailItems.Where(o => o.FactoryOrderDetailID == pId).FirstOrDefault();
        //            int? remainQnt = (pItem == null?0:pItem.RemainQnt);
        //            int? totalInputQnt = 0;

        //            //cal total load qnt that user input
        //            foreach (var item in dtoItem.Details.Where(o => o.FactoryOrderDetailID == pId))
        //            {
        //                int? userInputQnt = 0;
        //                int? qty = (item.Quantity.HasValue ? item.Quantity : 0);
        //                if (item.LoadingPlanDetailID > 0)
        //                {
        //                    var currentLoadingPlanItem = loadingPlanDetailItems.Where(o => o.LoadingPlanDetailID == item.LoadingPlanDetailID).FirstOrDefault();
        //                    userInputQnt = qty - (currentLoadingPlanItem.Quantity.HasValue ? currentLoadingPlanItem.Quantity : 0);
        //                }
        //                else
        //                {
        //                    userInputQnt = qty;
        //                }
        //                totalInputQnt += userInputQnt;
        //            }
        //            if (totalInputQnt > remainQnt)
        //            {
        //                throw new Exception("Quantity for item " + pItem.ArticleCode +"(" + pItem.Description + ") must be less than remaining order qnt");
        //            }
        //        }
        //    }
        //}

        private void CheckLoadQnt(int loadingPlanID)
        {
            using (LoadingPlanMngEntities context = CreateContext())
            {
                var loadingPlanDetails = context.LoadingPlanMng_LoadingPlanDetail_View.Where(o => o.LoadingPlanID == loadingPlanID).ToList();
                if (loadingPlanDetails != null)
                {
                    foreach (var item in loadingPlanDetails)
                    {
                        int loadedQnt = (item.Quantity.HasValue ? item.Quantity.Value : 0);
                        int remaintQnt = (item.RemainQnt.HasValue ? item.RemainQnt.Value : 0);
                        if (loadedQnt > remaintQnt)
                        {
                            throw new Exception("Quantity for item " + item.ArticleCode + "(" + item.Description + ") must be less than remaining order qnt");
                        }
                    }
                }
            }
        }

        public List<DTO.LoadingPlanMng.SampleProductSearchResult> QuicksearchSampleProduct(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.LoadingPlanMng.SampleProductSearchResult> data = new List<DTO.LoadingPlanMng.SampleProductSearchResult>();
            totalRows = 0;

            string ProformaInvoiceNo = null;
            string ClientUD = null;
            string FactoryUD = null;
            string ArticleCode = null;
            string Description = null;
            int FactoryID = 0;
            if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
            {
                FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
            }
            if (filters.ContainsKey("SearchQuery") && !string.IsNullOrEmpty(filters["SearchQuery"].ToString()))
            {
                ProformaInvoiceNo = ClientUD = FactoryUD = ArticleCode = Description = filters["SearchQuery"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (LoadingPlanMngEntities context = CreateContext())
                {
                    totalRows = context.LoadingPlanMng_function_SearchSampleProduct(ProformaInvoiceNo, ClientUD, FactoryUD, ArticleCode, Description, FactoryID, orderBy, orderDirection).Count();
                    var result = context.LoadingPlanMng_function_SearchSampleProduct(ProformaInvoiceNo, ClientUD, FactoryUD, ArticleCode, Description, FactoryID, orderBy, orderDirection);
                    data = converter.DB2DTO_SampleProductSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }

            return data;
        }        

    }
}
