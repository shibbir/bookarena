"use strict";

(function(app) {
    app.controller("StudentDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService, sharedService) {
            $(document).foundation();
            if (identityService.isAuthenticated()) {
                $scope.programs = sharedService.programs();
                $scope.batches = sharedService.batches();
                service.get("/students/student/" + $routeParams.id).success(function (result) {
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
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }

            $scope.update = function(student) {
                if (identityService.isAuthenticated() && $scope.StudentEditForm.$valid) {
                    service.post("/students/edit/", student).success(function (result) {
                        notifier.notify(result.response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));