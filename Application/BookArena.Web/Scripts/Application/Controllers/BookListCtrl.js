"use strict";

(function(app) {
    app.controller("BookListCtrl", ["$scope", "apiService", "notifierService", function ($scope, service, notifier) {
        $scope.books = [];

        service.call("/api/books").then(function (data) {
            if (data.length) {
                $scope.books = data;
                notifier.notify({responseType: "success", message: "Data retrieved successfully!"});
            }
        });
    }]);
})(angular.module("bookArenaApp"));