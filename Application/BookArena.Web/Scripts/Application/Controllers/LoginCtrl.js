"use strict";

(function(app) {
    app.controller("LoginCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                $location.path("/").replace();
            }
            $scope.data = {
                userName: "admin",
                password: "Hakuna matata"
            };
            
            $scope.login = function() {
                if ($scope.LoginForm.$valid) {
                    service.call("api/login", $("#LoginForm").serialize(), "POST").then(function(result) {
                        if (result.Data) {
                            $rootScope.authenticatedUser = result.Data;
                            $rootScope.authenticatedUser.IsAuthenticated = true;
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