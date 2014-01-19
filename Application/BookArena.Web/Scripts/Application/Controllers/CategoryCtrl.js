"use strict";

(function(app) {
    app.controller("CategoryCtrl", [
        "$scope", "apiService", "notifierService", function($scope, service, notifier) {
            $scope.categories = [];

            service.call("/api/categories").then(function(result) {
                if (result.Data.length) {
                    $scope.categories = result.Data;
                }
                notifier.notify(result.Response);
            });
        }
    ]);
})(angular.module("bookArenaApp"));