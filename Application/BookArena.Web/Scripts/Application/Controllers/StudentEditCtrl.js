"use strict";

(function(app) {
    app.controller("StudentEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "apiService", "notifierService", function($scope, $rootScope, $routeParams, service, notifier) {
            $rootScope.checkForPermisssionAfter();
            $scope.book = {};
            service.call("/api/student/" + $routeParams.id).then(function(result) {
                $scope.student = result.Data;
            });
            $scope.update = function() {
                if ($scope.StudentEditForm.$valid && $rootScope.authenticatedUser.IsAuthenticated) {
                    service.call("/api/editstudent/", $("form[name=StudentEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));