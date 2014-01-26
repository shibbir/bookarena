"use strict";

(function(app) {
    app.controller("StudentDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", function($scope, $rootScope, $routeParams, $location, service, notifier) {
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                $scope.book = {};
                service.call("/students/student/" + $routeParams.id).then(function(result) {
                    if (result.Data) {
                        $scope.student = result.Data;
                        if (result.Transactions.length) {
                            $scope.transactions = result.Transactions;
                        }
                        else {
                            $scope.transactions = {};
                        }
                    } else {
                        $location.path("/").replace();
                    }
                });
            } else {
                $rootScope.globalContainer = {
                    redirectTo: $location.path(),
                    response: {
                        ResponseType: "error",
                        Message: "Access Denied! You need to login first."
                    }
                };
                $location.path("/account/login").replace();
            }

            $scope.update = function() {
                if ($scope.StudentEditForm.$valid && $rootScope.authenticatedUser.IsAuthenticated) {
                    service.call("/students/edit/", $("form[name=StudentEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));