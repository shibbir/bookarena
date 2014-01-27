"use strict";

(function(app) {
    app.controller("CategoryCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $scope.categories = [];
            $scope.category = {};
            service.call("/categories/").then(function(result) {
                if (result.Data.length) {
                    $scope.categories = result.Data;
                }
            });
            $scope.addCategory = function () {
                if ($rootScope.authenticatedUser.IsAuthenticated && $scope.CategoryAddForm.$valid) {
                    service.call("/categories/add/", $("form[name=CategoryAddForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (result.Data) {
                            $scope.category.Title = "";
                            $scope.CategoryAddForm.$setPristine();
                            $scope.categories.push(result.Data);
                        }
                    });
                }
            };
            $scope.updateCategory = function () {
                if ($rootScope.authenticatedUser.IsAuthenticated && $scope.CategoryAddForm.$valid) {
                    service.call("/categories/edit/", $("form[name=CategoryEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));