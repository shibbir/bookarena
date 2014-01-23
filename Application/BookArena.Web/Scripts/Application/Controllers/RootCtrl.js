"use strict";

(function(app) {
    app.controller("RootCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $rootScope.authenticatedUser = {};
            $rootScope.checkAuthentication = function() {
                service.call("/account/").then(function(result) {
                    if (result.Data) {
                        $rootScope.authenticatedUser = result.Data;
                        $rootScope.authenticatedUser.IsAuthenticated = true;
                    }
                });
            };

            $rootScope.checkForPermisssionAfter = function() {
                if (!$rootScope.authenticatedUser.IsAuthenticated) {
                    $location.path("/account/login").replace();
                }
            };

            $rootScope.checkAuthentication();

            $scope.checkForPermisssionBefore = function(path) {
                if (!$rootScope.authenticatedUser.IsAuthenticated) {
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

            $scope.logout = function() {
                service.call("/account/logoff").then(function(result) {
                    $rootScope.authenticatedUser = {};
                    $rootScope.globalContainer = {
                        response: result.Response
                    };
                    $location.path("/account/login");
                });
            };
        }
    ]);
})(angular.module("bookArenaApp"));