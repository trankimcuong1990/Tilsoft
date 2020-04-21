using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

namespace TilsoftService.Helper
{
    public class CommonHelper
    {
        public static bool ValidateDTO<T>(T dtoItem, out Library.DTO.Notification notification)
        {
            var context = new ValidationContext(dtoItem, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dtoItem, context, results, true);
            notification = new Library.DTO.Notification();

            if (!isValid)
            {
                notification.Type = Library.DTO.NotificationType.Warning;
                notification.Message = "Invalid input";
                foreach (ValidationResult vResult in results)
                {
                    notification.DetailMessage.Add(vResult.ErrorMessage);
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        public static string GetUniqueFileName(string originalFileName, string location)
        {
            location = GetPathWithSlash(location);
            string tmpFileName = originalFileName.Replace(" ", "_");
            int subfix = 1;
            FileInfo tmpFileNameInfo = new FileInfo(tmpFileName);
            while (File.Exists(location + tmpFileName))
            {
                tmpFileName = tmpFileNameInfo.Name.Replace(tmpFileNameInfo.Extension, "").Replace(".", "") + "_" + subfix + tmpFileNameInfo.Extension;
                subfix++;
            }
            return tmpFileName;
        }

        //
        // private function
        //
        private static string GetPathWithSlash(string originalPath)
        {
            if (originalPath.EndsWith(@"\")) return originalPath;
            return originalPath + @"\";
        }
    }
}