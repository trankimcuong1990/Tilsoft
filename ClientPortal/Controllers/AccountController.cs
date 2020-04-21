using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientPortal.Library;

namespace ClientPortal.Controllers
{
    [AVSAuthorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            var cookie = Request.Cookies.Get(Library.Helper.COOKIE_BEARER_TOKEN);
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login");
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}