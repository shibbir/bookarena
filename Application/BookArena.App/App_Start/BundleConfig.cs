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
                "~/Scripts/foundation.js",
                "~/Scripts/foundation.reveal.js",
                "~/Scripts/foundation.offcanvas.js",
                "~/Vendors/bower_components/toastr/toastr.js",
                "~/Vendors/bower_components/raty/lib/jquery.raty.js",
                "~/Vendors/bower_components/dest/respond.src.js",
                "~/Vendors/bower_components/angular/angular.js",
                "~/Vendors/bower_components/angular-route/angular-route.js",
                "~/Vendors/bower_components/underscore/underscore.js",
                "~/Vendors/bower_components/raphael/raphael.js",
                "~/Vendors/bower_components/morris/morris.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/Application/app.js",
                "~/Scripts/Application/Services/identityService.js",
                "~/Scripts/Application/Services/sharedService.js",
                "~/Scripts/Application/Services/apiService.js",
                "~/Scripts/Application/Services/notifierService.js",
                "~/Scripts/Application/Directives/Directives.js",
                "~/Scripts/Application/Controllers/BaseCtrl.js",
                "~/Scripts/Application/Controllers/HomeCtrl.js",
                "~/Scripts/Application/Controllers/LoginCtrl.js",
                "~/Scripts/Application/Controllers/ProfileCtrl.js",
                "~/Scripts/Application/Controllers/CategoryCtrl.js",
                "~/Scripts/Application/Controllers/BookListCtrl.js",
                "~/Scripts/Application/Controllers/TransactionCtrl.js",
                "~/Scripts/Application/Controllers/BookAddCtrl.js",
                "~/Scripts/Application/Controllers/BookEditCtrl.js",
                "~/Scripts/Application/Controllers/BookDetailsCtrl.js",
                "~/Scripts/Application/Controllers/StudentListCtrl.js",
                "~/Scripts/Application/Controllers/StudentAddCtrl.js",
                "~/Scripts/Application/Controllers/StudentDetailsCtrl.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Vendors/bower_components/toastr/toastr.css",
                "~/Content/foundation.css",
                "~/Content/foundation-icons.css",
                "~/Vendors/bower_components/morris/morris.css",
                "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}