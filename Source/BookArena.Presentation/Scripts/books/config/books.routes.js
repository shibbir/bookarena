"use strict";

angular.module("books").config([
    "$routeProvider", function($routeProvider) {
        var authCheck = {
            auth: [
                "$q", "identityService", function($q, identityService) {
                    var defer = $q.defer();
                    if (!identityService.isLoggedIn()) {
                        defer.reject();
                    } else {
                        defer.resolve();
                    }
                    return defer.promise;
                }
            ]
        };
        $routeProvider
            .when("/books", {
                templateUrl: "Scripts/books/views/list.books.view.html",
                controller: "BookListCtrl",
                controllerAs: "vm"
            })
            .when("/books/add", {
                templateUrl: "Scripts/books/views/add.book.view.html",
                controller: "BookAddCtrl",
                controllerAs: "vm",
                resolve: authCheck
            })
            .when("/books/:id", {
                templateUrl: "Scripts/books/views/show.book.view.html",
                controller: "BookDetailsCtrl",
                controllerAs: "vm"
            })
            .when("/books/edit/:id", {
                templateUrl: "Scripts/books/views/edit.book.view.html",
                controller: "BookEditCtrl",
                controllerAs: "vm",
                resolve: authCheck
            })
            .when("/categories/:categoryId/books", {
                templateUrl: "Scripts/books/views/list.books.view.html",
                controller: "BookListCtrl",
                controllerAs: "vm"
            })
            .when("/categories", {
                templateUrl: "Scripts/books/views/list.categories.view.html",
                controller: "CategoryCtrl",
                controllerAs: "vm"
            });
    }
]);