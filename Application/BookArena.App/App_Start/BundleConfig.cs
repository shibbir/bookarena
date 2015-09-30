using System.Web.Optimization;

namespace BookArena.App
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Vendors/bower_components/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/Vendors/bower_components/jquery/dist/jquery.js",
                "~/Vendors/bower_components/foundation/js/foundation/foundation.js",
                "~/Vendors/bower_components/foundation/js/foundation/foundation.reveal.js",
                "~/Vendors/bower_components/foundation/js/foundation/foundation.offcanvas.js",
                "~/Vendors/bower_components/toastr/toastr.js",
                "~/Vendors/bower_components/raty/lib/jquery.raty.js",
                "~/Vendors/bower_components/dest/respond.src.js",
                "~/Vendors/bower_components/angular/angular.js",
                "~/Vendors/bower_components/angular-route/angular-route.js",
                "~/Vendors/bower_components/underscore/underscore.js",
                "~/Vendors/bower_components/raphael/raphael.js",
                "~/Vendors/bower_components/morrisjs/morris.js",
                "~/Vendors/bower_components/ng-file-upload/ng-file-upload.js"));

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
                "~/Vendors/bower_components/toastr/toastr.css",
                "~/Vendors/bower_components/foundation/css/foundation.css",
                "~/Vendors/bower_components/foundation-icon-fonts/foundation-icons.css",
                "~/Vendors/bower_components/morris/morris.css",
                "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}