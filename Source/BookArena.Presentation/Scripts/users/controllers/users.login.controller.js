(function(app) {
    "use strict";

    app.controller("LoginCtrl", [
        "$rootScope", "$location", "notifierService", "identityService", function($rootScope, $location, notifierService, identityService) {

            var vm = this;

            var tempGlobalContainer = $.extend(true, {}, $rootScope.globalContainer);
            $rootScope.globalContainer = null;

            if (identityService.isLoggedIn()) {
                $location.path("/").replace();
            }

            vm.user = {};

            if (tempGlobalContainer && tempGlobalContainer.message) {
                if (tempGlobalContainer.notifyType === "success") {
                    notifierService.notifySuccess(tempGlobalContainer.message);
                } else if (tempGlobalContainer.notifyType === "error") {
                    notifierService.notifyError(tempGlobalContainer.message);
                } else if (tempGlobalContainer.notifyType === "info") {
                    notifierService.notifyInfo(tempGlobalContainer.message);
                } else if (tempGlobalContainer.notifyType === "warning") {
                    notifierService.notifyWarning(tempGlobalContainer.message);
                }
            }

            vm.login = function() {
                vm.LoginForm.submitted = true;

                if (vm.LoginForm.$valid) {
                    vm.loginInProgress = true;

                    identityService.login(vm.user).success(function(data) {
                        vm.loginInProgress = false;

                        if (data.userName && data.access_token) {
                            identityService.setAccessToken(data.access_token);
                            identityService.setAuthorizedUserData(data);

                            if (tempGlobalContainer && tempGlobalContainer.redirectTo) {
                                $location.path(tempGlobalContainer.redirectTo).replace();
                            } else {
                                $location.path("/").replace();
                            }
                        }
                    }).error(function(error) {
                        vm.loginInProgress = false;

                        if (error.error_description) {
                            notifierService.notifyError(error.error_description);
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("users"));