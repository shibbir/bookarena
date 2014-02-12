"use strict";

(function(app) {
    app.factory("identityService", [
        "$rootScope", "$location", "apiService", function($rootScope, $location, apiService) {
            var isAuthenticated = function() {
                return !!($rootScope.authenticatedUser && $rootScope.authenticatedUser.isAuthenticated);
            };
            var authenticatedUser = function() {
                apiService.call("/account/").then(function(result) {
                    if (result.Data) {
                        $rootScope.authenticatedUser = result.Data;
                        $rootScope.authenticatedUser.isAuthenticated = true;

                        if ($location.path() === "/account/login") {
                            $location.path("/");
                        }
                    }
                });
            };
            return {
                isAuthenticated: isAuthenticated,
                authenticatedUser: authenticatedUser
            };
        }
    ]);
})(angular.module("bookArenaApp"));