"use strict";

(function(app) {
    app.controller("BookListCtrl", [
        "$scope", "apiService", "notifierService", function($scope, service, notifier) {
            $scope.books = [];

            service.call("/api/books").then(function(result) {
                if (result.Data.length) {
                    $scope.books = result.Data;
                }
                notifier.notify(result.Response);
            });
        }
    ]);
})(angular.module("bookArenaApp"));