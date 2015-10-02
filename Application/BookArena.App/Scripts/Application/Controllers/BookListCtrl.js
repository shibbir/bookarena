(function(app) {
    "use strict";

    app.controller("BookListCtrl", [
        "$routeParams", "$location", "apiService", function($routeParams, $location, service) {

            var vm = this;

            vm.categories = [];

            if (!$routeParams.categoryId) {
                var config = {
                    params: {
                        includeCategory: true
                    }
                };
                vm.fetchingBooks = true;
                service.get("/api/books", config).success(function(data) {
                    if (data.length) {
                        vm.categories = data;
                    }
                    vm.fetchingBooks = false;
                });
            } else {
                var id = parseInt($routeParams.categoryId);
                if (isNaN(id)) {
                    $location.path("/").replace();
                } else {
                    vm.fetchingBooks = true;
                    service.get("/api/categories/" + id + "/books").success(function(data) {
                        if (data.length) {
                            vm.categories = data;
                        }
                        vm.fetchingBooks = false;
                    });
                }
            }
        }
    ]);
})(angular.module("bookArena"));