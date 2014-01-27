"use strict";

(function(app) {
    app.controller("LoginCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            var tempGlobalContainer = $.extend(true, {}, $rootScope.globalContainer);
            $rootScope.globalContainer = null;

            if ($scope.authenticatedUser.IsAuthenticated) {
                $location.path("/").replace();
            }
            if (tempGlobalContainer && tempGlobalContainer.response) {
                notifier.notify(tempGlobalContainer.response);
            }
            $scope.data = {
                userName: "admin",
                password: "Hakuna matata"
            };

            $scope.login = function() {
                if ($scope.LoginForm.$valid) {
                    service.call("/account/login/", $("#LoginForm").serialize(), "POST").then(function(result) {
                        if (result.Data) {
                            $scope.authenticatedUser = result.Data;
                            $scope.authenticatedUser.IsAuthenticated = true;

                            if (tempGlobalContainer && tempGlobalContainer.redirectTo) {
                                $location.path(tempGlobalContainer.redirectTo).replace();
                            } else {
                                $location.path("/").replace();
                            }
                        } else {
                            notifier.notify(result.Response);
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));