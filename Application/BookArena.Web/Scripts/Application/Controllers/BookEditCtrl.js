"use strict";

(function(app) {
    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "apiService", "notifierService", function($scope, $rootScope, $routeParams, service, notifier) {
            $rootScope.checkForPermisssionAfter();
            $scope.book = {};
            $scope.categories = [];
            service.call("/books/categories/").then(function(result) {
                $scope.categories = result.Data;
            });
            service.call("/books/book/" + $routeParams.id).then(function(result) {
                result.Data = $.parseJSON(result.Data);
                $scope.book = result.Data;
                $scope.book.CategoryId = $scope.book.Category.CategoryId;
            });
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