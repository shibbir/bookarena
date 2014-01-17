"use strict";

(function (app) {
    app.controller("RootCtrl", ["$scope", "$rootScope", "apiService", function ($scope, $rootScope, service) {
        $rootScope.authenticatedUser = {};
        $rootScope.authenticatedUser.isAuthenticated = true;
        service.call("/api/account").then(function (data) {
            if (data) {
                $rootScope.authenticatedUser.name = data.Name;
                $rootScope.authenticatedUser.email = data.Email;
                $rootScope.authenticatedUser.address = data.Address;
                $rootScope.authenticatedUser.isAuthenticated = true;
            }
            else {
                $rootScope.authenticatedUser = {};
            }
        });

        $scope.logout = function () {
            service.call("/api/logout").then(function (data) {
                $rootScope.authenticatedUser = {};
            });
        };
    }]);
})(angular.module("bookArenaApp"));