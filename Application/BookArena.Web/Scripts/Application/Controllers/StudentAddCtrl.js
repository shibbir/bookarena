"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            if (!$rootScope.authenticatedUser.IsAuthenticated) {
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
            $scope.register = function() {
                if ($scope.StudentRegisterForm.$valid) {
                    service.call("/students/add/", $("form[name=StudentRegisterForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (!result.PreserveInput) {
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