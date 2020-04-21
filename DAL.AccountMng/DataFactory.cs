using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DAL.AccountMng
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private AccountMngEntities CreateContext()
        {
            return new AccountMngEntities(DALBase.Helper.CreateEntityConnectionString("AccountMngModel"));
        }

        public DTO.AccountMng.User GetUserInformation(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AccountMngEntities context = CreateContext())
                {
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.UserId == userId);
                    dbUser.LastLoginDate = DateTime.Now;
                    context.SaveChanges();

                    AccountMng_UserProfile_View result = context.AccountMng_UserProfile_View
                        .Include("AccountMng_UserPermission_View")
                        .Include("AccountMng_Branch_View")
                        .FirstOrDefault(o => o.UserId == userId);

                    return converter.DB2DTO_User(result);
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new DTO.AccountMng.User();
            }
        }

        public DTO.AccountMng.User GetUserInformationLight(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AccountMngEntities context = CreateContext())
                {
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.UserId == userId);
                    dbUser.LastLoginDate = DateTime.Now;
                    context.SaveChanges();

                    AccountMng_UserProfile_View result = context.AccountMng_UserProfile_View.FirstOrDefault(o => o.UserId == userId);
                    if (result == null)
                    {
                        throw new Exception("User info not found!");
                    }
                    return new DTO.AccountMng.User()
                    {
                        FullName = result.FullName,
                        Email = result.Email,
                        UserName = result.UserName,
                        PersonalPhoto_DisplayUrl = !string.IsNullOrEmpty( result.PersonalPhoto_DisplayUrl) ? FrameworkSetting.Setting.MediaThumbnailUrl + result.PersonalPhoto_DisplayUrl : string.Empty,
                        PhoneNumber = result.PhoneNumber
                    };
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new DTO.AccountMng.User();
            }
        }

        public List<DTO.AccountMng.User> GetUserWithFilter(Hashtable iFilters, string iSortedBy, string iSortedDirection, int iPageSize, int iPageIndex, out int oTotalRow, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            oTotalRow = 0;
            string UserName = null;
            string UserUD = null;
            string FullName = null;
            string PhoneNumber = null;
            string Email = null;
            string SkypeID = null;
            int? OfficeID = null;

            if (iFilters.ContainsKey("UserName"))
            {
                UserName = iFilters["UserName"].ToString().Replace("'", "''");
            }

            if (iFilters.ContainsKey("UserUD"))
            {
                UserUD = iFilters["UserUD"].ToString().Replace("'", "''");
            }

            if (iFilters.ContainsKey("FullName"))
            {
                FullName = iFilters["FullName"].ToString().Replace("'", "''");
            }

            if (iFilters.ContainsKey("PhoneNumber"))
            {
                PhoneNumber = iFilters["PhoneNumber"].ToString().Replace("'", "''");
            }

            if (iFilters.ContainsKey("Email"))
            {
                Email = iFilters["Email"].ToString().Replace("'", "''");
            }

            if (iFilters.ContainsKey("SkypeID"))
            {
                SkypeID = iFilters["SkypeID"].ToString().Replace("'", "''");
            }

            if (iFilters.ContainsKey("OfficeID"))
            {
                OfficeID = Convert.ToInt32(iFilters["OfficeID"]);
            }

            //try to get data
            try
            {
                using (AccountMngEntities context = CreateContext())
                {
                    oTotalRow = context.AccountMng_function_SearchUserProfile(UserName, UserUD, FullName, PhoneNumber, Email, SkypeID, OfficeID, iSortedBy, iSortedDirection).Count();
                    var result = context.AccountMng_function_SearchUserProfile(UserName, UserUD, FullName, PhoneNumber, Email, SkypeID, OfficeID, iSortedBy, iSortedDirection);
                    return converter.DB2DTO_UserList(result.Skip(iPageSize * (iPageIndex - 1)).Take(iPageSize).ToList());
                }                
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new List<DTO.AccountMng.User>();
            }
        }

        public bool IsAccountDisabled(string aspNetUserId)
        {
            try
            {
                using (AccountMngEntities context = CreateContext())
                {
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.AspNetUsersId == aspNetUserId);
                    if (dbUser.IsActivated.HasValue && dbUser.IsActivated.Value)
                        return false;
                    else
                        return true;
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return false;
            }
        }

        public bool IsNeedToChangePassword(string aspNetUserId)
        {
            int monthPasswordOld = 3;
            
            try
            {
                using (AccountMngEntities context = CreateContext())
                {
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.AspNetUsersId == aspNetUserId);
                    if (dbUser == null)
                    {
                        return true;
                    }

                    // if force password changed
                    if (dbUser.IsForcedToChangePassword.HasValue && dbUser.IsForcedToChangePassword.Value)
                    {
                        return true;
                    }

                    // if password older than 3 months
                    if (!dbUser.LastPasswordChanged.HasValue)
                    {
                        return true;
                    }
                    else
                    {
                        if (Math.Floor((Decimal)(DateTime.Now.Subtract(dbUser.LastPasswordChanged.Value).Days / 31)) >= monthPasswordOld)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        public void UpdateLastPasswordChange(string aspNetUserId)
        {
            try
            {
                using (AccountMngEntities context = CreateContext())
                {
                    UserProfile dbUser = context.UserProfile.FirstOrDefault(o => o.AspNetUsersId == aspNetUserId);
                    if (dbUser != null)
                    {
                        dbUser.LastPasswordChanged = DateTime.Now;
                        dbUser.IsForcedToChangePassword = false;
                        context.SaveChanges();
                    }
                }
            }
            catch { }
        }
    }
}
