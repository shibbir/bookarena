"use strict";

(function(app) {
    app.controller("TransactionCtrl", [
        "$scope", "$rootScope", "$location", "$routeParams", "apiService", "notifierService", function($scope, $rootScope, $location, $routeParams, service, notifier) {
            if ($rootScope.authenticatedUser.IsAuthenticated) {
                if ($routeParams.transactionId === undefined) {
                    $scope.transactions = [];
                    service.call("/transactions/").then(function(result) {
                        if (result.Data.length) {
                            $scope.transactions = result.Data;
                        }
                    });
                } else {
                    var id = parseInt($routeParams.transactionId);
                    if (isNaN(id)) {
                        $location.path("/").replace();
                    } else {
                        service.call("/transactions/transaction/" + id).then(function(result) {
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
            $scope.receiveBook = function(transactionId) {
                service.call("/books/receive/" + transactionId, null, "POST").then(function(result) {
                    notifier.notify(result.Response);
                    if (result.Data) {
                        $scope.cleared = true;
                        $scope.transactionDetail.IsActive = result.Data.IsActive;
                        $scope.transactionDetail.Status = result.Data.Status;
                    }

                });
            };
        }
    ]);
})(angular.module("bookArenaApp"));