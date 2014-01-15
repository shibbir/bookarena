using System.Web.Optimization;

namespace BookArena.Web.App_Start
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

            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/foundation.min.js",
                "~/Scripts/toastr.js",
                "~/Scripts/respond.js",
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/Application/app.js",
                "~/Scripts/Application/Services/DataService.js",
                "~/Scripts/Application/Services/Notifier.js",
                "~/Scripts/Application/Controllers/BookCtrl.js",
                "~/Scripts/Application/Controllers/StudentCtrl.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/toastr.css",
                "~/Content/foundation.css",
                "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}