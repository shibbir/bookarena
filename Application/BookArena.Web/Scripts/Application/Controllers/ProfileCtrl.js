"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "$rootScope", "$location", "identityService", function($scope, $rootScope, $location, identityService) {
            if (!identityService.isAuthenticated()) {
                $rootScope.globalContainer = {
                    redirectTo: $location.path(),
                    response: {
                        ResponseType: "error",
                        Message: "Access Denied! You need to login first."
                    }
                };
                $location.path("/account/login").replace();
            }
        }
    ]);
})(angular.module("bookArenaApp"));