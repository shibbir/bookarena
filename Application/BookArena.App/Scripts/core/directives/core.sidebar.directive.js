(function(app) {
    "use strict";

    app.directive("sideBar", [
        "notifierService", function(notifierService) {
            return {
                restrict: "E",
                replace: true,
                templateUrl: "/Scripts/core/views/sidebar.view.html",
                link: function($scope) {
                    $scope.notImplemented = function() {
                        notifierService.notifyError("Sorry! This feature is not available yet.");
                    };
                }
            };
        }
    ]);
})(angular.module("core"));