"use strict";

(function(app) {
    app.controller("StudentListCtrl", ["$scope", "apiService", "notifierService", function($scope, service, notifier) {
        $scope.students = [];
        service.call("/api/students").then(function (data) {
            if (data.length) {
                $scope.students = data;
                notifier.notify({ responseType: "success", message: "Data retrieved successfully!" });
            }
        });
    }]);
})(angular.module("bookArenaApp"));