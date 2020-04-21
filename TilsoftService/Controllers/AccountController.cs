using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.IO;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using TilsoftService.Providers;
using Microsoft.Owin;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        Module.Framework.BLL bll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getuserinfo")]
        public IHttpActionResult GetUserInfo()
        {
            int userId = ControllerContext.GetAuthUserId();
            // clear temp file
            try
            {
                string userFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                if (!Directory.Exists(userFolder))
                {
                    Directory.CreateDirectory(userFolder);
                }
                if (!Directory.Exists(userFolder + @"\thumbnail\"))
                {
                    Directory.CreateDirectory(userFolder + @"\thumbnail\");
                }

                DirectoryInfo dInfo = new DirectoryInfo(userFolder);
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    if (fInfo.CreationTime.AddDays(1).CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(fInfo.FullName);
                    }
                }

                dInfo = new DirectoryInfo(userFolder + @"\thumbnail\");
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    if (fInfo.CreationTime.AddDays(1).CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(fInfo.FullName);
                    }
                }
            }
            catch { }
            // 

            BLL.AccountMng bll = new BLL.AccountMng();
            Library.DTO.Notification notification;
            DTO.AccountMng.User userInfo = bll.GetUserInformation(userId, out notification);

            // implement mem cache - clear memcache
            Library.CacheHelper.ClearCache(Library.CacheHelper.USER_DATA + userId.ToString());

            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.AccountMng.User>() { Data = userInfo, Message = notification });
        }

        [HttpPost]
        [Route("get-user-info-light")]
        public IHttpActionResult GetUserInfoLight()
        {
            int userId = ControllerContext.GetAuthUserId();
            // clear temp file
            try
            {
                string userFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                if (!Directory.Exists(userFolder))
                {
                    Directory.CreateDirectory(userFolder);
                }
                if (!Directory.Exists(userFolder + @"\thumbnail\"))
                {
                    Directory.CreateDirectory(userFolder + @"\thumbnail\");
                }

                DirectoryInfo dInfo = new DirectoryInfo(userFolder);
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    if (fInfo.CreationTime.AddDays(1).CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(fInfo.FullName);
                    }
                }

                dInfo = new DirectoryInfo(userFolder + @"\thumbnail\");
                foreach (FileInfo fInfo in dInfo.GetFiles())
                {
                    if (fInfo.CreationTime.AddDays(1).CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(fInfo.FullName);
                    }
                }
            }
            catch { }
            // 

            BLL.AccountMng bll = new BLL.AccountMng();
            Library.DTO.Notification notification;
            DTO.AccountMng.User userInfo = bll.GetUserInformationLight(userId, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.AccountMng.User>() { Data = userInfo, Message = notification });
        }

        [HttpPost]
        [Route("getusers")]
        public IHttpActionResult GetUsers(DTO.Search iSearch)
        {
            BLL.AccountMng bll = new BLL.AccountMng();
            Library.DTO.Notification notification;
            int TotalRows=0;

            List<DTO.AccountMng.User> users = bll.GetUserInformationWithFilter(ControllerContext.GetAuthUserId(), iSearch.Filters, iSearch.SortedBy, iSearch.SortedDirection, iSearch.PageSize, iSearch.PageIndex, out TotalRows, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<List<DTO.AccountMng.User>>() { Data = users, TotalRows = TotalRows, Message = notification });
        }

        [HttpGet]
        [Route("registeruser")]
        [AllowAnonymous]
        public IHttpActionResult RegisterUser(string userName, string password, string email)
        {
            using (AuthRepository _repo = new AuthRepository())
            {
                _repo.CreateUser(userName, password, email);
            }
            return Ok();
        }

        [HttpGet]
        [Route("ali")]
        [AllowAnonymous]
        public IHttpActionResult AutoLogin(string identifier)
        {
            var tokenExpiration = TimeSpan.FromDays(1);
            IdentityUser user = null;            
            using (AuthRepository _repo = new AuthRepository())
            {
                //_repo.RetrieveHash();                
                user = _repo.FinByIdentifier(identifier);
            }
            if (user != null)
            {
                Module.Framework.DTO.UserInfoDTO dtoUserInfo = bll.GetUserInfo(user.UserName);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim("sub", user.UserName));
                identity.AddClaim(new Claim("subid", dtoUserInfo.UserID.ToString()));
                identity.AddClaim(new Claim("branchid", dtoUserInfo.UserBranchID.ToString()));
                identity.AddClaim(new Claim("companyid", dtoUserInfo.UserCompanyID.ToString()));
                identity.AddClaim(new Claim("factoryid", dtoUserInfo.UserFactoryID.ToString()));
                identity.AddClaim(new Claim("role", "user"));

                var props = new AuthenticationProperties()
                {
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                };

                var ticket = new AuthenticationTicket(identity, props);
                var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

                JObject tokenResponse = new JObject(
                                            new JProperty("access_token", accessToken),
                                            new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString())
                );

                return Ok(tokenResponse);
            }
            return Ok();
        }

        [HttpGet]
        [Route("switch-branch")]
        public IHttpActionResult SwitchBranch(int branchId)
        {
            var tokenExpiration = TimeSpan.FromDays(1);
            try
            {
                string userName = Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(o => o.Type == "sub").Value;
                Module.Framework.DTO.UserInfoDTO dtoUserInfo = bll.GetUserInfo(userName);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim("sub", userName));
                identity.AddClaim(new Claim("subid", dtoUserInfo.UserID.ToString()));
                identity.AddClaim(new Claim("branchid", branchId.ToString()));
                identity.AddClaim(new Claim("companyid", dtoUserInfo.UserCompanyID.ToString()));
                identity.AddClaim(new Claim("factoryid", dtoUserInfo.UserFactoryID.ToString()));
                identity.AddClaim(new Claim("role", "user"));

                var props = new AuthenticationProperties()
                {
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                };

                var ticket = new AuthenticationTicket(identity, props);
                var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

                JObject tokenResponse = new JObject(
                                            new JProperty("access_token", accessToken),
                                            new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString())
                );

                return Ok(tokenResponse);
            }
            catch (Exception ex) { }
            return Ok();
        }

        [HttpPost]
        [Route("changepassword")]
        [AllowAnonymous]
        public IHttpActionResult ChangePassword(Models.PasswordChangeViewModel data)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // change password
            using (AuthRepository _repo = new AuthRepository())
            {
                if (data.NewPassword.Length < 7)
                {
                    throw new Exception("Password length must be at least 7 chars");
                }
                string errMsg = string.Empty;
                if (!_repo.ChangePassword(data.UserName, data.NewPassword, data.OldPassword, out errMsg))
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = errMsg;
                    return Ok(new Library.DTO.ReturnData<string>() { Data = string.Empty, Message = notification });
                }
            }

            // auto login
            var tokenExpiration = TimeSpan.FromDays(1);
            IdentityUser user = null;
            using (AuthRepository _repo = new AuthRepository())
            {
                //_repo.RetrieveHash();
                user = _repo.FindByUserNameNormal(data.UserName);
            }
            if (user != null)
            {
                Module.Framework.DTO.UserInfoDTO dtoUserInfo = bll.GetUserInfo(user.UserName);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim("sub", user.UserName));
                identity.AddClaim(new Claim("subid", dtoUserInfo.UserID.ToString()));
                identity.AddClaim(new Claim("branchid", dtoUserInfo.UserBranchID.ToString()));
                identity.AddClaim(new Claim("companyid", dtoUserInfo.UserCompanyID.ToString()));
                identity.AddClaim(new Claim("factoryid", dtoUserInfo.UserFactoryID.ToString()));
                identity.AddClaim(new Claim("role", "user"));

                var props = new AuthenticationProperties()
                {
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                };

                var ticket = new AuthenticationTicket(identity, props);
                var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

                JObject tokenResponse = new JObject(
                                            new JProperty("access_token", accessToken),
                                            new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString())
                );

                return Ok(new Library.DTO.ReturnData<Object>() { Data = tokenResponse, Message = notification });
            }

            // if we get here, something wrong with the process
            return InternalServerError();
        }
    }
}
