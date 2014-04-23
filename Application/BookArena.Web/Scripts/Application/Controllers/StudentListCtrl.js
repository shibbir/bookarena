"use strict";

(function(app) {
    app.controller("StudentListCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "identityService", function ($scope, $rootScope, $routeParams, $location, service, identityService) {
            if (identityService.isAuthenticated()) {
                $scope.students = [];
                if ($routeParams.pageNumber === undefined) {
                    service.call("/students/").then(function(result) {
                        if (result.data.entities.length) {
                            $scope.students = result.data.entities;

                            $scope.hasNext = result.data.hasNext;
                            $scope.hasPrevious = result.data.hasPrevious;
                            $scope.currentPage = result.currentPage;
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
                            if (result.data.entities.length) {
                                $scope.students = result.data.entities;

                                $scope.hasNext = result.data.hasNext;
                                $scope.hasPrevious = result.data.hasPrevious;
                                $scope.currentPage = result.currentPage;
                                $scope.previousPage = $scope.currentPage - 1;
                                $scope.nextPage = $scope.currentPage + 1;
                            }
                        });
                    }
                }
            } else {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }

            $scope.goToPage = function(number) {
                $location.path("/students/page/" + number);
            };
        }
    ]);
})(angular.module("bookArenaApp"));