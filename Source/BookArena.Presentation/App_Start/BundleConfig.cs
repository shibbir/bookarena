using System.Web.Optimization;

namespace BookArena.App
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/node_modules/jquery/dist/jquery.js",
                "~/node_modules/foundation-sites/dist/foundation.js",
                "~/node_modules/toastr/toastr.js",
                "~/node_modules/raty-js/lib/jquery.raty.js",
                "~/node_modules/angular/angular.js",
                "~/node_modules/angular-route/angular-route.js",
                "~/node_modules/angular-messages/angular-messages.js",
                "~/node_modules/underscore/underscore.js",
                "~/node_modules/raphael/raphael.js",
                "~/node_modules/morris.js/morris.js",
                "~/node_modules/ng-file-upload/dist/ng-file-upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/core/config/config.js",
                "~/Scripts/core/config/init.js",
                "~/Scripts/core/core.module.js",
                "~/Scripts/core/config/core.routes.js",
                "~/Scripts/core/services/core.api.service.js",
                "~/Scripts/core/services/core.identity.service.js",
                "~/Scripts/core/services/core.file.service.js",
                "~/Scripts/core/services/core.notifier.service.js",
                "~/Scripts/core/services/core.shared.service.js",
                "~/Scripts/core/directives/core.topbar.directive.js",
                "~/Scripts/core/directives/core.sidebar.directive.js",
                "~/Scripts/core/directives/core.offcanvas.directive.js",
                "~/Scripts/core/controllers/core.home.controller.js",

                "~/Scripts/users/users.module.js",
                "~/Scripts/users/config/users.routes.js",
                "~/Scripts/users/controllers/users.login.controller.js",
                "~/Scripts/users/controllers/users.logout.controller.js",

                "~/Scripts/students/students.module.js",
                "~/Scripts/students/config/students.routes.js",
                "~/Scripts/students/controllers/students.list.controller.js",
                "~/Scripts/students/controllers/students.add.controller.js",
                "~/Scripts/students/controllers/students.show.controller.js",

                "~/Scripts/books/books.module.js",
                "~/Scripts/books/config/books.routes.js",
                "~/Scripts/books/controllers/books.category.controller.js",
                "~/Scripts/books/controllers/books.list.controller.js",
                "~/Scripts/books/controllers/books.add.controller.js",
                "~/Scripts/books/controllers/books.edit.controller.js",
                "~/Scripts/books/controllers/books.show.controller.js",
                "~/Scripts/books/directives/category.add.directive.js",
                "~/Scripts/books/directives/category.edit.directive.js",
                "~/Scripts/books/directives/books.rating.directive.js",
                "~/Scripts/books/directives/books.ratinginput.directive.js",
                "~/Scripts/books/directives/books.status.directive.js",

                "~/Scripts/transactions/transactions.module.js",
                "~/Scripts/transactions/config/transactions.routes.js",
                "~/Scripts/transactions/controllers/transactions.controller.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/node_modules/toastr/build/toastr.css",
                "~/node_modules/foundation-sites/dist/foundation.css",
                "~/node_modules/morris.js/morris.css",
                "~/Content/site.css")
                .Include("~/node_modules/foundation-icon-fonts/foundation-icons.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
    }
}
