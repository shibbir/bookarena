"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "apiService", function(apiService) {

            var vm = this;

            vm.fetchingBooks = true;
            vm.fetchingCategories = true;

            apiService.get("/api/books/").success(function(data) {
                if (data.length) {
                    vm.latestBooks = data;
                }
                vm.fetchingBooks = false;
            });

            apiService.get("/api/categories/").success(function(data) {
                if (data.length) {
                    Morris.Bar({
                        element: "bar-chart",
                        data: data,
                        xkey: "title",
                        ykeys: ["count"],
                        labels: ["Number Of Books"],
                        barRatio: 0.4,
                        xLabelAngle: 55,
                        hideHover: "auto"
                    });
                }
                vm.fetchingCategories = false;
            });
        }
    ]);
})(angular.module("bookArena"));