"use strict";

(function(app) {
    app.controller("TransactionCtrl", [
        "$scope", "$rootScope", "$location", "$routeParams", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, $routeParams, service, notifier, identityService) {
            if (identityService.isAuthenticated()) {
                if ($routeParams.transactionId === undefined) {
                    $scope.transactions = [];
                    service.get("/transactions/").success(function(result) {
                        if (result.data.length) {
                            $scope.transactions = result.data;
                        }
                    });
                } else {
                    var id = parseInt($routeParams.transactionId);
                    if (isNaN(id)) {
                        $location.path("/").replace();
                    } else {
                        service.get("/transactions/transaction/" + id).success(function(result) {
                            if (result.data) {
                                $scope.transactionDetail = result.data;
                            }
                        });
                    }
                }
            } else {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }
            $scope.receiveBook = function(transactionId) {
                service.post("/books/receive/" + transactionId, null).success(function(result) {
                    notifier.notify(result.response);
                    if (result.data) {
                        $scope.cleared = true;
                        $scope.transactionDetail.isActive = result.data.isActive;
                        $scope.transactionDetail.status = result.data.status;
                    }

                });
            };
        }
    ]);
})(angular.module("bookArenaApp"));