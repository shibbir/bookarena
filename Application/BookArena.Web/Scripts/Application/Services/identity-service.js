"use strict";

(function(app) {
    app.factory("identityService", [
        "$rootScope", "$location", "apiService", function($rootScope, $location, apiService) {
            var isAuthenticated = function() {
                return !!($rootScope.authenticatedUser && $rootScope.authenticatedUser.isAuthenticated);
            };
            var setAuthorizationData = function(data) {
                $rootScope.authenticatedUser = data;
                $rootScope.authenticatedUser.isAuthenticated = true;
            };
            var checkAuthentication = function() {
                $rootScope.authenticatedUser = {};
                apiService.call("/account/").then(function(result) {
                    if (result.data) {
                        setAuthorizationData(result.data);

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
                        response: result.response
                    };
                    $location.path("/account/login");
                });
            };
            return {
                isAuthenticated: isAuthenticated,
                checkAuthentication: checkAuthentication,
                setAuthorizationData: setAuthorizationData,
                logoff: logoff
            };
        }
    ]);
})(angular.module("bookArenaApp"));