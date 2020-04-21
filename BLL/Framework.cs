using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Framework
    {
        Module.Framework.DAL.DataFactory factory = new Module.Framework.DAL.DataFactory();

        public void WriteLog(int iRequesterID, int iModuleID, string iLogMessage)
        {
            if (FrameworkSetting.Setting.EnabledLog)
            {
                factory.WriteLog(iRequesterID, iModuleID, iLogMessage);
            }
        }

        public int GetUserID(string userName)
        {
            return factory.GetUserID(userName);
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
    }
}
