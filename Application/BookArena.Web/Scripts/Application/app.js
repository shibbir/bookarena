"use strict";

(function() {
    var bookArenaApp = angular.module("bookArenaApp", ["ngRoute"]);

    bookArenaApp.config(["$routeProvider", function($routeProvider) {
        $routeProvider.when("/", {
            templateUrl: "Template/Book/Index.html",
            controller: "BookCtrl"
        }).when("/book/details/:id", {
            templateUrl: "Template/Book/Details.html",
            controller: "BookDetailsCtrl"
        }).when("/book/upload", {
            templateUrl: "Template/Book/Upload.html",
            controller: "BookUploadCtrl"
        }).when("/student", {
            templateUrl: "Template/Student/Index.html",
            controller: "StudentCtrl"
        }).otherwise({
            redirectTo: "/"
        });
    }]);
})();