"use strict";

(function(app) {
    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", function($scope, $rootScope, $routeParams, $location, service, notifier) {
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                $scope.book = {};
                $scope.categories = [];
                service.call("/categories/").then(function(result) {
                    $scope.categories = result.Data;
                });
                service.call("/books/book/" + $routeParams.id).then(function(result) {
                    $scope.book = result.Data;
                    $scope.book.CategoryId = $scope.book.CategoryId;
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