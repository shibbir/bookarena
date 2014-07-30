"use strict";

(function(app) {
    app.controller("BaseCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, service, notifierService, identityService) {
            $scope.notImplemented = function() {
                notifierService.notifyError("Sorry! This feature is not available yet.");
            };

            $scope.redirectToHome = function() {
                $location.path("/");
            };

            $scope.checkForPermisssionBefore = function(path) {
                if (!identityService.isLoggedIn()) {
                    identityService.createAccessDeniedResponse(path);

                    if ($location.path() === "/account/login") {
                        notifierService.notifyError("Access Denied! You need to login first.");
                    } else {
                        $location.path("/account/login");
                    }
                } else {
                    $location.path(path);
                }
            };
            $scope.openModal = function(selector) {
                $(selector).foundation("reveal", "open");
            };
            $scope.closeModal = function(selector) {
                $(selector).foundation("reveal", "close");
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
})(_app);