(function(app) {
    "use strict";

    app.directive("offCanvasMenu", [
        function() {
            return {
                restrict: "E",
                replace: true,
                templateUrl: "/Scripts/core/views/offcanvasmenu.view.html"
            };
        }
    ]);
})(angular.module("core"));