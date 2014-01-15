"use strict";

(function(app) {
    app.controller("BookCtrl", ["$scope", "DataService", "Notifier", function($scope, service, notifier) {
        $scope.books = [];

        service.get("/books/get").then(function(data) {
            if (data.length) {
                notifier.notify({responseType: "success", message: "Data retrieved successfully!"});
            }
            $scope.books = data;
        });
    }]);
})(angular.module("bookArenaApp"));