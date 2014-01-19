"use strict";

(function(app) {
    app.controller("RootCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $rootScope.authenticatedUser = {};
            $rootScope.checkAuthentication = function() {
                service.call("/api/account").then(function(data) {
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
                    notifier.notify({ ResponseType: "error", Message: "Access Denied! You need to login first." });
                } else {
                    $location.path(path);
                }
            };

            $scope.logout = function() {
                service.call("/api/logout").then(function(data) {
                    $rootScope.authenticatedUser = {};
                });
            };
        }
    ]);
})(angular.module("bookArenaApp"));