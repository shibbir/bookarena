"use strict";

(function(app) {
    app.controller("BookCtrl", ["$scope", "DataService", "Notifier", function($scope, service, notifier) {
        $scope.books = [];

        service.get("/books/get").then(function(data) {
            if (data.length) {
                $scope.books = data;
                notifier.notify({responseType: "success", message: "Data retrieved successfully!"});
            }
        });
    }]);
})(angular.module("bookArenaApp"));