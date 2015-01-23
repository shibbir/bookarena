(function() {
    "use strict";

    var app = angular.module("bookArena", ["ngRoute"]);

    app.config([
        "$routeProvider", function($routeProvider) {
            var authCheck = {
                auth: function($q, identityService) {
                    var defer = $q.defer();
                    if (!identityService.isLoggedIn()) {
                        defer.reject();
                    } else {
                        defer.resolve();
                    }
                    return defer.promise;
                }
            };
            $routeProvider
                .when("/", {
                    templateUrl: "Templates/Home/Welcome.html",
                    controller: "HomeCtrl"
                })
                .when(
                    "/account/login", {
                        templateUrl: "Templates/Account/Login.html",
                        controller: "LoginCtrl"
                    })
                .when(
                    "/info", {
                        templateUrl: "Templates/Account/Profile.html",
                        controller: "ProfileCtrl"
                    })
                .when(
                    "/transactions", {
                        templateUrl: "Templates/Transaction/List.html",
                        controller: "TransactionCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/transactions/:transactionId", {
                        templateUrl: "Templates/Transaction/Details.html",
                        controller: "TransactionCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/books", {
                        templateUrl: "Templates/Book/List.html",
                        controller: "BookListCtrl"
                    })
                .when(
                    "/books/add", {
                        templateUrl: "Templates/Book/Add.html",
                        controller: "BookAddCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/books/:id", {
                        templateUrl: "Templates/Book/Details.html",
                        controller: "BookDetailsCtrl"
                    })
                .when(
                    "/books/edit/:id", {
                        templateUrl: "Templates/Book/Edit.html",
                        controller: "BookEditCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/categories/:categoryId/books", {
                        templateUrl: "Templates/Book/List.html",
                        controller: "BookListCtrl"
                    })
                .when(
                    "/categories", {
                        templateUrl: "Templates/Category/List.html",
                        controller: "CategoryCtrl"
                    })
                .when(
                    "/students", {
                        templateUrl: "Templates/Student/List.html",
                        controller: "StudentListCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/students/page/:pageNumber", {
                        templateUrl: "Templates/Student/List.html",
                        controller: "StudentListCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/students/add", {
                        templateUrl: "Templates/Student/Add.html",
                        controller: "StudentAddCtrl",
                        resolve: authCheck
                    }).when(
                    "/students/:id", {
                        templateUrl: "Templates/Student/Details.html",
                        controller: "StudentDetailsCtrl",
                        resolve: authCheck
                    })
                .when(
                    "/students/edit/:id", {
                        templateUrl: "Templates/Student/Edit.html",
                        controller: "StudentEditCtrl",
                        resolve: authCheck
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
})();