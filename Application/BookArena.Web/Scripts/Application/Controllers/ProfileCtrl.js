"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "$rootScope", "$location", function($scope, $rootScope, $location) {
            if (!$scope.authenticatedUser.IsAuthenticated) {
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