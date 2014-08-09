(function (app) {
    "use strict";

    app.controller("TransactionCtrl", [
        "$scope", "$rootScope", "$location", "$routeParams", "apiService", "notifierService", "identityService", function($scope, $rootScope, $location, $routeParams, apiService, notifierService, identityService) {
            if (identityService.isLoggedIn()) {

                var config = {
                    headers: identityService.getSecurityHeaders()
                };

                if (!$routeParams.transactionId) {
                    $scope.transactions = [];
                    apiService.get("/api/transactions/", config).success(function(result) {
                        if (result.length) {
                            $scope.transactions = result;
                        }
                    });
                } else {
                    var id = parseInt($routeParams.transactionId);
                    if (isNaN(id)) {
                        $location.path("/").replace();
                    } else {
                        apiService.get("/api/transactions/" + id, config).success(function(result) {
                            if (result) {
                                $scope.transactionDetail = result;
                            }
                        });
                    }
                }
            } else {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }
            $scope.receiveBook = function(transactionId) {
                apiService.put("/api/transactions/" + transactionId, null, config).success(function(result) {
                    notifierService.notifySuccess(result.message);
                    if (result.data) {
                        $scope.cleared = true;
                        $scope.transactionDetail.isActive = result.data.isActive;
                        $scope.transactionDetail.status = result.data.status;
                    }

                });
            };
        }
    ]);
})(_app);