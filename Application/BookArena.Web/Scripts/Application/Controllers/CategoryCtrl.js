"use strict";

(function(app) {
    app.controller("CategoryCtrl", [
        "$scope", "$rootScope", "$location", "$filter", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, $filter, service, notifier, identityService) {
            $scope.categories = [];
            $scope.category = {};
            service.call("/categories/").then(function(result) {
                if (result.Data.length) {
                    $scope.categories = result.Data;
                }
            });
            $scope.addCategory = function() {
                if (identityService.isAuthenticated() && $scope.CategoryAddForm.$valid) {
                    service.call("/categories/add/", $("form[name=CategoryAddForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (result.Data) {
                            $scope.category.Title = "";
                            $scope.CategoryAddForm.$setPristine();
                            result.Data.Count = 0;
                            $scope.categories.push(result.Data);
                        }
                    });
                }
            };
            $scope.updateCategory = function() {
                if (identityService.isAuthenticated() && $scope.CategoryEditForm.$valid) {
                    service.call("/categories/edit/", $("form[name=CategoryEditForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (result.Data) {
                            var filteredCategories = $filter("filter")($scope.categories, { CategoryId: result.Data.CategoryId }, true);
                            filteredCategories[0].Title = result.Data.Title;
                        }
                    });
                }
            };
            $scope.initCategoryEditForm = function(category) {
                $scope.openModal("#CategoryEditModal");
                $scope.editableCategory = $.extend(true, {}, category);
            };
        }
    ]);
})(angular.module("bookArenaApp"));