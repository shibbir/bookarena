"use strict";

(function (app) {
    app.controller("LoginCtrl", ["$scope", "$rootScope", "$location", "apiService", "notifierService", function ($scope, $rootScope, $location, service, notifier) {
        $scope.login = function () {
            if ($scope.LoginForm.$valid) {
                service.call("api/login", $("#LoginForm").serialize(), "POST").then(function (data) {
                    if (data) {
                        $rootScope.authenticatedUser.name = data.Name;
                        $rootScope.authenticatedUser.email = data.Email;
                        $rootScope.authenticatedUser.address = data.Address;
                        $rootScope.authenticatedUser.isAuthenticated = true;
                        $location.path("/");
                    } else {
                        notifier.notify({ responseType: "error", message: "Invalid username or password!" });
                    }
                });
            }
        };
    }]);
})(angular.module("bookArenaApp"));