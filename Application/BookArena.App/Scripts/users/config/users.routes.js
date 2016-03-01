"use strict";

angular.module("users").config([
    "$routeProvider", function($routeProvider) {
        $routeProvider
            .when("/account/login", {
                templateUrl: "Scripts/users/views/login.user.view.html",
                controller: "LoginCtrl",
                controllerAs: "vm"
            })
            .when("/account/logout", {
                template: "",
                controller: "LogoutCtrl"
            });
    }
]);