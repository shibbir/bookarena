"use strict";

(function(app) {
    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", function($scope, $rootScope, $routeParams, $location, service, notifier) {
            $(document).foundation();
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                $scope.book = {};
                $scope.categories = [];
                service.call("/books/book/" + $routeParams.id).then(function(result) {
                    if (result.Data) {
                        $scope.book = result.Data;
                        $scope.book.CategoryId = $scope.book.CategoryId;

                        service.call("/categories/").then(function(category) {
                            $scope.categories = category.Data;
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
                if ($scope.BookEditForm.$valid && $rootScope.authenticatedUser.IsAuthenticated) {
                    service.call("/books/edit/", $("form[name=BookEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));