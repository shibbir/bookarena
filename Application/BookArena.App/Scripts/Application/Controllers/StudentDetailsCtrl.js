"use strict";

(function(app) {
    app.controller("StudentDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $routeParams, $location, apiService, notifierService, identityService, sharedService) {
            if (identityService.isLoggedIn()) {
                $scope.programs = sharedService.programs();
                $scope.batches = sharedService.batches();

                var config = {
                    headers: identityService.getSecurityHeaders()
                };

                apiService.get("/api/students/" + $routeParams.id, config).success(function(result) {
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
                $scope.StudentEditForm.submitted = true;

                if (identityService.isLoggedIn() && $scope.StudentEditForm.$valid) {
                    $scope.editingStudent = true;
                    apiService.put("/api/students/", student, config).success(function(result) {
                        notifierService.notifySuccess(result.message);
                        $scope.editingStudent = false;
                    }).error(function(error) {
                        notifierService.notifyError(error.message);
                        $scope.editingStudent = false;
                    });
                }
            };
        }
    ]);
})(_app);