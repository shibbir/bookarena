using System.Web.Optimization;

namespace BookArena.App
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/foundation-sites/dist/foundation.js",
                "~/bower_components/toastr/toastr.js",
                "~/bower_components/raty/lib/jquery.raty.js",
                "~/bower_components/respond/dest/respond.src.js",
                "~/bower_components/angular/angular.js",
                "~/bower_components/angular-route/angular-route.js",
                "~/bower_components/angular-messages/angular-messages.js",
                "~/bower_components/underscore/underscore.js",
                "~/bower_components/raphael/raphael.js",
                "~/bower_components/morrisjs/morris.js",
                "~/bower_components/ng-file-upload/ng-file-upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/Application/app.js",
                "~/Scripts/Application/Services/identityService.js",
                "~/Scripts/Application/Services/sharedService.js",
                "~/Scripts/Application/Services/apiService.js",
                "~/Scripts/Application/Services/notifierService.js",
                "~/Scripts/Application/Services/fileService.js",
                "~/Scripts/Application/Directives/directives.js",
                "~/Scripts/Application/Controllers/baseCtrl.js",
                "~/Scripts/Application/Controllers/homeCtrl.js",
                "~/Scripts/Application/Controllers/loginCtrl.js",
                "~/Scripts/Application/Controllers/profileCtrl.js",
                "~/Scripts/Application/Controllers/categoryCtrl.js",
                "~/Scripts/Application/Controllers/bookListCtrl.js",
                "~/Scripts/Application/Controllers/transactionCtrl.js",
                "~/Scripts/Application/Controllers/bookAddCtrl.js",
                "~/Scripts/Application/Controllers/bookEditCtrl.js",
                "~/Scripts/Application/Controllers/bookDetailsCtrl.js",
                "~/Scripts/Application/Controllers/studentListCtrl.js",
                "~/Scripts/Application/Controllers/studentAddCtrl.js",
                "~/Scripts/Application/Controllers/studentDetailsCtrl.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/bower_components/toastr/toastr.css",
                "~/bower_components/foundation-sites/dist/foundation.css",
                "~/bower_components/morrisjs/morris.css",
                "~/Content/site.css")
                .Include("~/bower_components/foundation-icon-fonts/foundation-icons.css", new CssRewriteUrlTransform()));
        }
    }
}