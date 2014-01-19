"use strict";

(function(app) {
    app.controller("LoginCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $scope.login = function() {
                if ($scope.LoginForm.$valid) {
                    service.call("api/login", $("#LoginForm").serialize(), "POST").then(function(result) {
                        if (result.Data) {
                            $rootScope.authenticatedUser = result.Data;
                            $location.path("/");
                        } else {
                            notifier.notify(result.Response);
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));