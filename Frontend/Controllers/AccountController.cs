using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Frontend.Customize.Authentication;
using Frontend.Models;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Frontend.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
            : this(new UserManager<CustomIdentity>(new CustomIdentityStore()))
        {
        }

        public AccountController(UserManager<CustomIdentity> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<CustomIdentity> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ChangePassword(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> TokenLogin(string a, int e)
        {
            //APIDTO.APIToken oauthToken = new APIDTO.APIToken() { access_token = a, expires_in = e };
            APIDTO.APIUserInformation apiUser = null;
            string backendServiceUrl = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString();
            string errMsg = string.Empty;
            string errDescription = string.Empty;

            // get user info
            //apiUser = JsonConvert.DeserializeObject<APIDTO.APIUserInformation>(Customize.Helper.PostRequestAPI(backendServiceUrl + "api/account/getuserinfo", string.Empty, "application/json", "application/json", "bearer " + oauthToken.access_token, out errMsg, out errDescription));
            apiUser = JsonConvert.DeserializeObject<APIDTO.APIUserInformation>(Customize.Helper.GetAPIUserData(backendServiceUrl + "api/account/getuserinfo", a, out errMsg));
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
            if (apiUser.Message.Type == APIDTO.NotificationType.Error)
            {
                throw new Exception(apiUser.Message.Message);
            }

            // all gone well, start registering user and permission
            CustomIdentity user = new CustomIdentity()
            {
                Id = apiUser.Data.UserId.ToString(),
                UserName = apiUser.Data.UserName,
                Email = apiUser.Data.Email
            };
            await SignInAsync(user, false);

            // register custom information to session
            Session[Customize.Common.ProjectDefinition.TOKEN_SESSION] = a;
            Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION] = apiUser;
            return RedirectToAction("Index", "DashBoard", new object() { });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                CustomIdentity user = null;

                string backendServiceUrl = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString();
                string providedLoginInfo = "grant_type=password&username=" + model.Username + "&password=" + Url.Encode(model.Password.Replace("+", "[plus]"));
                //APIDTO.APIToken oauthToken = null;
                string token = string.Empty;
                APIDTO.APIUserInformation apiUser = null;
                string errMsg = string.Empty;
                string errDescription = string.Empty;

                // login - get token
                //oauthToken = JsonConvert.DeserializeObject<APIDTO.APIToken>(Customize.Helper.PostRequestAPI(backendServiceUrl + "token", providedLoginInfo, "application/json", "application-x-www-form-urlencode", string.Empty, out errMsg, out errDescription));
                token = Customize.Helper.GetAPIToken(backendServiceUrl + "token", model.Username, Url.Encode(model.Password.Replace("+", "[plus]")), out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    switch (errMsg)
                    {
                        case "IncorrectLogin":
                            ModelState.AddModelError("", "Username or password is invalid!");
                            break;

                        case "AccountDisabled":
                            ModelState.AddModelError("", "Account has been disabled!");
                            break;

                        case "ChangePassword":
                            return RedirectToAction("ChangePassword", "Account", new { username = model.Username });
                    }
                    return View(model);
                }
                else
                {
                    // get user info
                    //apiUser = JsonConvert.DeserializeObject<APIDTO.APIUserInformation>(Customize.Helper.PostRequestAPI(backendServiceUrl + "api/account/getuserinfo", providedLoginInfo, "application/json", "application/json", "bearer " + token, out errMsg, out errDescription));
                    apiUser = JsonConvert.DeserializeObject<APIDTO.APIUserInformation>(Customize.Helper.GetAPIUserData(backendServiceUrl + "api/account/getuserinfo", token, out errMsg));
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        ModelState.AddModelError("", errMsg);
                        return View(model);
                    }
                    if (apiUser.Message.Type == APIDTO.NotificationType.Error)
                    {
                        ModelState.AddModelError("", apiUser.Message.Message);
                        return View(model);
                    }

                    // all gone well, start registering user and permission
                    user = new CustomIdentity()
                    {
                        Id = apiUser.Data.UserId.ToString(),
                        UserName = apiUser.Data.UserName,
                        Email = apiUser.Data.Email
                    };
                    await SignInAsync(user, false);

                    // register custom information to session
                    Session[Customize.Common.ProjectDefinition.TOKEN_SESSION] = token;
                    Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION] = apiUser;

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "DashBoard", new object() { });
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Dashboard");
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<ActionResult> DirectAccess(string identifier, string url)
        //{
        //    CustomIdentity user = null;
        //    string responseString = string.Empty;
        //    string errMsg = string.Empty;
        //    string errDescription = string.Empty;
        //    APIDTO.APIToken oauthToken = null;
        //    APIDTO.APIUserInformation apiUser = null;
        //    string backendServiceUrl = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString();
        //    try
        //    {
        //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(backendServiceUrl + "/api/account/ali?identifier=" + identifier);
        //        req.Method = "GET";
        //        req.Timeout = 10000;

        //        // send request and get response
        //        var response = req.GetResponse();
        //        StreamReader sr = new StreamReader(response.GetResponseStream());
        //        responseString = sr.ReadToEnd();
        //        sr.Close();

        //        if (!string.IsNullOrEmpty(responseString))
        //        {
        //            oauthToken = JsonConvert.DeserializeObject<APIDTO.APIToken>(responseString);
        //            if (!string.IsNullOrEmpty(oauthToken.access_token))
        //            {
        //                // get user info
        //                //apiUser = JsonConvert.DeserializeObject<APIDTO.APIUserInformation>(Customize.Helper.PostRequestAPI(backendServiceUrl + "api/account/getuserinfo", "", "application/json", "application/json", "bearer " + oauthToken.access_token, out errMsg, out errDescription));
        //                apiUser = JsonConvert.DeserializeObject<APIDTO.APIUserInformation>(Customize.Helper.PostRequestAPI(backendServiceUrl + "api/account/getuserinfo", "", "application/json", "application/json", "bearer " + oauthToken.access_token, out errMsg, out errDescription));
        //                if (string.IsNullOrEmpty(errMsg) && apiUser.Message.Type != APIDTO.NotificationType.Error)
        //                {
        //                    // all gone well, start registering user and permission
        //                    user = new CustomIdentity()
        //                    {
        //                        Id = apiUser.Data.UserId.ToString(),
        //                        UserName = apiUser.Data.UserName,
        //                        Email = apiUser.Data.Email
        //                    };
        //                    await SignInAsync(user, false);

        //                    // register custom information to session
        //                    Session[Customize.Common.ProjectDefinition.TOKEN_SESSION] = oauthToken.access_token;
        //                    Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION] = apiUser;

        //                    return Redirect(url);
        //                }
        //            }
        //        }                
        //    }
        //    catch (WebException e)
        //    {
        //        errMsg = "Unable to connect to the remote server!";
        //        responseString = string.Empty;
        //    }
        //    return RedirectToAction("Login", "Account", new object { });
        //}

        //
        // PRIVATE
        //
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(CustomIdentity user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            // logging
            Library.Common.Helper.Log(Library.Common.Helper.varDump("CustomIdentity object in AccountController line 149" + Environment.NewLine + user));

            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void SignIn(CustomIdentity user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }


        public ActionResult SwitchBranch(int b)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Session[Customize.Common.ProjectDefinition.TOKEN_SESSION].ToString());
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                // send request get all items
                try
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString() + "api/account/switch-branch?branchId=" + b.ToString();
                    var anonymousObj = new { access_token = "", expires_in = 0 };
                    
                    // get new token from api service
                    var obj = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(client.DownloadString(url), anonymousObj);

                    // registering new user info into session
                    Session[Customize.Common.ProjectDefinition.TOKEN_SESSION] = obj.access_token;
                    Frontend.APIDTO.APIUserInformation currentUserInfo = (Frontend.APIDTO.APIUserInformation)Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION];
                    currentUserInfo.Data.BranchID = b;
                    currentUserInfo.Data.BranchUD = currentUserInfo.Data.Branches.FirstOrDefault(o => o.BranchID == b).BranchUD;
                    Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION] = currentUserInfo;

                }
                catch (WebException exception)
                {
                    //string responseText = string.Empty;
                    //var responseStream = exception.Response?.GetResponseStream();
                    //if (responseStream != null)
                    //{
                    //    using (var reader = new StreamReader(responseStream))
                    //    {
                    //        responseText = reader.ReadToEnd();
                    //    }
                    //}
                    //throw new Exception("getEUData: " + responseText);
                }
                return RedirectToAction("Index", "DashBoard", new object() { });
                //JObject data = (JObject)obj.ToObject();
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterSession(string returnUrl)
        {
            var userJson = Request.Form.GetValues("1A799D82AC9F4FAF866445B4A851D4B7")[0];
            var token = Request.Form.GetValues("31970B3468B84EEBBFCF66AB750CEF58")[0];
            var apiUser = JsonConvert.DeserializeObject<APIDTO.APIUser>(userJson);

            // all gone well, start registering user and permission
            CustomIdentity user = new CustomIdentity()
            {
                Id = apiUser.UserId.ToString(),
                UserName = apiUser.UserName,
                Email = apiUser.Email
            };
            SignIn(user, false);

            // register custom information to session
            Session[Customize.Common.ProjectDefinition.TOKEN_SESSION] = token;
            Session[Customize.Common.ProjectDefinition.USER_INFO_SESSION] = new APIDTO.APIUserInformation { Data = apiUser, Message = new APIDTO.APINotifyMessage() };

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "DashBoard", new object() { });
            }
        }
    }
}