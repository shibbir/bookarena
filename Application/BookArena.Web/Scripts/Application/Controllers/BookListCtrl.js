"use strict";

(function(app) {
    app.controller("BookListCtrl", [
        "$scope", "$routeParams", "$location", "apiService", function($scope, $routeParams, $location, service) {
            $scope.categories = [];

            if ($routeParams.categoryId === undefined) {
                service.call("/api/books").then(function(result) {
                    result.Data = $.parseJSON(result.Data);
                    if (result.Data.length) {
                        $scope.categories = result.Data;
                    }
                });
            } else {
                var id = parseInt($routeParams.categoryId);
                if (isNaN(id)) {
                    $location.path("/").replace();
                } else {
                    service.call("/api/getFilteredBooks/" + id).then(function(result) {
                        result.Data = $.parseJSON(result.Data);
                        if (result.Data.length) {
                            $scope.categories = result.Data;
                        }
                    });
                }
            }
        }
    ]);
})(angular.module("bookArenaApp"));