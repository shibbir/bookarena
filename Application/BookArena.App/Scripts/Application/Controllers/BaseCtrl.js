(function(app) {
    "use strict";

    app.controller("BaseCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, service, notifierService, identityService) {
            $scope.notImplemented = function() {
                notifierService.notifyError("Sorry! This feature is not available yet.");
            };

            $scope.redirectToHome = function() {
                $location.path("/");
            };

            $scope.logout = function() {
                identityService.logout().success(function(result) {
                    identityService.clearAccessToken();
                    identityService.clearAuthorizedUserData();
                    $rootScope.globalContainer = {
                        notifyType: "success",
                        message: result.message
                    };
                    $location.path("/account/login");
                });
            };
        }
    ]);
})(angular.module("bookArena"));