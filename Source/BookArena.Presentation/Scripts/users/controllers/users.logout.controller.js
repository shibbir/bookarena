(function(app) {
    "use strict";

    app.controller("LogoutCtrl", [
        "$rootScope", "$location", "identityService", function($rootScope, $location, identityService) {

            identityService.logout().success(function(result) {
                identityService.clearAccessToken();
                identityService.clearAuthorizedUserData();
                $rootScope.globalContainer = {
                    notifyType: "success",
                    message: result.message
                };
                $location.path("/account/login");
            });
        }
    ]);
})(angular.module("users"));