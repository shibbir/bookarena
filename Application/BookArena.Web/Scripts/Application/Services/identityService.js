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
                apiService.get("/account/").success(function(result) {
                    if (result.data) {
                        setAuthorizationData(result.data);

                        if ($location.path() === "/account/login") {
                            $location.path("/");
                        }
                    }
                });
            };
            var logoff = function() {
                apiService.get("/account/logoff").success(function(result) {
                    $rootScope.authenticatedUser = {};
                    $rootScope.globalContainer = {
                        response: result.response
                    };
                    $location.path("/account/login");
                });
            };
            var createAccessDeniedResponse = function(path) {
                $rootScope.globalContainer = {
                    redirectTo: path ? path : $location.path(),
                    response: {
                        responseType: "error",
                        message: "Access Denied! You need to login first."
                    }
                };
            };
            return {
                isAuthenticated: isAuthenticated,
                checkAuthentication: checkAuthentication,
                setAuthorizationData: setAuthorizationData,
                logoff: logoff,
                createAccessDeniedResponse: createAccessDeniedResponse
            };
        }
    ]);
})(angular.module("bookArenaApp"));