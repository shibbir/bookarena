"use strict";

(function(app) {
    app.controller("StudentListCtrl", [
        "$scope", "apiService", function($scope, service) {
            $scope.students = [];
            service.call("/api/students").then(function(result) {
                if (result.Data.length) {
                    $scope.students = result.Data;
                }
            });
        }
    ]);
})(angular.module("bookArenaApp"));