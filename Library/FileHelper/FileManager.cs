using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Library.FileHelper
{
    public class FileManager
    {
        private string _StoreFolder;

        public FileManager(string storeFolder)
        {
            _StoreFolder = storeFolder;
        }

        public bool StoreFile(string sourceFileFullPath, out string outputString, out string outputFullPath)
        {
            return StoreFile(_StoreFolder, sourceFileFullPath, out outputString, out outputFullPath);
        }

        public bool StoreFile(string storePath, string sourceFileFullPath, out string outputString, out string outputFullPath)
        {
            outputString = string.Empty;
            outputFullPath = string.Empty;

            if (!File.Exists(sourceFileFullPath))
            {
                return false;
            }

            try
            {
                FileInfo info = new FileInfo(sourceFileFullPath);
                string filePath = info.Name.Substring(0, 1).ToLower() + @"\" + DateTime.Now.ToString("ddMMyyyy") + @"\";
                string fileName = System.Guid.NewGuid().ToString().Replace("-", "").ToLower() + info.Extension;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(storePath + filePath);
                }

                File.Copy(sourceFileFullPath, storePath + filePath + fileName);

                outputString = filePath.Replace(@"\", "/") + fileName;
                outputFullPath = storePath + filePath + fileName;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public virtual void DeleteFile(string path)
        {
            try
            {
                path = _StoreFolder + path.Replace("/", @"\");
                File.Delete(path);
            }
            catch { }
        }
    }
}