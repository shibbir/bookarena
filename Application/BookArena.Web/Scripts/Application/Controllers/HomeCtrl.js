"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "apiService", function($scope, service) {
            $scope.latestBooks = [];
            service.call("/books/latest").then(function(result) {
                if (result.Data.length) {
                    $scope.latestBooks = result.Data;
                }
            });

            Morris.Bar({
                element: 'bar-chart',
                data: [
                    { category: 'ASP', geekbench: 2 },
                    { category: 'C#', geekbench: 3 },
                    { category: 'CSS', geekbench: 1 },
                    { category: 'JavaScript', geekbench: 4 },
                    { category: 'PHP', geekbench: 5 },
                    { category: 'Windows 8', geekbench: 6 }
                ],
                xkey: 'category',
                ykeys: ['geekbench'],
                labels: ['Number Of Books'],
                barRatio: 0.4,
                xLabelAngle: 35,
                hideHover: 'auto'
            });
        }
    ]);
})(angular.module("bookArenaApp"));