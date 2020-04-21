using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Module.Framework.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private FrameworkEntities CreateContext()
        {
            return new FrameworkEntities(Library.Helper.CreateEntityConnectionString("DAL.FrameworkModel"));
        }

        public void WriteLog(int userId, int moduleId, string logMessage)
        {
            using (FrameworkEntities context = CreateContext())
            {
                // deprecated
            }
        }

        public void LogAction(int userId, string controllerName, string actionName, string parameters, string browserInfo, string ipAddress)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    ApplicationUsage dbItem = new ApplicationUsage();
                    dbItem.UserID = userId;
                    dbItem.ActionName = actionName;
                    dbItem.ActionTime = DateTime.Now;
                    dbItem.BrowserInfo = browserInfo;
                    dbItem.ControllerName = controllerName;
                    dbItem.IPAddress = ipAddress;
                    dbItem.Parameters = parameters;
                    context.ApplicationUsage.Add(dbItem);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        public int GetUserID(string userName)
        {
            using (FrameworkEntities context = CreateContext())
            {
                return context.FW_UserProfile_View.FirstOrDefault(o => o.UserName == userName).UserId;
            }
        }

        public Module.Framework.DTO.UserInfoDTO GetUserInfo(string userName)
        {
            Module.Framework.DTO.UserInfoDTO data = new DTO.UserInfoDTO();
            using (FrameworkEntities context = CreateContext())
            {
                var dbItem = context.FW_UserProfile_View.FirstOrDefault(o => o.UserName == userName);
                data.UserID = dbItem.UserId;
                data.UserCompanyID = dbItem.CompanyID;
                data.UserBranchID = dbItem.BranchID;
                data.UserFactoryID = dbItem.FactoryID;
                data.UserCientID = dbItem.ClientID;
                return data;
            }
        }

        public int GetUserGroup(int userId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                return context.FW_UserProfile_View.FirstOrDefault(o => o.UserId == userId).UserGroupID.Value;
            }
        }

        public bool HasSpecialPermission(int userId, string specialPermissionUD)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    if (context.FW_ModuleSpecialPermissionAccess_View.Count(o => o.UserID == userId && o.ModuleSpecialPermissionUD == specialPermissionUD) > 0)
                    {
                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        public int GetUserOffice(int userId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                return context.FW_UserProfile_View.FirstOrDefault(o => o.UserId == userId).OfficeID;
            }
        }

        public int? GetCompanyID(int userId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                return context.FW_UserProfile_View.FirstOrDefault(o => o.UserId == userId).CompanyID;
            }
        }

        public List<int> GetCompanyIDs(int userId)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    return context.FW_UserCompanyAccess_View.Where(o => o.UserID == userId).Select(o => o.CompanyID.Value).ToList();
                }
            }
            catch { }

            return new List<int>();
        }

        public int IsInternalUser(int userId)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    return context.FW_function_CheckIfUserIsFromFactory(userId).FirstOrDefault().Value;
                }
            }
            catch { }
            return 0;
        }

        public bool CanPerformAction(int userId, string moduleCode, Library.DTO.ModuleAction action)
        {
            List<Module.Framework.DAL.FW_UserPermission_View> permissions = new List<FW_UserPermission_View>();
            List<Module.Framework.DAL.FW_UserGroupPermission_View> gPermissions = new List<FW_UserGroupPermission_View>();
            Module.Framework.DAL.FW_UserPermission_View permission;
            Module.Framework.DAL.FW_UserGroupPermission_View gPermission;

            try
            {
                object cacheObject = Library.CacheHelper.GetData(Library.CacheHelper.USER_DATA + userId.ToString());
                if (cacheObject == null)
                {
                    using (FrameworkEntities context = CreateContext())
                    {
                        permissions = context.FW_UserPermission_View.Where(o => o.UserId == userId).ToList();
                        gPermissions = context.FW_UserGroupPermission_View.Where(o => o.UserId == userId).ToList();
                        foreach (Module.Framework.DAL.FW_UserGroupPermission_View oGPermission in gPermissions)
                        {
                            permission = permissions.FirstOrDefault(o => o.ModuleID == oGPermission.ModuleID);
                            if (permission == null)
                            {
                                permissions.Add(new FW_UserPermission_View()
                                {
                                    ModuleID = oGPermission.ModuleID,
                                    CanApprove = oGPermission.CanApprove,
                                    CanCreate = oGPermission.CanCreate,
                                    CanDelete = oGPermission.CanDelete,
                                    CanPrint = oGPermission.CanPrint,
                                    CanRead = oGPermission.CanRead,
                                    CanReset = oGPermission.CanReset,
                                    CanUpdate = oGPermission.CanUpdate,
                                    Description = oGPermission.Description,
                                    DisplayImage = oGPermission.DisplayImage,
                                    DisplayOrder = oGPermission.DisplayOrder,
                                    DisplayText = oGPermission.DisplayText,
                                    ModuleUD = oGPermission.ModuleUD,
                                    ParentID = oGPermission.ParentID,
                                    URLLink = oGPermission.URLLink,
                                    UserId = oGPermission.UserId
                                });
                            }
                        }
                        foreach (Module.Framework.DAL.FW_UserPermission_View oPermission in permissions)
                        {
                            gPermission = gPermissions.FirstOrDefault(o => o.ModuleID == oPermission.ModuleID);
                            if (gPermission != null)
                            {
                                if (gPermission.CanCreate.HasValue && gPermission.CanCreate.Value)
                                {
                                    oPermission.CanCreate = gPermission.CanCreate;
                                }
                                if (gPermission.CanRead.HasValue && gPermission.CanRead.Value)
                                {
                                    oPermission.CanRead = gPermission.CanRead;
                                }
                                if (gPermission.CanUpdate.HasValue && gPermission.CanUpdate.Value)
                                {
                                    oPermission.CanUpdate = gPermission.CanUpdate;
                                }
                                if (gPermission.CanDelete.HasValue && gPermission.CanDelete.Value)
                                {
                                    oPermission.CanDelete = gPermission.CanDelete;
                                }
                                if (gPermission.CanApprove.HasValue && gPermission.CanApprove.Value)
                                {
                                    oPermission.CanApprove = gPermission.CanApprove;
                                }
                                if (gPermission.CanReset.HasValue && gPermission.CanReset.Value)
                                {
                                    oPermission.CanReset = gPermission.CanReset;
                                }
                                if (gPermission.CanPrint.HasValue && gPermission.CanPrint.Value)
                                {
                                    oPermission.CanPrint = gPermission.CanPrint;
                                }
                            }
                        }
                    }
                    Library.CacheHelper.SetData(Library.CacheHelper.USER_DATA + userId.ToString(), permissions);
                }
                else
                {
                    permissions = (List<Module.Framework.DAL.FW_UserPermission_View>)cacheObject;
                }
            }
            catch { }

            permission = permissions.FirstOrDefault(o => o.ModuleUD == moduleCode);
            bool result = false;
            if (permission != null)
            {
                switch (action)
                {
                    case Library.DTO.ModuleAction.CanCreate:
                        if (permission.CanCreate.HasValue && permission.CanCreate.Value) result = true;
                        break;

                    case Library.DTO.ModuleAction.CanRead:
                        if (permission.CanRead.HasValue && permission.CanRead.Value) result = true;
                        break;

                    case Library.DTO.ModuleAction.CanUpdate:
                        if (permission.CanUpdate.HasValue && permission.CanUpdate.Value) result = true;
                        break;

                    case Library.DTO.ModuleAction.CanDelete:
                        if (permission.CanDelete.HasValue && permission.CanDelete.Value) result = true;
                        break;

                    case Library.DTO.ModuleAction.CanPrint:
                        if (permission.CanPrint.HasValue && permission.CanPrint.Value) result = true;
                        break;

                    case Library.DTO.ModuleAction.CanApprove:
                        if (permission.CanApprove.HasValue && permission.CanApprove.Value) result = true;
                        break;

                    case Library.DTO.ModuleAction.CanReset:
                        if (permission.CanReset.HasValue && permission.CanReset.Value) result = true;
                        break;
                }
            }
            return result;
        }

        // file operation
        public bool UpdateFile(string fileUD, string friendlyName, string fileLocation, string fileExtension, string thumbnailLocation, int fileSize, out string returnUD)
        {
            returnUD = string.Empty;

            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    Files _file = context.Files.FirstOrDefault(o => o.FileUD == fileUD);
                    if (_file == null)
                    {
                        _file = new Files();
                        _file.FileUD = System.Guid.NewGuid().ToString().ToLower().Replace("-", "");
                        context.Files.Add(_file);
                    }
                    _file.FriendlyName = friendlyName;
                    _file.FileLocation = fileLocation;
                    _file.FileExtension = fileExtension;
                    _file.ThumbnailLocation = thumbnailLocation;
                    _file.FileSize = fileSize;

                    context.SaveChanges();

                    returnUD = _file.FileUD;
                    return true;
                }
                catch { return false; }
            }
        }

        public bool RemoveFile(string fileUD)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    Files _file = context.Files.FirstOrDefault(o => o.FileUD == fileUD);
                    if (_file != null)
                    {
                        context.Files.Remove(_file);
                        context.SaveChanges();
                    }
                    return true;
                }
                catch { return false; }
            }
        }

        public bool RemoveImageFile(string fileUD)
        {
            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    Files _file = context.Files.FirstOrDefault(o => o.FileUD == fileUD);
                    if (_file != null)
                    {
                        if (!string.IsNullOrEmpty(_file.FileLocation))
                        {
                            try
                            {
                                imgMng.DeleteFile(_file.FileLocation);
                            }
                            catch { }
                        }

                        if (!string.IsNullOrEmpty(_file.ThumbnailLocation))
                        {
                            try
                            {
                                imgMng.DeleteFile(_file.ThumbnailLocation);
                            }
                            catch { }
                        }

                        context.Files.Remove(_file);
                        context.SaveChanges();
                    }
                    return true;
                }
                catch { return false; }
            }
        }

        public void GetDBFileLocation(string fileUD, out string fileLocation, out string thumbnailLocation)
        {
            fileLocation = string.Empty;
            thumbnailLocation = string.Empty;

            using (FrameworkEntities context = CreateContext())
            {
                Files _file = context.Files.FirstOrDefault(o => o.FileUD == fileUD);
                if (_file != null)
                {
                    fileLocation = _file.FileLocation;
                    thumbnailLocation = _file.ThumbnailLocation;
                }
            }
        }

        public string CreateFilePointer(string tmpFolder, string newFile, string oldFilePointer)
        {
            string result = string.Empty;
            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

            using (FrameworkEntities context = CreateContext())
            {
                // delete phisycal files
                Files _file = context.Files.FirstOrDefault(o => o.FileUD == oldFilePointer);
                if (_file != null)
                {
                    if (!string.IsNullOrEmpty(_file.FileLocation))
                    {
                        try
                        {
                            imgMng.DeleteFile(_file.FileLocation);
                        }
                        catch { }
                        _file.FileLocation = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(_file.ThumbnailLocation))
                    {
                        try
                        {
                            imgMng.DeleteFile(_file.ThumbnailLocation);
                        }
                        catch { }
                        _file.ThumbnailLocation = string.Empty;
                    }

                    // remove the file pointer if new file is empty
                    if (string.IsNullOrEmpty(newFile))
                    {
                        // remove file registration in database
                        context.Files.Remove(_file);
                        context.SaveChanges();

                        result = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(newFile))
                {
                    // assign new file
                    string outFileFullPath, outThumbnailFullPath, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile;
                    outDBThumbnailLocation = "";

                    // copy the new file
                    imgMng.StoreFile(tmpFolder + newFile, out outDBFileLocation, out outFileFullPath);

                    // copy the thumbnail
                    imgMng.CreateThumbnail(tmpFolder + newFile, out outThumbnailFile, 350, 350);
                    if (!string.IsNullOrEmpty(outThumbnailFile))
                    {
                        imgMng.StoreThumbnail(outThumbnailFile, out outDBThumbnailLocation, out outThumbnailFullPath);
                    }

                    if (File.Exists(outFileFullPath))
                    {
                        FileInfo info = new FileInfo(outFileFullPath);

                        // insert/update file registration in database
                        try
                        {
                            if (_file == null)
                            {
                                _file = new Files();
                                _file.FileUD = System.Guid.NewGuid().ToString().ToLower().Replace("-", "");
                                context.Files.Add(_file);
                            }
                            _file.FriendlyName = newFile;
                            _file.FileLocation = outDBFileLocation;
                            _file.FileExtension = info.Extension;
                            _file.ThumbnailLocation = outDBThumbnailLocation;
                            _file.FileSize = (int)info.Length;

                            context.SaveChanges();

                            result = _file.FileUD;
                        }
                        catch
                        {
                            result = string.Empty;
                        }
                    }
                }
            }

            return result;
        }

        public string CreateFilePointer(string tmpFolder, string newFile, string oldFilePointer, string friendlyName)
        {
            string result = string.Empty;
            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

            using (FrameworkEntities context = CreateContext())
            {
                // delete phisycal files
                Files _file = context.Files.FirstOrDefault(o => o.FileUD == oldFilePointer);
                if (_file != null)
                {
                    if (!string.IsNullOrEmpty(_file.FileLocation))
                    {
                        try
                        {
                            imgMng.DeleteFile(_file.FileLocation);
                        }
                        catch { }
                        _file.FileLocation = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(_file.ThumbnailLocation))
                    {
                        try
                        {
                            imgMng.DeleteFile(_file.ThumbnailLocation);
                        }
                        catch { }
                        _file.ThumbnailLocation = string.Empty;
                    }

                    // remove the file pointer if new file is empty
                    if (string.IsNullOrEmpty(newFile))
                    {
                        // remove file registration in database
                        context.Files.Remove(_file);
                        context.SaveChanges();

                        result = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(newFile))
                {
                    // assign new file
                    string outFileFullPath, outThumbnailFullPath, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile;
                    outDBThumbnailLocation = "";

                    // copy the new file
                    imgMng.StoreFile(tmpFolder + newFile, out outDBFileLocation, out outFileFullPath);

                    // copy the thumbnail
                    imgMng.CreateThumbnail(tmpFolder + newFile, out outThumbnailFile, 350, 350);
                    if (!string.IsNullOrEmpty(outThumbnailFile))
                    {
                        imgMng.StoreThumbnail(outThumbnailFile, out outDBThumbnailLocation, out outThumbnailFullPath);
                    }

                    if (File.Exists(outFileFullPath))
                    {
                        FileInfo info = new FileInfo(outFileFullPath);

                        // insert/update file registration in database
                        try
                        {
                            if (_file == null)
                            {
                                _file = new Files();
                                _file.FileUD = System.Guid.NewGuid().ToString().ToLower().Replace("-", "");
                                context.Files.Add(_file);
                            }
                            _file.FriendlyName = friendlyName;
                            _file.FileLocation = outDBFileLocation;
                            _file.FileExtension = info.Extension;
                            _file.ThumbnailLocation = outDBThumbnailLocation;
                            _file.FileSize = (int)info.Length;

                            context.SaveChanges();

                            result = _file.FileUD;
                        }
                        catch
                        {
                            result = string.Empty;
                        }
                    }
                }
            }

            return result;
        }

        public string CreateNoneImageFilePointer(string tmpFolder, string newFile, string oldFilePointer, string friendlyName)
        {
            string result = string.Empty;
            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

            using (FrameworkEntities context = CreateContext())
            {
                // delete phisycal files
                Files _file = context.Files.FirstOrDefault(o => o.FileUD == oldFilePointer);
                if (_file != null)
                {
                    if (!string.IsNullOrEmpty(_file.FileLocation))
                    {
                        try
                        {
                            imgMng.DeleteFile(_file.FileLocation);
                        }
                        catch { }
                        _file.FileLocation = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(_file.ThumbnailLocation))
                    {
                        try
                        {
                            imgMng.DeleteFile(_file.ThumbnailLocation);
                        }
                        catch { }
                        _file.ThumbnailLocation = string.Empty;
                    }

                    // remove the file pointer if new file is empty
                    if (string.IsNullOrEmpty(newFile))
                    {
                        // remove file registration in database
                        context.Files.Remove(_file);
                        context.SaveChanges();

                        result = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(newFile))
                {
                    // assign new file
                    string outFileFullPath, outDBFileLocation;

                    // copy the new file
                    imgMng.StoreFile(tmpFolder + newFile, out outDBFileLocation, out outFileFullPath);

                    if (File.Exists(outFileFullPath))
                    {
                        FileInfo info = new FileInfo(outFileFullPath);

                        // insert/update file registration in database
                        try
                        {
                            if (_file == null)
                            {
                                _file = new Files();
                                _file.FileUD = System.Guid.NewGuid().ToString().ToLower().Replace("-", "");
                                context.Files.Add(_file);
                            }
                            if (string.IsNullOrEmpty(friendlyName))
                            {
                                _file.FriendlyName = newFile;
                            }
                            else
                            {
                                _file.FriendlyName = friendlyName;
                            }
                            _file.FileLocation = outDBFileLocation;
                            _file.FileExtension = info.Extension.Replace(".", "");
                            _file.FileSize = (int)info.Length;

                            context.SaveChanges();

                            result = _file.FileUD;
                        }
                        catch (Exception)
                        {
                            result = string.Empty;
                        }
                    }
                }
            }

            return result;
        }

        public string CreateNoneImageFilePointer(string tmpFolder, string newFile, string oldFilePointer)
        {
            return CreateNoneImageFilePointer(tmpFolder, newFile, oldFilePointer, string.Empty);
        }

        //
        // permission function
        //
        public int CheckBookingPermission(int userId, int bookingId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckBookingPermission(userId, bookingId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryOrderDetailPermission(int userId, int factoryOrderDetailId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryOrderDetailPermission(userId, factoryOrderDetailId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryOrderSparepartDetailPermission(int userId, int factoryOrderSparepartDetailId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryOrderSparepartDetailPermission(userId, factoryOrderSparepartDetailId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckLoadingPlanPermission(int userId, int loadingPlanId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckLoadingPlanPermission(userId, loadingPlanId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckPLCPermission(int userId, int plcId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckPLCPermission(userId, plcId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckQuotationPermission(int userId, int quotationId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckQuotationPermission(userId, quotationId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckModelPricePermission(int userId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckModelPricePermission(userId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckSupplierPermission(int userId, int supplierId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckSupplierPermission(userId, supplierId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryInvoicePermission(int userId, int factoryInvoiceId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryInvoicePermission(userId, factoryInvoiceId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryCreditNotePermission(int userId, int factoryCreditNoteId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryCreditNotePermission(userId, factoryCreditNoteId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryPaymentPermission(int userId, int factoryPaymentID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryPayment2Permission(userId, factoryPaymentID).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryProformaInvoicePermission(int userId, int factoryProformaInvoiceID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryProformaInvoicePermission(userId, factoryProformaInvoiceID).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryPermission(int userId, int factoryId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryPermission(userId, factoryId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckFactoryOrderPermission(int userId, int factoryOrderId)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckFactoryOrderPermission(userId, factoryOrderId).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckPurchasingInvoicePermission(int userId, int purchasingInvoiceID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckPurchasingInvoicePermission(userId, purchasingInvoiceID).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckDPBalancePermission(int userId, int factoryPayment2BalanceID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckDPBalancePermission(userId, factoryPayment2BalanceID).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckModelPermission(int userId, int modelId)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    return context.FW_function_CheckModelPermission(userId, modelId).FirstOrDefault().Value;
                }
            }
            catch
            {
                return 0;
            }
        }

        public bool IsDPBalanceClosed(int userId, int supplierId, string season)
        {
            bool result = true;
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    FW_DPBalance_View dbBalance = context.FW_function_getDPBalance(userId, season, supplierId).FirstOrDefault();
                    if (dbBalance != null && (!dbBalance.isClosed.HasValue || !dbBalance.isClosed.Value))
                    {
                        result = false;
                    }
                }
            }
            catch
            {
                result = true;
            }

            return result;
        }

        public List<int?> GetListSupplierByUser(int userID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_UserFactoryAccess_View.Where(o => o.UserID == userID).Select(x => x.SupplierID).ToList();
                }
                catch { }
            }
            return new List<int?>();
        }

        public List<int?> GetListFactoryByUser(int userID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_UserFactoryAccess_View.Where(o => o.UserID == userID).Select(x => x.FactoryID).ToList();
                }
                catch { }
            }
            return new List<int?>();
        }

        public int CheckPurchasingCreditNotePermission(int userId, int purchasingCreditNoteID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckPurchasingCreditNotePermission(userId, purchasingCreditNoteID).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }

        public int CheckPackingListPermission(int userId, int packingListID)
        {
            using (FrameworkEntities context = CreateContext())
            {
                try
                {
                    return context.FW_function_CheckPackingListPermission(userId, packingListID).FirstOrDefault().Value;
                }
                catch { }
            }
            return 0;
        }


        //
        // Description: data tracking detail
        // Author: The My
        //
        public void TrackingDataChange(DbContext toBeTracedContext, string moduleUD, int userId, out List<DTO.EntityTracking> NewlyCreatedEntities)
        {
            string originalValueText = string.Empty;
            string currentValueText = string.Empty;
            string tableName = string.Empty;
            string keyField = string.Empty;
            NewlyCreatedEntities = new List<DTO.EntityTracking>();

            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    DAL.DataTracking dbTracking = new DataTracking();
                    context.DataTracking.Add(dbTracking);
                    dbTracking.ModuleUD = moduleUD;
                    dbTracking.UserID = userId;
                    dbTracking.TrackingDate = DateTime.Now;
                    foreach (var entry in toBeTracedContext.ChangeTracker.Entries().Where(o => o.State != EntityState.Unchanged && o.State != EntityState.Detached))
                    {
                        originalValueText = currentValueText = tableName = keyField = string.Empty;
                        GetDBInfo(toBeTracedContext, entry, out keyField, out tableName);

                        DAL.DataTrackingAction dbTrackingAction = new DataTrackingAction();
                        dbTracking.DataTrackingAction.Add(dbTrackingAction);
                        dbTrackingAction.TableName = tableName;
                        dbTrackingAction.PrimaryKeyName = keyField;
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                dbTrackingAction.ActionPerformed = "C";
                                NewlyCreatedEntities.Add(new DTO.EntityTracking() { DataTrackingActionObj = dbTrackingAction, ToBeTrackedEntity = entry });
                                foreach (string propName in entry.CurrentValues.PropertyNames)
                                {
                                    currentValueText = string.Empty;
                                    if (entry.CurrentValues[propName] != null)
                                    {
                                        currentValueText = entry.CurrentValues[propName].ToString();
                                    }

                                    DAL.DataTrackingDetail dbTrackingDetail = new DataTrackingDetail();
                                    dbTrackingAction.DataTrackingDetail.Add(dbTrackingDetail);
                                    dbTrackingDetail.ColumnName = propName;
                                    dbTrackingDetail.NewValue = currentValueText;
                                }
                                break;

                            case EntityState.Deleted:
                                dbTrackingAction.ActionPerformed = "D";
                                dbTrackingAction.PrimaryKeyValue = Convert.ToInt32(entry.OriginalValues[keyField]);
                                foreach (string propName in entry.OriginalValues.PropertyNames)
                                {
                                    originalValueText = string.Empty;
                                    if (entry.OriginalValues[propName] != null)
                                    {
                                        originalValueText = entry.OriginalValues[propName].ToString();
                                    }

                                    DAL.DataTrackingDetail dbTrackingDetail = new DataTrackingDetail();
                                    dbTrackingAction.DataTrackingDetail.Add(dbTrackingDetail);
                                    dbTrackingDetail.ColumnName = propName;
                                    dbTrackingDetail.OldValue = originalValueText;
                                }
                                break;

                            case EntityState.Modified:
                                dbTrackingAction.ActionPerformed = "U";
                                dbTrackingAction.PrimaryKeyValue = Convert.ToInt32(entry.CurrentValues[keyField]);
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
                                        if (originalValue != null)
                                        {
                                            originalValueText = originalValue.ToString();
                                        }
                                        if (currentValue != null)
                                        {
                                            currentValueText = currentValue.ToString();
                                        }

                                        DAL.DataTrackingDetail dbTrackingDetail = new DataTrackingDetail();
                                        dbTrackingAction.DataTrackingDetail.Add(dbTrackingDetail);
                                        dbTrackingDetail.ColumnName = propName;
                                        dbTrackingDetail.OldValue = originalValueText;
                                        dbTrackingDetail.NewValue = currentValueText;
                                    }
                                }
                                break;
                        }
                    }
                    context.SaveChanges();
                    NewlyCreatedEntities.ForEach(o => o.DataTrackingActionID = o.DataTrackingActionObj.DataTrackingActionID);
                }
            }
            catch (Exception) { }
        }

        public void UpdateDataTracking(DbContext toBeTracedContext, List<DTO.EntityTracking> NewlyCreatedEntities)
        {
            string tableName = string.Empty;
            string keyField = string.Empty;

            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    foreach (DTO.EntityTracking eTracking in NewlyCreatedEntities)
                    {
                        DataTrackingAction action = context.DataTrackingAction.FirstOrDefault(o => o.DataTrackingActionID == eTracking.DataTrackingActionID);
                        if (action != null)
                        {
                            tableName = keyField = string.Empty;
                            GetDBInfo(toBeTracedContext, eTracking.ToBeTrackedEntity, out keyField, out tableName);
                            action.PrimaryKeyValue = Convert.ToInt32(eTracking.ToBeTrackedEntity.CurrentValues[keyField]);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception) { }
        }

        private void GetDBInfo(DbContext toBeTracedContext, DbEntityEntry entry, out string keyField, out string tableName)
        {
            var objectContext = ((IObjectContextAdapter)toBeTracedContext).ObjectContext;
            Type t = ObjectContext.GetObjectType(entry.Entity.GetType());
            MethodInfo m = (objectContext.GetType().GetMethod("CreateObjectSet", new Type[] { })).MakeGenericMethod(t);
            object set = m.Invoke(objectContext, null);
            PropertyInfo entitySetPI = set.GetType().GetProperty("EntitySet");
            EntitySet entitySet = (EntitySet)entitySetPI.GetValue(set, null);
            keyField = entitySet.ElementType.KeyMembers.FirstOrDefault().Name;
            tableName = entitySet.Name;
        }
        //
        // end
        //

        //
        // Author: TheMy
        // Description: create image cache
        //
        public bool CreateImageCache(string fileUD, int iWidth, int iHeight, bool originalFile)
        {
            // check if width or height is invalid
            if (iWidth < 0 || iHeight < 0)
            {
                return false;
            }

            // create cache folder
            string fileCacheFolder = FrameworkSetting.Setting.AbsoluteBaseFolder + @"\Media\Cache\File\";
            string thumbnailCacheFolder = FrameworkSetting.Setting.AbsoluteBaseFolder + @"\Media\Cache\Thumbnail\";
            if (iWidth > 0 && iHeight > 0)
            {
                fileCacheFolder = fileCacheFolder + iWidth.ToString() + "x" + iHeight.ToString() + @"\";
                if (!Directory.Exists(fileCacheFolder))
                {
                    Directory.CreateDirectory(fileCacheFolder);
                }

                thumbnailCacheFolder = thumbnailCacheFolder + iWidth.ToString() + "x" + iHeight.ToString() + @"\";
                if (!Directory.Exists(thumbnailCacheFolder))
                {
                    Directory.CreateDirectory(thumbnailCacheFolder);
                }
            }

            // return the phisical file if exists
            if (originalFile)
            {
                if (File.Exists(fileCacheFolder + fileUD + ".jpg"))
                {
                    return true;
                }
            }
            else
            {
                if (File.Exists(thumbnailCacheFolder + fileUD + ".jpg"))
                {
                    return true;
                }
            }

            // create the file if needed
            using (FrameworkEntities context = CreateContext())
            {
                Library.FileHelper.ImageManager iMng = new Library.FileHelper.ImageManager("", "");
                Files dbFile = context.Files.FirstOrDefault(o => o.FileUD == fileUD);
                if (dbFile != null)
                {
                    string outputFile = string.Empty;
                    if (originalFile)
                    {
                        if (!string.IsNullOrEmpty(dbFile.FileLocation))
                        {
                            outputFile = iMng.resizeImage(FrameworkSetting.Setting.AbsoluteFileFolder + dbFile.FileLocation, iWidth, iHeight);
                            if (!string.IsNullOrEmpty(outputFile))
                            {
                                File.Copy(outputFile, fileCacheFolder + fileUD + ".jpg");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dbFile.ThumbnailLocation))
                        {
                            outputFile = iMng.resizeImage(FrameworkSetting.Setting.AbsoluteThumbnailFolder + dbFile.ThumbnailLocation, iWidth, iHeight);
                            if (!string.IsNullOrEmpty(outputFile))
                            {
                                File.Copy(outputFile, thumbnailCacheFolder + fileUD + ".jpg");
                            }
                        }
                    }

                    if (File.Exists(thumbnailCacheFolder + fileUD + ".jpg"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //
        // systemsetting
        //
        public string GetSystemSetting(string settingKey, string season)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    var dbItem = context.SystemSetting.FirstOrDefault(o => o.SettingKey == settingKey && o.Season == season);
                    if (dbItem != null)
                    {
                        return dbItem.SettingValue;
                    }
                }
            }
            catch (Exception)
            {

            }

            return string.Empty;
        }

        public List<DTO.SystemSettingDTO> GetSystemSettings(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.SystemSettingDTO> data = new List<DTO.SystemSettingDTO>();
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    data = converter.DB2DTO_SystemSettingDTO(context.FW_SystemSetting_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public bool UpdateSystemSetting(List<DTO.SystemSettingDTO> dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FrameworkEntities context = CreateContext())
                {

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return false;
        }

        public bool HasPermissionCanRead(int userID, string moduleUDs, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.Framework_function_GetPermissionByModule(moduleUDs, userID).FirstOrDefault();
                    if (dbItem == null || !dbItem.HasValue)
                    {
                        return false;
                    }

                    return dbItem.Value;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public int? GetUserIDFromSecreteKey(string secreteKey, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.FW_UserProfile_View.FirstOrDefault(o => o.APIKey == secreteKey);
                    if (dbItem != null)
                    {
                        return dbItem.UserId;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return null;
        }

        public void SendEmailNotificationBasedOn(string moduleCode, int id, string subjectEmail, int statusID)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    // Get information notification with status
                    var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == moduleCode && o.StatusID == statusID).ToList();

                    int i = 0;
                    int? notificationGroupID = null;

                    foreach (var groupStatus in groupStatuses)
                    {
                        if (i == 0 || groupStatus.NotificationGroupID != notificationGroupID)
                        {
                            notificationGroupID = groupStatus.NotificationGroupID;

                            if (groupStatus != null)
                            {
                                EmailNotificationMessage dbEmail = new EmailNotificationMessage();

                                // Create subject email
                                dbEmail.EmailSubject = subjectEmail;

                                // Create email body
                                dbEmail.EmailBody = FrameworkSetting.Setting.FrontendUrl + groupStatus.URLLink + "/Edit/" + id.ToString();

                                if (!string.IsNullOrEmpty(groupStatus.EmailTemplate))
                                {
                                    dbEmail.EmailBody = "Please check: "+ groupStatus.EmailTemplate + id.ToString();
                                }

                                // Create send to
                                dbEmail.SendTo = null;

                                var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == notificationGroupID);
                                foreach (var groupMember in dbGroupMember.ToList())
                                {
                                    if (!string.IsNullOrEmpty(dbEmail.SendTo))
                                    {
                                        dbEmail.SendTo = dbEmail.SendTo + ";";
                                    }

                                    dbEmail.SendTo = dbEmail.SendTo + groupMember.Email;
                                                                      
                                }

                                context.EmailNotificationMessage.Add(dbEmail);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SendEmailNotificationBasedOn2(string moduleCode, int id, string subjectEmail, int statusID, int? subID = null)
        {
            try
            {
                using (FrameworkEntities context = CreateContext())
                {
                    // Get information notification with status
                    var groupStatuses = context.FW_NotificationGroupStatus_View.Where(o => o.ModuleUD == moduleCode && o.StatusID == statusID).ToList();

                    int i = 0;
                    int? notificationGroupID = null;

                    foreach (var groupStatus in groupStatuses)
                    {
                        if (i == 0 || groupStatus.NotificationGroupID != notificationGroupID)
                        {
                            notificationGroupID = groupStatus.NotificationGroupID;

                            if (groupStatus != null)
                            {
                                EmailNotificationMessage dbEmail = new EmailNotificationMessage();

                                // Create subject email
                                dbEmail.EmailSubject = subjectEmail;

                                // Create email body
                                dbEmail.EmailBody = FrameworkSetting.Setting.FrontendUrl + groupStatus.URLLink + "/Edit/" + id.ToString();
                                if (subID.HasValue)
                                {
                                    dbEmail.EmailBody = dbEmail.EmailBody + "/" + subID.Value.ToString();
                                }

                                if (!string.IsNullOrEmpty(groupStatus.EmailTemplate))
                                {
                                    dbEmail.EmailBody = dbEmail.EmailBody + Environment.NewLine + groupStatus.EmailTemplate + id.ToString();
                                }

                                // Create send to
                                dbEmail.SendTo = null;

                                var dbGroupMember = context.FW_NotificationGroupMember_View.Where(o => o.NotificationGroupID == notificationGroupID);
                                foreach (var groupMember in dbGroupMember.ToList())
                                {
                                    if (!string.IsNullOrEmpty(dbEmail.SendTo))
                                    {
                                        dbEmail.SendTo = dbEmail.SendTo + ";";
                                    }

                                    dbEmail.SendTo = dbEmail.SendTo + groupMember.Email;                                    
                                }

                                context.EmailNotificationMessage.Add(dbEmail);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
