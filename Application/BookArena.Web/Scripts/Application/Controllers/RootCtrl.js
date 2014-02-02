"use strict";

(function(app) {
    app.controller("RootCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "notifierService", function($scope, $rootScope, $location, service, notifier) {
            $rootScope.authenticatedUser = {};
            $scope.quantityArray = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
            $scope.programs = ["BBA", "CSE", "CSI", "CEN", "ECO", "EEE", "ENG"];
            $scope.batches = ["25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50"];
            $scope.notImplemented = function() {
                notifier.notify({
                    ResponseType: "error",
                    Message: "Sorry! This feature is not available yet."
                });
            };
            $scope.checkAuthentication = function() {
                service.call("/account/").then(function(result) {
                    if (result.Data) {
                        $rootScope.authenticatedUser = result.Data;
                        $rootScope.authenticatedUser.IsAuthenticated = true;

                        if ($location.path() === "/account/login") {
                            $location.path("/");
                        }
                    }
                });
            };

            $scope.checkAuthentication();

            $scope.checkForPermisssionBefore = function(path) {
                if (!$rootScope.authenticatedUser.IsAuthenticated) {
                    $rootScope.globalContainer = {
                        redirectTo: path,
                        response: {
                            ResponseType: "error",
                            Message: "Access Denied! You need to login first."
                        }
                    };

                    if ($location.path() === "/account/login") {
                        notifier.notify({
                            ResponseType: "error",
                            Message: "Access Denied! You need to login first."
                        });
                    } else {
                        $location.path("/account/login");
                    }
                } else {
                    $location.path(path);
                }
            };
            $scope.openModal = function(selector) {
                $(selector).foundation("reveal", "open");
            };
            $scope.closeModal = function(selector) {
                $(selector).foundation("reveal", "close");
            };

            $scope.logout = function() {
                service.call("/account/logoff").then(function(result) {
                    $rootScope.authenticatedUser = {};
                    $rootScope.globalContainer = {
                        response: result.Response
                    };
                    $location.path("/account/login");
                });
            };
        }
    ]);
})(angular.module("bookArenaApp"));