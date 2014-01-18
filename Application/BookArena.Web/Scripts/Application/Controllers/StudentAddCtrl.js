"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            if (!$rootScope.authenticatedUser.isAuthenticated) {
                $location.path("account/login").replace();
            }
            $scope.register = function() {
                if ($scope.StudentRegisterForm.$valid) {
                    service.call("api/students/add", $("#StudentRegisterForm").serialize(), "POST").then(function(data) {
                        notifier.notify(data);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));