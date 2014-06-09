"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $location, service, notifier, identityService, sharedService) {
            $(document).foundation();
            if (!identityService.isAuthenticated()) {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }
            $scope.programs = sharedService.programs();
            $scope.batches = sharedService.batches();

            $scope.register = function(student) {
                if ($scope.StudentRegisterForm.$valid) {
                    service.post("/students/add/", student).success(function (result) {
                        notifier.notify(result.response);
                        if (!result.preserveInput) {
                            $scope.student.firstName = "";
                            $scope.student.lastName = "";
                            $scope.student.program = "";
                            $scope.student.batch = "";
                            $scope.student.idCardNumber = "";
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));