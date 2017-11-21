"use strict";

angular.module("students").config([
    "$routeProvider", function($routeProvider) {
        var authCheck = {
            auth: [
                "$q", "identityService", function($q, identityService) {
                    var defer = $q.defer();
                    if (!identityService.isLoggedIn()) {
                        defer.reject();
                    } else {
                        defer.resolve();
                    }
                    return defer.promise;
                }
            ]
        };
        $routeProvider
            .when("/students", {
                templateUrl: "Scripts/students/views/list.students.view.html",
                controller: "StudentListCtrl",
                controllerAs: "vm",
                resolve: authCheck
            })
            .when("/students/page/:pageNumber", {
                templateUrl: "Scripts/students/views/list.students.view.html",
                controller: "StudentListCtrl",
                controllerAs: "vm",
                resolve: authCheck
            })
            .when("/students/add", {
                templateUrl: "Scripts/students/views/add.student.view.html",
                controller: "StudentAddCtrl",
                controllerAs: "vm",
                resolve: authCheck
            })
            .when("/students/:id", {
                templateUrl: "Scripts/students/views/show.student.view.html",
                controller: "StudentDetailsCtrl",
                controllerAs: "vm",
                resolve: authCheck
            });
    }
]);