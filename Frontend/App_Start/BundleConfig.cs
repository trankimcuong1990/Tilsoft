using System.Web;
using System.Web.Optimization;

namespace Frontend
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/templatejs").Include(
                    "~/TemplateResource/js/libs/jquery-2.1.1.min.js",
                    "~/TemplateResource/js/libs/jquery.base64.js",                    
                    "~/TemplateResource/js/libs/jquery-ui-1.10.3.min.js",
                    "~/TemplateResource/js/app.config.js",
                    "~/TemplateResource/js/plugin/autoNumeric-1.9.41.js",
                    "~/TemplateResource/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",                    
                    "~/TemplateResource/js/bootstrap/bootstrap.min.js",
                    "~/TemplateResource/js/plugin/bootstrap-timepicker/bootstrap-timepicker.min.js",
                    "~/TemplateResource/js/notification/SmartNotification.min.js",
                    "~/TemplateResource/js/smartwidgets/jarvis.widget.min.js",
                    "~/TemplateResource/js/plugin/jquery-validate/jquery.validate.min.js",
                    "~/TemplateResource/js/plugin/masked-input/jquery.maskedinput.min.js",
                    "~/TemplateResource/js/plugin/select2/select2.min.js",
                    "~/TemplateResource/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
                    "~/TemplateResource/js/plugin/msie-fix/jquery.mb.browser.min.js",
                    "~/TemplateResource/js/plugin/fastclick/fastclick.min.js",
                    "~/TemplateResource/js/app.js",
                    "~/TemplateResource/js/plugin/dropzone/dropzone.min.js",
                    "~/TemplateResource/js/plugin/featherlight.min.js",
                    "~/TemplateResource/js/plugin/featherlight.gallery.min.js",
                    "~/TemplateResource/js/plugin/masonry.pkgd.min.js",
                    "~/TemplateResource/js/plugin/js-webshim/minified/polyfiller.js",
                    "~/Scripts/furnindo.js",
                    "~/Angular/js/angular.js",
                    "~/Angular/js/angular-route.js",
                    "~/Angular/js/angular-sanitize.min.js",
                    "~/Angular/js/select2.js",                    
                    "~/Angular/js/angular-locale_nl-nl.js",
                    "~/Angular/js/angular-ui-date.js",
                    "~/Angular/js/angular-cookies.min.js",
                    "~/Angular/js/angular-route.js",
                    "~/Angular/js/angular-ui.min.js",
                    "~/Angular/js/customfilters.js",
                    "~/Angular/js/ng-currency.js",
                    "~/Angular/js/ui-bootstrap-tpls-0.14.3.min.js",
                    "~/Angular/js/lodash.js",
                    "~/Angular/js/moment.js",
                    "~/Angular/js/bootstrap-datetimepicker.js",
                    "~/TemplateResource/js/plugin/jquery.cookie.js",
                    "~/TemplateResource/js/plugin/morris/raphael.min.js",
                    "~/TemplateResource/js/plugin/morris/morris.min.js",
                    "~/TemplateResource/js/plugin/moment/moment.min.js",
                    "~/TemplateResource/js/plugin/fullcalendar/jquery.fullcalendar.min.js",
                    //"~/TemplateResource/js/plugin/highchart/highcharts.js",
                    //"~/TemplateResource/js/plugin/highchart/data.js",
                    //"~/TemplateResource/js/plugin/highchart/drilldown.js",
                    "~/TemplateResource/js/plugin/highcharts.js",                    
                    "~/TemplateResource/js/plugin/video.min.js",
                    "~/TemplateResource/js/plugin/jquery-barcode/jquery-barcode.min.js",
                    "~/TemplateResource/js/plugin/xml2json.min.js",
                    "~/TemplateResource/js/plugin/bootbox/bootbox.min.js",
                    "~/TemplateResource/js/plugin/superbox/superbox.min.js",
                    "~/TemplateResource/js/plugin/papaparse.min.js",
                    "~/TemplateResource/js/plugin/xlsx.core.min.js",
                    "~/TemplateResource/js/plugin/xmlToJson.min.js",

                    /* included angular widgets */
                    "~/Angular/app/widgets/widgets.module.js",
                    "~/Angular/app/widgets/showDialogLink/show-dialog-link.js"
                    ));                                     

            bundles.Add(new ScriptBundle("~/bundles/templatejs/login").Include(
                    "~/TemplateResource/js/libs/jquery-2.1.1.min.js",
                    "~/TemplateResource/js/libs/jquery-ui-1.10.3.min.js",
                    "~/TemplateResource/js/app.config.js",
                    "~/TemplateResource/js/bootstrap/bootstrap.min.js",
                    "~/TemplateResource/js/plugin/jquery-validate/jquery.validate.min.js",
                    "~/TemplateResource/js/plugin/masked-input/jquery.maskedinput.min.js",
                    "~/TemplateResource/js/app.min.js",
                    "~/Angular/js/angular.js",
                    "~/Angular/js/angular-cookies.min.js",
                    "~/Angular/js/helper.js"));

            bundles.Add(new StyleBundle("~/bundles/templatecss").Include(
                    "~/TemplateResource/css/bootstrap.min.css",
                    "~/TemplateResource/css/bootstrap-datetimepicker.min.css",
                    "~/TemplateResource/css/font-awesome.min.css",
                    "~/TemplateResource/css/smartadmin-production-plugins.min.css",
                    "~/TemplateResource/css/smartadmin-production.min.css",
                    "~/TemplateResource/css/smartadmin-skins.min.css",
                    "~/TemplateResource/css/smartadmin-rtl.min.css",
                    "~/TemplateResource/css/featherlight.min.css",
                    "~/TemplateResource/css/featherlight.gallery.min.css",
                    "~/TemplateResource/css/extra.css",
                    "~/TemplateResource/css/angular/ui-grid.css"));
        }
    }
}
