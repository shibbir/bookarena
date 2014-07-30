"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $location, apiService, notifierService, identityService, sharedService) {

            if (!identityService.isLoggedIn()) {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }

            var config = {
                headers: identityService.getSecurityHeaders()
            };

            $scope.programs = sharedService.programs();
            $scope.batches = sharedService.batches();

            $scope.register = function (student) {
                $scope.StudentRegisterForm.submitted = true;

                if ($scope.StudentRegisterForm.$valid) {
                    $scope.addingStudent = true;
                    apiService.post("/api/students/", student, config).success(function(result) {
                        notifierService.notifySuccess(result.message);
                        $scope.student.firstName = "";
                        $scope.student.lastName = "";
                        $scope.student.program = "";
                        $scope.student.batch = "";
                        $scope.student.idCardNumber = "";

                        $scope.addingStudent = false;
                    }).error(function(error) {
                        notifierService.notifyError(error.message);
                        $scope.addingStudent = false;
                    });
                }
            };
        }
    ]);
})(_app);