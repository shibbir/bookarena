"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "apiService", function($scope, service) {
            service.call("/books/latest").then(function(result) {
                if (result.Data.length) {
                    $scope.latestBooks = result.Data;
                }
            });

            service.call("/categories/").then(function(result) {
                if (result.Data.length) {
                    Morris.Bar({
                        element: "bar-chart",
                        data: result.Data,
                        xkey: "Title",
                        ykeys: ["Count"],
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