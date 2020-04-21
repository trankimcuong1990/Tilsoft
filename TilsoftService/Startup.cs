using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TilsoftService.Providers;
using System.IO;
using System.Configuration;

[assembly: OwinStartup(typeof(TilsoftService.Startup))]
namespace TilsoftService
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            //
            // CONFIG LOG
            //
            Library.Common.Helper.LogFolder = HttpContext.Current.Server.MapPath("~/Log");


            if (File.Exists(HttpContext.Current.Server.MapPath("~/DevConnectionString.txt")))
            {
                // DESCRIPTION: DEBUG MODE
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/DevConnectionString.txt"));
                FrameworkSetting.Setting.SqlConnection = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                // DESCRIPTION: RELEASE MODE
                FrameworkSetting.Setting.SqlConnection = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            }

            // custom configuration            
            FrameworkSetting.Setting.EnabledLog = (ConfigurationManager.AppSettings["EnabledLog"] == "1");

            FrameworkSetting.Setting.AbsoluteBaseFolder = HttpContext.Current.Server.MapPath("~/");
            FrameworkSetting.Setting.AbsoluteFileFolder = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["FileUrl"]);
            FrameworkSetting.Setting.AbsoluteThumbnailFolder = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"]);
            FrameworkSetting.Setting.AbsoluteUserTempFolder = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["TempUrl"]);
            FrameworkSetting.Setting.AbsoluteReportTmpFolder = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["ReportTempUrl"]);
            FrameworkSetting.Setting.AbsoluteReportFolder = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["ReportFolderUrl"]);

            FrameworkSetting.Setting.ThumbnailUrl = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"];
            FrameworkSetting.Setting.FullSizeUrl = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["FileUrl"];
            FrameworkSetting.Setting.ReportTempUrl = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ReportTempUrl"];
            FrameworkSetting.Setting.ServiceUrl = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"];
            FrameworkSetting.Setting.MediaUrl = System.Configuration.ConfigurationManager.AppSettings["MediaUrl"];

            FrameworkSetting.Setting.SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
            FrameworkSetting.Setting.SMTPRelayServer = System.Configuration.ConfigurationManager.AppSettings["SMTPRelayServer"];
            FrameworkSetting.Setting.SMTPUsername = System.Configuration.ConfigurationManager.AppSettings["SMTPUsername"];
            FrameworkSetting.Setting.SMTPPassword = System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"];
            FrameworkSetting.Setting.SMTPEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SMTPEmailAddress"];
            FrameworkSetting.Setting.SMTPPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);

            FrameworkSetting.Setting.FrontendUrl = System.Configuration.ConfigurationManager.AppSettings["FrontendUrl"];

            if (System.Configuration.ConfigurationManager.AppSettings["SMTPSSL"] == "1")
            {
                FrameworkSetting.Setting.SMTPSSL = true;
            }
            

            // normal flow
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);            
        }

    }
}