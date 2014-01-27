"use strict";

(function(app) {
    app.controller("CategoryCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $scope.categories = [];
            service.call("/categories/").then(function(result) {
                if (result.Data.length) {
                    $scope.categories = result.Data;
                }
            });
            $scope.add = function() {
                if ($scope.authenticatedUser.IsAuthenticated && $scope.CategoryAddForm.$valid) {
                    service.call("/categories/add", $("form[name=CategoryAddForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (result.Data.length) {
                            $scope.categories.push({

                            });
                        }
                    });
                }
            };
            $scope.update = function () {
                if ($scope.authenticatedUser.IsAuthenticated && $scope.CategoryAddForm.$valid) {
                    service.call("/categories/edit", $("form[name=CategoryEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (result.Data.length) {
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));