"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "apiService", function($scope, service) {
            service.call("/books/latest").then(function(result) {
                if (result.data.length) {
                    $scope.latestBooks = result.data;
                }
            });

            service.call("/categories/").then(function(result) {
                if (result.data.length) {
                    Morris.Bar({
                        element: "bar-chart",
                        data: result.data,
                        xkey: "title",
                        ykeys: ["count"],
                        labels: ["Number Of Books"],
                        barRatio: 0.4,
                        xLabelAngle: 55,
                        hideHover: "auto"
                    });
                }
            });
        }
    ]);
})(angular.module("bookArenaApp"));