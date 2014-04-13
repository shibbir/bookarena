"use strict";

(function(app) {
    app.controller("StudentDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService, sharedService) {
            $(document).foundation();
            if (identityService.isAuthenticated()) {
                $scope.programs = sharedService.programs();
                $scope.batches = sharedService.batches();
                service.call("/students/student/" + $routeParams.id).then(function(result) {
                    if (result.data) {
                        $scope.student = result.data;
                        if (result.transactions.length) {
                            $scope.transactions = result.transactions;
                        } else {
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
                if (identityService.isAuthenticated() && $scope.StudentEditForm.$valid) {
                    service.call("/students/edit/", $("form[name=StudentEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));