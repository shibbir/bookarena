(function(app) {
    "use strict";

    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $location, apiService, notifierService, identityService, sharedService) {

            var config = {
                headers: identityService.getSecurityHeaders()
            };

            $scope.programs = sharedService.programs();
            $scope.batches = sharedService.batches();

            $scope.register = function(student) {
                $scope.StudentRegisterForm.submitted = true;

                if ($scope.StudentRegisterForm.$valid) {
                    $scope.addingStudent = true;

                    apiService.post("/api/students/", student, config).success(function() {
                        notifierService.notifySuccess("Student registered successfully.");
                        $scope.student.firstName = "";
                        $scope.student.lastName = "";
                        $scope.student.program = "";
                        $scope.student.batch = "";
                        $scope.student.idCardNumber = "";

                        $scope.addingStudent = false;
                        $scope.StudentRegisterForm.submitted = false;
                    }).error(function(errorResponse) {
                        $scope.addingStudent = false;
                        $scope.displayErrors(errorResponse);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArena"));