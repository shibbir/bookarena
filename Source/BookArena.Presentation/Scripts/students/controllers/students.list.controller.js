(function(app) {
    "use strict";

    app.controller("StudentListCtrl", [
        "$rootScope", "$routeParams", "$location", "apiService", "identityService", function($rootScope, $routeParams, $location, service, identityService) {

            var vm = this;

            vm.students = [];

            var config = {
                headers: identityService.getSecurityHeaders(),
                params: {
                    page: 0,
                    pageSize: 10
                }
            };
            if (!$routeParams.pageNumber) {
                vm.fetchingStudents = true;
                service.get("/api/students", config).success(function(result) {
                    if (result.entities.length) {
                        vm.students = result.entities;

                        vm.hasNext = result.hasNext;
                        vm.hasPrevious = result.hasPrevious;
                        vm.currentPage = result.currentPage;
                        vm.previousPage = vm.currentPage - 1;
                        vm.nextPage = vm.currentPage + 1;
                    }
                    vm.fetchingStudents = false;
                });
            } else {
                var pageNumber = parseInt($routeParams.pageNumber);
                if (isNaN(pageNumber) || pageNumber <= 0) {
                    $location.path("/students/").replace();
                } else {
                    config.params.page = pageNumber;
                    service.get("/api/students", config).success(function(result) {
                        if (result.entities.length) {
                            vm.students = result.entities;

                            vm.hasNext = result.hasNext;
                            vm.hasPrevious = result.hasPrevious;
                            vm.currentPage = result.currentPage;
                            vm.previousPage = vm.currentPage - 1;
                            vm.nextPage = vm.currentPage + 1;
                        }
                    });
                }
            }

            vm.goToPage = function(number) {
                $location.path("/students/page/" + number);
            };
        }
    ]);
})(angular.module("students"));