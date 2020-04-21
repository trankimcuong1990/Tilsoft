using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Framework
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public void WriteLog(int iRequesterID, int objectID, string iLogMessage)
        {
            if (FrameworkSetting.Setting.EnabledLog)
            {
                //factory.WriteLog(iRequesterID, objectID, iLogMessage);
            }
        }

        public void LogAction(int userId, string controllerName, string actionName, string parameters, string browserInfo, string ipAddress)
        {
            if (FrameworkSetting.Setting.EnabledLog)
            {
                factory.LogAction(userId, controllerName, actionName, parameters, browserInfo, ipAddress);
            }
        }

        public int GetUserID(string userName)
        {
            return factory.GetUserID(userName);
        }

        public DTO.UserInfoDTO GetUserInfo(string userName)
        {
            return factory.GetUserInfo(userName);
        }

        public bool CanPerformAction(int userId, string moduleCode, Library.DTO.ModuleAction action)
        {
            return factory.CanPerformAction(userId, moduleCode, action);
        }

        public bool UpdateFile(string fileUD, string friendlyName, string fileLocation, string fileExtension, string thumbnailLocation, int fileSize, out string returnUD)
        {
            return factory.UpdateFile(fileUD, friendlyName, fileLocation, fileExtension, thumbnailLocation, fileSize, out returnUD);
        }

        public bool RemoveFile(string fileUD)
        {
            return factory.RemoveFile(fileUD);
        }

        public int GetUserOffice(int userID)
        {
            return factory.GetUserOffice(userID);
        }

        public int GetUserGroup(int userID)
        {
            return factory.GetUserGroup(userID);
        }

        public int? GetCompanyID(int userID)
        {
            return factory.GetCompanyID(userID);
        }

        public List<DTO.SystemSettingDTO> GetSystemSettings(out Library.DTO.Notification notification)
        {
            return factory.GetSystemSettings(out notification);
        }

        public int? GetUserIDFromSecreteKey(string secreteKey, out Library.DTO.Notification notification)
        {
            return factory.GetUserIDFromSecreteKey(secreteKey, out notification);
        }
    }
}
