"use strict";

(function(app) {
    app.controller("BookTransactionCtrl", [
        "$scope", "$rootScope", "$location", "$routeParams", "apiService", function($scope, $rootScope, $location, $routeParams, service) {
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                if ($routeParams.transactionId === undefined) {
                    $scope.transactions = [];
                    service.call("/books/transactions/").then(function(result) {
                        if (result.Data.length) {
                            $scope.transactions = result.Data;
                        }
                    });
                } else {
                    var id = parseInt($routeParams.transactionId);
                    if (isNaN(id)) {
                        $location.path("/").replace();
                    } else {
                        service.call("/books/transaction/" + id).then(function(result) {
                            result.Data = $.parseJSON(result.Data);
                            if (result.Data) {
                                $scope.transactionDetail = result.Data;
                            }
                        });
                    }
                }
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
        }
    ]);
})(angular.module("bookArenaApp"));