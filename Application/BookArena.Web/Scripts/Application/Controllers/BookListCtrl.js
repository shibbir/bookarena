"use strict";

(function(app) {
    app.controller("BookListCtrl", [
        "$scope", "$routeParams", "$location", "apiService", function($scope, $routeParams, $location, service) {
            $scope.categories = [];

            if ($routeParams.categoryId === undefined) {
                service.call("/books/").then(function(result) {
                    if (result.data.length) {
                        $scope.categories = result.data;
                    }
                });
            } else {
                var id = parseInt($routeParams.categoryId);
                if (isNaN(id)) {
                    $location.path("/").replace();
                } else {
                    service.call("/books/category/" + id).then(function(result) {
                        if (result.data.length) {
                            $scope.categories = result.data;
                        }
                    });
                }
            }
        }
    ]);
})(angular.module("bookArenaApp"));