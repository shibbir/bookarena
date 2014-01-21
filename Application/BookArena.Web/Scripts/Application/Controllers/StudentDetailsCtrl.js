"use strict";

(function(app) {
    app.controller("StudentDetailsCtrl", [
        "$scope", "$routeParams", "apiService", function($scope, $routeParams, service) {
            service.call("/students/student/" + $routeParams.id).then(function(result) {
                $scope.student = result;
            });
        }
    ]);
})(angular.module("bookArenaApp"));