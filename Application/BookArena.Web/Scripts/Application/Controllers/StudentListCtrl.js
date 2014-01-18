"use strict";

(function(app) {
    app.controller("StudentListCtrl", [
        "$scope", "apiService", "notifierService", function($scope, service, notifier) {
            $scope.students = [];
            service.call("/api/students").then(function(result) {
                if (result.Data.length) {
                    $scope.students = result.Data;
                }
                notifier.notify(result.Response);
            });
        }
    ]);
})(angular.module("bookArenaApp"));