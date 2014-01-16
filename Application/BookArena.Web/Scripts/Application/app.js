﻿"use strict";

(function() {
    var routes =
    {
        "/": {
            templateUrl: "Template/Home/Welcome.html",
            controller: "WelcomeCtrl",
            requireLogin: false
        },
        "/account/login": {
            templateUrl: "Template/Account/Login.html",
            controller: "LoginCtrl",
            requireLogin: false
        },
        "/book": {
            templateUrl: "Template/Book/List.html",
            controller: "BookListCtrl",
            requireLogin: false
        },
        "/book/details/:id": {
            templateUrl: "Template/Book/Details.html",
            controller: "BookDetailsCtrl",
            requireLogin: false
        },
        "/book/upload": {
            templateUrl: "Template/Book/Upload.html",
            controller: "BookUploadCtrl",
            requireLogin: true
        },
        "/student": {
            templateUrl: "Template/Student/List.html",
            controller: "StudentListCtrl",
            requireLogin: false
        },
        "student/register": {
            templateUrl: "Template/Student/Register.html",
            controller: "StudentRegisterCtrl",
            requireLogin: true
        }
    };
    var bookArenaApp = angular.module("bookArenaApp", ["ngRoute", "ngAnimate"]);

    bookArenaApp.config(["$routeProvider", function($routeProvider) {
        for (var path in routes) {
            $routeProvider.when(path, routes[path]);
        }
        $routeProvider.otherwise({ redirectTo: "/" });
    }]);

    bookArenaApp.run(["$rootScope", "$location", "apiService", function($rootScope, $location, service) {
        $rootScope.$on("$locationChangeSuccess", function(event, next) {
            for (var i in routes) {
                if (next.indexOf(i) != -1) {
                    if (routes[i].requireLogin) {
                        service.call("/account/isauthenticated").then(function(data) {
                            if (data !== "true") {
                                $location.path("/account/login");
                            }
                        });
                    }
                }
            }
        });
    }]);
})();