"use strict";

(function(app) {
    app.controller("BookAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "identityService", "notifierService", "sharedService", function($scope, $rootScope, $location, apiService, identityService, notifier, sharedService) {
            $(document).foundation();
            if (identityService.isAuthenticated()) {
                $scope.book = {};
                $scope.book.isRequired = true;
                $scope.categories = [];
                $scope.bookQuantities = sharedService.bookQuantities();
                apiService.call("/categories/").then(function(result) {
                    $scope.categories = result.data;
                });
            } else {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }
            $scope.add = function() {
                if ($scope.BookAddForm.$valid) {
                    apiService.call("/books/add/", $("form[name=BookAddForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.response);

                        if (!result.preserveInput) {
                            $scope.book.title = "";
                            $scope.book.author = "";
                            $scope.book.categoryId = "";
                            $scope.book.quantity = "";
                            $scope.book.shortDescription = "";
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));