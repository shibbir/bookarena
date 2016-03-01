"use strict";

(function(app) {
    app.config([
        "$routeProvider", function($routeProvider) {
            $routeProvider
                .when("/", {
                    templateUrl: "Scripts/core/views/home.view.html",
                    controller: "HomeCtrl",
                    controllerAs: "vm"
                })
                .when("/credit", {
                    templateUrl: "Scripts/core/views/credit.view.html",
                    controller: "CreditCtrl",
                    controllerAs: "vm"
                })
                .otherwise({ redirectTo: "/" });
        }
    ]);

    app.run([
        "$rootScope", "$timeout", "$location", "identityService", function($rootScope, $timeout, $location, identityService) {

            $rootScope.$on("$routeChangeError", function(event, current) {
                identityService.createAccessDeniedResponse(current);
                $location.path("/account/login");
            });

            $rootScope.$on("$locationChangeStart", function() {
                if (identityService.getAccessToken()) {
                    identityService.getUserInfo().success(function(result) {
                        if (result.email) {
                            identityService.setAuthorizedUserData(result);
                        }
                    }).error(function() {
                        identityService.clearAuthorizedUserData();
                    });
                }
            });

            $rootScope.$on("$routeChangeStart", function() {
                $("body").css("opacity", "0.3");
            });
            $rootScope.$on("$viewContentLoaded", function() {
                $("body").css("opacity", "1");
                $(document).foundation();
            });
        }
    ]);
})(angular.module("core"));