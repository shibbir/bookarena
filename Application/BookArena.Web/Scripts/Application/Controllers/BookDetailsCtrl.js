"use strict";

(function(app) {
    app.controller("BookDetailsCtrl", [
        "$scope", "$routeParams", "apiService", "notifierService", function($scope, $routeParams, service, notifier) {
            service.call("/api/book/" + $routeParams.id).then(function(result) {
                $scope.book = result.Data;
                notifier.notify(result.Response);
            });
        }
    ]);
})(angular.module("bookArenaApp"));