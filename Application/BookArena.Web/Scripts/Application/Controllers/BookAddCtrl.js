﻿"use strict";

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
            $scope.add = function() {
                if ($scope.BookAddForm.$valid) {
                    apiService.call("/books/add/", $("form[name=BookAddForm]").serialize(), "POST").then(function(result) {
                        notifier.notify(result.Response);

                        if (!result.PreserveInput) {
                            $scope.book.Title = "";
                            $scope.book.Author = "";
                            $scope.book.CategoryId = "";
                            $scope.book.Edition = "";
                            $scope.book.Quantity = "";
                            $scope.book.ShortDescription = "";
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));