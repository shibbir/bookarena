"use strict";

(function(app) {
    app.controller("BookAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                $scope.book = {};
                $scope.book.isRequired = true;
                $scope.categories = [];
                service.call("/books/categories/").then(function(result) {
                    $scope.categories = result.Data;
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
            $scope.upload = function() {
                if ($scope.BookAddForm.$valid) {
                    service.call("/books/add/", $("form[name=BookAddForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);

                        $scope.book.Title = "";
                        $scope.book.Author = "";
                        $scope.book.CategoryId = "";
                        $scope.book.Edition = "";
                        $scope.book.StatusId = "";
                        $scope.book.ShortDescription = "";
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));