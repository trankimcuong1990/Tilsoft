using System.Web;
using System.Web.Optimization;

namespace ClientPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/templatejs").Include(
                "~/js/libs/jquery-3.2.1.min.js",
                "~/js/libs/jquery-ui.min.js",
                "~/js/app.config.js",
                "~/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "~/js/bootstrap/bootstrap.min.js",
                "~/js/notification/SmartNotification.min.js",
                "~/js/plugin/msie-fix/jquery.mb.browser.min.js",
                "~/js/plugin/fastclick/fastclick.min.js",
                "~/js/plugin/autoNumeric-1.9.41.js",
                "~/js/app.min.js",
                "~/js/angular/angular.min.js",
                "~/js/angular/angular-sanitize.min.js",
                "~/js/angular/angular-cookies.min.js",
                "~/js/angular/angular-locale_nl-nl.js",
                "~/js/angular/avs-bootstrap.js",
                "~/js/angular/avs-dataservice.js",
                "~/js/angular/avs-directives.js",
                "~/js/helper.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/templatecss").Include(
                "~/css/bootstrap.min.css",
                "~/css/font-awesome.min.css",
                "~/css/smartadmin-production-plugins.min.css",
                "~/css/smartadmin-production.min.css",
                "~/css/smartadmin-skins.min.css",
                "~/css/tilsoft.css"
            ));
        }
    }
}
