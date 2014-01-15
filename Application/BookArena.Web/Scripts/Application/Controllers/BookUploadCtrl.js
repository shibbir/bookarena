"use strict";

(function(app) {
    app.controller("BookUploadCtrl", ["$scope", "DataService", "Notifier", function($scope, service, notifier) {
        $scope.data = {};
        $scope.data.isRequired = true;
        $scope.uploadBook = function() {
            if ($scope.BookUploadForm.$valid) {
                service.get("/books/upload/", {}).then(function(data) {
                    notifier.notify({ responseType: "success", message: "Book uploaded successfully!" });
                });
            }
        };
    }]);
})(angular.module("bookArenaApp"));