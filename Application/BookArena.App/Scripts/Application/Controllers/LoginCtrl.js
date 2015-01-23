(function(app) {
    "use strict";

    app.controller("LoginCtrl", [
        "$scope", "$rootScope", "$location", "notifierService", "identityService", function($scope, $rootScope, $location, notifierService, identityService) {
            var tempGlobalContainer = $.extend(true, {}, $rootScope.globalContainer);
            $rootScope.globalContainer = null;

            if (identityService.isLoggedIn()) {
                $location.path("/").replace();
            }

            $scope.user = {
                email: "shibbir.cse@gmail.com",
                password: "HakunaMatata71"
            };

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

            $scope.login = function(user) {
                $scope.LoginForm.submitted = true;

                if ($scope.LoginForm.$valid) {
                    $scope.loginInProgress = true;

                    identityService.login(user).success(function(data) {
                        $scope.loginInProgress = false;

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
                        $scope.loginInProgress = false;

                        if (error.error_description) {
                            notifierService.notifyError(error.error_description);
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArena"));