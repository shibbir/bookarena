"use strict";

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
        "/books": {
            templateUrl: "Template/Book/List.html",
            controller: "BookListCtrl",
            requireLogin: false
        },
        "/books/book/:id": {
            templateUrl: "Template/Book/Details.html",
            controller: "BookDetailsCtrl",
            requireLogin: false
        },
        "/books/edit/:id": {
            templateUrl: "Template/Book/Edit.html",
            controller: "BookEditCtrl",
            requireLogin: true
        },
        "/books/add": {
            templateUrl: "Template/Book/Add.html",
            controller: "BookAddCtrl",
            requireLogin: true
        },
        "/students": {
            templateUrl: "Template/Student/List.html",
            controller: "StudentListCtrl",
            requireLogin: false
        },
        "/students/add": {
            templateUrl: "Template/Student/Add.html",
            controller: "StudentAddCtrl",
            requireLogin: true
        }
    };
    var bookArenaApp = angular.module("bookArenaApp", ["ngRoute", "ngAnimate"]);

    bookArenaApp.config([
        "$routeProvider", function($routeProvider) {
            for (var path in routes) {
                $routeProvider.when(path, routes[path]);
            }
            $routeProvider.otherwise({ redirectTo: "/" });
        }
    ]);

    bookArenaApp.run([
        "$rootScope", "$location", "notifierService", function($rootScope, $location, notifier) {
            var blackList = ["add", "edit", "delete"];
            $rootScope.$on("$locationChangeStart", function(event, next) {
                for (var i in routes) {
                    if (next.indexOf(i) != -1) {
                        if (routes[i].requireLogin && !$rootScope.authenticatedUser.isAuthenticated) {
                            notifier.notify({ responseType: "error", message: "Access Denied! You need to login first." });
                            event.preventDefault();
                        }
                    }
                }
            });
        }
    ]);
})();