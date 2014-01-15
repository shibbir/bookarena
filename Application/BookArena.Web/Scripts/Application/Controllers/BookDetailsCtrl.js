"use strict";

(function(app) {
    app.controller("BookDetailsCtrl", ["$scope", "DataService", "Notifier", "$routeParams", function($scope, service, notifier, $routeParams) {
        service.get("/books/details/" + $routeParams.id).then(function(data) {
            $scope.book = data;
            notifier.notify({ responseType: "success", message: "Data retrieved successfully!" });
        });
    }]);
})(angular.module("bookArenaApp"));