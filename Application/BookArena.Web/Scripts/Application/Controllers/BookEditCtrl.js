"use strict";

(function(app) {
    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService) {
            $(document).foundation();
            if (identityService.isAuthenticated()) {
                $scope.book = {};
                $scope.categories = [];
                service.call("/books/book/" + $routeParams.id).then(function(result) {
                    if (result.data) {
                        $scope.book = result.data;

                        service.call("/categories/").then(function(category) {
                            $scope.categories = category.data;
                        });
                    } else {
                        $location.path("/").replace();
                    }
                });
            } else {
                $rootScope.globalContainer = {
                    redirectTo: $location.path(),
                    response: {
                        ResponseType: "error",
                        Message: "Access Denied! You need to login first."
                    }
                };
                $location.path("/account/login").replace();
            }
            $scope.update = function() {
                if (identityService.isAuthenticated() && $scope.BookEditForm.$valid) {
                    service.call("/books/edit/", $("form[name=BookEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));