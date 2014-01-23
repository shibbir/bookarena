"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "apiService", "notifierService", function($scope, service, notifier) {
            $scope.latestBooks = [];
            service.call("/books/latest").then(function(result) {
                if (result.Data.length) {
                    $scope.latestBooks = result.Data;
                }
            });

            $scope.pieChart = function(target, plotData) {
                Highcharts.getOptions().colors = $.map(Highcharts.getOptions().colors, function(color) {
                    return {
                        radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
                        stops: [
                            [0, color],
                            [1, Highcharts.Color(color).brighten(-0.3).get('rgb')]
                        ]
                    };
                });

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: target,
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false
                    },
                    title: {
                        text: ''
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage}%</b>',
                        percentageDecimals: 1
                    },
                    credits: {
                        enabled: false
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false,
                                color: '#000000',
                                connectorColor: '#000000',
                                formatter: function() {
                                    return '<b>' + this.point.name + '</b>: ' + this.y + ' books';
                                }
                            },
                            showInLegend: true
                        }
                    },
                    series: [
                        {
                            type: 'pie',
                            name: 'Popularity',
                            data: plotData
                        }
                    ]
                });
            };
            $scope.pieChart("chart", [
                { name: "ASP", y: 1 },
                { name: "PHP", y: 2 },
                { name: "JavaScript", y: 1 },
                { name: "CSS", y: 2 },
                { name: "Windows 8", y: 1 },
                { name: "CSS", y: 2 },
                { name: "Windows 8", y: 1 },
                { name: "CMS", y: 2 }
            ]);
        }
    ]);
})(angular.module("bookArenaApp"));