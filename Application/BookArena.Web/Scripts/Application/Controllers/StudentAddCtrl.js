"use strict";

(function(app) {
    app.controller("StudentAddCtrl", [
        "$scope", "$rootScope", "apiService", "notifierService", function($scope, $rootScope, service, notifier) {
            $rootScope.checkForPermisssionAfter();
            $scope.register = function() {
                if ($scope.StudentRegisterForm.$valid) {
                    service.call("api/addstudent/", $("#StudentRegisterForm").serialize(), "POST").then(function(data) {
                        notifier.notify(data);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));