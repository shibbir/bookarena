"use strict";

(function(app) {
    app.controller("StudentDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "apiService", "notifierService", function($scope, $rootScope, $routeParams, service, notifier) {
            $rootScope.checkForPermisssionAfter();
            $scope.book = {};
            service.call("/students/student/" + $routeParams.id).then(function(result) {
                $scope.student = result.Data;
            });

            $scope.update = function() {
                if ($scope.StudentEditForm.$valid && $rootScope.authenticatedUser.IsAuthenticated) {
                    service.call("/students/edit/", $("form[name=StudentEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));