"use strict";

(function(app) {
    app.controller("StudentListCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "identityService", function($scope, $rootScope, $routeParams, $location, service, identityService) {
            if (identityService.isLoggedIn()) {
                $scope.students = [];
                var config = {
                    headers: identityService.getSecurityHeaders(),
                    params: {
                        page: 0,
                        pageSize: 10
                    }
                };
                if (!$routeParams.pageNumber) {
                    $scope.fetchingStudents = true;
                    service.get("/api/students", config).success(function(result) {
                        if (result.data.entities.length) {
                            $scope.students = result.data.entities;

                            $scope.hasNext = result.data.hasNext;
                            $scope.hasPrevious = result.data.hasPrevious;
                            $scope.currentPage = result.currentPage;
                            $scope.previousPage = $scope.currentPage - 1;
                            $scope.nextPage = $scope.currentPage + 1;
                        }
                        $scope.fetchingStudents = false;
                    });
                } else {
                    var pageNumber = parseInt($routeParams.pageNumber);
                    if (isNaN(pageNumber) || pageNumber <= 0) {
                        $location.path("/students/").replace();
                    } else {
                        config.params.page = pageNumber;
                        service.get("/api/students", config).success(function(result) {
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
})(_app);