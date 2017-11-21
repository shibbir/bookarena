(function(app) {
    "use strict";

    app.controller("TransactionCtrl", [
        "$rootScope", "$location", "$routeParams", "apiService", "notifierService", "identityService", function($rootScope, $location, $routeParams, apiService, notifierService, identityService) {

            var vm = this;

            var config = {
                headers: identityService.getSecurityHeaders()
            };

            if (!$routeParams.transactionId) {
                vm.transactions = [];

                apiService.get("/api/transactions/", config).success(function(result) {
                    if (result.length) {
                        vm.transactions = result;
                    }
                });
            } else {
                var id = parseInt($routeParams.transactionId);
                if (isNaN(id)) {
                    $location.path("/").replace();
                } else {
                    apiService.get("/api/transactions/" + id, config).success(function(result) {
                        if (result) {
                            vm.transactionDetail = result;
                        }
                    });
                }
            }

            vm.receiveBook = function(transactionId) {
                apiService.put("/api/transactions/" + transactionId, null, config).success(function(result) {
                    notifierService.notifySuccess(result.message);

                    if (result.data) {
                        vm.cleared = true;
                        vm.transactionDetail.isActive = result.data.isActive;
                        vm.transactionDetail.status = result.data.status;
                    }

                });
            };
        }
    ]);
})(angular.module("transactions"));