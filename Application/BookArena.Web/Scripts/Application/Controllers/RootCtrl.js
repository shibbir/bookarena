"use strict";

(function(app) {
    app.controller("RootCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", function ($scope, $rootScope, $location, service, notifier, identityService) {
            $scope.notImplemented = function() {
                notifier.notify({
                    ResponseType: "error",
                    Message: "Sorry! This feature is not available yet."
                });
            };

            identityService.authenticatedUser();

            $scope.checkForPermisssionBefore = function(path) {
                if (!identityService.isAuthenticated()) {
                    $rootScope.globalContainer = {
                        redirectTo: path,
                        response: {
                            ResponseType: "error",
                            Message: "Access Denied! You need to login first."
                        }
                    };

                    if ($location.path() === "/account/login") {
                        notifier.notify({
                            ResponseType: "error",
                            Message: "Access Denied! You need to login first."
                        });
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

            $scope.logout = function () {
                identityService.logoff();
            };
        }
    ]);
})(angular.module("bookArenaApp"));