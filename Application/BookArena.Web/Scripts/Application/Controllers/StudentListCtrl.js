"use strict";

(function(app) {
    app.controller("StudentListCtrl", [
        "$scope", "$routeParams", "$location", "apiService", function($scope, $routeParams, $location, service) {
            $scope.students = [];
            if ($routeParams.pageNumber === undefined) {
                service.call("/students/").then(function(result) {
                    if (result.Data.Entities.length) {
                        $scope.students = result.Data.Entities;

                        $scope.hasNext = result.Data.HasNext;
                        $scope.hasPrevious = result.Data.HasPrevious;
                        $scope.currentPage = result.CurrentPage;
                        $scope.previousPage = $scope.currentPage - 1;
                        $scope.nextPage = $scope.currentPage + 1;
                    }
                });
            } else {
                var pageNumber = parseInt($routeParams.pageNumber);
                if (isNaN(pageNumber) || pageNumber <= 0) {
                    $location.path("/students/").replace();
                } else {
                    service.call("/students/index?page=" + pageNumber).then(function(result) {
                        if (result.Data.Entities.length) {
                            $scope.students = result.Data.Entities;

                            $scope.hasNext = result.Data.HasNext;
                            $scope.hasPrevious = result.Data.HasPrevious;
                            $scope.currentPage = result.CurrentPage;
                            $scope.previousPage = $scope.currentPage - 1;
                            $scope.nextPage = $scope.currentPage + 1;
                        }
                    });
                }
            }

            $scope.goToPage = function (number) {
                $location.path("/students/page/" + number);
            };
        }
    ]);
})(angular.module("bookArenaApp"));