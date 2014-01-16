"use strict";

(function(app) {
    app.controller("LoginCtrl", ["$scope", "apiService", "notifierService", function ($scope, service, notifier) {
        $scope.login = function () {
            if ($scope.LoginForm.$valid) {
                service.call("account/login", $("#LoginForm").serialize()).then(function (data) {
                    if (data === "true") {
                        notifier.notify({ responseType: "success", message: "Data retrieved successfully!" });
                    } else {
                        notifier.notify({ responseType: "error", message: "Invalid username or password!" });
                    }
                });
            }
        };
    }]);
})(angular.module("bookArenaApp"));