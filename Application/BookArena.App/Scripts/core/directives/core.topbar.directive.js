(function(app) {
    "use strict";

    app.directive("topBar", [
        function() {
            return {
                restrict: "E",
                replace: true,
                templateUrl: "/Scripts/core/views/topbar.view.html"
            };
        }
    ]);
})(angular.module("core"));