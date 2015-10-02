(function(app) {
    "use strict";

    app.controller("CategoryCtrl", [
        "$scope", "$rootScope", "$location", "$filter", "apiService", "notifierService", "identityService", "sharedService",
        function($scope, $rootScope, $location, $filter, apiService, notifierService, identityService, sharedService) {

            var vm = this;

            vm.categories = [];
            vm.fetchingCategories = true;

            apiService.get("/api/categories/").success(function(data) {
                if (data.length) {
                    vm.categories = data;
                }
                vm.fetchingCategories = false;
            });

            $scope.addCategory = function() {
                $scope.CategoryAddForm.submitted = true;

                if (identityService.isLoggedIn() && $scope.CategoryAddForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.post("/api/categories/", $scope.category, config).success(function (newCategory) {
                        notifierService.notifySuccess("Category created successfully.");
                        $scope.CategoryAddForm.submitted = false;

                        $scope.category.title = "";
                        $scope.CategoryAddForm.$setPristine();
                        newCategory.count = 0;

                        vm.categories.push(newCategory);
                        
                    }).error(function(errorResponse) {
                        sharedService.displayErrors(errorResponse);
                    });
                }
            };

            $scope.updateCategory = function(editableCategory) {
                $scope.CategoryEditForm.submitted = true;

                if (identityService.isLoggedIn() && $scope.CategoryEditForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.put("/api/categories/", editableCategory, config).success(function(category) {
                        notifierService.notifySuccess("Category updated successfully.");

                        var filteredCategories = $filter("filter")(vm.categories, { id: category.id }, true);
                        filteredCategories[0].title = category.title;

                    }).error(function(errorResponse) {
                        sharedService.displayErrors(errorResponse);
                    });
                }
            };

            vm.initCategoryEditForm = function(category) {
                $("#CategoryEditModal").foundation("reveal", "open");
                $scope.editableCategory = $.extend(true, {}, category);
            };
        }
    ]);
})(angular.module("bookArena"));