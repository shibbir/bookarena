using System.Web.Optimization;

namespace BookArena.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/foundation.js",
                "~/Scripts/foundation.topbar.js",
                "~/Scripts/foundation.reveal.js",
                "~/Scripts/foundation.abide.js",
                "~/Scripts/foundation.offcanvas.js",
                "~/Scripts/toastr.js",
                "~/Scripts/jquery.raty.js",
                "~/Scripts/respond.js",
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/raphael.js",
                "~/Scripts/morris.js",
                "~/Scripts/Application/app.js",
                "~/Scripts/Application/Services/identityService.js",
                "~/Scripts/Application/Services/sharedService.js",
                "~/Scripts/Application/Services/apiService.js",
                "~/Scripts/Application/Services/notifierService.js",
                "~/Scripts/Application/Directives/Directives.js",
                "~/Scripts/Application/Controllers/HomeCtrl.js",
                "~/Scripts/Application/Controllers/RootCtrl.js",
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
                "~/Content/toastr.css",
                "~/Content/foundation.css",
                "~/Content/foundation-icons.css",
                "~/Content/morris.css",
                "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}