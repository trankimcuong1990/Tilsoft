using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Helper
{
    public static class AuthHelper
    {
        public static string GetActionButtonStatus(string moduleCode, ActionEnum action, HttpContext currentContext)
        {
            string result = "disabled";

            if (currentContext.Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION] == null)
            {
                return result;
            }

            APIDTO.APIUserPermission permission = ((APIDTO.APIUserInformation)currentContext.Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION]).Data.Permissions.FirstOrDefault(o => o.ModuleUD == moduleCode);
            var counter = ((APIDTO.APIUserInformation)currentContext.Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION]).Data.Permissions.Where(o => o.ModuleUD == moduleCode);
            if (permission == null)
            {
                return result;
            }
           
            switch (action)
                { 
                    case ActionEnum.Create:
                        result = permission.CanCreate ? "" : "disabled";
                        break;

                    case ActionEnum.Read:
                        result = permission.CanRead ? "" : "disabled";
                        break;

                    case ActionEnum.Update:
                        result = permission.CanUpdate ? "" : "disabled";
                        break;

                    case ActionEnum.Delete:
                        result = permission.CanDelete ? "" : "disabled";
                        break;

                    case ActionEnum.Print:
                        result = permission.CanPrint ? "" : "disabled";
                        break;

                    case ActionEnum.Approve:
                        result = permission.CanApprove ? "" : "disabled";
                        break;

                    case ActionEnum.Reset:
                        result = permission.CanReset ? "" : "disabled";
                        break;
                }

            return result;
        }


        public static APIDTO.APIUserInformation GetCurrentUserInfo(HttpContext currentContext)
        {
            Frontend.APIDTO.APIUserInformation userInfo = (Frontend.APIDTO.APIUserInformation)currentContext.Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            return userInfo;
        }
    }

    public enum ActionEnum
    { 
        Create,
        Read,
        Update,
        Delete,
        Print,
        Approve,
        Reset
    }
}