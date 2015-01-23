"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "apiService", function ($scope, apiService) {
            $scope.fetchingBooks = true;
            $scope.fetchingCategories = true;

            apiService.get("/api/books/").success(function (result) {
                if (result.length) {
                    $scope.latestBooks = result;
                }
                $scope.fetchingBooks = false;
            });

            apiService.get("/api/categories/").success(function (result) {
                if (result.length) {
                    Morris.Bar({
                        element: "bar-chart",
                        data: result,
                        xkey: "title",
                        ykeys: ["count"],
                        labels: ["Number Of Books"],
                        barRatio: 0.4,
                        xLabelAngle: 55,
                        hideHover: "auto"
                    });
                }
                $scope.fetchingCategories = false;
            });
        }
    ]);
})(angular.module("bookArena"));