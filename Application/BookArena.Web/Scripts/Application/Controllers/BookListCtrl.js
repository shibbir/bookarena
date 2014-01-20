"use strict";

(function(app) {
    app.controller("BookListCtrl", [
        "$scope", "apiService", "notifierService", function($scope, service, notifier) {
            $scope.categories = [];

            service.call("/api/books").then(function (result) {
                if (result.Data.length) {
                    $scope.categories = result.Data;
                }
                notifier.notify(result.Response);
            });
        }
    ]);
})(angular.module("bookArenaApp"));