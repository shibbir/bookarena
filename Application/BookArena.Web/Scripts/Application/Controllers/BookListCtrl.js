"use strict";

(function(app) {
    app.controller("BookListCtrl", [
        "$scope", "$routeParams", "$location", "apiService", function($scope, $routeParams, $location, service) {
            $scope.categories = [];

            if ($routeParams.categoryId === undefined) {
                service.get("/books/").success(function(result) {
                    if (result.data.length) {
                        $scope.categories = result.data;
                    }
                });
            } else {
                var id = parseInt($routeParams.categoryId);
                if (isNaN(id)) {
                    $location.path("/").replace();
                } else {
                    service.get("/books/category/" + id).success(function(result) {
                        if (result.data.length) {
                            $scope.categories = result.data;
                        }
                    });
                }
            }
        }
    ]);
})(angular.module("bookArenaApp"));