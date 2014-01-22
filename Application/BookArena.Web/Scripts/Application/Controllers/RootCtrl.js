"use strict";

(function(app) {
    app.controller("RootCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $rootScope.authenticatedUser = {};
            $rootScope.checkAuthentication = function() {
                service.call("/account/").then(function(data) {
                    if (data) {
                        $rootScope.authenticatedUser = data;
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
                service.call("/account/logout").then(function(data) {
                    $rootScope.authenticatedUser = {};
                    $rootScope.globalContainer = {
                        response: data.Response
                    };
                    $location.path("/account/login");
                });
            };
        }
    ]);
})(angular.module("bookArenaApp"));