"use strict";

(function(app) {
    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService) {
            $(document).foundation();
            if (identityService.isAuthenticated()) {
                $scope.book = {};
                $scope.categories = [];
                service.get("/books/book/" + $routeParams.id).success(function(result) {
                    if (result.data) {
                        $scope.book = result.data;

                        service.get("/categories/").success(function(category) {
                            $scope.categories = category.data;
                        });
                    } else {
                        $location.path("/").replace();
                    }
                });
            } else {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }
            $scope.update = function(book) {
                if (identityService.isAuthenticated() && $scope.BookEditForm.$valid) {
                    service.post("/books/edit/", book).success(function(result) {
                        notifier.notify(result.response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));