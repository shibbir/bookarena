"use strict";

angular.module("transactions").config([
    "$routeProvider", function($routeProvider) {
        var authCheck = {
            auth: [
                "$q", "identityService", function($q, identityService) {
                    var defer = $q.defer();
                    if (!identityService.isLoggedIn()) {
                        defer.reject();
                    } else {
                        defer.resolve();
                    }
                    return defer.promise;
                }
            ]
        };
        $routeProvider
            .when("/transactions", {
                templateUrl: "Scripts/transactions/views/list.transactions.view.html",
                controller: "TransactionCtrl",
                controllerAs: "vm",
                resolve: authCheck
            })
            .when("/transactions/:transactionId", {
                templateUrl: "Scripts/transactions/views/show.transaction.view.html",
                controller: "TransactionCtrl",
                controllerAs: "vm",
                resolve: authCheck
            });
    }
]);