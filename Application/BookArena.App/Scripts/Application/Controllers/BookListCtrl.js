"use strict";

(function(app) {
    app.controller("BookListCtrl", [
        "$scope", "$routeParams", "$location", "apiService", function($scope, $routeParams, $location, service) {
            $scope.categories = [];

            if (!$routeParams.categoryId) {
                var config = {
                    params: {
                        includeCategory: true
                    }
                };
                $scope.fetchingBooks = true;
                service.get("/api/books", config).success(function(result) {
                    if (result.length) {
                        $scope.categories = result;
                    }
                    $scope.fetchingBooks = false;
                });
            } else {
                var id = parseInt($routeParams.categoryId);
                if (isNaN(id)) {
                    $location.path("/").replace();
                } else {
                    $scope.fetchingBooks = true;
                    service.get("/api/categories/" + id + "/books").success(function(result) {
                        if (result.length) {
                            $scope.categories = result;
                        }
                        $scope.fetchingBooks = false;
                    });
                }
            }
        }
    ]);
})(_app);