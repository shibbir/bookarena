"use strict";

(function(app) {
    app.factory("identityService", [
        "$rootScope", "$location", "apiService", function($rootScope, $location, apiService) {
            var isAuthenticated = function() {
                return !!($rootScope.authenticatedUser && $rootScope.authenticatedUser.isAuthenticated);
            };
            var setAuthorization = function(data) {
                $rootScope.authenticatedUser = data;
                $rootScope.authenticatedUser.isAuthenticated = true;
            };
            var authenticatedUser = function() {
                $rootScope.authenticatedUser = {};
                apiService.call("/account/").then(function(result) {
                    if (result.Data) {
                        setAuthorization(result.Data);

                        if ($location.path() === "/account/login") {
                            $location.path("/");
                        }
                    }
                });
            };
            var logoff = function() {
                apiService.call("/account/logoff").then(function(result) {
                    $rootScope.authenticatedUser = {};
                    $rootScope.globalContainer = {
                        response: result.Response
                    };
                    $location.path("/account/login");
                });
            };
            return {
                isAuthenticated: isAuthenticated,
                authenticatedUser: authenticatedUser,
                setAuthorization: setAuthorization,
                logoff: logoff
            };
        }
    ]);
})(angular.module("bookArenaApp"));