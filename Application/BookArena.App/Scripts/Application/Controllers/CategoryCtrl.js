"use strict";

(function(app) {
    app.controller("CategoryCtrl", [
        "$scope", "$rootScope", "$location", "$filter", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, $filter, apiService, notifierService, identityService) {
            $scope.categories = [];
            $scope.fetchingCategories = true;
            apiService.get("/api/categories/").success(function(result) {
                if (result.length) {
                    $scope.categories = result;
                }
                $scope.fetchingCategories = false;
            });
            $scope.addCategory = function(category) {
                $scope.CategoryAddForm.submitted = true;

                if (identityService.isLoggedIn() && $scope.CategoryAddForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.post("/api/categories/", category, config).success(function(result) {
                        notifierService.notifySuccess(result.message);
                        if (result.data) {
                            $scope.category.title = "";
                            $scope.CategoryAddForm.$setPristine();
                            result.data.count = 0;
                            $scope.categories.push(result.data);
                        }
                    }).error(function(error) {
                        notifierService.notifyError(error.message);
                    });
                }
            };
            $scope.updateCategory = function(editableCategory) {
                $scope.CategoryEditForm.submitted = true;

                if (identityService.isLoggedIn() && $scope.CategoryEditForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.put("/api/categories/", editableCategory, config).success(function(result) {
                        notifierService.notifySuccess(result.message);
                        if (result.data) {
                            var filteredCategories = $filter("filter")($scope.categories, { categoryId: result.data.categoryId }, true);
                            filteredCategories[0].title = result.data.title;
                        }
                    }).error(function(error) {
                        notifierService.notifyError(error.message);
                    });
                }
            };
            $scope.initCategoryEditForm = function(category) {
                $scope.openModal("#CategoryEditModal");
                $scope.editableCategory = $.extend(true, {}, category);
            };
        }
    ]);
})(_app);