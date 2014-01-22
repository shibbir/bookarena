"use strict";

(function(app) {
    app.controller("BookDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", function($scope, $rootScope, $routeParams, $location, service) {
            service.call("/books/book/" + $routeParams.id).then(function(result) {
                result.Data = $.parseJSON(result.Data);
                $scope.book = result.Data;
            });
            $scope.loginToEditBook = function() {
                $rootScope.globalContainer = {
                    redirectTo: "/books/edit/" + $routeParams.id
                };
                $location.path("/account/login/");
            };
        }
    ]);
})(angular.module("bookArenaApp"));