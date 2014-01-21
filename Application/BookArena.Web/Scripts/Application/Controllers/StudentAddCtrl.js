"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "apiService", "notifierService", function($scope, $rootScope, service, notifier) {
            $rootScope.checkForPermisssionAfter();
            $scope.student = {};
            $scope.register = function() {
                if ($scope.StudentRegisterForm.$valid) {
                    service.call("/students/add/", $("form[name=StudentRegisterForm]").serialize(), "POST").then(function (result) {
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