(function(app) {
    "use strict";

    app.controller("StudentDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $routeParams, $location, apiService, notifierService, identityService, sharedService) {
            if (identityService.isLoggedIn()) {
                $scope.programs = sharedService.programs();
                $scope.batches = sharedService.batches();

                var config = {
                    headers: identityService.getSecurityHeaders()
                };

                apiService.get("/api/students/" + $routeParams.id, config).success(function(student) {
                    if (student.id) {
                        $scope.student = angular.copy(student);
                        delete $scope.student.transactions;

                        if (student.transactions && student.transactions.length) {
                            $scope.transactions = student.transactions;
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
                    apiService.put("/api/students/", student, config).success(function() {
                        notifierService.notifySuccess("Student updated successfully.");
                        $scope.editingStudent = false;
                        $scope.StudentEditForm.submitted = false;
                    }).error(function(errorResponse) {
                        $scope.editingStudent = false;
                        $scope.displayErrors(errorResponse);
                    });
                }
            };
        }
    ]);
})(_app);