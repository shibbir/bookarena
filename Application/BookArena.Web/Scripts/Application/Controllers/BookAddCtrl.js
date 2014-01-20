"use strict";

(function(app) {
    app.controller("BookAddCtrl", [
        "$scope", "$rootScope", "apiService", "notifierService", function($scope, $rootScope, service, notifier) {
            $rootScope.checkForPermisssionAfter();
            $scope.book = {};
            $scope.book.isRequired = true;
            $scope.categories = [];
            service.call("/api/categories/").then(function (result) {
                $scope.categories = result.Data;
            });
            $scope.upload = function() {
                if ($scope.BookAddForm.$valid) {
                    service.call("/api/addbook/", $("form[name=BookAddForm]").serialize(), "POST").then(function(result) {
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