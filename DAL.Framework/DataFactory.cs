using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Framework
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private FrameworkEntities CreateContext()
        {
            return new FrameworkEntities(DALBase.Helper.CreateEntityConnectionString("FrameworkModel"));
        }

        public void WriteLog(int userId, int moduleId, string logMessage)
        {

        }

        public int GetUserID(string userName)
        {
            using (FrameworkEntities context = CreateContext())
            {
                return context.FW_UserProfile_View.FirstOrDefault(o => o.UserName == userName).UserId;
            }
        }

        public bool CanPerformAction(int userId, string moduleCode, DTO.Framework.ModuleAction action)
        {
            using (FrameworkEntities context = CreateContext())
            {
                FW_UserPermission_View permission = context.FW_UserPermission_View.FirstOrDefault(o => o.UserId == userId && o.CanRead.HasValue && o.CanRead.Value == true && o.ModuleUD == moduleCode);
                if (permission == null)
                { 
                    return false; 
                }
                else
                {
                    bool result = false;
                    switch (action)
                    { 
                        case DTO.Framework.ModuleAction.CanCreate:
                            if (permission.CanCreate.HasValue && permission.CanCreate.Value) result = true;
                            break;

                        case DTO.Framework.ModuleAction.CanRead:
                            if (permission.CanRead.HasValue && permission.CanRead.Value) result = true;
                            break;

                        case DTO.Framework.ModuleAction.CanUpdate:
                            if (permission.CanUpdate.HasValue && permission.CanUpdate.Value) result = true;
                            break;

                        case DTO.Framework.ModuleAction.CanDelete:
                            if (permission.CanDelete.HasValue && permission.CanDelete.Value) result = true;
                            break;

                        case DTO.Framework.ModuleAction.CanPrint:
                            if (permission.CanPrint.HasValue && permission.CanPrint.Value) result = true;
                            break;

                        case DTO.Framework.ModuleAction.CanApprove:
                            if (permission.CanApprove.HasValue && permission.CanApprove.Value) result = true;
                            break;

                        case DTO.Framework.ModuleAction.CanReset:
                            if (permission.CanReset.HasValue && permission.CanReset.Value) result = true;
                            break;
                    }

                    return result;
                }
            }
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
                    string outFileFullPath, outThumbnailFullPath, outDBFileLocation, outDBThumbnailLocation, outThumbnailFile, outResizedFile;
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

        public string CreateNoneImageFilePointer(string tmpFolder, string newFile, string oldFilePointer)
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
                            _file.FriendlyName = newFile;
                            _file.FileLocation = outDBFileLocation;
                            _file.FileExtension = info.Extension;
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
    }
}
