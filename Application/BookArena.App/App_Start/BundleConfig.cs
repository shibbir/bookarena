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
                "~/Scripts/core/controllers/core.credit.controller.js",

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
                "~/bower_components/toastr/toastr.css",
                "~/bower_components/foundation-sites/dist/foundation.css",
                "~/bower_components/morrisjs/morris.css",
                "~/Content/site.css")
                .Include("~/bower_components/foundation-icon-fonts/foundation-icons.css", new CssRewriteUrlTransform()));
        }
    }
}