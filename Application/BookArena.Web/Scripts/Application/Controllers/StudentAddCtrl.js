"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", "identityService", "sharedService", function($scope, $rootScope, $location, service, notifier, identityService, sharedService) {
            $(document).foundation();
            if (!identityService.isAuthenticated()) {
                $rootScope.globalContainer = {
                    redirectTo: $location.path(),
                    response: {
                        ResponseType: "error",
                        Message: "Access Denied! You need to login first."
                    }
                };
                $location.path("/account/login").replace();
            }
            $scope.student = {};
            $scope.programs = sharedService.programs();
            $scope.batches = sharedService.batches();
            $scope.register = function() {
                if ($scope.StudentRegisterForm.$valid) {
                    service.call("/students/add/", $("form[name=StudentRegisterForm]").serialize(), "POST").then(function(result) {
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