using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Module.UserFileMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FileSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                int UserID = -1;
                if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                {
                    UserID = Convert.ToInt32(filters["UserID"].ToString());
                }

                if (UserID == -1)
                {
                    throw new Exception("Unknow user!");
                }
                string userFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + UserID.ToString() + @"\";
                prepareStorageFolder(userFolder);

                DirectoryInfo dInfo = new DirectoryInfo(userFolder);
                string filename = string.Empty;
                string filePath = string.Empty;
                string thumbPath = string.Empty;
                string fileExtension = string.Empty;
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    filename = fInfo.Name;
                    fileExtension = fInfo.Extension;
                    switch (fileExtension.ToLower())
                    {
                        case ".jpg":
                        case ".bmp":
                        case ".gif":
                        case ".png":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/Temp/" + UserID.ToString() + "/" + filename;
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/Temp/" + UserID.ToString() + "/thumbnail/" + filename.Replace(fileExtension, ".jpg");
                            break;

                        case ".doc":
                        case ".docx":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/docx.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/docx.jpg"; 
                            break;                        

                        case ".mp4":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/mp4.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/mp4.jpg";
                            break;

                        case ".pdf":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/pdf.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/pdf.jpg";
                            break;

                        case ".rar":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/rar.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/rar.jpg";
                            break;

                        case ".txt":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/txt.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/txt.jpg";
                            break;

                        case ".xls":
                        case ".xlsx":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/xlsx.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/xlsx.jpg";
                            break;

                        case ".zip":
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/zip.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/zip.jpg";
                            break;

                        default:
                            filePath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/unknow.jpg";
                            thumbPath = FrameworkSetting.Setting.ServiceUrl + "Media/FileType/thumbnail/unknow.jpg";
                            break;
                    }
                    filePath = filePath + "?" + System.Guid.NewGuid().ToString().Replace("-", "");
                    thumbPath = thumbPath + "?" + System.Guid.NewGuid().ToString().Replace("-", "");
                    data.Data.Add(new DTO.FileSearchResult() { FileName = filename, FilePath = filePath, ThumbnailPath = thumbPath, AbsoluteURL = FrameworkSetting.Setting.MediaUrl + "Temp/" + UserID.ToString() + "/" + filename });
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
            throw new NotImplementedException();
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
        public bool RotateFile(int userId, string filename, int direction, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                string userFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                prepareStorageFolder(userFolder);
                Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager("", "");
                if (direction > 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }

                if (File.Exists(userFolder + filename))
                {
                    imgMng.RotateImage(userFolder + filename, 90 * direction);
                }
                if (File.Exists(userFolder + @"\thumbnail\" + filename))
                {
                    imgMng.RotateImage(userFolder + @"\thumbnail\" + filename, 90 * direction);
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

        public bool DeleteFile(int userId, string filename, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                string userFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                prepareStorageFolder(userFolder);

                if (File.Exists(userFolder + filename))
                {
                    File.Delete(userFolder + filename);
                }
                if (File.Exists(userFolder + @"\thumbnail\" + filename))
                {
                    File.Delete(userFolder + @"\thumbnail\" + filename);
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

        public bool DeleteFiles(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                string userFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                prepareStorageFolder(userFolder);

                DirectoryInfo dInfo = new DirectoryInfo(userFolder);
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    File.Delete(fInfo.FullName);
                }

                dInfo = new DirectoryInfo(userFolder + @"\thumbnail\");
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    File.Delete(fInfo.FullName);
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

        private void prepareStorageFolder(string path)
        {
            string userFolder = path;
            if (!Directory.Exists(userFolder))
            {
                Directory.CreateDirectory(userFolder);
            }
            if (!Directory.Exists(userFolder + @"\thumbnail\"))
            {
                Directory.CreateDirectory(userFolder + @"\thumbnail\");
            }
        }
    }
}
