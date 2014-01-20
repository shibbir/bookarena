"use strict";

(function(app) {
    app.controller("BookDetailsCtrl", [
        "$scope", "$routeParams", "apiService", function($scope, $routeParams, service) {
            service.call("/api/book/" + $routeParams.id).then(function(result) {
                $scope.book = result.Data;
            });
        }
    ]);
})(angular.module("bookArenaApp"));