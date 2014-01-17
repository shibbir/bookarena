"use strict";

(function(app) {
    app.controller("BookEditCtrl", ["$scope", "$routeParams", "apiService", "notifierService", function($scope, $routeParams, service, notifier) {
        service.call("/api/book/" + $routeParams.id).then(function (data) {
            $scope.book = data;
            //notifier.notify({ responseType: "success", message: "Data retrieved successfully!" });
        });
    }]);
})(angular.module("bookArenaApp"));