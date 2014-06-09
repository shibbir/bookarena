"use strict";

(function(app) {
    app.controller("CategoryCtrl", [
        "$scope", "$rootScope", "$location", "$filter", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, $filter, service, notifier, identityService) {
            $scope.categories = [];
            service.get("/categories/").success(function(result) {
                if (result.data.length) {
                    $scope.categories = result.data;
                }
            });
            $scope.addCategory = function(category) {
                if (identityService.isAuthenticated() && $scope.CategoryAddForm.$valid) {
                    service.post("/categories/add/", category).success(function(result) {
                        notifier.notify(result.response);
                        if (result.data) {
                            $scope.category.title = "";
                            $scope.CategoryAddForm.$setPristine();
                            result.data.count = 0;
                            $scope.categories.push(result.data);
                        }
                    });
                }
            };
            $scope.updateCategory = function (editableCategory) {
                if (identityService.isAuthenticated() && $scope.CategoryEditForm.$valid) {
                    service.post("/categories/edit/", editableCategory).success(function (result) {
                        notifier.notify(result.response);
                        if (result.data) {
                            var filteredCategories = $filter("filter")($scope.categories, { categoryId: result.data.categoryId }, true);
                            filteredCategories[0].title = result.data.title;
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